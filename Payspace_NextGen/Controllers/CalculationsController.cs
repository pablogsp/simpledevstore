﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Payspace_NextGen.Models;
using System.IO;
using System.Text;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace Payspace_NextGen.Controllers
{
    public class CalculationsController : Controller, IEquatable<CalculationsController>, ICalculationsController
    {
        private localdbEntities db = new localdbEntities();

        protected override bool DisableAsyncSupport => base.DisableAsyncSupport;

        public CalculationsController(localdbEntities db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public CalculationsController()
        {
        }

        // GET: Calculations
        public async Task<ActionResult> Index()
        {
            return View(await db.Calculations.ToListAsync());
        }

        // GET: Calculations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calculation calculation = await db.Calculations.FindAsync(id);
            if (calculation == null)
            {
                return HttpNotFound();
            }
            return View(calculation);
        }

        // GET: Calculations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calculations/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Month,Year,NamePayment")] Calculation calculation)
        {
            if (ModelState.IsValid)
            {
                APIcalculationDetailsController detailsController = new APIcalculationDetailsController();
                await detailsController.PostProcesscalculation(calculation);
                return RedirectToAction("Details\\"+calculation.Id.ToString());
            }

            return View(calculation);
        }

        // GET: Calculations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calculation calculation = await db.Calculations.FindAsync(id);
            if (calculation == null)
            {
                return HttpNotFound();
            }
            return View(calculation);
        }

        // POST: Calculations/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Month,Year,NamePayment")] Calculation calculation)
        {
            if (ModelState.IsValid)
            {
                APIcalculationDetailsController aPIcalculationDetailsController = new APIcalculationDetailsController();
                foreach (var item in aPIcalculationDetailsController.GetcalculationDetails())
                {
                    item.DetailValue = item.Employe.Salary;
                    await aPIcalculationDetailsController.PutcalculationDetail(item.Id, item);

                }
                return RedirectToAction("Detail");
            }
            return View(calculation);
        }

        // GET: Calculations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calculation calculation = await db.Calculations.FindAsync(id);
            if (calculation == null)
            {
                return HttpNotFound();
            }
            return View(calculation);
        }

        // POST: Calculations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Calculation calculation = await db.Calculations.FindAsync(id);
            db.Calculations.Remove(calculation);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CalculationsController);
        }

        public bool Equals(CalculationsController other)
        {
            return other != null &&
                   EqualityComparer<localdbEntities>.Default.Equals(db, other.db);
        }

        public override int GetHashCode()
        {
            return 794399803 + EqualityComparer<localdbEntities>.Default.GetHashCode(db);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void Execute(RequestContext requestContext)
        {
            base.Execute(requestContext);
        }

        protected override ContentResult Content(string content, string contentType, Encoding contentEncoding)
        {
            return base.Content(content, contentType, contentEncoding);
        }

        protected override IActionInvoker CreateActionInvoker()
        {
            return base.CreateActionInvoker();
        }

        protected override ITempDataProvider CreateTempDataProvider()
        {
            return base.CreateTempDataProvider();
        }

        protected override void ExecuteCore()
        {
            base.ExecuteCore();
        }

        protected override FileContentResult File(byte[] fileContents, string contentType, string fileDownloadName)
        {
            return base.File(fileContents, contentType, fileDownloadName);
        }

        protected override FileStreamResult File(Stream fileStream, string contentType, string fileDownloadName)
        {
            return base.File(fileStream, contentType, fileDownloadName);
        }

        protected override FilePathResult File(string fileName, string contentType, string fileDownloadName)
        {
            return base.File(fileName, contentType, fileDownloadName);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            base.HandleUnknownAction(actionName);
        }

        protected override HttpNotFoundResult HttpNotFound(string statusDescription)
        {
            return base.HttpNotFound(statusDescription);
        }

        protected override JavaScriptResult JavaScript(string script)
        {
            return base.JavaScript(script);
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return base.Json(data, contentType, contentEncoding);
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return base.Json(data, contentType, contentEncoding, behavior);
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            base.OnAuthentication(filterContext);
        }

        protected override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            base.OnAuthenticationChallenge(filterContext);
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        protected override PartialViewResult PartialView(string viewName, object model)
        {
            return base.PartialView(viewName, model);
        }

        protected override RedirectResult Redirect(string url)
        {
            return base.Redirect(url);
        }

        protected override RedirectResult RedirectPermanent(string url)
        {
            return base.RedirectPermanent(url);
        }

        protected override RedirectToRouteResult RedirectToAction(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            return base.RedirectToAction(actionName, controllerName, routeValues);
        }

        protected override RedirectToRouteResult RedirectToActionPermanent(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            return base.RedirectToActionPermanent(actionName, controllerName, routeValues);
        }

        protected override RedirectToRouteResult RedirectToRoute(string routeName, RouteValueDictionary routeValues)
        {
            return base.RedirectToRoute(routeName, routeValues);
        }

        protected override RedirectToRouteResult RedirectToRoutePermanent(string routeName, RouteValueDictionary routeValues)
        {
            return base.RedirectToRoutePermanent(routeName, routeValues);
        }

        protected override ViewResult View(string viewName, string masterName, object model)
        {
            return base.View(viewName, masterName, model);
        }

        protected override ViewResult View(IView view, object model)
        {
            return base.View(view, model);
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            return base.BeginExecute(requestContext, callback, state);
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            return base.BeginExecuteCore(callback, state);
        }

        protected override void EndExecute(IAsyncResult asyncResult)
        {
            base.EndExecute(asyncResult);
        }

        protected override void EndExecuteCore(IAsyncResult asyncResult)
        {
            base.EndExecuteCore(asyncResult);
        }

        public static bool operator ==(CalculationsController controller1, CalculationsController controller2)
        {
            return EqualityComparer<CalculationsController>.Default.Equals(controller1, controller2);
        }

        public static bool operator !=(CalculationsController controller1, CalculationsController controller2)
        {
            return !(controller1 == controller2);
        }
    }
}
