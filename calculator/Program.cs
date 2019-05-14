using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    class Program
    {
        static string[] a = { "0" };
        static char[] signs = { '+', '-', '*', '/' };
        static double[] num;//有哪些數字
        static char[] sign;//有哪些運算符號
        static int[] sign_position;//運算符號在哪些位置，分割數字用
        static void Main(string[] args)
        {
            string formula = Console.ReadLine();//輸入運算
            char[] char_formula = formula.ToCharArray();//轉為字元陣列
            sign = new char[Num_and_Sign(char_formula)];//運算符號陣列長度
            sign_position = new int[sign.Length + 2];//運算符號位置的陣列長度
            num = new double[sign.Length + 1];//數字的陣列長度
            Sign_Position(char_formula);
            Console.WriteLine(Answer());

            Main(a);
        }

        //判斷有幾個運算符號來指定陣列長度
        static int Num_and_Sign(char[] formula)
        {
            int count = 0;
            for (int i = 0; i < formula.Length; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    if (formula[i] == signs[j])
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        //第幾位是運算符號，分別是什麼運算符號
        //分割數字
        static void Sign_Position(char[] formula)
        {
            string number;
            for (int i = 0, k = 0; i < formula.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    //第i位是運算符號時執行
                    if (formula[i] == signs[j])
                    {
                        sign[k] = signs[j];//將運算符號存在陣列sign
                        sign_position[k + 1] = i;//判斷第幾位是運算符號，以便分割數字
                        k++;
                    }
                }
            }

            //設定sign_position頭尾的數字
            sign_position[0] = -1;                                   
            sign_position[sign_position.Length - 1] = formula.Length;

            //分割數字後存進num陣列
            for (int i = 0; i < sign_position.Length - 1; i++)
            {
                number = new string(formula, sign_position[i] + 1, sign_position[i + 1] - sign_position[i] - 1);//分割數字
                num[i] = Convert.ToDouble(number);//存進陣列
            }
        }

        //先乘除後加減，回傳答案
        static double Answer()
        {
            int count = 0;//已經連續做了幾個乘除運算
            //先乘除
            for (int i = 0; i < sign.Length; i++)
            {
                if (sign[i] == signs[2])//運算符號為*時
                {
                    num[i - count] = num[i - count] * num[i + 1];
                    count++;
                }
                else if (sign[i] == signs[3])//運算符號為/時
                {
                    num[i - count] = num[i - count] / num[i + 1];
                    count++;
                }
                else//若沒有連續乘除，count歸零
                {
                    count = 0;
                }
            }
            //後加減
            double ans = num[0];
            for (int i = 0; i < sign.Length; i++)
            {
                if (sign[i] == signs[0])
                {
                    ans += num[i + 1];
                }
                else if (sign[i] == signs[1])
                {
                    ans -= num[i + 1];
                }
            }
            return ans;
        }
    }
}
