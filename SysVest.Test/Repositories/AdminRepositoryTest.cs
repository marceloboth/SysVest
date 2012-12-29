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
        public void Pode_Consultar_Linq_Usando_Repositorio_Test()
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
        public void Pode_Inserir_Test()
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
        public void Nao_Pode_Inserir_Admin_Com_Mesmo_Email_Test()
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
        public void Nao_Pode_Inserir_Admin_Com_Mesmo_Login_Test()
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

        [TestMethod]
        public void Pode_Alterar_Test()
        {
            // Ambiente
            var emailEsperado = _entidade.Email;
            var loginEsperado = _entidade.Login;
            var nomeEsperado = _entidade.NomeTratamento;
            var senhaEsperada = _entidade.Senha;

            _adminRepository.Inserir(_entidade);

            var adminAlterar = (from a in _adminRepository.Admins
                                where a.Id == _entidade.Id
                                select a).FirstOrDefault();

            adminAlterar.NomeTratamento = "Marcelos J. Both";
            adminAlterar.Email = "marcelos.both@gmail.com";
            adminAlterar.Login = "marcelos.both";
            adminAlterar.Senha = "123456s";

            _adminRepository.Alterar(adminAlterar);

            // Ação
            var retorno = (from a in _adminRepository.Admins
                           where a.Id.Equals(_entidade.Id)
                           select a).FirstOrDefault();

            // Assertivas
            Assert.AreEqual(retorno.Id, _entidade.Id);
            Assert.AreNotEqual(loginEsperado, retorno.Login);
            Assert.AreNotEqual(emailEsperado, retorno.Email);
            Assert.AreNotEqual(nomeEsperado, retorno.NomeTratamento);
            Assert.AreNotEqual(senhaEsperada, retorno.Senha);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Nao_Pode_Alterar_Admin_Com_Mesmo_Email_Cadastrado_Test()
        {
            // Ambiente
            _adminRepository.Inserir(_entidade);

            // Usuário que o admin será comparado
            var adminContraProva = new Admin
            {
                NomeTratamento = "Marcelo J. Both",
                Email = "marcelo.both@gmail.com",
                Login = "marcelo.both",
                Senha = "123456"
            };

            _adminRepository.Inserir(adminContraProva);

            var adminAlterar = (
                from a in _adminRepository.Admins
                where a.Id == _entidade.Id
                select a
            ).FirstOrDefault();

            adminAlterar.Login = "marcelo.both";
            adminAlterar.Email = adminContraProva.Email;
            adminAlterar.NomeTratamento = "Marcelo J. Both";
            adminAlterar.Senha = "123456";

            _adminRepository.Alterar(adminAlterar);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Nao_Pode_Alterar_Admin_Com_Mesmo_Login_Cadastrado_Test()
        {
            // Ambiente
            _adminRepository.Inserir(_entidade);

            // Usuário que o admin será comparado
            var adminContraProva = new Admin
            {
                Email = "both@gmail.com",
                Login = "both",
                NomeTratamento = "Marcelo Both",
                Senha = "1234"
            };

            _adminRepository.Inserir(adminContraProva);

            var adminAlterar = (
                from a in _adminRepository.Admins
                where a.Id == _entidade.Id
                select a
            ).FirstOrDefault();

            adminAlterar.Login = "both";
            adminAlterar.Email = adminContraProva.Email;
            adminAlterar.NomeTratamento = "Marcelo Both";
            adminAlterar.Senha = "1234";

            _adminRepository.Alterar(adminAlterar);
        }


        [TestMethod]
        public void Pode_Excluir_Test()
        {
            // Ambiente
            _adminRepository.Inserir(_entidade);
            
            // Ação
            _adminRepository.Excluir(_entidade.Id);

            // Assert
            var result = from a in _vestContext.Admins
                         where a.Id.Equals(_entidade.Id)
                         select a;
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Nao_Pode_Excluir_Algo_Que_Nao_Exista_Test()
        {
            _adminRepository.Excluir(231);
        }


        [TestMethod]
        public void Pode_Recuperar_Por_Id_Test()
        {
            // Ambiente
            _adminRepository.Inserir(_entidade);
            
            // Ação
            var result = _adminRepository.Retornar(_entidade.Id);

            // Assertivas
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Admin));
            Assert.AreEqual(_entidade, result);
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