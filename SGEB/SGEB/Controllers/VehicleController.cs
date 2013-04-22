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
    public class VehicleController : Controller
    {
        private VehicleRepository repository;

        public VehicleController()
        {
            this.repository = new VehicleRepository();
        }

        [HttpGet]
        public ViewResult Vehicles()
        {
            IList<VehicleViewModel> vehiclesViewModel = new List<VehicleViewModel>();
            
            foreach(Vehicle vehicle in this.repository.Vehicles.Where<Vehicle>(v => !v.Type.Equals("Secondary")).OrderBy(v => v.Plate).ToList<Vehicle>())
            {
                VehicleViewModel vvm = new VehicleViewModel();
                vvm.Id = vehicle.Id;
                vvm.Type = vehicle.Type;
                vvm.Model = vehicle.Model;

                if (vvm.Type.Equals("Bi-Trem"))
                    vvm.Plate = string.Format("{0}/{1}", vehicle.Plate, vehicle.SecondaryVehicle.Plate);
                else
                    vvm.Plate = vehicle.Plate;

                vehiclesViewModel.Add(vvm);
            }

            return View(vehiclesViewModel);
        }

        [HttpGet]
        public ViewResult Vehicle(int Id)
        {
            Vehicle vehicle = this.repository.GetVehicle(Id);
            vehicle.Plate = this.PlateFormat(vehicle.Plate);
            vehicle.Owner.Phone = this.PhoneFormat(vehicle.Owner.Phone);

            if (vehicle.SecondaryVehicle != null)
            {
                vehicle.SecondaryVehicle.Plate = this.PlateFormat(vehicle.SecondaryVehicle.Plate);
                vehicle.SecondaryVehicle.Owner.Phone = this.PhoneFormat(vehicle.SecondaryVehicle.Owner.Phone);
            }

            return View(vehicle);
        }

        [HttpGet]
        public ViewResult SelectType()
        {
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult SelectType(string Type)
        {
            return RedirectToAction("Add", "Vehicle", new { Type });
        }

        [HttpGet]
        public ActionResult Add(string Type)
        {
            Vehicle vehicle = null;

            if (Type.Equals("Caminhão") || Type.Equals("Carreta"))
                vehicle = new Vehicle() { Type = Type, Owner = new Owner() { Address = new Address() } };
            else if (Type.Equals("Bi-Trem"))
                vehicle = new Vehicle()
                {
                    Type = Type,
                    Owner = new Owner() { Address = new Address() },
                    SecondaryVehicle = new Vehicle() { Type = Type, Owner = new Owner() { Address = new Address() } }
                };
            else
                return RedirectToAction("SelectType");

            return View("AddEdit", vehicle);
        }

        [HttpGet]
        public ViewResult Edit(int Id)
        {
            return View("AddEdit", this.repository.Vehicles.Where<Vehicle>(v => v.Id == Id).SingleOrDefault<Vehicle>());
        }

        [HttpPost]
        public ActionResult Save(Vehicle vehicle, HttpPostedFileBase[] Images)
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
                        vehicle.Image = ConfigurationManager.AppSettings["ImagesDirectory"] + vehicle.Renavam + Path.GetExtension(Images[0].FileName);
                        Images[0].SaveAs(Server.MapPath(vehicle.Image));
                    }

                    if (Images.Length == 2 && Images[1] != null)
                    {
                        vehicle.SecondaryVehicle.Image = ConfigurationManager.AppSettings["ImagesDirectory"] + vehicle.SecondaryVehicle.Renavam + Path.GetExtension(Images[1].FileName);
                        Images[1].SaveAs(Server.MapPath(vehicle.SecondaryVehicle.Image));
                    }

                    if (vehicle.Id == 0)
                    {
                        if (this.repository.Add(vehicle))
                            return RedirectToAction("Vehicles");
                    }
                    else
                    {
                        if (this.repository.Update(vehicle))
                            return RedirectToAction("Vehicles");
                    }
                }
                else
                    TempData["ErrorMessage"] = "Existem dados incorretos";
            }
            else
                TempData["ErrorMessage"] = "Sómente imagens com a extensão JPG são aceitas";

            return View("AddEdit", vehicle);
        }

        public RedirectToRouteResult Delete(int Id)
        {
            var vehicleToDelete = (from v in this.repository.Vehicles
                                   where v.Id == Id
                                   select new { FirstImage = v.Image, SecondImage = v.SecondaryVehicle.Image }).Single();

            if (this.repository.Delete(Id))
            {
                if (vehicleToDelete != null)
                {
                    if (!String.IsNullOrWhiteSpace(vehicleToDelete.FirstImage))
                        if (System.IO.File.Exists(Server.MapPath(vehicleToDelete.FirstImage)))
                            System.IO.File.Delete(Server.MapPath(vehicleToDelete.FirstImage));

                    if (!String.IsNullOrWhiteSpace(vehicleToDelete.SecondImage))
                        if (System.IO.File.Exists(Server.MapPath(vehicleToDelete.SecondImage)))
                            System.IO.File.Delete(Server.MapPath(vehicleToDelete.SecondImage));
                }
            }              
            else
                TempData["ErrorMessage"] = "Ocorreu um erro na operação. Verifique se não existem fichas relacionadas à esse veiculo";

            return RedirectToAction("Vehicles");
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
        private string PlateFormat(string plate)
        {
            if (plate.Length == 7)
                plate = string.Format("{0}-{1}", plate.Substring(0, 3), plate.Substring(3, 4));

            return plate;
        }
    }
}
