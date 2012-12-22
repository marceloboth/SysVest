using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysVest.DomainModel.Entities;

namespace SysVest.Test.Entities
{
    [TestClass]
    public class CandidatoTest
    {
        public Candidato candidato1, candidato2;

        [TestInitialize]
        public void InicializarTest()
        {
            candidato1 = new Candidato
                             {
                                 Id = 1,
                                 Nome = "Candidato 1",
                                 Cpf = "12345678910",
                                 Nascimento = new DateTime(1990,1,1),
                                 Email = "candidato1@email.com",
                                 Aprovado = true
                             };
        }

        [TestMethod]
        public void Garantir_que_2_candidatos_sao_iguais_quando_tem_o_mesmo_Id()
        {
            candidato2 = new Candidato
            {
                Id = 1,
                Nome = "Candidato 2",
                Cpf = "11345678910",
                Nascimento = new DateTime(1990, 3, 1),
                Email = "candidato2@email.com",
                Aprovado = false
            };

            Assert.AreEqual(candidato1.Id, candidato2.Id);
            Assert.AreEqual(candidato1, candidato2);
        }

        [TestMethod]
        public void Garantir_que_2_candidatos_sao_iguais_quando_tem_o_mesmo_Cpf()
        {
            candidato2 = new Candidato
            {
                Id = 2,
                Nome = "Candidato 2",
                Cpf = "12345678910",
                Nascimento = new DateTime(1990, 3, 1),
                Email = "candidato2@email.com",
                Aprovado = false
            };

            Assert.AreEqual(candidato1.Cpf, candidato2.Cpf);
            Assert.AreEqual(candidato1, candidato2);
        }

        [TestMethod]
        public void Garantir_que_2_candidatos_sao_iguais_quando_tem_o_mesmo_Email()
        {
            candidato2 = new Candidato
            {
                Id = 3,
                Nome = "Candidato 2",
                Cpf = "11345678910",
                Nascimento = new DateTime(1990, 3, 1),
                Email = "candidato1@email.com",
                Aprovado = false
            };

            Assert.AreEqual(candidato1.Email, candidato2.Email);
            Assert.AreEqual(candidato1, candidato2);
        }
    }
}
