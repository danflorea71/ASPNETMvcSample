using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC1.DataAccessLayer;

namespace MVC1.Controllers
{
    public class StudentController : Controller
    {

        
        // GET: Student
        public ActionResult Index()
        {
            //return View(MvcApplication.studentsList);
            return View(DBContext.SelectAllStudents());
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Models.Student student = DBContext.SelectStudentById(id);

            if(student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(Models.Student student)
        {
            try
            {
                // TODO: Add insert logic here
                //student.StudentId = ++ MvcApplication.globalStudentId;
                //MvcApplication.studentsList.Add(student);
                if(ModelState.IsValid)
                {
                    if(student == null) { return HttpNotFound(); }

                    DBContext.CreateStudent(student);

                    return RedirectToAction("Index");
                }

            }
            catch(DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again");

            }

            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                Models.Student student = DBContext.SelectStudentById(id);
                if (student == null) { return HttpNotFound(); }

                return View(student);
            }
            catch
            {
                return View();
            }
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, Models.Student student)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (student == null) { return HttpNotFound(); }

            try
            {
                // TODO: Add update logic here
                if(ModelState.IsValid)
                {
                    DBContext.UpdateStudentById(id, student);
                    return RedirectToAction("Index");
                }

                
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again");

            }

            return View(student);


        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                Models.Student student = DBContext.SelectStudentById(id);
                if (student == null) { return HttpNotFound(); }

                return View(student);
            }
            catch
            {
                return View();
            }
            
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Models.Student student)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (student == null) { return HttpNotFound(); }

            try
            {
                // TODO: Add delete logic here
                DBContext.DeleteStudentById(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
