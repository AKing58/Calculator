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
        static List<string> tempList = new List<string> { "" };

        String curNum = "";
        bool on = false;
        int bracketCount = 0;
        String memCalc = "0";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            inputs.Clear();
            tempList.Clear();
        }

        // Turns "on" the calculator
        private void onButton(object sender, EventArgs e)
        {
            if(!on)
                inputTxt.Text = "0";
            on = true;
            
        }

        private void printDisplay(List<string> inputList)
        // Print function that copies the list to a single string
        {
            if(inputList.Count==0 && curNum == "")
            {
                inputTxt.Text = "0";
                return;
            }
                
            inputTxt.Text = "";
            if(curNum != "" && double.Parse(curNum)!=0)
                curNum = curNum.TrimStart('0');
            for (int i = 0; i < inputList.Count; i++)
            {
                inputTxt.Text += inputList[i];
            }
            
            inputTxt.Text += curNum;
            
        }

        // Adds numbers to the display
        private void numClick(object sender, EventArgs e)
        {
            if (!on) { return; }

            Button thisBtn = (Button)sender;
            numDo(thisBtn.Text);
        }

        private void numDo(string numToAdd)
        {
            if (inputs.Count > 0 && inputs[inputs.Count - 1] == ")")
                inputs.Add("x");
            curNum += numToAdd;
            printDisplay(inputs);
        }

        // Adds operations to the display
        private void opClick(object sender, EventArgs e)
        {
            if (!on) { return; }
            Button thisBtn = (Button)sender;
            opDo(thisBtn.Text);
        }

        private void opDo(string opToAdd)
        {
            if (curNum != "" && inputs.Count == 0)
            {
                inputs.Add(curNum);
                curNum = "";
                inputs.Add(opToAdd);
            }
            else if (inputs.Count > 0)
            {
                if ((inputs[inputs.Count - 1] == "+" && curNum == "") ||
               (inputs[inputs.Count - 1] == "-" && curNum == "") ||
               (inputs[inputs.Count - 1] == "x" && curNum == "") ||
               (inputs[inputs.Count - 1] == "/" && curNum == ""))
                {
                    inputs.RemoveAt(inputs.Count - 1);
                    inputs.Add(opToAdd);
                }
                else
                {
                    if (inputs[inputs.Count - 1] != ")" && curNum!="")
                    {
                        inputs.Add(curNum);
                        curNum = "";
                    }
                    inputs.Add(opToAdd);
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
            equalsDo();
        }

        private void equalsDo()
        {
            if (curNum != "")
            {
                inputs.Add(curNum);
                curNum = "";
            }
            tempList = deleteExtraBrackets(inputs);

            while (hasBracket(tempList))
            {
                bedmas(findBracketLoc(tempList), tempList);
            }
            if (curNum == "" && inputs.Count == 0)
            {
                inputs.Add("0");
                tempList.Add("0");
            }
            else if (curNum == "")
            {
                if (tempList[tempList.Count - 1] == "+" ||
                tempList[tempList.Count - 1] == "-" ||
                tempList[tempList.Count - 1] == "x" ||
                tempList[tempList.Count - 1] == "/")
                {
                    inputs.RemoveAt(inputs.Count - 1);
                    tempList.RemoveAt(tempList.Count - 1);
                }

            }
            foreach (var things in tempList)
            {
                Debug.Write(things);
            }
            Debug.WriteLine("\nNext");

            foreach (var things in inputs)
            {
                Debug.Write(things + "/");
            }
            Debug.WriteLine("|");
            bedmas(0, tempList);
            inputs = tempList;
            printDisplay(inputs);
        }

        // Performs order of operations on the list of strings
        private double bedmas(int startLoc, List<string> inList)
        {
            double temp = 0;
            int i;
            if (hasBracket(tempList)) {
                tempList.RemoveAt(startLoc);
                bracketCount--;
            }

            for (i = startLoc; i < tempList.Count; i++)
            {
                if(tempList[i] == ")") { break; }
                if (tempList[i] == "x") {
                    temp = double.Parse(tempList[i - 1]) * double.Parse(tempList[i + 1]);
                    tempList[i - 1] = temp.ToString();
                    tempList.RemoveAt(i);
                    tempList.RemoveAt(i);
                    i -= 2;
                }
                else if(tempList[i] == "/") {
                    temp = double.Parse(tempList[i - 1]) / double.Parse(tempList[i + 1]);
                    tempList[i - 1] = temp.ToString();
                    tempList.RemoveAt(i);
                    tempList.RemoveAt(i);
                    i -= 2;
                }
            }

            for (i = startLoc; i < tempList.Count; i++)
            {
                if (tempList[i] == ")") { break; }
                if (tempList[i] == "+")
                {
                    Debug.WriteLine("Before and After i: " + tempList[i - 1] + ", " + tempList[i + 1]);
                    temp = double.Parse(tempList[i - 1]) + double.Parse(tempList[i + 1]);
                    tempList[i - 1] = temp.ToString();
                    tempList.RemoveAt(i);
                    tempList.RemoveAt(i);
                    i -= 2;
                }
                else if (tempList[i] == "-")
                {
                    temp = double.Parse(tempList[i - 1]) - double.Parse(tempList[i + 1]);
                    tempList[i - 1] = temp.ToString();
                    tempList.RemoveAt(i);
                    tempList.RemoveAt(i);
                    i -= 2;
                }
            }
            
            if (bracketCount != 0 && tempList[i] == ")")
            {
                tempList.RemoveAt(i);
                bracketCount++;
            }
            if (tempList.Count == 0)
                return (double)0;
            else
                return double.Parse(tempList[startLoc]);
        }

        // Adds a decimal to the display
        private void decClick(object sender, EventArgs e)
        {

        }

        // Clears the display
        private void clearInput(object sender, EventArgs e)
        {
            if (!on) { return; }
            inputTxt.Text = "0";
            curNum = "";
            inputs.Clear();
            tempList.Clear();
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
                            opDo("x");
                            break;
                        }
                        else if (e.KeyCode == Keys.D9 && e.Shift)
                        {
                            Debug.Write("(");
                            addLBDo();
                            break;
                        }
                        else if (e.KeyCode == Keys.D0 && e.Shift)
                        {
                            Debug.Write("(");
                            addRBDo();
                            break;
                        }
                        else
                        {
                            Debug.Write((int)(e.KeyCode - 48));
                            numDo(((int)(e.KeyCode - 48)).ToString());
                            break;
                        }
                    }
                case Keys.Oemplus: 
                    {
                        if (e.KeyCode == Keys.Oemplus && e.Shift)
                        {
                            Debug.Write("+");
                            opDo("+");
                            break;
                        } else
                        {
                            Debug.Write("=");
                            equalsDo();
                            break;
                        }
                    }
                case Keys.OemMinus:
                    {
                        Debug.Write("-");
                        opDo("-");
                        break;
                    }
                case Keys.OemQuestion:
                    {
                        Debug.Write("/");
                        opDo("/");
                        break;
                    }
                case Keys.Back:
                    {
                        Debug.Write("Backspace");
                        backspaceDo();
                        break;
                    }
                case Keys.Delete:
                    {
                        Debug.Write("Delete");
                        CEDo();
                        break;
                    }

            }

        }

        private void backspaceDo()
        {
            if(curNum!="")
                curNum = curNum.Remove(curNum.Length - 1);
            
            printDisplay(inputs);
        }

        private void posNegClick(object sender, EventArgs e)
        {
            if (curNum == "")
                return;
            if (curNum.Substring(0, 1) != "-")
                curNum = "-" + curNum;
            else
                curNum = curNum.TrimStart('-');
            printDisplay(inputs);
        }

        /**
         * Finds the position of the last left bracket
         */
        private int findBracketLoc(List<string> inputList)
        {
            for(int i = inputs.Count-1; i >= 0; i--)
            {
                if (inputs[i] == "(")
                    return i;
            }
            return -1;
        }

        private bool hasBracket(List<string> inputList)
        {
            foreach (var bracket in inputList)
                if (bracket == "(")
                    return true;
            return false;
        }

        private List<string> deleteExtraBrackets(List<string> inputList)
        {
            int lastLocation = 0;
            for (int i = inputList.Count - 1; i >= 0; i--)
            {
                if (inputList[i] == ")")
                    lastLocation = i;
            }

            while (bracketCount > 0)
            {
                for(int i=lastLocation; i<inputList.Count; i++) {
                    if(inputList[i] == "(")
                    {
                        inputList.RemoveAt(i);
                    }
                }
                bracketCount--;
            }
            return inputList;
        }


        private void addLeftBracket(object sender, EventArgs e)
        {
            if (!on) { return; }
            addLBDo();
        }
        private void addLBDo()
        {
            
            bracketCount++;
            if (curNum != "")
            {
                inputs.Add(curNum);
                curNum = "";
            }
            if (inputs.Count > 0 &&
                inputs[inputs.Count - 1] != "+" &&
                inputs[inputs.Count - 1] != "-" &&
                inputs[inputs.Count - 1] != "x" &&
                inputs[inputs.Count - 1] != "/")
            {
                inputs.Add("x");
            }
            inputs.Add("(");
            printDisplay(inputs);
        }

        private void addRightBracket(object sender, EventArgs e)
        {
            if (!on) { return; }
            addRBDo();
        }

        private void addRBDo()
        {
            if (!on) { return; }
            if (bracketCount <= 0)
                return;
            bracketCount--;
            if (curNum != "")
            {
                inputs.Add(curNum);
                curNum = "";
            }
            inputs.Add(")");
            printDisplay(inputs);
        }
        // Clears the memory, setting it to blank
        private void mcClick(object sender, EventArgs e)
        {
            if (!on) { return; }
            memCalc = "0";
        }

        // Stores the current display to memory
        private void msClick(object sender, EventArgs e)
        {
            if (!on) { return; }
            if (curNum == "")
            {
                memCalc = inputs[inputs.Count - 1];
            }
            else
            {
                memCalc = curNum;
            }
        }

        // Adds the current calculation to memory
        private void mplusClick(object sender, EventArgs e)
        {
            if (!on) { return; }
            
            double tempMem = double.Parse(memCalc);
            if (curNum == "")
            {
                tempMem = tempMem + double.Parse(inputs[inputs.Count - 1]);
            }
            else
            {
                tempMem = tempMem + double.Parse(curNum);
            }
            memCalc = tempMem.ToString();
        }

        // Returns calculation from memory
        private void mrClick(object sender, EventArgs e)
        {
            if (!on) { return; }
            if(inputs.Count > 0 && inputs[inputs.Count - 1].All(c => char.IsDigit(c))){
                inputs.RemoveAt(inputs.Count - 1);
                inputs.Add(memCalc);
            }
            else
            {
                curNum = memCalc;
            }

            printDisplay(inputs);
        }

        private void CEClick(object sender, EventArgs e)
        {
            if (!on) { return; }
            CEDo();
        }

        private void CEDo()
        {
            if(inputs.Count == 0)
                return;
            if (inputs[inputs.Count - 1] != "+" &&
                inputs[inputs.Count - 1] != "-" &&
                inputs[inputs.Count - 1] != "x" &&
                inputs[inputs.Count - 1] != "/")
                inputs.RemoveAt(inputs.Count - 1);
            curNum = "";
            printDisplay(inputs);
        }

        private void backspaceClick(object sender, EventArgs e)
        {
            backspaceDo();
        }
    }
}
