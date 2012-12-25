using System;
using System.Linq;
using SysVest.DomainModel.Abstracts;
using SysVest.DomainModel.Entities;

namespace SysVest.DomainModel.Concrets
{
    public class CursoRepository : ICursoRepository
    {
        private readonly VestContext _context;

        public CursoRepository(VestContext context)
        {
            _context = context;
        }

        #region ICursoRepository Members

        public IQueryable<Curso> Cursos
        {
            get { return _context.Cursos.AsQueryable(); }
        }

        public void Alterar(Curso curso)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public void Inserir(Curso curso)
        {
            throw new NotImplementedException();
        }

        public Curso RetornarPorId(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}