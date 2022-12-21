using gioiasApi.Interfaces;
using gioiasApi.Models;
using Microsoft.EntityFrameworkCore;

namespace gioiasApi.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly GioiaContext _context; 
        public StudentRepository(GioiaContext context)
        {
            _context = context;
        }
        public void Create(Student student)
        {
            _context.Student.Add(student);
        }

        public void Delete(Student student)
        {
            _context.Student.Remove(student);
        }

        public void Edit(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _context.Student.ToListAsync();
        }

        public async Task<Student> GetById(int id)
        {
            return await _context.Student.Where(x => x.StudentId == id).FirstOrDefaultAsync();
        }

        public async Task<Student> GetByAproximatedName(string toFind)
        {
            return await _context.Student.Where(x => x.Name.Contains(toFind)).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
