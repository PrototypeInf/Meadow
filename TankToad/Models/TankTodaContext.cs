using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Twilio.Rest.Api.V2010.Account;
using System.Data.Entity;

namespace TankToad.Models
{
    public class TankToadContext:DbContext
    {
        public DbSet<SMS> SMS { get; set; }
        public DbSet<Data> Datas { get; set; }
        public DbSet<DeviceAttributes> DeviceAttributes { get; set; }
        public DbSet<DeviceSpecificConstants> DeviceSpecificConstants { get; set; }
        public DbSet<Diagnostics> Diagnostics { get; set; }
        public DbSet<DeviceAttributesLog> DeviceAttributesLogs { get; set; }
    }
}