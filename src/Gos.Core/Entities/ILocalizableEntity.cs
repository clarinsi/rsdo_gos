namespace Gos.Core.Entities
{
    public interface ILocalizableEntity<TTranslation>
        where TTranslation : Translation<TTranslation>, new()
    {
        TranslationCollection<TTranslation> Translations { get; set; }
    }
}