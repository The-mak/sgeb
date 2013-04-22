using System;
using System.Data.SqlClient;
using System.Linq;

namespace SGEB.Model
{
    public class VehicleRepository : IDisposable
    {
        private SGEBEntities context;
        public IQueryable<Vehicle> Vehicles
        {
            get
            {
                return this.context.Vehicles;
            }
        }

        public VehicleRepository()
        {
            this.context = new SGEBEntities();
        }

        public Vehicle GetVehicle(int id)
        {
            return this.Vehicles.Where<Vehicle>(v => v.Id == id).Single<Vehicle>();
        }

        public bool Add(Vehicle vehicle)
        {
            this.context.Vehicles.AddObject(vehicle);

            if (this.context.SaveChanges() > 0)
                return true;
                
            return false;
        }

        public bool Update(Vehicle vehicle)
        {
            var vehicleToUpdate = this.Vehicles.Where<Vehicle>(v => v.Id == vehicle.Id).SingleOrDefault<Vehicle>();

            vehicleToUpdate.Plate = vehicle.Plate;
            vehicleToUpdate.Model = vehicle.Model;
            vehicleToUpdate.Chassi = vehicle.Chassi;
            vehicleToUpdate.Renavam = vehicle.Renavam;
            vehicleToUpdate.Color = vehicle.Color;
            vehicleToUpdate.Year = vehicle.Year;
            vehicleToUpdate.City = vehicle.City;
            vehicleToUpdate.State = vehicle.State;
            vehicleToUpdate.ANTT = vehicle.ANTT;

            if(!String.IsNullOrWhiteSpace(vehicle.Image))
                vehicleToUpdate.Image = vehicle.Image;

            vehicleToUpdate.Owner.Name = vehicle.Owner.Name;
            vehicleToUpdate.Owner.DocNumber = vehicle.Owner.DocNumber;
            vehicleToUpdate.Owner.Address.Street = vehicle.Owner.Address.Street;
            vehicleToUpdate.Owner.Address.Number = vehicle.Owner.Address.Number;
            vehicleToUpdate.Owner.Address.Neighborhood = vehicle.Owner.Address.Neighborhood;
            vehicleToUpdate.Owner.Address.ZipCode = vehicle.Owner.Address.ZipCode;
            vehicleToUpdate.Owner.Address.City = vehicle.Owner.Address.City;
            vehicleToUpdate.Owner.Address.State = vehicle.Owner.Address.State;
            vehicleToUpdate.Owner.Phone = vehicle.Owner.Phone;

            if (vehicle.Type.Equals("Bi-Trem"))
            {
                vehicleToUpdate.SecondaryVehicle.Plate = vehicle.SecondaryVehicle.Plate;
                vehicleToUpdate.SecondaryVehicle.Model = vehicle.SecondaryVehicle.Model;
                vehicleToUpdate.SecondaryVehicle.Chassi = vehicle.SecondaryVehicle.Chassi;
                vehicleToUpdate.SecondaryVehicle.Renavam = vehicle.SecondaryVehicle.Renavam;
                vehicleToUpdate.SecondaryVehicle.Color = vehicle.SecondaryVehicle.Color;
                vehicleToUpdate.SecondaryVehicle.Year = vehicle.SecondaryVehicle.Year;
                vehicleToUpdate.SecondaryVehicle.City = vehicle.SecondaryVehicle.City;
                vehicleToUpdate.SecondaryVehicle.State = vehicle.SecondaryVehicle.State;
                vehicleToUpdate.SecondaryVehicle.ANTT = vehicle.SecondaryVehicle.ANTT;

                if (!String.IsNullOrEmpty(vehicle.SecondaryVehicle.Image) && !String.IsNullOrWhiteSpace(vehicle.SecondaryVehicle.Image))
                    vehicleToUpdate.SecondaryVehicle.Image = vehicle.SecondaryVehicle.Image;

                vehicleToUpdate.SecondaryVehicle.Owner.Name = vehicle.SecondaryVehicle.Owner.Name;
                vehicleToUpdate.SecondaryVehicle.Owner.DocNumber = vehicle.SecondaryVehicle.Owner.DocNumber;
                vehicleToUpdate.SecondaryVehicle.Owner.Address.Street = vehicle.SecondaryVehicle.Owner.Address.Street;
                vehicleToUpdate.SecondaryVehicle.Owner.Address.Number = vehicle.SecondaryVehicle.Owner.Address.Number;
                vehicleToUpdate.SecondaryVehicle.Owner.Address.Neighborhood = vehicle.SecondaryVehicle.Owner.Address.Neighborhood;
                vehicleToUpdate.SecondaryVehicle.Owner.Address.ZipCode = vehicle.SecondaryVehicle.Owner.Address.ZipCode;
                vehicleToUpdate.SecondaryVehicle.Owner.Address.City = vehicle.SecondaryVehicle.Owner.Address.City;
                vehicleToUpdate.SecondaryVehicle.Owner.Address.State = vehicle.SecondaryVehicle.Owner.Address.State;
                vehicleToUpdate.SecondaryVehicle.Owner.Phone = vehicle.SecondaryVehicle.Owner.Phone;
            }

            if (this.context.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        public bool Delete(int Id)
        {
            Vehicle vehicleToDelete = this.Vehicles.Where<Vehicle>(v => v.Id == Id).SingleOrDefault<Vehicle>();
            this.context.ObjectStateManager.ChangeObjectState(vehicleToDelete.Owner.Address, System.Data.EntityState.Deleted);
            this.context.ObjectStateManager.ChangeObjectState(vehicleToDelete.Owner, System.Data.EntityState.Deleted);
            if(vehicleToDelete.SecondaryVehicle != null)
            {
                this.context.ObjectStateManager.ChangeObjectState(vehicleToDelete.SecondaryVehicle.Owner.Address, System.Data.EntityState.Deleted);
                this.context.ObjectStateManager.ChangeObjectState(vehicleToDelete.SecondaryVehicle.Owner, System.Data.EntityState.Deleted);
                this.context.ObjectStateManager.ChangeObjectState(vehicleToDelete.SecondaryVehicle, System.Data.EntityState.Deleted);
            }

            this.context.ObjectStateManager.ChangeObjectState(vehicleToDelete, System.Data.EntityState.Deleted);

            try
            {
                if (this.context.SaveChanges() > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (SqlException) { return false; }
            catch (Exception) { return false; }
        }

        #region Dispose Pattern
        private bool isDisposed;

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!this.isDisposed)
            {
                if (isDisposing)
                {
                    this.context.Dispose();
                }

                this.isDisposed = true;
            }
        }
        #endregion
    }
}
