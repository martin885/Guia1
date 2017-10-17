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
   public class ProductoEditarViewModel: BindableBase, INotifyPropertyChanged,INavigationAware
    {
#pragma warning disable CS0108 // El miembro oculta el miembro heredado. Falta una contraseña nueva
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0108 // El miembro oculta el miembro heredado. Falta una contraseña nueva
        DialogService dialogService;
        public DataService dataService;
        INavigationService _navigationService;

        public object Title { get;  set; }

        public DelegateCommand NavigateCommand { get; private set; }

        #region Propiedades
        public ProductoA _itemSeleccionado;
        public ProductoA ItemSeleccionado
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
        public ProductoPrincipal _seleccionProducto;
        public ProductoPrincipal SeleccionProducto
        {
            get { return _seleccionProducto; }
            set
            {
                if (_seleccionProducto != value)
                {
                    _seleccionProducto = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SeleccionProducto)));
                }
            }

        }
        public string _nombre;
        public string nombre
        {
            get { return _nombre; }
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(nombre)));
                }
            }

        }

        public string _clasificacion;
        public string Clasificacion
        {
            get { return _clasificacion; }
            set
            {
                if (_clasificacion != value)
                {
                    _clasificacion = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Clasificacion)));
                }
            }

        }

        public ObservableCollection<ProductoA> _seleccionProdA;
        public ObservableCollection<ProductoA> SeleccionProdA
        {
            get { return _seleccionProdA; }
            set
            {
                if (_seleccionProdA != value)
                {
                    _seleccionProdA = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SeleccionProdA)));
                }
            }

        }
        #endregion
        #region Commands
        public ICommand ItemEvento
        {
            get
            {
                return new RelayCommand(Selected);
            }

        }

        private void Selected()
        {

            NavigateProdDefinir(ItemSeleccionado);

        }

        #endregion


        public ProductoEditarViewModel(INavigationService navigationService)
        {

            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand(Navigate);
            dialogService = new DialogService();
            dataService = new DataService();
        }

        private async void Navigate()
        {
         await   _navigationService.NavigateAsync("HojaCalculo");
        }

        private async void NavigateProdDefinir(ProductoA itemSeleccionado)
        {
            var parametros = new NavigationParameters();
            parametros.Add("Item", ItemSeleccionado);
            await _navigationService.NavigateAsync("HojaCalculo", parametros);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
          
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Item"))
            {
                SeleccionProducto =(ProductoPrincipal)parameters["Item"];
                nombre = SeleccionProducto.Nombre;
                SeleccionProdA = new ObservableCollection<ProductoA>(SeleccionProducto.Productos);
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}
