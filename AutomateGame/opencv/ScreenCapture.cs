using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;

namespace AutomateGame.opencv
{
    internal class ScreenCapture
    {
        static void Main()
        {
            try
            {
                // Creating a new Bitmap object
                // Bitmap captureBitmap = new Bitmap(int width, int height, PixelFormat);
                Bitmap captureBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);


                // Creating a Rectangle object which will
                // capture our Current Screen
                System.Drawing.Rectangle captureRectangle = Screen.AllScreens[0].Bounds;


                // Creating a New Graphics Object
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);

                // Copying Image from The Screen
                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);

                // Saving the Image File 
                captureBitmap.Save("../../../opencv/captured.jpg", ImageFormat.Jpeg);

                // Display the captured screen (optional)
                CvInvoke.Imshow("Screen Captured", CvInvoke.Imread("../../../opencv/captured.jpg"));

                Console.WriteLine("Screen captured and saved.");
                CvInvoke.WaitKey();

                // Displaying the Successfull Result
                //MessageBox.Show("Screen Captured");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
