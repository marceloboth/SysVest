using System.Linq;
using SysVest.DomainModel.Entities;

namespace SysVest.DomainModel.Abstracts
{
    public interface ICursoRepository
    {
        IQueryable<Curso> Cursos { get; }

        void Alterar(Curso curso);

        void Excluir(int id);

        void Inserir(Curso curso);

        Curso RetornarPorId(int id);
    }
}