using gioiasApi.Models;

namespace gioiasApi.Interfaces
{
    public interface IStudentRepository
    {
        void Create(Student student);
        void Edit(Student student);
        void Delete(Student student);

        Task<Student> GetById(int id);
        Task<Student> GetByAproximatedName(string toFind);

        Task<IEnumerable<Student>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
