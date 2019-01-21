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
        static List<string> inputs= new List<string> { " " };
        
        String curNum;
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
            inputTxt.Text = "";
            for (int i = 0; i < input.Count; i++)
            {
                inputTxt.Text += input[i];
                inputTxt.Text += " ";
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
            if (inputs.Count != 0)
            {
                if (inputs[inputs.Count - 1] == "+" ||
               inputs[inputs.Count - 1] == "-" ||
               inputs[inputs.Count - 1] == "x" ||
               inputs[inputs.Count - 1] == "/")
                {
                    inputs.RemoveAt(inputs.Count-1);
                    inputs.Add(thisBtn.Text);
                }
                else
                {
                    inputs.Add(curNum);
                    curNum = "";
                    inputs.Add(thisBtn.Text);
                }
            }
            else
            {
                inputs.Add(curNum);
                inputs.Add(thisBtn.Text);
                curNum = "";
            }
           
            printDisplay(inputs);
        }

        private void equalsClick(object sender, EventArgs e)
        {
            curNum = "";
            total = bedmas(inputs);
        }

        private double bedmas(List<String> input)
        {
            double output = 0;
            double temp = 0;
            int i;
            inputs.Add(curNum);
            for(i = 1; i < inputs.Count; i++)
            {
                if(inputs[i] == "x") {
                    temp = double.Parse(inputs[i - 1]) * double.Parse(inputs[i + 1]);
                    inputs[i - 1] = temp.ToString();
                    inputs.RemoveAt(i);
                    inputs.RemoveAt(i);
                }
                else if(inputs[i] == "/") {
                    temp = double.Parse(inputs[i - 1]) / double.Parse(inputs[i + 1]);
                    inputs[i - 1] = temp.ToString();
                    inputs.RemoveAt(i);
                    inputs.RemoveAt(i);
                }
            }

            for (i = 1; i < inputs.Count; i++)
            {
                if (inputs[i] == "+")
                {
                    temp = double.Parse(inputs[i - 1]) + double.Parse(inputs[i + 1]);
                    inputs[i - 1] = temp.ToString();
                    inputs.RemoveAt(i);
                    inputs.RemoveAt(i);
                }
                else if (inputs[i] == "-")
                {
                    temp = double.Parse(inputs[i - 1]) - double.Parse(inputs[i + 1]);
                    inputs[i - 1] = temp.ToString();
                    inputs.RemoveAt(i);
                    inputs.RemoveAt(i);
                }
            }

            //return double.Parse(inputs[0]);
            return inputs.Count();
        }

        private void decClick(object sender, EventArgs e)
        {

        }

        private void clearInput(object sender, EventArgs e)
        {
            inputTxt.Text = "0";
            curNum = "";
            inputs.Clear();
            
        }

        
    }
}
