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
    public class InterpretesController : ApiController
    {
        private DiscosEntities db = new DiscosEntities();

        // GET: api/Interpretes
        public IQueryable<Interprete> GetInterpretes()
        {
            return db.Interpretes;
        }

        // GET: api/Interpretes/5
        [ResponseType(typeof(Interprete))]
        public async Task<IHttpActionResult> GetInterprete(int id)
        {
            Interprete interprete = await db.Interpretes.FindAsync(id);
            if (interprete == null)
            {
                return NotFound();
            }

            return Ok(interprete);
        }

        // PUT: api/Interpretes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInterprete(int id, Interprete interprete)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != interprete.IdInterprete)
            {
                return BadRequest();
            }

            db.Entry(interprete).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterpreteExists(id))
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

        // POST: api/Interpretes
        [ResponseType(typeof(Interprete))]
        public async Task<IHttpActionResult> PostInterprete(Interprete interprete)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Interpretes.Add(interprete);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InterpreteExists(interprete.IdInterprete))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = interprete.IdInterprete }, interprete);
        }

        // DELETE: api/Interpretes/5
        [ResponseType(typeof(Interprete))]
        public async Task<IHttpActionResult> DeleteInterprete(int id)
        {
            Interprete interprete = await db.Interpretes.FindAsync(id);
            if (interprete == null)
            {
                return NotFound();
            }

            db.Interpretes.Remove(interprete);
            await db.SaveChangesAsync();

            return Ok(interprete);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InterpreteExists(int id)
        {
            return db.Interpretes.Count(e => e.IdInterprete == id) > 0;
        }
    }
}