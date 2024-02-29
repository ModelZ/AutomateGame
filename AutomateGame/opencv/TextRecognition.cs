using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using System;
using System.Drawing;

namespace AutomateGame.opencv
{
    internal class TextRecognition
    {
        static void Main()
        {
            // Load an image
            Mat image = CvInvoke.Imread("C:\\Users\\User\\Desktop\\GB_Build\\granblue_ws\\AutomateGame\\AutomateGame\\opencv\\crop_re_quest.png", ImreadModes.Color);

            // Convert the image to grayscale
            Mat gray = new Mat();
            CvInvoke.CvtColor(image, gray, ColorConversion.Bgr2Gray);

            // Apply some preprocessing if needed (e.g., thresholding, smoothing)

            // Create Tesseract OCR engine
            Tesseract ocr = new Tesseract("C:\\Program Files\\Tesseract-OCR\\tessdata", "eng", OcrEngineMode.LstmOnly);

            // Set the image to OCR engine
            ocr.SetImage(gray);

            // Perform OCR
            string recognizedText = ocr.GetUTF8Text();

            // Print the recognized text
            Console.WriteLine("Recognized Text: " + recognizedText);

            // Release resources
            image.Dispose();
            gray.Dispose();
            ocr.Dispose();
        }
    }

}
