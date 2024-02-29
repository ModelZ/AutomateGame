using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateGame.automation
{
    class Threads
    {
        static void Mains(string[] args)
        {
            Thread t1 = new Thread(new ThreadStart(Thread1));
            Thread t2 = new Thread(new ThreadStart(Thread2));

            t1.Start();
            t2.Start();
        }

        static void Thread1()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Thread 1");
            }
        }

        static void Thread2()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Thread 2");
            }
        }
    }
}
