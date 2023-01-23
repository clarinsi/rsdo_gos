namespace Gos.Core.Entities
{
    public abstract class Translation<T>
        where T : Translation<T>, new()
    {
        public string CultureName { get; set; }

        public int Id { get; set; }
    }
}