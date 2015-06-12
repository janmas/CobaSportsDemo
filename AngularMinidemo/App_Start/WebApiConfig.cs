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

            ActionConfiguration changeCaption = builder.Entity<Sport>().TransientAction("ChangeCaption");
            changeCaption.Parameter<string>("Caption");
            changeCaption.Returns<string>();

            changeCaption.HasActionLink(ctx => {
                var sport = ctx.EntityInstance as Sport;
                if (sport.Id % 2 == 0)
                {
                    return new System.Uri(ctx.Url.CreateODataLink(
                        new System.Web.Http.OData.Routing.EntitySetPathSegment(ctx.EntitySet),
                        new System.Web.Http.OData.Routing.KeyValuePathSegment(sport.Id.ToString()),
                        new System.Web.Http.OData.Routing.ActionPathSegment(changeCaption.Name)));
                }
                else
                {
                    return null;
                }
            }, followsConventions: true);


            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    
        }
    }
}
