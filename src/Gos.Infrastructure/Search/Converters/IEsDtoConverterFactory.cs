namespace Gos.Infrastructure.Search.Converters
{
    public interface IEsDtoConverterFactory
    {
        IEsDtoConverter<TEntity, TDto> GetConverter<TEntity, TDto>();
    }
}