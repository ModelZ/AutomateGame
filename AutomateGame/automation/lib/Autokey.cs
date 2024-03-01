using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;
using WindowsInput;

namespace AutomateGame.automation.lib
{
    internal class Autokey
    {
        public static void pressKey(bool active, InputSimulator isim, VirtualKeyCode key, int interval)
        {
            if (!active) return;

            Thread.Sleep(interval);
            isim.Keyboard.KeyDown(key);
            Thread.Sleep(250);
            isim.Keyboard.KeyUp(key);
        }

        public static void pressKey(bool active, InputSimulator isim, VirtualKeyCode key)
        {
            if (!active) return;

            Thread.Sleep(500);
            isim.Keyboard.KeyDown(key);
            Thread.Sleep(250);
            isim.Keyboard.KeyUp(key);
        }

        public static void holdKey(InputSimulator isim, VirtualKeyCode key, ref bool active, ref bool isKeyDown)
        {
            // If toggle key is on and not keydown
            if (active && !isKeyDown)
            {
                // Hold W Key and E Key every 2 seconds

                Console.WriteLine("Holding Key");
                isKeyDown = true;
                isim.Keyboard.KeyDown(key);
            }

            // If toggle key is on and keydown
            if (!active && isKeyDown)
            {

                Console.WriteLine("Release Key");
                isKeyDown = false;
                isim.Keyboard.KeyUp(key);

                // ***ALWAYS*** Release key when Thread terminate this program
            }
        }
    }
}
