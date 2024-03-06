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
using Emgu.CV;
using AutomateGame.automation;
using Point = System.Drawing.Point;
using AutomateGame.automation.lib;

namespace AutomateGame
{
    class GlanblueAFKFarming
    {

        // import the function in your class
        [DllImport("user32.dll")]
        static extern int SetForegroundWindow(IntPtr point);


        static void keyListener(InputSimulator isim, ref bool active, VirtualKeyCode TOGGLEKEY, VirtualKeyCode EXITKEY)
        {
            Console.WriteLine("keyListener Thread Active....");
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

        

        static void Mains(string[] args)
        {
            Console.WriteLine("Welcome to Granblue Fantasy Relink Bypass 10 times Reset Repeat Quest");
            Console.WriteLine("Press X to toggle on/off, Press Z to terminate the program");
            Console.WriteLine("How to use: go to some quest and idle and press X");
            Console.WriteLine("Author: ModelZ");
            Console.WriteLine("*****************************************************************");
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
            Autoclick.leftClick(true, isim);

            // Thread Delay for 1 second
            Thread.Sleep(1000);


            // Toggle Automate
            bool active = false;

            // Thread function for Listening Keyboard Input
            Thread listener = new Thread(() => keyListener(isim, ref active, VirtualKeyCode.VK_X, VirtualKeyCode.VK_Z));
            listener.Start();

            // check string
            string cancelRepeat = "Cancel";
            string repeatQuest = "Quest";
            string bypassContinueQuest = "Continue";
            string questFreeze = "reviewing";

            while (true)
            {

                // If toggle key is on
                if (active)
                {

                    // Capture Screen to Mat every 3 seconds

                    Thread.Sleep(3000);

                    // Playsound when capturing
                    Playsound.Mains();

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
                    // Prevent Quest Freeze to the confirmation (Are you done reviewing .....)
                    if (bypasssText.Contains(questFreeze))
                    {
                        Console.WriteLine("Preventing Quest Freeze");

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
            }
        }

    }
}
