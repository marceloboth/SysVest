using System.Linq;
using SysVest.DomainModel.Entities;

namespace SysVest.DomainModel.Abstracts
{
    public interface IAdminRepository
    {
        IQueryable<Admin> Admins { get; }

        void Alterar(Admin admin);

        void Excluir(int id);

        void Inserir(Admin admin);

        Admin Retornar(int id);
    }
}