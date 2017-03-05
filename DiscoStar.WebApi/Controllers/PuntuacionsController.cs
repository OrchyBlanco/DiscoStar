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
using DiscoStar.WebApi.Models;

namespace DiscoStar.WebApi.Controllers
{
    public class PuntuacionsController : ApiController
    {
        private DiscosEntities db = new DiscosEntities();

        // GET: api/Puntuacions
        public IQueryable<Puntuacion> GetPuntuacions()
        {
            return db.Puntuacions;
        }

        // GET: api/Puntuacions/5
        [ResponseType(typeof(Puntuacion))]
        public async Task<IHttpActionResult> GetPuntuacion(int id)
        {
            Puntuacion puntuacion = await db.Puntuacions.FindAsync(id);
            if (puntuacion == null)
            {
                return NotFound();
            }

            return Ok(puntuacion);
        }

        // PUT: api/Puntuacions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPuntuacion(int id, Puntuacion puntuacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != puntuacion.Id)
            {
                return BadRequest();
            }

            db.Entry(puntuacion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuntuacionExists(id))
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

        // POST: api/Puntuacions
        [ResponseType(typeof(Puntuacion))]
        public async Task<IHttpActionResult> PostPuntuacion(Puntuacion puntuacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Puntuacions.Add(puntuacion);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PuntuacionExists(puntuacion.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = puntuacion.Id }, puntuacion);
        }

        // DELETE: api/Puntuacions/5
        [ResponseType(typeof(Puntuacion))]
        public async Task<IHttpActionResult> DeletePuntuacion(int id)
        {
            Puntuacion puntuacion = await db.Puntuacions.FindAsync(id);
            if (puntuacion == null)
            {
                return NotFound();
            }

            db.Puntuacions.Remove(puntuacion);
            await db.SaveChangesAsync();

            return Ok(puntuacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PuntuacionExists(int id)
        {
            return db.Puntuacions.Count(e => e.Id == id) > 0;
        }
    }
}