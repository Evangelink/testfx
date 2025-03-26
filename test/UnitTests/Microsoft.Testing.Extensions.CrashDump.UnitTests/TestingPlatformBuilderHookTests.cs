using Microsoft.Testing.Extensions.CrashDump;
using Microsoft.Testing.Platform.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Microsoft.Testing.Extensions.CrashDump.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="TestingPlatformBuilderHook"/> class.
    /// </summary>
    [TestClass]
    public class TestingPlatformBuilderHookTests
    {
        /// <summary>
        /// Tests that the AddExtensions method successfully calls AddCrashDumpProvider on the provided ITestApplicationBuilder when valid inputs are provided.
        /// </summary>
        [TestMethod]
        public void AddExtensions_ValidBuilderAndEmptyArgs_CallsAddCrashDumpProvider()
        {
            // Arrange
            var mockBuilder = new Mock<ITestApplicationBuilder>();
            string[] args = new string[0];

            // Act
            TestingPlatformBuilderHook.AddExtensions(mockBuilder.Object, args);

            // Assert
            mockBuilder.Verify(builder => builder.AddCrashDumpProvider(true), Times.Once,
                "Expected AddCrashDumpProvider to be called once with ignoreIfNotSupported set to true.");
        }

        /// <summary>
        /// Tests that the AddExtensions method successfully calls AddCrashDumpProvider on the provided ITestApplicationBuilder
        /// even when the command line arguments parameter is null.
        /// </summary>
        [TestMethod]
        public void AddExtensions_ValidBuilderAndNullArgs_CallsAddCrashDumpProvider()
        {
            // Arrange
            var mockBuilder = new Mock<ITestApplicationBuilder>();

            // Act
            TestingPlatformBuilderHook.AddExtensions(mockBuilder.Object, null);

            // Assert
            mockBuilder.Verify(builder => builder.AddCrashDumpProvider(true), Times.Once,
                "Expected AddCrashDumpProvider to be called once with ignoreIfNotSupported set to true when command line arguments are null.");
        }

        /// <summary>
        /// Tests that the AddExtensions method throws a NullReferenceException when the ITestApplicationBuilder parameter is null.
        /// </summary>
        [TestMethod]
        public void AddExtensions_NullBuilder_ThrowsNullReferenceException()
        {
            // Arrange
            ITestApplicationBuilder nullBuilder = null;
            string[] args = new string[0];

            // Act & Assert
            Assert.ThrowsException<NullReferenceException>(() => TestingPlatformBuilderHook.AddExtensions(nullBuilder, args),
                "Expected a NullReferenceException when testApplicationBuilder is null.");
        }
    }
}
