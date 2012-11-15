using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MVC_jQuery_AutoComplete.Models;

namespace MVC_jQuery_AutoComplete.Controllers
{
    public class EmployeesController : ApiController
    {
        private CompanyEntities db = new CompanyEntities();

        // GET api/Employees
        public IEnumerable<EmployeeInfo> GetEmployeeInfoes()
        {
            return db.EmployeeInfoes.AsEnumerable();
        }

        // GET api/Employees/5
        public EmployeeInfo GetEmployeeInfo(int id)
        {
            EmployeeInfo employeeinfo = db.EmployeeInfoes.Single(e => e.EmpNo == id);
            if (employeeinfo == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return employeeinfo;
        }

        // PUT api/Employees/5
        public HttpResponseMessage PutEmployeeInfo(int id, EmployeeInfo employeeinfo)
        {
            if (ModelState.IsValid && id == employeeinfo.EmpNo)
            {
                db.EmployeeInfoes.Attach(employeeinfo);
                db.ObjectStateManager.ChangeObjectState(employeeinfo, EntityState.Modified);

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Employees
        public HttpResponseMessage PostEmployeeInfo(EmployeeInfo employeeinfo)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeInfoes.AddObject(employeeinfo);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, employeeinfo);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = employeeinfo.EmpNo }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Employees/5
        public HttpResponseMessage DeleteEmployeeInfo(int id)
        {
            EmployeeInfo employeeinfo = db.EmployeeInfoes.Single(e => e.EmpNo == id);
            if (employeeinfo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.EmployeeInfoes.DeleteObject(employeeinfo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, employeeinfo);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}