using GymManagmentBLL.Service.InterFaces.AttachmentService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Classes.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        private readonly string[] allowedextenion = { "", ".jpeg", ".png" };
        private readonly long MAxFilesize= 5 * 1024 * 1024;
        private readonly IWebHostEnvironment _webHost;

        public AttachmentService(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }


        public string? Upload(string foldername, IFormFile file)
        {
            try
            {
                if (string.IsNullOrEmpty(foldername) || file.Length == 0 || file is null) return null;
                if (file.Length > MAxFilesize) return null;

                var extention = Path.GetExtension(file.FileName).ToLower();
                if (!allowedextenion.Contains(extention)) return null;

                var folderpath = Path.Combine(_webHost.WebRootPath, "images", foldername);
                if (!Directory.Exists(folderpath))
                {
                    Directory.CreateDirectory(folderpath);

                }
                var fileName = Guid.NewGuid().ToString() + extention;
                var filePath = Path.Combine(folderpath, fileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);
                return fileName;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Faild to upload file to folder ={foldername}:{ex}");
                return null;

            }
           }

        public bool Delete(string filename, string foldername)
        {
            try
            {
                if (string.IsNullOrEmpty(filename) || string.IsNullOrEmpty(foldername)) return false;

                var Fullpath = Path.Combine(_webHost.WebRootPath, "images", foldername, filename);
                if (File.Exists(Fullpath))
                {
                    File.Delete(Fullpath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Faild to delete file with name{filename}:{ex}");
                return false;
            }
        }


    }
}
