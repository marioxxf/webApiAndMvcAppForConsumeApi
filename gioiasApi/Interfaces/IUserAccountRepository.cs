﻿using gioiasApi.Models;

namespace gioiasApi.Interfaces
{
    public interface IUserAccountRepository
    {
        void Create(UserAccount userAccount);
        void Edit(UserAccount userAccount);
        void Delete(UserAccount userAccount);

        Task<UserAccount> GetById(int id);
        Task<UserAccount> GetByAproximatedName(string toFind);

        Task<IEnumerable<UserAccount>> GetAll();
        Task<bool> SaveAllAsync();
    }
}
