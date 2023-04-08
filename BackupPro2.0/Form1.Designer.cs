namespace BackupPro2._0
{
    partial class frmHome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHome));
            this.btnBackup = new System.Windows.Forms.Button();
            this.txtDestino = new System.Windows.Forms.TextBox();
            this.txtOrigem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOrigem = new System.Windows.Forms.Button();
            this.btnDestino = new System.Windows.Forms.Button();
            this.BuscaDiretorio = new System.Windows.Forms.FolderBrowserDialog();
            this.BuscaArquivo = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkCompacto = new System.Windows.Forms.RadioButton();
            this.chkNormal = new System.Windows.Forms.RadioButton();
            this.btnFTP = new System.Windows.Forms.Button();
            this.lblOU = new System.Windows.Forms.Label();
            this.tabConfig = new System.Windows.Forms.TabControl();
            this.guiConfig = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFtp = new System.Windows.Forms.TextBox();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.lblConfig = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblLoad = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pbxLoad = new System.Windows.Forms.PictureBox();
            this.pbrProgresso = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.tabConfig.SuspendLayout();
            this.guiConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoad)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(70, 182);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(173, 23);
            this.btnBackup.TabIndex = 0;
            this.btnBackup.Text = "Fazer Backup";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // txtDestino
            // 
            this.txtDestino.Location = new System.Drawing.Point(70, 141);
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.Size = new System.Drawing.Size(173, 20);
            this.txtDestino.TabIndex = 1;
            // 
            // txtOrigem
            // 
            this.txtOrigem.Location = new System.Drawing.Point(70, 95);
            this.txtOrigem.Name = "txtOrigem";
            this.txtOrigem.Size = new System.Drawing.Size(173, 20);
            this.txtOrigem.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Origem";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Destino";
            // 
            // btnOrigem
            // 
            this.btnOrigem.Location = new System.Drawing.Point(249, 95);
            this.btnOrigem.Name = "btnOrigem";
            this.btnOrigem.Size = new System.Drawing.Size(27, 23);
            this.btnOrigem.TabIndex = 5;
            this.btnOrigem.Text = "...";
            this.btnOrigem.UseVisualStyleBackColor = true;
            this.btnOrigem.Click += new System.EventHandler(this.btnOrigem_Click);
            // 
            // btnDestino
            // 
            this.btnDestino.Location = new System.Drawing.Point(249, 141);
            this.btnDestino.Name = "btnDestino";
            this.btnDestino.Size = new System.Drawing.Size(27, 23);
            this.btnDestino.TabIndex = 6;
            this.btnDestino.Text = "...";
            this.btnDestino.UseVisualStyleBackColor = true;
            this.btnDestino.Click += new System.EventHandler(this.btnDestino_Click);
            // 
            // BuscaArquivo
            // 
            this.BuscaArquivo.Multiselect = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkCompacto);
            this.groupBox1.Controls.Add(this.chkNormal);
            this.groupBox1.Location = new System.Drawing.Point(68, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 42);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo do Backup";
            // 
            // chkCompacto
            // 
            this.chkCompacto.AutoSize = true;
            this.chkCompacto.Checked = true;
            this.chkCompacto.Location = new System.Drawing.Point(102, 19);
            this.chkCompacto.Name = "chkCompacto";
            this.chkCompacto.Size = new System.Drawing.Size(73, 17);
            this.chkCompacto.TabIndex = 10;
            this.chkCompacto.TabStop = true;
            this.chkCompacto.Text = "Compacto";
            this.chkCompacto.UseVisualStyleBackColor = true;
            // 
            // chkNormal
            // 
            this.chkNormal.AutoSize = true;
            this.chkNormal.Location = new System.Drawing.Point(15, 19);
            this.chkNormal.Name = "chkNormal";
            this.chkNormal.Size = new System.Drawing.Size(58, 17);
            this.chkNormal.TabIndex = 9;
            this.chkNormal.TabStop = true;
            this.chkNormal.Text = "Normal";
            this.chkNormal.UseVisualStyleBackColor = true;
            // 
            // btnFTP
            // 
            this.btnFTP.Location = new System.Drawing.Point(70, 224);
            this.btnFTP.Name = "btnFTP";
            this.btnFTP.Size = new System.Drawing.Size(173, 27);
            this.btnFTP.TabIndex = 9;
            this.btnFTP.Text = "Salvar em Nuvem";
            this.btnFTP.UseVisualStyleBackColor = true;
            this.btnFTP.Click += new System.EventHandler(this.btnFTP_Click);
            // 
            // lblOU
            // 
            this.lblOU.AutoSize = true;
            this.lblOU.Location = new System.Drawing.Point(67, 208);
            this.lblOU.Name = "lblOU";
            this.lblOU.Size = new System.Drawing.Size(179, 13);
            this.lblOU.TabIndex = 10;
            this.lblOU.Text = "--------------------------  E  -------------------------";
            // 
            // tabConfig
            // 
            this.tabConfig.Controls.Add(this.guiConfig);
            this.tabConfig.Location = new System.Drawing.Point(0, 0);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.SelectedIndex = 0;
            this.tabConfig.Size = new System.Drawing.Size(306, 270);
            this.tabConfig.TabIndex = 12;
            this.tabConfig.Visible = false;
            // 
            // guiConfig
            // 
            this.guiConfig.Controls.Add(this.label6);
            this.guiConfig.Controls.Add(this.label5);
            this.guiConfig.Controls.Add(this.label3);
            this.guiConfig.Controls.Add(this.txtPass);
            this.guiConfig.Controls.Add(this.txtUser);
            this.guiConfig.Controls.Add(this.label4);
            this.guiConfig.Controls.Add(this.txtFtp);
            this.guiConfig.Controls.Add(this.btnVoltar);
            this.guiConfig.Controls.Add(this.btnSalvar);
            this.guiConfig.Location = new System.Drawing.Point(4, 22);
            this.guiConfig.Name = "guiConfig";
            this.guiConfig.Padding = new System.Windows.Forms.Padding(3);
            this.guiConfig.Size = new System.Drawing.Size(298, 244);
            this.guiConfig.TabIndex = 0;
            this.guiConfig.Text = "Configurações";
            this.guiConfig.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Senha:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Usuario:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label3.Location = new System.Drawing.Point(45, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Configurações para Backup em nuvem.";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(55, 94);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(176, 20);
            this.txtPass.TabIndex = 26;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(55, 68);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(176, 20);
            this.txtUser.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "FTP:";
            // 
            // txtFtp
            // 
            this.txtFtp.Location = new System.Drawing.Point(55, 42);
            this.txtFtp.Name = "txtFtp";
            this.txtFtp.Size = new System.Drawing.Size(176, 20);
            this.txtFtp.TabIndex = 23;
            // 
            // btnVoltar
            // 
            this.btnVoltar.Location = new System.Drawing.Point(149, 132);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(82, 23);
            this.btnVoltar.TabIndex = 22;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(55, 132);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(82, 23);
            this.btnSalvar.TabIndex = 21;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lblConfig
            // 
            this.lblConfig.AutoSize = true;
            this.lblConfig.Location = new System.Drawing.Point(5, 3);
            this.lblConfig.Name = "lblConfig";
            this.lblConfig.Size = new System.Drawing.Size(75, 13);
            this.lblConfig.TabIndex = 13;
            this.lblConfig.Text = "Configurações";
            this.lblConfig.Click += new System.EventHandler(this.lblConfig_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblLoad
            // 
            this.lblLoad.AutoSize = true;
            this.lblLoad.BackColor = System.Drawing.Color.Transparent;
            this.lblLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblLoad.ForeColor = System.Drawing.Color.Black;
            this.lblLoad.Location = new System.Drawing.Point(133, 311);
            this.lblLoad.Name = "lblLoad";
            this.lblLoad.Size = new System.Drawing.Size(18, 20);
            this.lblLoad.TabIndex = 15;
            this.lblLoad.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(166, 311);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "%";
            // 
            // pbxLoad
            // 
            this.pbxLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pbxLoad.Image = ((System.Drawing.Image)(resources.GetObject("pbxLoad.Image")));
            this.pbxLoad.Location = new System.Drawing.Point(123, 257);
            this.pbxLoad.Name = "pbxLoad";
            this.pbxLoad.Size = new System.Drawing.Size(53, 42);
            this.pbxLoad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLoad.TabIndex = 14;
            this.pbxLoad.TabStop = false;
            this.pbxLoad.Visible = false;
            this.pbxLoad.Click += new System.EventHandler(this.pbxLoad_Click);
            // 
            // pbrProgresso
            // 
            this.pbrProgresso.Location = new System.Drawing.Point(0, 334);
            this.pbrProgresso.Name = "pbrProgresso";
            this.pbrProgresso.Size = new System.Drawing.Size(331, 23);
            this.pbrProgresso.TabIndex = 17;
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(331, 358);
            this.Controls.Add(this.pbrProgresso);
            this.Controls.Add(this.lblLoad);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pbxLoad);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblOU);
            this.Controls.Add(this.btnFTP);
            this.Controls.Add(this.btnDestino);
            this.Controls.Add(this.btnOrigem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOrigem);
            this.Controls.Add(this.txtDestino);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.tabConfig);
            this.Controls.Add(this.lblConfig);
            this.Name = "frmHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backup Pro 2.0";
            this.Load += new System.EventHandler(this.frmHome_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabConfig.ResumeLayout(false);
            this.guiConfig.ResumeLayout(false);
            this.guiConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.TextBox txtDestino;
        private System.Windows.Forms.TextBox txtOrigem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOrigem;
        private System.Windows.Forms.Button btnDestino;
        private System.Windows.Forms.FolderBrowserDialog BuscaDiretorio;
        private System.Windows.Forms.OpenFileDialog BuscaArquivo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton chkNormal;
        private System.Windows.Forms.RadioButton chkCompacto;
        private System.Windows.Forms.Button btnFTP;
        private System.Windows.Forms.Label lblOU;
        private System.Windows.Forms.TabControl tabConfig;
        private System.Windows.Forms.TabPage guiConfig;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFtp;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblConfig;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblLoad;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pbxLoad;
        private System.Windows.Forms.ProgressBar pbrProgresso;
    }
}

