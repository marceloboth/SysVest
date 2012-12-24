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
            var validacao = from a in Admins
                            where (a.Login.ToUpper().Equals(admin.Login) || a.Email.ToUpper().Equals(admin.Email))
                            && (!a.Id.Equals(admin.Id))
                            select a;
            if (validacao.Any())
            {
                throw new InvalidOperationException("Administrador já cadastrado com esse login");
            }

            _vestContext.SaveChanges();
        }

        public void Excluir(int id)
        {
            var validacao = from a in Admins
                            where a.Id.Equals(id)
                            select a;
            if (!validacao.Any())
                throw new InvalidOperationException("Administrador não encontrado no repositório");
            
            _vestContext.Admins.Remove(validacao.FirstOrDefault());
            _vestContext.SaveChanges();
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
            return Admins.FirstOrDefault(a => a.Id.Equals(id));
        }
    }
}