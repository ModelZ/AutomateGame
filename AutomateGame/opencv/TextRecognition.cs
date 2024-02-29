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
            // TesserectPath maybe argument in cli
            string TesserectPath = "C:\\Program Files\\Tesseract-OCR\\tessdata";

            // Text Image Path
            string imagePath = "../../../opencv/crop_quest.png";

            // Load an image
            Mat image = CvInvoke.Imread(imagePath, ImreadModes.Color);

            // Convert the image to grayscale
            Mat gray = new Mat();
            CvInvoke.CvtColor(image, gray, ColorConversion.Bgr2Gray);

            // Apply some preprocessing if needed (e.g., thresholding, smoothing)

            // Create Tesseract OCR engine
            Tesseract ocr = new Tesseract(TesserectPath, "eng", OcrEngineMode.LstmOnly);

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
