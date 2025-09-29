using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace LanChatApp
{
    public partial class Form1 : Form
    {
        public event Action<bool, string, string>? FormSwitch;
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

            FormSwitch?.Invoke(isHost, ip, nick);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //Koyu Tema
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

            // Sabit label'lar
            label1.BackColor = SystemColors.Control;
            label1.ForeColor = SystemColors.ControlText;

            lblClientIP.BackColor = SystemColors.Control;
            lblClientIP.ForeColor = SystemColors.ControlText;
        }

        private void ApplyDarkTheme()
        {
            this.BackColor = Color.FromArgb(45, 45, 48); // koyu gri
            this.ForeColor = Color.White;

            foreach (Control ctrl in this.Controls)
            {
                ctrl.BackColor = Color.FromArgb(28, 28, 28);
                ctrl.ForeColor = Color.White;

                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.FromArgb(63, 63, 70);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = Color.Gray;
                }
                else if (ctrl is RadioButton rb)
                {
                    rb.BackColor = Color.FromArgb(45, 45, 48);
                }
            }

            // Sabit label'lar
            label1.BackColor = Color.FromArgb(45, 45, 48);
            label1.ForeColor = Color.White;

            lblClientIP.BackColor = Color.FromArgb(45, 45, 48);
            lblClientIP.ForeColor = Color.White;
        }

        private bool isDarkTheme = false;


        private void button1_Click_1(object sender, EventArgs e)
        {
            if (isDarkTheme)
            {
                ApplyLightTheme();
                isDarkTheme = false;
            }
            else
            {
                ApplyDarkTheme();
                isDarkTheme = true;
            }
        }
    }
}
