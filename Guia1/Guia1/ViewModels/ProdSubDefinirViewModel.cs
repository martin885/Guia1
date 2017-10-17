using GalaSoft.MvvmLight.Command;
using Guia1.Models;
using Guia1.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Guia1.ViewModels
{
    class ProdSubDefinirViewModel : BindableBase, INotifyPropertyChanged, INavigationAware
    {
#pragma warning disable CS0108 // El miembro oculta el miembro heredado. Falta una contraseña nueva
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0108 // El miembro oculta el miembro heredado. Falta una contraseña nueva
        #region servicios
        DataService dataService;
        DialogService dialogService;
        #endregion
        #region propiedades
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

        List<ProductoPrincipal> ListaProductoPrincipal;


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

        #endregion

        #region commands
        INavigationService _navigationService;

        public DelegateCommand NavigateCommand { get; private set; }


        public ICommand Refrescar
        {

            get
            {
                return new RelayCommand(RefrescarCommand);
            }

        }

        private async void RefrescarCommand()
        {

            foreach (var list in ListaProducto)
            {
                await dialogService.ShowMessage("Dependencia", list.Dependencia);
            }
        }

        public ICommand Complete
        {

            get
            {
                return new RelayCommand(Display);
            }

        }

        //Mostrar los Sub-Productos en pantalla luego de la definición de cuantos son
        public void Display()
        {


            Producto = new List<ProductoA>();
            ProductoPrincipal = new ProductoPrincipal { Nombre = NombreProducto, Productos = Producto };
            ListaProductoPrincipal.Add(ProductoPrincipal);
            Producto.Add(new ProductoA { Nombres = NombreProducto });
            for (int i = 0; i < int.Parse(CantP); i++)
            {

                Producto.Add(new ProductoA {IsEnabledNombre=true, IsEnabledDependencia = true });

                IsEnable = false;
                if (i == 0)
                {
                    Producto.First().Dependencia = "0";
                    Producto.First().Cantidad = "1";

                }

            }
            ListaProducto = new ObservableCollection<ProductoA>(Producto);
        }


        public ICommand BorrarCommand
        {
            get
            {
                return new RelayCommand(Borrar);
            }
        }
        private async void Borrar()
        {

            dataService.Delete(ProductoPrincipal);
            foreach (var productoA in ListaProducto)
            {
              
                foreach(var SemanaA in dataService.Get<SemanasA>(true).Where(x => x.ProductoId == productoA.ProductoId))
                {
                    dataService.Delete(SemanaA);
                }
                dataService.Delete(productoA);
            }
            ListaProducto.Clear();
            ProductoPrincipal.Borrar = true;
            await dialogService.ShowMessage(
               "Mensaje", "Se borró el contenido con exito"
               );
        }
        public ICommand GuardarCommand
        {
            get
            {
                return new RelayCommand(Guardar);
            }

        }
        private async void Guardar()
        {

            //// saber si hay un casillero nulo en clasificación
            //foreach (var lista in ListaProducto)
            //{
            //    if (string.IsNullOrEmpty(lista.Clasificacion))
            //    {
            //        await dialogService.ShowMessage(
            //     "Error", "Agregar el código de clasificación a todos los elementos"
            //     );
            //        return;
            //    }
            //}
            ////            filtro para clasificaciones que no cumplen con la nomenclatura


            //foreach (var lista in ListaProducto)
            //{
            //    var NewLista = lista.Clasificacion.Replace(".", "").Replace("-", "").Replace("_", "");

            //    int result = 0;
            //    if (!int.TryParse(NewLista, out result))
            //    {
            //        await dialogService.ShowMessage(
            //              "Error", "El código " + lista.Clasificacion + " no es vàlido" + ". Ingresar el código con el siguiente formato:1.1.2"
            //          );

            //        return;
            //    }
            //}
            //aviso de valores repetidos         
            //Primero se selecciona la "Clasificacion" como parámetro de agrupación y luego se pone como condición que sean mayor que 1 para agruparlos
            //var agrupar = ListaProducto.GroupBy(x => x.Clasificacion.Replace(".", "").Replace("-", "").Replace("_", "")).Where(x => x.Count() > 1).ToList();
            ////lista con los Keys armados
            //foreach (var listaKey in agrupar)
            //{
            //    //lista con los ProductoA.Clasificacion en cada Key
            //    foreach (var listaClasif in listaKey)
            //    {

            //        await dialogService.ShowMessage(
            //           "Error", "El código de clasificación "
            //           + listaClasif.Clasificacion + " se repite "
            //           + listaKey.Count()
            //           + " veces. Debe haber un número de clasificación distinto por cada producto."
            //       );
            //        return;

            //    }
            //}

            dataService.Save(ListaProductoPrincipal, true);
            dataService.Save(ProductoPrincipal.Productos, true);
            dataService.Update(ProductoPrincipal, true);
            await dialogService.ShowMessage(
                          "Mensaje", "El producto se guardó exitosamente"
                      );

        }




        #endregion
        public ProdSubDefinirViewModel(INavigationService navigationService)
        {
            IsEnable = true;
            ListaProductoPrincipal = new List<ProductoPrincipal>();
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand(Navigate);
            dialogService = new DialogService();
            dataService = new DataService();
        }

        private async void Navigate()
        {
            if (ProductoPrincipal != null)
            {
                if (ProductoPrincipal.Calculo == true)
                {
                    var parametros = new NavigationParameters();
                    parametros.Add("Item", ProductoPrincipal);
                    await _navigationService.GoBackAsync(parametros);

                }
                else { 
                await _navigationService.GoBackAsync();
                }
            }

        }

        public async void OnNavigatedFrom(NavigationParameters parameters)
        {
           
        }
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Item"))
            {
                IsEnable = false;

                ProductoPrincipal = (ProductoPrincipal)parameters["Item"];
                ProductoPrincipal.Calculo = true;
                foreach (var lista in ProductoPrincipal.Productos)
                {
                    lista.IsEnabledDependencia = true;
                }

                ListaProducto = new ObservableCollection<ProductoA>(ProductoPrincipal.Productos);


            }
        }


        public async void OnNavigatingTo(NavigationParameters parameters)
        {
           
        }
    }
}