using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesMotorcycle.Data;
using SalesMotorcycle.Models;

namespace SalesMotorcycle.Controllers
{
    public class MotorsController : Controller
    {
        private readonly DatabaseContext _Motors;
        private readonly IWebHostEnvironment _hostEnvironment;
        public MotorsController(DatabaseContext databaseContext, IWebHostEnvironment evn)
        {
            _Motors = databaseContext;
            _hostEnvironment = evn;
        }

        // GET: Motors
        public ActionResult Index()
        {
            var result = _Motors.Data_Motors.Where(it => true).ToList();

            return View(result);
        }

        // GET: Order_List
        public IActionResult Order_List()
        {
            var data_list = _Motors.Order.Where(it => true).ToList();
            return View(data_list);
        }

        public IActionResult Edit_Order(int id)
        {
            var ById = _Motors.Order.Find(id);
            return View(ById);
        }

        // Edit: Order_list
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit_Order(Order order)
        {
            _Motors.Order.Update(order);
            _Motors.SaveChanges();
            return RedirectToAction(nameof(Order_List));
        }

        // GET : Delete_Order
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete_Order(int id)
        {
            var Up_Order = _Motors.Order.Find(id);
            _Motors.Order.Remove(Up_Order);
            _Motors.SaveChanges();
            return RedirectToAction(nameof(Order_List));
        }

        // GET: Motors/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Motors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Motors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Data_Motor data, IFormFile File)
        {
            try
            {
                // TODO: Add insert logic here
                if (File != null)
                {
                    //upload files to wwwroot
                    var fileName = Path.GetFileName(File.FileName);
                    var filePath = Path.Combine(_hostEnvironment.WebRootPath, "Images", fileName);

                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        File.CopyToAsync(fileSteam);
                    }
                    //your logic to save filePath to database, for example
                    data.Data_Img = fileName;
                    data.Data_Time = DateTime.Now;
                    data.Status_item = "1";

                    _Motors.Data_Motors.Add(data);
                    _Motors.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Motors/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Motors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Motors/Delete/5
        public ActionResult Delete(int id)
        {
            var item = _Motors.Data_Motors.Find(id);

            return View(item);
        }

        // POST: Motors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Data_Motor data)
        {
            try
            {
                // TODO: Add delete logic here
                _Motors.Data_Motors.Remove(data);
                _Motors.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}