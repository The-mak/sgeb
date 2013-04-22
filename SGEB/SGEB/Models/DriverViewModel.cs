
namespace SGEB.Models
{
    public class DriverViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private string celphone;
        public string Celphone
        {
            get
            {
                return this.celphone;
            }
            set
            {
                if(value.Length == 10)
                    this.celphone = string.Format("({0}) {1}-{2}", value.Substring(0, 2), value.Substring(2, 4), value.Substring(6, 4));
                else if (value.Length == 11)
                    this.celphone = string.Format("({0}) {1}-{2}", value.Substring(0, 2), value.Substring(2, 5), value.Substring(7, 4));
                else
                    this.celphone = value;
            }
        }
    }
}