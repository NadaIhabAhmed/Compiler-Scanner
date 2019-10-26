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

struct token
{
    token_type type;
    int index;
    string value;
};


namespace scanner
{

    public partial class Form1 : Form
    {
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
            token_table.Columns.Add("Token Type", typeof(string));
            token_table.Columns.Add("Token Value", typeof(string));
            dataGridView1.DataSource = token_table;

        }
        string symbols(string word)
        {

            switch (word)
            {
                case "+":
                    return "puls";
                case "-":
                    return "mins";
                case "*":
                    return "multi";
                case "/":
                    return "div";
                case ":=":
                    return "Assignment";
                case "<":
                    return "LessThan";
                case ">":
                    return "GreaterThan";
                case "=":
                    return "Equal";
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


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            char[] sperator = { '\n' };
            char[] sperator2 = { ' ', ';', '}' };
            line = textBox1.Text;

            // separate each line of the code in a certain index in a string array
            string[] new_line = line.Split(sperator, StringSplitOptions.RemoveEmptyEntries);
            
            for(int i = 0; i < new_line.Length; i++)
            {
                new_line[i] = new_line[i].Replace('\r', ' ');
            }


            foreach (string a in new_line)
            {
                // separate each word in each line to compare it with the reserved word
                
                string[] each_ling = a.Split(sperator2, StringSplitOptions.RemoveEmptyEntries);
                
                  
                

                foreach (string s in each_ling)
                {
                    if (s[0] == '{')
                    {
                        break;
                    }

                    if (Is_Reserved(s) != -1)
                    {
                        token_table.Rows.Add(s, reserved[Is_Reserved(s)].ToUpper());
                        //token_table.Rows.Add(s, "Reserved");
                        //and make the if(Is_Reserved(s)) only and modify the function to return bool
                    }
                    else if (symbols(s) != "other")
                    {
                        token_table.Rows.Add(s, symbols(s));
                    }
                    else if (s[0] != '{')
                    {
                        if (Char.IsDigit(s[0]))
                        {
                            Boolean sign = true;
                            for (int i = 0; i < s.Length; i++)
                            {
                                if (!Char.IsDigit(s[i]))
                                {
                                    /*
                                    if(s[i] == '\r')
                                    {
                                        continue;
                                    }
                                    */
                                    sign = false;
                                }
                            }
                            if (sign == true)
                            {
                                token_table.Rows.Add(s, "Number");
                            }
                            else
                            {
                                MessageBox.Show("Undefined Number", "Error");
                            }
                        }
                        else if ((s[0] >= 'a' && s[0] <= 'z') || (s[0] >= 'A' && s[0] <= 'Z'))
                        {

                            Boolean sign = true;
                            for (int i = 0; i < s.Length; i++)
                            {
                                if (!Char.IsLetter(s[i]))
                                {
                                    /*
                                    if (s[i] == '\r')
                                    {
                                        continue;
                                    }
                                    */
                                    sign = false;
                                }
                            }
                            if (sign == true)
                            {
                                token_table.Rows.Add(s, "Identifier");
                            }
                            else
                            {
                                MessageBox.Show("Undefined Letter", "Error");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Undefined token", "Error");
                        }
                    }

                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            token_table.Rows.Clear();
            textBox1.Clear();
        }
    }


}
