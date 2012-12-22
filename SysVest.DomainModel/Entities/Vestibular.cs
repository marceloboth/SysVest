using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysVest.DomainModel.Entities
{
    [Table("Vestibulares")]
    public class Vestibular
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public DateTime InicioInscricao { get; set; }

        public DateTime FimInscricao { get; set; }

        public DateTime Prova { get; set; }

        public virtual ICollection<Candidato> Candidatos { get; set; }

        public override bool Equals(object obj)
        {
            var param = (Vestibular) obj;
            if (Id == param.Id || Descricao == param.Descricao)
                return true;

            return false;
        }
    }
}