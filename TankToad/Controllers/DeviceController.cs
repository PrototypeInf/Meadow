using System;
using System.Collections.Generic;

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TankToad.Models;

namespace TankToad.Controllers
{
    class DeviceMethods:ApiController
    {
        private TankToadContext db;
        public DeviceMethods()
        {
            db = new TankToadContext();
        }
        public IHttpActionResult SaveToDeviseALog(DeviceAttributes deviceAttributes)
        {
            DeviceAttributesLog deviceAttributesLog = new DeviceAttributesLog()
            {
                BatteryLowLevel = deviceAttributes.BatteryLowLevel,
                BatteryShutdownLevel = deviceAttributes.BatteryShutdownLevel,
                BatteryTopLevel = deviceAttributes.BatteryTopLevel,
                CellNumber = deviceAttributes.CellNumber,
                CurrentAssignedCustomerName = deviceAttributes.CurrentAssignedCustomerName,
                CurrentLocationLatitude = deviceAttributes.CurrentLocationLatitude,
                CurrentLocationLongitude = deviceAttributes.CurrentLocationLongitude,
                CurrentLocationTimeZone = deviceAttributes.CurrentLocationTimeZone,
                CustomerPhoneNumber = deviceAttributes.CustomerPhoneNumber,
                DeviceAttributesId = deviceAttributes.Id,
                EnergyMode = deviceAttributes.EnergyMode,
                FirmwareBranch = deviceAttributes.FirmwareBranch,
                FirmwareCommit = deviceAttributes.FirmwareCommit,
                FirmwareName = deviceAttributes.FirmwareName,
                GatewayPhoneNumber = deviceAttributes.GatewayPhoneNumber,
                HardwareVersion = deviceAttributes.HardwareVersion,
                IMEI = deviceAttributes.IMEI,
                Name = deviceAttributes.Name,
                NotesAboutTheDevice = deviceAttributes.NotesAboutTheDevice,
                NumberOfSleepPeriods = deviceAttributes.NumberOfSleepPeriods,
                OperationMode = deviceAttributes.OperationMode,
                Operator = deviceAttributes.Operator,
                SignalQuality = deviceAttributes.SignalQuality,
                SIMnumber = deviceAttributes.SIMnumber,
                SIMtype = deviceAttributes.SIMtype,
                SleepPeriod = deviceAttributes.SleepPeriod,
                Status = deviceAttributes.Status,
                TimeOfAlert = deviceAttributes.TimeOfAlert,
                VoltageFeedback = deviceAttributes.VoltageFeedback,
                WaterHighLevel = deviceAttributes.WaterHighLevel,
                WaterLowLevel = deviceAttributes.WaterLowLevel,

                SMSId=deviceAttributes.SMSId
            };

            db.DeviceAttributesLogs.Add(deviceAttributesLog);
            db.SaveChanges();
            return Ok(deviceAttributesLog);
        }
    }

    [Authorize(Roles = "admin")]
    [RoutePrefix("api/Device")]
    public class DeviceController : ApiController
    {
        private TankToadContext db = new TankToadContext();
        private DeviceMethods deviceMethods = new DeviceMethods();

        [Route("Coords")]
        public async Task<IHttpActionResult> GetCoordsDeviceAttributes()
        {
            //List<Dictionary<string, string>> res = new List<Dictionary<string, string>>();
            var res = db.DeviceAttributes
                .Where(d => d.Status == "active")
                .Select(d => new
                {
                    d.Id,
                    d.Name,
                    d.CurrentLocationLatitude,
                    d.CurrentLocationLongitude
                });

           return Ok(res);
        }

        // GET: api/Device
        public IQueryable<DeviceAttributes> GetDeviceAttributes()
        {
            return db.DeviceAttributes;
        }

        // GET: api/Device/5
        [ResponseType(typeof(DeviceAttributes))]
        public async Task<IHttpActionResult> GetDeviceAttributes(int id)
        {
            DeviceAttributes deviceAttributes = await db.DeviceAttributes.FindAsync(id);
            if (deviceAttributes == null)
            {
                return NotFound();
            }

            return Ok(deviceAttributes);
        }

        // PUT: api/Device/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDeviceAttributes(int id, DeviceAttributes deviceAttributes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(deviceAttributes).State = EntityState.Modified;

            try
            {
                 db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceAttributesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            deviceMethods.SaveToDeviseALog(deviceAttributes);

            return Ok(deviceAttributes);
        }

        // POST: api/Device
        [ResponseType(typeof(DeviceAttributes))]
        public async Task<IHttpActionResult> PostDeviceAttributes(DeviceAttributes deviceAttributes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DeviceAttributes.Add(deviceAttributes);
            await db.SaveChangesAsync();
            deviceMethods.SaveToDeviseALog(deviceAttributes);

            return CreatedAtRoute("DefaultApi", new { id = deviceAttributes.Id }, deviceAttributes);
        }

        // DELETE: api/Device/5
        [ResponseType(typeof(DeviceAttributes))]
        public async Task<IHttpActionResult> DeleteDeviceAttributes(int id)
        {
            DeviceAttributes deviceAttributes = await db.DeviceAttributes.FindAsync(id);
            if (deviceAttributes == null)
            {
                return NotFound();
            }

            db.DeviceAttributes.Remove(deviceAttributes);
            await db.SaveChangesAsync();

            return Ok(deviceAttributes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeviceAttributesExists(int id)
        {
            return db.DeviceAttributes.Count(e => e.Id == id) > 0;
        }
    }
}