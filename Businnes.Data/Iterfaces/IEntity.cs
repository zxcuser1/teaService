namespace Businnes.Data.Iterfaces
{
     public interface IEntity
    {
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public void UpdateBeforeSave(DateTime now);
    }
}