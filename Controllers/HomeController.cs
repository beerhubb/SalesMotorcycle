using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalesMotorcycle.Data;
using SalesMotorcycle.Models;

namespace SalesMotorcycle.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _Motor;
        private readonly IWebHostEnvironment _environment;

        public HomeController(DatabaseContext context, IWebHostEnvironment evn)
        {
            _Motor = context;
            _environment = evn;
        }

        public IActionResult Index()
        {
            ViewBag.id = HttpContext.Session.GetInt32("id");
            ViewBag.usename = HttpContext.Session.GetString("Name");
            var item = _Motor.Data_Motors.Where(it => true).ToList();
            return View(item);
        }

        public IActionResult Details(int id)
        {
            ViewBag.id = HttpContext.Session.GetInt32("id");
            ViewBag.usename = HttpContext.Session.GetString("Name");
            var item = _Motor.Data_Motors.Find(id);

            return View(item);
        }

        //Register
        public IActionResult Create()
        {
            ViewBag.id = HttpContext.Session.GetInt32("id");
            ViewBag.usename = HttpContext.Session.GetString("Name");
            if (ViewBag.usename == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(UseRegister data, IFormFile File)
        {
            try
            {
                // TODO: Add insert logic here
                if (File != null)
                {
                    //upload files to wwwroot
                    var fileName = Path.GetFileName(File.FileName);
                    var filePath = Path.Combine(_environment.WebRootPath, "Images", fileName);

                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        File.CopyToAsync(fileSteam);
                    }
                    //your logic to save filePath to database, for example
                    data.Pictrue = fileName;

                    _Motor.User_Register.Add(data);
                    _Motor.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //สั่งซื้อสินค้า
        public IActionResult Buy(int id)
        {
            ViewBag.id = HttpContext.Session.GetInt32("id");
            ViewBag.usename = HttpContext.Session.GetString("Name");
            ViewBag.address = HttpContext.Session.GetString("address");
            ViewBag.phon = HttpContext.Session.GetString("phon");

            if (ViewBag.id == null)
            {
                return RedirectToAction(nameof(Login));
            }
            var select_item = _Motor.Data_Motors.Find(id);

            var order = new Order
            {
                Fname_Lname = ViewBag.usename,
                Address = ViewBag.address,
                Phon = ViewBag.phon,
                Data_Name = select_item.Data_Name,
                Data_Price = select_item.Data_Price,
                Number_item = 1

            };

            return View(order);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Buy(Order data, int id)
        {
            ViewBag.id = HttpContext.Session.GetInt32("id");
            var select_item = _Motor.Data_Motors.Find(id);
            data.Id = 0;
            data.ID_User = Convert.ToString(ViewBag.id);
            data.ID_object = select_item.Id.ToString();
            data.Sum_Price = data.Data_Price * data.Number_item;
            data.STT_item = "1";
            _Motor.Order.Add(data);
            _Motor.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Profile()
        {
            ViewBag.id = HttpContext.Session.GetInt32("id");
            ViewBag.usename = HttpContext.Session.GetString("Name");
            ViewBag.address = HttpContext.Session.GetString("address");
            ViewBag.phon = HttpContext.Session.GetString("phon");

            if (ViewBag.id == null)
            {
                return RedirectToAction(nameof(Login));
            }
            string id = Convert.ToString(ViewBag.id);
            var user = _Motor.Order.Where(it => it.ID_User == id).ToList();
            return View(user);
        }

        //จ่ายเงิน
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult Pay(int id)
        {
            var order = _Motor.Order.Find(id);
            order.STT_item = "2";
            _Motor.Order.Update(order);
            _Motor.SaveChanges();
            return RedirectToAction(nameof(Profile));
        }

        //ยกเลิกการซื้อ
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete_Buy(int id)
        {
            var order = _Motor.Order.Find(id);
            _Motor.Order.Remove(order);
            _Motor.SaveChanges();
            return RedirectToAction(nameof(Profile));
        }

        public IActionResult Login()
        {
            ViewBag.id = HttpContext.Session.GetInt32("id");
            if (ViewBag.id != null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Login(UseRegister data)
        {
            var use = _Motor.User_Register.Where(it => it.UserName == data.UserName && it.PassWord == data.PassWord).FirstOrDefault();
            if (use != null)
            {
                HttpContext.Session.SetInt32("id", use.Id);
                HttpContext.Session.SetString("Name", use.Fname_Lname);
                HttpContext.Session.SetString("age", use.Bdate.ToString());
                HttpContext.Session.SetString("address", use.Address);
                HttpContext.Session.SetString("phon", use.Phon);
                HttpContext.Session.SetString("pictrue", use.Pictrue);
                HttpContext.Session.SetString("email", use.Email);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(data);
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("id");
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("age");
            HttpContext.Session.Remove("address");
            HttpContext.Session.Remove("phon");
            HttpContext.Session.Remove("pictrue");
            HttpContext.Session.Remove("email");

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
