using ExcelDataReader;
using Microsoft.AspNetCore.Http;
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
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ImportUsers(IFormFile file)
        {
            // copy
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);

                fileStream.Flush();
            }

            // read data

            List<User> users = getAllUsersFromFile(file.FileName);

            HttpClient client = new HttpClient();

            string URL = "https://localhost:44371/api/Referral/ImportAllUsers";

            HttpContent content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<bool>().Result;

            return RedirectToAction("Index", "Referral");
        }

        private List<User> getAllUsersFromFile(string fileName)
        {
            List<User> users = new List<User>();

            string filePath  = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using(var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                 using(var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        Guid roleGuid = Guid.Empty;
                        string role = reader.GetValue(4).ToString();
                        roleGuid = Guid.Parse(role);

                        users.Add(new Models.User
                        {
                            FirstName = reader.GetValue(0).ToString(),
                            LastName = reader.GetValue(1).ToString(),
                            Address = reader.GetValue(2).ToString(),
                            Email = reader.GetValue(3).ToString(),
                            Role = roleGuid,
                            Password = reader.GetValue(5).ToString(),
                            ConfirmPassword = reader.GetValue(6).ToString()
                        });
                    }
                }
            }

            return users;
        }
    }
}
