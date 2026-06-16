using System.ComponentModel;
using System.Text.Json.Serialization;
using Business.Data.Interfaces;

namespace Business.Data.BaseEntities
{
    public class Entity : IEntity
    {
        public Entity()
        {
            DateCreate = DateTime.UtcNow;
            DateUpdate = DateCreate;
        }

        [ReadOnly(true)]
        [JsonIgnore]
        public DateTime DateCreate { get; set; }

        [ReadOnly(true)]
        [JsonIgnore]
        public DateTime DateUpdate { get; set; }

        public void UpdateBeforeSave(DateTime now)
        {
            DateUpdate = now;
        }
    }
}
