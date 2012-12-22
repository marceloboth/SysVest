using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysVest.DomainModel.Entities
{
    [Table("Candidatos")]
    public class Candidato
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public DateTime Nascimento { get; set; }

        public string Senha { get; set; }

        public string Sexo { get; set; }

        public string Cpf { get; set; }

        public virtual Vestibular Vestibular { get; set; }

        public virtual Curso Curso { get; set; }

        public bool Aprovado { get; set; }

        public bool Ativo { get; set; }


        public override bool Equals(object obj)
        {
            var param = (Candidato) obj;
            if (Id == param.Id || Cpf == param.Cpf || Email == param.Email)
                return true;

            return false;
        }
    }
}