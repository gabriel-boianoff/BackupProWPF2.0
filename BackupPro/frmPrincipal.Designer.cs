namespace BackupPro
{
    partial class frmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.btnOrigem = new System.Windows.Forms.Button();
            this.btnDestino = new System.Windows.Forms.Button();
            this.BtnFTP = new System.Windows.Forms.Button();
            this.btnSobre = new System.Windows.Forms.Button();
            this.btnCopiar = new System.Windows.Forms.Button();
            this.btnZipar = new System.Windows.Forms.Button();
            this.btnEnviarFTP = new System.Windows.Forms.Button();
            this.pbrProgresso = new System.Windows.Forms.ProgressBar();
            this.pbxLoad = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoad)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOrigem
            // 
            this.btnOrigem.Location = new System.Drawing.Point(16, 13);
            this.btnOrigem.Name = "btnOrigem";
            this.btnOrigem.Size = new System.Drawing.Size(77, 54);
            this.btnOrigem.TabIndex = 0;
            this.btnOrigem.Text = "Origem";
            this.btnOrigem.UseVisualStyleBackColor = true;
            this.btnOrigem.Click += new System.EventHandler(this.btnOrigem_Click);
            // 
            // btnDestino
            // 
            this.btnDestino.Location = new System.Drawing.Point(157, 13);
            this.btnDestino.Name = "btnDestino";
            this.btnDestino.Size = new System.Drawing.Size(77, 54);
            this.btnDestino.TabIndex = 1;
            this.btnDestino.Text = "Destino";
            this.btnDestino.UseVisualStyleBackColor = true;
            this.btnDestino.Click += new System.EventHandler(this.btnDestino_Click);
            // 
            // BtnFTP
            // 
            this.BtnFTP.Location = new System.Drawing.Point(294, 13);
            this.BtnFTP.Name = "BtnFTP";
            this.BtnFTP.Size = new System.Drawing.Size(77, 54);
            this.BtnFTP.TabIndex = 2;
            this.BtnFTP.Text = "FTP";
            this.BtnFTP.UseVisualStyleBackColor = true;
            // 
            // btnSobre
            // 
            this.btnSobre.Location = new System.Drawing.Point(444, 13);
            this.btnSobre.Name = "btnSobre";
            this.btnSobre.Size = new System.Drawing.Size(77, 54);
            this.btnSobre.TabIndex = 3;
            this.btnSobre.Text = "Sobre";
            this.btnSobre.UseVisualStyleBackColor = true;
            this.btnSobre.Click += new System.EventHandler(this.btnSobre_Click);
            // 
            // btnCopiar
            // 
            this.btnCopiar.Location = new System.Drawing.Point(85, 172);
            this.btnCopiar.Name = "btnCopiar";
            this.btnCopiar.Size = new System.Drawing.Size(77, 54);
            this.btnCopiar.TabIndex = 4;
            this.btnCopiar.Text = "Copiar";
            this.btnCopiar.UseVisualStyleBackColor = true;
            this.btnCopiar.Click += new System.EventHandler(this.btnCopiar_Click);
            // 
            // btnZipar
            // 
            this.btnZipar.Location = new System.Drawing.Point(234, 172);
            this.btnZipar.Name = "btnZipar";
            this.btnZipar.Size = new System.Drawing.Size(77, 54);
            this.btnZipar.TabIndex = 5;
            this.btnZipar.Text = "Zipar";
            this.btnZipar.UseVisualStyleBackColor = true;
            this.btnZipar.Click += new System.EventHandler(this.btnZipar_Click);
            // 
            // btnEnviarFTP
            // 
            this.btnEnviarFTP.Location = new System.Drawing.Point(377, 172);
            this.btnEnviarFTP.Name = "btnEnviarFTP";
            this.btnEnviarFTP.Size = new System.Drawing.Size(77, 54);
            this.btnEnviarFTP.TabIndex = 6;
            this.btnEnviarFTP.Text = "Enviar pro FTP";
            this.btnEnviarFTP.UseVisualStyleBackColor = true;
            // 
            // pbrProgresso
            // 
            this.pbrProgresso.Location = new System.Drawing.Point(85, 287);
            this.pbrProgresso.Name = "pbrProgresso";
            this.pbrProgresso.Size = new System.Drawing.Size(369, 33);
            this.pbrProgresso.TabIndex = 7;
            // 
            // pbxLoad
            // 
            this.pbxLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pbxLoad.Image = ((System.Drawing.Image)(resources.GetObject("pbxLoad.Image")));
            this.pbxLoad.Location = new System.Drawing.Point(234, 239);
            this.pbxLoad.Name = "pbxLoad";
            this.pbxLoad.Size = new System.Drawing.Size(53, 42);
            this.pbxLoad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLoad.TabIndex = 15;
            this.pbxLoad.TabStop = false;
            this.pbxLoad.Visible = false;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 347);
            this.Controls.Add(this.pbxLoad);
            this.Controls.Add(this.pbrProgresso);
            this.Controls.Add(this.btnEnviarFTP);
            this.Controls.Add(this.btnZipar);
            this.Controls.Add(this.btnCopiar);
            this.Controls.Add(this.btnSobre);
            this.Controls.Add(this.BtnFTP);
            this.Controls.Add(this.btnDestino);
            this.Controls.Add(this.btnOrigem);
            this.MaximizeBox = false;
            this.Name = "frmPrincipal";
            this.Text = "BackupPro 2.0 - Tela Principal";
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoad)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOrigem;
        private System.Windows.Forms.Button btnDestino;
        private System.Windows.Forms.Button BtnFTP;
        private System.Windows.Forms.Button btnSobre;
        private System.Windows.Forms.Button btnCopiar;
        private System.Windows.Forms.Button btnZipar;
        private System.Windows.Forms.Button btnEnviarFTP;
        private System.Windows.Forms.ProgressBar pbrProgresso;
        private System.Windows.Forms.PictureBox pbxLoad;
    }
}

