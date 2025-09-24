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
            rbHost = new RadioButton();
            rbClient = new RadioButton();
            txtIPNiz = new TextBox();
            txtIPGirin = new TextBox();
            btnStart = new Button();
            lblHostIP = new Label();
            lblClientIP = new Label();
            otoIP = new Button();
            HostCopy = new Button();
            HostPaste = new Button();
            ClientPaste = new Button();
            ClientCopy = new Button();
            txtNick = new TextBox();
            SuspendLayout();
            // 
            // rbHost
            // 
            rbHost.AutoSize = true;
            rbHost.Checked = true;
            rbHost.Location = new Point(27, 64);
            rbHost.Name = "rbHost";
            rbHost.Size = new Size(101, 19);
            rbHost.TabIndex = 0;
            rbHost.TabStop = true;
            rbHost.Text = "Sunucu (Host)";
            rbHost.CheckedChanged += rbHost_CheckedChanged;
            // 
            // rbClient
            // 
            rbClient.AutoSize = true;
            rbClient.Location = new Point(147, 64);
            rbClient.Name = "rbClient";
            rbClient.Size = new Size(105, 19);
            rbClient.TabIndex = 1;
            rbClient.Text = "İstemci (Client)";
            rbClient.CheckedChanged += rbClient_CheckedChanged;
            // 
            // txtIPNiz
            // 
            txtIPNiz.Location = new Point(27, 114);
            txtIPNiz.Name = "txtIPNiz";
            txtIPNiz.PlaceholderText = "Sunucu kendi IP’si";
            txtIPNiz.Size = new Size(200, 23);
            txtIPNiz.TabIndex = 2;
            // 
            // txtIPGirin
            // 
            txtIPGirin.Location = new Point(27, 201);
            txtIPGirin.Name = "txtIPGirin";
            txtIPGirin.PlaceholderText = "Sunucu IP adresini gir";
            txtIPGirin.Size = new Size(200, 23);
            txtIPGirin.TabIndex = 3;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(27, 278);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(100, 30);
            btnStart.TabIndex = 4;
            btnStart.Text = "Başlat";
            btnStart.Click += btnStart_Click;
            // 
            // lblHostIP
            // 
            lblHostIP.Location = new Point(27, 94);
            lblHostIP.Name = "lblHostIP";
            lblHostIP.Size = new Size(100, 23);
            lblHostIP.TabIndex = 5;
            lblHostIP.Text = "Sunucu IP:";
            // 
            // lblClientIP
            // 
            lblClientIP.Location = new Point(27, 181);
            lblClientIP.Name = "lblClientIP";
            lblClientIP.Size = new Size(100, 23);
            lblClientIP.TabIndex = 6;
            lblClientIP.Text = "Bağlanılacak IP:";
            // 
            // otoIP
            // 
            otoIP.Location = new Point(233, 113);
            otoIP.Name = "otoIP";
            otoIP.Size = new Size(75, 23);
            otoIP.TabIndex = 7;
            otoIP.Text = "Oto IP";
            otoIP.UseVisualStyleBackColor = true;
            otoIP.Click += otoIP_Click;
            // 
            // HostCopy
            // 
            HostCopy.Location = new Point(27, 143);
            HostCopy.Name = "HostCopy";
            HostCopy.Size = new Size(96, 23);
            HostCopy.TabIndex = 8;
            HostCopy.Text = "Kopyala";
            HostCopy.UseVisualStyleBackColor = true;
            HostCopy.Click += HostCopy_Click;
            // 
            // HostPaste
            // 
            HostPaste.Location = new Point(129, 143);
            HostPaste.Name = "HostPaste";
            HostPaste.Size = new Size(98, 23);
            HostPaste.TabIndex = 9;
            HostPaste.Text = "Yapıştır";
            HostPaste.UseVisualStyleBackColor = true;
            HostPaste.Click += HostPaste_Click;
            // 
            // ClientPaste
            // 
            ClientPaste.Location = new Point(129, 230);
            ClientPaste.Name = "ClientPaste";
            ClientPaste.Size = new Size(98, 23);
            ClientPaste.TabIndex = 11;
            ClientPaste.Text = "Yapıştır";
            ClientPaste.UseVisualStyleBackColor = true;
            ClientPaste.Click += ClientPaste_Click;
            // 
            // ClientCopy
            // 
            ClientCopy.Location = new Point(27, 230);
            ClientCopy.Name = "ClientCopy";
            ClientCopy.Size = new Size(96, 23);
            ClientCopy.TabIndex = 10;
            ClientCopy.Text = "Kopyala";
            ClientCopy.UseVisualStyleBackColor = true;
            ClientCopy.Click += ClientCopy_Click;
            // 
            // txtNick
            // 
            txtNick.Location = new Point(97, 22);
            txtNick.Name = "txtNick";
            txtNick.Size = new Size(100, 23);
            txtNick.TabIndex = 12;
            // 
            // Form1
            // 
            ClientSize = new Size(346, 373);
            Controls.Add(txtNick);
            Controls.Add(ClientPaste);
            Controls.Add(ClientCopy);
            Controls.Add(HostPaste);
            Controls.Add(HostCopy);
            Controls.Add(otoIP);
            Controls.Add(rbHost);
            Controls.Add(rbClient);
            Controls.Add(txtIPNiz);
            Controls.Add(txtIPGirin);
            Controls.Add(btnStart);
            Controls.Add(lblHostIP);
            Controls.Add(lblClientIP);
            Name = "Form1";
            Text = "LAN Chat - Giriş";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private Button otoIP;
        private Button HostCopy;
        private Button HostPaste;
        private Button ClientPaste;
        private Button ClientCopy;
        private TextBox txtNick;
    }
}
