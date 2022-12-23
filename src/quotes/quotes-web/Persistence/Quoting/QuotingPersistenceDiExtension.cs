using Microsoft.EntityFrameworkCore;

namespace quotes_web.Persistence.Quoting
{
    public static class QuotingPersistenceDiExtension
    {
        public static IServiceCollection AddQuotingPersistence(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddDbContextFactory<QuotesContext>(opt => opt.UseSqlServer(configurationManager.GetConnectionString("quotes")));


            return services;
        }
    }
}
