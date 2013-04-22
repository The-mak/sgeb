using SGEB.Model;
using SGEB.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGEB.Controllers
{
    [Authorize]
    public class DriverController : Controller
    {
        private DriverRepository repository;

        public DriverController()
        {
            this.repository = new DriverRepository();
        }

        [HttpGet]
        public ViewResult Drivers()
        {
            IEnumerable<DriverViewModel> drivers = (from d in this.repository.Drivers
                                                    select new DriverViewModel() { Id = d.Id, Name = d.Rg.Name, Celphone = d.Contact.CelPhone }).AsEnumerable<DriverViewModel>();
            return View("Drivers", drivers.OrderBy(x => x.Name));
        }

        [HttpGet]
        public ViewResult Driver(int Id)
        {
            Driver driver = this.repository.GetDriver(Id);
            driver.Contact.HomePhone = this.PhoneFormat(driver.Contact.HomePhone);
            driver.Contact.CelPhone = this.PhoneFormat(driver.Contact.CelPhone);
            driver.Contact.RefPhone1 = this.PhoneFormat(driver.Contact.RefPhone1);
            driver.Contact.RefPhone2 = this.PhoneFormat(driver.Contact.RefPhone2);

            return View(driver);
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View("AddEdit", new Driver() { Rg = new Rg(), Cpf = new Cpf(), Cnh = new Cnh(),
                                                  Address = new Address(), Contact = new Contact()});
        }

        [HttpGet]
        public ViewResult Edit(int Id)
        {
            return View("AddEdit", this.repository.Drivers.Where<Driver>(d => d.Id == Id).SingleOrDefault<Driver>());
        }

        [HttpPost]
        public ActionResult Save(Driver driver, HttpPostedFileBase[] Images)
        {
            bool jpegImages = true;

            foreach (HttpPostedFileBase image in Images)
            {
                if (image != null && !image.ContentType.Equals("image/jpeg"))
                {
                    jpegImages = false;
                    break;
                }
            }

            if (jpegImages)
            {
                if (ModelState.IsValid)
                {
                    if (Images[0] != null)
                    {
                        driver.Rg.Image = ConfigurationManager.AppSettings["ImagesDirectory"] + driver.Rg.Number + Path.GetExtension(Images[0].FileName);
                        Images[0].SaveAs(Server.MapPath(driver.Rg.Image));
                    }

                    if (Images[1] != null)
                    {
                        driver.Cpf.Image = ConfigurationManager.AppSettings["ImagesDirectory"] + driver.Cpf.Number + Path.GetExtension(Images[1].FileName);
                        Images[1].SaveAs(Server.MapPath(driver.Cpf.Image));
                    }

                    if (Images[2] != null)
                    {
                        driver.Cnh.Image = ConfigurationManager.AppSettings["ImagesDirectory"] + driver.Cnh.Number + Path.GetExtension(Images[2].FileName);
                        Images[2].SaveAs(Server.MapPath(driver.Cnh.Image));
                    }

                    if (driver.Id == 0)
                    {
                        if (this.repository.Add(driver))
                            return RedirectToAction("Drivers");
                    }
                    else if (driver.Id != 0)
                    {
                        if (this.repository.Update(driver))
                            return RedirectToAction("Drivers");
                    }
                }
                else
                    TempData["ErrorMessage"] = "Existem dados incorretos";
            }
            else
                TempData["ErrorMessage"] = "Somente arquivos com a extensão .JPG são aceitos";

            return View("AddEdit", driver);
        }

        [HttpGet]
        public RedirectToRouteResult Delete(int Id)
        {
            var Images = (from d in this.repository.Drivers
                          where d.Id == Id
                          select new { RgImage = d.Rg.Image, CpfImage = d.Cpf.Image, CnhImage = d.Cnh.Image }).Single();


            if (this.repository.Delete(Id))
            {
                if(System.IO.File.Exists(Server.MapPath(Images.RgImage)))
                    System.IO.File.Delete(Server.MapPath(Images.RgImage));
                if(System.IO.File.Exists(Server.MapPath(Images.CpfImage)))
                    System.IO.File.Delete(Server.MapPath(Images.CpfImage));
                if(System.IO.File.Exists(Server.MapPath(Images.CnhImage)))
                    System.IO.File.Delete(Server.MapPath(Images.CnhImage));
            }
            else
                TempData["ErrorMessage"] = "Ocorreu um erro na operação. Verifique se não existem fichas relacionadas à este motorista.";
            
            return RedirectToAction("Drivers");
        }

        /* IS HERE THE RIGHT LOCATION OF THIS METHOD ???????? */
        private string PhoneFormat(string phone)
        {
            if (phone.Length == 10)
                phone = String.Format("({0}) {1}-{2}", phone.Substring(0, 2),
                                                       phone.Substring(2, 4),
                                                       phone.Substring(6, 4));
            else if (phone.Length == 11)
                phone = String.Format("({0}) {1}-{2}", phone.Substring(0, 2),
                                                       phone.Substring(2, 5),
                                                       phone.Substring(7, 4));
            return phone;
        }
    }
}
