using ClosedXML.Excel;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using MojTerminAdminApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MojTerminAdminApp.Controllers
{
    public class ReferralController : Controller
    {
        public ReferralController()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44371/api/Referral/GetAllReferrals";

            HttpResponseMessage response = client.GetAsync(URL).Result;

            var data = response.Content.ReadAsAsync<List<Referral>>().Result;

            return View(data);
        }

        public IActionResult Details(Guid referralId)
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44371/api/Referral/GetDetailsForReferral";

            var model = new
            {
                Id = referralId
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<Referral>().Result;

            return View(data);
        }

        public FileContentResult PrintReferral(Guid referralId)
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44371/api/Referral/GetDetailsForReferral";

            var model = new
            {
                Id = referralId
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var result = response.Content.ReadAsAsync<Referral>().Result;

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "PrintReferral.docx");
            var document = DocumentModel.Load(templatePath);

            document.Content.Replace("{{Id}}", result.Id.ToString());
            document.Content.Replace("{{PatientName}}", result.Patient.Name);
            document.Content.Replace("{{PatientSurname}}", result.Patient.Surname);
            document.Content.Replace("{{PatientSsn}}", result.Patient.Ssn.ToString());
            document.Content.Replace("{{PatientUhid}}", result.Patient.Uhid.ToString());
            document.Content.Replace("{{CurrentDoctor}}", result.Patient.Doctor.PrintFullName());
            document.Content.Replace("{{DoctorForwardedTo}}", result.ForwardTo.PrintFullName());
            document.Content.Replace("{{Term}}", result.Term.ToString());

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());

            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportReferral.pdf");
        }

        [HttpGet]
        public FileContentResult ExportAllReferrals()
        {
            string fileName = "Referrals.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("All Referrals");

                worksheet.Cell(1, 1).Value = "Referral Id";
                worksheet.Cell(1, 2).Value = "Patient Name";
                worksheet.Cell(1, 3).Value = "Patient Surname";
                worksheet.Cell(1, 4).Value = "Patient Ssn";
                worksheet.Cell(1, 5).Value = "Patient Uhid";
                worksheet.Cell(1, 6).Value = "Current Doctor";
                worksheet.Cell(1, 7).Value = "Doctor Forwarded To";

                HttpClient client = new HttpClient();

                string URL = "https://localhost:44371/api/Referral/GetAllReferrals";

                HttpResponseMessage response = client.GetAsync(URL).Result;

                var data = response.Content.ReadAsAsync<List<Referral>>().Result;

                for (int i = 1; i <= data.Count; i++)
                {
                    var item = data[i-1];
                    worksheet.Cell(i + 1, 1).Value = item.Id.ToString();
                    worksheet.Cell(i + 1, 2).Value = item.Patient.Name;
                    worksheet.Cell(i + 1, 3).Value = item.Patient.Surname;
                    worksheet.Cell(i + 1, 4).Value = item.Patient.Ssn;
                    worksheet.Cell(i + 1, 5).Value = item.Patient.Uhid;
                    worksheet.Cell(i + 1, 6).Value = item.Patient.Doctor.PrintFullName();
                    worksheet.Cell(i + 1, 7).Value = item.ForwardTo.PrintFullName();
                }

                using(var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }
        }
    }
}
