using System;
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
    }
}
