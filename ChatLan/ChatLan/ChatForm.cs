using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LanChatApp
{
    public partial class ChatForm : Form
    {
        private readonly bool isHost;
        private readonly string ipAddress;

        private TcpListener? listener;
        private TcpClient? client;
        private StreamReader? reader;
        private StreamWriter? writer;

        private string HostNick = "Kurucu";
        private string ClientNick = "Kullanıcı";

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
            nickBox.Text = HostNick;
            try
            {
                listener = new TcpListener(IPAddress.Any, 5000);
                listener.Start();
                AddMessage("Sunucu dinliyor...");

                client = await listener.AcceptTcpClientAsync();
                SetupStream();

                // Client bağlandığını hem kendi pencereye hem client'a ilet
                AddMessage($"{ClientNick} bağlandı.");
                writer?.WriteLine($"System|{ClientNick} bağlandı.");

                _ = ReceiveMessagesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sunucu başlatılamadı: " + ex.Message);
            }
        }

        private async Task StartClientAsync()
        {
            nickBox.Text = ClientNick;
            try
            {
                client = new TcpClient();
                await client.ConnectAsync(ipAddress, 5000);
                SetupStream();

                // Sunucuya bağlandığını hem kendi pencereye hem sunucuya ilet
                AddMessage($"Sunucuya bağlandı.");
                writer?.WriteLine($"System|{ClientNick} bağlandı.");

                _ = ReceiveMessagesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı hatası: " + ex.Message);
            }
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
                    if (data == null)
                    {
                        string who = isHost ? "İstemci" : "Sunucu";
                        AddMessage($"{who} bağlantıyı kapattı.");
                        break;
                    }

                    string[] parts = data.Split('|', 2);
                    if (parts.Length == 2)
                    {
                        string senderName = parts[0];
                        string message = parts[1];

                        string timestamp = DateTime.Now.ToString("HH:mm:ss");
                        if (senderName == "System")
                            AddMessage($"[{timestamp}] {message}");
                        else
                            AddMessage($"[{timestamp}] {senderName}: {message}");
                    }
                }
            }
            catch (Exception ex)
            {
                AddMessage("Bağlantı hatası: " + ex.Message);
            }
            finally
            {
                reader?.Close();
                writer?.Close();
                client?.Close();
                listener?.Stop();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                string senderName = isHost ? HostNick : ClientNick;
                writer?.WriteLine(senderName + "|" + message);
                string timestamp = DateTime.Now.ToString("HH:mm:ss");
                AddMessage($"[{timestamp}] {senderName}: {message}");
                txtMessage.Clear();
            }
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Ayrılma mesajı gönder
                if (isHost)
                    writer?.WriteLine($"System|Sunucu kapandı.");
                else
                    writer?.WriteLine($"System|{ClientNick} ayrıldı.");

                writer?.Close();
                reader?.Close();
                client?.Close();
                listener?.Stop();
            }
            catch { }
        }

        private void AddMessage(string msg)
        {
            if (InvokeRequired)
                Invoke(new Action(() => lstChat.Items.Add(msg)));
            else
                lstChat.Items.Add(msg);
        }

        private void outBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (isHost)
                    writer?.WriteLine($"System|Sunucu kapandı.");
                else
                    writer?.WriteLine($"System|{ClientNick} ayrıldı.");

                reader?.Close();
                writer?.Close();
                client?.Close();
                listener?.Stop();
            }
            catch { }

            Application.Exit();
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde nickBox'ı güncelle
            try
            {
                nickBox.Text = isHost ? HostNick : ClientNick;
            }
            catch { /* Güvenlik için sessiz yakala */ }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newNick = nickBox.Text.Trim();
            if (string.IsNullOrEmpty(newNick)) return;

            if (isHost)
            {
                HostNick = newNick;
                AddMessage($"[Sistem] Kurucu adı '{HostNick}' olarak ayarlandı.");
                // Bağlıysa karşı tarafa bilgi gönder (gönderici null olabilir, kontrol et)
                try { writer?.WriteLine($"System|Kurucu ismini '{HostNick}' olarak güncelledi."); } catch { }
            }
            else
            {
                ClientNick = newNick;
                AddMessage($"[Sistem] Kullanıcı adı '{ClientNick}' olarak ayarlandı.");
                try { writer?.WriteLine($"System|Kullanıcı ismini '{ClientNick}' olarak güncelledi."); } catch { }
            }
        }

    }
}
