using Microsoft.Xrm.Sdk;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Codebug.PaypalTokenGenerator.UnitTests
{
    public class PaypalServiceTests
    {
        private PaypalService _paypalService;
        private ITracingService _tracingService;

        [SetUp]
        public void Init()
        {
            _tracingService = Substitute.For<ITracingService>();            
        }

        [Test]
        public void Constructor_NullSend_ThrowsError()
        {
            Assert.Throws<ArgumentNullException>(() => new PaypalService(null));
        }

        [Test]
        public async Task GenerateTokenAsync_EmptyClient_ThrowsError()
        {
            _paypalService = new PaypalService(_tracingService);
            Assert.ThrowsAsync<ArgumentNullException>(() => _paypalService.GenerateTokenAsync("", Generator.Secret, true));
        }

        [Test]
        public async Task GenerateTokenAsync_EmptySecret_ThrowsError()
        {
            _paypalService = new PaypalService(_tracingService);
            Assert.ThrowsAsync<ArgumentNullException>(() => _paypalService.GenerateTokenAsync(Generator.ClientId, "", true));
        }

        [Test]
        public async Task GenerateTokenAsync_WrongSecret_ThrowsError()
        {
            _paypalService = new PaypalService(_tracingService);
            Assert.ThrowsAsync<InvalidPluginExecutionException>(() => _paypalService.GenerateTokenAsync("Hello", "Hello", true));
        }

        [Test]
        [Explicit("Integration Test. Will not work without correct client id and secret")]
        public async Task GenerateTokenAsync_Success()
        {
            _paypalService = new PaypalService(_tracingService);
            var result = await _paypalService.GenerateTokenAsync(Generator.ClientId, Generator.Secret, false);

            Assert.IsTrue(result.GetType() == typeof(string));
            _tracingService.Received(3).Trace(Arg.Any<string>());
        }
    }
}
