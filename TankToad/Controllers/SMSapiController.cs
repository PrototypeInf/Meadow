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
    [RoutePrefix("api/SMSapi")]
    public class SMSapiController : ApiController
    {
        private TankToadContext db = new TankToadContext();

        // GET: api/SMSapi
        public IQueryable<SMS> GetSMS()
        {
            return db.SMS;
        }

        // GET: api/SMSapi/5
        [ResponseType(typeof(SMS))]
        public async Task<IHttpActionResult> GetSMS(int id)
        {
            SMS sMS = await db.SMS.FindAsync(id);
            if (sMS == null)
            {
                return NotFound();
            }

            return Ok(sMS);
        }

        // PUT: api/SMSapi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSMS(int id, SMS sMS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sMS.Id)
            {
                return BadRequest();
            }

            db.Entry(sMS).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SMSExists(id))
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

        // POST: api/SMSapi
        [ResponseType(typeof(SMS))]
        public async Task<IHttpActionResult> PostSMS(SMS sMS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SMS.Add(sMS);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sMS.Id }, sMS);
        }

        // DELETE: api/SMSapi/5
        [ResponseType(typeof(SMS))]
        public async Task<IHttpActionResult> DeleteSMS(int id)
        {
            SMS sMS = await db.SMS.FindAsync(id);
            if (sMS == null)
            {
                return NotFound();
            }

            db.SMS.Remove(sMS);
            await db.SaveChangesAsync();

            return Ok(sMS);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SMSExists(int id)
        {
            return db.SMS.Count(e => e.Id == id) > 0;
        }
    }
}