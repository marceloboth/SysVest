using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysVest.DomainModel.Abstracts;
using SysVest.DomainModel.Concrets;
using SysVest.DomainModel.Entities;

namespace SysVest.Test.Repositories
{
    [TestClass]
    public class CursoRepositoryTest
    {
        private readonly VestContext _context = new VestContext();
        private ICursoRepository _repository;
        private Curso _entidade;

        [TestInitialize]
        public void InicializarTest()
        {
            // Injeção de dependência manual
            _repository = new CursoRepository(_context);

            _entidade = new Curso
            {
                Descricao = "Asp.net mvc",
                Vagas = 2
            };
        }

        [TestMethod]
        public void Pode_Consultar_Linq_Usando_Repositorio_Test()
        {
            // Ambiente
            _context.Cursos.Add(_entidade);
            _context.SaveChanges();

            // Ação
            var cursos = _repository.Cursos;
            var retorno = (from a in cursos
                           where a.Descricao.Equals(_entidade.Descricao)
                           select a).FirstOrDefault();

            // Assertivas
            Assert.IsInstanceOfType(cursos, typeof(IQueryable<Curso>));
            Assert.AreEqual(retorno, _entidade);
        }


        [TestMethod]
        public void Pode_Inserir_Test()
        {
            // Ação
            _repository.Inserir(_entidade);
            var retorno = (from a in _repository.Cursos
                           where a.Id.Equals(_entidade.Id)
                           select a).FirstOrDefault();

            // Assertivas
            Assert.AreEqual(retorno, _entidade);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Nao_Pode_Inserir_Curso_Com_Mesma_Descricao_Test()
        {
            // Ambiente
            var contraProva = new Curso
            {
                Descricao = "Asp.net mvc"
            };

            _repository.Inserir(_entidade);

            // Ação
            _repository.Inserir(contraProva);
        }

        [TestMethod]
        public void Pode_Alterar_Test()
        {
            // Ambiente
            var descricaoEsperada = _entidade.Descricao;

            _repository.Inserir(_entidade);

            var cursoAlterar = (from a in _repository.Cursos
                                where a.Id == _entidade.Id
                                select a).FirstOrDefault();

            cursoAlterar.Descricao = "Django";
           

            _repository.Alterar(cursoAlterar);

            // Ação
            var retorno = (from a in _repository.Cursos
                           where a.Id.Equals(_entidade.Id)
                           select a).FirstOrDefault();

            // Assertivas
            Assert.AreEqual(retorno.Id, _entidade.Id);
            Assert.AreNotEqual(descricaoEsperada, retorno.Descricao);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Nao_Pode_Alterar_Curso_Com_Mesma_Descricao_Cadastrada_Test()
        {
            // Ambiente
            _repository.Inserir(_entidade);

            // Usuário que o admin será comparado
            var cursoFake = new Curso()
            {
                Descricao = "Ruby on Rails 3"
            };

            _repository.Inserir(cursoFake);

            var cursoAlterar = (
                from c in _repository.Cursos
                where c.Id == _entidade.Id
                select c
            ).FirstOrDefault();
            
            cursoAlterar.Descricao = cursoFake.Descricao;
            _repository.Alterar(cursoAlterar);
        }


        [TestMethod]
        public void Pode_Excluir_Test()
        {
            // Ambiente
            _repository.Inserir(_entidade);

            // Ação
            _repository.Excluir(_entidade.Id);

            // Assert
            var result = from c in _context.Cursos
                         where c.Id.Equals(_entidade.Id)
                         select c;
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Nao_Pode_Excluir_Algo_Que_Nao_Exista_Test()
        {
            _repository.Excluir(231);
        }


        [TestMethod]
        public void Pode_Recuperar_Por_Id_Test()
        {
            // Ambiente
            _repository.Inserir(_entidade);

            // Ação
            var result = _repository.RetornarPorId(_entidade.Id);

            // Assertivas
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Curso));
            Assert.AreEqual(_entidade, result);
        }


        [TestCleanup]
        public void LimparCenario()
        {
            var exclusao = from c in _context.Cursos
                           select c;

            foreach (var curso in exclusao)
            {
                _context.Cursos.Remove(curso);
            }
            _context.SaveChanges();
        }
    }
}
