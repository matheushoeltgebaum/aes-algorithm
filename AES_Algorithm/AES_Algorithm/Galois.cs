using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES_Algorithm
{
    public class Galois
    {
        private static string[,] L_Table = new string[16, 16]
        {
            { "00", "00", "19", "01", "32", "02", "1A", "C6", "4B", "C7", "1B", "68", "33", "EE", "DF", "03" },
            { "64", "04", "E0", "0E", "34", "8D", "81", "EF", "4C", "71", "08", "C8", "F8", "69", "1C", "C1" },
            { "7D", "C2", "1D", "B5", "F9", "B9", "27", "6A", "4D", "E4", "A6", "72", "9A", "C9", "09", "78" },
            { "65", "2F", "8A", "05", "21", "0F", "E1", "24", "12", "F0", "82", "45", "35", "93", "DA", "8E" },
            { "96", "8F", "DB", "BD", "36", "D0", "CE", "94", "13", "5C", "D2", "F1", "40", "46", "83", "38" },
            { "66", "DD", "FD", "30", "BF", "06", "8B", "62", "B3", "25", "E2", "98", "22", "88", "91", "10" },
            { "7E", "6E", "48", "C3", "A3", "B6", "1E", "42", "3A", "6B", "28", "54", "FA", "85", "3D", "BA" },
            { "2B", "79", "0A", "15", "9B", "9F", "5E", "CA", "4E", "D4", "AC", "E5", "F3", "73", "A7", "57" },
            { "AF", "58", "A8", "50", "F4", "97", "D6", "74", "4F", "AE", "E9", "D5", "E7", "E6", "AD", "E8" },
            { "2C", "D7", "75", "7A", "EB", "2A", "0B", "F5", "59", "CB", "5F", "B0", "9C", "A9", "51", "A0" },
            { "7F", "0C", "F6", "6F", "17", "06", "49", "EC", "D8", "43", "1F", "2D", "A4", "76", "7B", "B7" },
            { "CC", "BB", "3E", "5A", "FB", "D5", "B1", "86", "3B", "52", "A1", "6C", "AA", "55", "29", "9D" },
            { "97", "B2", "87", "90", "61", "A6", "DC", "FC", "BC", "95", "CF", "CD", "37", "3F", "5B", "D1" },
            { "53", "39", "84", "3C", "41", "03", "6D", "47", "14", "2A", "9E", "5D", "56", "F2", "D3", "AB" },
            { "44", "11", "92", "D9", "23", "D9", "2E", "89", "B4", "7C", "B8", "26", "77", "99", "E3", "A5" },
            { "67", "4A", "ED", "DE", "C5", "E6", "FE", "18", "0D", "63", "8C", "80", "C0", "F7", "70", "07" },
        };

        private static string[,] E_Table = new string[16, 16]
        {
            { "01", "03", "05", "0F", "11", "33", "55", "FF", "1A", "2E", "72", "96", "A1", "F8", "13", "35" },
            { "5F", "E1", "38", "48", "D8", "73", "95", "A4", "F7", "02", "06", "0A", "1E", "22", "66", "AA" },
            { "E5", "34", "5C", "E4", "37", "59", "EB", "26", "6A", "BE", "D9", "70", "90", "AB", "E6", "31" },
            { "53", "F5", "04", "0C", "14", "3C", "44", "CC", "4F", "D1", "68", "B8", "D3", "6E", "B2", "CD" },
            { "4C", "D4", "67", "A9", "E0", "3B", "4D", "D7", "62", "A6", "F1", "08", "18", "28", "78", "88" },
            { "83", "9E", "B9", "D0", "6B", "BD", "DC", "7F", "81", "98", "B3", "CE", "49", "DB", "76", "9A" },
            { "B5", "C4", "57", "F9", "10", "30", "50", "F0", "0B", "1D", "27", "69", "BB", "D6", "61", "A3" },
            { "FE", "19", "2B", "7D", "87", "92", "AD", "EC", "2F", "71", "93", "AE", "E9", "20", "60", "A0" },
            { "FB", "16", "3A", "4E", "D2", "6D", "B7", "C2", "5D", "E7", "32", "56", "FA", "15", "3F", "41" },
            { "C3", "5E", "E2", "3D", "47", "C9", "40", "C0", "5B", "ED", "2C", "74", "9C", "BF", "DA", "75" },
            { "9F", "BA", "D5", "64", "AC", "EF", "2A", "7E", "82", "9D", "BC", "DF", "7A", "8E", "89", "80" },
            { "9B", "B6", "C1", "58", "E8", "23", "65", "AF", "EA", "25", "6F", "B1", "C8", "43", "C5", "54" },
            { "FC", "1F", "21", "63", "A5", "F4", "07", "09", "1B", "2D", "77", "99", "B0", "CB", "46", "CA" },
            { "45", "CF", "4A", "DE", "79", "8B", "86", "91", "A8", "E3", "3E", "42", "C6", "51", "F3", "0E" },
            { "12", "36", "5A", "EE", "29", "7B", "8D", "8C", "8F", "8A", "85", "94", "A7", "F2", "0D", "17" },
            { "39", "4B", "DD", "7C", "84", "97", "A2", "FD", "1C", "24", "6C", "B4", "C7", "52", "F6", "01" },
        };

        public static string GetNewHexString(char line, char column)
        {
            int l = 0, c = 0;

            if (!int.TryParse(line.ToString(), out l))
            {
                switch (line)
                {
                    case 'A':
                        l = 10;
                        break;
                    case 'B':
                        l = 11;
                        break;
                    case 'C':
                        l = 12;
                        break;
                    case 'D':
                        l = 13;
                        break;
                    case 'E':
                        l = 14;
                        break;
                    case 'F':
                        l = 15;
                        break;
                }
            }

            if (!int.TryParse(column.ToString(), out c))
            {
                switch (column)
                {
                    case 'A':
                        c = 10;
                        break;
                    case 'B':
                        c = 11;
                        break;
                    case 'C':
                        c = 12;
                        break;
                    case 'D':
                        c = 13;
                        break;
                    case 'E':
                        c = 14;
                        break;
                    case 'F':
                        c = 15;
                        break;
                }
            }

            return L_Table[l, c];
        }

        public static string GetFinalNewHexString(char line, char column)
        {
            int l = 0, c = 0;

            if (!int.TryParse(line.ToString(), out l))
            {
                switch (line)
                {
                    case 'A':
                        l = 10;
                        break;
                    case 'B':
                        l = 11;
                        break;
                    case 'C':
                        l = 12;
                        break;
                    case 'D':
                        l = 13;
                        break;
                    case 'E':
                        l = 14;
                        break;
                    case 'F':
                        l = 15;
                        break;
                }
            }

            if (!int.TryParse(column.ToString(), out c))
            {
                switch (column)
                {
                    case 'A':
                        c = 10;
                        break;
                    case 'B':
                        c = 11;
                        break;
                    case 'C':
                        c = 12;
                        break;
                    case 'D':
                        c = 13;
                        break;
                    case 'E':
                        c = 14;
                        break;
                    case 'F':
                        c = 15;
                        break;
                }
            }

            return E_Table[l, c];
        }
    }
}
