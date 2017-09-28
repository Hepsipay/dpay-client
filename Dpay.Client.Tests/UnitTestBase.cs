using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Dpay.Client.Tests
{
    [TestFixture]
    public abstract class UnitTestBase
    {
        private MockRepository _mockRepository;

        protected Mock<T> MockFor<T>() where T : class
        {
            return _mockRepository.Create<T>();
        }

        protected Mock<T> MockFor<T>(MockBehavior mockBehavior) where T : class
        {
            return _mockRepository.Create<T>(mockBehavior);
        }

        protected T Create<T>()
        {

            RegisterDateTimeAssertionForType<T>();
            return FakeDataRepository.Create<T>();
        }

        protected List<T> CreateMany<T>()
        {
            RegisterDateTimeAssertionForType<T>();
            return FakeDataRepository.CreateMany<T>().ToList();
        }

        protected List<T> CreateMany<T>(int count)
        {
            RegisterDateTimeAssertionForType<T>();
            return FakeDataRepository.CreateMany<T>(count).ToList();
        }

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            FakeDataRepository = new Fixture();

            FakeDataRepository.Behaviors.Add(new OmitOnRecursionBehavior(2));

            VerifyAll = true;
            FinalizeSetUp();
        }

        public bool VerifyAll { get; set; }

        protected virtual void FinalizeSetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
            if (VerifyAll)
            {
                _mockRepository.VerifyAll();
            }
            else
            {
                _mockRepository.Verify();
            }

            FinalizeTearDown();
        }

        protected IFixture FakeDataRepository { get; set; }

        protected virtual void FinalizeTearDown() { }


        protected void RegisterDateTimeAssertionForType<T>()
        {
            AssertionOptions.AssertEquivalencyUsing(options =>
                options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation)).WhenTypeIs<DateTime>()
                );
        }
    }
}
