using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
enum token_type
{
    reserved,
    symobl,
    Identifier,
    number
};



namespace scanner
{

    public partial class Form1 : Form
    {
        int error_checking = 0;
        string line;
        string[] reserved = { "if", "then", "else", "end", "repeat", "until", "write", "read" };

        // Data grid view
        public static Panel panels = new Panel();
        DataTable token_table = new DataTable();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // data grid view
            token_table.Columns.Add("Token Value", typeof(string));
            token_table.Columns.Add("Token Type", typeof(string)) ;
            dataGridView1.DataSource =  token_table;

        }
        string symbols(char word)
        {

            switch (word)
            {
                case '+':
                    return "puls";
                case '-':
                    return "minus";
                case '*':
                    return "multi";
                case '/':
                    return "div";
                case '<':
                    return "LessThan";
                case '>':
                    return "GreaterThan";
                case '=':
                    return "Equal";
                case '[':
                    return "Left bracket";
                case ']':
                    return "Right bracket";
                case '(':
                    return "Left Parentheses";
                case ')':
                    return "Right Parenthese";
                default:
                    return "other";
            }


        }
        //check if the word is a reserved kewword or not
        int Is_Reserved(string each_word_in_each_line)
        {
            int check = -1;
            for (int i = 0; i < reserved.Length; i++)
            {
                if (string.Equals(each_word_in_each_line, reserved[i]))
                {
                    check = i;
                    break;
                }
            }

            return check;

        }

        void check(string line)
        {
            char[] sperator2 = { ' ', '}' };
            string[] new_line = line.Split(sperator2, StringSplitOptions.RemoveEmptyEntries);
            string num = "", word = "";

            if (error_checking == 0)
            {
                foreach (string s in new_line)
                {
                    num = ""; word = "";

                    if (s[0] == '{')
                        break;

                    if (Is_Reserved(s) != -1)
                    {
                        token_table.Rows.Add(s, reserved[Is_Reserved(s)].ToUpper() + "   Reserved");
                        continue;
                    }

                    for (int i = 0; i < s.Length; i++)
                    {

                        if ((s[i] == ':') && (s[i + 1] == '='))
                        {
                            if (num != "")
                            {
                                token_table.Rows.Add(num, "Number");
                                num = "";
                            }
                            else if (word != "")
                            {
                                token_table.Rows.Add(word, "Identifier");
                                word = "";
                            }
                            token_table.Rows.Add(":=", "Assignment");
                            i++;
                        }
                        else if ((s[i] == ';'))
                        {
                            if (num != "")
                            {
                                token_table.Rows.Add(num, "Number");
                                num = "";
                            }
                            else if (word != "")
                            {
                                token_table.Rows.Add(word, "Identifier");
                                word = "";
                            }
                            token_table.Rows.Add(";", "Separator");
                        }
                        else if (symbols(s[i]) != "other")
                        {
                            if (num != "")
                            {
                                token_table.Rows.Add(num, "Number");
                                num = "";
                            }
                            else if (word != "")
                            {
                                token_table.Rows.Add(word, "Identifier");
                                word = "";
                            }

                            token_table.Rows.Add(s[i], symbols(s[i]));
                        }
                        else if (Char.IsDigit(s[i]))
                            num += s[i];

                        else if (Char.IsLetter(s[i]))
                            word += s[i];

                        if (num != "" && word != "")
                        {
                            MessageBox.Show("Undefined", "Error");
                            error_checking = 1;
                            return;
                        }
                    }

                    if (num != "")
                        token_table.Rows.Add(num, "Number");
                    else if (word != "")
                        token_table.Rows.Add(word, "Identifier");

                }
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            char[] sperator = { '\n' };
            char[] sperator2 = { ' ', '}' };
            line = textBox1.Text;

            // separate each line of the code in a certain index in a string array
            string[] new_line = line.Split(sperator, StringSplitOptions.RemoveEmptyEntries);
            
            for(int i = 0; i < new_line.Length; i++)
            {
                new_line[i] = new_line[i].Replace('\r', ' ');
            }


            foreach (string a in new_line)
            {
                check(a);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            error_checking = 0;
            token_table.Rows.Clear();
            textBox1.Clear();
        }
    }


}
