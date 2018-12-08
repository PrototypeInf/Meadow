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
    public class DeviceSpecificConstantsController : ApiController
    {
        private TankToadContext db = new TankToadContext();

        // GET: api/DeviceSpecificConstants
        public IQueryable<DeviceSpecificConstants> GetDeviceSpecificConstants()
        {
            return db.DeviceSpecificConstants;
        }

        // GET: api/DeviceSpecificConstants/5
        [ResponseType(typeof(DeviceSpecificConstants))]
        public async Task<IHttpActionResult> GetDeviceSpecificConstants(int id)
        {
            DeviceSpecificConstants deviceSpecificConstants = await db.DeviceSpecificConstants.FindAsync(id);
            if (deviceSpecificConstants == null)
            {
                return NotFound();
            }

            return Ok(deviceSpecificConstants);
        }

        // PUT: api/DeviceSpecificConstants/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDeviceSpecificConstants(int id, DeviceSpecificConstants deviceSpecificConstants)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(deviceSpecificConstants).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceSpecificConstantsExists(id))
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

        // POST: api/DeviceSpecificConstants
        [ResponseType(typeof(DeviceSpecificConstants))]
        public async Task<IHttpActionResult> PostDeviceSpecificConstants(DeviceSpecificConstants deviceSpecificConstants)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DeviceSpecificConstants.Add(deviceSpecificConstants);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = deviceSpecificConstants.Id }, deviceSpecificConstants);
        }

        // DELETE: api/DeviceSpecificConstants/5
        [ResponseType(typeof(DeviceSpecificConstants))]
        public async Task<IHttpActionResult> DeleteDeviceSpecificConstants(int id)
        {
            DeviceSpecificConstants deviceSpecificConstants = await db.DeviceSpecificConstants.FindAsync(id);
            if (deviceSpecificConstants == null)
            {
                return NotFound();
            }

            db.DeviceSpecificConstants.Remove(deviceSpecificConstants);
            await db.SaveChangesAsync();

            return Ok(deviceSpecificConstants);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeviceSpecificConstantsExists(int id)
        {
            return db.DeviceSpecificConstants.Count(e => e.Id == id) > 0;
        }
    }
}