using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ERPSystem.Models.Manufacturing;
using ERPSystem.Services.Manufacturing;
using ERPSystem.Data.Repository;

namespace ERPSystem.Tests.Services.Manufacturing
{
    public class InventoryServiceTests
    {
        private readonly Mock<IRepository<Inventory>> _mockRepository;
        private readonly InventoryService _service;

        public InventoryServiceTests()
        {
            _mockRepository = new Mock<IRepository<Inventory>>();
            _service = new InventoryService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllItemsAsync_ReturnsAllItems()
        {
            // Arrange
            var items = new List<Inventory>
            {
                new Inventory { Quantity = 10 },
                new Inventory { Quantity = 20 }
            };
            ReflectionHelper.SetPropertyValue(items[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(items[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(items);

            // Act
            var result = await _service.GetAllItemsAsync();

            // Assert
            Assert.NotNull(result);
            var itemList = result.ToList();
            Assert.Equal(2, itemList.Count);
            Assert.All(itemList, item =>
            {
                Assert.NotNull(item);
                Assert.True(item.Quantity > 0);
            });
        }

        [Fact]
        public async Task GetItemByIdAsync_ReturnsItem()
        {
            // Arrange
            var item = new Inventory { Quantity = 10 };
            ReflectionHelper.SetPropertyValue(item, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(item);

            // Act
            var result = await _service.GetItemByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(10, result?.Quantity);
        }

        [Fact]
        public async Task AddItemAsync_CallsAddAsync()
        {
            // Arrange
            var item = new Inventory { Quantity = 10 };

            // Act
            await _service.AddItemAsync(item);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(item), Times.Once);
        }

        [Fact]
        public async Task UpdateItemAsync_CallsUpdateAsync()
        {
            // Arrange
            var item = new Inventory { Quantity = 10 };

            // Act
            await _service.UpdateItemAsync(item);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(item), Times.Once);
        }

        [Fact]
        public async Task DeleteItemAsync_CallsDeleteAsync()
        {
            // Act
            await _service.DeleteItemAsync(1);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task FindItemAsync_ReturnsItemMatchingPredicate()
        {
            // Arrange
            var items = new List<Inventory>
            {
                new Inventory { Quantity = 10 },
                new Inventory { Quantity = 20 }
            };
            ReflectionHelper.SetPropertyValue(items[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(items[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(items);

            // Act
            var result = await _service.FindItemAsync(i => i.Quantity == 20);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(20, result?.Quantity);
        }
    }
}
