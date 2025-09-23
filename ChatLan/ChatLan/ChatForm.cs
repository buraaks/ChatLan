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

        public ChatForm(bool isHost, string ipAddress)
        {
            InitializeComponent();
            this.isHost = isHost;
            this.ipAddress = ipAddress;

            Text = isHost ? $"Sunucu Modu - {ipAddress}" : $"İstemci Modu - {ipAddress}";

            if (isHost)
                _ = StartServerAsync();
            else
                _ = StartClientAsync();
        }

        private async Task StartServerAsync()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 5000);
                listener.Start();
                AddMessage("Sunucu dinliyor...");

                client = await listener.AcceptTcpClientAsync();
                AddMessage("İstemci bağlandı.");

                SetupStream();
                _ = ReceiveMessagesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sunucu başlatılamadı: " + ex.Message);
            }
        }

        private async Task StartClientAsync()
        {
            try
            {
                client = new TcpClient();
                await client.ConnectAsync(ipAddress, 5000);
                AddMessage("Sunucuya bağlanıldı.");

                SetupStream();
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
                    string? message = await reader!.ReadLineAsync();
                    if (message != null)
                    {
                        string timestamp = DateTime.Now.ToString("HH:mm:ss");
                        AddMessage($"[{timestamp}] Karşı taraf: {message}");
                    }
                }
            }
            catch
            {
                AddMessage("Bağlantı kesildi.");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                string timestamp = DateTime.Now.ToString("HH:mm:ss");
                AddMessage($"[{timestamp}] Siz: {message}");

                writer?.WriteLine(message);
                txtMessage.Clear();
            }
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
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
            {
                Invoke(new Action(() => lstChat.Items.Add(msg)));
            }
            else
            {
                lstChat.Items.Add(msg);
            }
        }
    }
}
