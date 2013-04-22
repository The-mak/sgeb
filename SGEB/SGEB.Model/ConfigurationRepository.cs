using System;
using System.Linq;

namespace SGEB.Model
{
    public class ConfigurationRepository : IDisposable
    {
        private SGEBEntities context;
        public Configuration Configuration
        {
            get
            {
                Configuration temp = this.context.Configurations.SingleOrDefault<Configuration>();

                if(temp == null)
                {
                    temp = new Configuration() { Name = String.Empty, DocNumber = String.Empty, StateRegistration = String.Empty,
                        Telephone = String.Empty, CelPhone = String.Empty, RadioPhone = String.Empty, Email = String.Empty };
                    
                    this.context.Configurations.AddObject(temp);
                    this.context.SaveChanges();
                }

                return temp;
            }
            set
            {
                if (value != null)
                {
                    Configuration objToUpdate = this.Configuration;
                    objToUpdate.Name = value.Name;
                    objToUpdate.DocNumber = value.DocNumber;
                    objToUpdate.StateRegistration = value.StateRegistration;
                    objToUpdate.Telephone = value.Telephone;
                    objToUpdate.CelPhone = value.CelPhone;
                    objToUpdate.RadioPhone = value.RadioPhone;
                    objToUpdate.Email = value.Email;

                    this.context.SaveChanges();
                }
            }
        }

        public ConfigurationRepository()
        {
            this.context = new SGEBEntities();
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
