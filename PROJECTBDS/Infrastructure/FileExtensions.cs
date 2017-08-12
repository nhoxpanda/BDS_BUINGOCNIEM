using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PROJECTBDS.Infrastructure
{
    public static class FileExtensions
    {
        public static bool HasFile(this HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0) ? true : false;
        }

        public static bool AllowFile(this HttpPostedFileBase file)
        {
            var allowedExtensions = new[] { ".jpg", ".png", ".gif", "jpge" };

            if (!file.HasFile()) return false;

            var extension = Path.GetExtension(file.FileName);

            return extension != null && allowedExtensions.Contains(extension.ToLower()) ? true : false;
        }
        //FileExtensions.DeleteFile(image.Image, "~/Uploads/News/");
        public static void DeleteFile(string file, string folder)
        {
            var location = System.Web.HttpContext.Current.Server.MapPath(folder);

            var fileName = file;

            if (location == null) return;

            var path = Path.Combine(location, fileName);
            var fileOrg = new FileInfo(path);
            if (fileOrg.Exists)
            {
                fileOrg.Delete();
            }
        }
    }
}