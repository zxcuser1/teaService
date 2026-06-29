using System.ComponentModel.DataAnnotations.Schema;
using Business.Data.BaseEntities;

namespace Business.Data.Models
{
    public class RefreshToken : BaseEntity
    {
        public bool IsRevoked {get; set;} = false;
        public string TokenHash {get; set;} = string.Empty;
        public Guid? PreviousTokenId {get; set;} = null;
        [ForeignKey(nameof(PreviousTokenId))]
        public RefreshToken? PreviousToken { get; set; }
        public DateTime? RevokedAt {get; set;} = null;
        public DateTime ExpiresAt {get; set;}
        [ForeignKey(nameof(UserSessionId))]
        public UserSession UserSession {get; set;}
        public Guid UserSessionId {get; set;}
    }
}