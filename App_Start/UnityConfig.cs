using MeterReaderCMS.Repositories.Implementation;
using MeterReaderCMS.Repositories.Interfaces;
using System.Web.Http;
using System.Web.Mvc;
using Unity;

namespace MeterReaderCMS
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<IMeterReaderRepository, MeterReaderRepository>();            
            container.RegisterType<IDashboardRepository, DashboardRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<INotebookRepository, NotebookRepository>();
            container.RegisterType<ISearchRepository, SearchRepository>();
            container.RegisterType<ITrackRepository, TrackRepository>();
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}