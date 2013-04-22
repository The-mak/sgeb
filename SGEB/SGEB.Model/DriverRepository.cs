using System;
using System.Data.SqlClient;
using System.Linq;

namespace SGEB.Model
{
    public class DriverRepository : IDisposable
    {
        private SGEBEntities context;
        public IQueryable<Driver> Drivers
        {
            get
            {
                return this.context.Drivers;
           }
        }
        
        public DriverRepository()
        {
            this.context = new SGEBEntities();
        }

        public Driver GetDriver(int Id)
        {
            return (from d in this.Drivers
                    where d.Id == Id
                    select d).SingleOrDefault<Driver>();
        }

        public bool Add(Driver driver)
        {
            this.context.Drivers.AddObject(driver);

            if (this.context.SaveChanges() > 0)
                return true;

            return false;
        }

        public bool Update(Driver driver)
        {
            var driverToUpdate = (from d in this.Drivers where d.Id == driver.Id select d).SingleOrDefault<Driver>();

            driverToUpdate.Rg.Name = driver.Rg.Name;
            driverToUpdate.Rg.Number = driver.Rg.Number;
            driverToUpdate.Rg.BornDate = driver.Rg.BornDate;
            driverToUpdate.Rg.EmittedDate = driver.Rg.EmittedDate;
            driverToUpdate.Rg.FathersName = driver.Rg.FathersName;
            driverToUpdate.Rg.MothersName = driver.Rg.MothersName;
            driverToUpdate.Rg.City = driver.Rg.City;
            driverToUpdate.Rg.State = driver.Rg.State;
            if(!String.IsNullOrEmpty(driver.Rg.Image) && !String.IsNullOrWhiteSpace(driver.Rg.Image))
                driverToUpdate.Rg.Image = driver.Rg.Image;

            driverToUpdate.Cpf.Number = driver.Cpf.Number;
            if(!String.IsNullOrEmpty(driver.Cpf.Image) && !String.IsNullOrWhiteSpace(driver.Cpf.Image))
                driverToUpdate.Cpf.Image = driver.Cpf.Image;

            driverToUpdate.Cnh.Number = driver.Cnh.Number;
            driverToUpdate.Cnh.Category = driver.Cnh.Category;
            driverToUpdate.Cnh.Record = driver.Cnh.Record;
            driverToUpdate.Cnh.EmittedDate = driver.Cnh.EmittedDate;
            driverToUpdate.Cnh.DueDate = driver.Cnh.DueDate;
            if(!String.IsNullOrEmpty(driver.Cnh.Image) && !String.IsNullOrWhiteSpace(driver.Cnh.Image))
                driverToUpdate.Cnh.Image = driver.Cnh.Image;

            driverToUpdate.Address.Street = driver.Address.Street;
            driverToUpdate.Address.Number = driver.Address.Number;
            driverToUpdate.Address.Neighborhood = driver.Address.Neighborhood;
            driverToUpdate.Address.ZipCode = driver.Address.ZipCode;
            driverToUpdate.Address.City = driver.Address.City;
            driverToUpdate.Address.State = driver.Address.State;

            driverToUpdate.Contact.HomePhone = driver.Contact.HomePhone;
            driverToUpdate.Contact.CelPhone = driver.Contact.CelPhone;
            driverToUpdate.Contact.RefPhone1 = driver.Contact.RefPhone1;
            driverToUpdate.Contact.RefContact1 = driver.Contact.RefContact1;
            driverToUpdate.Contact.RefPhone2 = driver.Contact.RefPhone2;
            driverToUpdate.Contact.RefContact2 = driver.Contact.RefContact2;

            if (this.context.SaveChanges() > 0)
                return true;

            return false;
        }

        public bool Delete(int Id)
        {
            Driver driver = this.Drivers.Where<Driver>(d => d.Id == Id).SingleOrDefault<Driver>();
            this.context.ObjectStateManager.ChangeObjectState(driver.Rg, System.Data.EntityState.Deleted);
            this.context.ObjectStateManager.ChangeObjectState(driver.Cpf, System.Data.EntityState.Deleted);
            this.context.ObjectStateManager.ChangeObjectState(driver.Cnh, System.Data.EntityState.Deleted);
            this.context.ObjectStateManager.ChangeObjectState(driver.Address, System.Data.EntityState.Deleted);
            this.context.ObjectStateManager.ChangeObjectState(driver.Contact, System.Data.EntityState.Deleted);

            this.context.Drivers.DeleteObject(driver);

            try
            {
                if (this.context.SaveChanges() > 0)
                    return true;
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
