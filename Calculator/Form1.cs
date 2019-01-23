using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        static List<string> inputs= new List<string> { "" };
        
        String curNum = "";
        double total;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            inputTxt.Text = "0";
            inputs.Clear();
        }

        private void printDisplay(List<String> input)
        {
            foreach(var strinput in inputs)
            {
                Debug.WriteLine(strinput);
            }
            Debug.Flush();
            inputTxt.Text = "";
            for (int i = 0; i < input.Count; i++)
            {
                inputTxt.Text += input[i];
            }
            inputTxt.Text += curNum;
        }

        private void numClick(object sender, EventArgs e)
        {
            Button thisBtn = (Button) sender;
            curNum += thisBtn.Text;
            printDisplay(inputs);
        }

        
        private void opClick(object sender, EventArgs e)
        {
            Button thisBtn = (Button)sender;
            Debug.WriteLine(curNum);
            if (curNum != "" && inputs.Count == 0)
            {
                    inputs.Add(curNum);
                    curNum = "";
                    inputs.Add(thisBtn.Text);
            }else if(inputs.Count > 0)
            {
                if ((inputs[inputs.Count - 1] == "+" && curNum == "")||
               (inputs[inputs.Count - 1] == "-" && curNum == "")||
               (inputs[inputs.Count - 1] == "x" && curNum == "")||
               (inputs[inputs.Count - 1] == "/" && curNum == ""))
                {
                    inputs.RemoveAt(inputs.Count-1);
                    inputs.Add(thisBtn.Text);
                }else
                {
                    inputs.Add(curNum);
                    curNum = "";
                    inputs.Add(thisBtn.Text);
                }
            }

            printDisplay(inputs);
        }
        

        private void equalsClick(object sender, EventArgs e)
        {
            if(curNum == "" && inputs.Count == 0)
            {
                inputs.Add("0");
            }
            else if(inputs[inputs.Count-1] == "+" ||
                inputs[inputs.Count - 1] == "-" ||
                inputs[inputs.Count - 1] == "x" ||
                inputs[inputs.Count - 1] == "/")
            {
                inputs.RemoveAt(inputs.Count - 1);
            }else
            {
                inputs.Add(curNum);
            }
            
            total = bedmas(inputs);
            
            printDisplay(inputs);

            curNum = inputs[0];
        }

        private double bedmas(List<String> input)
        {
            double temp = 0;
            int i;
            for(i = 0; i < inputs.Count; i++)
            {
                if(inputs[i] == "x") {
                    temp = double.Parse(inputs[i - 1]) * double.Parse(inputs[i + 1]);
                    inputs[i - 1] = temp.ToString();
                    inputs.RemoveAt(i);
                    inputs.RemoveAt(i);
                    i -= 2;
                }
                else if(inputs[i] == "/") {
                    temp = double.Parse(inputs[i - 1]) / double.Parse(inputs[i + 1]);
                    inputs[i - 1] = temp.ToString();
                    inputs.RemoveAt(i);
                    inputs.RemoveAt(i);
                    i -= 2;
                }
            }

            for (i = 0; i < inputs.Count; i++)
            {
                if (inputs[i] == "+")
                {
                    temp = double.Parse(inputs[i - 1]) + double.Parse(inputs[i + 1]);
                    inputs[i - 1] = temp.ToString();
                    inputs.RemoveAt(i);
                    inputs.RemoveAt(i);
                    i -= 2;
                }
                else if (inputs[i] == "-")
                {
                    temp = double.Parse(inputs[i - 1]) - double.Parse(inputs[i + 1]);
                    inputs[i - 1] = temp.ToString();
                    inputs.RemoveAt(i);
                    inputs.RemoveAt(i);
                    i -= 2;
                }
            }
            
            return double.Parse(inputs[0]);
        }

        private void decClick(object sender, EventArgs e)
        {
            inputTxt.Text = (inputs.Count).ToString();
        }

        private void clearInput(object sender, EventArgs e)
        {
            inputTxt.Text = "0";
            curNum = "";
            inputs.Clear();
            
        }

        private void keyPress(object sender, KeyEventArgs e)
        {
           if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
            {
                Debug.Write((int)(e.KeyCode - 48));
            } else
            {
                Debug.Write(e.KeyCode);
            }
        }

        private void posNegClick(object sender, EventArgs e)
        {
            inputTxt.Text = "\"";
            inputTxt.Text += inputs[0];
            inputTxt.Text += inputs[1];
            inputTxt.Text += inputs.Count;
            inputTxt.Text += "\"";
        }
    }
}
