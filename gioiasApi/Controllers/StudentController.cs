using gioiasApi.Interfaces;
using gioiasApi.Models;
using gioiasApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Razor.Hosting;

namespace gioiasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return Ok(await _studentRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var student = await _studentRepository.GetById(id);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult> AddStudent(Student student)
        {
            _studentRepository.Create(student);
            if(await _studentRepository.SaveAllAsync())
            {
                return Ok(student);
            }

            return BadRequest("Error at create a new student.");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateStudent(Student student)
        {
            _studentRepository.Edit(student);
            if(await _studentRepository.SaveAllAsync())
            {
                return Ok(student);
            }

            return BadRequest("Error in operation.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var student = await _studentRepository.GetById(id);

            if(student == null)
            {
                return NotFound("Student not found.");
            }

            _studentRepository.Delete(student);

            if(await _studentRepository.SaveAllAsync())
            {
                return Ok(student);
            }

            return BadRequest("Error in operation.");
        }        
    }
}
