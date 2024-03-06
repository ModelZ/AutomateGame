using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;
using WindowsInput;
using AutomateGame.opencv;
using Point = System.Drawing.Point;

namespace AutomateGame.automation.lib
{
    internal class GlanblueAutoaction
    {


        public static void runAndLockOn(InputSimulator isim, ref bool active)
        {
            // For blocking KeyHolding Loop
            bool isKeyDown = false;

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

        public static void bypassContinueThisQuest(InputSimulator isim, ref bool active)
        {
            // Capture Screen to Mat every 4 seconds

            Thread.Sleep(4000);

            // Playsound when capturing
            Playsound.Mains();

            // check string
            string cancelRepeat = "Cancel";
            string repeatQuest = "Quest";
            string bypassContinueQuest = "Continue";

            // Capture Screen to Mat
            string capturedText = CapturedScreenToTextRecognition.capturedToText(new Point(188, 946), new Point(347, 1005)); // 1920 x 1080

            string bypasssText = CapturedScreenToTextRecognition.capturedToText(new Point(746, 442), new Point(1234, 583)); // 1920 x 1080

            // Bypasses (Do you want continue this quest?)
            if (bypasssText.Contains(bypassContinueQuest))
            {
                Console.WriteLine("Bypasses Continue Quest");

                Autokey.pressKey(active, isim, VirtualKeyCode.UP);
                Autoclick.leftClick(active, isim, 1000);
            }
            else

            if (capturedText.Contains(repeatQuest)) // Show RepeatQuest Button State
            {
                Console.WriteLine("ShowRepeatQuestState");

                // auto repeat quest at first time
                Autokey.pressKey(active, isim, VirtualKeyCode.VK_3);
                Autoclick.leftClick(active, isim, 1000);

            }
            else if (capturedText.Contains(cancelRepeat)) // Show CancelRepeat Button State
            {
                Console.WriteLine("ShowCancelRepeatState");
                Autoclick.leftClick(active, isim);
                Autoclick.leftClick(active, isim, 1000);

            }
            else  // No Button State
            {
                Console.WriteLine("No State");
            }
        }


        public static void ferryNormalAttack(InputSimulator isim, ref bool active)
        {
            Console.WriteLine("ferryNormalAttack Thread Active....");

            while (true)
            {
                // Perform Fery Left Click every 3 seconds 3 hits
                Thread.Sleep(2000);

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


        public static void evaluationBypass(InputSimulator isim, ref bool active)
        {
            Console.WriteLine("Evaluation Thread Active...");
            while (true)
            {
                if (!active) continue;
                // Condition check every 1 second
                Thread.Sleep(1000);

                // Capture Screen to Text
                string evaluateText = CapturedScreenToTextRecognition.capturedToText(new Point(318, 371), new Point(686, 527)); // 1920 x 1080
                // Text to check condition
                string evaluation = "Eva";

                if (evaluateText.Contains(evaluation))
                {
                    Console.WriteLine("Evaluation Bypass");

                    Autoclick.leftClick(active, isim);
                }
            }

        }




    }
}
