using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using Xamarin.Forms;
using System.IO;
using Guia1.Interfaces;
using Guia1.Models;
using SQLiteNetExtensions.Extensions;

namespace Guia1.Data

{
    public class DataAccess : IDisposable
    {
        SQLiteConnection connection;

        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            connection = new SQLiteConnection(config.Platform,
               Path.Combine(config.DirectoryDB, "MRP-Producto.db3"));
            connection.CreateTable<ProductoPrincipal>();
            connection.CreateTable<ProductoA>();
                
        }

        public void Insert<T>(T model, bool WithChildren)
        {
            if (WithChildren)
            {
                connection.InsertWithChildren(model);
            }
            else { 
            connection.Insert(model);
            }
        }

        public void Update<T>(T model,bool WithChildren)
        {
            if (WithChildren) { 
                connection.UpdateWithChildren(model);
            }
           else
            {
                connection.Update(model);
            }
        }


        public void Delete<T>(T model)
        {
            connection.Delete(model);
        }


        public void DeleteAll<T>()
        {
            connection.DeleteAll<T>();
        }

        public T First<T>(bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return connection.GetAllWithChildren<T>().FirstOrDefault();
            }
            else
            {
                return connection.Table<T>().FirstOrDefault();
            }
        }

        public List<T> GetList<T>(bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return connection.GetAllWithChildren<T>().ToList();
            }
            else
            {
                return connection.Table<T>().ToList();
            }
        }

        public T Find<T>(int pk, bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return connection
                    .GetAllWithChildren<T>()
                    .FirstOrDefault(m => m.GetHashCode() == pk);
            }
            else
            {
                return connection
                    .Table<T>()
                    .FirstOrDefault(m => m.GetHashCode() == pk);
            }
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
