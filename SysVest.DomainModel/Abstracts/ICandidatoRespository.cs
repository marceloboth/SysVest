using System.Collections.Generic;
using System.Linq;
using SysVest.DomainModel.Entities;

namespace SysVest.DomainModel.Abstracts
{
    public interface ICandidatoRespository
    {
        IQueryable<Candidato> Candidatos { get; }

        void RealizarInscricao(Candidato candidato);

        void AtualizarCadastro(Candidato candidato);

        void ExcluirCadastro(int id);

        void Aprovar(int id);

        IList<Candidato> RetornarTodos();

        IList<Candidato> RetornarCandidatosPorVestibularPorCurso(int idVestibular, int idCurso);
    }
}