using System.Windows.Forms;

namespace AES_Algorithm
{
    partial class Form1
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
            this.lbArquivo = new System.Windows.Forms.Label();
            this.tbFile = new System.Windows.Forms.TextBox();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.tbDirectory = new System.Windows.Forms.TextBox();
            this.lbDiretorioExtracao = new System.Windows.Forms.Label();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.lbCipherKey = new System.Windows.Forms.Label();
            this.tbCipherKey = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbArquivo
            // 
            this.lbArquivo.AutoSize = true;
            this.lbArquivo.Location = new System.Drawing.Point(12, 9);
            this.lbArquivo.Name = "lbArquivo";
            this.lbArquivo.Size = new System.Drawing.Size(46, 13);
            this.lbArquivo.TabIndex = 0;
            this.lbArquivo.Text = "Arquivo:";
            // 
            // tbFile
            // 
            this.tbFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFile.Location = new System.Drawing.Point(129, 6);
            this.tbFile.Name = "tbFile";
            this.tbFile.Size = new System.Drawing.Size(520, 20);
            this.tbFile.TabIndex = 1;
            this.tbFile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbArquivo_KeyDown);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEncrypt.Location = new System.Drawing.Point(574, 84);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 2;
            this.btnEncrypt.Text = "Criptografar";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // tbDirectory
            // 
            this.tbDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDirectory.Location = new System.Drawing.Point(129, 32);
            this.tbDirectory.Name = "tbDirectory";
            this.tbDirectory.Size = new System.Drawing.Size(520, 20);
            this.tbDirectory.TabIndex = 4;
            this.tbDirectory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbDiretorio_KeyDown);
            // 
            // lbDiretorioExtracao
            // 
            this.lbDiretorioExtracao.AutoSize = true;
            this.lbDiretorioExtracao.Location = new System.Drawing.Point(12, 35);
            this.lbDiretorioExtracao.Name = "lbDiretorioExtracao";
            this.lbDiretorioExtracao.Size = new System.Drawing.Size(107, 13);
            this.lbDiretorioExtracao.TabIndex = 3;
            this.lbDiretorioExtracao.Text = "Local de exportação:";
            // 
            // rtbLog
            // 
            this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLog.Location = new System.Drawing.Point(12, 113);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(637, 314);
            this.rtbLog.TabIndex = 5;
            this.rtbLog.Text = "";
            // 
            // lbCipherKey
            // 
            this.lbCipherKey.AutoSize = true;
            this.lbCipherKey.Location = new System.Drawing.Point(12, 61);
            this.lbCipherKey.Name = "lbCipherKey";
            this.lbCipherKey.Size = new System.Drawing.Size(111, 13);
            this.lbCipherKey.TabIndex = 6;
            this.lbCipherKey.Text = "Chave de criptografia:";
            // 
            // tbCipherKey
            // 
            this.tbCipherKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCipherKey.Location = new System.Drawing.Point(129, 58);
            this.tbCipherKey.Name = "tbCipherKey";
            this.tbCipherKey.Size = new System.Drawing.Size(520, 20);
            this.tbCipherKey.TabIndex = 7;
            this.tbCipherKey.Text = "65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 439);
            this.Controls.Add(this.tbCipherKey);
            this.Controls.Add(this.lbCipherKey);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.tbDirectory);
            this.Controls.Add(this.lbDiretorioExtracao);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.tbFile);
            this.Controls.Add(this.lbArquivo);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BJM_AES - Criptografia AES";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lbArquivo;
        private TextBox tbFile;
        private Button btnEncrypt;
        private TextBox tbDirectory;
        private Label lbDiretorioExtracao;
        private RichTextBox rtbLog;
        private Label lbCipherKey;
        private TextBox tbCipherKey;
    }
}

