using AutoFixture;
using AutoFixture.Xunit2;

namespace GYM.BLL.Tests
{
    public class AutoDomainDataAttribute : AutoDataAttribute
    {
        public AutoDomainDataAttribute()
            : base(() =>
            {
                var fixture = new Fixture();
                fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => fixture.Behaviors.Remove(b));
                fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

                return fixture;
            })
        {
        }
    }
}
