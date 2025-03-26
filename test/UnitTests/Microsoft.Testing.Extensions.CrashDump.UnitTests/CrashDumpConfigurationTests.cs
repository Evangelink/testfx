// Copyright (c) Microsoft Corporation. All rights reserved.

using Microsoft.Testing.Extensions.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Testing.Extensions.Diagnostics.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="CrashDumpConfiguration"/> class.
    /// </summary>
    [TestClass]
    public class CrashDumpConfigurationTests
    {
        private readonly CrashDumpConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrashDumpConfigurationTests"/> class.
        /// </summary>
        public CrashDumpConfigurationTests()
        {
            _config = new CrashDumpConfiguration();
        }

        /// <summary>
        /// Tests that the default value of the <see cref="CrashDumpConfiguration.Enable"/> property is true.
        /// </summary>
        [TestMethod]
        public void Constructor_Default_EnablePropertyIsTrue()
        {
            // Act
            bool defaultEnable = _config.Enable;

            // Assert
            Assert.IsTrue(defaultEnable, "Expected the default value of Enable to be true.");
        }

        /// <summary>
        /// Tests that the default value of the <see cref="CrashDumpConfiguration.DumpFileNamePattern"/> property is null.
        /// </summary>
        [TestMethod]
        public void Constructor_Default_DumpFileNamePatternIsNull()
        {
            // Act
            string? defaultPattern = _config.DumpFileNamePattern;

            // Assert
            Assert.IsNull(defaultPattern, "Expected the default value of DumpFileNamePattern to be null.");
        }

        /// <summary>
        /// Tests that setting the <see cref="CrashDumpConfiguration.DumpFileNamePattern"/> property to a valid pattern
        /// returns the same pattern upon retrieval.
        /// </summary>
        [TestMethod]
        public void DumpFileNamePattern_SetValue_ReturnsSameValue()
        {
            // Arrange
            string expectedPattern = "CrashDump_{0}.dmp";

            // Act
            _config.DumpFileNamePattern = expectedPattern;
            string? actualPattern = _config.DumpFileNamePattern;

            // Assert
            Assert.AreEqual(expectedPattern, actualPattern, "DumpFileNamePattern did not return the expected value after setting.");
        }

        /// <summary>
        /// Tests that setting the <see cref="CrashDumpConfiguration.DumpFileNamePattern"/> property to null 
        /// is handled correctly.
        /// </summary>
        [TestMethod]
        public void DumpFileNamePattern_SetToNull_ReturnsNull()
        {
            // Arrange
            _config.DumpFileNamePattern = "InitialValue";

            // Act
            _config.DumpFileNamePattern = null;
            string? actualPattern = _config.DumpFileNamePattern;

            // Assert
            Assert.IsNull(actualPattern, "DumpFileNamePattern should return null after being set to null.");
        }

        /// <summary>
        /// Tests that setting the <see cref="CrashDumpConfiguration.Enable"/> property to false 
        /// correctly updates its value.
        /// </summary>
        [TestMethod]
        public void Enable_SetToFalse_ReturnsFalse()
        {
            // Arrange
            _config.Enable = true; // Ensure starting with a true value
            bool expectedEnable = false;

            // Act
            _config.Enable = expectedEnable;
            bool actualEnable = _config.Enable;

            // Assert
            Assert.AreEqual(expectedEnable, actualEnable, "Enable property did not return false after being set to false.");
        }

        /// <summary>
        /// Tests that setting the <see cref="CrashDumpConfiguration.Enable"/> property to true 
        /// correctly updates its value.
        /// </summary>
        [TestMethod]
        public void Enable_SetToTrue_ReturnsTrue()
        {
            // Arrange
            _config.Enable = false; // Ensure starting with a false value
            bool expectedEnable = true;

            // Act
            _config.Enable = expectedEnable;
            bool actualEnable = _config.Enable;

            // Assert
            Assert.AreEqual(expectedEnable, actualEnable, "Enable property did not return true after being set to true.");
        }
    }
}
