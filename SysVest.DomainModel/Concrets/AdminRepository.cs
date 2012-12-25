using System;
using System.Linq;
using SysVest.DomainModel.Abstracts;
using SysVest.DomainModel.Entities;

namespace SysVest.DomainModel.Concrets
{
    public class AdminRepository : IAdminRepository
    {
        private readonly VestContext _context;

        public AdminRepository(VestContext context)
        {
            _context = context;
        }

        public IQueryable<Admin> Admins
        {
            get 
            { 
                return _context.Admins.AsQueryable();
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

            _context.SaveChanges();
        }

        public void Excluir(int id)
        {
            var validacao = from a in Admins
                            where a.Id.Equals(id)
                            select a;
            if (!validacao.Any())
                throw new InvalidOperationException("Administrador não encontrado no repositório");
            
            _context.Admins.Remove(validacao.FirstOrDefault());
            _context.SaveChanges();
        }

        public void Inserir(Admin admin)
        {
            var validacao = from a in Admins
                            where a.Login.ToUpper().Equals(admin.Login) || a.Email.ToUpper().Equals(admin.Email)
                            select a;

            if (validacao.Any())
                throw new InvalidOperationException("Login ou e-mail informado já estão vinculados a algum cadastro");

            _context.Admins.Add(admin);
            _context.SaveChanges();
        }

        public Admin Retornar(int id)
        {
            return Admins.FirstOrDefault(a => a.Id.Equals(id));
        }
    }
}