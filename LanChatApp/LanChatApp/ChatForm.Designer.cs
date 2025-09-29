namespace LanChatApp
{
    partial class ChatForm
    {
        private System.ComponentModel.IContainer components = null;

        private ListBox lstChat;
        private RichTextBox txtMessage;
        private Button btnSend;
        private Button btnSendFile;
        private TextBox nickBox;
        private Button btnChangeNick;
        private Label lblNick;
        private Button btnDisconnect;
        private Button btnToggleTheme;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lstChat = new ListBox();
            txtMessage = new RichTextBox();
            btnSend = new Button();
            btnSendFile = new Button();
            nickBox = new TextBox();
            btnChangeNick = new Button();
            lblNick = new Label();
            btnDisconnect = new Button();
            btnToggleTheme = new Button();
            SuspendLayout();
            // 
            // lstChat
            // 
            lstChat.Location = new Point(12, 60);
            lstChat.Name = "lstChat";
            lstChat.Size = new Size(630, 199);
            lstChat.TabIndex = 0;
            lstChat.DoubleClick += lstChat_DoubleClick;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(12, 270);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(630, 50);
            txtMessage.TabIndex = 1;
            txtMessage.Text = "";
            txtMessage.KeyDown += txtMessage_KeyDown;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(12, 330);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(98, 25);
            btnSend.TabIndex = 2;
            btnSend.Text = "Gönder";
            btnSend.Click += btnSend_Click;
            // 
            // btnSendFile
            // 
            btnSendFile.Location = new Point(116, 330);
            btnSendFile.Name = "btnSendFile";
            btnSendFile.Size = new Size(98, 25);
            btnSendFile.TabIndex = 3;
            btnSendFile.Text = "Dosya Gönder";
            btnSendFile.Click += btnSendFile_Click;
            // 
            // nickBox
            // 
            nickBox.Location = new Point(60, 20);
            nickBox.Name = "nickBox";
            nickBox.Size = new Size(135, 23);
            nickBox.TabIndex = 3;
            // 
            // btnChangeNick
            // 
            btnChangeNick.Location = new Point(210, 20);
            btnChangeNick.Name = "btnChangeNick";
            btnChangeNick.Size = new Size(90, 23);
            btnChangeNick.TabIndex = 4;
            btnChangeNick.Text = "Değiştir";
            btnChangeNick.Click += button1_Click;
            // 
            // lblNick
            // 
            lblNick.AutoSize = true;
            lblNick.Location = new Point(12, 23);
            lblNick.Name = "lblNick";
            lblNick.Size = new Size(32, 15);
            lblNick.TabIndex = 5;
            lblNick.Text = "İsim:";
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(310, 20);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(110, 23);
            btnDisconnect.TabIndex = 6;
            btnDisconnect.Text = "Sunucudan Ayrıl";
            btnDisconnect.Click += outBtn_Click;
            // 
            // btnToggleTheme
            // 
            btnToggleTheme.Location = new Point(523, 326);
            btnToggleTheme.Name = "btnToggleTheme";
            btnToggleTheme.Size = new Size(119, 25);
            btnToggleTheme.TabIndex = 7;
            btnToggleTheme.Text = "Karanlık tema (beta)";
            btnToggleTheme.Click += btnToggleThema_Click;
            // 
            // ChatForm
            // 
            ClientSize = new Size(654, 375);
            Controls.Add(lstChat);
            Controls.Add(txtMessage);
            Controls.Add(btnSend);
            Controls.Add(btnSendFile);
            Controls.Add(nickBox);
            Controls.Add(btnChangeNick);
            Controls.Add(lblNick);
            Controls.Add(btnDisconnect);
            Controls.Add(btnToggleTheme);
            Name = "ChatForm";
            Text = "LAN Chat";
            FormClosing += ChatForm_FormClosing;
            Load += ChatForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}