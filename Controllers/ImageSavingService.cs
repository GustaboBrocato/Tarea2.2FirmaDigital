﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;
using Tarea2._2FirmaDigital.Controllers;

namespace Tarea2._2FirmaDigital.Controllers
{
    public class ImageSavingService
    {
        public static ImageSavingService Instance { get; private set; }

        public ImageSavingService()
        {
            Instance = this;
        }
        public async Task<string> SaveImageAsync(string base64Image, string fileName, string folderName)
        {

            // Check and request storage permission if needed
            var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (status != PermissionStatus.Granted)
                {
                    // Handle permission denied
                    return null;
                }
            }

            byte[] imageData = Convert.FromBase64String(base64Image);

            var externalStoragePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var folderPath = Path.Combine(externalStoragePath, folderName);
            var filePath = Path.Combine(folderPath, fileName);

            Directory.CreateDirectory(folderPath);

            await File.WriteAllBytesAsync(filePath, imageData);

            return filePath;
        }
    }
}
