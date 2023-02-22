using GYM.DAL.Entities;

namespace GYM.BLL.Tests.TestData
{
    public static class TestEntities
    {
        public static IEnumerable<CouchEntity> GetCouchEntitiesForTest()
        {
            return new[]
            {
                new CouchEntity
                {
                    Id = 1,
                    FirstName = "Arnold",
                    LastName = "Schwarzenegger",
                    Description = "Fitness, Bodybuilding",
                    Visitors = new List<VisitorEntity>(),
                },
                new CouchEntity()
                { Id = 2,
                    FirstName = "Mick",
                    LastName = "Smith",
                    Description = "Fitness, Cross-fit",
                    Visitors = new List<VisitorEntity>()
                },
                new CouchEntity()
                { Id = 3,
                    FirstName = "Olivia",
                    LastName = "Johnson",
                    Description = "Cross-fit, Triathlon",
                    Visitors = new List<VisitorEntity>()
                }
            };
        }

        public static IEnumerable<VisitorEntity> GetVisitorEntitiesForTest()
        {
            return new[]
            {
                new VisitorEntity
                {
                    Id = 1,
                    FirstName = "Connor",
                    LastName = "Williams",
                    Couches =new List<CouchEntity>(),
                    Orders = new List<OrderEntity>()
                },
                new VisitorEntity
                {
                    Id = 2,
                    FirstName = "Margaret",
                    LastName = "Miller",
                    Couches =new List<CouchEntity>(),
                    Orders = new List<OrderEntity>(),
                },
                new VisitorEntity
                {
                    Id = 3,
                    FirstName = "Emma",
                    LastName = "Smith",
                    Couches =new List<CouchEntity>(),
                    Orders = new List<OrderEntity>()
                }
            };
        }

        public static IEnumerable<OrderEntity> GetOrderEntitiesForTest()
        {
            return new[]
            {
                new OrderEntity
                {
                    Id = 1,
                    Title = "Cross-fit #1",
                    Description = "Cross-fit program for beginner",
                    Cost = 25,
                    Date = DateTime.Now.Subtract(TimeSpan.FromDays(4)),
                    VisitorId = 1,
                    Visitor = new VisitorEntity
                    {
                        Id = 1,
                        FirstName = "Connor",
                        LastName = "Williams",
                        Couches =new List<CouchEntity>(),
                        Orders = new List<OrderEntity>()
                    }
                },
                new OrderEntity
                {
                    Id = 2,
                    Title = "Full-body program #2",
                    Description = "Full body program, difficult level: medium",
                    Cost = 20,
                    Date = DateTime.Now.Subtract(TimeSpan.FromDays(3)),
                    VisitorId = 1,
                    Visitor = new VisitorEntity
                    {
                        Id = 1,
                        FirstName = "Connor",
                        LastName = "Williams",
                        Couches =new List<CouchEntity>(),
                        Orders = new List<OrderEntity>()
                    }
                },
                new OrderEntity
                {
                    Id = 3,
                    Title = "Cross-fit #5",
                    Description = "Cross-fit program for professional sportsmen",
                    Cost = 30,
                    Date = DateTime.Now.Subtract(TimeSpan.FromDays(2)),
                    VisitorId = 2,
                    Visitor = new VisitorEntity
                    {
                       Id = 2,
                       FirstName = "Margaret",
                       LastName = "Miller",
                       Couches =new List<CouchEntity>(),
                       Orders = new List<OrderEntity>(),
                    }
                },
                new OrderEntity
                {
                    Id = 4,
                    Title = "Full-body program #7",
                    Description = "Full body program, difficult level: medium",
                    Cost = 15,
                    Date = DateTime.Now.Subtract(TimeSpan.FromDays(1)),
                    VisitorId = 2,
                    Visitor = new VisitorEntity
                    {
                        Id = 2,
                        FirstName = "Margaret",
                        LastName = "Miller",
                        Couches =new List<CouchEntity>(),
                        Orders = new List<OrderEntity>(),
                    }
                }
            };
        }
    }
}
