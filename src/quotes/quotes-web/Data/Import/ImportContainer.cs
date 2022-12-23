using quotes_web.Persistence.Quoting;

namespace quotes_web.Data.Import
{
    public class ImportContainer
    {
        public List<Author>? Authors { get; set; }

        public List<Quote>? Quotes { get; set; }
    }
}
