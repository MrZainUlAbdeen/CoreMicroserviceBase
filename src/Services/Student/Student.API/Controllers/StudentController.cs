using Microsoft.AspNetCore.Mvc;
using Student.DTO;
using Student.Infrastructure.Interfaces;

namespace Student.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await _studentRepository.GetAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student is null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentPostDto request)
        {
            var student = new Domain.Student
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                City = request.City,
                State = request.State,
                Country = request.Country,
                ZipCode = request.ZipCode
            };

            _studentRepository.Insert(student);
            await _studentRepository.SaveChangesAsync();
            return Ok(student);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromBody] StudentUpdateDto entityToUpdate)
        {
            var isExistingStudent = await _studentRepository.GetByIdAsync(entityToUpdate.Id);

            if (isExistingStudent is null)
            {
                return NotFound(new { Error = " student not found" });
            }

            isExistingStudent.PhoneNumber = entityToUpdate.PhoneNumber;
            isExistingStudent.Address = entityToUpdate.Address;
            isExistingStudent.City = entityToUpdate.City;
            isExistingStudent.State = entityToUpdate.State;
            isExistingStudent.Country = entityToUpdate.Country;
            isExistingStudent.ZipCode = entityToUpdate.ZipCode;

            _studentRepository.Update(isExistingStudent);
            return Ok(isExistingStudent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var isExistingStudent = await _studentRepository.GetByIdAsync(id);
            if (isExistingStudent is null)
            {
                return NotFound();
            }
            _studentRepository.Delete(isExistingStudent);
            return Ok();
        }
    }
}