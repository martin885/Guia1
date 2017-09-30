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

namespace Guia1.ViewModels

{
  public  class MainViewModel :BindableBase, INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;

        INavigationService _navigationService;

        public DelegateCommand NavigateAgregarCommand { get; private set; }
        public DelegateCommand NavigateProductoCommand { get; private set; }


        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateAgregarCommand = new DelegateCommand(NavigateAgregar);
            NavigateProductoCommand = new DelegateCommand(NavigateProducto);
        }

        private void NavigateProducto()
        {
         _navigationService.NavigateAsync("ProdSubDefinir");
        }

        private void NavigateAgregar()
        {
         
         _navigationService.NavigateAsync("AgregarProducto");

        }

    }
}
