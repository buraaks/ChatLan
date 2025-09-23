namespace LanChatApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private RadioButton rbHost;
        private RadioButton rbClient;
        private TextBox txtIPNiz;
        private TextBox txtIPGirin;
        private Button btnStart;
        private Label lblHostIP;
        private Label lblClientIP;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.rbHost = new RadioButton();
            this.rbClient = new RadioButton();
            this.txtIPNiz = new TextBox();
            this.txtIPGirin = new TextBox();
            this.btnStart = new Button();
            this.lblHostIP = new Label();
            this.lblClientIP = new Label();

            this.SuspendLayout();

            // rbHost
            this.rbHost.AutoSize = true;
            this.rbHost.Location = new System.Drawing.Point(30, 20);
            this.rbHost.Text = "Sunucu (Host)";
            this.rbHost.Checked = true;

            // rbClient
            this.rbClient.AutoSize = true;
            this.rbClient.Location = new System.Drawing.Point(150, 20);
            this.rbClient.Text = "İstemci (Client)";

            // lblHostIP
            this.lblHostIP.Location = new System.Drawing.Point(30, 50);
            this.lblHostIP.Text = "Sunucu IP:";

            // txtIPNiz
            this.txtIPNiz.Location = new System.Drawing.Point(30, 70);
            this.txtIPNiz.Size = new System.Drawing.Size(200, 23);
            this.txtIPNiz.PlaceholderText = "Sunucu kendi IP’si";

            // lblClientIP
            this.lblClientIP.Location = new System.Drawing.Point(30, 110);
            this.lblClientIP.Text = "Bağlanılacak IP:";

            // txtIPGirin
            this.txtIPGirin.Location = new System.Drawing.Point(30, 130);
            this.txtIPGirin.Size = new System.Drawing.Size(200, 23);
            this.txtIPGirin.PlaceholderText = "Sunucu IP adresini gir";

            // btnStart
            this.btnStart.Location = new System.Drawing.Point(30, 170);
            this.btnStart.Size = new System.Drawing.Size(100, 30);
            this.btnStart.Text = "Başlat";
            this.btnStart.Click += new EventHandler(this.btnStart_Click);

            // Form1
            this.ClientSize = new System.Drawing.Size(300, 230);
            this.Controls.Add(this.rbHost);
            this.Controls.Add(this.rbClient);
            this.Controls.Add(this.txtIPNiz);
            this.Controls.Add(this.txtIPGirin);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblHostIP);
            this.Controls.Add(this.lblClientIP);
            this.Name = "Form1";
            this.Text = "LAN Chat - Giriş";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
