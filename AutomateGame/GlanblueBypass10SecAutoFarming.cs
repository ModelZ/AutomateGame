using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;
using WindowsInput;
using AutomateGame.opencv;

namespace AutomateGame
{
    class GlanblueBypass10SecAutoFarming
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
                    // Release key when Thread terminate this program
                    Console.WriteLine("Release Key");
                    isim.Keyboard.KeyUp(VirtualKeyCode.VK_W);
                    isim.Keyboard.KeyUp(VirtualKeyCode.VK_E);

                    Console.WriteLine("Exit Program Successfully!");
                    Environment.Exit(0);
                }

                // Add some delay to avoid high CPU usage
                Thread.Sleep(100);
            }

        }

        static void runAndLockOn(InputSimulator isim, ref bool active, ref bool isKeyDown)
        {
            

            // If toggle key is on and not keydown
            if (active && !isKeyDown)
            {

                Console.WriteLine("Holding Key");
                isKeyDown = true;
                isim.Keyboard.KeyDown(VirtualKeyCode.VK_W);
                isim.Keyboard.KeyDown(VirtualKeyCode.VK_E);
            }

            // If toggle key is on and keydown
            if (!active && isKeyDown)
            {

                Console.WriteLine("Release Key");
                isKeyDown = false;
                isim.Keyboard.KeyUp(VirtualKeyCode.VK_W);
                isim.Keyboard.KeyUp(VirtualKeyCode.VK_E);

                // ***ALWAYS*** Release key when Thread terminate this program
            }
        }

        static void Mains(string[] args)
        {
            // Get the array of process run by this name
            Process[] ps = Process.GetProcessesByName("granblue_fantasy_relink");

            // Get the first array of process
            Process GameProcess = ps.FirstOrDefault();

            if (GameProcess == null)
            {
                Console.WriteLine("Error: GranblueFantasyRelink Process not Found");
                Environment.Exit(-1);
            }
            

            Console.WriteLine("Bringing Game on Focus....");

            // bring game to focus
            IntPtr h = GameProcess.MainWindowHandle;
            SetForegroundWindow(h);

            // Call InputSimulator Object
            InputSimulator isim = new InputSimulator();

            // Left Click to hide mouse focus into the game (For FullScreen Game)
            Thread.Sleep(50);
            isim.Mouse.LeftButtonDown();
            Thread.Sleep(50);
            isim.Mouse.LeftButtonUp();

            // Thread Delay for 1 second
            Thread.Sleep(1000);


            // Toggle Automate
            bool active = false;

            // Thread function for Listening Keyboard Input
            Thread listener = new Thread(() => keyListener(isim, ref active, VirtualKeyCode.VK_X, VirtualKeyCode.VK_Z));
            listener.Start();

            // For blocking KeyHolding Loop
            bool isKeyDown = false;

            while (true)
            {
                // Hold W Key and E Key
                runAndLockOn(isim, ref active, ref isKeyDown);


                // If toggle key is on
                if (active)
                {
                    // Do Something

                    


                }
            }
        }

    }
}
