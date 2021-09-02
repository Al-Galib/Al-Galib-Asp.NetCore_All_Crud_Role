using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManageMent.Models;
using EmployeeManageMent.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManageMent.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IEmployeeRefository _employeeRefository;
        private readonly IHostingEnvironment env;

        public HomeController(IEmployeeRefository employeeRefository,
                        IHostingEnvironment env)
        {
            this._employeeRefository = employeeRefository;
            this.env = env;
        }
       [AllowAnonymous]
        public IActionResult Index()
        {
            var e = _employeeRefository.GetEmployees();
            return View(e);
        }
        public IActionResult Details(int? id)
        {
            //throw new Exception ("Error in Details view");
            Employee employee = _employeeRefository.GetEmployee(id.Value );
            if(employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value );
            }

            AllEmployee allEmployee = new AllEmployee()
            {
                Employee = employee,
            };

            return View(allEmployee);
           
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        //duto Create Methode e shothik Prothom ta sir er parer ta online tutorial
        public IActionResult Create(EmployeeVM vM)
        {
            //if (ModelState.IsValid)
            //{
            //    Employee pNew = new Employee { Name = vM.Name, Email=vM.Email, Picture = "no-picture.png" };
            //    if (vM.Picture != null && vM.Picture.Length > 0)
            //    {
            //        var imagePath = env.WebRootPath + @"\uploads";
            //        if (!Directory.Exists(imagePath))
            //        {
            //            Directory.CreateDirectory(imagePath);
            //        }
            //        string fileName = Path.GetFileNameWithoutExtension(vM.Picture.FileName) + Guid.NewGuid().ToString().Substring(0, 4) + Path.GetExtension(vM.Picture.FileName);
            //        FileStream fs = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
            //        vM.Picture.CopyTo(fs);
            //        fs.Flush();
            //        fs.Close();
            //        pNew.Picture = fileName;
            //    }
            //    _employeeRefository.Add(pNew);

            //    return RedirectToAction("Details");


            //}
            //return View(vM);



            // Single file uploade by online
            if (ModelState.IsValid)
            {
                Employee pNew = new Employee { Name = vM.Name, Email = vM.Email, Picture = "no-picture.png" };
                string uniqFile = ProcessUpload(vM);
                Employee employee = new Employee
                {
                    Name = vM.Name,
                    Email = vM.Email,
                    Department = vM.Department,
                    Picture = uniqFile
                };
                _employeeRefository.Add(employee);
                return RedirectToAction("Details", new { id = employee.Id });
            }

            return View(vM);



            //multiple file uploade
            //    if (ModelState.IsValid)
            //    {

            //        string uniqFile = null;
            //        if (vM.Pictures != null && vM.Pictures.Count > 0)
            //        {
            //            foreach (IFormFile picture in vM.Pictures)
            //            {
            //                string upF = Path.Combine(env.WebRootPath + @"\uploads");
            //                uniqFile = Guid.NewGuid().ToString() + "_" + picture.FileName;
            //                string filePath = Path.Combine(upF, uniqFile);
            //                picture.CopyTo(new FileStream(filePath, FileMode.Create));
            //            }


            //        }
            //        Employee employee = new Employee
            //        {
            //            Name = vM.Name,
            //            Email = vM.Email,
            //            Department = vM.Department,
            //            Picture = uniqFile
            //        };
            //        _employeeRefository.Add(employee);
            //        return RedirectToAction("Details", new { id = employee.Id });
            //    }

            //    return View(vM);
        }
        public IActionResult Edit(int id)
        {
            Employee employee = _employeeRefository.GetEmployee(id);
            EmployeeEditVM employeeEditVM = new EmployeeEditVM
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
               FilePath = employee.Picture
            };

            return View(employeeEditVM);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeEditVM model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRefository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if(model.Picture != null)
                {
                    if(model.FilePath != null)
                    {
                        string file = Path.Combine(env.WebRootPath, "uploads",
                            model.FilePath);
                        System.IO.File.Delete(file);
                    }
                    employee.Picture = ProcessUpload(model);
                }

                _employeeRefository.Update(employee);
                return RedirectToAction("Index");
            }

            return View(model);

           
        }

        public IActionResult Delete(int id)
        {
            try
            {
                Employee employee = _employeeRefository.Delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
        private string ProcessUpload(EmployeeVM model)
        {
            string uniqFile = null;
            if (model.Picture != null)
            {

                string upF = Path.Combine(env.WebRootPath + @"\uploads");
                uniqFile = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                string filePath = Path.Combine(upF, uniqFile);
                using (var fileStram = new FileStream(filePath, FileMode.Create))
                {
                    model.Picture.CopyTo(fileStram);
                }
                

            }

            return uniqFile;
        }
       
        

    }
}
