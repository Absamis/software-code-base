using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Game
{
    class Setting
    {
       private FileStream file;
       public Setting()
        {
            
        }
        public void resetFile(string pth)
        {
            file = new FileStream(pth, FileMode.Open);
            file.SetLength(0);
            file.Close();
        }
        public void Append_Data(string txt, string pth)
        {
            byte[] cnt = Encoding.ASCII.GetBytes(txt + "\n");
            file = File.Open(pth, FileMode.Append);
            file.Write(cnt, 0, cnt.Length);
            //Console.WriteLine("lloooo     "+txt);
            file.Close();
        }
        public bool FileEmpty(string pth)
        {
            file = File.Open(pth, FileMode.Open);
            long len = file.Length;

            file.Close();
            if (len == 0)
                return true;
            else
                return false;

        }
        public string fetch_Data(string pth)
        {
            file = new FileStream(pth, FileMode.Open);
            StreamReader strd = new StreamReader(file);
            string sets = strd.ReadLine();
            file.Close();
            return sets;
        }
        public string[] fetch_Data(string pth, bool all)
        {
            List<string> alltxt = new List<string>();
            file = new FileStream(pth, FileMode.Open);
            StreamReader strd = new StreamReader(file);
            string st = strd.ReadLine();
            while(st != null)
            {
                alltxt.Add(st);
                st = strd.ReadLine();
            }
            file.Close();
            return alltxt.ToArray();
        }
        public void write_Data(string pth, string txt)
        {
            try
            {
                byte[] cnt = Encoding.ASCII.GetBytes(txt + "\n");
                file = new FileStream(pth, FileMode.Open);
                file.SetLength(0);
                file.Write(cnt, 0, cnt.Length);
                file.Close();
            }
            catch (Exception e) { Console.WriteLine("Error : " + e); }
        }
    }
}
