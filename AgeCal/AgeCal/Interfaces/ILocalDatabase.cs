﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Interfaces
{
    public interface ILocalDatabase
    {
        SQLiteConnection Connection();
    }
}
