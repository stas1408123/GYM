using GYM.BlazorApp.Data.ViewModels;

namespace GYM.BlazorApp.Services
{
    public class OrdersService : GenericService<OrderViewModel>
    {
        private const string RouteForCouches = "https://localhost:7163/api/Orders";
        public OrdersService(IHttpClientFactory clientFactory) : base(clientFactory, RouteForCouches)
        {

        }
    }
}
