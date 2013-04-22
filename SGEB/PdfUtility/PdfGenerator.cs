using iTextSharp.text.pdf;
using SGEB.Model;
using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace PdfUtility
{
    public static class PdfGenerator
    {
        public static byte[] Generate(int Id)
        {
            byte[] generatedPdf = null;

            using (SheetRepository repository = new SheetRepository())
            using (ConfigurationRepository configurations = new ConfigurationRepository())
            {
                Sheet sheet = repository.Sheets.Where<Sheet>(s => s.Id == Id).SingleOrDefault<Sheet>();

                if (sheet != null)
                {
                    MemoryStream output = new MemoryStream();

                    PdfReader reader = new PdfReader(ConfigurationManager.AppSettings[(sheet.Cart.SecondaryVehicle != null) ? "FormPathBi" : "FormPath"]);
                    PdfStamper stamper = new PdfStamper(reader, output, PdfWriter.VERSION_1_7);

                    stamper.AcroFields.SetField("enterpriseName", configurations.Configuration.Name);
                    stamper.AcroFields.SetField("enterpriseDocNumber", configurations.Configuration.DocNumber);
                    stamper.AcroFields.SetField("enterpriseRegState", configurations.Configuration.StateRegistration);
                    stamper.AcroFields.SetField("enterpriseTelephone", PdfGenerator.PhoneFormat(configurations.Configuration.Telephone));
                    stamper.AcroFields.SetField("enterpriseCelphone", PdfGenerator.PhoneFormat(configurations.Configuration.CelPhone));
                    stamper.AcroFields.SetField("enterpriseRadio", configurations.Configuration.RadioPhone);
                    stamper.AcroFields.SetField("enterpriseEmail", string.Format("E-mail: {0}", configurations.Configuration.Email));

                    stamper.AcroFields.SetField("driverRgName", sheet.Driver.Rg.Name);
                    stamper.AcroFields.SetField("driverRgBornDate", sheet.Driver.Rg.BornDate.ToShortDateString());
                    stamper.AcroFields.SetField("driverRgEmittedDate", sheet.Driver.Rg.EmittedDate.ToShortDateString());
                    stamper.AcroFields.SetField("driverRgNumber", sheet.Driver.Rg.Number);
                    stamper.AcroFields.SetField("driverRgCity", sheet.Driver.Rg.City);
                    stamper.AcroFields.SetField("driverRgState", sheet.Driver.Rg.State);
                    stamper.AcroFields.SetField("driverRgFatherMother", string.Format("{0} / {1}", sheet.Driver.Rg.FathersName, sheet.Driver.Rg.MothersName));

                    stamper.AcroFields.SetField("driverCpfNumber", sheet.Driver.Cpf.Number);

                    stamper.AcroFields.SetField("driverCnhNumber", sheet.Driver.Cnh.Number);
                    stamper.AcroFields.SetField("driverCnhEmittedDate", sheet.Driver.Cnh.EmittedDate.ToShortDateString());
                    stamper.AcroFields.SetField("driverCnhDueDate", sheet.Driver.Cnh.DueDate.ToShortDateString());
                    stamper.AcroFields.SetField("driverCnhRecord", sheet.Driver.Cnh.Record);
                    stamper.AcroFields.SetField("driverCnhCategory", sheet.Driver.Cnh.Category);

                    stamper.AcroFields.SetField("driverAddressAddress", string.Format("{0}, {1}", sheet.Driver.Address.Street, sheet.Driver.Address.Number));
                    stamper.AcroFields.SetField("driverAddressNeighborhood", sheet.Driver.Address.Neighborhood);
                    stamper.AcroFields.SetField("driverAddressZipCode", ((sheet.Driver.Address.ZipCode.Length == 8)? string.Format("{0}-{1}", sheet.Driver.Address.ZipCode.Substring(0, 5),
                        sheet.Driver.Address.ZipCode.Substring(5, 3)): sheet.Driver.Address.ZipCode));
                    stamper.AcroFields.SetField("driverAddressCityState", string.Format("{0}/{1}", sheet.Driver.Address.City, sheet.Driver.Address.State));

                    
                    stamper.AcroFields.SetField("driverContactHomePhone", PdfGenerator.PhoneFormat(sheet.Driver.Contact.HomePhone));
                    stamper.AcroFields.SetField("driverContactCelPhone", PdfGenerator.PhoneFormat(sheet.Driver.Contact.CelPhone));
                    stamper.AcroFields.SetField("driverContactRefPhone1", string.Format("{0} ({1})", PdfGenerator.PhoneFormat(sheet.Driver.Contact.RefPhone1),
                        sheet.Driver.Contact.RefContact1));
                    stamper.AcroFields.SetField("driverContactRefPhone2", string.Format("{0} ({1})", PdfGenerator.PhoneFormat(sheet.Driver.Contact.RefPhone2),
                        sheet.Driver.Contact.RefContact2));

                    stamper.AcroFields.SetField("truckPlate", PdfGenerator.PlateFormat(sheet.Truck.Plate));
                    stamper.AcroFields.SetField("truckRenavam", sheet.Truck.Renavam);
                    stamper.AcroFields.SetField("truckAntt", sheet.Truck.ANTT);
                    stamper.AcroFields.SetField("truckColor", sheet.Truck.Color);
                    stamper.AcroFields.SetField("truckYear", sheet.Truck.Year.ToString());
                    stamper.AcroFields.SetField("truckModel", sheet.Truck.Model);
                    stamper.AcroFields.SetField("truckChassi", sheet.Truck.Chassi);
                    stamper.AcroFields.SetField("truckOwner", sheet.Truck.Owner.Name);
                    stamper.AcroFields.SetField("truckOwnerDoc", sheet.Truck.Owner.DocNumber);
                    stamper.AcroFields.SetField("truckOwnerAddress", string.Format("{0}, {1} - {2} - {3}/{4}",
                        sheet.Truck.Owner.Address.Street, sheet.Truck.Owner.Address.Number.ToString(), 
                        sheet.Truck.Owner.Address.Neighborhood,
                        sheet.Truck.Owner.Address.City, sheet.Truck.Owner.Address.State));
                    stamper.AcroFields.SetField("truckOwnerPhone", PdfGenerator.PhoneFormat(sheet.Truck.Owner.Phone));

                    stamper.AcroFields.SetField("cartPlate", PdfGenerator.PlateFormat(sheet.Cart.Plate));
                    stamper.AcroFields.SetField("cartRenavam", sheet.Cart.Renavam);
                    stamper.AcroFields.SetField("cartAntt", sheet.Cart.ANTT);
                    stamper.AcroFields.SetField("cartColor", sheet.Cart.Color);
                    stamper.AcroFields.SetField("cartYear", sheet.Cart.Year.ToString());
                    stamper.AcroFields.SetField("cartModel", sheet.Cart.Model);
                    stamper.AcroFields.SetField("cartChassi", sheet.Cart.Chassi);
                    stamper.AcroFields.SetField("cartOwner", sheet.Cart.Owner.Name);
                    stamper.AcroFields.SetField("cartOwnerDoc", sheet.Cart.Owner.DocNumber);
                    stamper.AcroFields.SetField("cartOwnerAddress", string.Format("{0}, {1} - {2} - {3}/{4}",
                        sheet.Cart.Owner.Address.Street, sheet.Cart.Owner.Address.Number.ToString(),
                        sheet.Cart.Owner.Address.Neighborhood,
                        sheet.Cart.Owner.Address.City, sheet.Cart.Owner.Address.State));
                    stamper.AcroFields.SetField("cartOwnerPhone", PdfGenerator.PhoneFormat(sheet.Cart.Owner.Phone));

                    if (sheet.Cart.Type.Equals("Bi-Trem"))
                    {
                        stamper.AcroFields.SetField("secondaryCartPlate", PdfGenerator.PlateFormat(sheet.Cart.SecondaryVehicle.Plate));
                        stamper.AcroFields.SetField("secondaryCartRenavam", sheet.Cart.SecondaryVehicle.Renavam);
                        stamper.AcroFields.SetField("secondaryCartAntt", sheet.Cart.SecondaryVehicle.ANTT);
                        stamper.AcroFields.SetField("secondaryCartColor", sheet.Cart.SecondaryVehicle.Color);
                        stamper.AcroFields.SetField("secondaryCartYear", sheet.Cart.SecondaryVehicle.Year.ToString());
                        stamper.AcroFields.SetField("secondaryCartModel", sheet.Cart.SecondaryVehicle.Model);
                        stamper.AcroFields.SetField("secondaryCartChassi", sheet.Cart.SecondaryVehicle.Chassi);
                        stamper.AcroFields.SetField("secondaryCartOwner", sheet.Cart.SecondaryVehicle.Owner.Name);
                        stamper.AcroFields.SetField("secondaryCartOwnerDoc", sheet.Cart.Owner.DocNumber);
                        stamper.AcroFields.SetField("secondaryCartOwnerAddress", string.Format("{0}, {1} - {2} - {3}/{4}",
                            sheet.Cart.SecondaryVehicle.Owner.Address.Street, sheet.Cart.SecondaryVehicle.Owner.Address.Number.ToString(),
                            sheet.Cart.SecondaryVehicle.Owner.Address.Neighborhood,
                            sheet.Cart.SecondaryVehicle.Owner.Address.City, sheet.Cart.SecondaryVehicle.Owner.Address.State));
                        stamper.AcroFields.SetField("secondaryCartOwnerPhone", PdfGenerator.PhoneFormat(sheet.Cart.SecondaryVehicle.Owner.Phone));
                    }

                    stamper.FormFlattening = true;

                    stamper.Close();
                    reader.Close();

                    generatedPdf = output.ToArray();

                    output.Close();
                }
            }

            return generatedPdf;
        }

        /* IS HERE THE RIGHT LOCATION OF THIS METHOD ???????? */
        private static string PhoneFormat(string phone)
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
        private static string PlateFormat(string plate)
        {
            if (plate.Length == 7)
                plate = string.Format("{0}-{1}", plate.Substring(0, 3), plate.Substring(3, 4));

            return plate;
        }
    }
}
