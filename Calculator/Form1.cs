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
        List<String> inputs;
        double currNum;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void numClick(object sender, EventArgs e)
        {
            Button thisBtn = (Button) sender;
            inputTxt.Text += thisBtn.Text;
        }

        private void opClick(object sender, EventArgs e)
        {
            String[] curInput = inputTxt.Text.Split(' ');
            Button thisBtn = (Button)sender;
            Debug.Write(curInput.Length);
            if(curInput[curInput.Length-1] == "+" ||
                curInput[curInput.Length-1] == "-" ||
                curInput[curInput.Length-1] == "x" ||
                curInput[curInput.Length-1] == "/")
            {
                curInput[curInput.Length-1] = thisBtn.Text.Trim();
                inputTxt.Text = String.Join(" ", curInput);
                inputTxt.Text += " ";
            }
            else
            {
                inputTxt.Text += thisBtn.Text;
            }
        }

        private void equalsClick(object sender, EventArgs e)
        {
            String[] splitInput = inputTxt.Text.Split(' ');
            inputTxt.Text = bedmas(splitInput);
        }

        private string bedmas(String[] input)
        {
            double output = 0;
            List<String> tempList = input.ToList();
            double temp = 0;
            int i;
            for(i = 0; i < tempList.Count; i++)
            {
                if(tempList[i] == "x") {
                    temp = double.Parse(tempList[i - 1]) * double.Parse(tempList[i + 1]);
                    tempList[i - 1] = temp.ToString();
                    tempList.RemoveAt(i);
                    tempList.RemoveAt(i);
                }
                else if(tempList[i] == "/") {
                    temp = double.Parse(tempList[i - 1]) / double.Parse(tempList[i + 1]);
                    tempList[i - 1] = temp.ToString();
                    tempList.RemoveAt(i);
                    tempList.RemoveAt(i);
                }
            }

            for (i = 0; i < tempList.Count; i++)
            {
                if (tempList[i] == "+")
                {
                    temp = double.Parse(tempList[i - 1]) + double.Parse(tempList[i + 1]);
                    tempList[i - 1] = temp.ToString();
                    tempList.RemoveAt(i);
                    tempList.RemoveAt(i);
                }
                else if (tempList[i] == "-")
                {
                    temp = double.Parse(tempList[i - 1]) - double.Parse(tempList[i + 1]);
                    tempList[i - 1] = temp.ToString();
                    tempList.RemoveAt(i);
                    tempList.RemoveAt(i);
                }
            }

            return tempList[0];
        }

        private void decClick(object sender, EventArgs e)
        {

        }

        private void clearInput(object sender, EventArgs e)
        {
            inputTxt.Text = "";
        }

        
    }
}
