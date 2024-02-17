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
            var isExisting = await _studentRepository.GetByIdAsync(entityToUpdate.Id);

            if (isExisting is null)
            {
                return NotFound(new { Error = " student not found" });
            }

            isExisting.PhoneNumber = entityToUpdate.PhoneNumber;
            isExisting.Address = entityToUpdate.Address;
            isExisting.City = entityToUpdate.City;
            isExisting.State = entityToUpdate.State;
            isExisting.Country = entityToUpdate.Country;
            isExisting.ZipCode = entityToUpdate.ZipCode;

            _studentRepository.Update(isExisting);
            return Ok(isExisting);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var isExisting = await _studentRepository.GetByIdAsync(id);
            if (isExisting is null)
            {
                return NotFound();
            }
            _studentRepository.Delete(isExisting);
            return Ok();
        }
    }
}