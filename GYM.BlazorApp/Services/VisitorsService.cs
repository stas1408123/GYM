using GYM.BlazorApp.Data.ViewModels;

namespace GYM.BlazorApp.Services
{
    public class VisitorsService : GenericService<VisitorViewModel>
    {
        private const string RouteForCouches = "https://localhost:7163/api/Orders";
        public VisitorsService(IHttpClientFactory clientFactory) : base(clientFactory, RouteForCouches)
        {

        }
    }
}
