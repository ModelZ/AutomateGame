using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;
using WindowsInput;

namespace AutomateGame.automation.lib
{
    internal class ThreadListener
    {
        public static void keyListener(InputSimulator isim, ref bool active, VirtualKeyCode TOGGLEKEY, VirtualKeyCode EXITKEY)
        {
            Console.WriteLine("Key Listener Thread Active....");
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
                        Console.WriteLine("Toggle Program: " + active);
                    }

                }
                if (isim.InputDeviceState.IsHardwareKeyUp(TOGGLEKEY))
                {
                    isKeyUp = true;
                }

                if (isim.InputDeviceState.IsHardwareKeyDown(EXITKEY))
                {
                    resetAllKeyDown(isim);
                    Console.WriteLine("Exit Program Successfully!");
                    Environment.Exit(0);
                }

                // Add some delay to avoid high CPU usage
                Thread.Sleep(100);
            }

        }

        public static void resetAllKeyDown(InputSimulator isim)
        {
            HashSet<VirtualKeyCode> keysPressed = new HashSet<VirtualKeyCode>();
            Console.WriteLine("Release All Key Processing....");

            // Loop all key in VirtualKeyCode to check
            foreach (VirtualKeyCode key in keysPressed)
            {
                // if key is keyDown State then keyUp
                if (isim.InputDeviceState.IsKeyDown(key))
                {
                    isim.Keyboard.KeyUp(key);
                    Console.WriteLine("Release Key Id: " +  key);
                }
            }
        }



    }
}
