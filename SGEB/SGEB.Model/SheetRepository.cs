using System;
using System.Linq;

namespace SGEB.Model
{
    public class SheetRepository : IDisposable
    {
        private SGEBEntities context;
        public IQueryable<Sheet> Sheets
        {
            get { return this.context.Sheets; }
        }

        public SheetRepository()
        {
            this.context = new SGEBEntities();
        }

        public bool Add(int DriverId, int TruckId, int CartId)
        {
            if (this.context.Sheets.Where<Sheet>(s => (s.Driver.Id == DriverId && s.Truck.Id == TruckId && s.Cart.Id == CartId)).SingleOrDefault<Sheet>() == null)
            {
                Driver driver = this.context.Drivers.Where<Driver>(d => d.Id == DriverId).Single<Driver>();
                Vehicle truck = this.context.Vehicles.Where<Vehicle>(v => v.Id == TruckId).Single<Vehicle>();
                Vehicle cart = this.context.Vehicles.Where<Vehicle>(v => v.Id == CartId).Single<Vehicle>();

                Sheet sheet = this.context.Sheets.CreateObject();
                sheet.Driver = driver;
                sheet.Truck = truck;
                sheet.Cart = cart;

                this.context.Sheets.AddObject(sheet);

                if (this.context.SaveChanges() > 0)
                    return true;
            }
            
            return false;
        }

        public bool Delete(int id)
        {
            Sheet sheetToDelete = this.context.Sheets.Where<Sheet>(s => s.Id == id).Single<Sheet>();
            this.context.Sheets.DeleteObject(sheetToDelete);

            if (this.context.SaveChanges() > 0)
                return true;
            
            return false;
        }

        #region Disposable Pattern
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
