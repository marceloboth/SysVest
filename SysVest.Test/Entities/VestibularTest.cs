using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysVest.DomainModel.Entities;

namespace SysVest.Test.Entities
{
    [TestClass]
    public class VestibularTest
    {
        public Vestibular vestibular1, vestibular2;

        [TestInitialize]
        public void InicializarTest()
        {
            vestibular1 = new Vestibular
            {
                Id = 1,
                Descricao = "Descricao1",
                FimInscricao = new DateTime(2012, 1, 2),
                InicioInscricao = new DateTime(2011, 12, 12),
                Prova = new DateTime(2012, 3, 1)
            };
        }

        [TestMethod]
        public void Garantir_que_2_vestibulares_sao_iguais_quando_tem_o_mesmo_Id()
        {
            vestibular2 = new Vestibular
            {
                Id = 1,
                Descricao = "Descricao2",
                FimInscricao = new DateTime(2012, 2, 2),
                InicioInscricao = new DateTime(2011, 11, 12),
                Prova = new DateTime(2012, 3, 2)
            };

            Assert.AreEqual(vestibular1.Id, vestibular2.Id);
            Assert.AreEqual(vestibular1, vestibular2);
        }

        [TestMethod]
        public void Garantir_que_2_vestibulares_sao_iguais_quando_tem_o_mesmo_Descricao()
        {
            vestibular2 = new Vestibular
            {
                Id = 2,
                Descricao = "Descricao1",
                FimInscricao = new DateTime(2012, 1, 1),
                InicioInscricao = new DateTime(2011, 12, 1),
                Prova = new DateTime(2012, 3, 2)
            };

            Assert.AreEqual(vestibular1.Descricao, vestibular2.Descricao);
            Assert.AreEqual(vestibular1, vestibular2);
        }
    }
}
