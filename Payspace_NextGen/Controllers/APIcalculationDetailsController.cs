using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using Newtonsoft.Json;
using Payspace_NextGen.Models;

namespace Payspace_NextGen.Controllers
{
    public class APIcalculationDetailsController : ApiController, IAPIcalculationDetailsController
    {
        private localdbEntities db = new localdbEntities();

        public APIcalculationDetailsController(localdbEntities db)
        {
            this.db = db;
        }

        public APIcalculationDetailsController()
        {
        }

        // GET: api/APIcalculationDetails
        public IQueryable<calculationDetail> GetcalculationDetails()
        {
            return db.calculationDetails;
        }

        // GET: api/APIcalculationDetails/5
        [ResponseType(typeof(calculationDetail))]
        public async Task<IHttpActionResult> GetcalculationDetail(int id)
        {
            calculationDetail calculationDetail = await db.calculationDetails.FindAsync(id);
            if (calculationDetail == null)
            {
                return NotFound();
            }

            return Ok(calculationDetail);
        }

        // PUT: api/APIcalculationDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutcalculationDetail(int id, calculationDetail calculationDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != calculationDetail.Id)
            {
                return BadRequest();
            }

            db.Entry(calculationDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!calculationDetailExists(id))
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

        [ResponseType(typeof(calculationDetail))]
        public async Task<IHttpActionResult> PostProcesscalculation(Calculation calculation)
        {
            decimal impostfinal = 0;
            calculation.DateGenerate = DateTime.Now;
            APIEmployesController employesController = new APIEmployesController();
            foreach (var item in employesController.GetEmployes())
            {
                decimal salary = item.Salary;
                decimal impost = 0;
                // Include value salary on the calculation detail
                await PostcalculationDetail(new calculationDetail()
                {
                    Calculation = calculation,
                    EmployeID = item.CPF,
                    DetailName = "Salary at month (" + calculation.Month.ToString() + "/" + calculation.Year.ToString() + ")",
                    DetailValue = salary,
                });
                if (true)
                {

                }
                if(item.PostCode.TaxRate == "Progressive")
                {
                    foreach (RateValue item2 in (from p in item.PostCode.Type.RateValues
                                                where p.From <= item.Salary
                                                select p))
                    {
                        impost = item2.Rate;
                        decimal valueprogressive = 0;
                        if (item.Salary >= item2.To)
                        {
                            valueprogressive = item2.To;
                        }
                        else
                        {
                            valueprogressive = salary - item2.From;
                        }
                        await PostcalculationDetail(new calculationDetail()
                        {
                            Calculation = calculation,
                            EmployeID = item.CPF,
                            DetailName = "Impost " + impost.ToString() + "% Tax - Base From:"+item2.From.ToString()+" To:"+valueprogressive.ToString()+" (" + calculation.Month.ToString() + "/" + calculation.Year.ToString() + ")",
                            DetailValue = valueprogressive * (impost / 100)
                        });
                    }
                }
                else
                {
                    impost += GetcalculationResultRateNoProgressive(item);
                    // Include value TaxRate on the calculation detail, consult the table and calculation of tax.
                    await PostcalculationDetail(new calculationDetail()
                    {
                        Calculation = calculation,
                        EmployeID = item.CPF,
                        DetailName = "Impost at month " + impost.ToString() + "% (" + calculation.Month.ToString() + "/" + calculation.Year.ToString() + ")",
                        DetailValue = salary * (impost / 100)
                    });
                }
                
                
            }
            return CreatedAtRoute("DefaultApi", new { }, new object());
        }

        // POST: api/apicalcrates
        [ResponseType(typeof(calculationDetail))]
        public decimal GetcalculationResultRateNoProgressive(Employe employe)
        {
            decimal valuenow = 0;
            foreach (RateValue item in (from p in employe.PostCode.Type.RateValues
                                        where p.From <= employe.Salary && p.To >= employe.Salary
                                        select p))
            {
                valuenow = item.Rate;
            }
            return valuenow;
        }


        // POST: api/APIcalculationDetails
        [ResponseType(typeof(calculationDetail))]
        public async Task<IHttpActionResult> PostcalculationDetail(calculationDetail calculationDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.calculationDetails.Add(calculationDetail);
            await db.SaveChangesAsync();



            return CreatedAtRoute("DefaultApi", new { id = calculationDetail.Id }, calculationDetail);
        }

        // DELETE: api/APIcalculationDetails/5
        [ResponseType(typeof(calculationDetail))]
        public async Task<IHttpActionResult> DeletecalculationDetail(int id)
        {
            calculationDetail calculationDetail = await db.calculationDetails.FindAsync(id);
            if (calculationDetail == null)
            {
                return NotFound();
            }

            db.calculationDetails.Remove(calculationDetail);
            await db.SaveChangesAsync();

            return Ok(calculationDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool calculationDetailExists(int id)
        {
            return db.calculationDetails.Count(e => e.Id == id) > 0;
        }

        public override bool Equals(object obj)
        {
            var controller = obj as APIcalculationDetailsController;
            return controller != null &&
                   EqualityComparer<localdbEntities>.Default.Equals(db, controller.db);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            return base.ExecuteAsync(controllerContext, cancellationToken);
        }

        protected override BadRequestResult BadRequest()
        {
            return base.BadRequest();
        }

        protected override BadRequestErrorMessageResult BadRequest(string message)
        {
            return base.BadRequest(message);
        }

        protected override InvalidModelStateResult BadRequest(ModelStateDictionary modelState)
        {
            return base.BadRequest(modelState);
        }

        protected override ConflictResult Conflict()
        {
            return base.Conflict();
        }

        protected override NegotiatedContentResult<T> Content<T>(HttpStatusCode statusCode, T value)
        {
            return base.Content(statusCode, value);
        }

        protected override FormattedContentResult<T> Content<T>(HttpStatusCode statusCode, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType)
        {
            return base.Content(statusCode, value, formatter, mediaType);
        }

        protected override CreatedNegotiatedContentResult<T> Created<T>(Uri location, T content)
        {
            return base.Created(location, content);
        }

        protected override CreatedAtRouteNegotiatedContentResult<T> CreatedAtRoute<T>(string routeName, IDictionary<string, object> routeValues, T content)
        {
            return base.CreatedAtRoute(routeName, routeValues, content);
        }

        protected override InternalServerErrorResult InternalServerError()
        {
            return base.InternalServerError();
        }

        protected override ExceptionResult InternalServerError(Exception exception)
        {
            return base.InternalServerError(exception);
        }

        protected override JsonResult<T> Json<T>(T content, JsonSerializerSettings serializerSettings, Encoding encoding)
        {
            return base.Json(content, serializerSettings, encoding);
        }

        protected override NotFoundResult NotFound()
        {
            return base.NotFound();
        }

        protected override OkResult Ok()
        {
            return base.Ok();
        }

        protected override OkNegotiatedContentResult<T> Ok<T>(T content)
        {
            return base.Ok(content);
        }

        protected override RedirectResult Redirect(string location)
        {
            return base.Redirect(location);
        }

        protected override RedirectResult Redirect(Uri location)
        {
            return base.Redirect(location);
        }

        protected override RedirectToRouteResult RedirectToRoute(string routeName, IDictionary<string, object> routeValues)
        {
            return base.RedirectToRoute(routeName, routeValues);
        }

        protected override ResponseMessageResult ResponseMessage(HttpResponseMessage response)
        {
            return base.ResponseMessage(response);
        }

        protected override StatusCodeResult StatusCode(HttpStatusCode status)
        {
            return base.StatusCode(status);
        }

        protected override UnauthorizedResult Unauthorized(IEnumerable<AuthenticationHeaderValue> challenges)
        {
            return base.Unauthorized(challenges);
        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
        }
    }
}