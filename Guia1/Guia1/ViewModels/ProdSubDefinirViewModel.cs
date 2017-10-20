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
        

        string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set
            {
                if (_titulo != value)
                {
                    _titulo = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Titulo)));
                }
            }

        }
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
        bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsVisible)));
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


        //Mostrar los Sub-Productos en pantalla luego de la definición de cuantos son
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
            if (string.IsNullOrEmpty(CantP))
            {
                CantP = "0";
            }
            IsEnable = false;
            IsVisible = true;
            Producto = new List<ProductoA>();
            ProductoPrincipal = new ProductoPrincipal { Nombre = NombreProducto, Productos = Producto };
            ListaProductoPrincipal.Add(ProductoPrincipal);
            Producto.Add(new ProductoA { Nombres = NombreProducto, Dependencia="0",Cantidad="1" });
            for (int i = 0; i < int.Parse(CantP); i++)
            {

                Producto.Add(new ProductoA {IsEnabledDependencia = true });

               

            }
            ListaProducto = new ObservableCollection<ProductoA>(Producto);
        }
        //Agregar un subproducto después de haber desplegado la lista
        public ICommand AgregarCommand
        {
            get
            {
                return new RelayCommand(Agregar);
            }
        }
        private async void Agregar()
        {  //Si no viene desde la edición
            if (ProductoPrincipal.Editar != true)
            {
                Producto.Add(new ProductoA { IsEnabledDependencia = true });
                ListaProducto = new ObservableCollection<ProductoA>(Producto);
                await dialogService.ShowMessage(
                   "Mensaje", "Sub-Producto agregado"
                   );
            }
            else
            //Si  viene desde la edición
            {
                ProductoPrincipal.Productos.Add(new ProductoA { IsEnabledDependencia = true });
                ListaProducto = new ObservableCollection<ProductoA>(ProductoPrincipal.Productos);
                await dialogService.ShowMessage(
                   "Mensaje", "Sub-Producto agregado"
                   );
            }
        }
        //Borrar un subproducto después de haber desplegado la lista
        public ICommand Borrar1ArtCommand
        {
            get
            {
                return new RelayCommand(Borrar1Art);
            }
        }
        private async void Borrar1Art()
        {
            //Si no viene desde la edición
            if (ProductoPrincipal.Editar != true)
            { 
            if (ListaProducto.Count() == 1)
            {
                await dialogService.ShowMessage(
               "Mensaje", "No se pueden eliminar más sub-productos"
               );
                return;
            }
            Producto.Remove(Producto.Last());
            ListaProducto = new ObservableCollection<ProductoA>(Producto);
            await dialogService.ShowMessage(
               "Mensaje", "Sub-Producto borrado"
               );
            }
            //Si viene desde la edición
            else
            {
                if (ListaProducto.Count() == 1)
                {
                    await dialogService.ShowMessage(
                   "Mensaje", "No se pueden eliminar más sub-productos"
                   );
                    return;
                }
                ProductoPrincipal.Productos.Remove(Producto.Last());
                ListaProducto = new ObservableCollection<ProductoA>(ProductoPrincipal.Productos);
                await dialogService.ShowMessage(
                   "Mensaje", "Sub-Producto borrado"
                   );
            }
        }
        //Borrar todos
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
            IsVisible = false;
            Titulo = "Producto Borrado";
            await dialogService.ShowMessage(
               "Mensaje", "Se borró el contenido con exito"
               );
}

        //Guardar todos
        public ICommand GuardarCommand
        {
            get
            {
                return new RelayCommand(Guardar);
            }

        }
        private async void Guardar()
        {

            // saber si hay un casillero nulo en dependencia
            foreach (var lista in ListaProducto)
            {
                if (string.IsNullOrEmpty(lista.Dependencia))
                {
                    await dialogService.ShowMessage(
                 "Error", "Agregar dependencia a todos los elementos"
                 );
                    return;
                }
            }

            //filtro para dependencia que no cumplen con la nomenclatura
            foreach (var productoA in ListaProducto)
            {

                if (productoA.Dependencia != "0")
                {
                    if (ListaProducto.Any(x => x.Nombres == productoA.Dependencia) == false)
                    {
                        await dialogService.ShowMessage(
                              "Error", "La dependencia " + productoA.Dependencia + " no es vàlida" + ". Ingresar una que coincida con algún producto creado"
                          );
                        return;
                    }
                }
                else
                {
                    if (ListaProducto.First().Dependencia != "0")
                    {
                        await dialogService.ShowMessage(
                        "Error", "La dependencia " + productoA.Dependencia + " no es vàlida" + ". El valor debe ser de 0"
                    );
                        return;
                    }
                    //aviso de valores repetidos         
                    //Primero se selecciona la "Clasificacion" como parámetro de agrupación y luego se pone como condición que sean mayor que 1 para agruparlos
                    var agrupar = ListaProducto.GroupBy(x => x.Dependencia).Where(x => x.Count() > 1).ToList();
                    //lista con los Keys armados
                    foreach (var listaKey in agrupar)
                    {
                        //lista con los ProductoA.Clasificacion en cada Key
                        foreach (var listaClasif in listaKey)
                        {
                            if (listaClasif.Dependencia == "0")
                            {
                                await dialogService.ShowMessage(
                                   "Error", "La dependencia debe ser distinta de 0."
                               );
                                return;
                            }
                        }
                    }
                }
            }
            if (ListaProducto.First().Nombres != ProductoPrincipal.Nombre)
            {
                await dialogService.ShowMessage(
                                  "Error", "El nombre del primer producto fue cambiado."
                              );
                return;
            }
            //Guardo la lista de productos principales(si existe el producto lo actualiza, y si no lo agrega), 
            //la lista de productosA en el producto principal, 
            //y actualizo el producto principal por si se viene por la zona de edición donde no creo ListaProductoPrincipal

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
            Titulo = "Agregar Producto";
            IsEnable = true;
            ListaProductoPrincipal = new List<ProductoPrincipal>();
            _navigationService = navigationService;
            //NavigateCommand = new DelegateCommand(Navigate);
            dialogService = new DialogService();
            dataService = new DataService();
        }
 //Por ahora no se usa
        //private async void Navigate()
        //{
        //    if (ProductoPrincipal != null)
        //    {
        //        if (ProductoPrincipal.Calculo == true)
        //        {
        //            var parametros = new NavigationParameters();
        //            parametros.Add("Item", ProductoPrincipal);
        //            await _navigationService.GoBackAsync(parametros);

        //        }
        //        else { 
        //        await _navigationService.GoBackAsync();
        //        }
        //    }

        //}

        public  void OnNavigatedFrom(NavigationParameters parameters)
        {
           
        }
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Item"))
            {
                IsEnable = false;
                Titulo = "Editar Producto";
                ProductoPrincipal = (ProductoPrincipal)parameters["Item"];
                ProductoPrincipal.Calculo = true;
                NombreProducto = ProductoPrincipal.Nombre;
                IsEnable = false;
                IsVisible = true;
               

                foreach (var lista in ProductoPrincipal.Productos)
                {
                    if (lista.Dependencia == "0")
                    {
                        lista.IsEnabledDependencia = false;
                    }
                    else { 
                    lista.IsEnabledDependencia = true;
                    }
                }
                
             

                ListaProducto = new ObservableCollection<ProductoA>(ProductoPrincipal.Productos);


            }
        }


        public  void OnNavigatingTo(NavigationParameters parameters)
        {
           
        }
    }
}