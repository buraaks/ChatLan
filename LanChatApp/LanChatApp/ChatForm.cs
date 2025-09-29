using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using LanChatApp.Models;
using System.Collections.Concurrent;


namespace LanChatApp
{
    public partial class ChatForm : Form
    {
        private bool isHost;
        private string ipAddress;
        private string HostNick = "";
        private string ClientNick = "";
        private TcpListener? listener;
        private TcpClient? client;
        private List<TcpClient> clients = new();
        private StreamReader? reader;
        private StreamWriter? writer;
        private readonly ConcurrentDictionary<string, SortedDictionary<int, byte[]>> fileChunkBuffer
            = new();
        private readonly ConcurrentDictionary<string, int> fileChunkReceivedCount
            = new();

        public ChatForm(bool isHost, string ipAddress, string nick)
        {
            InitializeComponent();
            this.isHost = isHost;
            this.ipAddress = ipAddress;

            if (isHost)
                HostNick = string.IsNullOrEmpty(nick) ? "Kurucu" : nick;
            else
                ClientNick = string.IsNullOrEmpty(nick) ? "Kullanıcı" : nick;

            Text = isHost ? $"Sunucu Modu - {ipAddress}" : $"İstemci Modu - {ipAddress}";
            nickBox.Text = nick;

            if (isHost)
                _ = StartServerAsync();
            else
                _ = StartClientAsync();
        }

        private async Task StartServerAsync()
        {
            listener = new TcpListener(IPAddress.Any, 5000);
            listener.Start();
            AddMessage("Sunucu dinliyor...");

            while (true)
            {
                TcpClient newClient = await listener.AcceptTcpClientAsync();
                clients.Add(newClient);
                _ = HandleClientAsync(newClient);
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                using var reader = new StreamReader(client.GetStream());
                while (true)
                {
                    string? data = await reader.ReadLineAsync();
                    if (data == null) break;

                    // Sunucu kendi ekranına gelen mesajı yansıt (dosyada otomatik indirme yok)
                    LanChatApp.Models.ChatMessage? msg = JsonSerializer.Deserialize<LanChatApp.Models.ChatMessage>(data);
                    if (msg != null)
                    {
                        string ts = string.IsNullOrWhiteSpace(msg.Timestamp)
                            ? DateTime.Now.ToString("HH:mm:ss")
                            : msg.Timestamp;

                        if (msg.Type == "chat")
                            AddMessage($"[{ts}] {msg.Sender}: {msg.Message}");
                        else if (msg.Type == "system")
                            AddMessage($"[{ts}] {msg.Message}");
                        else if (msg.Type == "file" && msg.FileContentBase64 != null)
                        {
                            lstChat.Items.Add(new LanChatApp.Models.ChatListItem
                            {
                                Text = $"[{ts}] {msg.Sender} 📎 {msg.Message} gönderdi (indirmek için çift tıkla)",
                                FileName = msg.Message,
                                FileContentBase64 = msg.FileContentBase64
                            });
                        }
                    }

                    // Tüm istemcilere ilet
                    BroadcastJson(data);
                }
            }
            catch (Exception ex)
            {
                AddMessage("İstemci bağlantı hatası: " + ex.Message);
            }
            finally
            {
                clients.Remove(client);
                SendSystemMessage("Bir istemci ayrıldı.");
            }
        }

        private async Task StartClientAsync()
        {
            client = new TcpClient();
            await client.ConnectAsync(ipAddress, 5000);
            SetupStream();
            _ = ReceiveMessagesAsync();

            SendSystemMessage($"{ClientNick} bağlandı.");
        }

        private void SetupStream()
        {
            if (client == null) return;
            var stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };
        }

        private async Task ReceiveMessagesAsync()
        {
            try
            {
                while (true)
                {
                    string? data = await reader!.ReadLineAsync();
                    if (data == null) break;

                    ChatMessage? msg = JsonSerializer.Deserialize<ChatMessage>(data);
                    if (msg == null) continue;

                    string ts = string.IsNullOrWhiteSpace(msg.Timestamp)
                        ? DateTime.Now.ToString("HH:mm:ss")
                        : msg.Timestamp;

                    if (msg.Type == "chat")
                    {
                        AddMessage($"[{ts}] {msg.Sender}: {msg.Message}");
                    }
                    else if (msg.Type == "system")
                    {
                        AddMessage($"[{ts}] {msg.Message}");
                    }
                    else if (msg.Type == "file-chunk" && msg.FileContentBase64 != null && msg.ChunkIndex.HasValue && msg.TotalChunks.HasValue)
                    {
                        string key = $"{msg.Sender}|{msg.Message}";

                        // Parça havuzu oluştur
                        var pool = fileChunkBuffer.GetOrAdd(key, _ => new SortedDictionary<int, byte[]>());

                        // Parçayı ekle
                        if (!pool.ContainsKey(msg.ChunkIndex.Value))
                        {
                            pool[msg.ChunkIndex.Value] = Convert.FromBase64String(msg.FileContentBase64);
                        }

                        // İlerleme bilgisi göster (isteğe bağlı)
                        AddMessage($"[{ts}] {msg.Sender} → {msg.Message} parça {msg.ChunkIndex + 1}/{msg.TotalChunks}");

                        // Tüm parçalar geldiyse birleştir
                        if (pool.Count == msg.TotalChunks.Value)
                        {
                            using var ms = new MemoryStream();
                            foreach (var kv in pool)
                            {
                                ms.Write(kv.Value, 0, kv.Value.Length);
                            }

                            string fullBase64 = Convert.ToBase64String(ms.ToArray());

                            lstChat.Items.Add(new ChatListItem
                            {
                                Text = $"[{ts}] {msg.Sender} 📎 {msg.Message} (indirmek için çift tıkla)",
                                FileName = msg.Message,
                                FileContentBase64 = fullBase64
                            });

                            AddMessage($"[{ts}] {msg.Message} dosya alımı tamamlandı.");

                            fileChunkBuffer.TryRemove(key, out _);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AddMessage("Bağlantı hatası: " + ex.Message);
            }
        }
        

        private void BroadcastJson(string json)
        {
            foreach (var c in clients)
            {
                try
                {
                    var writer = new StreamWriter(c.GetStream()) { AutoFlush = true };
                    writer.WriteLine(json);
                }
                catch { }
            }
        }

        private void SendSystemMessage(string text)
        {
            var msg = new ChatMessage
            {
                Type = "system",
                Sender = "System",
                Message = text,
                Timestamp = DateTime.Now.ToString("HH:mm:ss")
            };

            string json = JsonSerializer.Serialize(msg);
            if (isHost)
                BroadcastJson(json);
            else
                writer?.WriteLine(json);

            AddMessage($"[{msg.Timestamp}] {msg.Message}");
        }

        private void AddMessage(string msg)
        {
            if (InvokeRequired)
                Invoke(new Action(() => lstChat.Items.Add(msg)));
            else
                lstChat.Items.Add(msg);
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Title = "Gönderilecek dosyayı seç",
                Filter = "Tüm Dosyalar|*.*"
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            string filePath = ofd.FileName;
            const int ChunkSize = 64 * 1024; // 64 KB parçalar
            string fileName = Path.GetFileName(filePath);
            string senderName = isHost ? HostNick : ClientNick;

            // Dosya boyutunu öğren
            long fileLength = new FileInfo(filePath).Length;
            int totalChunks = (int)Math.Ceiling((double)fileLength / ChunkSize);

            using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            byte[] buffer = new byte[ChunkSize];
            int bytesRead;
            int chunkIndex = 0;

            while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
            {
                byte[] actualData = new byte[bytesRead];
                Array.Copy(buffer, actualData, bytesRead);

                var msg = new ChatMessage
                {
                    Type = "file-chunk",
                    Sender = senderName,
                    Message = fileName,
                    Timestamp = DateTime.Now.ToString("HH:mm:ss"),
                    FileContentBase64 = Convert.ToBase64String(actualData),
                    ChunkIndex = chunkIndex,
                    TotalChunks = totalChunks
                };

                string json = JsonSerializer.Serialize(msg);

                if (isHost)
                    BroadcastJson(json);   // Sunucu tüm istemcilere yollar
                else
                    writer?.WriteLine(json); // İstemci sunucuya yollar

                chunkIndex++;
            }

            // Gönderenin ekranına bilgi mesajı
            lstChat.Items.Add(new ChatListItem
            {
                Text = $"[{DateTime.Now:HH:mm:ss}] {senderName} 📎 {fileName} gönderildi ({totalChunks} parça)",
                FileName = fileName
            });
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                btnSend_Click(this, EventArgs.Empty);
            }
        }

        private void buttonChangeNick_Click(object sender, EventArgs e)
        {
            string newNick = nickBox.Text.Trim();
            if (string.IsNullOrEmpty(newNick)) return;

            if (isHost)
            {
                HostNick = newNick;
                AddMessage($"[Sistem] Kurucu adı '{HostNick}' olarak ayarlandı.");
                SendSystemMessage($"Kurucu ismini '{HostNick}' olarak güncelledi.");
            }
            else
            {
                ClientNick = newNick;
                AddMessage($"[Sistem] Kullanıcı adı '{ClientNick}' olarak ayarlandı.");
                SendSystemMessage($"Kullanıcı ismini '{ClientNick}' olarak güncelledi.");
            }
        }

        private bool isDarkTheme = false;
        private void ApplyDarkTheme()
        {
            this.BackColor = Color.FromArgb(25, 25, 30);
            this.ForeColor = Color.White;

            foreach (Control ctrl in this.Controls)
            {
                switch (ctrl)
                {
                    case TextBox or RichTextBox or ListBox:
                        ctrl.BackColor = Color.FromArgb(40, 40, 50);
                        ctrl.ForeColor = Color.White;
                        break;
                    case Button btn:
                        btn.BackColor = Color.FromArgb(60, 60, 70);
                        btn.ForeColor = Color.White;
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderSize = 0;
                        break;
                    case Label lbl:
                        lbl.ForeColor = Color.Gainsboro;
                        break;
                    case RadioButton rb:
                        rb.ForeColor = Color.White;
                        rb.BackColor = Color.FromArgb(25, 25, 30);
                        break;
                }
            }
        }

        private void ApplyLightTheme()
        {
            this.BackColor = SystemColors.Control;
            this.ForeColor = SystemColors.ControlText;

            foreach (Control ctrl in this.Controls)
            {
                ctrl.BackColor = SystemColors.Window;
                ctrl.ForeColor = SystemColors.ControlText;

                if (ctrl is Button btn)
                {
                    btn.BackColor = SystemColors.Control;
                    btn.FlatStyle = FlatStyle.Standard;
                }
                else if (ctrl is RadioButton rb)
                {
                    rb.BackColor = SystemColors.Control;
                }
            }
        }

        private void outBtn_Click(object sender, EventArgs e)
        {
            SendSystemMessage(isHost ? "Sunucu kapandı." : $"{ClientNick} ayrıldı.");
            reader?.Close();
            writer?.Close();
            client?.Close();
            listener?.Stop();
            this.Close();
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SendSystemMessage(isHost ? "Sunucu kapandı." : $"{ClientNick} ayrıldı.");
            writer?.Close();
            reader?.Close();
            client?.Close();
            listener?.Stop();
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            nickBox.Text = isHost ? HostNick : ClientNick;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text.Trim();

            if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show("Lütfen bir mesaj girin.");
                return;
            }

            if (message.Length > 1000)
            {
                MessageBox.Show("Mesaj çok uzun. Lütfen 1000 karakterden kısa tutun.");
                return;
            }

            var msg = new ChatMessage
            {
                Type = "chat",
                Sender = isHost ? HostNick : ClientNick,
                Message = message,
                Timestamp = DateTime.Now.ToString("HH:mm:ss")
            };

            string json = JsonSerializer.Serialize(msg);

            try
            {
                // 🔹 Sunucu tüm istemcilere yollar, istemci sadece sunucuya yollar
                if (isHost)
                    BroadcastJson(json);
                else
                    writer?.WriteLine(json);

                // 🔹 Gönderen kendi ekranında da görsün
                AddMessage($"[{msg.Timestamp}] {msg.Sender}: {msg.Message}");

                txtMessage.Clear();
            }
            catch (Exception ex)
            {
                AddMessage("Mesaj gönderilemedi: " + ex.Message);
            }
        }
        private void btnToggleThema_Click(object sender, EventArgs e)
        {
            isDarkTheme = !isDarkTheme;

            if (isDarkTheme)
            {
                ApplyDarkTheme();
                AddMessage("[Sistem] Karanlık tema etkinleştirildi.");
            }
            else
            {
                ApplyLightTheme();
                AddMessage("[Sistem] Açık tema etkinleştirildi.");
            }
        }
        private void button1_Click(object sender, EventArgs e) //bu aslında btnChangeNick_Click 
        {
            string newNick = nickBox.Text.Trim();
            if (string.IsNullOrEmpty(newNick))
            {
                MessageBox.Show("Lütfen geçerli bir isim girin.");
                return;
            }

            if (isHost)
            {
                HostNick = newNick;
                AddMessage($"[Sistem] Kurucu adı '{HostNick}' olarak ayarlandı.");
                SendSystemMessage($"Kurucu ismini '{HostNick}' olarak güncelledi.");
            }
            else
            {
                ClientNick = newNick;
                AddMessage($"[Sistem] Kullanıcı adı '{ClientNick}' olarak ayarlandı.");
                SendSystemMessage($"Kullanıcı ismini '{ClientNick}' olarak güncelledi.");
            }
        }

        private void lstChat_DoubleClick(object sender, EventArgs e)
        {
            if (lstChat.SelectedItem is ChatListItem item && item.FileContentBase64 != null)
            {
                try
                {
                    using var sfd = new SaveFileDialog
                    {
                        FileName = item.FileName,
                        Title = "Dosyayı kaydet"
                    };

                    if (sfd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(sfd.FileName))
                    {
                        byte[] bytes = Convert.FromBase64String(item.FileContentBase64);
                        File.WriteAllBytes(sfd.FileName, bytes);
                        AddMessage($"[Sistem] Dosya kaydedildi: {sfd.FileName}");
                    }
                }
                catch (Exception ex)
                {
                    AddMessage($"Dosya kaydedilemedi: {ex.Message}");
                }
            }
        }
    }
}