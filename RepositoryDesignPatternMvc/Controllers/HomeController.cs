using RepositoryDesignPatternMvc.Models;
using RepositoryDesignPatternMvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RepositoryDesignPatternMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController()
        {
            _employeeRepository = new EmployeeRepository(new EmployeeContext());
        }

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public ActionResult Index()
        {
            var employee = _employeeRepository.GetEmployees();
            return View(employee);
        }

        public ActionResult Details(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            return View(employee);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.NewEmployee(employee);
                _employeeRepository.Save();
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
            var employee = _employeeRepository.GetEmployeeById(id);
            if (employee==null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.UpdateEmployee(employee);
                _employeeRepository.Save();
                return RedirectToAction("Index", "Home");

            }
            return View(employee);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
            var employee = _employeeRepository.GetEmployeeById(id);
            if (employee==null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            _employeeRepository.DeleteEmployee(id);
            _employeeRepository.Save();
            return RedirectToAction("Index", "Home");
        }
    }
}