using Microsoft.AspNetCore.Mvc;
using Teacher.DTO;
using Teacher.Infrastructure.Interfaces;

namespace Teacher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeachersController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var teacher = await _teacherRepository.GetAsync();
            return Ok(teacher);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);
            if (teacher is null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeacherPostDto request)
        {
            var teacher = new Domain.Teacher
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

            _teacherRepository.Insert(teacher);
            await _teacherRepository.SaveChangesAsync();
            return Ok(teacher);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TeacherUpdateDto entityToUpdate)
        {
            var isExistingTeacher = await _teacherRepository.GetByIdAsync(entityToUpdate.Id);

            if (isExistingTeacher is null)
            {
                return NotFound(new { Error = "teacher not found" });
            }

            isExistingTeacher.PhoneNumber = entityToUpdate.PhoneNumber;
            isExistingTeacher.Address = entityToUpdate.Address;
            isExistingTeacher.City = entityToUpdate.City;
            isExistingTeacher.State = entityToUpdate.State;
            isExistingTeacher.Country = entityToUpdate.Country;
            isExistingTeacher.ZipCode = entityToUpdate.ZipCode;

            _teacherRepository.Update(isExistingTeacher);
            await _teacherRepository.SaveChangesAsync();
            return Ok(isExistingTeacher);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isExistingTeacher = await _teacherRepository.GetByIdAsync(id);
            if (isExistingTeacher is null)
            {
                return NotFound();
            }
            _teacherRepository.Delete(isExistingTeacher);
            return Ok();
        }
    }
}