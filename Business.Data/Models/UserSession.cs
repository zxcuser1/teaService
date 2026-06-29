using System.ComponentModel.DataAnnotations.Schema;
using Business.Data.BaseEntities;

namespace Business.Data.Models
{
    public class UserSession : BaseEntity
    {
        public string DeviceId {get; set;} = string.Empty;
        public DateTime? ClosedAt {get; set;} = null;
        public Guid? CurrentRefreshTokenId {get; set;}
        [ForeignKey(nameof(CurrentRefreshTokenId))]
        public RefreshToken? CurrentRefreshToken {get; set;}
        [ForeignKey(nameof(UserId))]
        public User User {get; set;}
        public Guid UserId {get; set;}
    }
}