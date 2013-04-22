
namespace SGEB.Models
{
    public class VehicleViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }

        private string plate;
        public string Plate
        {
            get { return this.plate; }
            set
            {
                if (value.Length == 7)
                    this.plate = string.Format("{0}-{1}", value.Substring(0, 3), value.Substring(3, 4));
                else if (value.Length == 15)
                    this.plate = string.Format("{0}-{1}/{2}-{3}", value.Substring(0, 3), value.Substring(3, 4),
                                                                  value.Substring(8, 3), value.Substring(11, 4));
                else
                    this.plate = value;
            }
        }
    }
}