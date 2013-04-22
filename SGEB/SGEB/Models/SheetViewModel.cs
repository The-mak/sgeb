using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGEB.Models
{
    public class SheetViewModel
    {
        public int Id { get; set; }
        public DriverSheetViewModel Driver { get; set; }
        public VehicleSheetViewModel Truck { get; set; }
        public VehicleSheetViewModel Cart { get; set; }
    }

    public class AddSheetViewModel
    {
        public List<DriverSheetViewModel> Drivers { get; set; }
        public List<VehicleSheetViewModel> Trucks { get; set; }
        public List<VehicleSheetViewModel> Carts { get; set; }
    }

    public class DriverSheetViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class VehicleSheetViewModel
    {
        public int Id { get; set; }

        private string plate;
        public string Plate
        {
            get { return this.plate; }
            set
            {
                if (value.Length == 7)
                    this.plate = string.Format("{0}-{1}", value.Substring(0, 3), value.Substring(3, 4));
                else
                    this.plate = value;
            }
        }

        private string secondaryPlate;
        public string SecondaryPlate
        {
            get { return this.secondaryPlate; }
            set
            {
                if (value != null && value.Length == 7)
                    this.secondaryPlate = string.Format("{0}-{1}", value.Substring(0, 3), value.Substring(3, 4));
                else
                    this.secondaryPlate = value;
            }
        }
    }
}