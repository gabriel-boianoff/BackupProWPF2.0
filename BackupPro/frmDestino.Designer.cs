namespace BackupPro
{
    partial class frmDestino
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
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.fbdDestino = new System.Windows.Forms.FolderBrowserDialog();
            this.lbxDestinos = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(12, 12);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(75, 23);
            this.btnAdicionar.TabIndex = 3;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // lbxDestinos
            // 
            this.lbxDestinos.FormattingEnabled = true;
            this.lbxDestinos.Location = new System.Drawing.Point(12, 41);
            this.lbxDestinos.Name = "lbxDestinos";
            this.lbxDestinos.Size = new System.Drawing.Size(384, 95);
            this.lbxDestinos.TabIndex = 4;
            // 
            // frmDestino
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 270);
            this.Controls.Add(this.lbxDestinos);
            this.Controls.Add(this.btnAdicionar);
            this.Name = "frmDestino";
            this.Text = "frmDestino";
            this.Shown += new System.EventHandler(this.frmDestino_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.FolderBrowserDialog fbdDestino;
        private System.Windows.Forms.ListBox lbxDestinos;
    }
}