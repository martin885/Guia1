using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Guia1.Services
{
    public class DialogService
    {
        public async Task ShowMessage(string title, string messsage)
        {
            await Application.Current.MainPage.DisplayAlert(
                title,
                messsage,
                "Aceptar","Cancelar");
        }
    }
}
