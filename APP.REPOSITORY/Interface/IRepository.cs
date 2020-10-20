using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP.REPOSITORY.Interface
{
    public interface IRepository<T> where T : class
    {
        //Repositorio Generico     
        //CRUD

        //C - Create
        Task Add(T entity);

        //R - READ
        Task<IEnumerable<T>> GetAllAsync();

        //R - READ By ID
        Task<T> GetByIdAsync(string id);

        //U -UPDATE
        void Udpate(T entity);

        // D -DELETE
        void Delete(T entity);

        Task<bool> SaveChangesAsync();

        Task<IEnumerable<T>> FindByEmailAsync(string email);
    }
}