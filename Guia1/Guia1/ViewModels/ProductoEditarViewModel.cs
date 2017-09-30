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
        public event PropertyChangedEventHandler PropertyChanged;

        INavigationService _navigationService;

        public object Title { get;  set; }

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
        public ObservableCollection<ProductoA> _seleccionProdA;
        public  ObservableCollection<ProductoA> SeleccionProdA
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

        public ProductoEditarViewModel(INavigationService navigationService)
        {

            _navigationService = navigationService;
           
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
          
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Item"))
            {
                SeleccionProducto =(ProductoPrincipal)parameters["Item"];
                nombre = SeleccionProducto.ProductoPrincipalId.ToString();
                SeleccionProdA = new ObservableCollection<ProductoA>(SeleccionProducto.Productos);
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}
