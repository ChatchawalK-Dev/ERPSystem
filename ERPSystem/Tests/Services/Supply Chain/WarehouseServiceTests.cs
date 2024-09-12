using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ERPSystem.Models.Supply_Chain;
using ERPSystem.Services.SupplyChain;
using ERPSystem.Data.Repository.SupplyChain;

namespace ERPSystem.Tests.Services.SupplyChain
{
    public class WarehouseServiceTests
    {
        private readonly Mock<IWarehouseRepository> _mockRepository;
        private readonly WarehouseService _service;

        public WarehouseServiceTests()
        {
            _mockRepository = new Mock<IWarehouseRepository>();
            _service = new WarehouseService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllWarehousesAsync_ReturnsAllWarehouses()
        {
            // Arrange
            var warehouses = new List<Warehouse>
            {
                new Warehouse { /* Initialize properties */ },
                new Warehouse { /* Initialize properties */ }
            };
            ReflectionHelper.SetPropertyValue(warehouses[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(warehouses[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(warehouses);

            // Act
            var result = await _service.GetAllWarehousesAsync();

            // Assert
            Assert.NotNull(result);
            var warehouseList = result.ToList();
            Assert.Equal(2, warehouseList.Count);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(warehouseList[0], "Id"));
            Assert.Equal(2, ReflectionHelper.GetPropertyValue(warehouseList[1], "Id"));
        }

        [Fact]
        public async Task GetWarehouseByIdAsync_ReturnsCorrectWarehouse()
        {
            // Arrange
            var warehouse = new Warehouse { /* Initialize properties */ };
            ReflectionHelper.SetPropertyValue(warehouse, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(warehouse);

            // Act
            var result = await _service.GetWarehouseByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(result!, "Id"));
        }

        [Fact]
        public async Task CreateWarehouseAsync_CallsRepositoryAdd()
        {
            // Arrange
            var warehouse = new Warehouse { /* Initialize properties */ };

            // Act
            await _service.CreateWarehouseAsync(warehouse);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(warehouse), Times.Once);
        }

        [Fact]
        public async Task UpdateWarehouseAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var warehouse = new Warehouse { /* Initialize properties */ };

            // Act
            await _service.UpdateWarehouseAsync(warehouse);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(warehouse), Times.Once);
        }

        [Fact]
        public async Task DeleteWarehouseAsync_CallsRepositoryDelete()
        {
            // Arrange
            int id = 1;

            // Act
            await _service.DeleteWarehouseAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetAllWarehousesWithInventoriesAsync_ReturnsWarehousesWithInventories()
        {
            // Arrange
            var warehouses = new List<Warehouse>
            {
                new Warehouse { /* Initialize properties */ },
                new Warehouse { /* Initialize properties */ }
            };
            _mockRepository.Setup(repo => repo.GetAllWarehousesWithInventoriesAsync()).ReturnsAsync(warehouses);

            // Act
            var result = await _service.GetAllWarehousesWithInventoriesAsync();

            // Assert
            Assert.NotNull(result);
            var warehouseList = result.ToList();
            Assert.Equal(2, warehouseList.Count);
        }
    }
}
