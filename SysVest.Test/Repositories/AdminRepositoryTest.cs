using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysVest.DomainModel.Abstracts;
using SysVest.DomainModel.Concrets;
using SysVest.DomainModel.Entities;

namespace SysVest.Test.Repositories
{
    [TestClass]
    public class AdminRepositoryTest
    {
        private readonly VestContext _vestContext = new VestContext();
        private IAdminRepository _adminRepository;
        private Admin _entidade;

        [TestInitialize]
        public void InicializarTest()
        {
            // Injeção de dependência manual
            _adminRepository = new AdminRepository(_vestContext);

            _entidade = new Admin
            {
                NomeTratamento = "Marcelo J. Both",
                Email = "marcelo.both@gmail.com",
                Login = "marcelo.both",
                Senha = "123456"
            };
        }

        [TestMethod]
        public void PodeConsultarLinqUsandoRepositorioTest()
        {
            // Ambiente
            _vestContext.Admins.Add(_entidade);
            _vestContext.SaveChanges();

            // Ação
            var admins = _adminRepository.Admins;
            var retorno = (from a in admins
                           where a.Login.Equals(_entidade.Login)
                           select a).FirstOrDefault();

            // Assertivas
            Assert.IsInstanceOfType(admins, typeof(IQueryable<Admin>));
            Assert.AreEqual(retorno, _entidade);
        }


        [TestMethod]
        public void PodeInserirTest()
        {
            // Ação
            _adminRepository.Inserir(_entidade);
            var retorno = (from a in _adminRepository.Admins
                           where a.Login.Equals(_entidade.Login)
                           select a).FirstOrDefault();

            // Assertivas
            Assert.AreEqual(retorno, _entidade);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoPodeInserirAdminComMesmoEmailTest()
        {
            // Ambiente
            var contraProva = new Admin
                                  {
                                      NomeTratamento = "Contra Prova",
                                      Email = _entidade.Email,
                                      Login = "contra.prova",
                                      Senha = "1234"
                                  };

            _adminRepository.Inserir(_entidade);

            // Ação
            _adminRepository.Inserir(contraProva);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoPodeInserirAdminComMesmoLoginTest()
        {
            // Ambiente
            var contraProva = new Admin
            {
                NomeTratamento = "Contra Prova",
                Email = "contra.prova@gmail.com",
                Login = _entidade.Login,
                Senha = "1234"
            };

            _adminRepository.Inserir(_entidade);

            // Ação
            _adminRepository.Inserir(contraProva);
        }


        [TestCleanup]
        public void LimparCenario()
        {
            var exclusao = from a in _vestContext.Admins
                           select a;

            foreach (var admin in exclusao)
            {
                _vestContext.Admins.Remove(admin);
            }
            _vestContext.SaveChanges();
        }
    }
}