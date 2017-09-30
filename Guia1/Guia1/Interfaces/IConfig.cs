﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Interop;

namespace Guia1.Interfaces
{
    public interface IConfig
    {
         string DirectoryDB { get;  }
         ISQLitePlatform Platform { get;}

    }
}
