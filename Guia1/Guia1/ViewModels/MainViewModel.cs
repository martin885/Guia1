using GalaSoft.MvvmLight.Command;
using Guia1.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Navigation;
using Guia1.Services;

namespace Guia1.ViewModels

{
  public  class MainViewModel :BindableBase, INotifyPropertyChanged

    {
#pragma warning disable CS0108 // El miembro oculta el miembro heredado. Falta una contraseña nueva
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0108 // El miembro oculta el miembro heredado. Falta una contraseña nueva
        DataService dataservice;
        INavigationService _navigationService;
        public bool Revisar { get; set; }

        public DelegateCommand NavigateCalcularCommand { get; private set; }
        public DelegateCommand NavigateProductoCommand { get; private set; }
        public DelegateCommand RevisarCommand { get; private set; }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCalcularCommand = new DelegateCommand(NavigateAgregar);
            NavigateProductoCommand = new DelegateCommand(NavigateProducto);
            RevisarCommand = new DelegateCommand(NavigateRevisar);
            dataservice = new DataService();
        }

        private async void NavigateRevisar()
        {
            var parametros = new NavigationParameters();
            parametros.Add("Revisar", Revisar=true);
            await _navigationService.NavigateAsync("AgregarProducto",parametros);
        }

        private async void NavigateProducto()
        {
        await  _navigationService.NavigateAsync("ProdSubDefinir");
        }

        private async void NavigateAgregar()
        {
         
        await _navigationService.NavigateAsync("AgregarProducto");

        }

    }
}
