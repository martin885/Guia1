using Guia1.Views;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Guia1
{
    public class App : PrismApplication
    {

        protected override void OnInitialized()
        {
            NavigationService.NavigateAsync("NavBar/Main");
        }
        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<Main>();
            Container.RegisterTypeForNavigation<AgregarProducto>();
            Container.RegisterTypeForNavigation<ProdSubDefinir>();
            Container.RegisterTypeForNavigation<ProductoEditar>();
            Container.RegisterTypeForNavigation<HojaCalculo>();
            Container.RegisterTypeForNavigation<NavBar>();
        }
        public class NavBar : NavigationPage
        {
            public NavBar(Page navBar) : base(navBar) { }
        }
    }
}
