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
    [Authorize(Roles = "admin")]
    [RoutePrefix("api/DeviceAttributesLogs")]
    public class DeviceAttributesLogsController : ApiController
    {
        private TankToadContext db = new TankToadContext();

        // GET: api/DeviceAttributesLogs
        public IQueryable<DeviceAttributesLog> GetDeviceAttributesLogs()
        {
            return db.DeviceAttributesLogs;
        }

        // GET: api/DeviceAttributesLogs/5
        [ResponseType(typeof(DeviceAttributesLog))]
        public async Task<IHttpActionResult> GetDeviceAttributesLog(int id)
        {
            DeviceAttributesLog deviceAttributesLog = await db.DeviceAttributesLogs.FindAsync(id);
            if (deviceAttributesLog == null)
            {
                return NotFound();
            }

            return Ok(deviceAttributesLog);
        }

        // PUT: api/DeviceAttributesLogs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDeviceAttributesLog(int id, DeviceAttributesLog deviceAttributesLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deviceAttributesLog.Id)
            {
                return BadRequest();
            }

            db.Entry(deviceAttributesLog).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceAttributesLogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/DeviceAttributesLogs
        [ResponseType(typeof(DeviceAttributesLog))]
        public async Task<IHttpActionResult> PostDeviceAttributesLog(DeviceAttributesLog deviceAttributesLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DeviceAttributesLogs.Add(deviceAttributesLog);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = deviceAttributesLog.Id }, deviceAttributesLog);
        }

        // DELETE: api/DeviceAttributesLogs/5
        [ResponseType(typeof(DeviceAttributesLog))]
        public async Task<IHttpActionResult> DeleteDeviceAttributesLog(int id)
        {
            DeviceAttributesLog deviceAttributesLog = await db.DeviceAttributesLogs.FindAsync(id);
            if (deviceAttributesLog == null)
            {
                return NotFound();
            }

            db.DeviceAttributesLogs.Remove(deviceAttributesLog);
            await db.SaveChangesAsync();

            return Ok(deviceAttributesLog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeviceAttributesLogExists(int id)
        {
            return db.DeviceAttributesLogs.Count(e => e.Id == id) > 0;
        }
    }
}