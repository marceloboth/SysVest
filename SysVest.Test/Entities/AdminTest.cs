using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysVest.DomainModel.Entities;

namespace SysVest.Test.Entities
{
    [TestClass]
    public class AdminTest
    {
        public Admin admin1, admin2;

        [TestInitialize]
        public void InicializarTest()
        {
            admin1 = new Admin
                         {
                             Id = 1,
                             Email = "joao@gmail.com",
                             Login = "joao",
                             NomeTratamento = "Joaozinho",
                             Senha = "123456"
                         };
        }

        [TestMethod]
        public void Garantir_que_2_admins_sao_iguais_quando_tem_o_mesmo_Id()
        {
            admin2 = new Admin
                         {
                             Id = 1,
                             Email = "marcelo@gmail.com",
                             Login = "Marcelo",
                             NomeTratamento = "Marcelinho",
                             Senha = "123456"
                         };

            Assert.AreEqual(admin1.Id, admin2.Id);
            Assert.AreEqual(admin1, admin2);
        }

        [TestMethod]
        public void Garantir_que_2_admins_sao_iguais_quando_tem_o_mesmo_Login()
        {
            admin2 = new Admin
            {
                Id = 2,
                Email = "joao@gmail.com",
                Login = "joao",
                NomeTratamento = "Joao",
                Senha = "123453"
            };

            Assert.AreEqual(admin1.Login, admin2.Login);
            Assert.AreEqual(admin1, admin2);
        }

        [TestMethod]
        public void Garantir_que_2_admins_sao_iguais_quando_tem_o_mesmo_Email()
        {
            admin2 = new Admin
            {
                Id = 2,
                Email = "joao@gmail.com",
                Login = "joao",
                NomeTratamento = "Joao",
                Senha = "123453"
            };

            Assert.AreEqual(admin1.Email, admin2.Email);
            Assert.AreEqual(admin1, admin2);
        }
    }
}