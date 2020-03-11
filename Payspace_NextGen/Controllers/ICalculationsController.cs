using System.Threading.Tasks;
using System.Web.Mvc;
using Payspace_NextGen.Models;

namespace Payspace_NextGen.Controllers
{
    public interface ICalculationsController
    {
        ActionResult Create();
        Task<ActionResult> Create([Bind(Include = "Id,Month,Year,NamePayment")] Calculation calculation);
        Task<ActionResult> Delete(int? id);
        Task<ActionResult> DeleteConfirmed(int id);
        Task<ActionResult> Details(int? id);
        Task<ActionResult> Edit([Bind(Include = "Id,Month,Year,NamePayment")] Calculation calculation);
        Task<ActionResult> Edit(int? id);
        bool Equals(CalculationsController other);
        bool Equals(object obj);
        int GetHashCode();
        Task<ActionResult> Index();
        string ToString();
    }
}