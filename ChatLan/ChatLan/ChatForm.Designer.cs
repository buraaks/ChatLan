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
            nickBox = new TextBox();
            button1 = new Button();
            label1 = new Label();
            outBtn = new Button();
            SuspendLayout();
            // 
            // lstChat
            // 
            lstChat.Location = new Point(12, 62);
            lstChat.Name = "lstChat";
            lstChat.Size = new Size(400, 199);
            lstChat.TabIndex = 0;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(20, 267);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(300, 23);
            txtMessage.TabIndex = 1;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(330, 267);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(90, 23);
            btnSend.TabIndex = 2;
            btnSend.Text = "Gönder";
            btnSend.Click += btnSend_Click;
            // 
            // nickBox
            // 
            nickBox.Location = new Point(61, 24);
            nickBox.Name = "nickBox";
            nickBox.Size = new Size(135, 23);
            nickBox.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(212, 23);
            button1.Name = "button1";
            button1.Size = new Size(90, 23);
            button1.TabIndex = 4;
            button1.Text = "Değiştir";
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(12, 27);
            label1.Name = "label1";
            label1.Size = new Size(43, 20);
            label1.TabIndex = 5;
            label1.Text = "İsim:";
            // 
            // outBtn
            // 
            outBtn.Location = new Point(308, 24);
            outBtn.Name = "outBtn";
            outBtn.Size = new Size(104, 23);
            outBtn.TabIndex = 6;
            outBtn.Text = "Sunucudan Ayrıl";
            outBtn.Click += outBtn_Click;
            // 
            // ChatForm
            // 
            ClientSize = new Size(450, 337);
            Controls.Add(outBtn);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(nickBox);
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

        private TextBox nickBox;
        private Button button1;
        private Label label1;
        private Button outBtn;
    }
}
