using GalaSoft.MvvmLight.Command;
using Guia1.Interfaces;
using Guia1.Models;
using Guia1.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Guia1.ViewModels
{
    public class HojaCalculoViewModel : BindableBase, INotifyPropertyChanged, INavigationAware
    {

#pragma warning disable CS0108 // El miembro oculta el miembro heredado. Falta una contraseña nueva
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0108 // El miembro oculta el miembro heredado. Falta una contraseña nueva
        INavigationService _navigationService;
        DialogService dialogService;
        public DataService dataService;
        public DelegateCommand NavigateCommand { get; private set; }
        public int Index { get; private set; }
        public int cont { get; private set; }

        #region Propiedades y atributos
        public ProductoA _seleccionProducto;
        public ProductoA SeleccionProducto
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
        public string _nombreProducto;
        public string NombreProducto
        {
            get { return _nombreProducto; }
            set
            {
                if (_nombreProducto != value)
                {
                    _nombreProducto = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NombreProducto)));
                }
            }

        }


        public ObservableCollection<SemanasA> _semanasCollection;
        public ObservableCollection<SemanasA> SemanasCollection
        {
            get { return _semanasCollection; }
            set
            {
                if (_semanasCollection != value)
                {
                    _semanasCollection = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SemanasCollection)));
                }
            }

        }

        public List<SemanasA> _semanasLista;
        public List<SemanasA> SemanasLista
        {
            get { return _semanasLista; }
            set
            {
                if (_semanasLista != value)
                {
                    _semanasLista = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SemanasLista)));
                }
            }

        }

        public List<ProductoA> _productoALista;
        public List<ProductoA> ProductoALista
        {
            get { return _productoALista; }
            set
            {
                if (_productoALista != value)
                {
                    _productoALista = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProductoALista)));
                }
            }

        }

        public bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }

        }
        public bool _visibleReCalcular;
        public bool VisibleReCalcular
        {
            get { return _visibleReCalcular; }
            set
            {
                if (_visibleReCalcular != value)
                {
                    _visibleReCalcular = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VisibleReCalcular)));
                }
            }

        }
        public string _cantSemanas;
        public string CantSemanas
        {
            get { return _cantSemanas; }
            set
            {
                if (_cantSemanas != value)
                {
                    _cantSemanas = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CantSemanas)));
                }
            }

        }

        public string _invInicial;
        public string InvInicial
        {
            get { return _invInicial; }
            set
            {
                if (_invInicial != value)
                {
                    _invInicial = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InvInicial)));
                }
            }

        }

        public string _requerimientoBruto;
        public string RequerimientoBruto
        {
            get { return _requerimientoBruto; }
            set
            {
                if (_requerimientoBruto != value)
                {
                    _requerimientoBruto = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RequerimientoBruto)));
                }
            }

        }

        public string _tiempoFabricación;
        public string TiempoFabricacion
        {
            get { return _tiempoFabricación; }
            set
            {
                if (_tiempoFabricación != value)
                {
                    _tiempoFabricación = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TiempoFabricacion)));
                }
            }

        }


        public SemanasA _semanas;
        public SemanasA Semanas
        {
            get { return _semanas; }
            set
            {
                if (_semanas != value)
                {
                    _semanas = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Semanas)));
                }
            }

        }

        public bool _libOrdenvisible;
        public bool LibOrdenVisible
        {
            get { return _libOrdenvisible; }
            set
            {
                if (_libOrdenvisible != value)
                {
                    _libOrdenvisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LibOrdenVisible)));
                }
            }

        }

        public bool _semanasDias;
        public bool SemanasDias
        {
            get { return _semanasDias; }
            set
            {
                if (_semanasDias != value)
                {
                    _semanasDias = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SemanasDias)));
                }
            }

        }


        public string _semanasDiasCalculo;
        public string SemanasDiasCalculo
        {
            get { return _semanasDiasCalculo; }
            set
            {
                if (_semanasDiasCalculo != value)
                {
                    _semanasDiasCalculo = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SemanasDiasCalculo)));
                }
            }

        }

        public bool _isVisiblePropiedad;
        public bool IsVisiblePropiedad
        {
            get { return _isVisiblePropiedad; }
            set
            {
                if (_isVisiblePropiedad != value)
                {
                    _isVisiblePropiedad = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsVisiblePropiedad)));
                }
            }

        }

        public DateTime _dateSelected;
        public DateTime DateSelected
        {
            get { return _dateSelected; }
            set
            {
                if (_dateSelected != value)
                {
                    _dateSelected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DateSelected)));
                }
            }

        }

        public string _dateSelectedLibOrden;
        public string DateSelectedLibOrden
        {
            get { return _dateSelectedLibOrden; }
            set
            {
                if (_dateSelectedLibOrden != value)
                {
                    _dateSelectedLibOrden = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DateSelectedLibOrden)));
                }
            }

        }

        public bool _isVisibleDate;
        public bool IsVisibleDate
        {
            get { return _isVisibleDate; }
            set
            {
                if (_isVisibleDate != value)
                {
                    _isVisibleDate = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsVisibleDate)));
                }
            }

        }
        public ProductoPrincipal _productoPricipal;
        public ProductoPrincipal ProductoPrincipal
        {
            get { return _productoPricipal; }
            set
            {
                if (_productoPricipal != value)
                {
                    _productoPricipal = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProductoPrincipal)));
                }
            }

        }
        #endregion
        #region Commands
        public ICommand Editar
        {
            get
            {
                return new RelayCommand(EditarEvent);
            }
        }

        private void EditarEvent()
        {
            NavigationProdSubDefinir(ProductoPrincipal);
        }


        public ICommand PasarPaginaCommand
        {
            get
            {
                return new RelayCommand(PasarPagina);
            }
        }

        private async void PasarPagina()
        {
          
        if (ProductoPrincipal.Calculo == false)
            { 
                await dialogService.ShowMessage("Error", "Se debe realizar, en primera medida, el cálculo");
                return;
            }
         
            cont++;
            if (cont >= ProductoPrincipal.Productos.Count)
            {
                cont = 0;
            }
            if (ProductoPrincipal.Productos[cont].Semanas.All(x=>x==null))
            {
                await dialogService.ShowMessage("Error", "Se debe realizar, en primera medida, el cálculo con el nuevo sub-producto agregado");
                cont = 0;
            }
            pasarLista(cont);
        }

        private  void pasarLista(int cont)
        {
            if (ProductoPrincipal != null)
            {
                NombreProducto = ProductoPrincipal.Productos[cont].Nombres;
            }
            if (ProductoPrincipal.Borrar == true)
            {
                NombreProducto = "Producto Borrado";
            }
           
            SemanasCollection = new ObservableCollection<SemanasA>(ProductoPrincipal.Productos[cont].Semanas);
        }

        public ICommand Complete
        {
            get
            {
                return new RelayCommand(Display);
            }

        }
        //Despliegue de semanas o días para calcular
        private async void Display()
        {
            if(DateSelected.DayOfWeek==DayOfWeek.Saturday || DateSelected.DayOfWeek== DayOfWeek.Sunday)
            {
                await dialogService.ShowMessage("Mensaje", "La fecha seleccionada " + DateSelected.ToString().Remove(10) + " pertenece al fin de semana. Por lo que se tomará en cuenta el día de semana más próximo.");
            }

            var difDias = 0;
            if(int.Parse(CantSemanas)<= TiempoFabricacionTotal())
            {
                await dialogService.ShowMessage("Error", "La cantidad de días debe ser mayor, ya que el tiempo de fabricación total será de "+ TiempoFabricacionTotal()+" días");
                return;
            }
            foreach (var productoA in ProductoPrincipal.Productos)
            {

                productoA.Semanas = new List<SemanasA>(); 

                if (string.IsNullOrEmpty(productoA.TiempoFabricacion))
                {
                    productoA.TiempoFabricacion = 0.ToString();
                }
              

                difDias = DateSelected.DayOfYear - DateTime.Today.DayOfYear;

                var contador = 0;
                //Agregado de objetos a la lista, empieza en 1 para poder visualizar desde ese número y no desde 0
                for (int i = 1; i <= (int.Parse(CantSemanas) + difDias); i++)
                {

                    Semanas = new SemanasA();
                    if (DateSelected.Date == DateTime.Today)
                    {
                        //Agregado de fecha 
                        if (DateSelected.Date.AddDays(contador).DayOfWeek== DayOfWeek.Saturday || DateSelected.Date.AddDays(contador).DayOfWeek == DayOfWeek.Sunday)
                        {
              
                            contador = contador + 2;
                            Semanas.FechaDias = DateSelected.Date.AddDays(contador).ToString().Remove(10);
                        }
                        else {
                        Semanas.FechaDias = DateSelected.Date.AddDays(contador).ToString().Remove(10);
                        }


                    }
                    else
                    {
                        //Agregado de fecha al lado del dia con la resta necesaria para empezar la cuenta desde el día de hoy
                        if (DateSelected.Date.AddDays(contador - difDias).DayOfWeek == DayOfWeek.Saturday || DateSelected.Date.AddDays(contador - difDias).DayOfWeek == DayOfWeek.Sunday)
                        {

                            contador = contador + 2;
                            Semanas.FechaDias = DateSelected.Date.AddDays(contador - difDias).ToString().Remove(10);
                        }
                        else
                        {
                            Semanas.FechaDias = DateSelected.Date.AddDays(contador - difDias).ToString().Remove(10);
                        }
                    }
                    contador++;
                    Semanas.IsEnabled = true;
                    Semanas.SemanasNumero = i.ToString();
                    Semanas.SemanasDiasCalculo = SemanasDiasCalculo.Replace("s", "");
                    productoA.Semanas.Add(Semanas);
                }

                //Bloqueo de días anteriores a los permitidos por la fecha de fabricación
                if (DateSelected.Date == DateTime.Today)
                {
                    
                        for (int i = 0; i < TiempoFabricacionTotal(); i++)
                    {
                        productoA.Semanas[i].IsEnabled = false;
                    }
                }
                if (DateSelected.Date != DateTime.Today)
                {
                    for (int i = 0; i < difDias; i++)
                    {
                        productoA.Semanas[i].IsEnabled = false;
                    }
                }

                IsEnabled = true;
            }
            SemanasCollection = new ObservableCollection<SemanasA>(ProductoPrincipal.Productos[0].Semanas);
        }

        private  int TiempoFabricacionTotal()
        {
            var TiempoTotal = 0;
            //Separa en grupos definidos por la dependencia
            foreach (var grupo in ProductoPrincipal.Productos.GroupBy(x=>x.Dependencia).ToList())
            {

               //Saca El máximo valor de cada Key del grupo creado
                 TiempoTotal = TiempoTotal + int.Parse(grupo.Max(x => x.TiempoFabricacion));
                
            }
              
            return TiempoTotal;
        }

        public ICommand CalcularCommand
        {
            get
            {
                return new RelayCommand(Calcular);
            }
        }
        //Càlculo del MRP con las semanas o días definidos
        private async void Calcular()
        {
           
                foreach (var productoA in ProductoPrincipal.Productos)
            {
                    if (productoA.Cantidad == null)
                {
                    productoA.Cantidad = 1.ToString();
                }

                if (productoA.InventarioInicial == null || string.IsNullOrEmpty(productoA.InventarioInicial))
                {
                    productoA.InventarioInicial = 0.ToString();
                }

                else if (ProductoPrincipal.Productos[0].Semanas.All(x => x.ReqBruto == null))
                {
                    await dialogService.ShowMessage("Error", "Al menos un campo con requerimiento bruto debe contener un valor mayor a cero"
                        );
                    return;
                }
                else
                {
                    foreach (var list in productoA.Semanas.Where(x => x.ReqBruto == null).ToList())
                    {
                        list.ReqBruto = 0.ToString();
                    }
                    IsVisiblePropiedad = false;
                }
                var difDias = 0;
                difDias = DateSelected.DayOfYear - DateTime.Today.DayOfYear;
                for (int i = 0; i < int.Parse(CantSemanas)+difDias; i++)
                {

                    //calculo primer dia
                    if (i == 0)
                    {
                        if (productoA.Dependencia != "0")
                        {
                            //Busca el producto con el mismo nombre que la dependencia
                            var libOrden = ProductoPrincipal.Productos.Find(x => x.Nombres == productoA.Dependencia);
                            productoA.Semanas[i].ReqBruto = libOrden.Semanas[i].LibOrden ;
                        }
                        if (string.IsNullOrEmpty(productoA.Semanas[i].ReqBruto))
                        {
                            productoA.Semanas[i].ReqBruto = 0.ToString();
                        }
                        productoA.Semanas[i].ReqBruto = (int.Parse(productoA.Semanas[i].ReqBruto) * int.Parse(productoA.Cantidad)).ToString();
                        productoA.Semanas[i].InventarioInicial = productoA.InventarioInicial;
                        productoA.Semanas[i].ReqNeto = (int.Parse(productoA.Semanas[i].ReqBruto) - int.Parse(productoA.Semanas[i].InventarioInicial)).ToString();
                        productoA.Semanas[i].InvFinal = (int.Parse(productoA.Semanas[i].InventarioInicial) - int.Parse(productoA.Semanas[i].ReqBruto)).ToString();
                        if (int.Parse(productoA.Semanas[i].InvFinal) < 0)
                        {
                            await dialogService.ShowMessage("Error", "El producto "
                            + productoA.Nombres + " Exige una orden un día anterior al comtemplado."
                            );
                            return;
                        }
                        if (int.Parse(productoA.Semanas[i].ReqNeto) <= 0)
                        {
                            productoA.Semanas[i].ReqNeto = 0.ToString();
                        }
                    }
                    else if (i > 0)
                    {

                        if (productoA.Dependencia != "0")
                        {
                            var libOrden = ProductoPrincipal.Productos.Find(x => x.Nombres == productoA.Dependencia);
                            productoA.Semanas[i].ReqBruto =libOrden.Semanas[i].LibOrden;
                        }
                        if (productoA.Semanas[i].ReqBruto == null)
                        {
                            productoA.Semanas[i].ReqBruto = 0.ToString();
                        }
                        productoA.Semanas[i].ReqBruto = (int.Parse(productoA.Semanas[i].ReqBruto) * int.Parse(productoA.Cantidad)).ToString();
                        productoA.Semanas[i].InventarioInicial = productoA.Semanas[i - 1].InvFinal;
                        productoA.Semanas[i].ReqNeto = (int.Parse(productoA.Semanas[i].ReqBruto) - int.Parse(productoA.Semanas[i].InventarioInicial)).ToString();
                        if (int.Parse(productoA.Semanas[i].ReqNeto) <= 0)
                        {
                            productoA.Semanas[i].ReqNeto = 0.ToString();
                        }
                        productoA.Semanas[i].InvFinal = (int.Parse(productoA.Semanas[i].InventarioInicial) - int.Parse(productoA.Semanas[i].ReqBruto)).ToString();
                        if (int.Parse(productoA.Semanas[i].InvFinal) < 0)
                        {
                            //Index para ubicar en la lista el objeto guardado donde se tiene que hacer el pedido

                            Index = i - int.Parse(productoA.TiempoFabricacion);
                            //if (Index < 0)
                            //{
                            //    await dialogService.ShowMessage("Error", "El plazo de fabricación debe estar contemplado dentro del rango de semanas");
                            //    return;
                            //}

                            //liberación de la orden
                            productoA.Semanas[Index].LibOrden = productoA.Semanas[i].ReqNeto;
                            productoA.Semanas[Index].IsVisibleDate = true;
                            if (SemanasDias == false)
                            {
                                //Calculo de la fecha donde se debe realizar el pedido
                                productoA.Semanas[Index].DateSelectedLibOrden = DateSelected.Date.AddDays(Index).ToString().Remove(10);

                            }
                            else
                            {
                                productoA.Semanas[Index].DateSelectedLibOrden = DateSelected.AddDays(Index * 7).ToString().Remove(10);

                            }
                            productoA.Semanas[i].InvFinal = (int.Parse(productoA.Semanas[i].InventarioInicial) + int.Parse(productoA.Semanas[Index].LibOrden) - int.Parse(productoA.Semanas[i].ReqBruto)).ToString();

                        }
                    }
                    ProductoPrincipal.Calculo = true;
                }
        
                
            }
            foreach(var productoA in ProductoPrincipal.Productos)
            {
                dataService.Delete(productoA);
                dataService.Insert(productoA, true);
            }

            dataService.Update(ProductoPrincipal, true);
            SemanasCollection = new ObservableCollection<SemanasA>(ProductoPrincipal.Productos[0].Semanas);
            IsEnabled = false;
            VisibleReCalcular = true;

        }
        public ICommand ReCalcularCommand
        {
            get
            {
                return new RelayCommand(ReCalcular);
            }
        }
        public ICommand XlsCommand
        {
            get
            {
                return new RelayCommand(Xls);
            }
        }

        private async void Xls()
        {
            if (ProductoPrincipal.Calculo == false)
            {
                await dialogService.ShowMessage("Error", "Se debe realizar, en primera medida, el cálculo");
                return;
            }
            using (ExcelEngine excelEngine = new ExcelEngine())
            {

                cont = 0;
                //Seleccionar versión de Excel 2013
                excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2013;

                //Crear workbook con una hoja de trabajo
                IWorkbook workbook = excelEngine.Excel.Workbooks.Create(1);


                foreach (var productoA in ProductoPrincipal.Productos)         
                {
                    CantSemanas = productoA.Semanas.Count.ToString();
                   
                   
                    //Acceder a la primera hoja de trabajo desde la instancia de workbook
                    IWorksheet worksheet = workbook.Worksheets[0];

                    IMigrantRange migrantRange = worksheet.MigrantRange;
                    // Writing Data.
                    //cont aumenta en 7 la posición de las filas en cada producto, las columnas dependen de los días elegidos
                       

                        worksheet["A" + (cont+1).ToString()].Text = productoA.Nombres;
                  
                        migrantRange["A" + (2 + cont).ToString()].Text = "Requerimiento Bruto";
               
                        migrantRange["A" + (3 + cont).ToString()].Text = "Inventario Inicial";
                  
                        migrantRange["A" + (4 + cont).ToString()].Text = "Requerimiento Neto";
                   
                        migrantRange["A" + (5 + cont).ToString()].Text = "Liberación Orden";
                    
                        migrantRange["A" + (6 + cont).ToString()].Text = "Inventario Final";
                    
                    //Recorrido de columnas desde la columna 2 en adelante 
                    for (int column = 2; column < int.Parse(CantSemanas) + 2; column++)
                        {
                         
                            migrantRange.ResetRowColumn(cont+1, column);
                            migrantRange.Text = "Día" + (column - 1).ToString()+"  "+ DateSelected.Date.AddDays(column-2).ToString().Remove(10); ;
                            migrantRange.ResetRowColumn(cont+2, column);
                            migrantRange.Text = productoA.Semanas[column - 2].ReqBruto;
                            migrantRange.ResetRowColumn(cont+3, column);
                            migrantRange.Text = productoA.Semanas[column - 2].InventarioInicial;
                            migrantRange.ResetRowColumn(cont+4, column);
                            migrantRange.Text = productoA.Semanas[column - 2].ReqNeto;
                            migrantRange.ResetRowColumn(cont+5, column);
                            migrantRange.Text = productoA.Semanas[column - 2].LibOrden;
                            migrantRange.ResetRowColumn(cont+6, column);
                            migrantRange.Text = productoA.Semanas[column - 2].InvFinal;
                        }
                    cont = cont + 7;
                    worksheet.UsedRange.AutofitColumns();
                }
                //Save the workbook to stream in xlsx format. 
                    MemoryStream stream = new MemoryStream();
                    workbook.SaveAs(stream);

                    workbook.Close();
                
                //Save the stream as a file in the device and invoke it for viewing
              await DependencyService.Get<ISave>().SaveAndView(ProductoPrincipal.Nombre+".xlsx", "application/msexcel", stream);

                await dialogService.ShowMessage("Mensaje", "El Producto se guardó como hoja de Excel en la carpeta MRP");
            }
        }
        private void ReCalcular()
        {
           
            ProductoPrincipal.Calculo = false;

            foreach (var productoA in ProductoPrincipal.Productos)
            {//Borrado de todas las semanas en la base de datos
                foreach (var semanas in productoA.Semanas)
                {
                    dataService.Delete(semanas);
                }
                //Limpiando las listas y la UI
                productoA.Semanas.Clear();
                SemanasCollection = new ObservableCollection<SemanasA>(productoA.Semanas);
            }

            NombreProducto = ProductoPrincipal.Productos.First().Nombres;
            VisibleReCalcular = false;
            IsVisiblePropiedad = true;
            //Actualización del producto principal en la base de datos, así paso el parámetro ProductoPrincipal.Calculo = false y no se lo encuentra en la lista a revisar
            dataService.Update(ProductoPrincipal, true);
        }
        #endregion
        public HojaCalculoViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand(Navigation);
            dialogService = new DialogService();
            dataService = new DataService();
            SemanasDiasCalculo = "Días";
            IsEnabled = false;
            IsVisiblePropiedad = true;
            VisibleReCalcular = false;
            DateSelected = DateTime.Now;

        }

        private async void NavigationProdSubDefinir(ProductoPrincipal productoPrincipal)
        {
            var parametros = new NavigationParameters();
            productoPrincipal.Editar = true;
            parametros.Add("Item", productoPrincipal);
            await _navigationService.NavigateAsync("ProdSubDefinir", parametros);
        }

        private async void Navigation()
        {
            await _navigationService.NavigateAsync("Main");
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
          
            if (parameters.ContainsKey("Item"))
            {
                
                ProductoPrincipal = (ProductoPrincipal)parameters["Item"];
                ProductoPrincipal.Calculo = false;
                if (ProductoPrincipal != null)
                {
                   
                    SeleccionProducto = ProductoPrincipal.Productos.First();
                    
                    NombreProducto = ProductoPrincipal.Nombre;
                    if (SemanasCollection != null)
                    {
                        SemanasCollection.Clear();
                    }
                    if (ProductoPrincipal.Borrar == true)
                    {

                        NombreProducto = "Producto Borrado";
                        ProductoPrincipal.Productos.Clear();
                        if (SemanasCollection != null)
                        {
                            SemanasCollection.Clear();
                        }
                    }
                }
                }

            else if(parameters.ContainsKey("Revisar"))
            {
                ProductoPrincipal = (ProductoPrincipal)parameters["Revisar"];
                if (ProductoPrincipal != null)
                {
                    IsVisiblePropiedad = false;
                    IsEnabled = false;
                    NombreProducto = ProductoPrincipal.Nombre;
                    ProductoPrincipal.Calculo = true;
                    ProductoPrincipal.Productos = dataService.Get<ProductoA>(true).Where(x => x.ProductoPrincipal.ProductoPrincipalId == ProductoPrincipal.ProductoPrincipalId).ToList();
                    SemanasCollection = new ObservableCollection<SemanasA>(ProductoPrincipal.Productos.First().Semanas);
                }
            }

            if (ProductoPrincipal.Borrar == true)
            { 
                NombreProducto = "Producto Borrado";
                IsVisiblePropiedad = false;
                IsEnabled = false;
            }
           
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {



        }
    }
}
