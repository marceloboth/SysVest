using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysVest.DomainModel.Abstracts;
using SysVest.DomainModel.Concrets;
using SysVest.DomainModel.Entities;

namespace SysVest.Test.Repositories
{
    [TestClass]
    public class VestibularRepositoryTest
    {
        private readonly VestContext _context = new VestContext();
        private Vestibular _entidade;
        private IVestibularRepository _repository;

        [TestInitialize]
        public void InicializarTest()
        {
            // Injeção de dependência manual
            _repository = new VestibularRepository(_context);

            _entidade = new Vestibular
                            {
                                InicioInscricao = new DateTime(2012, 12, 28),
                                FimInscricao = new DateTime(2012, 12, 28).AddDays(5),
                                Prova = new DateTime(2012, 12, 28).AddDays(7),
                                Descricao = "Vestibular 2012-1"
                            };
        }

        [TestMethod]
        public void Pode_Consultar_Linq_Usando_Repositorio_Test()
        {
            // Ambiente
            _context.Vestibulares.Add(_entidade);
            _context.SaveChanges();

            // Ação
            var vestibulares = _repository.Vestibulares;
            var retorno = (from v in vestibulares
                           where v.Descricao.Equals(_entidade.Descricao)
                           select v).FirstOrDefault();

            // Assertivas
            Assert.IsInstanceOfType(vestibulares, typeof (IQueryable<Vestibular>));
            Assert.AreEqual(retorno, _entidade);
        }


        [TestMethod]
        public void Pode_Inserir_Test()
        {
            // Ação
            _repository.Inserir(_entidade);
            var retorno = (from a in _repository.Vestibulares
                                  where a.Id.Equals(_entidade.Id)
                                  select a).FirstOrDefault();

            // Assertivas
            Assert.AreEqual(retorno, _entidade);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Nao_Pode_Inserir_Vestibular_Com_Mesma_Descricao_Test()
        {
            // Ambiente
            var vestibularFake = new Vestibular
            {
                InicioInscricao = _entidade.InicioInscricao,
                FimInscricao = _entidade.FimInscricao,
                Prova = _entidade.Prova,
                Descricao = _entidade.Descricao
            };

            _repository.Inserir(_entidade);

            // Ação
            _repository.Inserir(vestibularFake);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Nao_Pode_Inserir_Vestibular_Sem_Data_Test()
        {
            // Ambiente
            var vestibularFake = new Vestibular
            {
                Descricao = "Vestibular 2012-2"
            };

            // Ação
            _repository.Inserir(vestibularFake);
        }

        //[TestMethod]
        //public void Pode_Alterar_Test()
        //{
        //    // Ambiente
        //    string descricaoEsperada = _entidade.Descricao;

        //    _repository.Inserir(_entidade);

        //    Vestibular VestibularAlterar = (from a in _repository.Vestibulares
        //                                    where a.Id == _entidade.Id
        //                                    select a).FirstOrDefault();

        //    VestibularAlterar.Descricao = "Django";


        //    _repository.Alterar(VestibularAlterar);

        //    // Ação
        //    Vestibular retorno = (from a in _repository.Vestibulares
        //                          where a.Id.Equals(_entidade.Id)
        //                          select a).FirstOrDefault();

        //    // Assertivas
        //    Assert.AreEqual(retorno.Id, _entidade.Id);
        //    Assert.AreNotEqual(descricaoEsperada, retorno.Descricao);
        //}


        //[TestMethod]
        //[ExpectedException(typeof (InvalidOperationException))]
        //public void Nao_Pode_Alterar_Vestibular_Com_Mesma_Descricao_Cadastrada_Test()
        //{
        //    // Ambiente
        //    _repository.Inserir(_entidade);

        //    // Usuário que o admin será comparado
        //    var VestibularFake = new Vestibular
        //                             {
        //                                 Descricao = "Ruby on Rails 3"
        //                             };

        //    _repository.Inserir(VestibularFake);

        //    Vestibular VestibularAlterar = (
        //                                       from c in _repository.Vestibulares
        //                                       where c.Id == _entidade.Id
        //                                       select c
        //                                   ).FirstOrDefault();

        //    VestibularAlterar.Descricao = VestibularFake.Descricao;
        //    _repository.Alterar(VestibularAlterar);
        //}


        //[TestMethod]
        //public void Pode_Excluir_Test()
        //{
        //    // Ambiente
        //    _repository.Inserir(_entidade);

        //    // Ação
        //    _repository.Excluir(_entidade.Id);

        //    // Assert
        //    IQueryable<Vestibular> result = from c in _context.Vestibulares
        //                                    where c.Id.Equals(_entidade.Id)
        //                                    select c;
        //    Assert.AreEqual(0, result.Count());
        //}

        //[TestMethod]
        //[ExpectedException(typeof (InvalidOperationException))]
        //public void Nao_Pode_Excluir_Algo_Que_Nao_Exista_Test()
        //{
        //    _repository.Excluir(231);
        //}
        
        [TestCleanup]
        public void LimparCenario()
        {
            var exclusao = from v in _context.Vestibulares select v;

            foreach (var vestibular in exclusao)
            {
                _context.Vestibulares.Remove(vestibular);
            }
            _context.SaveChanges();
        }
    }
}