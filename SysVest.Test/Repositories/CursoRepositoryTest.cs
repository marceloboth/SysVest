using System;
using System.Collections.ObjectModel;
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
        public void Pode_consultar_Linq_usando_repositorio_Test()
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
        public void Pode_inserir_Test()
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
        public void Nao_pode_inserir_curso_com_mesma_descricao_Test()
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
        public void Pode_alterar_Test()
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


        //[TestMethod]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void NaoPodeAlterarAdminComMesmoEmailCadastradoTest()
        //{
        //    // Ambiente
        //    _repository.Inserir(_entidade);

        //    // Usuário que o admin será comparado
        //    var adminContraProva = new Admin
        //    {
        //        NomeTratamento = "Marcelo J. Both",
        //        Email = "marcelo.both@gmail.com",
        //        Login = "marcelo.both",
        //        Senha = "123456"
        //    };

        //    _repository.Inserir(adminContraProva);

        //    var adminAlterar = (
        //        from a in _repository.Admins
        //        where a.Id == _entidade.Id
        //        select a
        //    ).FirstOrDefault();

        //    adminAlterar.Login = "marcelo.both";
        //    adminAlterar.Email = adminContraProva.Email;
        //    adminAlterar.NomeTratamento = "Marcelo J. Both";
        //    adminAlterar.Senha = "123456";

        //    _repository.Alterar(adminAlterar);
        //}


        //[TestMethod]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void NaoPodeAlterarAdminComMesmoLoginCadastradoTest()
        //{
        //    // Ambiente
        //    _repository.Inserir(_entidade);

        //    // Usuário que o admin será comparado
        //    var adminContraProva = new Admin
        //    {
        //        Email = "both@gmail.com",
        //        Login = "both",
        //        NomeTratamento = "Marcelo Both",
        //        Senha = "1234"
        //    };

        //    _repository.Inserir(adminContraProva);

        //    var adminAlterar = (
        //        from a in _repository.Admins
        //        where a.Id == _entidade.Id
        //        select a
        //    ).FirstOrDefault();

        //    adminAlterar.Login = "both";
        //    adminAlterar.Email = adminContraProva.Email;
        //    adminAlterar.NomeTratamento = "Marcelo Both";
        //    adminAlterar.Senha = "1234";

        //    _repository.Alterar(adminAlterar);
        //}


        //[TestMethod]
        //public void PodeExcluirTest()
        //{
        //    // Ambiente
        //    _repository.Inserir(_entidade);

        //    // Ação
        //    _repository.Excluir(_entidade.Id);

        //    // Assert
        //    var result = from a in _context.Admins
        //                 where a.Id.Equals(_entidade.Id)
        //                 select a;
        //    Assert.AreEqual(0, result.Count());
        //}

        //[TestMethod]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void NaoPodeExcluirAlgoQueNaoExistaTest()
        //{
        //    _repository.Excluir(231);
        //}


        //[TestMethod]
        //public void PodeRecuperarPorIdTest()
        //{
        //    // Ambiente
        //    _repository.Inserir(_entidade);

        //    // Ação
        //    var result = _repository.Retornar(_entidade.Id);

        //    // Assertivas
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(Admin));
        //    Assert.AreEqual(_entidade, result);
        //}


        //[TestCleanup]
        //public void LimparCenario()
        //{
        //    var exclusao = from a in _context.Admins
        //                   select a;

        //    foreach (var admin in exclusao)
        //    {
        //        _context.Admins.Remove(admin);
        //    }
        //    _context.SaveChanges();
        //}
    }
}
