using System.Collections.Generic;
using System.Linq;
using SysVest.DomainModel.Entities;

namespace SysVest.DomainModel.Abstracts
{
    public interface IVestibularRepository
    {
        IQueryable<Vestibular> Vestibulares { get; }

        void Alterar(Vestibular vestibular);

        void Excluir(int id);

        void Inserir(Vestibular vestibular);

        IList<Candidato> RetornarCandidatosPorVestibular(int idVestibular);
    }
}