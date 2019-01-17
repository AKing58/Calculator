using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
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

        private void equalsClick(object sender, EventArgs e)
        {
            String[] splitInput = inputTxt.Text.Split(' ');
            inputTxt.Text += " = " + bedmas(splitInput);
        }

        private double bedmas(String[] input)
        {
            double output = 0;
            List<String> tempList = input.ToList();
            double temp = 0;
            for(int i = 0; i<tempList.Count; i++)
            {
                if(tempList[i] == "X") {
                    temp = double.Parse(tempList[i - 1]) * double.Parse(tempList[i+1]);
                }
                else if(tempList[i] == "/") {
                    temp = double.Parse(tempList[i - 1]) / double.Parse(tempList[i + 1]);
                }
            }
            return output;
        }
    }
}
