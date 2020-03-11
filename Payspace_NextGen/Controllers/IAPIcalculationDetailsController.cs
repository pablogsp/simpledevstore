using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Payspace_NextGen.Models;

namespace Payspace_NextGen.Controllers
{
    public interface IAPIcalculationDetailsController
    {
        Task<IHttpActionResult> DeletecalculationDetail(int id);
        bool Equals(object obj);
        Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken);
        Task<IHttpActionResult> GetcalculationDetail(int id);
        IQueryable<calculationDetail> GetcalculationDetails();
        decimal GetcalculationResultRateNoProgressive(Employe employe);
        int GetHashCode();
        Task<IHttpActionResult> PostcalculationDetail(calculationDetail calculationDetail);
        Task<IHttpActionResult> PostProcesscalculation(Calculation calculation);
        Task<IHttpActionResult> PutcalculationDetail(int id, calculationDetail calculationDetail);
        string ToString();
    }
}