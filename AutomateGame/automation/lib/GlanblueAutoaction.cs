using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;
using WindowsInput;

namespace AutomateGame.automation.lib
{
    internal class GlanblueAutoaction
    {


        public static void runAndLockOn(InputSimulator isim, ref bool active, ref bool isKeyDown)
        {
            Console.WriteLine("runAndLockOn Thread Active....");

            while (true)
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

        }


        public static void ferryNormalAttack(InputSimulator isim, ref bool active)
        {
            Console.WriteLine("ferryNormalAttack Thread Active....");

            while (true)
            {
                // Perform Fery Left Click every 3 seconds 3 hits
                Thread.Sleep(3000);

                Autoclick.leftClick(active, isim, 1000);
                Autoclick.leftClick(active, isim, 1000);
                Autoclick.leftClick(active, isim, 1000);
            }
        }

        public static void ferrySkillSets(InputSimulator isim, ref bool active)
        {
            Console.WriteLine("ferrySkillSets Thread Active....");

            while (true)
            {
                // Auto Action Every 1 seconds
                Thread.Sleep(1000);

                Console.WriteLine("Sending Action");

                // Release all ferry skill (if necessery)
                Autokey.pressKey(active, isim, VirtualKeyCode.VK_1, 1000);
                Autokey.pressKey(active, isim, VirtualKeyCode.SHIFT);

                Autokey.pressKey(active, isim, VirtualKeyCode.VK_2);
                Autokey.pressKey(active, isim, VirtualKeyCode.SHIFT);

                Autokey.pressKey(active, isim, VirtualKeyCode.VK_3);
                Thread.Sleep(2500);

                Autokey.pressKey(active, isim, VirtualKeyCode.VK_4);
                Autokey.pressKey(active, isim, VirtualKeyCode.SHIFT);

                // Auto Skill Action Every 20 seconds
                Thread.Sleep(20000);                
            }

        }




    }
}
