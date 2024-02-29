using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WindowsInput;

namespace AutomateGame
{
    class Minecraft
    {
        // import the function in your class
        [DllImport("user32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        static void Jumping(InputSimulator isim)
        {
            // Jumping Routine
            // Press SPACE Key every 2 seconds
            Thread.Sleep(2000);
            Console.WriteLine("Sending jump");

            isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.SPACE);
            Thread.Sleep(50);
            isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.SPACE);
        }

        static void placeTorch(InputSimulator isim)
        {
            // Place torch 
            // Click Right Mouse Button every 2 seconds
            Thread.Sleep(2000);
            Console.WriteLine("Place a Torch");
            isim.Mouse.RightButtonDown();
            Thread.Sleep(50);
            isim.Mouse.RightButtonUp();
        }

        static void breakTorch(InputSimulator isim)
        {
            // Break torch 
            // Click Left Mouse Button every 2 seconds
            Thread.Sleep(2000);
            Console.WriteLine("Break a Torch");
            isim.Mouse.LeftButtonDown();
            Thread.Sleep(50);
            isim.Mouse.LeftButtonUp();
        }

        static void keyListener(InputSimulator isim, ref bool active)
        {
            Console.WriteLine("Thread Active....");
            bool isXKeyUp = true;
            while (true)
            {
                if (isim.InputDeviceState.IsHardwareKeyDown(WindowsInput.Native.VirtualKeyCode.VK_X))
                {
                    // Block Repeating Toggle
                    if (isXKeyUp)
                    {
                        active = !active;
                        isXKeyUp = false;
                        Console.WriteLine("Toggle: " +  active);
                    }

                }
                if (isim.InputDeviceState.IsHardwareKeyUp(WindowsInput.Native.VirtualKeyCode.VK_X))
                {
                    isXKeyUp = true;
                }
                
                if (isim.InputDeviceState.IsHardwareKeyDown(WindowsInput.Native.VirtualKeyCode.VK_Z))
                {
                    Console.WriteLine("Exit Program Successfully!");
                    System.Environment.Exit(0);
                }

                // Add some delay to avoid high CPU usage
                Thread.Sleep(100);                
            }

        }


        static void Mains(string[] args)
        {
            // Get the array of process run by this name
            Process[] ps = Process.GetProcessesByName("javaw");
            // Print the number of array of processes
            Console.WriteLine(ps.Length);

            // Loop out to check array of processes contents 
            /*foreach (Process p in ps)
            {
                Console.WriteLine(p);
            }*/

            // Get the first array of process
            Process minecraftProcess = ps.FirstOrDefault();
            //Print out minecraftProcess
            Console.WriteLine(minecraftProcess);

            if (minecraftProcess != null)
            {
                Console.WriteLine("Bringing Minecraft on Focus....");

                // bring minecraft app to focus
                IntPtr h = minecraftProcess.MainWindowHandle;
                SetForegroundWindow(h); 

                // Thread Delay for 1 second
                Thread.Sleep(1000);

                Console.WriteLine("Getting Minecraft out of Game Menu");
                  

                // Getting Minecraft out of Game Menu by press ESC Key
                InputSimulator isim = new InputSimulator();
                isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.ESCAPE);

                // Thread function for Listening Keyboard Input
                bool active = false;
                Thread listener = new Thread(() => keyListener(isim, ref active));
                listener.Start();
                
                Console.WriteLine("Start jumping Routine");
                while(true)
                {


                    //Auto jumping 
                    if (active) Jumping(isim);

                    // Auto Place Torch
                    if (active) placeTorch(isim);

                    // Auto Break Torch
                    if (active) breakTorch(isim); 
                   


                }

            }
            else
            {
                Console.Error.WriteLine("Connot Find Mincraft Running");
            }

        }
    }
}