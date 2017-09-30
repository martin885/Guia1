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
    class ProdSubDefinirViewModel:BindableBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        DataService dataService;
        DialogService dialogService;

        public AgregarProductoViewModel a;
        public string CantP { get; set; }

        string _nombrProducto;

        public string NombreProducto
        {
            get { return _nombrProducto; }
            set
            {
                if (_nombrProducto != value)
                {
                    _nombrProducto = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NombreProducto)));
                }
            }

        }


        bool _isEnable;
        public bool IsEnable
        {
            get { return _isEnable; }
            set
            {
                if (_isEnable != value)
                {
                    _isEnable = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnable)));
                }
            }

        }

        public ProductoPrincipal ProductoPrincipal { get; set; }

        List<ProductoPrincipal> productoPrincipal;

        ObservableCollection<ProductoPrincipal> _productoPrincipalCollection;

        public ObservableCollection<ProductoPrincipal> ProductoPrincipalCollection
        {
            get { return _productoPrincipalCollection; }
            set
            {
                if (_productoPrincipalCollection != value)
                {
                    _productoPrincipalCollection = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProductoPrincipal)));
                }
            }

        }

        public ProductoA productoA { get; set; }

        List<ProductoA> Producto;

        ObservableCollection<ProductoA> _listaProducto;

       public ObservableCollection<ProductoA> ListaProducto
        {
            get { return _listaProducto; }
    set
            {
                if (_listaProducto != value)
                {
                    _listaProducto = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListaProducto)));
                }
            }

        }
       

        INavigationService _navigationService;

        public DelegateCommand NavigateCommand { get; private set; }

        public ICommand Complete
        {
            get
            {
                return new RelayCommand(Display);
            }
           
        }
        public ICommand GuardarCommand
        {
            get
            {
                return new RelayCommand(Guardar);
            }

        }
        public ICommand BorrarCommand
        {
            get
            {
                return new RelayCommand(Borrar);
            }
        }

        private async  void Borrar()
        {
        
            dataService.DeleteAll<ProductoPrincipal>();
            dataService.DeleteAll<ProductoA>();
            await dialogService.ShowMessage(
               "Mensaje", "Se borró el contenido con exito"
               );
        }

        private async void Guardar()
        {

            //saber si hay un casillero nulo en clasificación
            foreach (var lista in ListaProducto)
            {
                if (string.IsNullOrEmpty(lista.Clasificacion))
                {
                    await dialogService.ShowMessage(
                 "Error", "Agregar el código de clasificación a todos los elementos"
                 );                   
                    return;
                } 
            }
            //filtro para clasificaciones que no cumplen con la nomenclatura 
           
         
            foreach(var lista in ListaProducto) {
                var NewLista = lista.Clasificacion.Replace(".", "").Replace("-", "").Replace("_", "");

                int result=0;
                if(!int.TryParse(NewLista,out result)) { 
            await dialogService.ShowMessage(
                  "Error", "El código "+lista.Clasificacion+" no es vàlido" +". Ingresar el código con el siguiente formato:1.1.2"
              );

                    return;
                }
            }
            //aviso de valores repetidos         

                    var agrupar = ListaProducto.GroupBy(x => x.Clasificacion.Replace(".", "").Replace("-", "").Replace("_", "")).Where(x => x.Count() > 1).ToList();

                    foreach (var listaKey in agrupar)
                    {
                
                        foreach (var listaClasif in listaKey)
                        {

                            await dialogService.ShowMessage(
                               "Error", "El código de clasificación "
                               + listaClasif.Clasificacion + " se repite "
                               + listaKey.Count()
                               + " veces. Debe haber un número de clasificación distinto por cada producto."
                           );
                            return;

                        }
                    }

           
            //dataService.Save(ProductoPrincipal.Productos,false);
           
            dataService.Save(productoPrincipal,true);
            await dialogService.ShowMessage(
                          "Mensaje", "El producto se guardó exitosamente"
                      );

        }
    

        //Mostrar los Sub-Productos en pantalla luego de la definición de cuantos son
        public async void Display()
        {
            productoPrincipal = new List<ProductoPrincipal>();
            Producto = new List<ProductoA> { };
            ProductoPrincipal = new ProductoPrincipal {Nombre=NombreProducto, Productos = Producto };
            productoPrincipal.Add(ProductoPrincipal);
            for (int i=0;i<int.Parse(CantP);i++) {

                Producto.Add(new ProductoA());
               
                IsEnable = false;
      
                
            }
            ListaProducto= new ObservableCollection<ProductoA>(ProductoPrincipal.Productos);
        }
     
        public ProdSubDefinirViewModel(INavigationService navigationService)
        {
            IsEnable = true;
            a = new AgregarProductoViewModel(navigationService);
            productoPrincipal = new List<ProductoPrincipal>();
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand(Navigate);
            dialogService = new DialogService();
            dataService = new DataService();

            ListaProducto = new ObservableCollection<ProductoA>(dataService.Get<ProductoA>(false));
         
        }

        private void Navigate()
        {
            _navigationService.GoBackAsync();
        }

    }
}
