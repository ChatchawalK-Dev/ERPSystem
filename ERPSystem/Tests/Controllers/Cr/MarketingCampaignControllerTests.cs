using ERPSystem.Controllers.Cr;
using ERPSystem.Models.Cr;
using ERPSystem.Services.Cr;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ERPSystem.Tests.Controllers.Cr
{
    public class MarketingCampaignControllerTests
    {
        private readonly Mock<IMarketingCampaignService> _mockMarketingCampaignService;
        private readonly MarketingCampaignController _controller;

        public MarketingCampaignControllerTests()
        {
            _mockMarketingCampaignService = new Mock<IMarketingCampaignService>();
            _controller = new MarketingCampaignController(_mockMarketingCampaignService.Object);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithMarketingCampaign()
        {
            // Arrange
            var campaignId = 1;
            var marketingCampaign = new MarketingCampaign();
            ReflectionHelper.SetProperty(marketingCampaign, "Id", campaignId);
            ReflectionHelper.SetProperty(marketingCampaign, "Name", "Campaign A");

            _mockMarketingCampaignService.Setup(service => service.GetByIdAsync(campaignId))
                                         .ReturnsAsync(marketingCampaign);

            // Act
            var result = await _controller.GetById(campaignId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<MarketingCampaign>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<MarketingCampaign>(okResult.Value);
            Assert.Equal(campaignId, returnValue.Id);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfMarketingCampaigns()
        {
            // Arrange
            var campaigns = new List<MarketingCampaign>
            {
                new MarketingCampaign(),
                new MarketingCampaign()
            };
            ReflectionHelper.SetProperty(campaigns[0], "Id", 1);
            ReflectionHelper.SetProperty(campaigns[0], "Name", "Campaign A");
            ReflectionHelper.SetProperty(campaigns[1], "Id", 2);
            ReflectionHelper.SetProperty(campaigns[1], "Name", "Campaign B");

            _mockMarketingCampaignService.Setup(service => service.GetAllAsync())
                                         .ReturnsAsync(campaigns);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<MarketingCampaign>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<MarketingCampaign>>(okResult.Value);
            Assert.Equal(2, ((List<MarketingCampaign>)returnValue).Count);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var marketingCampaign = new MarketingCampaign();
            ReflectionHelper.SetProperty(marketingCampaign, "Id", 1);
            ReflectionHelper.SetProperty(marketingCampaign, "Name", "Campaign A");

            _mockMarketingCampaignService.Setup(service => service.AddAsync(marketingCampaign))
                                         .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(marketingCampaign);

            // Assert
            var actionResult = Assert.IsType<ActionResult<MarketingCampaign>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsType<MarketingCampaign>(createdAtActionResult.Value);
            Assert.Equal(marketingCampaign.Id, returnValue.Id);
        }

        [Fact]
        public async Task Update_ReturnsNoContentResult()
        {
            // Arrange
            var marketingCampaign = new MarketingCampaign();
            ReflectionHelper.SetProperty(marketingCampaign, "Id", 1);
            ReflectionHelper.SetProperty(marketingCampaign, "Name", "Campaign A");

            _mockMarketingCampaignService.Setup(service => service.UpdateAsync(marketingCampaign))
                                         .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(marketingCampaign.Id, marketingCampaign);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentResult()
        {
            // Arrange
            var campaignId = 1;
            _mockMarketingCampaignService.Setup(service => service.DeleteAsync(campaignId))
                                         .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(campaignId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
