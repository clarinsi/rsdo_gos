using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace Gos.Core.Entities
{
    public class TranslationCollection<T> : Collection<T>
        where T : Translation<T>, new()
    {
        public T this[CultureInfo culture]
        {
            get
            {
                var translation = this.FirstOrDefault(t => t.CultureName == culture.Name);
                if (translation == null)
                {
                    translation = new T();
                    translation.CultureName = culture.Name;
                    Add(translation);
                }

                return translation;
            }
            set
            {
                var translation = this.FirstOrDefault(t => t.CultureName == culture.Name);
                if (translation != null)
                {
                    Remove(translation);
                }

                Add(value);
            }
        }

        public bool HasCulture(CultureInfo culture)
        {
            return this.Any(t => t.CultureName == culture.Name);
        }
    }
}