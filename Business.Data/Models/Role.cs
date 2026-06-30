using Business.Data.BaseEntities;
using Business.Data.Enums;

namespace Business.Data.Models
{
    public class Role : BaseEntity
    {
        public Roles UserRole {get; set;}
    }
}