using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Messaging;
//Agregar nuget Xam.Plugins.Messaging
namespace Guia1.Interfaces
{
    public interface IEmail
    {
        IEmailMessage EmailMessage(string filename);
    }
}
