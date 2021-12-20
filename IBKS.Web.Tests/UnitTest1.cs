using IBKS.Web.Controllers;
using IBKS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using Type = IBKS.Web.Models.Type;

namespace IBKS.Web.Tests
{
    [TestClass]
    public class UnitTest1
    {
        DbContextOptions<IBKSDbContext> options;

        public UnitTest1()
        {
            options = new DbContextOptionsBuilder<IBKSDbContext>()
                .UseInMemoryDatabase("ibks_database")
                .Options;

            Seed();
        }

        private void Seed()
        {
            using var context = new IBKSDbContext(options);
            context.Database.EnsureDeleted();

            var room1 = new Room()
            {
                Id = 1,
                Name = "Room 1"
            };
            var room2 = new Room()
            {
                Id = 2,
                Name = "Room 2"
            };

            var type1 = new Type()
            {
                Id = 1,
                Name = "Type 1"
            };
            var type2 = new Type()
            {
                Id = 2,
                Name = "Type 2"
            };

            context.AddRange(type1, type2, room1, room2);
            context.SaveChanges();
        }

        [TestMethod]
        public async Task RoomsController_GetAllRooms_Success()
        {
            //Arrange
            using var dbContext = new IBKSDbContext(options);
            var controller = new RoomsController(dbContext);

            //Act
            var rooms = (await controller.GetRooms()).Value;

            //Assert
            Assert.AreEqual(2, rooms.Count());
            Assert.AreEqual(1, rooms.First().Id);
            Assert.AreEqual("Room 1", rooms.First().Name);
        }

        [TestMethod]
        public async Task LightsController_AddLight_Added()
        {
            //Arrange
            using var dbContext = new IBKSDbContext(options);
            var controller = new LightsController(dbContext);

            //Act
            await controller.PostLight(new Light
            {
                Name = "Light test 1",
                RoomId = 1,
                TypeId = 1
            });

            //Assert
            Assert.AreEqual(1, dbContext.Lights.Count());
            Assert.AreEqual("Light test 1", dbContext.Lights.First().Name);
            Assert.AreEqual(1, dbContext.Lights.First().RoomId);
            Assert.AreEqual(1, dbContext.Lights.First().TypeId);
        }

        [TestMethod]
        public async Task LightsController_GetLight_NotFound()
        {
            //Arrange
            using var dbContext = new IBKSDbContext(options);
            var controller = new LightsController(dbContext);

            //Act
            var room = (await controller.GetLight(5));

            //Assert
            Assert.IsInstanceOfType(room.Result, typeof(NotFoundResult));
        }

    }
}
