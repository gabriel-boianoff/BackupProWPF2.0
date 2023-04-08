namespace BackupPro
{
    partial class frmOrigem
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
            this.ofdLocalizaArquivo = new System.Windows.Forms.OpenFileDialog();
            this.lbxArquivos = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(12, 12);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(75, 23);
            this.btnAdicionar.TabIndex = 1;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // ofdLocalizaArquivo
            // 
            this.ofdLocalizaArquivo.FileName = "openFileDialog1";
            // 
            // lbxArquivos
            // 
            this.lbxArquivos.FormattingEnabled = true;
            this.lbxArquivos.Location = new System.Drawing.Point(12, 41);
            this.lbxArquivos.Name = "lbxArquivos";
            this.lbxArquivos.Size = new System.Drawing.Size(360, 95);
            this.lbxArquivos.TabIndex = 3;
            // 
            // frmOrigem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 354);
            this.Controls.Add(this.lbxArquivos);
            this.Controls.Add(this.btnAdicionar);
            this.Name = "frmOrigem";
            this.Text = "frmOrigem";
            this.Shown += new System.EventHandler(this.frmOrigem_Shown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.OpenFileDialog ofdLocalizaArquivo;
        private System.Windows.Forms.ListBox lbxArquivos;
    }
}