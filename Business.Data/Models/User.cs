using System.ComponentModel.DataAnnotations.Schema;
using Business.Data.BaseEntities;

namespace Business.Data.Models
{
    public class User : BaseEntity
    {
        public string NickName {get; set;} = string.Empty;
        public string Email {get; set;} = string.Empty;
        public string Password {get; set;} = string.Empty;
        public bool IsVerified {get; set;} = false;
        public bool IsBanned {get; set;} = false;

        [ForeignKey(nameof(RoleId))]
        public Role Role {get; set;}
        public Guid RoleId {get; set;}
    }
}
