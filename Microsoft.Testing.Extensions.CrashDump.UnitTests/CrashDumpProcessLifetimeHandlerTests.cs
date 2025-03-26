// using Microsoft.Testing.Extensions.Diagnostics;
// using Microsoft.Testing.Extensions.Diagnostics.Resources;
// using Microsoft.Testing.Platform.CommandLine;
// using Microsoft.Testing.Platform.Extensions.Messages;
// using Microsoft.Testing.Platform.Extensions.OutputDevice;
// using Microsoft.Testing.Platform.Extensions.TestHostControllers;
// using Microsoft.Testing.Platform.Helpers;
// using Microsoft.Testing.Platform.Messages;
// using Microsoft.Testing.Platform.OutputDevice;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Moq;
// using System;
// using System.Globalization;
// using System.IO;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
// 
// namespace Microsoft.Testing.Extensions.Diagnostics.UnitTests
// {
//     /// <summary>
//     /// Unit tests for the <see cref="CrashDumpProcessLifetimeHandler"/> class.
//     /// </summary>
//     [TestClass]
//     public class CrashDumpProcessLifetimeHandlerTests
//     {
//         private readonly Mock<ICommandLineOptions> _commandLineOptionsMock;
//         private readonly Mock<IMessageBus> _messageBusMock;
//         private readonly Mock<IOutputDevice> _outputDisplayMock;
// 
//         public CrashDumpProcessLifetimeHandlerTests()
//         {
//             _commandLineOptionsMock = new Mock<ICommandLineOptions>();
//             _messageBusMock = new Mock<IMessageBus>();
//             _outputDisplayMock = new Mock<IOutputDevice>();
//         }
// 
//         /// <summary>
//         /// Creates an instance of CrashDumpConfiguration for testing purposes.
//         /// </summary>
//         /// <param name="enable">Whether crash dump is enabled.</param>
//         /// <param name="dumpFileNamePattern">The dump file name pattern.</param>
//         /// <returns>A new instance of CrashDumpConfiguration.</returns>
//         private CrashDumpConfiguration CreateCrashDumpConfiguration(bool enable, string dumpFileNamePattern)
//         {
//             return new CrashDumpConfiguration
//             {
//                 Enable = enable,
//                 DumpFileNamePattern = dumpFileNamePattern
//             };
//         }
// 
//         /// <summary>
//         /// Creates an instance of CrashDumpProcessLifetimeHandler with provided configuration.
//         /// </summary>
//         private CrashDumpProcessLifetimeHandler CreateHandler(CrashDumpConfiguration configuration)
//         {
//             return new CrashDumpProcessLifetimeHandler(_commandLineOptionsMock.Object, _messageBusMock.Object, _outputDisplayMock.Object, configuration);
//         }
// 
//         /// <summary>
//         /// Creates a mock of ITestHostProcessInformation with specified PID and graceful exit flag.
//         /// </summary>
// //         private Mock<ITestHostProcessInformation> CreateTestHostProcessInformation(int pid, bool hasExitedGracefully) [Error] (67-34)CS0518 Predefined type 'System.Int32' is not defined or imported [Error] (67-39)CS0518 Predefined type 'System.Int32' is not defined or imported [Error] (68-34)CS0518 Predefined type 'System.Boolean' is not defined or imported [Error] (68-55)CS0518 Predefined type 'System.Boolean' is not defined or imported
// //         {
// //             var mock = new Mock<ITestHostProcessInformation>();
// //             mock.SetupGet(x => x.PID).Returns(pid);
// //             mock.SetupGet(x => x.HasExitedGracefully).Returns(hasExitedGracefully);
// //             return mock;
// //         }
// 
//         /// <summary>
//         /// Tests that IsEnabledAsync returns true when the command line option is set and crash dump is enabled.
//         /// </summary>
// //         [TestMethod] [Error] (79-48)CS0518 Predefined type 'System.Boolean' is not defined or imported
// //         public async Task IsEnabledAsync_OptionSetAndEnabled_ReturnsTrue()
// //         {
// //             // Arrange
// //             _commandLineOptionsMock.Setup(x => x.IsOptionSet(CrashDumpCommandLineOptions.CrashDumpOptionName)).Returns(true);
// //             var config = CreateCrashDumpConfiguration(true, "dummy_%p.dmp");
// //             var handler = CreateHandler(config);
// // 
// //             // Act
// //             bool enabled = await handler.IsEnabledAsync();
// // 
// //             // Assert
// //             Assert.IsTrue(enabled, "Expected IsEnabledAsync to return true when option is set and configuration is enabled.");
// //         }
// 
//         /// <summary>
//         /// Tests that IsEnabledAsync returns false when the command line option is not set.
//         /// </summary>
// //         [TestMethod] [Error] (97-48)CS0518 Predefined type 'System.Boolean' is not defined or imported
// //         public async Task IsEnabledAsync_OptionNotSet_ReturnsFalse()
// //         {
// //             // Arrange
// //             _commandLineOptionsMock.Setup(x => x.IsOptionSet(CrashDumpCommandLineOptions.CrashDumpOptionName)).Returns(false);
// //             var config = CreateCrashDumpConfiguration(true, "dummy_%p.dmp");
// //             var handler = CreateHandler(config);
// // 
// //             // Act
// //             bool enabled = await handler.IsEnabledAsync();
// // 
// //             // Assert
// //             Assert.IsFalse(enabled, "Expected IsEnabledAsync to return false when the command line option is not set.");
// //         }
// 
//         /// <summary>
//         /// Tests that IsEnabledAsync returns false when the crash dump configuration is disabled.
//         /// </summary>
// //         [TestMethod] [Error] (115-48)CS0518 Predefined type 'System.Boolean' is not defined or imported
// //         public async Task IsEnabledAsync_DisabledConfiguration_ReturnsFalse()
// //         {
// //             // Arrange
// //             _commandLineOptionsMock.Setup(x => x.IsOptionSet(CrashDumpCommandLineOptions.CrashDumpOptionName)).Returns(true);
// //             var config = CreateCrashDumpConfiguration(false, "dummy_%p.dmp");
// //             var handler = CreateHandler(config);
// // 
// //             // Act
// //             bool enabled = await handler.IsEnabledAsync();
// // 
// //             // Assert
// //             Assert.IsFalse(enabled, "Expected IsEnabledAsync to return false when configuration is disabled.");
// //         }
// 
//         /// <summary>
//         /// Tests that BeforeTestHostProcessStartAsync completes without throwing.
//         /// </summary>
//         [TestMethod]
//         public async Task BeforeTestHostProcessStartAsync_Always_CompletesTask()
//         {
//             // Arrange
//             var config = CreateCrashDumpConfiguration(true, "dummy_%p.dmp");
//             var handler = CreateHandler(config);
//             using var cancellation = new CancellationTokenSource();
// 
//             // Act
//             Task task = handler.BeforeTestHostProcessStartAsync(cancellation.Token);
//             await task;
// 
//             // Assert
//             Assert.IsTrue(task.IsCompleted, "Expected BeforeTestHostProcessStartAsync to complete immediately.");
//         }
// 
//         /// <summary>
//         /// Tests that OnTestHostProcessStartedAsync completes without throwing.
//         /// </summary>
//         [TestMethod]
//         public async Task OnTestHostProcessStartedAsync_Always_CompletesTask()
//         {
//             // Arrange
//             var config = CreateCrashDumpConfiguration(true, "dummy_%p.dmp");
//             var handler = CreateHandler(config);
//             using var cancellation = new CancellationTokenSource();
//             var processInfoMock = CreateTestHostProcessInformation(1234, false);
// 
//             // Act
//             Task task = handler.OnTestHostProcessStartedAsync(processInfoMock.Object, cancellation.Token);
//             await task;
// 
//             // Assert
//             Assert.IsTrue(task.IsCompleted, "Expected OnTestHostProcessStartedAsync to complete immediately.");
//         }
// 
//         /// <summary>
//         /// Tests that OnTestHostProcessExitedAsync does nothing when cancellation is requested.
//         /// </summary>
// //         [TestMethod] [Error] (184-44)CS0246 The type or namespace name 'Task' could not be found (are you missing a using directive or an assembly reference?) [Error] (185-41)CS0246 The type or namespace name 'Task' could not be found (are you missing a using directive or an assembly reference?)
// //         public async Task OnTestHostProcessExitedAsync_WhenCancellationRequested_DoesNothing()
// //         {
// //             // Arrange
// //             // Set no process data in AppDomain to interfere.
// //             AppDomain.CurrentDomain.SetData("ProcessKilledByHangDump", null);
// //             var config = CreateCrashDumpConfiguration(true, "dummy_%p.dmp");
// //             var handler = CreateHandler(config);
// //             var processInfoMock = CreateTestHostProcessInformation(1234, false);
// //             using var cts = new CancellationTokenSource();
// //             cts.Cancel();
// // 
// //             // Act
// //             await handler.OnTestHostProcessExitedAsync(processInfoMock.Object, cts.Token);
// // 
// //             // Assert
// //             _outputDisplayMock.Verify(x => x.DisplayAsync(It.IsAny<object>(), It.IsAny<ErrorMessageOutputDeviceData>()), Times.Never);
// //             _messageBusMock.Verify(x => x.PublishAsync(It.IsAny<object>(), It.IsAny<FileArtifact>()), Times.Never);
// //         }
// 
//         /// <summary>
//         /// Tests that OnTestHostProcessExitedAsync does nothing when the process has exited gracefully.
//         /// </summary>
// //         [TestMethod] [Error] (205-44)CS0246 The type or namespace name 'Task' could not be found (are you missing a using directive or an assembly reference?) [Error] (206-41)CS0246 The type or namespace name 'Task' could not be found (are you missing a using directive or an assembly reference?)
// //         public async Task OnTestHostProcessExitedAsync_WhenHasExitedGracefully_DoesNothing()
// //         {
// //             // Arrange
// //             AppDomain.CurrentDomain.SetData("ProcessKilledByHangDump", null);
// //             var config = CreateCrashDumpConfiguration(true, "dummy_%p.dmp");
// //             var handler = CreateHandler(config);
// //             var processInfoMock = CreateTestHostProcessInformation(1234, true);
// //             using var cts = new CancellationTokenSource();
// // 
// //             // Act
// //             await handler.OnTestHostProcessExitedAsync(processInfoMock.Object, cts.Token);
// // 
// //             // Assert
// //             _outputDisplayMock.Verify(x => x.DisplayAsync(It.IsAny<object>(), It.IsAny<ErrorMessageOutputDeviceData>()), Times.Never);
// //             _messageBusMock.Verify(x => x.PublishAsync(It.IsAny<object>(), It.IsAny<FileArtifact>()), Times.Never);
// //         }
// 
//         /// <summary>
//         /// Tests that OnTestHostProcessExitedAsync does nothing when the ProcessKilledByHangDump flag is set to true.
//         /// </summary>
// //         [TestMethod] [Error] (226-44)CS0246 The type or namespace name 'Task' could not be found (are you missing a using directive or an assembly reference?) [Error] (227-41)CS0246 The type or namespace name 'Task' could not be found (are you missing a using directive or an assembly reference?)
// //         public async Task OnTestHostProcessExitedAsync_WhenProcessKilledByHangDumpTrue_DoesNothing()
// //         {
// //             // Arrange
// //             AppDomain.CurrentDomain.SetData("ProcessKilledByHangDump", "true");
// //             var config = CreateCrashDumpConfiguration(true, "dummy_%p.dmp");
// //             var handler = CreateHandler(config);
// //             var processInfoMock = CreateTestHostProcessInformation(1234, false);
// //             using var cts = new CancellationTokenSource();
// // 
// //             // Act
// //             await handler.OnTestHostProcessExitedAsync(processInfoMock.Object, cts.Token);
// // 
// //             // Assert
// //             _outputDisplayMock.Verify(x => x.DisplayAsync(It.IsAny<object>(), It.IsAny<ErrorMessageOutputDeviceData>()), Times.Never);
// //             _messageBusMock.Verify(x => x.PublishAsync(It.IsAny<object>(), It.IsAny<FileArtifact>()), Times.Never);
// //             AppDomain.CurrentDomain.SetData("ProcessKilledByHangDump", null);
// //         }
// 
//         /// <summary>
//         /// Tests that OnTestHostProcessExitedAsync publishes the expected dump file when it exists.
//         /// </summary>
// //         [TestMethod] [Error] (258-64)CS0518 Predefined type 'System.String' is not defined or imported [Error] (257-48)CS0246 The type or namespace name 'Task' could not be found (are you missing a using directive or an assembly reference?) [Error] (262-64)CS1061 'FileArtifact' does not contain a definition for 'File' and no accessible extension method 'File' accepting a first argument of type 'FileArtifact' could be found (are you missing a using directive or an assembly reference?) [Error] (263-31)CS0518 Predefined type 'System.String' is not defined or imported [Error] (264-31)CS0518 Predefined type 'System.Nullable`1' is not defined or imported [Error] (261-45)CS0246 The type or namespace name 'Task' could not be found (are you missing a using directive or an assembly reference?)
// //         public async Task OnTestHostProcessExitedAsync_WhenFileExists_PublishesExpectedDumpFile()
// //         {
// //             // Arrange
// //             AppDomain.CurrentDomain.SetData("ProcessKilledByHangDump", null);
// //             string tempDir = Path.GetTempPath();
// //             int pid = 1234;
// //             string dumpPattern = Path.Combine(tempDir, "test_dump_%p.dmp");
// //             string expectedDumpFile = dumpPattern.Replace("%p", pid.ToString(CultureInfo.InvariantCulture));
// // 
// //             // Ensure the expected dump file exists
// //             File.WriteAllText(expectedDumpFile, "dummy content");
// //             try
// //             {
// //                 var config = CreateCrashDumpConfiguration(true, dumpPattern);
// //                 var handler = CreateHandler(config);
// //                 var processInfoMock = CreateTestHostProcessInformation(pid, false);
// //                 using var cts = new CancellationTokenSource();
// // 
// //                 // Act
// //                 await handler.OnTestHostProcessExitedAsync(processInfoMock.Object, cts.Token);
// // 
// //                 // Assert
// //                 _outputDisplayMock.Verify(x => x.DisplayAsync(handler, 
// //                     It.Is<ErrorMessageOutputDeviceData>(d => d.Message.Contains(string.Format(CultureInfo.InvariantCulture, CrashDumpResources.CrashDumpProcessCrashedDumpFileCreated, pid)))),
// //                     Times.Once);
// // 
// //                 _messageBusMock.Verify(x => x.PublishAsync(handler,
// //                     It.Is<FileArtifact>(fa => string.Equals(fa.File.FullName, expectedDumpFile, StringComparison.InvariantCultureIgnoreCase)
// //                         && fa.DisplayName == CrashDumpResources.CrashDumpArtifactDisplayName
// //                         && fa.Description == CrashDumpResources.CrashDumpArtifactDescription)),
// //                     Times.Once);
// //             }
// //             finally
// //             {
// //                 if (File.Exists(expectedDumpFile))
// //                 {
// //                     File.Delete(expectedDumpFile);
// //                 }
// //             }
// //         }
// 
//         /// <summary>
//         /// Tests that OnTestHostProcessExitedAsync publishes all available dump files when the expected dump file does not exist.
//         /// </summary>
// //         [TestMethod] [Error] (309-48)CS0246 The type or namespace name 'Task' could not be found (are you missing a using directive or an assembly reference?) [Error] (314-64)CS1061 'FileArtifact' does not contain a definition for 'File' and no accessible extension method 'File' accepting a first argument of type 'FileArtifact' could be found (are you missing a using directive or an assembly reference?) [Error] (315-31)CS0518 Predefined type 'System.String' is not defined or imported [Error] (316-31)CS0518 Predefined type 'System.Nullable`1' is not defined or imported [Error] (313-45)CS0246 The type or namespace name 'Task' could not be found (are you missing a using directive or an assembly reference?)
// //         public async Task OnTestHostProcessExitedAsync_WhenFileDoesNotExist_PublishesAllAvailableDumpFiles()
// //         {
// //             // Arrange
// //             AppDomain.CurrentDomain.SetData("ProcessKilledByHangDump", null);
// //             string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
// //             Directory.CreateDirectory(tempDir);
// //             int pid = 5678;
// //             string dumpPattern = Path.Combine(tempDir, "test_dump_%p.dmp");
// //             string expectedDumpFile = dumpPattern.Replace("%p", pid.ToString(CultureInfo.InvariantCulture));
// //             // Ensure expected dump file does not exist.
// //             if (File.Exists(expectedDumpFile))
// //             {
// //                 File.Delete(expectedDumpFile);
// //             }
// //             // Create a dummy .dmp file in the directory.
// //             string dummyDumpFile = Path.Combine(tempDir, "other_dump.dmp");
// //             File.WriteAllText(dummyDumpFile, "dummy content");
// // 
// //             try
// //             {
// //                 var config = CreateCrashDumpConfiguration(true, dumpPattern);
// //                 var handler = CreateHandler(config);
// //                 var processInfoMock = CreateTestHostProcessInformation(pid, false);
// //                 using var cts = new CancellationTokenSource();
// // 
// //                 // Act
// //                 await handler.OnTestHostProcessExitedAsync(processInfoMock.Object, cts.Token);
// // 
// //                 // Assert: Verify that DisplayAsync was called twice.
// //                 _outputDisplayMock.Verify(x => x.DisplayAsync(handler,
// //                     It.IsAny<ErrorMessageOutputDeviceData>()), Times.Exactly(2));
// // 
// //                 // Verify that PublishAsync was called for the dummy .dmp file.
// //                 _messageBusMock.Verify(x => x.PublishAsync(handler,
// //                     It.Is<FileArtifact>(fa => string.Equals(fa.File.FullName, dummyDumpFile, StringComparison.InvariantCultureIgnoreCase)
// //                         && fa.DisplayName == CrashDumpResources.CrashDumpDisplayName
// //                         && fa.Description == CrashDumpResources.CrashDumpArtifactDescription)),
// //                     Times.Once);
// //             }
// //             finally
// //             {
// //                 if (Directory.Exists(tempDir))
// //                 {
// //                     Directory.Delete(tempDir, true);
// //                 }
// //             }
// //         }
// 
//         /// <summary>
//         /// Tests that OnTestHostProcessExitedAsync throws an exception when DumpFileNamePattern is null.
//         /// </summary>
//         [TestMethod]
//         public async Task OnTestHostProcessExitedAsync_WhenDumpFileNamePatternIsNull_ThrowsException()
//         {
//             // Arrange
//             AppDomain.CurrentDomain.SetData("ProcessKilledByHangDump", null);
//             var config = CreateCrashDumpConfiguration(true, null);
//             var handler = CreateHandler(config);
//             var processInfoMock = CreateTestHostProcessInformation(4321, false);
//             using var cts = new CancellationTokenSource();
// 
//             // Act & Assert
//             await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () => 
//             {
//                 await handler.OnTestHostProcessExitedAsync(processInfoMock.Object, cts.Token);
//             }, "Expected an exception when DumpFileNamePattern is null.");
//         }
// 
//         /// <summary>
//         /// Tests that the property getters return the expected values.
//         /// </summary>
// //         [TestMethod] [Error] (360-29)CS0122 'AppVersion' is inaccessible due to its protection level
// //         public void Properties_ReturnExpectedValues()
// //         {
// //             // Arrange
// //             var config = CreateCrashDumpConfiguration(true, "dummy_%p.dmp");
// //             var handler = CreateHandler(config);
// // 
// //             // Act & Assert
// //             Assert.AreEqual(nameof(CrashDumpProcessLifetimeHandler), handler.Uid, "Uid property did not return expected value.");
// //             Assert.AreEqual(AppVersion.DefaultSemVer, handler.Version, "Version property did not return expected value.");
// //             Assert.AreEqual(CrashDumpResources.CrashDumpDisplayName, handler.DisplayName, "DisplayName property did not return expected value.");
// //             Assert.AreEqual(CrashDumpResources.CrashDumpDescription, handler.Description, "Description property did not return expected value.");
// //             CollectionAssert.AreEqual(new Type[] { typeof(FileArtifact) }, handler.DataTypesProduced.ToArray(), "DataTypesProduced property did not return expected value.");
// //         }
//     }
// }
