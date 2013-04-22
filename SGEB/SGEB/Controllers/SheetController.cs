using PdfUtility;
using SGEB.Model;
using SGEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace SGEB.Controllers
{
    [Authorize]
    public class SheetController : Controller
    {
        private SheetRepository repository;

        public SheetController()
        {
            this.repository = new SheetRepository();
        }

        public ViewResult Sheets()
        {
            List<SheetViewModel> sheets = (from s in this.repository.Sheets
                                           select new SheetViewModel()
                                           {
                                               Id = s.Id,
                                               Driver = new DriverSheetViewModel() { Id = s.Driver.Id, Name = s.Driver.Rg.Name },
                                               Truck = new VehicleSheetViewModel() { Id = s.Truck.Id, Plate = s.Truck.Plate, SecondaryPlate = null },
                                               Cart = new VehicleSheetViewModel() { Id = s.Cart.Id, Plate = (s.Cart.Plate), SecondaryPlate = s.Cart.SecondaryVehicle.Plate },
                                           }).ToList<SheetViewModel>();
            return View(sheets);
        }

        [HttpGet]
        public ViewResult AddSheet()
        {
            AddSheetViewModel sheet = new AddSheetViewModel();

            using (DriverRepository repo = new DriverRepository())
            {
                sheet.Drivers = (from d in repo.Drivers
                                  select new DriverSheetViewModel() { Id = d.Id, Name = d.Rg.Name }).ToList<DriverSheetViewModel>();
            }

            using (VehicleRepository repo = new VehicleRepository())
            {
                sheet.Trucks = (from t in repo.Vehicles
                                 where t.Type.Equals("Caminhão")
                                 select new VehicleSheetViewModel() { Id = t.Id, Plate = t.Plate, SecondaryPlate = null }).ToList<VehicleSheetViewModel>();
                sheet.Carts = (from c in repo.Vehicles
                                where (c.Type.Equals("Carreta") || c.Type.Equals("Bi-Trem"))
                                select new VehicleSheetViewModel() { Id = c.Id, Plate = c.Plate, SecondaryPlate = c.SecondaryVehicle.Plate }).ToList<VehicleSheetViewModel>();
            }

            return View(sheet);
        }

        [HttpPost]
        public ActionResult AddSheet(int DriverId = 0, int TruckId = 0, int CartId = 0)
        {
            if (DriverId == 0 || TruckId == 0 || CartId == 0)
            {
                TempData["ErrorMessage"] = "Selecione os dados corretamente";
                return RedirectToAction("AddSheet");
            }
            else
            {
                if (this.repository.Add(DriverId, TruckId, CartId))
                {
                    return RedirectToAction("Sheets", "Sheet");
                }
                else
                {
                    TempData["ErrorMessage"] = "A ficha não pode ser gerada";
                    return RedirectToAction("AddSheet");
                }
            }
        }
        
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (!this.repository.Delete(id))
                TempData["ErrorMessage"] = "Ocorreu um erro na operação";

            return RedirectToAction("Sheets", "Sheet");
        }

        public FileResult ViewPdf(int id)
        {
            if(Request.Browser.IsMobileDevice)
                return File(PdfGenerator.Generate(id), "application/pdf", "ficha.pdf");
            else
                return File(PdfGenerator.Generate(id), "application/pdf");
        }

        [HttpGet]
        public ViewResult SendRecord(int Id)
        {
            return View(Id);
        }

        [HttpPost]
        public RedirectToRouteResult SendRecord()
        {
            return RedirectToAction("Sheets");
        }

        public void SendMail(int id, string email)
        {
            List<string> files = (from s in this.repository.Sheets
                                  where s.Id == id
                                  select new List<String> { s.Driver.Rg.Image, s.Driver.Cpf.Image, s.Driver.Cnh.Image,
                                            s.Truck.Image, s.Cart.Image, s.Cart.SecondaryVehicle.Image}).Single();

            for (int x = 0; x < files.Count(); x++)
            {
                if (!string.IsNullOrWhiteSpace(files[x]))
                    files[x] = Server.MapPath(files[x]);
                else
                    files.RemoveAt(x);
            }

            Mailers.UserMailer m = new Mailers.UserMailer();
            m.Welcome(email, new MemoryStream(PdfGenerator.Generate(id)), files).SendAsync();
        }
    }
}
