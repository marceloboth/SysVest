using System.Data.Entity;
using SysVest.DomainModel.Entities;

namespace SysVest.DomainModel.Concrets
{
    public class VestContext: DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Vestibular> Vestibulares { get; set; }
    }
}