using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace LanChatApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string ip = rbHost.Checked ? txtIPNiz.Text : txtIPGirin.Text;
            bool isHost = rbHost.Checked;
            string nick = txtNick.Text.Trim();

            if (string.IsNullOrEmpty(nick))
                nick = isHost ? "Kurucu" : "Kullanýcý";

            ChatForm chatForm = new ChatForm(isHost, ip, nick);
            chatForm.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtIPNiz.Enabled = true;
            txtIPGirin.Enabled = false;
            HostCopy.Enabled = true;
            HostPaste.Enabled = true;
            ClientCopy.Enabled = false;
            ClientPaste.Enabled = false;
        }

        private void rbHost_CheckedChanged(object sender, EventArgs e)
        {
            txtIPNiz.Enabled = true;
            txtIPGirin.Enabled = false;
            otoIP.Enabled = true;
            HostCopy.Enabled = true;
            HostPaste.Enabled = true;
            ClientCopy.Enabled = false;
            ClientPaste.Enabled = false;
        }

        private void rbClient_CheckedChanged(object sender, EventArgs e)
        {
            txtIPNiz.Enabled = false;
            txtIPGirin.Enabled = true;
            otoIP.Enabled = false;
            HostCopy.Enabled = false;
            HostPaste.Enabled = false;
            ClientCopy.Enabled = true;
            ClientPaste.Enabled = true;
        }

        private string GetLocalIPAddress()
        {
            try
            {
                string localIP = "";
                foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                        break;
                    }
                }
                return localIP;
            }
            catch (Exception ex)
            {
                MessageBox.Show("IP alýnamadý: " + ex.Message);
                return "";
            }
        }

        private void otoIP_Click(object sender, EventArgs e)
        {
            txtIPNiz.Text = GetLocalIPAddress();
        }

        private void HostCopy_Click(object sender, EventArgs e)
        {
            string toCopy = txtIPNiz.SelectedText.Length > 0 ? txtIPNiz.SelectedText : txtIPNiz.Text;
            if (!string.IsNullOrEmpty(toCopy))
                Clipboard.SetText(toCopy);
        }

        private void HostPaste_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
                txtIPNiz.Text = Clipboard.GetText();
        }

        private void ClientCopy_Click(object sender, EventArgs e)
        {
            string toCopy = txtIPGirin.SelectedText.Length > 0 ? txtIPGirin.SelectedText : txtIPGirin.Text;
            if (!string.IsNullOrEmpty(toCopy))
                Clipboard.SetText(toCopy);
        }

        private void ClientPaste_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
                txtIPGirin.Text = Clipboard.GetText();
        }
    }
}
