using AutoFixture;
using AutoFixture.Xunit2;
using GYM.API.Models;

namespace GYM.API.IntegrationTests
{
    public class AutoDomainDataAttribute : AutoDataAttribute
    {
        public AutoDomainDataAttribute()
            : base(() =>
            {
                var fixture = new Fixture();
                fixture.Customize<CouchViewModel>(couch => couch.Without(p => p.Id).With(p => p.Visitors, new List<VisitorViewModel>()));
                fixture.Customize<OrderViewModel>(order => order.Without(p => p.Id));
                fixture.Customize<VisitorViewModel>(visitor => visitor
                    .Without(p => p.Id)
                    .Without(p => p.Id)
                    .With(p => p.Orders, new List<OrderViewModel>())
                    .With(p => p.Couches, new List<CouchViewModel>()));

                return fixture;
            })
        {
        }
    }
}
