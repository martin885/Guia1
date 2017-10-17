
using System.ComponentModel;
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
    public class AgregarProductoViewModel : BindableBase, INotifyPropertyChanged, INavigationAware
    {
#pragma warning disable CS0108 // El miembro oculta el miembro heredado. Falta una contraseña nueva
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0108 // El miembro oculta el miembro heredado. Falta una contraseña nueva
        DialogService dialogService;
        public DataService dataService;

        #region Prodpiedades

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
        public bool Revisar { get; private set; }
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
        #region Constructors
        public AgregarProductoViewModel(INavigationService navigationService)
        {

            dialogService = new DialogService();
            dataService = new DataService();
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand(Navigate);

        }

        #endregion
        #region Navegación 
        private async void Navigate()
        {
            await _navigationService.GoBackAsync();
        }
        private async void NavigateProdDefinir(ProductoPrincipal ItemSeleccionado)
        {
            var parametros = new NavigationParameters();
            if (Revisar == false)
            {
                parametros.Add("Item", ItemSeleccionado);
            }
            else
            {
                parametros.Add("Revisar", ItemSeleccionado);
            }
            await _navigationService.NavigateAsync("HojaCalculo", parametros);
        }
        #endregion

        #region Pase de Parámetros
        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

            if (parameters.ContainsKey("Revisar"))
            {
                Revisar = (bool)parameters["Revisar"];
            }

            if (Revisar != true)
            {

                ProductoPrincipal = new ObservableCollection<ProductoPrincipal>(dataService.Get<ProductoPrincipal>(true));
            }
            else
            {
                ProductoPrincipal = new ObservableCollection<ProductoPrincipal>();


                foreach (var productoPrincipal in dataService.Get<ProductoPrincipal>(true))
                {
                    {
                        if (productoPrincipal.Calculo == true)
                        {
                            ProductoPrincipal.Add(productoPrincipal);
                        }

                    }
                }

            }
        }




        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }
        #endregion

    }
}