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
    public class DiagnosticsController : ApiController
    {
        private TankToadContext db = new TankToadContext();

        // GET: api/Diagnostics
        public IQueryable<Diagnostics> GetDiagnostics()
        {
            return db.Diagnostics;
        }

        // GET: api/Diagnostics/5
        [ResponseType(typeof(Diagnostics))]
        public async Task<IHttpActionResult> GetDiagnostics(int id)
        {
            Diagnostics diagnostics = await db.Diagnostics.FindAsync(id);
            if (diagnostics == null)
            {
                return NotFound();
            }

            return Ok(diagnostics);
        }

        // PUT: api/Diagnostics/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDiagnostics(int id, Diagnostics diagnostics)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != diagnostics.Id)
            {
                return BadRequest();
            }

            db.Entry(diagnostics).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiagnosticsExists(id))
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

        // POST: api/Diagnostics
        [ResponseType(typeof(Diagnostics))]
        public async Task<IHttpActionResult> PostDiagnostics(Diagnostics diagnostics)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Diagnostics.Add(diagnostics);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = diagnostics.Id }, diagnostics);
        }

        // DELETE: api/Diagnostics/5
        [ResponseType(typeof(Diagnostics))]
        public async Task<IHttpActionResult> DeleteDiagnostics(int id)
        {
            Diagnostics diagnostics = await db.Diagnostics.FindAsync(id);
            if (diagnostics == null)
            {
                return NotFound();
            }

            db.Diagnostics.Remove(diagnostics);
            await db.SaveChangesAsync();

            return Ok(diagnostics);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiagnosticsExists(int id)
        {
            return db.Diagnostics.Count(e => e.Id == id) > 0;
        }
    }
}