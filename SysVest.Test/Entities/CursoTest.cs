using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysVest.DomainModel.Entities;

namespace SysVest.Test.Entities
{
    [TestClass]
    public class CursoTest
    {
        public Curso curso1, curso2;

        [TestInitialize]
        public void InicializarTest()
        {
            curso1 = new Curso
                         {
                             Id = 1,
                             Descricao = "Descricao1",
                             Vagas = 50
                         };
        }

        [TestMethod]
        public void Garantir_que_2_cursos_sao_iguais_quando_tem_o_mesmo_Id()
        {
            curso2 = new Curso
            {
                Id = 1,
                Descricao = "Descricao2",
                Vagas = 34
            };

            Assert.AreEqual(curso1.Id, curso2.Id);
            Assert.AreEqual(curso1, curso2);
        }

        [TestMethod]
        public void Garantir_que_2_cursos_sao_iguais_quando_tem_o_mesmo_Descricao()
        {
            curso2 = new Curso
            {
                Id = 2,
                Descricao = "Descricao1",
                Vagas = 34
            };

            Assert.AreEqual(curso1.Descricao, curso2.Descricao);
            Assert.AreEqual(curso1, curso2);
        }
    }
}
