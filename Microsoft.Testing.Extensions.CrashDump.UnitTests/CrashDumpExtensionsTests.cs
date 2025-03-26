// using System;
// using System.Collections.Generic;
// using System.Reflection;
// using Microsoft.Testing.Extensions;
// using Microsoft.Testing.Platform.Builder;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Moq;
// 
// namespace Microsoft.Testing.Extensions.UnitTests
// {
//     /// <summary>
//     /// Unit tests for the <see cref="CrashDumpExtensions"/> class.
//     /// </summary>
//     [TestClass]
//     public class CrashDumpExtensionsTests
//     {
//         private readonly FakeTestApplicationBuilder _fakeBuilder;
// 
//         /// <summary>
//         /// Initializes a new instance of the <see cref="CrashDumpExtensionsTests"/> class.
//         /// </summary>
//         public CrashDumpExtensionsTests()
//         {
//             _fakeBuilder = new FakeTestApplicationBuilder();
//         }
// 
//         /// <summary>
//         /// Tests that AddCrashDumpProvider registers the environment variable provider, process lifetime handler, and command line provider when ignoreIfNotSupported is false.
//         /// </summary>
//         [TestMethod]
//         public void AddCrashDumpProvider_WithIgnoreIfNotSupportedFalse_RegistersProviders()
//         {
//             // Arrange
//             // Using the fake builder that captures the provider registrations.
// 
//             // Act
//             CrashDumpExtensions.AddCrashDumpProvider(_fakeBuilder, ignoreIfNotSupported: false);
// 
//             // Assert
//             Assert.AreEqual(1, _fakeBuilder.TestHostControllers.EnvironmentVariableProviderFactories.Count,
//                 "Expected one environment variable provider to be registered.");
//             Assert.AreEqual(1, _fakeBuilder.TestHostControllers.ProcessLifetimeHandlerFactories.Count,
//                 "Expected one process lifetime handler to be registered.");
//             Assert.AreEqual(1, _fakeBuilder.CommandLine.ProviderFactories.Count,
//                 "Expected one command line provider to be registered.");
// 
//             // Additionally, invoke the registered factories with a fake service provider to verify they create the expected instances.
//             var fakeServiceProvider = new FakeServiceProvider();
// 
//             var envProviderFactory = _fakeBuilder.TestHostControllers.EnvironmentVariableProviderFactories[0];
//             var envProvider = envProviderFactory(fakeServiceProvider);
//             Assert.IsNotNull(envProvider, "The environment variable provider factory did not produce an instance.");
//             Assert.AreEqual("CrashDumpEnvironmentVariableProvider", envProvider.GetType().Name,
//                 "The created instance is not of type CrashDumpEnvironmentVariableProvider.");
// 
//             var procHandlerFactory = _fakeBuilder.TestHostControllers.ProcessLifetimeHandlerFactories[0];
//             var procHandler = procHandlerFactory(fakeServiceProvider);
//             Assert.IsNotNull(procHandler, "The process lifetime handler factory did not produce an instance.");
//             Assert.AreEqual("CrashDumpProcessLifetimeHandler", procHandler.GetType().Name,
//                 "The created instance is not of type CrashDumpProcessLifetimeHandler.");
// 
//             var cmdLineFactory = _fakeBuilder.CommandLine.ProviderFactories[0];
//             var cmdLineProvider = cmdLineFactory();
//             Assert.IsNotNull(cmdLineProvider, "The command line provider factory did not produce an instance.");
//             Assert.AreEqual("CrashDumpCommandLineProvider", cmdLineProvider.GetType().Name,
//                 "The created instance is not of type CrashDumpCommandLineProvider.");
//         }
// 
//         /// <summary>
//         /// Tests that AddCrashDumpProvider registers the providers when ignoreIfNotSupported is true.
//         /// </summary>
//         [TestMethod]
//         public void AddCrashDumpProvider_WithIgnoreIfNotSupportedTrue_RegistersProviders()
//         {
//             // Arrange
//             // Clear any previous registrations.
//             _fakeBuilder.TestHostControllers.Clear();
//             _fakeBuilder.CommandLine.Clear();
// 
//             // Act
//             CrashDumpExtensions.AddCrashDumpProvider(_fakeBuilder, ignoreIfNotSupported: true);
// 
//             // Assert
//             Assert.AreEqual(1, _fakeBuilder.TestHostControllers.EnvironmentVariableProviderFactories.Count,
//                 "Expected one environment variable provider to be registered when ignoreIfNotSupported is true.");
//             Assert.AreEqual(1, _fakeBuilder.TestHostControllers.ProcessLifetimeHandlerFactories.Count,
//                 "Expected one process lifetime handler to be registered when ignoreIfNotSupported is true.");
//             Assert.AreEqual(1, _fakeBuilder.CommandLine.ProviderFactories.Count,
//                 "Expected one command line provider to be registered when ignoreIfNotSupported is true.");
// 
//             // Additionally, invoke one of the factories to verify if the CrashDumpConfiguration's Enable flag is set as expected.
//             var fakeServiceProvider = new FakeServiceProvider();
//             var envProvider = _fakeBuilder.TestHostControllers.EnvironmentVariableProviderFactories[0](fakeServiceProvider);
//             Assert.IsNotNull(envProvider, "The environment variable provider factory did not produce an instance.");
// 
//             // Use reflection to get the crashDumpConfiguration field from the created provider.
//             FieldInfo configField = envProvider.GetType().GetField("crashDumpGeneratorConfiguration", BindingFlags.Instance | BindingFlags.NonPublic);
//             Assert.IsNotNull(configField, "Could not find the crashDumpGeneratorConfiguration field via reflection.");
//             object configValue = configField.GetValue(envProvider);
//             Assert.IsNotNull(configValue, "The crashDumpGeneratorConfiguration field is null.");
// 
//             PropertyInfo enableProperty = configValue.GetType().GetProperty("Enable", BindingFlags.Instance | BindingFlags.Public);
//             Assert.IsNotNull(enableProperty, "Could not find the Enable property on CrashDumpConfiguration.");
//             bool enableFlag = (bool)enableProperty.GetValue(configValue);
//             
//             // The expected value of Enable depends on the compilation constant. If not NETCOREAPP then it should be false.
//             // Since unit tests typically run on .NET Core, we assume NETCOREAPP is defined and thus the flag remains default.
// #if NETCOREAPP
//             bool expectedEnable = true;
// #else
//             bool expectedEnable = false;
// #endif
//             Assert.AreEqual(expectedEnable, enableFlag, $"Expected CrashDumpConfiguration.Enable to be {expectedEnable} when ignoreIfNotSupported is true.");
//         }
// 
//         /// <summary>
//         /// Tests that calling AddCrashDumpProvider with a null builder throws a NullReferenceException.
//         /// </summary>
//         [TestMethod]
//         public void AddCrashDumpProvider_NullBuilder_ThrowsNullReferenceException()
//         {
//             // Arrange
//             ITestApplicationBuilder nullBuilder = null;
// 
//             // Act & Assert
//             Assert.ThrowsException<NullReferenceException>(() => CrashDumpExtensions.AddCrashDumpProvider(nullBuilder),
//                 "Expected a NullReferenceException when null is passed as the builder.");
//         }
//     }
// 
//     /// <summary>
//     /// A fake implementation of ITestApplicationBuilder for unit testing purposes.
//     /// </summary>
// //     internal class FakeTestApplicationBuilder : ITestApplicationBuilder [Error] (134-49)CS0535 'FakeTestApplicationBuilder' does not implement interface member 'ITestApplicationBuilder.TestHost' [Error] (134-49)CS0738 'FakeTestApplicationBuilder' does not implement interface member 'ITestApplicationBuilder.TestHostControllers'. 'FakeTestApplicationBuilder.TestHostControllers' cannot implement 'ITestApplicationBuilder.TestHostControllers' because it does not have the matching return type of 'ITestHostControllersManager'. [Error] (134-49)CS0738 'FakeTestApplicationBuilder' does not implement interface member 'ITestApplicationBuilder.CommandLine'. 'FakeTestApplicationBuilder.CommandLine' cannot implement 'ITestApplicationBuilder.CommandLine' because it does not have the matching return type of 'ICommandLineManager'. [Error] (134-49)CS0535 'FakeTestApplicationBuilder' does not implement interface member 'ITestApplicationBuilder.Configuration' [Error] (134-49)CS0535 'FakeTestApplicationBuilder' does not implement interface member 'ITestApplicationBuilder.Logging' [Error] (134-49)CS0246 The type or namespace name 'Func<,>' could not be found (are you missing a using directive or an assembly reference?) [Error] (134-49)CS0246 The type or namespace name 'Task<>' could not be found (are you missing a using directive or an assembly reference?)
// //     {
// //         public FakeTestHostControllers TestHostControllers { get; }
// //         public FakeCommandLine CommandLine { get; }
// // 
// //         public FakeTestApplicationBuilder()
// //         {
// //             TestHostControllers = new FakeTestHostControllers();
// //             CommandLine = new FakeCommandLine();
// //         }
// //     }
// 
//     /// <summary>
//     /// A fake implementation of the test host controllers to capture provider registrations.
//     /// </summary>
// //     internal class FakeTestHostControllers [Error] (159-84)CS0246 The type or namespace name 'CrashDumpEnvironmentVariableProvider' could not be found (are you missing a using directive or an assembly reference?) [Error] (160-79)CS0246 The type or namespace name 'CrashDumpProcessLifetimeHandler' could not be found (are you missing a using directive or an assembly reference?)
// //     {
// //         public List<Func<IServiceProvider, object>> GenericEnvironmentVariableProviderFactories { get; }
// //         public List<Func<IServiceProvider, object>> GenericProcessLifetimeHandlerFactories { get; }
// // 
// //         public List<Func<IServiceProvider, CrashDumpEnvironmentVariableProvider>> EnvironmentVariableProviderFactories { get; } [Error] (154-44)CS0246 The type or namespace name 'CrashDumpEnvironmentVariableProvider' could not be found (are you missing a using directive or an assembly reference?)
// //         public List<Func<IServiceProvider, CrashDumpProcessLifetimeHandler>> ProcessLifetimeHandlerFactories { get; } [Error] (155-44)CS0246 The type or namespace name 'CrashDumpProcessLifetimeHandler' could not be found (are you missing a using directive or an assembly reference?)
// 
//         public FakeTestHostControllers()
//         {
//             EnvironmentVariableProviderFactories = new List<Func<IServiceProvider, CrashDumpEnvironmentVariableProvider>>();
//             ProcessLifetimeHandlerFactories = new List<Func<IServiceProvider, CrashDumpProcessLifetimeHandler>>();
//             GenericEnvironmentVariableProviderFactories = new List<Func<IServiceProvider, object>>();
//             GenericProcessLifetimeHandlerFactories = new List<Func<IServiceProvider, object>>();
//         }
// 
// //         public void AddEnvironmentVariableProvider(Func<IServiceProvider, CrashDumpEnvironmentVariableProvider> factory) [Error] (165-75)CS0246 The type or namespace name 'CrashDumpEnvironmentVariableProvider' could not be found (are you missing a using directive or an assembly reference?)
// //         {
// //             EnvironmentVariableProviderFactories.Add(factory);
// //             GenericEnvironmentVariableProviderFactories.Add(sp => factory(sp));
// //         }
// 
// //         public void AddProcessLifetimeHandler(Func<IServiceProvider, CrashDumpProcessLifetimeHandler> factory) [Error] (171-70)CS0246 The type or namespace name 'CrashDumpProcessLifetimeHandler' could not be found (are you missing a using directive or an assembly reference?)
// //         {
// //             ProcessLifetimeHandlerFactories.Add(factory);
// //             GenericProcessLifetimeHandlerFactories.Add(sp => factory(sp));
// //         }
// 
//         public void Clear()
//         {
//             EnvironmentVariableProviderFactories.Clear();
//             ProcessLifetimeHandlerFactories.Clear();
//             GenericEnvironmentVariableProviderFactories.Clear();
//             GenericProcessLifetimeHandlerFactories.Clear();
//         }
//     }
// 
//     /// <summary>
//     /// A fake implementation of the command line to capture provider registrations.
//     /// </summary>
// //     internal class FakeCommandLine [Error] (195-47)CS0246 The type or namespace name 'CrashDumpCommandLineProvider' could not be found (are you missing a using directive or an assembly reference?)
// //     {
// //         public List<Func<CrashDumpCommandLineProvider>> ProviderFactories { get; } [Error] (191-26)CS0246 The type or namespace name 'CrashDumpCommandLineProvider' could not be found (are you missing a using directive or an assembly reference?)
// 
//         public FakeCommandLine()
//         {
//             ProviderFactories = new List<Func<CrashDumpCommandLineProvider>>();
//         }
// 
// //         public void AddProvider(Func<CrashDumpCommandLineProvider> factory) [Error] (198-38)CS0246 The type or namespace name 'CrashDumpCommandLineProvider' could not be found (are you missing a using directive or an assembly reference?)
// //         {
// //             ProviderFactories.Add(factory);
// //         }
// 
//         public void Clear()
//         {
//             ProviderFactories.Clear();
//         }
//     }
// 
//     /// <summary>
//     /// A fake implementation of IServiceProvider and the required extension methods for unit testing.
//     /// </summary>
//     internal class FakeServiceProvider : IServiceProvider
//     {
//         public object GetService(Type serviceType)
//         {
//             return new object();
//         }
//     }
// 
//     /// <summary>
//     /// Extension methods for FakeServiceProvider to simulate service retrieval.
//     /// </summary>
//     internal static class FakeServiceProviderExtensions
//     {
//         public static object GetConfiguration(this IServiceProvider serviceProvider)
//         {
//             return new object();
//         }
// 
//         public static object GetCommandLineOptions(this IServiceProvider serviceProvider)
//         {
//             return new object();
//         }
// 
//         public static object GetTestApplicationModuleInfo(this IServiceProvider serviceProvider)
//         {
//             return new object();
//         }
// 
//         public static object GetLoggerFactory(this IServiceProvider serviceProvider)
//         {
//             return new object();
//         }
// 
//         public static object GetMessageBus(this IServiceProvider serviceProvider)
//         {
//             return new object();
//         }
// 
//         public static object GetOutputDevice(this IServiceProvider serviceProvider)
//         {
//             return new object();
//         }
//     }
// }
