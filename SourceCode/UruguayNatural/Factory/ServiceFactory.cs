using BusinessLogic;
using Microsoft.Extensions.DependencyInjection;
using BusinessLogicInterface;
using DataAccess;
using DataAccessInterface;
using Context;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Factory
{
    public class ServiceFactory
    {
        private readonly IServiceCollection Services;

        public ServiceFactory(IServiceCollection services)
        {
            this.Services = services;
        }
        
        public void AddDbContextService()
        {
            Services.AddDbContext<DbContext, UruguayNaturalContext>(); 
        }

        public void AddCustomerServices()
        {
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IAdministratorLogic, AdministratorLogic>();
            Services.AddScoped<ISessionLogic, SessionLogic>();
            Services.AddScoped<ITouristSpotLogic, TouristSpotLogic>();
            Services.AddScoped<ILodgingLogic, LodgingLogic>();
            Services.AddScoped<IBookingLogic, BookingLogic>();
            Services.AddScoped<IBookingStateLogic, BookingStateLogic>();
            Services.AddScoped<IReportLogic, ReportLogic>();
            Services.AddScoped<ILodgingReviewLogic, LodgingReviewLogic>();
            Services.AddScoped<IImportLogic, ImportLogic>();
            Services.AddScoped<IRegionLogic, RegionLogic>();
            Services.AddScoped<ICategoryLogic, CategoryLogic>();
        }
    }
}
