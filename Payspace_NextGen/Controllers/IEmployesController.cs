using System.Web.Mvc;
using Payspace_NextGen.Models;

namespace Payspace_NextGen.Controllers
{
    public interface IEmployesController
    {
        ActionResult Create();
        ActionResult Create([Bind(Include = "Name,SocialName,CPF,PostCodeId,Salary")] Employe employe);
        ActionResult Delete(string id);
        ActionResult DeleteConfirmed(string id);
        ActionResult Details(string id);
        ActionResult Edit([Bind(Include = "Name,SocialName,CPF,PostCodeId,Salary")] Employe employe);
        ActionResult Edit(string id);
        bool Equals(EmployesController other);
        bool Equals(object obj);
        int GetHashCode();
        ActionResult Index();
        string ToString();
    }
}