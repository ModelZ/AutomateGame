using System;
using System.Threading;

class ThreadParameter
{
    static void Mains(string[] args)
    {
        Thread thread1 = new Thread(Hello);
        thread1.Start("Marcus");

        Parameter param = new Parameter();
        param.name = "Bob";
        param.word = "How are you going?";

        Thread thread2 = new Thread(Question);
        thread2.Start(param);
    }

    static void Hello (object name)
    {
        Console.WriteLine("Hello " + name);
    }

    static void Question (object data) {
        Parameter param = (Parameter) data;
        Console.WriteLine(param.name + ", " + param.word);
    } 

    class Parameter {
        public string name;
        public string word;
    }
}