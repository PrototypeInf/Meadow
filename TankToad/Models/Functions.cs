using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Globalization;
namespace TankToad.Models
{
    public static class ModelsFunctions
    {
        public static string ToString(DateTime? dateTime)
        {
            return dateTime != null ? ((DateTime)dateTime).ToString("dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture) : "";
        }
    }
}