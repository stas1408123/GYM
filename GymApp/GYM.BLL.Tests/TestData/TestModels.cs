using GYM.BLL.Models;

namespace GYM.BLL.Tests.TestData
{
    public static class TestModels
    {
        public static IEnumerable<CouchModel> GetCouchModelsForTest()
        {
            return new[]
            {
                new CouchModel()
                {
                    Id = 1,
                    FirstName = "Arnold",
                    LastName = "Schwarzenegger",
                    Description = "Fitness, Bodybuilding",
                    Visitors = null,
                },
                new CouchModel()
                {
                    Id = 2,
                    FirstName = "Mick",
                    LastName = "Smith",
                    Description = "Fitness, Cross-fit",
                    Visitors = null
                },
                new CouchModel()
                {
                    Id = 3,
                    FirstName = "Olivia",
                    LastName = "Johnson",
                    Description = "Cross-fit, Triathlon",
                    Visitors = null
                },
            };
        }

        public static IEnumerable<VisitorModel> GetVisitorModelsForTest()
        {
            return new[]
            {
                new VisitorModel
                {
                    Id = 1,
                    FirstName = "Connor",
                    LastName = "Williams",
                    Couches = new List<CouchModel>(),
                    Orders = new List<OrderModel>()

                },
                new VisitorModel
                {
                    Id = 2,
                    FirstName = "Margaret",
                    LastName = "Miller",
                    Couches = new List<CouchModel>(),
                    Orders = new List<OrderModel>()

                },
                new VisitorModel
                {
                    Id = 3,
                    FirstName = "Emma",
                    LastName = "Smith",
                    Couches = new List<CouchModel>(),
                    Orders = new List<OrderModel>()
                }
            };
        }

        public static IEnumerable<OrderModel> GetOrderModelsForTest()
        {
            return new[]
            {
                new OrderModel
                {
                    Id = 1,
                    Title = "Cross-fit #1",
                    Description = "Cross-fit program for beginner",
                    Cost = 25,
                    Date = DateTime.Now.Subtract(TimeSpan.FromDays(4)),
                    VisitorId = 1,
                },
                new OrderModel
                {
                    Id = 2,
                    Title = "Full-body program #2",
                    Description = "Full body program, difficult level: medium",
                    Cost = 20,
                    Date = DateTime.Now.Subtract(TimeSpan.FromDays(3)),
                    VisitorId = 1
                },
                new OrderModel
                {
                    Id = 3,
                    Title = "Cross-fit #5",
                    Description = "Cross-fit program for professional sportsmen",
                    Cost = 30,
                    Date = DateTime.Now.Subtract(TimeSpan.FromDays(2)),
                    VisitorId = 2
                },
                new OrderModel
                {
                    Id = 4,
                    Title = "Full-body program #7",
                    Description = "Full body program, difficult level: medium",
                    Cost = 15,
                    Date = DateTime.Now.Subtract(TimeSpan.FromDays(1)),
                    VisitorId = 2,
                }
            };
        }
    }
}
