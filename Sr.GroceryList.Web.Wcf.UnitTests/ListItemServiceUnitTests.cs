using System;
using Autofac.Extras.Moq;
using Sr.GroceryList.Dal;
using Sr.GroceryList.Entities;
using Sr.GroceryList.Wcf.Service;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Sr.GroceryList.Wcf.UnitTests
{
    [TestClass]
    public class ListItemServiceUnitTests
    {
        [TestMethod]
        public void GetById_Should_Return_ListItem()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var dummy = new ListItemDto()
                {
                    Id = 1,
                    Name = "Henk",
                    Description = "ABC",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Status = "Available"
                };

                mock.Mock<IListItemRepository>()
                    .Setup(srv => srv.GetById(It.IsAny<int>()))
                    .Returns<int>(id => dummy);

                // Sut
                var sut = mock.Create<ListItemService>();

                // Act
                var actual = sut.GetById("1");

                // Assert
                mock.Mock<IListItemRepository>().Verify(x => x.GetById(It.IsAny<int>()));

                // ToDo SR write cool equal
                //actual.Result.Should().Be(dummy)
                actual.Result.Id.Should().Be(dummy.Id);
            }
        }
    }
}
