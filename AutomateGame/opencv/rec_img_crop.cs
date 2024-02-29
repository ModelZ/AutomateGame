using Emgu.CV;
using System.Drawing;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;
using Size = System.Drawing.Size;


namespace AutomateGame.opencv
{
    internal class rec_img_crop
    {
        static void Mains()
        {
            // Load the image
            Mat image = CvInvoke.Imread("C:\\Users\\User\\Desktop\\GB_Build\\granblue_ws\\AutomateGame\\AutomateGame\\opencv\\no_repeat.png");

            // Define the upper-left and lower-right points of the rectangle
            Point upperLeft = new Point(195, 964); // Example coordinates of upper-left point
            Point lowerRight = new Point(341, 1020); // Example coordinates of lower-right point

            // Calculate the width and height of the rectangle
            int width = lowerRight.X - upperLeft.X;
            int height = lowerRight.Y - upperLeft.Y;

            // Create a rectangle using the upper-left point and the calculated width and height
            Rectangle cropRect = new Rectangle(upperLeft, new Size(width, height));

            // Crop the region from the image
            Mat croppedImage = new Mat(image, cropRect);

            // Display the original and cropped images
            CvInvoke.Imshow("Original Image", image);
            CvInvoke.Imshow("Cropped Image", croppedImage);
            CvInvoke.WaitKey(0);

            CvInvoke.DestroyAllWindows();
        }
    }
}
