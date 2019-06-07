using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        private byte[] RoundConstant = new byte[10] { 1, 2, 4, 8, 16, 32, 64, 128, 27, 54 };

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
            rtbLog.Clear();
            RoundKeys.Clear();

            try
            {
                if (!string.IsNullOrEmpty(PlainFile) && !string.IsNullOrEmpty(PathExport))
                {
                    GenerateMatrixKey();
                    PrintKey("****Chave****", Key);
                    GenerateRoundKeys();
                    EncryptData();
                }
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
                        Key[j, i] = bytes[currentIndex];
                    }
                }
            }
            else
            {
                throw new Exception("Não foi informada uma chave de criptografia!");
            }
        }

        private void PrintKey(string label, byte[,] key)
        {
            rtbLog.AppendText($"{label}\n");
            for (var i = 0; i < 4; i++)
                rtbLog.AppendText($"0x{key[i, 0].ToString("X2")} 0x{key[i, 1].ToString("X2")} 0x{key[i, 2].ToString("X2")} 0x{key[i, 3].ToString("X2")}\n");

            rtbLog.AppendText("\n");
        }

        #region Rotinas de expansão de chave

        private void GenerateRoundKeys()
        {
            //Adiciona a chave em si
            RoundKeys.Add(Key);
            PrintKey("****RoundKey=0****", RoundKeys[0]);

            for (var i = 1; i < 11; i++)
            {
                var firstWord = GenerateFirstWord(RoundKeys[i - 1].GetColumn(3), RoundKeys[i - 1].GetColumn(0), i);
                var secondWord = ExclusiveOR(RoundKeys[i - 1].GetColumn(1), firstWord);
                var thirdWord = ExclusiveOR(RoundKeys[i - 1].GetColumn(2), secondWord);
                var forthWord = ExclusiveOR(RoundKeys[i - 1].GetColumn(3), thirdWord);

                RoundKeys.Add(new byte[4, 4] {
                    { firstWord[0], secondWord[0], thirdWord[0], forthWord[0] },
                    { firstWord[1], secondWord[1], thirdWord[1], forthWord[1] },
                    { firstWord[2], secondWord[2], thirdWord[2], forthWord[2] },
                    { firstWord[3], secondWord[3], thirdWord[3], forthWord[3] }
                });

                PrintKey($"****RoundKey={i}****", RoundKeys[i]);
            }
        }

        private byte[] GenerateFirstWord(byte[] previousLastWord, byte[] previousFirstWord, int index)
        {
            //RotWord
            var firstWord = LeftShiftArray(previousLastWord, 1);

            //SubWord
            firstWord = CalculateSBox(firstWord);

            //RoundConstant
            var roundConst = new byte[4] { RoundConstant[index - 1], 0, 0, 0 };

            //XOR - Palavra com RoundConstant
            var firstXor = ExclusiveOR(firstWord, roundConst);

            //XOR - Primeira palavra anterior com XOR anterior
            var secondXor = ExclusiveOR(previousFirstWord, firstXor);
            return secondXor;
        }

        public static byte[] LeftShiftArray(byte[] array, int shift)
        {
            var newArray = new byte[array.Length];

            for (var i = 0; i < array.Length; i++)
                newArray[i] = array[i];

            shift = shift % array.Length;
            byte[] buffer = new byte[shift];
            Array.Copy(newArray, buffer, shift);
            Array.Copy(newArray, shift, newArray, 0, newArray.Length - shift);
            Array.Copy(buffer, 0, newArray, newArray.Length - shift, shift);

            return newArray;
        }

        private byte[] CalculateSBox(byte[] word)
        {
            byte[] newWord = new byte[word.Length];

            for (int i = 0; i < word.Length; i++)
            {
                var b = word[i];
                var hexString = b.ToString("X2");

                var firstPartHex = hexString[0];
                var secondPartHex = hexString[1];

                hexString = SBox.GetNewHexString(firstPartHex, secondPartHex);
                newWord[i] = Convert.ToByte(hexString, 16);
            }

            return newWord;
        }

        public static byte[] ExclusiveOR(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
                throw new Exception("Arrays não possuem o mesmo tamanho.");

            byte[] result = new byte[array1.Length];

            for (int i = 0; i < array1.Length; i++)
                result[i] = (byte)(array1[i] ^ array2[i]);

            return result;
        }

        #endregion

        #region Rotinas de criptografia dos dados
        private void EncryptData()
        {
            byte[] buffer;
            using (var stream = File.Open(PlainFile, FileMode.Open))
            {
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
            }

            byte[,] Chunk = new byte[4, 4];
            byte[] encryptedBuffer = new byte[buffer.Length];

            for (var i = 0; i < buffer.Length; i += 16)
            {
                //Popula o bloco de bytes que será encriptado
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        int currentIndex = (j * 4) + k;
                        try { Chunk[k, j] = buffer[currentIndex]; }
                        catch { Chunk[k, j] = 0; }
                    }
                }

                //XOR (Texto simples, RoundKey[0])
                var A = ExclusiveOR(Chunk, RoundKeys[0]);
                PrintKey("****AddRoundKey-Round 0****", A);

                byte[,] B, C, D;

                for (int a = 1; a < 10; a++)
                {
                    //SubBytes
                    B = CalculateSBox(A);
                    PrintKey($"****SubBytes-Round {a}****", B);

                    //ShiftRows
                    C = ShiftRows(B);
                    PrintKey($"****ShiftRows-Round {a}****", C);

                    //MixColumns
                    D = MixColumns(C);
                    PrintKey($"****MixedColumns-Round {a}****", D);

                    //XOR (D, RoundKey(i))
                    A = ExclusiveOR(D, RoundKeys[a]);
                    PrintKey($"****addRoundKey-Round {a}****", A);
                }

                //SubBytes
                B = CalculateSBox(A);
                PrintKey("****SubBytes-Round 10****", B);

                //ShiftRows
                C = ShiftRows(B);
                PrintKey("****ShiftRows-Round 10****", C);

                //XOR(C, RoundKey(10)
                var E = ExclusiveOR(C, RoundKeys[10]);
                PrintKey("****addRoundKey-Round 10****", E);
            }

            using (var stream = File.Create(Path.Combine(PathExport, "encryptedFile.bjm_aes")))
                stream.Write(encryptedBuffer, 0, encryptedBuffer.Length);

            rtbLog.AppendText($"Arquivo gerado com sucesso!\n\nLocal do arquivo: {Path.Combine(PathExport, "encryptedFile.bjm_aes")}");
        }

        public static byte[,] ExclusiveOR(byte[,] matrix1, byte[,] matrix2)
        {
            if (matrix1.GetLength(0) != matrix2.GetLength(0) && matrix1.GetLength(1) != matrix2.GetLength(1))
                throw new Exception("Matrizes não possuem o mesmo tamanho.");

            var columns = new List<byte[]>();

            for (var i = 0; i < matrix1.GetLength(1); i++)
            {
                var array1 = matrix1.GetColumn(i);
                var array2 = matrix2.GetColumn(i);
                var result = new byte[array1.Length];

                for (int j = 0; j < array1.Length; j++)
                    result[j] = (byte)(array1[j] ^ array2[j]);

                columns.Add(result);
            }

            return new byte[4, 4] {
                { columns[0][0], columns[1][0], columns[2][0], columns[3][0] },
                { columns[0][1], columns[1][1], columns[2][1], columns[3][1] },
                { columns[0][2], columns[1][2], columns[2][2], columns[3][2] },
                { columns[0][3], columns[1][3], columns[2][3], columns[3][3] },
            };
        }

        private byte[,] CalculateSBox(byte[,] matrix)
        {
            byte[,] newWord = new byte[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < newWord.GetLength(0); i++)
            {
                for (int j = 0; j < newWord.GetLength(1); j++)
                {
                    var currentByte = matrix[j, i];
                    var hexString = currentByte.ToString("X2");

                    var firstPartHex = hexString[0];
                    var secondPartHex = hexString[1];

                    hexString = SBox.GetNewHexString(firstPartHex, secondPartHex);
                    newWord[j, i] = Convert.ToByte(hexString, 16);
                }
            }

            return newWord;
        }

        private byte[,] ShiftRows(byte[,] matrix)
        {
            var firstRow = matrix.GetRow(0);
            var secondRow = LeftShiftArray(matrix.GetRow(1), 1);
            var thirdRow = LeftShiftArray(matrix.GetRow(2), 2);
            var forthRow = LeftShiftArray(matrix.GetRow(3), 3);

            return new byte[4, 4]
            {
                { firstRow[0], firstRow[1], firstRow[2], firstRow[3] },
                { secondRow[0], secondRow[1], secondRow[2], secondRow[3] },
                { thirdRow[0], thirdRow[1], thirdRow[2], thirdRow[3] },
                { forthRow[0], forthRow[1], forthRow[2], forthRow[3] }
            };
        }

        private byte[,] MixColumns(byte[,] matrix)
        {
            byte[,] multiplyMatrix = new byte[4, 4]
            {
                { 2, 3, 1, 1 },
                { 1, 2, 3, 1 },
                { 1, 1, 2, 3 },
                { 3, 1, 1, 2 }
            };

            byte[] firstColumn = new byte[4];
            byte[] secondColumn = new byte[4];
            byte[] thirdColumn = new byte[4];
            byte[] forthColumn = new byte[4];

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    var matrixColumn = matrix.GetColumn(i);
                    var multMatrixRow = multiplyMatrix.GetRow(j);

                    byte firstTerm = MultiplyGalois(matrixColumn[0], multMatrixRow[0]);
                    byte secondTerm = MultiplyGalois(matrixColumn[1], multMatrixRow[1]);
                    byte thirdTerm = MultiplyGalois(matrixColumn[2], multMatrixRow[2]);
                    byte forthTerm = MultiplyGalois(matrixColumn[3], multMatrixRow[3]);

                    var currentByte = (byte)(firstTerm ^ secondTerm ^ thirdTerm ^ forthTerm);

                    if (i == 0)
                        firstColumn[j] = currentByte;
                    else if (i == 1)
                        secondColumn[j] = currentByte;
                    else if (i == 2)
                        thirdColumn[j] = currentByte;
                    else
                        forthColumn[j] = currentByte;
                }
            }

            return new byte[4, 4]
            {
                { firstColumn[0], secondColumn[0], thirdColumn[0], forthColumn[0] },
                { firstColumn[1], secondColumn[1], thirdColumn[1], forthColumn[1] },
                { firstColumn[2], secondColumn[2], thirdColumn[2], forthColumn[2] },
                { firstColumn[3], secondColumn[3], thirdColumn[3], forthColumn[3] }
            };
        }

        private byte MultiplyGalois(byte firstElement, byte secondElement)
        {
            var hexString = firstElement.ToString("X2");

            var firstPartHex = hexString[0];
            var secondPartHex = hexString[1];

            hexString = Galois.GetNewHexString(firstPartHex, secondPartHex);
            var newFirst = Convert.ToByte(hexString, 16);

            hexString = secondElement.ToString("X2");
            firstPartHex = hexString[0];
            secondPartHex = hexString[1];

            hexString = Galois.GetNewHexString(firstPartHex, secondPartHex);
            var newSecond = Convert.ToByte(hexString, 16);

            int result;

            if (firstElement == 0 || secondElement == 0)
                result = 0;
            else if (firstElement == 1)
                result = newSecond;
            else if (secondElement == 1)
                result = newFirst;
            else
                result = newFirst + newSecond;

            if (result > 255)
                result -= 255;

            hexString = result.ToString("X2");

            firstPartHex = hexString[0];
            secondPartHex = hexString[1];

            var finalResult = Galois.GetFinalNewHexString(firstPartHex, secondPartHex);
            return Convert.ToByte(finalResult, 16);
        }
        #endregion
    }
}
