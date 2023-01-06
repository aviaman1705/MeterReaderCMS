using AutoMapper;
using MeterReaderCMS.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MeterReaderCMS.Helper
{
    public class Utilities
    {
        private static void CheckDirectory(string directory)
        {
            bool isExsist = Directory.Exists(directory);
            if (!isExsist)
            {
                Directory.CreateDirectory(directory);
            }
        }

        public static string Upload(HttpPostedFileBase file, string path)
        {
            string fileName = Path.GetFileName(file.FileName);

            string filePath = HttpContext.Current.Server.MapPath(@"~/" + path);
            CheckDirectory(Path.Combine(HttpContext.Current.Server.MapPath(@"~/" + path)));
            filePath = Path.Combine(filePath, fileName);
            file.SaveAs(filePath);
            return $"{path}/{fileName}";
        }

        public static string StripHTML(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            string str = Regex.Replace(input, "<.*?>", String.Empty).Replace("(?:\r\n|\n|\r)", string.Empty).Replace("\n", "").Replace("\r", "").Replace("\t", ""); ;
            return str;
        }

        public static DateTime RandomAge()
        {
            Random gen = new Random();
            int range = 5 * 365; //5 years          
            DateTime randomDate = DateTime.Today.AddDays(-gen.Next(range));
            return randomDate;
        }

        public static void updateCache<T>(string cacheKey, int cacheTime, List<T> list)
        {
            MemoryCacher.Delete(cacheKey);
            var tracks = new List<T>();
            tracks = Mapper.Map<List<T>>(list);
            MemoryCacher.Add(cacheKey, list, DateTimeOffset.Now.AddMinutes(cacheTime));
        }
    }
}