using System;
using System.Linq;
using SysVest.DomainModel.Abstracts;
using SysVest.DomainModel.Entities;

namespace SysVest.DomainModel.Concrets
{
    public class VestibularRepository : IVestibularRepository
    {
        private readonly VestContext _context;

        public VestibularRepository(VestContext context)
        {
            _context = context;
        }
        

        public IQueryable<Vestibular> Vestibulares
        {
            get { return _context.Vestibulares.AsQueryable(); }
        }

        public void Alterar(Vestibular vestibular)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public void Inserir(Vestibular vestibular)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IList<Candidato> RetornarCandidatosPorVestibular(int idVestibular)
        {
            throw new NotImplementedException();
        }
    }
}