using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysVest.DomainModel.Entities
{
    [Table("Cursos")]
    public class Curso
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public int Vagas { get; set; }

        public virtual ICollection<Candidato> Candidatos { get; set; }

        public override bool Equals(object obj)
        {
            var param = (Curso) obj;
            if (Id == param.Id || Descricao == param.Descricao)
                return true;

            return false;
        }
    }
}