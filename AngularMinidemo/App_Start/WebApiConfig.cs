using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using AngularMinidemo.Models;
using System.Web.Http;

namespace AngularMinidemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);                       

            System.Data.Entity.Database.SetInitializer(new CobaSportsContextInitializer());


            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Sport>("Sports");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    
        }
    }
}
