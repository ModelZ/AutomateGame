using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace AutomateGame
{
    class Autojump
    {
        // import the function in your class
        [DllImport("user32.dll")]
        static extern int SetForegroundWindow(IntPtr point);
    

        static void keyListener(InputSimulator isim, ref bool active, VirtualKeyCode TOGGLEKEY, VirtualKeyCode EXITKEY)
        {
            Console.WriteLine("Thread Active....");
            bool isKeyUp = true;
            while (true)
            {
                if (isim.InputDeviceState.IsHardwareKeyDown(TOGGLEKEY))
                {
                    // Block Repeating Toggle
                    if (isKeyUp)
                    {
                        active = !active;
                        isKeyUp = false;
                        Console.WriteLine("Toggle: " + active);
                    }

                }
                if (isim.InputDeviceState.IsHardwareKeyUp(TOGGLEKEY))
                {
                    isKeyUp = true;
                }

                if (isim.InputDeviceState.IsHardwareKeyDown(EXITKEY))
                {
                    Console.WriteLine("Exit Program Successfully!");
                    System.Environment.Exit(0);
                }

                // Add some delay to avoid high CPU usage
                Thread.Sleep(100);
            }

        }

        static void Main(string[] args)
        {
            // Get the array of process run by this name
            Process[] ps = Process.GetProcessesByName("granblue_fantasy_relink");
           /* // Print the number of array of processes
            Console.WriteLine(ps.Length);*/

/*            // Loop out to check array of processes contents 
            foreach (Process p in ps)
            {
                Console.WriteLine(p);
            }*/

            // Get the first array of process
            Process minecraftProcess = ps.FirstOrDefault();
/*            //Print out minecraftProcess
            Console.WriteLine(minecraftProcess);*/

            if (minecraftProcess == null)
            {
                Console.WriteLine("Error: No Process Found");
                System.Environment.Exit(-1);
            }

            // Call InputSimulator Object
            InputSimulator isim = new InputSimulator();

            // Thread function for Listening Keyboard Input
            bool active = false;
            Thread listener = new Thread(() => keyListener(isim, ref active, VirtualKeyCode.VK_X, VirtualKeyCode.VK_Z));
            listener.Start();

            while (true)
            {
                // If toggle key is on
                if(active)
                {

                }
            }


        }






    }
}
