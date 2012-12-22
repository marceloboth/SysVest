using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysVest.DomainModel.Entities
{
    [Table("Admins")]
    public class Admin
    {
       // [Key]
        public int Id { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        public string NomeTratamento { get; set; }

        public string Email { get; set; }

        public override bool Equals(object obj)
        {
            var param = (Admin) obj;
            if (Id == param.Id || Login == param.Login || Email == param.Email)
                return true;

            return false;
        }
    }
}