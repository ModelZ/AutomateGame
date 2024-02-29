using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateGame.opencv
{
    internal class showimage
    {
        static void Mains(string[] args)
        {
            Mat pic = CvInvoke.Imread("C:\\Users\\User\\Desktop\\GB_Build\\granblue_ws\\AutomateGame\\AutomateGame\\opencv\\no_repeat.png");
            CvInvoke.Imshow("Djeeta", pic);

            CvInvoke.WaitKey();
        }
    }
}
