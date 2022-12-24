namespace quotes_web.Domain.ImportExport
{
    public static class ImportExportDiExtension
    {
        public static IServiceCollection AddImportExport(this IServiceCollection services)
        {
            services.AddTransient<IImportExportService, ImportExportService>();

            return services;
        }
    }
}
