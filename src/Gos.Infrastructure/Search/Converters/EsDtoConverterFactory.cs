using Autofac;

namespace Gos.Infrastructure.Search.Converters
{
    public class EsDtoConverterFactory : IEsDtoConverterFactory
    {
        private readonly ILifetimeScope lifetimeScope;

        public EsDtoConverterFactory(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public IEsDtoConverter<TEntity, TDto> GetConverter<TEntity, TDto>()
        {
            return lifetimeScope.Resolve<IEsDtoConverter<TEntity, TDto>>();
        }
    }
}