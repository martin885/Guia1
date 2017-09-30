using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guia1.Views;
using Xamarin.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Guia1.Services;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Navigation;
using Guia1.Models;
using System.Collections.ObjectModel;

namespace Guia1.ViewModels
{
    public class AgregarProductoViewModel :BindableBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        DialogService dialogService;
        public DataService dataService;

        public ProductoPrincipal productoPrincipal { get; set; }

        public ObservableCollection<ProductoPrincipal> _productoPrincipal;
        public ObservableCollection<ProductoPrincipal> ProductoPrincipal
        {
            get { return _productoPrincipal; }
            set
            {
                if (_productoPrincipal != value)
                {
                    _productoPrincipal = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProductoPrincipal)));
                }
            }

        }

        public ProductoPrincipal _itemSeleccionado;

        public ProductoPrincipal ItemSeleccionado
        {
            get { return _itemSeleccionado; }
            set
            {
                if (_itemSeleccionado != value)
                {
                    _itemSeleccionado = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemSeleccionado)));
                }
            }

        }


        INavigationService _navigationService;
        public DelegateCommand NavigateCommand { get; private set; }


        bool _nombre;
        public bool Nombre
        {
            get { return _nombre; }
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nombre)));
                }
            }
        }

        public ICommand ItemEvento
        {
        get
            {
                return new RelayCommand(Selected);
            }

        }
          
        private async void Selected()
        {
            
            NavigateProdDefinir( ItemSeleccionado);

        }
        public AgregarProductoViewModel(INavigationService navigationService)
        {
          
            dialogService = new DialogService();
            dataService = new DataService();
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand(Navigate);
            ProductoPrincipal = new ObservableCollection<ProductoPrincipal>(dataService.Get<ProductoPrincipal>(true));
        }

        private async void Navigate()
        {
         await   _navigationService.GoBackAsync();
        }
        private async void NavigateProdDefinir(ProductoPrincipal ItemSeleccionado)
        {
            var parametros = new NavigationParameters();
            
            parametros.Add("Item",ItemSeleccionado);
          await  _navigationService.NavigateAsync("ProductoEditar", parametros);
        }
    }

}
