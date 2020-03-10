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
using Payspace_NextGen.Models;

namespace Payspace_NextGen.Controllers
{
    public class APIEmployesController : ApiController
    {
        private localdbEntities db = new localdbEntities();

        // GET: api/APIEmployes
        public IQueryable<Employe> GetEmployes()
        {
            return db.Employes;
        }

        // GET: api/APIEmployes/5
        [ResponseType(typeof(Employe))]
        public async Task<IHttpActionResult> GetEmploye(string id)
        {
            Employe employe = await db.Employes.FindAsync(id);
            if (employe == null)
            {
                return NotFound();
            }

            return Ok(employe);
        }

        // PUT: api/APIEmployes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmploye(string id, Employe employe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employe.CPF)
            {
                return BadRequest();
            }

            db.Entry(employe).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeExists(id))
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

        // POST: api/APIEmployes
        [ResponseType(typeof(Employe))]
        public async Task<IHttpActionResult> PostEmploye(Employe employe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employes.Add(employe);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeExists(employe.CPF))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = employe.CPF }, employe);
        }

        // DELETE: api/APIEmployes/5
        [ResponseType(typeof(Employe))]
        public async Task<IHttpActionResult> DeleteEmploye(string id)
        {
            Employe employe = await db.Employes.FindAsync(id);
            if (employe == null)
            {
                return NotFound();
            }

            db.Employes.Remove(employe);
            await db.SaveChangesAsync();

            return Ok(employe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeExists(string id)
        {
            return db.Employes.Count(e => e.CPF == id) > 0;
        }
    }
}