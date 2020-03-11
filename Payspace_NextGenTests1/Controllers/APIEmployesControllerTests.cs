using NUnit.Framework;
using Payspace_NextGen.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payspace_NextGen.Controllers.Tests
{
    [TestFixture()]
    public class APIEmployesControllerTests
    {
        public List<Models.Employe> lista;
        APIEmployesController aPIEmployes;
        [SetUp]
        public void _setup()
        {
            
        }

        [Test()]
        public void GetEmployesTest()
        {
            aPIEmployes = new APIEmployesController();
            foreach (var item in aPIEmployes.GetEmployes())
            {
                lista.Add(item);
            }
            Assert.Pass("Retornou o Empregado Cadastrado");
        }

        [Test()]
        public void GetEmployeTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void PutEmployeTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void PostEmployeTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DeleteEmployeTest()
        {
            Assert.Fail();
        }
    }
}