namespace MyMedCalendar.Repositories
{
    /// <summary>
    /// Extension methods for setting up repository dependencies in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class RepositoriesDIExtensions
    {
        /// <summary>
        /// Adds scoped service registrations for repository interfaces to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance so that additional calls can be chained.</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Registers UnitOfWork as a scoped service
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Registers repositories as scoped services
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IDrugRepository, DrugRepository>();
            services.AddScoped<IMedicationScheduleRepository, MedicationScheduleRepository>();

            return services;
        }
    }
}
