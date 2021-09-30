using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Calculation
    {
        bool result = false;
        private string value = "";
        private List<string> opr = new List<string>()
        {
            "+", "-", "*", "/"
        };
        public void setValue(string arg)
        {
            this.value += arg;
        }
        public string getValue()
        {
            return this.value;
        }
        public string ResetValue()
        {
            this.value = "";
            return "0";
        }
        public void deleteValue()
        {
            if(this.value.Length > 0)
                this.value.Remove(value.Length - 1);
        }
        public bool CheckOperator(string oprt)
        {
            foreach(string oprs in opr)
            {
                if(oprs == oprt)
                {
                    result = true;
                }else if(oprt == ".")
                {
                    result = false;
                }
            }
            return result;
        }

        public string ThrowError(string error)
        {
            return error;
        }
        public double getResult(string val, char opr)
        {
            if (opr == '√')
            {
                double num1 = Convert.ToDouble(val);
                return Math.Sqrt(num1);
            }
            else if (opr == '²')
            {
                double num1 = Convert.ToDouble(val);
                return num1*num1;
            }
            else {
                string[] nums = value.Split(opr);
                double num1 = Convert.ToDouble(nums[0]);
                double num2 = Convert.ToDouble(nums[1]);
                try
                {
                    switch (opr)
                    {
                        case '+':
                            return num1 + num2;
                            break;
                        case '-':
                            return num1 - num2;
                            break;
                        case '*':
                            return num1 * num2;
                            break;
                        case '/':
                            return num1 / num2;
                            break;
                        default:
                            return 0;
                            break;
                    }
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
        }
    }
}