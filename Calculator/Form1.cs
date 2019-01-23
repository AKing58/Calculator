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
    /// <summary>
    /// Purpose: Runs the application
    /// Input: Numbers and Operations
    /// Output: Numbers
    /// Author: Adam King, Justin Kwok
    /// Date: 23/01/2019
    /// Updated By: Adam King, Justin Kwok
    /// Date: 23/01/2019
    /// </summary>
    public partial class Form1 : Form
    {
        static List<string> inputs= new List<string> { "" };
        
        String curNum = "";
        double total;
        bool on = false;
        String memCalc = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            inputs.Clear();
        }

        // Turns "on" the calculator
        private void onButton(object sender, EventArgs e)
        {
            on = true;
            inputTxt.Text = "0";
        }

        // Print function that copies the list to a single string
        private void printDisplay(List<String> input)
        {
            inputTxt.Text = "";
            for (int i = 0; i < input.Count; i++)
            {
                inputTxt.Text += input[i];
            }
            inputTxt.Text += curNum;
        }

        // Adds numbers to the display
        private void numClick(object sender, EventArgs e)
        {
            if (!on) { return; }
            Button thisBtn = (Button) sender;
            curNum += thisBtn.Text;
            printDisplay(inputs);
        }

        // Adds operations to the display
        private void opClick(object sender, EventArgs e)
        {
            if (!on) { return; }
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
            else
            {
                return;
            }

            printDisplay(inputs);
        }
        
        // Calls the BEDMAS function and prints to display
        private void equalsClick(object sender, EventArgs e)
        {
            if (!on) { return; }
            if (curNum == "" && inputs.Count == 0)
            {
                inputs.Add("0");
            }
            else if(curNum == "")
            {
                if (inputs[inputs.Count - 1] == "+" ||
                inputs[inputs.Count - 1] == "-" ||
                inputs[inputs.Count - 1] == "x" ||
                inputs[inputs.Count - 1] == "/")
                    inputs.RemoveAt(inputs.Count - 1);
            }else
            {
                inputs.Add(curNum);
                curNum = "";
            }
            
            total = bedmas(inputs);
            
            printDisplay(inputs);
        }

        // Performs order of operations on the list of strings
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

        // Adds a decimal to the display
        private void decClick(object sender, EventArgs e)
        {
            inputTxt.Text = (inputs.Count).ToString();
        }

        // Clears the display
        private void clearInput(object sender, EventArgs e)
        {
            if (!on) { return; }
            inputTxt.Text = "0";
            curNum = "";
            inputs.Clear();
            
        }

        // Enables keyboard input for application
        private void keyPress(object sender, KeyEventArgs e)
        {
           if (!on) { return; }
           switch (e.KeyCode)
            {
                case Keys.D0: case Keys.D1: case Keys.D2:
                case Keys.D3: case Keys.D4: case Keys.D5:
                case Keys.D6: case Keys.D7: case Keys.D8:
                case Keys.D9:
                    {
                        if (e.KeyCode == Keys.D8 && e.Shift)
                        {
                            Debug.Write("*");
                            break;
                        }
                        else
                        {
                            Debug.Write((int)(e.KeyCode - 48));
                            break;
                        }
                    }
                case Keys.Oemplus: 
                    {
                        if (e.KeyCode == Keys.Oemplus && e.Shift)
                        {
                            Debug.Write("+");
                            break;
                        } else
                        {
                            Debug.Write("=");
                            break;
                        }
                    }
                case Keys.OemMinus:
                    {
                        Debug.Write("-");
                        break;
                    }
                case Keys.OemQuestion:
                    {
                        Debug.Write("/");
                        break;
                    }
                case Keys.Back:
                    {
                        Debug.Write("Backspace");
                        break;
                    }
                case Keys.Delete:
                    {
                        Debug.Write("Delete");
                        break;
                    }
                    
            }

        }

        private void posNegClick(object sender, EventArgs e)
        {

        }

        // Clears the memory, setting it to blank
        private void mcClick(object sender, EventArgs e)
        {
            if (!on) { return; }
            memCalc = "";
        }

        // Stores the current display to memory
        private void msClick(object sender, EventArgs e)
        {
            if (!on) { return; }
        }

        // Adds the current calculation to memory
        private void mplusClick(object sender, EventArgs e)
        {

        }

        // Returns calculation from memory
        private void mrClick(object sender, EventArgs e)
        {

        }
    }
}
