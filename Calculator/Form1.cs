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
        static List<string> tempList = new List<string> { "" };

        String curNum = "";
        double total;
        bool on = false;
        int bracketCount = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            inputs.Clear();
            tempList.Clear();
        }

        private void onButton(object sender, EventArgs e)
        {
            on = true;
            inputTxt.Text = "0";
        }

        private void printDisplay(List<string> inputList)
        {
            inputTxt.Text = "";
            for (int i = 0; i < inputList.Count; i++)
            {
                inputTxt.Text += inputList[i];
            }
            inputTxt.Text += curNum;
        }

        private void numClick(object sender, EventArgs e)
        {
            if (!on) { return; }
            Button thisBtn = (Button) sender;
            curNum += thisBtn.Text;
            printDisplay(inputs);
        }

        
        private void opClick(object sender, EventArgs e)
        {
            if (!on) { return; }
            Button thisBtn = (Button)sender;
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
                    if(inputs[inputs.Count - 1] != ")")
                    {
                        inputs.Add(curNum);
                        curNum = "";
                    }
                    inputs.Add(thisBtn.Text);
                }
            }
            else
            {
                return;
            }

            printDisplay(inputs);
        }
        

        private void equalsClick(object sender, EventArgs e)
        {
            if (!on) { return; }
            if(curNum != "")
            {
                inputs.Add(curNum);
                tempList.Add(curNum);
                curNum = "";
            }

            tempList = inputs;

            while (hasBracket(tempList))
            {
                bedmas(findBracketLoc(tempList), tempList);
            }
            if (curNum == "" && inputs.Count == 0)
            {
                inputs.Add("0");
                tempList.Add("0");
            }
            else if(curNum == "")
            {
                if (inputs[inputs.Count - 1] == "+" ||
                inputs[inputs.Count - 1] == "-" ||
                inputs[inputs.Count - 1] == "x" ||
                inputs[inputs.Count - 1] == "/") {
                    inputs.RemoveAt(inputs.Count - 1);
                    tempList.RemoveAt(tempList.Count - 1);
                }
                    
            }else
            {
                inputs.Add(curNum);
                tempList.Add(curNum);
                curNum = "";
            }
            
            total = bedmas(0, tempList);
            inputs = tempList;
            printDisplay(inputs);
        }

        private double bedmas(int startLoc, List<string> inList)
        {
            double temp = 0;
            int i;
            if (hasBracket(tempList)) {
                tempList.RemoveAt(startLoc);
                bracketCount--;
            }
            foreach(var value in tempList)
            {
                Debug.Write(value);
                Debug.Write("|");
            }
            Debug.WriteLine("");
            for (i = startLoc; i < tempList.Count; i++)
            {
                if(tempList[i] == ")") { break; }
                if (tempList[i] == "x") {
                    foreach (var value in tempList)
                    {
                        Debug.Write(value);
                    }
                    Debug.WriteLine("");
                    Debug.WriteLine("i: " + i);
                    Debug.WriteLine(tempList[i-1] + " : " + tempList[i+1]);
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

                Debug.WriteLine("Yo: " + tempList.Count);
            }

            for (i = startLoc; i < tempList.Count; i++)
            {
                if (tempList[i] == ")") { break; }
                if (tempList[i] == "+")
                {
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
                Debug.WriteLine("No: " + tempList.Count);
            }


            Debug.WriteLine("bracket" + bracketCount);
            if (bracketCount != 0 && tempList[i] == ")")
            {
                Debug.WriteLine("Templist[i]: " + tempList[i]);
                tempList.RemoveAt(i);
                bracketCount++;
            }

            Debug.WriteLine("------------");
            foreach (var value in tempList)
            {
                Debug.Write(value);
            }
            Debug.WriteLine("");
            Debug.WriteLine("i: " + i);
            Debug.WriteLine("------------");
            return double.Parse(tempList[startLoc]);
        }

        private void decClick(object sender, EventArgs e)
        {

        }

        private void clearInput(object sender, EventArgs e)
        {
            if (!on) { return; }
            inputTxt.Text = "0";
            curNum = "";
            inputs.Clear();
            tempList.Clear();
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

        private void addLeftBracket(object sender, EventArgs e)
        {
            if (!on) { return; }
            bracketCount++;
            if (curNum != "")
            {
                inputs.Add(curNum);
                curNum = "";
            }
            inputs.Add("(");
            printDisplay(inputs);
        }

        private void addRightBracket(object sender, EventArgs e)
        {
            if (!on) { return; }
            bracketCount--;
            if (curNum != "")
            {
                inputs.Add(curNum);
                curNum = "";
            }
            inputs.Add(")");
            printDisplay(inputs);
        }
    }
}
