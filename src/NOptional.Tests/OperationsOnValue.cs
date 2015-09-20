using System.Threading.Tasks;
using Moq;
using Xunit;

namespace NOptional.Tests
{
    public class OperationsOnValue
    {
        private const string TestValue = "hello world";

        [Fact]
        public void IfPresentIsCalledOnSome()
        {
            var some = Optional<string>.Of(TestValue);

            var mock = BuildTestConsumer();

            some.IfPresent(value =>
            {
                mock.Object.Comsume(value);
            });

            mock.Verify(framework => framework.Comsume(TestValue), Times.Once());
        }

        [Fact]
        public async Task IfPresentAsyncIsCalledOnSome()
        {
            var some = Optional<string>.Of(TestValue);

            var mock = BuildTestConsumer();

            await some.IfPresentAsync(async value =>
            {
                await mock.Object.ConsumeAsync(value);
            });

            mock.Verify(framework => framework.ConsumeAsync(TestValue), Times.Once());
        }

        [Fact]
        public void IfPresentIsNotCalledOnNone()
        {
            var none = Optional<string>.Empty;

            var mock = BuildTestConsumer();

            none.IfPresent(value =>
            {
                mock.Object.Comsume(value);
            });

            mock.Verify(framework => framework.Comsume(TestValue), Times.Never());
        }

        [Fact]
        public async Task IfPresentAsyncIsNotCalledOnNone()
        {
            var none = Optional<string>.Empty;

            var mock = BuildTestConsumer();

            await none.IfPresentAsync(async value =>
            {
                await mock.Object.ConsumeAsync(value);
            });

            mock.Verify(framework => framework.ConsumeAsync(TestValue), Times.Never());
        }

        public interface ITestConsumer
        {
            void Comsume(string value);
            Task ConsumeAsync(string value);
        }

        private Mock<ITestConsumer> BuildTestConsumer()
        {
            var mock = new Mock<ITestConsumer>();

            mock.Setup(framework => framework.Comsume(TestValue));

            mock.Setup(framework => framework.ConsumeAsync(TestValue)).Returns(Task.FromResult<object>(null));

            return mock;
        }
    }
}
