using quotes_web.Domain.Quoting.Author;
using quotes_web.Domain.Quoting.Quote;
using quotes_web.Persistence.Quoting;

namespace quotes_web.Domain.Quoting
{
    public static class QuotingDiExtension
    {
        public static IServiceCollection AddQuoting(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddQuotingPersistence(configurationManager);
            services.AddTransient<IQuoteService, QuoteService>();
            services.AddTransient<IQuoteReadOnlyService, QuoteReadOnlyService>();

            services.AddTransient<IAuthorReadOnlyService, AuthorReadOnlyService>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IImageFileService, ImageFileService>();

            return services;
        }
    }
}
