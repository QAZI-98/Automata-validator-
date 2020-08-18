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

namespace DFA_with_filing
{
    public partial class Form1 : Form
    {
        string path="";
        string[] final=new string[10];
        int count_of_final_states = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            
            try
            {

                OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                openFileDialog1.ShowDialog();
                path = openFileDialog1.FileName;
                label2.Text = "states  inputs  outputs\n";
                using (var rd = new StreamReader(path))
                {
                    richTextBox1.Text += rd.ReadToEnd().Replace(" ","    \t   ").Replace("\r","\n");
                }
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (count_of_final_states < final.Length)
            {
                if (textBox1.Text.Trim()!="")
                {
                    try
                    {

                    final[count_of_final_states] = textBox1.Text;
                    label5.Text += final[count_of_final_states] + " ";
                    count_of_final_states++;

                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show( ee.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("invalid input");
                }
            }
            else
            {
                MessageBox.Show("max count reached");
            }
            textBox1.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            string[] text_file;
            if (path == "")
            {
                MessageBox.Show("select file");
            }
            else
            {
                try
                {

               
                label8.Text = "path ";
               using (var rd = new StreamReader(path))
                {
                    text_file = rd.ReadToEnd().Replace("\r", "").Split(' ', '\n');

                }
                string[] states = new string[text_file.Length];

                /////////////////////////////////////////////////////////////////////////

                for (int i = 0; i < text_file.Length; i++)
                {
                    if (text_file[i] != "")
                    {
                        states[i] = text_file[i];

                    }
                    i = i + 2;
                }

                string[] uniquestates_with_null_values = states.Distinct().ToArray();
                string[] state = states.Distinct().ToArray();
                int nos = 0;
                for (int i = 0; i < uniquestates_with_null_values.Length; i++)
                {
                    if (uniquestates_with_null_values[i] != null)
                    {
                        state[nos] = uniquestates_with_null_values[i];
                        nos++;
                    }
                }



                string[] input = new string[text_file.Length];
                int s = 0;
                for (int i = 1; i < text_file.Length; i++)
                {
                    if (text_file[i] != "")
                    {
                        input[s] = text_file[i];
                        s++;
                    }
                    i = i + 2;
                }

                string[] qq = input.Distinct().ToArray();
                int noi = 0;
                for (int i = 0; i < qq.Length; i++)
                {
                    if (qq[i] != null)
                    {
                        noi++;
                    }
                }


                string[] output = new string[text_file.Length];
                int d = 0;
                for (int i = 2; i < text_file.Length; i++)
                {
                    if (text_file[i] != "")
                    {
                        output[d] = text_file[i];
                        d++;
                    }
                    i = i + 2;
                }

                //////////////////////////////////////////////////////////////////// 

                string[,,] arr = new string[nos, (noi * nos), noi * nos];
                int counter = 0;
                for (int i = 0; i < nos; i++)
                {
                    arr[i, i, i] = state[i];
                    for (int l = 0; l < noi; l++)
                    {
                        arr[i, i + 1 + l, i] = input[counter];
                        arr[i, i, i + 1 + l] = output[counter];
                        counter++;
                    }

                }



                if (textBox2.Text != "")
                {

                    string strinG = textBox2.Text;
                    char[] inputs = strinG.ToCharArray();
                    string current = arr[0, 0, 0];
                    // bool inside = false;
                    for (int k = 0; k < inputs.Length; k++)
                    {

                        for (int i = 0; i < nos; i++)
                        {

                            if (current == arr[i, i, i])
                            {

                                for (int l = 0; l < noi; l++)
                                {
                                    if (inputs[k].ToString() == arr[i, l + 1 + i, i])
                                    {

                                        current = arr[i, i, i + l + 1];
                                        label8.Text += current + " ";
                                        goto a;
                                    }

                                }

                            }
                        }
                        a:
                        Console.WriteLine("");

                    }

                    //String.Compare
                    int comp = 0;
                    string check = "";
                    for (int i = 0; i < count_of_final_states; i++)
                    {
                        comp = (String.Compare(final[i], current, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreSymbols));
                        check = (comp == 0) ? "accepted" : "rejected";
                        if (check == "accepted")
                        {
                             label6.ForeColor = Color.FromArgb(102, 3, 252);
                            break;
                        }

                    }

                    if (check == "rejected")
                        label6.ForeColor = Color.Red;

                    label6.Text = check;
                    Console.ForegroundColor = ConsoleColor.White;

                }
                }
                catch (Exception)
                {
                    MessageBox.Show("invalid data entry");
                }
            }


        }
    }
}
