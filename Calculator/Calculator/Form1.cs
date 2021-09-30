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
        Calculation calc = new Calculation();
        char ops, op;
        bool allowDecimal, valset = true, opera = false;
         private List<string> opr = new List<string>()
        {
            "+", "-", "*", "/"
        };
        public Form1()
        {
            InitializeComponent();
        }


        private void btn_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            bool strVal = true;
            if (btn.Text == "√" || btn.Text == "²")
            {
                strVal = false;
            }
            bool seen = false;
            //Check if operator is clicked
            foreach (string oprs in opr)
            {
                if (btn.Text == oprs)
                {
                    opera = true;
                    ops = Convert.ToChar(btn.Text);
                }
                if (calc.getValue().Contains(oprs))
                {
                    seen = true;
                    op = Convert.ToChar(oprs);
                }

                if ((btn.Text == oprs || btn.Text == ".") && calc.getValue().Length == 0)
                {
                    calc.setValue("0");
                }
            }
            /** Ends here **/
            if (seen && opera && valset)
            {
                this.Compute("val", op);
            }
            //Check for an operator before square root
            else if (!strVal && seen && valset)
            {
                this.Compute("val", op);
                if(btn.Text == "²")
                    this.Compute(this.textBox1.Text, '²');
                if (btn.Text == "√")
                    this.Compute(this.textBox1.Text, '√');
            }
            else if (!strVal && valset)
            {
                if (calc.getValue() == "")
                    calc.setValue("0");

                if (btn.Text == "²")
                    this.Compute(this.textBox1.Text, '²');
                if (btn.Text == "√")
                    this.Compute(this.textBox1.Text, '√');
            }

            if (strVal)
            {
                //Check reoccurrence of operator
                foreach (string oprs in opr)
                {
                    if (opera && this.textBox1.Text.Contains(oprs) && !valset)
                    {
                        string nw = calc.getValue().Remove(calc.getValue().Length - 1);
                        calc.ResetValue();
                        calc.setValue(nw);
                    }
                }
                /** Ends here **/
                if (!allowDecimal && btn.Text == "." && this.textBox1.Text.Contains(".")) ;
                else
                {
                    if (opera)
                    {
                        valset = false;
                        opera = false;
                    }
                    else if(!valset && btn.Text == ".")
                        valset = false;
                    else
                        valset = true;

                    calc.setValue(btn.Text);
                    this.textBox1.Text = calc.getValue();
                }

                //Allow decimal point after operator
                if (calc.CheckOperator(btn.Text))
                    allowDecimal = true;
                else
                    allowDecimal = false;
            }
        }

        private void Compute(string arg, char arg1)
        {
            this.textBox1.Text = calc.getResult(arg, arg1).ToString();
            calc.ResetValue();
            calc.setValue(this.textBox1.Text);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            int a = 1;
            foreach (string oprs in opr)
            {
                if (calc.getValue().Contains(oprs))
                {
                    a = 0;
                }
            }

            if (!valset || (!valset && !allowDecimal))
            {
                calc.ResetValue();
                this.textBox1.Text = calc.ThrowError("Syntax error");
            }
            else if (valset && a == 1) ;

            else
                this.Compute("val", ops);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = calc.ResetValue();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (calc.getValue().Length > 1)
            {
                string nw = calc.getValue().Remove(calc.getValue().Length - 1);
                calc.ResetValue();
                calc.setValue(nw);
                this.textBox1.Text = calc.getValue();
            }
            else
            {
                this.textBox1.Text = calc.ResetValue();
            }
        }
    }
}
