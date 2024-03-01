using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;

namespace AutomateGame.automation.lib
{
    internal class Autoclick
    {
        public static void leftClick(bool active, InputSimulator isim)
        {
            if (!active) return;

            Thread.Sleep(500);
            isim.Mouse.LeftButtonDown();
            Thread.Sleep(250);
            isim.Mouse.LeftButtonUp();
        }

        public static void rightClick(bool active, InputSimulator isim)
        {
            if (!active) return;

            Thread.Sleep(500);
            isim.Mouse.RightButtonDown();
            Thread.Sleep(250);
            isim.Mouse.RightButtonUp();
        }
        public static void leftClick(bool active, InputSimulator isim, int interval)
        {
            if (!active) return;

            Thread.Sleep(interval);
            isim.Mouse.LeftButtonDown();
            Thread.Sleep(250);
            isim.Mouse.LeftButtonUp();
        }

        public static void rightClick(bool active, InputSimulator isim, int interval)
        {
            if (!active) return;

            Thread.Sleep(interval);
            isim.Mouse.RightButtonDown();
            Thread.Sleep(250);
            isim.Mouse.RightButtonUp();
        }
    }
}
