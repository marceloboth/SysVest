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
            var validacao = from c in Cursos
                            where (c.Descricao.ToUpper().Equals(curso.Descricao) && (!c.Id.Equals(curso.Id)))
                            select c;
            if (validacao.Any())
            {
                throw new InvalidOperationException("Já existe um curso cadastrado com essa descrição");
            }

            _context.SaveChanges();
        }

        public void Excluir(int id)
        {
            var validacao = from c in Cursos
                            where c.Id.Equals(id)
                            select c;
            if (!validacao.Any())
                throw new InvalidOperationException("Curso não encontrado no repositório");

            _context.Cursos.Remove(validacao.FirstOrDefault());
            _context.SaveChanges();
        }

        public void Inserir(Curso curso)
        {
            var validacao = from c in Cursos
                            where c.Descricao.ToUpper().Equals(curso.Descricao)
                            select c;

            if (validacao.Any())
                throw new InvalidOperationException("Já existe um curso cadastrado com essa descrição");

            _context.Cursos.Add(curso);
            _context.SaveChanges();
        }

        public Curso RetornarPorId(int id)
        {
            return Cursos.FirstOrDefault(a => a.Id.Equals(id));
        }

        #endregion
    }
}