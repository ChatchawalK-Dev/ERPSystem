using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ERPSystem.Models.Cr;
using ERPSystem.Services.Cr;
using ERPSystem.Data.Repository.Cr;

namespace ERPSystem.Tests.Services.Cr
{
    public class MarketingCampaignServiceTests
    {
        private readonly Mock<IMarketingCampaignRepository> _mockRepository;
        private readonly MarketingCampaignService _service;

        public MarketingCampaignServiceTests()
        {
            _mockRepository = new Mock<IMarketingCampaignRepository>();
            _service = new MarketingCampaignService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMarketingCampaign()
        {
            // Arrange
            var marketingCampaign = new MarketingCampaign { CampaignName = "Summer Sale" };
            ReflectionHelper.SetPropertyValue(marketingCampaign, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(marketingCampaign);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Summer Sale", result.CampaignName);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllMarketingCampaigns()
        {
            // Arrange
            var marketingCampaigns = new List<MarketingCampaign>
            {
                new MarketingCampaign { CampaignName = "Summer Sale" },
                new MarketingCampaign { CampaignName = "Winter Sale" }
            };

            ReflectionHelper.SetPropertyValue(marketingCampaigns[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(marketingCampaigns[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(marketingCampaigns);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            var campaignList = result.ToList();
            Assert.Equal(2, campaignList.Count);
            Assert.Contains(campaignList, c => c.Id == 1 && c.CampaignName == "Summer Sale");
            Assert.Contains(campaignList, c => c.Id == 2 && c.CampaignName == "Winter Sale");
        }

        [Fact]
        public async Task AddAsync_CallsRepositoryAdd()
        {
            // Arrange
            var marketingCampaign = new MarketingCampaign { CampaignName = "Spring Sale" };
            ReflectionHelper.SetPropertyValue(marketingCampaign, "Id", 3);

            // Act
            await _service.AddAsync(marketingCampaign);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<MarketingCampaign>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var marketingCampaign = new MarketingCampaign { CampaignName = "Spring Sale" };
            ReflectionHelper.SetPropertyValue(marketingCampaign, "Id", 3);

            // Act
            await _service.UpdateAsync(marketingCampaign);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<MarketingCampaign>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsRepositoryDelete()
        {
            // Arrange
            var id = 3;

            // Act
            await _service.DeleteAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }
    }
}
