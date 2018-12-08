using System;
using System.Collections.Generic;
using System.Data;
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
    public class DataController : ApiController
    {
        private TankToadContext db = new TankToadContext();

        // GET: api/Data
        public IQueryable<Data> GetDatas()
        {
            return db.Datas
                //.Include(ttt =>ttt.BatteryLevel);
                .Include(d => d.DeviceAttributes);
        }

        // GET: api/Data/5
        [ResponseType(typeof(Data))]
        public async Task<IHttpActionResult> GetData(int id)
        {
            Data data = await db.Datas.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // PUT: api/Data/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutData(int id, Data data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != data.Id)
            {
                return BadRequest();
            }

            db.Entry(data).State = System.Data.Entity.EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataExists(id))
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

        // POST: api/Data
        [ResponseType(typeof(Data))]
        public async Task<IHttpActionResult> PostData(Data data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Datas.Add(data);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = data.Id }, data);
        }

        // DELETE: api/Data/5
        [ResponseType(typeof(Data))]
        public async Task<IHttpActionResult> DeleteData(int id)
        {
            Data data = await db.Datas.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            db.Datas.Remove(data);
            await db.SaveChangesAsync();

            return Ok(data);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DataExists(int id)
        {
            return db.Datas.Count(e => e.Id == id) > 0;
        }
    }
}