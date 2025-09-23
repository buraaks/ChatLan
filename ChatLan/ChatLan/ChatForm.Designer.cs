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
            this.lstChat = new ListBox();
            this.txtMessage = new TextBox();
            this.btnSend = new Button();

            this.SuspendLayout();

            // lstChat
            this.lstChat.Location = new System.Drawing.Point(20, 20);
            this.lstChat.Size = new System.Drawing.Size(400, 200);

            // txtMessage
            this.txtMessage.Location = new System.Drawing.Point(20, 230);
            this.txtMessage.Size = new System.Drawing.Size(300, 23);

            // btnSend
            this.btnSend.Location = new System.Drawing.Point(330, 230);
            this.btnSend.Size = new System.Drawing.Size(90, 23);
            this.btnSend.Text = "Gönder";
            this.btnSend.Click += new EventHandler(this.btnSend_Click);

            // ChatForm
            this.ClientSize = new System.Drawing.Size(450, 280);
            this.Controls.Add(this.lstChat);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnSend);
            this.Name = "ChatForm";
            this.Text = "LAN Chat";

            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
