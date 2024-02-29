using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using Emgu.CV.Reg;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;
using Size = System.Drawing.Size;
using Emgu.CV.OCR;

namespace AutomateGame.opencv
{
    internal class CapturedScreenToTextRecognition
    {
        public static Mat screenCapturedToMat()
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

            // Convert Bitmap to Mat
            Mat captureMat = captureBitmap.ToMat();

            return captureMat;
        }

        public static Mat twoPointRectangleCrop(Mat imageToCrop, Point UpperLeftPoint, Point LowerRightPoint)
        {
            // Define the upper-left and lower-right points of the rectangle
            // You can find 2 set of point from img_coord_finder.py
            // And click 2 points you want to crop upperLeft and lowerRight to make rectangle cropping
            Point upperLeft = UpperLeftPoint; // Example coordinates of upper-left point
            Point lowerRight = LowerRightPoint; // Example coordinates of lower-right point

            // Calculate the width and height of the rectangle
            int width = lowerRight.X - upperLeft.X;
            int height = lowerRight.Y - upperLeft.Y;

            // Create a rectangle using the upper-left point and the calculated width and height
            Rectangle cropRect = new Rectangle(upperLeft, new Size(width, height));

            // Crop the region from the image
            Mat croppedImage = new Mat(imageToCrop, cropRect);

            return croppedImage;
        }

        public static string imageTextRecognition(Mat inputImage, string TesserectPath)
        {
            // TesserectPath maybe argument in cli
            // Tesserect is Optical Text Recognition Open Source From Google
            // You must install it from https://tesseract-ocr.github.io/
            //string TesserectPath = "C:\\Program Files\\Tesseract-OCR\\tessdata";

            // Convert the image to grayscale
            Mat grayCroppedImage = new Mat();
            CvInvoke.CvtColor(inputImage, grayCroppedImage, ColorConversion.Bgr2Gray);

            // Apply some preprocessing if needed (e.g., thresholding, smoothing)

            // Create Tesseract OCR engine
            Tesseract ocr = new Tesseract(TesserectPath, "eng", OcrEngineMode.LstmOnly);

            // Set the image to OCR engine
            ocr.SetImage(grayCroppedImage);

            // Perform OCR
            string recognizedText = ocr.GetUTF8Text();

            // Print the recognized text
            //Console.WriteLine("Recognized Text: " + recognizedText);

            // Release resources
            inputImage.Dispose();
            grayCroppedImage.Dispose();
            ocr.Dispose();

            return recognizedText;
        }

        public static string imageTextRecognition(Mat inputImage)
        {
            // TesserectPath maybe argument in cli
            // Tesserect is Optical Text Recognition Open Source From Google
            // You must install it from https://tesseract-ocr.github.io/
            string TesserectPath = "C:\\Program Files\\Tesseract-OCR\\tessdata";

            // Convert the image to grayscale
            Mat grayCroppedImage = new Mat();
            CvInvoke.CvtColor(inputImage, grayCroppedImage, ColorConversion.Bgr2Gray);

            // Apply some preprocessing if needed (e.g., thresholding, smoothing)

            // Create Tesseract OCR engine
            Tesseract ocr = new Tesseract(TesserectPath, "eng", OcrEngineMode.LstmOnly);

            // Set the image to OCR engine
            ocr.SetImage(grayCroppedImage);

            // Perform OCR
            string recognizedText = ocr.GetUTF8Text();

            // Print the recognized text
            //Console.WriteLine("Recognized Text: " + recognizedText);

            // Release resources
            inputImage.Dispose();
            grayCroppedImage.Dispose();
            ocr.Dispose();

            return recognizedText;
        }

        public static string capturedToText(Point UpperLeftPoint, Point LowerRightPoint)
        {
            // ************* Capture Process ****************//

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

            // Convert Bitmap to Mat
            Mat captureMat = captureBitmap.ToMat();

            // ************* Crop Process ****************//

            // Define the upper-left and lower-right points of the rectangle
            // You can find 2 set of point from img_coord_finder.py
            // And click 2 points you want to crop upperLeft and lowerRight to make rectangle cropping
            Point upperLeft = UpperLeftPoint; // Example coordinates of upper-left point
            Point lowerRight = LowerRightPoint; // Example coordinates of lower-right point

            // Calculate the width and height of the rectangle
            int width = lowerRight.X - upperLeft.X;
            int height = lowerRight.Y - upperLeft.Y;

            // Create a rectangle using the upper-left point and the calculated width and height
            Rectangle cropRect = new Rectangle(upperLeft, new Size(width, height));

            // Crop the region from the image
            Mat croppedImage = new Mat(captureMat, cropRect);

            // ************* Text Recognition Process ****************//

            // TesserectPath maybe argument in cli
            // Tesserect is Optical Text Recognition Open Source From Google
            // You must install it from https://tesseract-ocr.github.io/
            string TesserectPath = "C:\\Program Files\\Tesseract-OCR\\tessdata";

            // Convert the image to grayscale
            Mat grayCroppedImage = new Mat();
            CvInvoke.CvtColor(croppedImage, grayCroppedImage, ColorConversion.Bgr2Gray);

            // Apply some preprocessing if needed (e.g., thresholding, smoothing)

            // Create Tesseract OCR engine
            Tesseract ocr = new Tesseract(TesserectPath, "eng", OcrEngineMode.LstmOnly);

            // Set the image to OCR engine
            ocr.SetImage(grayCroppedImage);

            // Perform OCR
            string recognizedText = ocr.GetUTF8Text();

            // Print the recognized text
            //Console.WriteLine("Recognized Text: " + recognizedText);

            // Release resources
            croppedImage.Dispose();
            grayCroppedImage.Dispose();
            ocr.Dispose();

            return recognizedText;
        }

        public static string capturedToText(Point UpperLeftPoint, Point LowerRightPoint, string TesserectPath)
        {
            // ************* Capture Process ****************//

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

            // Convert Bitmap to Mat
            Mat captureMat = captureBitmap.ToMat();

            // ************* Crop Process ****************//

            // Define the upper-left and lower-right points of the rectangle
            // You can find 2 set of point from img_coord_finder.py
            // And click 2 points you want to crop upperLeft and lowerRight to make rectangle cropping
            Point upperLeft = UpperLeftPoint; // Example coordinates of upper-left point
            Point lowerRight = LowerRightPoint; // Example coordinates of lower-right point

            // Calculate the width and height of the rectangle
            int width = lowerRight.X - upperLeft.X;
            int height = lowerRight.Y - upperLeft.Y;

            // Create a rectangle using the upper-left point and the calculated width and height
            Rectangle cropRect = new Rectangle(upperLeft, new Size(width, height));

            // Crop the region from the image
            Mat croppedImage = new Mat(captureMat, cropRect);

            // ************* Text Recognition Process ****************//

            // TesserectPath maybe argument in cli
            // Tesserect is Optical Text Recognition Open Source From Google
            // You must install it from https://tesseract-ocr.github.io/
            //string TesserectPath = "C:\\Program Files\\Tesseract-OCR\\tessdata";

            // Convert the image to grayscale
            Mat grayCroppedImage = new Mat();
            CvInvoke.CvtColor(croppedImage, grayCroppedImage, ColorConversion.Bgr2Gray);

            // Apply some preprocessing if needed (e.g., thresholding, smoothing)

            // Create Tesseract OCR engine
            Tesseract ocr = new Tesseract(TesserectPath, "eng", OcrEngineMode.LstmOnly);

            // Set the image to OCR engine
            ocr.SetImage(grayCroppedImage);

            // Perform OCR
            string recognizedText = ocr.GetUTF8Text();

            // Print the recognized text
            //Console.WriteLine("Recognized Text: " + recognizedText);

            // Release resources
            croppedImage.Dispose();
            grayCroppedImage.Dispose();
            ocr.Dispose();

            return recognizedText;
        }

    }
}
