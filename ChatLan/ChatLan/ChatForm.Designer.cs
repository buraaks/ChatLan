namespace LanChatApp
{
    partial class ChatForm
    {
        private System.ComponentModel.IContainer components = null;
        private ListBox lstChat;
        private TextBox txtMessage;
        private Button btnSend;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lstChat = new ListBox();
            txtMessage = new TextBox();
            btnSend = new Button();
            SuspendLayout();
            // 
            // lstChat
            // 
            lstChat.Location = new Point(20, 20);
            lstChat.Name = "lstChat";
            lstChat.Size = new Size(400, 199);
            lstChat.TabIndex = 0;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(20, 230);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(300, 23);
            txtMessage.TabIndex = 1;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(330, 230);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(90, 23);
            btnSend.TabIndex = 2;
            btnSend.Text = "Gönder";
            btnSend.Click += btnSend_Click;
            // 
            // ChatForm
            // 
            ClientSize = new Size(450, 280);
            Controls.Add(lstChat);
            Controls.Add(txtMessage);
            Controls.Add(btnSend);
            Name = "ChatForm";
            Text = "LAN Chat";
            FormClosing += ChatForm_FormClosing;
            Load += ChatForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
