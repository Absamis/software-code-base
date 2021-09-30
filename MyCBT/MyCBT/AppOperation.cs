using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace MyCBT
{
    class AppOperation
    {
        private FileStream que;
        private int sbjtcnt, anscnt, quescnt;
        public int qnum;
        private Random rand;
        private StreamReader read;
        public bool nx, strt;
        private int[,] quesDone;
        string[,] quesDoneAns, ansChose;
        private string question;
        public string[] option;
        public int sub = 4, ans = 10;
        string[] sbj = { "Mathematics", "English", "Chemistry", "Physics" };
        string[] subjects = { @"C:/Users/Absam/Documents/math.abs", @"C:/Users/Absam/Documents/English.abs", @"C:/Users/Absam/Documents/Chemistry.abs", @"C:/Users/Absam/Documents/Physics.abs" };
        public AppOperation()
        {
            rand = new Random();
            quesDone = new int[sub, ans];
            quesDoneAns = new string[sub, ans];
            ansChose = new string[sub, ans];
            sbjtcnt = 0;
            quescnt = 0;
            option = new string[4];
            strt = true;
            qnum = 1;
            nx = false;

        }
        public void setQuestion(int arg1, int arg2)
        {
            try
            {
                que = new FileStream(subjects[arg1], FileMode.Open);
                read = new StreamReader(que);
                for (int i = 1; i <= arg2; i++)
                {
                    string[] words = read.ReadLine().Split('`');
                    question = words[0];
                    option = words[1].Split(',');
                    quesDone[arg1, quescnt] = arg2;
                    quesDoneAns[arg1, quescnt] = words[2];
                 }
                quescnt++;
                read.Close();
                que.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                read.Close();
                que.Close();
            };
        }
        public void QuestionNavigation()
        {
            if (qnum > quesDone.GetLength(1) && sbjtcnt == sub - 1)
            {
                nx = true;
                qnum = quesDone.GetLength(1);
            }
            else if (qnum <= 0 && sbjtcnt == 0)
            {
                qnum = 1;
                nx = true;
                //Console.WriteLine("Quu");
            }
            else if (qnum > quesDone.GetLength(1))
            {
                nx = true;
                sbjtcnt++;
                quescnt = 0;
                qnum = 1;
                Console.WriteLine("Nxt ");
                if (nx && subQuesLength(sbjtcnt) > 0)
                {
                    nx = false;
                }
                else
                {
                    //Console.WriteLine(sbjtcnt + "opiuu");
                    loadQuestion(sbjtcnt);
                    int n = quesDone[sbjtcnt, quescnt];
                    setQuestion(sbjtcnt, n);
                }
            }
            else if (qnum == 0 && sbjtcnt > 0)
            {
                nx = true;
                sbjtcnt--;
                qnum = ans;
                quescnt = subQuesLength(sbjtcnt) - 1;
                int n = quesDone[sbjtcnt, quescnt];
                setQuestion(sbjtcnt, n);
            }
            if (nx)
            {
                nx = false;
            }
            else
            {
                Console.WriteLine("Entered;");
                //Next Question
                if (qnum > subQuesLength(sbjtcnt))
                {
                    int n = setNum(sbjtcnt);
                    setQuestion(sbjtcnt, n);
                    Console.WriteLine("Next" + qnum);
                    //setQuestion(sbjtcnt, quescnt);
                }
                else if (qnum <= subQuesLength(sbjtcnt) && qnum > quescnt)
                {
                    int n = quesDone[sbjtcnt, quescnt];
                    setQuestion(sbjtcnt, n);
                }
                else
                {
                    quescnt -= 2;
                    int n = quesDone[sbjtcnt, quescnt];
                    setQuestion(sbjtcnt, n);
                }
            }
            if (strt)
            {
                loadQuestion(sbjtcnt);
                nx = false;
                strt = false;
            }
        }
        public void loadQuestion(int arg)
        {
            List<int> num = new List<int>();
            num.Clear();
            int nums = 0;
            for (int i = 0; i < ans; i++)
            {
                que = new FileStream(subjects[arg], FileMode.Open);
                read = new StreamReader(que);
                bool seen = false;
                while (!seen)
                {
                    seen = true;
                    nums = rand.Next(1, ans + 1);
                    foreach(int elem in num)
                    {
                        if (elem == nums)
                            seen = false;
                    }
                }
                num.Add(nums);
                List<string> words = new List<string>();
                for (int j = 1; j <= nums; j++)
                {
                    words = new List<string>();
                    words.Clear();
                    words.AddRange(read.ReadLine().Split('`').ToList());
                }

                if (quesDone[arg, i] == 0)
                {
                    quesDone[arg, i] = nums;
                    quesDoneAns[arg, i] = words[2];
                }
                que.Close();
                read.Close();
            }
        }
        public void storeAns(string arg)
        {
            ansChose[sbjtcnt, quescnt - 1] = arg;
        }
        public string fetchAnswer()
        {
            Console.WriteLine("Koop " + quescnt);
            return ansChose[sbjtcnt, quescnt - 1];
        }
        public int setNum(int arg1)
        {
            bool seen = false;
            int nums = 0;
            while (!seen)
            {
                nums = rand.Next(1, ans + 1);
                seen = true;
                for (int i = 0; i < quesDone.GetLength(1); i++)
                {
                    if (quesDone[arg1, i] == nums)
                    {
                        seen = false;
                    }
                }
            }
            return nums;
        }
        public string[] LoadOption()
        {
            return option;
        }
        public int subQuesLength(int arg1)
        {
            int lnt = 0;
            for (int x = 0; x < quesDone.GetLength(0); x++)
            {
                for (int y = 0; y < quesDone.GetLength(1); y++)
                {
                    if (x == arg1 && quesDone[x, y] != 0)
                        lnt++;
                }
                if (x == arg1)
                    break;
            }
            return lnt;
        }
        public string showQuestion()
        {
            return question;
        }
        public string ShowSbj()
        {
            return sbj[sbjtcnt];
        }
        public string ComputeAnswer()
        {
            int mrk = 0;
            for (int x = 0; x < quesDoneAns.GetLength(0); x++)
            {
                for (int y = 0; y < quesDoneAns.GetLength(1); y++)
                {
                    if (ansChose[x, y] == quesDoneAns[x, y] && quesDoneAns[x, y] != null)
                        mrk++;
                }
            }
            int total = quesDone.GetLength(0) * quesDone.GetLength(1);
            return mrk + " / " + total;
        }
        public void fetchSubject(int arg)
        {
            //nx = true;
            if (arg != 0 && subQuesLength(arg-1) < ans)
                loadQuestion(arg - 1);


            qnum = 1;
            sbjtcnt = arg;
            quescnt = 0;
            if (subQuesLength(arg) == 0)
            {
                loadQuestion(sbjtcnt);
                int n = quesDone[sbjtcnt, quescnt];
                setQuestion(sbjtcnt, n);
            }
            else
            {
                int n = quesDone[sbjtcnt, quescnt];
                setQuestion(sbjtcnt, n);
            }
        }
        public void fetchQuestion(int arg)
        {
            qnum = arg;
            quescnt = arg - 1;
            int n = quesDone[sbjtcnt, quescnt];
            setQuestion(sbjtcnt, n);
        }
    }
}
