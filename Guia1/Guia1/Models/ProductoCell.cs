using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Guia1.Models
{
    class ProductoCell : ViewCell
    {
        public ProductoCell()
        {
            var IDProductoLabel = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand

            };
            IDProductoLabel.SetBinding(Label.TextProperty, new Binding("ProductoId"));

            var NombresEntry = new Entry
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            NombresEntry.SetBinding(Entry.PlaceholderProperty, new Binding("Nombres"));

            var ClaseEntry = new Entry
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            ClaseEntry.SetBinding(Entry.TextProperty, new Binding("FechaContrato"));

            //var ActivoSwitch = new Switch
            //{
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    IsEnabled = false
            //};
            //ActivoSwitch.SetBinding(Switch.IsToggledProperty, new Binding("Activo"));

            var panel1 = new StackLayout
            {

                Children =
                {
                    IDProductoLabel,
                    NombresEntry,
                    ClaseEntry

                },
                //Orientation = StackOrientation.Horizontal

            };
            //var panel2 = new StackLayout
            //{

            //    Children =
            //    {
            //        SalarioLabel,
            //        FechaContratoLabel,
            //        ActivoSwitch

            //    },
            //    Orientation = StackOrientation.Horizontal
            //};

            View = new StackLayout
            {

                Children =
                {
                    panel1

                }

            };
        }
    }
}
