using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeSol.Application_start
{
    public class Logger
    {
        private static readonly Lazy<Logger> _lazy =
           new Lazy<Logger>(() => new Logger());
        public static string ErrFilepath = "Logfile";
        private string date;
        private string Logformat;

        private Logger()
        {

            Console.WriteLine("Instance created");
        }
        public static Logger Instance
        {
            get
            {
                return _lazy.Value;
            }
        }
       
        public void LogAPIError(string APIName, string strError)
        {

            Logformat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString();
            string filePath = ErrFilepath;
            string Year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string Day = DateTime.Now.Day.ToString();
            date = Year + month + Day;
            string extension = ".log";

            string firstPath = Path.GetFullPath(filePath);
            string MainPath = firstPath + date + extension;


            if (!File.Exists(MainPath))
            {
                FileStream path = new FileStream(MainPath, FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(path);
                writer.WriteLine("\n");
                writer.WriteLine("API Name-" + APIName + " " + Logformat);
                writer.WriteLine("Error :" + strError);
                writer.WriteLine("\n");
                writer.Close();
                path.Close();
            }
            else
            {
                FileStream path = new FileStream(MainPath, FileMode.Append, FileAccess.Write);
                StreamWriter writer = new StreamWriter(path);
                writer.WriteLine("\n");
                writer.WriteLine("API Name-" + APIName + "" + Logformat);
                writer.WriteLine("Error :" + strError);
                writer.WriteLine("\n");
                writer.Close();
                path.Close();
            }
        }

        public bool SaveImage(string base64, string ImgName, string folderName="ItemImg")
        {
            try
            {
                var bytes = Convert.FromBase64String(base64); 
                string filedir = Path.Combine(Directory.GetCurrentDirectory(), "Resource/Images/" + folderName);
                

                
                if (!Directory.Exists(filedir))
                { //check if the folder exists;
                    Directory.CreateDirectory(filedir);
                }
              
                string file = Path.Combine(filedir, ImgName);
                

                if (bytes.Length > 0)
                {
                    using (var stream = new FileStream(file, FileMode.Create))
                    {
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Flush();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                LogAPIError("SaveImage", e.Message);
                return false;
            }
        }

    }
}