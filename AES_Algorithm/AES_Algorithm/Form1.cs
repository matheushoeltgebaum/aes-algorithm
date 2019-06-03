using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AES_Algorithm
{
    public partial class Form1 : Form
    {
        private string PlainFile { get { return tbFile.Text; } set { tbFile.Text = value; } }
        private string PathExport { get { return tbDirectory.Text; } set { tbDirectory.Text = value; } }

        private string CipherKey { get { return tbCipherKey.Text; } }

        private byte[,] Key { get; set; } = new byte[4, 4];

        private List<byte[,]> RoundKeys = new List<byte[,]>(11);

        public Form1()
        {
            InitializeComponent();
        }

        private void tbArquivo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
                SelectFile();
        }

        private void tbDiretorio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
                SelectPath();
        }

        private void SelectFile()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.CheckFileExists = true;
                dialog.CheckPathExists = true;
                dialog.Filter = "All files (*.*)|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                    PlainFile = dialog.FileName;
            }
        }

        private void SelectPath()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    PathExport = dialog.SelectedPath;
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(PlainFile) && !string.IsNullOrEmpty(PathExport))
                //{
                GenerateMatrixKey();
                PrintKey();
                GenerateRoundKeys();
                //}
            }
            catch (Exception exc)
            {
                MessageBox.Show($"Ocorreu um erro no programa. Mensagem: {exc.Message}");
            }
        }

        private void GenerateMatrixKey()
        {
            if (!string.IsNullOrEmpty(CipherKey))
            {
                string[] words = CipherKey.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                byte[] bytes = new byte[16];

                for (var i = 0; i < words.Length; i++)
                    bytes[i] = byte.Parse(words[i]);

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        int currentIndex = (i * 4) + j;
                        var a = words[currentIndex];
                        Key[j, i] = bytes[(i * 4) + j];
                    }
                }
            }
            else
            {
                throw new Exception("Não foi informada uma chave de criptografia!");
            }
        }

        private void PrintKey()
        {
            rtbLog.AppendText("****Chave****\n");
            for (var i = 0; i < 4; i++)
                rtbLog.AppendText($"0x{Key[i, 0].ToString("X2")} 0x{Key[i, 1].ToString("X2")} 0x{Key[i, 2].ToString("X2")} 0x{Key[i, 3].ToString("X2")}\n");
        }

        private void GenerateRoundKeys()
        {
            RoundKeys.Add(Key);

            for (var i = 1; i < 11; i++)
            {
                var lastWord = RoundKeys[i - 1].GetColumn(3);
                var firstWord = GenerateFirstWord(lastWord);
            }
        }

        private byte[] GenerateFirstWord(byte[] lastWord)
        {
            return new byte[10];
        }
    }
}
