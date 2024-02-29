using IronOcr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateGame.opencv
{
    internal class IronOCR
    {
        static void Mains()
        {

            IronTesseract ocr = new IronTesseract();
            using OcrInput input = new OcrInput();

            // Add multiple images
            input.LoadImage("../../../opencv/crop_re_quest.png");

            OcrResult result = ocr.Read(input);
            Console.WriteLine(result.Text);
        }
    }
}
