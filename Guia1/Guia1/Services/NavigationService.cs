using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guia1.Interface;
using Xamarin.Forms;
using Guia1.Views;

namespace Guia1.Services
{
   public  class NavigationService:INavigationService
    {
        public async void NavigateToSecondPage()
        {
            var currentPage = GetCurrentPage();
            await currentPage.Navigation.PushModalAsync(new AgregarProducto());
        }
        public async void NavigateBack()
        {
            var currentPage = GetCurrentPage();
            await currentPage.Navigation.PopModalAsync();
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
    }
}
