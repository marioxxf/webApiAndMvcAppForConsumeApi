using gioiasMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace gioiasMvc.Controllers
{
    public class StudentsController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        Student _oStudent = new Student();
        List<Student> _oStudents = new List<Student>();

        public StudentsController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        
        public IActionResult Index()
        {
            HttpContext.Session.SetString("SessionKey", "Gioias"); // Isso trava a geração de um novo SessionID a cada requisição na mesma instância de navegador
            ViewData["SessionId"] = HttpContext.Session.Id;
            return View();
        }

        [HttpGet]
        public async Task<List<Student>> GetAllStudents()
        {
            _oStudents = new List<Student>();

            using(var httpClient = new HttpClient(_clientHandler)) 
            {
                using(var response = await httpClient.GetAsync("https://localhost:7200/api/Student?apiKey=secretKey"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oStudents = JsonConvert.DeserializeObject<List<Student>>(apiResponse);
                }
            }
            return _oStudents;
        }

        [HttpGet]
        public async Task<Student> GetById(int studentId)
        {
            _oStudent = new Student();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:7200/api/Student/" + studentId + "?apiKey=secretKey"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oStudent = JsonConvert.DeserializeObject<Student>(apiResponse);
                }
            }
            return _oStudent;
        }

        [HttpGet]
        public async Task<Student> GetByAproximatedName(string studentToFind)
        {
            _oStudent = new Student();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:7200/api/Student/query/" + studentToFind + "?apiKey=secretKey"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oStudent = JsonConvert.DeserializeObject<Student>(apiResponse);
                }
            }
            return _oStudent;
        }

        [HttpPost]
        public async Task<Student> AddStudent(Student student)
        {
            _oStudent = new Student();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7200/api/Student?apiKey=secretKey", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oStudent = JsonConvert.DeserializeObject<Student>(apiResponse);
                }
            }
            return _oStudent;
        }

        [HttpPut]
        public async Task<Student> UpdateStudent(Student student)
        {
            _oStudent = new Student();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:7200/api/Student?apiKey=secretKey", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oStudent = JsonConvert.DeserializeObject<Student>(apiResponse);
                }
            }
            return _oStudent;
        }

        [HttpDelete]
        public async Task<Student> DeleteStudent(int studentId)
        {
            _oStudent = new Student();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:7200/api/Student/" + studentId + "?apiKey=secretKey"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oStudent = JsonConvert.DeserializeObject<Student>(apiResponse);
                }
            }
            return _oStudent;
        }
    }
}