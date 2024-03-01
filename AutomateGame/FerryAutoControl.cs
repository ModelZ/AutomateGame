using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;
using WindowsInput;
using AutomateGame.automation.lib;

namespace AutomateGame
{
    internal class FerryAutoControl
    {
        // import the function in your class
        [DllImport("user32.dll")]
        static extern int SetForegroundWindow(IntPtr point);


        static void keyListener(InputSimulator isim, ref bool active, VirtualKeyCode TOGGLEKEY, VirtualKeyCode EXITKEY)
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
                    ThreadListener.resetAllKeyDown(isim);
                    Environment.Exit(0);
                }

                // Add some delay to avoid high CPU usage
                Thread.Sleep(100);
            }

        }

        static void Main(string[] args)
        {
            // Get the array of process run by this name
            Process[] ps = Process.GetProcessesByName("granblue_fantasy_relink");

            // Get the first array of process
            Process GameProcess = ps.FirstOrDefault();

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

            // Left Click to hide mouse focus into the game (For FullScreen Game)
            Autoclick.leftClick(true, isim, 250);

            // Thread Delay for 1 second
            Thread.Sleep(1000);

            /************** Thread *****************/

            // active for running thread
            bool active = false;
            // For blocking KeyHolding Loop
            bool isKeyDown = false;

            // Thread function for Listening Keyboard Input
            Thread listener = new Thread(() => keyListener(isim, ref active, VirtualKeyCode.VK_X, VirtualKeyCode.VK_Z));
            listener.Start();


            // Thread function for Run and Lockon
            Thread runAndLockOnThread = new Thread(() => GlanblueAutoaction.runAndLockOn(isim, ref active, ref isKeyDown));
            runAndLockOnThread.Start();


            // Thread function for Ferry Normal Attack 
            Thread ferryNormalAttackThread = new Thread(() => GlanblueAutoaction.ferryNormalAttack(isim, ref active));
            ferryNormalAttackThread.Start();


            // Thread function for Ferry Skill Sets 
            Thread ferrySkillSetThread = new Thread(() => GlanblueAutoaction.ferrySkillSets(isim, ref active));
            ferrySkillSetThread.Start();


            // Main Thread Wait for all threads
            listener.Join();
            runAndLockOnThread.Join();
            ferryNormalAttackThread.Join();
            ferrySkillSetThread.Join();

            /**********************************/



        }

    }
}
