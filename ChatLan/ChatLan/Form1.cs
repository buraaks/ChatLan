using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.JavaScript;
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

            ChatForm chatForm = new ChatForm(isHost, ip);
            chatForm.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtIPNiz.Enabled = true;
            txtIPGirin.Enabled = false;
        }

        private void rbHost_CheckedChanged(object sender, EventArgs e)
        {
            txtIPNiz.Enabled = true;
            txtIPGirin.Enabled = false;
            otoIP.Enabled = true;
        }

        private void rbClient_CheckedChanged(object sender, EventArgs e)
        {
            txtIPNiz.Enabled = false;
            txtIPGirin.Enabled = true;
            otoIP.Enabled = false;
        }

        private string GetLocalIPAddress()
        {
            try
            {
                string localIP = "";
                foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork) // IPv4
                    {
                        localIP = ip.ToString();
                        break;
                    }
                }
                return localIP;
            }
            catch (Exception ex)
            {
                MessageBox.Show("IP alınamadı: " + ex.Message);
                return "";
            }
        }

        private void otoIP_Click(object sender, EventArgs e)
        {
            string ip = GetLocalIPAddress();
            txtIPNiz.Text = ip;
        }
    }
}
