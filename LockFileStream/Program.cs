using System;
using System.IO;
using System.Threading;

namespace LockFileStream
{
    class DataAccess
    {
        internal void EnterDetails(string data)
        {
            FileStream fs = new FileStream(@"C:/Users/Admin/Desktop/stud.txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(data);
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        public void PrintData()
        {
            Monitor.Enter(this);
            string[] dataArr = { "This", "is", "an", "Example", "of", "Locking", "in", "C#" };
            foreach (string s in dataArr)//lock(this)
            {
                EnterDetails(s);
            }
            Monitor.Exit(this);
        }
    }
    class Program
    {
        static DataAccess da = new DataAccess();

        public static void Thread1()
        {
            Console.WriteLine("Thread 1 Writing");
            da.PrintData();
        }
        public static void Thread2()
        {
            Console.WriteLine("Thread 2 Writing");
            da.PrintData();
        }
        static void Main(string[] args)
        {
            ThreadStart ts1 = new ThreadStart(Thread1);
            ThreadStart ts2 = new ThreadStart(Thread2);

            Thread t1 = new Thread(ts1);
            Thread t2 = new Thread(ts2);

            t1.Start();
            t2.Start();
            Console.ReadKey();
        }
    }
}