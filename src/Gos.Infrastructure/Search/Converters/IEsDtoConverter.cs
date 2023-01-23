using System.Threading.Tasks;

namespace Gos.Infrastructure.Search.Converters
{
    public interface IEsDtoConverter<TEntity, TDto> : IEsDtoConverter
    {
        Task<TDto> Convert(TEntity entity);
    }

    public interface IEsDtoConverter
    {
    }
}