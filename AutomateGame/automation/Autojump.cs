using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace AutomateGame.automation
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
                    Environment.Exit(0);
                }

                // Add some delay to avoid high CPU usage
                Thread.Sleep(100);
            }

        }

        static void Mains(string[] args)
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
            Process GameProcess = ps.FirstOrDefault();
            /*            //Print out GameProcess
                        Console.WriteLine(GameProcess);*/

            if (GameProcess == null)
            {
                Console.WriteLine("Error: No Process Found");
                Environment.Exit(-1);
            }


            Console.WriteLine("Bringing Game on Focus....");

            // bring game to focus
            IntPtr h = GameProcess.MainWindowHandle;
            SetForegroundWindow(h);

            // Call InputSimulator Object
            InputSimulator isim = new InputSimulator();

            // Left Click to hide mouse focus into the game
            Thread.Sleep(50);
            isim.Mouse.LeftButtonDown();
            Thread.Sleep(50);
            isim.Mouse.LeftButtonUp();

            // Thread Delay for 1 second
            Thread.Sleep(1000);



            // Thread function for Listening Keyboard Input
            bool active = false;
            Thread listener = new Thread(() => keyListener(isim, ref active, VirtualKeyCode.VK_X, VirtualKeyCode.VK_Z));
            listener.Start();


            while (true)
            {
                // If toggle key is on
                if (active)
                {
                    // Jumping Routine
                    // Press SPACE Key every 2 seconds
                    Thread.Sleep(2000);
                    Console.WriteLine("Sending jump");

                    isim.Keyboard.KeyDown(VirtualKeyCode.SPACE);
                    Thread.Sleep(50);
                    isim.Keyboard.KeyUp(VirtualKeyCode.SPACE);
                }
            }
        }









    }
}
