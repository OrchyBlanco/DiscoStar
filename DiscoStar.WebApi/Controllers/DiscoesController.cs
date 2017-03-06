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
using System.Linq.Expressions;

namespace DiscoStar.WebApi.Controllers
{
    [RoutePrefix("api/Discos")]
    public class DiscoesController : ApiController
    {
        private DiscosEntities db = new DiscosEntities();

        
        // GET: api/Discos
        [Route("")]
        public IQueryable<Disco> GetDiscoes()
        {            
            return db.Discoes;
        }

        // GET: api/Discos/Top
        [Route("Top")]
        [ResponseType(typeof(Disco))]
        public IList<Disco> GetTopDisco()
        {
            string consulta = "SELECT TOP 6 Disco.IdDisco ,Disco.Titulo,Disco.Agno,Disco.Imagen,Disco.Audio,Disco.IdInterprete,sum(Puntuacion.Puntuacion) as Puntos FROM Disco INNER JOIN Puntuacion on Disco.IdDisco = Puntuacion.iddisco group by Disco.IdDisco,Disco.Titulo,Disco.Agno,Disco.Imagen,Disco.Audio,Disco.IdInterprete order by sum(Puntuacion.Puntuacion) desc ";     
            var top = db.Discoes.SqlQuery(consulta).ToList();
            return top;
        }

        // GET: api/Discos/5
        [Route("{id:int}")]
        [ResponseType(typeof(Disco))]
        public async Task<IHttpActionResult> GetDisco(int id)
        {
            Disco disco = await db.Discoes.FindAsync(id);
            if (disco == null)
            {
                return NotFound();
            }

            return Ok(disco);
        }

        // PUT: api/Discoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDisco(int id, Disco disco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != disco.IdDisco)
            {
                return BadRequest();
            }

            db.Entry(disco).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscoExists(id))
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

        // POST: api/Discoes
        [ResponseType(typeof(Disco))]
        public async Task<IHttpActionResult> PostDisco(Disco disco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Discoes.Add(disco);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DiscoExists(disco.IdDisco))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = disco.IdDisco }, disco);
        }

        // DELETE: api/Discoes/5
        [ResponseType(typeof(Disco))]
        public async Task<IHttpActionResult> DeleteDisco(int id)
        {
            Disco disco = await db.Discoes.FindAsync(id);
            if (disco == null)
            {
                return NotFound();
            }

            db.Discoes.Remove(disco);
            await db.SaveChangesAsync();

            return Ok(disco);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiscoExists(int id)
        {
            return db.Discoes.Count(e => e.IdDisco == id) > 0;
        }
    }
}