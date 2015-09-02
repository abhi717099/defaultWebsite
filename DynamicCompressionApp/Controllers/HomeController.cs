using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DynamicCompressionApp.Controllers
{
    public class HomeController : Controller
    {
        private string textName;
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message =textName;
            string ac = Request.ContentType;
            int a;
            return View();
        }
        public void accept(string txtName)
        {
            textName = txtName;
            string type = Request.ContentType;
            int separator = type.IndexOf("/");
            string compressionFormat = type.Substring(separator+1);

            int len = txtName.Length;

            //compress
            byte[] stringToByte=new byte[len+1];
            int byteIndex=0;
            foreach(char c in txtName.ToCharArray())
            {
                stringToByte[byteIndex++] = (byte)c;
            }
            var bytes = Encoding.ASCII.GetBytes(txtName);

            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            System.IO.Compression.GZipStream gzipStream = new System.IO.Compression.GZipStream(memoryStream,System.IO.Compression.CompressionMode.Compress);


            gzipStream.Write(bytes,0,bytes.Length);
            gzipStream.Close();

            stringToByte = memoryStream.ToArray();

            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(stringToByte.Length);
            foreach(byte b in stringToByte)
            {
                stringBuilder.Append((char)b);
            }
            memoryStream.Close();
            gzipStream.Dispose();
            memoryStream.Dispose();

            string s = stringBuilder.ToString();

            //Decompress
            byte[] decompressStream=new byte[s.Length];
            byteIndex=0;
            foreach(char c in s.ToCharArray())
            {
                decompressStream[byteIndex++]=(byte)c;
            }

            System.IO.MemoryStream ms = new System.IO.MemoryStream(decompressStream);
            System.IO.Compression.GZipStream gs = new System.IO.Compression.GZipStream(ms,System.IO.Compression.CompressionMode.Decompress);

            int byteRead = gs.Read(decompressStream,0,decompressStream.Length);

            System.Text.StringBuilder builder = new System.Text.StringBuilder(byteRead);
             foreach(byte b in decompressStream)
             {
                 builder.Append((char)b);
             }

             ms.Close();
             gs.Close();
             ms.Dispose();
             gs.Dispose();

             String str = builder.ToString();
             int a = 0;

        }
    }
}
