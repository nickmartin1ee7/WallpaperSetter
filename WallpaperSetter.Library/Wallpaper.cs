/*
 * Author:      https://stackoverflow.com/users/55164/neil-n
 * Edited by:   https://github.com/nickmartin1ee7
 */

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace WallpaperSetter.Library
{
    public static class Wallpaper
    {
        public enum Style
        {
            Tiled,
            Centered,
            Stretched
        }

        private const int SPI_SET_DESKTOP_WALLPAPER = 20;
        private const int SPIF_UPDATE_INI_FILE = 0x01;
        private const int SPIF_SEND_WIN_INI_CHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public static void Set(Uri uri, Style style)
        {
            var tempPath = DownloadImageFromUriToTempPath(uri);

            var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);

            switch (style)
            {
                case Style.Tiled:
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 1.ToString());
                    break;
                case Style.Centered:
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                    break;
                case Style.Stretched:
                    key.SetValue(@"WallpaperStyle", 2.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, "Style does not exist!");
            }

            // Set in system
            SystemParametersInfo(SPI_SET_DESKTOP_WALLPAPER,
                0,
                tempPath,
                SPIF_UPDATE_INI_FILE | SPIF_SEND_WIN_INI_CHANGE);
        }

        private static string DownloadImageFromUriToTempPath(Uri uri)
        {
            var s = new WebClient().OpenRead(uri.AbsoluteUri);
            using var img =
                Image.FromStream(s ?? throw new InvalidOperationException("Failed to download image from URI!"));
            var tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
            img.Save(tempPath, ImageFormat.Bmp);
            return tempPath;
        }
    }
}