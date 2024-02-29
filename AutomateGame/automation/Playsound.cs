using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace AutomateGame.automation
{
    internal class Playsound
    {
        public static void Mains()
        {
            // Specify the path to the sound file
            string soundFilePath = "../../../automation/cute_sound.wav"; // Replace with the actual path

            // Create a SoundPlayer instance
            SoundPlayer player = new SoundPlayer(soundFilePath);

            try
            {
                // Play the sound
                player.Play();

               /* // Wait for the sound to finish playing (you can do other work here)
                Console.WriteLine("Press Enter to exit.");
                Console.ReadLine();*/
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., if the file is not found
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                // Dispose of the SoundPlayer to release resources
                player.Dispose();
            }
        }
    }
}
