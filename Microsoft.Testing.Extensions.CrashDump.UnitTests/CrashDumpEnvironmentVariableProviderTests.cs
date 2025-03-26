// using Microsoft.Testing.Extensions.Diagnostics;
// using Microsoft.Testing.Platform.CommandLine;
// using Microsoft.Testing.Platform.Configurations;
// using Microsoft.Testing.Platform.Extensions;
// using Microsoft.Testing.Platform.Extensions.TestHostControllers;
// using Microsoft.Testing.Platform.Logging;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Moq;
// using System;
// using System.Collections.Generic;
// using System.Globalization;
// using System.IO;
// using System.Text;
// using System.Threading.Tasks;
// 
// namespace Microsoft.Testing.Extensions.Diagnostics.UnitTests
// {
//     /// <summary>
//     /// Minimal representation of an environment variable owned by the test host.
//     /// </summary>
//     internal class OwnedEnvironmentVariable
//     {
//         public string Name { get; }
//         public string Value { get; }
//         public bool Flag1 { get; }
//         public bool Flag2 { get; }
// 
//         public OwnedEnvironmentVariable(string name, string value, bool flag1, bool flag2)
//         {
//             Name = name;
//             Value = value;
//             Flag1 = flag1;
//             Flag2 = flag2;
//         }
//     }
// 
//     /// <summary>
//     /// Fake implementation for IEnvironmentVariables and IReadOnlyEnvironmentVariables used for testing.
//     /// </summary>
// //     internal class FakeEnvironmentVariables : IEnvironmentVariables, IReadOnlyEnvironmentVariables [Error] (40-47)CS0518 Predefined type 'System.Void' is not defined or imported [Error] (40-47)CS0518 Predefined type 'System.Void' is not defined or imported [Error] (40-70)CS0518 Predefined type 'System.Boolean' is not defined or imported
// //     {
// //         private readonly Dictionary<string, OwnedEnvironmentVariable> _variables = new Dictionary<string, OwnedEnvironmentVariable>(StringComparer.OrdinalIgnoreCase);
// //         public void SetVariable(OwnedEnvironmentVariable variable)
// //         {
// //             _variables[variable.Name] = variable;
// //         }
// // 
// //         public bool TryGetVariable(string name, out OwnedEnvironmentVariable variable)
// //         {
// //             return _variables.TryGetValue(name, out variable);
// //         }
// // 
// //         public IReadOnlyDictionary<string, OwnedEnvironmentVariable> Variables => _variables;
// //     }
// 
//     /// <summary>
//     /// Minimal representation of ValidationResult used by CrashDumpEnvironmentVariableProvider.
//     /// </summary>
//     internal class ValidationResult
//     {
//         public bool IsValid { get; }
//         public string? ErrorMessage { get; }
// 
//         private ValidationResult(bool isValid, string? errorMessage)
//         {
//             IsValid = isValid;
//             ErrorMessage = errorMessage;
//         }
// 
//         public static ValidationResult Invalid(string errorMessage)
//         {
//             return new ValidationResult(false, errorMessage);
//         }
// 
//         public static readonly ValidationResult ValidTask = new ValidationResult(true, string.Empty);
//     }
// 
//     /// <summary>
//     /// Unit tests for the <see cref = "CrashDumpEnvironmentVariableProvider"/> class.
//     /// </summary>
// //     [TestClass] [Error] (99-43)CS0518 Predefined type 'System.String' is not defined or imported [Error] (102-48)CS0518 Predefined type 'System.Boolean' is not defined or imported [Error] (103-48)CS0518 Predefined type 'System.Boolean' is not defined or imported [Error] (104-55)CS0246 The type or namespace name 'ITestApplicationModuleInfo' could not be found (are you missing a using directive or an assembly reference?) [Error] (105-102)CS1503 Argument 1: cannot convert from 'string' to '?' [Error] (112-43)CS0518 Predefined type 'System.String' is not defined or imported [Error] (113-29)CS0122 'ITestApplicationModuleInfo' is inaccessible due to its protection level
// //     public class CrashDumpEnvironmentVariableProviderTests
// //     {
// //         private const string CrashDumpOptionName = "CrashDump";
// //         private const string CrashDumpTypeOptionName = "CrashDumpType";
// //         private const string CrashDumpFileNameOptionName = "CrashDumpFileName";
// //         private readonly Mock<IConfiguration> _configurationMock;
// //         private readonly Mock<ICommandLineOptions> _commandLineOptionsMock;
// //         private readonly Mock<ITestApplicationModuleInfo> _testApplicationModuleInfoMock; [Error] (89-31)CS0246 The type or namespace name 'ITestApplicationModuleInfo' could not be found (are you missing a using directive or an assembly reference?)
//         private readonly CrashDumpConfiguration _crashDumpConfig;
//         private readonly Mock<ILoggerFactory> _loggerFactoryMock;
//         private readonly Mock<ILogger<CrashDumpEnvironmentVariableProvider>> _loggerMock;
//         private readonly string _testResultDirectory = "TestResultDir";
//         private readonly string _testAppPath = @"C:\Test\app.exe";
//         private readonly CrashDumpEnvironmentVariableProvider _provider;
//         public CrashDumpEnvironmentVariableProviderTests()
//         {
//             _configurationMock = new Mock<IConfiguration>();
//             _configurationMock.Setup(c => c.GetTestResultDirectory()).Returns(_testResultDirectory);
//             _commandLineOptionsMock = new Mock<ICommandLineOptions>();
//             // By default, set option to false; individual tests will adjust behavior.
//             _commandLineOptionsMock.Setup(c => c.IsOptionSet(It.IsAny<string>())).Returns(false);
//             _commandLineOptionsMock.Setup(c => c.TryGetOptionArgumentList(It.IsAny<string>(), out It.Ref<string[]?>.IsAny)).Returns(false);
//             _testApplicationModuleInfoMock = new Mock<ITestApplicationModuleInfo>();
//             _testApplicationModuleInfoMock.Setup(t => t.GetCurrentTestApplicationFullPath()).Returns(_testAppPath);
//             _crashDumpConfig = new CrashDumpConfiguration
//             {
//                 Enable = true
//             };
//             _loggerMock = new Mock<ILogger<CrashDumpEnvironmentVariableProvider>>();
//             _loggerFactoryMock = new Mock<ILoggerFactory>();
//             _loggerFactoryMock.Setup(l => l.CreateLogger(It.IsAny<string>())).Returns(_loggerMock.Object);
//             _provider = new CrashDumpEnvironmentVariableProvider(_configurationMock.Object, _commandLineOptionsMock.Object, _testApplicationModuleInfoMock.Object, _crashDumpConfig, _loggerFactoryMock.Object);
//         }
// 
//         /// <summary>
//         /// Tests IsEnabledAsync returns true when the CrashDump option is set and CrashDumpConfiguration.Enable is true.
//         /// </summary>
// //         [TestMethod] [Error] (123-48)CS0518 Predefined type 'System.Boolean' is not defined or imported
// //         public async Task IsEnabledAsync_OptionSetAndEnabled_ReturnsTrue()
// //         {
// //             // Arrange
// //             _commandLineOptionsMock.Setup(c => c.IsOptionSet(CrashDumpOptionName)).Returns(true);
// //             _crashDumpConfig.Enable = true;
// //             // Act
// //             bool isEnabled = await _provider.IsEnabledAsync();
// //             // Assert
// //             Assert.IsTrue(isEnabled, "Expected IsEnabledAsync to return true when option is set and configuration is enabled.");
// //         }
// 
//         /// <summary>
//         /// Tests IsEnabledAsync returns false when the CrashDump option is not set.
//         /// </summary>
// //         [TestMethod] [Error] (138-48)CS0518 Predefined type 'System.Boolean' is not defined or imported
// //         public async Task IsEnabledAsync_OptionNotSet_ReturnsFalse()
// //         {
// //             // Arrange
// //             _commandLineOptionsMock.Setup(c => c.IsOptionSet(CrashDumpOptionName)).Returns(false);
// //             _crashDumpConfig.Enable = true;
// //             // Act
// //             bool isEnabled = await _provider.IsEnabledAsync();
// //             // Assert
// //             Assert.IsFalse(isEnabled, "Expected IsEnabledAsync to return false when the CrashDump option is not set.");
// //         }
// 
//         /// <summary>
//         /// Tests IsEnabledAsync returns false when the CrashDumpConfiguration.Enable is false.
//         /// </summary>
// //         [TestMethod] [Error] (153-48)CS0518 Predefined type 'System.Boolean' is not defined or imported
// //         public async Task IsEnabledAsync_ConfigurationDisabled_ReturnsFalse()
// //         {
// //             // Arrange
// //             _commandLineOptionsMock.Setup(c => c.IsOptionSet(CrashDumpOptionName)).Returns(true);
// //             _crashDumpConfig.Enable = false;
// //             // Act
// //             bool isEnabled = await _provider.IsEnabledAsync();
// //             // Assert
// //             Assert.IsFalse(isEnabled, "Expected IsEnabledAsync to return false when configuration is disabled.");
// //         }
// 
//         /// <summary>
//         /// Tests UpdateAsync sets expected environment variables with default settings when no dump type or file name options are provided.
//         /// </summary>
// //         [TestMethod] [Error] (169-48)CS0518 Predefined type 'System.Boolean' is not defined or imported [Error] (170-48)CS0518 Predefined type 'System.Boolean' is not defined or imported
// //         public async Task UpdateAsync_DefaultSettings_SetsExpectedVariables()
// //         {
// //             // Arrange
// //             // No command line arguments for crash dump type or file name.
// //             _commandLineOptionsMock.Setup(c => c.TryGetOptionArgumentList(CrashDumpTypeOptionName, out It.Ref<string[]?>.IsAny)).Returns(false);
// //             _commandLineOptionsMock.Setup(c => c.TryGetOptionArgumentList(CrashDumpFileNameOptionName, out It.Ref<string[]?>.IsAny)).Returns(false);
// //             var fakeEnv = new FakeEnvironmentVariables();
// //             // Act
// //             await _provider.UpdateAsync(fakeEnv);
// //             // Expected file name is computed as:
// //             // Path.Combine(_testResultDirectory, $"{Path.GetFileName(_testAppPath)}_%p_crash.dmp")
// //             string expectedFileName = Path.Combine(_testResultDirectory, $"{Path.GetFileName(_testAppPath)}_%p_crash.dmp");
// //             // Default mini dump type is "4".
// //             string expectedMiniDumpType = "4";
// //             string expectedEnableValue = "1";
// //             // Assert
// //             string[] prefixes = new[]
// //             {
// //                 "DOTNET_",
// //                 "COMPlus_"
// //             };
// //             foreach (string prefix in prefixes)
// //             {
// //                 // Enable variables
// //                 Assert.IsTrue(fakeEnv.TryGetVariable($"{prefix}DbgEnableMiniDump", out OwnedEnvironmentVariable enableVar), $"Expected variable {prefix}DbgEnableMiniDump to be set.");
// //                 Assert.AreEqual(expectedEnableValue, enableVar.Value, $"Expected {prefix}DbgEnableMiniDump to be '{expectedEnableValue}'.");
// //                 Assert.IsTrue(fakeEnv.TryGetVariable($"{prefix}CreateDumpDiagnostics", out OwnedEnvironmentVariable diagVar), $"Expected variable {prefix}CreateDumpDiagnostics to be set.");
// //                 Assert.AreEqual(expectedEnableValue, diagVar.Value, $"Expected {prefix}CreateDumpDiagnostics to be '{expectedEnableValue}'.");
// //                 Assert.IsTrue(fakeEnv.TryGetVariable($"{prefix}CreateDumpVerboseDiagnostics", out OwnedEnvironmentVariable verboseVar), $"Expected variable {prefix}CreateDumpVerboseDiagnostics to be set.");
// //                 Assert.AreEqual(expectedEnableValue, verboseVar.Value, $"Expected {prefix}CreateDumpVerboseDiagnostics to be '{expectedEnableValue}'.");
// //                 // MiniDumpType set to default
// //                 Assert.IsTrue(fakeEnv.TryGetVariable($"{prefix}DbgMiniDumpType", out OwnedEnvironmentVariable miniDumpTypeVar), $"Expected variable {prefix}DbgMiniDumpType to be set.");
// //                 Assert.AreEqual(expectedMiniDumpType, miniDumpTypeVar.Value, $"Expected {prefix}DbgMiniDumpType to be '{expectedMiniDumpType}'.");
// //                 // MiniDumpName set correctly
// //                 Assert.IsTrue(fakeEnv.TryGetVariable($"{prefix}DbgMiniDumpName", out OwnedEnvironmentVariable miniDumpNameVar), $"Expected variable {prefix}DbgMiniDumpName to be set.");
// //                 Assert.AreEqual(expectedFileName, miniDumpNameVar.Value, $"Expected {prefix}DbgMiniDumpName to be '{expectedFileName}'.");
// //             }
// //         }
// 
//         /// <summary>
//         /// Tests UpdateAsync sets the MiniDumpType to the corresponding value when the crash dump type option is provided as "mini".
//         /// </summary>
// //         [TestMethod] [Error] (215-48)CS0518 Predefined type 'System.Boolean' is not defined or imported
// //         public async Task UpdateAsync_WithCrashDumpTypeOption_SetsExpectedMiniDumpType()
// //         {
// //             // Arrange
// //             string[] dumpTypeArgs = new[]
// //             {
// //                 "mini"
// //             };
// //             _commandLineOptionsMock.Setup(c => c.TryGetOptionArgumentList(CrashDumpTypeOptionName, out dumpTypeArgs)).Returns(true);
// //             var fakeEnv = new FakeEnvironmentVariables();
// //             // Act
// //             await _provider.UpdateAsync(fakeEnv);
// //             // Assert: When "mini" is provided, the expected mini dump type value is "1".
// //             string[] prefixes = new[]
// //             {
// //                 "DOTNET_",
// //                 "COMPlus_"
// //             };
// //             foreach (string prefix in prefixes)
// //             {
// //                 Assert.IsTrue(fakeEnv.TryGetVariable($"{prefix}DbgMiniDumpType", out OwnedEnvironmentVariable miniDumpTypeVar), $"Expected variable {prefix}DbgMiniDumpType to be set.");
// //                 Assert.AreEqual("1", miniDumpTypeVar.Value, $"Expected {prefix}DbgMiniDumpType to be '1' when dump type 'mini' is specified.");
// //             }
// //         }
// 
//         /// <summary>
//         /// Tests UpdateAsync sets the MiniDumpName based on the provided crash dump file name option.
//         /// </summary>
// //         [TestMethod] [Error] (243-48)CS0518 Predefined type 'System.Boolean' is not defined or imported
// //         public async Task UpdateAsync_WithCrashDumpFileNameOption_SetsExpectedFileName()
// //         {
// //             // Arrange
// //             string[] dumpFileNameArgs = new[]
// //             {
// //                 "custom.dmp"
// //             };
// //             _commandLineOptionsMock.Setup(c => c.TryGetOptionArgumentList(CrashDumpFileNameOptionName, out dumpFileNameArgs)).Returns(true);
// //             var fakeEnv = new FakeEnvironmentVariables();
// //             // Act
// //             await _provider.UpdateAsync(fakeEnv);
// //             // Expected file name is computed as:
// //             // Path.Combine(_testResultDirectory, "custom.dmp")
// //             string expectedFileName = Path.Combine(_testResultDirectory, "custom.dmp");
// //             // Assert
// //             string[] prefixes = new[]
// //             {
// //                 "DOTNET_",
// //                 "COMPlus_"
// //             };
// //             foreach (string prefix in prefixes)
// //             {
// //                 Assert.IsTrue(fakeEnv.TryGetVariable($"{prefix}DbgMiniDumpName", out OwnedEnvironmentVariable miniDumpNameVar), $"Expected variable {prefix}DbgMiniDumpName to be set.");
// //                 Assert.AreEqual(expectedFileName, miniDumpNameVar.Value, $"Expected {prefix}DbgMiniDumpName to be '{expectedFileName}'.");
// //             }
// //         }
// 
//         /// <summary>
//         /// Tests ValidateTestHostEnvironmentVariablesAsync returns a valid result when environment variables are correctly set.
//         /// </summary>
// //         [TestMethod] [Error] (276-44)CS0518 Predefined type 'System.Boolean' is not defined or imported
// //         public async Task ValidateTestHostEnvironmentVariablesAsync_ValidEnvironment_ReturnsValid()
// //         {
// //             // Arrange
// //             var fakeEnv = new FakeEnvironmentVariables();
// //             // Use UpdateAsync to set all the expected variables and initialize _miniDumpNameValue.
// //             await _provider.UpdateAsync(fakeEnv);
// //             // Act
// //             var validationResult = await _provider.ValidateTestHostEnvironmentVariablesAsync(fakeEnv);
// //             // Assert
// //             Assert.IsTrue(validationResult.IsValid, "Expected ValidateTestHostEnvironmentVariablesAsync to return a valid result when environment is correctly configured.");
// //         }
// 
//         /// <summary>
//         /// Tests ValidateTestHostEnvironmentVariablesAsync returns an invalid result when a required enable mini dump environment variable is missing.
//         /// </summary>
// //         [TestMethod] [Error] (292-18)CS7036 There is no argument given that corresponds to the required parameter 'value' of 'CollectionExtensions.Remove<TKey, TValue>(IDictionary<TKey, TValue>, TKey, out TValue)' [Error] (296-45)CS0518 Predefined type 'System.Boolean' is not defined or imported [Error] (297-47)CS0518 Predefined type 'System.Nullable`1' is not defined or imported [Error] (298-52)CS0518 Predefined type 'System.Nullable`1' is not defined or imported
// //         public async Task ValidateTestHostEnvironmentVariablesAsync_InvalidEnvironment_MissingEnableMiniDump_ReturnsInvalid()
// //         {
// //             // Arrange
// //             var fakeEnv = new FakeEnvironmentVariables();
// //             await _provider.UpdateAsync(fakeEnv);
// //             // Remove one of the required variables: remove "DOTNET_DbgEnableMiniDump".
// //             // Simulate missing variable by not having it in the fake environment.
// //             // Remove the variable if exists.
// //             var dict = ((FakeEnvironmentVariables)fakeEnv).Variables;
// //             dict.Remove("DOTNET_DbgEnableMiniDump");
// //             // Act
// //             var validationResult = await _provider.ValidateTestHostEnvironmentVariablesAsync(fakeEnv);
// //             // Assert
// //             Assert.IsFalse(validationResult.IsValid, "Expected ValidateTestHostEnvironmentVariablesAsync to return an invalid result when a required variable is missing.");
// //             Assert.IsNotNull(validationResult.ErrorMessage);
// //             StringAssert.Contains(validationResult.ErrorMessage, "DOTNET_DbgEnableMiniDump", "Error message should mention the missing variable.");
// //         }
// 
//         /// <summary>
//         /// Tests ValidateTestHostEnvironmentVariablesAsync returns an invalid result when an environment variable has an invalid mini dump type value.
//         /// </summary>
// //         [TestMethod] [Error] (316-45)CS0518 Predefined type 'System.Boolean' is not defined or imported [Error] (317-47)CS0518 Predefined type 'System.Nullable`1' is not defined or imported [Error] (318-52)CS0518 Predefined type 'System.Nullable`1' is not defined or imported
// //         public async Task ValidateTestHostEnvironmentVariablesAsync_InvalidEnvironment_InvalidMiniDumpType_ReturnsInvalid()
// //         {
// //             // Arrange
// //             var fakeEnv = new FakeEnvironmentVariables();
// //             await _provider.UpdateAsync(fakeEnv);
// //             // For one prefix, set an invalid mini dump type value.
// //             string invalidValue = "invalid";
// //             fakeEnv.SetVariable(new OwnedEnvironmentVariable("DOTNET_DbgMiniDumpType", invalidValue, false, true));
// //             // Act
// //             var validationResult = await _provider.ValidateTestHostEnvironmentVariablesAsync(fakeEnv);
// //             // Assert
// //             Assert.IsFalse(validationResult.IsValid, "Expected ValidateTestHostEnvironmentVariablesAsync to return an invalid result when mini dump type value is invalid.");
// //             Assert.IsNotNull(validationResult.ErrorMessage);
// //             StringAssert.Contains(validationResult.ErrorMessage, "DOTNET_DbgMiniDumpType", "Error message should mention the invalid mini dump type variable.");
// //         }
//     }
// }
