/****************************
** q503.cs
** Magic Spell Checker
** Patrick Xie, 5/03/2011
****************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace q503
{
    public partial class q503 : Form
    {
        public q503()
        {
            InitializeComponent();
        }
        //s holds the line of text
        private string s;
        //container holds all the words in the dictionary text file
        List<string> dictionaryText = new List<string>();
        private void q503_Load(object sender, EventArgs e)
        {
            //add the words from dictionary.txt into the dictionary list
            string[] dicStr = Properties.Resources.dictionary.Split('\r','\n');
            foreach (string dicWord in (dicStr))
            {
                dictionaryText.Add(dicWord);
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //opens the open file dialog to pick what text file to load
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream fStream = new FileStream(@openFileDialog1.FileName, FileMode.Open);
                StreamReader sReader = new StreamReader(fStream);
                rTextBox.Font = new Font("Courier New", 10, FontStyle.Regular);
                s = "";
                //while loop reads entire text file
                while (!sReader.EndOfStream)
                {
                    //s is one line in the text file
                    s = sReader.ReadLine();
                    if (s != "")
                    {
                        //splits s into an array of strings from the line of text
                        string[] sArray = s.Split(' ');
                        foreach (string word in sArray)
                        {
                            //converts the original word into modWord in lower case
                            string modWord = word.ToLower();
                            int start = rTextBox.TextLength;
                            if (word != "")
                            {
                                //checks if the last char in the word has a punctuation
                                if (char.IsPunctuation(modWord[modWord.Length - 1]))
                                {
                                    modWord = modWord.Substring(0, (modWord.Length - 1));
                                }
                                //checks if the dictionarylist has the lowercase no punctuation word
                                if (dictionaryText.Contains(modWord))
                                {
                                    //if is contained then is colored in black font
                                    rTextBox.AppendText(word + " ");
                                    rTextBox.Select(start, rTextBox.TextLength - start);
                                    rTextBox.SelectionColor = Color.Black;
                                    rTextBox.SelectionLength = 0;
                                }
                                else
                                {
                                    //else not contained then colored in red font
                                    rTextBox.AppendText(word + " ");
                                    rTextBox.Select(start, rTextBox.TextLength - start);
                                    rTextBox.SelectionColor = Color.Red;
                                    rTextBox.SelectionLength = 0;
                                }
                            }
                        }
                    }
                    //adds a line break
                    rTextBox.AppendText(Environment.NewLine);
                }
                //closes the file stream
                fStream.Close();
            }
        }
        //for the save click on the menu strip
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //saves what is displayed in the rich text box into a text file
            if (DialogResult.OK == saveFileDialog1.ShowDialog())
            {
                FileStream fileToWrite = new FileStream(saveFileDialog1.FileName,FileMode.Create);
                StreamWriter fout = new StreamWriter(fileToWrite);
                saveFileDialog1.Filter = "txt files (*.txt) | *.txt| All Files (*.*)|*.*";
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.FileName.Length > 0)
                {
                    fout.Write(rTextBox.Text);
                    fout.Flush();
                    fileToWrite.Close();
                }
            }
        }
    }
}