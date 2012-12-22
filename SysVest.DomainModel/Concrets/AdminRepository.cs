using System;
using System.Linq;
using SysVest.DomainModel.Abstracts;
using SysVest.DomainModel.Entities;

namespace SysVest.DomainModel.Concrets
{
    public class AdminRepository : IAdminRepository
    {
        private readonly VestContext _vestContext;

        public AdminRepository(VestContext context)
        {
            _vestContext = context;
        }

        public IQueryable<Admin> Admins
        {
            get 
            { 
                return _vestContext.Admins.AsQueryable();
            }
        }

        public void Alterar(Admin admin)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public void Inserir(Admin admin)
        {
            var validacao = from a in Admins
                            where a.Login.ToUpper().Equals(admin.Login) || a.Email.ToUpper().Equals(admin.Email)
                            select a;

            if (validacao.Any())
                throw new InvalidOperationException("Login ou e-mail informado já estão vinculados a algum cadastro");

            _vestContext.Admins.Add(admin);
            _vestContext.SaveChanges();
        }

        public Admin Retornar(int id)
        {
            throw new NotImplementedException();
        }
    }
}