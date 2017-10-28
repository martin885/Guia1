using Plugin.Messaging;
using System;
using Xamarin.Forms;
using Guia1.Interfaces;

[assembly: Dependency(typeof(Guia1.Droid.Implementations.EmailAttachment))]

namespace Guia1.Droid.Implementations
{
    public class EmailAttachment : IEmail
    {

        public IEmailMessage EmailMessage(string filename)
        {
            string root = null;
            //Get the root path in android device.
            if (Android.OS.Environment.IsExternalStorageEmulated)
            {
                root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            }
            else
                root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //Create directory and file 
            Java.IO.File myDir = new Java.IO.File(root + "/MRP");
            myDir.Mkdir();

            Java.IO.File file = new Java.IO.File(myDir, string.Format("{0}.xlsx",filename));

            
            var emailprueba = new EmailMessageBuilder()
                  .Subject(string.Format("Hoja de càlculo del Producto {0}",filename))
                  .Body(string.Format("Envío el archivo con la hoja de cálculos del producto {0}. Saludos",filename))
                  .WithAttachment(file)
                  .Build();
           
            return emailprueba;
            

        }
    }
}