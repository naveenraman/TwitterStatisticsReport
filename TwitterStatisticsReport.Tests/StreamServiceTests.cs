using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using TwitterStatisticsReport.Interfaces;
using TwitterStatisticsReport.Models;

namespace TwitterStatisticsReport.Tests
{
    public class StreamServiceTests
    {
        private readonly Mock<IStreamService> _mockStreamService;
        private readonly Mock<ILoggerService> _mockLoggerService;
        private readonly Mock<IOptions<Settings>> _mockOptionsSettings;


        public StreamServiceTests()
        {
            _mockStreamService = new Mock<IStreamService>();
            _mockLoggerService = new Mock<ILoggerService>();
            _mockOptionsSettings = new Mock<IOptions<Settings>>();
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetTwitterStream_WhenStreamUrl_IsCalled_ReturnsSuccess()
        {
            // Arrange
            Settings settings = new Settings
            {
                BearerToken = "Test",
                ApiUrl = "http://test.com"
            };
            var mockedHandler = new Mock<HttpMessageHandler>();
            var mockResponse =
                new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(File.ReadAllText(@"TestData\Tweet1.json", Encoding.UTF8)) };
            mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            mockedHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(mockResponse);
            var httpClient = new HttpClient(mockedHandler.Object);
            _mockOptionsSettings.Setup(x => x.Value)
                    .Returns(settings)
                    .Verifiable();
            _mockStreamService.Setup(x => x.GetSampleStream())
                .Returns(Task.FromResult(true))
                .Verifiable();

            // Act
            var _streamService =
                new StreamService(_mockOptionsSettings.Object, httpClient, _mockLoggerService.Object);
            var processTweets = _streamService.GetSampleStream().ConfigureAwait(false).GetAwaiter();

            // Assert
            Assert.That(bool.TrueString,Is.EqualTo(processTweets.IsCompleted.ToString()));
        }
    }
}