// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TestFramework.ForTestingMSTest;

namespace Microsoft.VisualStudio.TestPlatform.TestFramework.UnitTests.Attributes;
public class DynamicDataAttributeTests : TestContainer
{
    private readonly DummyTestClass _dummyTestClass;
    private readonly MethodInfo _testMethodInfo;

    private DynamicDataAttribute _dynamicDataAttribute;

    public DynamicDataAttributeTests()
    {
        _dummyTestClass = new DummyTestClass();
        _testMethodInfo = _dummyTestClass.GetType().GetTypeInfo().GetDeclaredMethod("TestMethod1");
        _dynamicDataAttribute = new DynamicDataAttribute("ReusableTestDataProperty");
    }

    public void GetDataShouldThrowExceptionIfInvalidPropertyNameIsSpecifiedOrPropertyDoesNotExist()
    {
        Action action = () =>
        {
            _dynamicDataAttribute = new DynamicDataAttribute("ABC");
            _dynamicDataAttribute.GetData(_testMethodInfo);
        };

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    public void GetDataShouldReadDataFromProperty()
    {
        var methodInfo = _dummyTestClass.GetType().GetTypeInfo().GetDeclaredMethod("TestMethod1");
        _dynamicDataAttribute = new DynamicDataAttribute("ReusableTestDataProperty");
        var data = _dynamicDataAttribute.GetData(methodInfo);

        data.Should().HaveCount(2);
    }

    public void GetDataShouldReadDataFromPropertyInDifferentClass()
    {
        var methodInfo = _dummyTestClass.GetType().GetTypeInfo().GetDeclaredMethod("TestMethod1");
        _dynamicDataAttribute = new DynamicDataAttribute("ReusableTestDataProperty2", typeof(DummyTestClass2));
        var data = _dynamicDataAttribute.GetData(methodInfo);

        data.Should().HaveCount(2);
    }

    public void GetDataShouldReadDataFromMethod()
    {
        var methodInfo = _dummyTestClass.GetType().GetTypeInfo().GetDeclaredMethod("TestMethod2");
        _dynamicDataAttribute = new DynamicDataAttribute("ReusableTestDataMethod", DynamicDataSourceType.Method);
        var data = _dynamicDataAttribute.GetData(methodInfo);

        data.Should().HaveCount(2);
    }

    public void GetDataShouldReadDataFromMethodInDifferentClass()
    {
        var methodInfo = _dummyTestClass.GetType().GetTypeInfo().GetDeclaredMethod("TestMethod2");
        _dynamicDataAttribute = new DynamicDataAttribute("ReusableTestDataMethod2", typeof(DummyTestClass2), DynamicDataSourceType.Method);
        var data = _dynamicDataAttribute.GetData(methodInfo);

        data.Should().HaveCount(2);
    }

    public void GetDataShouldThrowExceptionIfPropertyReturnsNull()
    {
        Action action = () =>
        {
            var methodInfo = _dummyTestClass.GetType().GetTypeInfo().GetDeclaredMethod("TestMethod4");
            _dynamicDataAttribute = new DynamicDataAttribute("NullProperty", typeof(DummyTestClass));
            _dynamicDataAttribute.GetData(methodInfo);
        };

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    public void GetDataShouldThrowExceptionIfPropertyReturnsEmpty()
    {
        Action action = () =>
        {
            var methodInfo = _dummyTestClass.GetType().GetTypeInfo().GetDeclaredMethod("TestMethod5");
            _dynamicDataAttribute = new DynamicDataAttribute("EmptyProperty", typeof(DummyTestClass));
            _dynamicDataAttribute.GetData(methodInfo);
        };

        action.Should().ThrowExactly<ArgumentException>();
    }

    public void GetDataShouldThrowExceptionIfPropertyDoesNotReturnCorrectType()
    {
        Action action = () =>
        {
            var methodInfo = _dummyTestClass.GetType().GetTypeInfo().GetDeclaredMethod("TestMethod3");
            _dynamicDataAttribute = new DynamicDataAttribute("WrongDataTypeProperty", typeof(DummyTestClass));
            _dynamicDataAttribute.GetData(methodInfo);
        };

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    public void GetDisplayNameShouldReturnDisplayName()
    {
        var data = new object[] { 1, 2, 3 };

        var displayName = _dynamicDataAttribute.GetDisplayName(_testMethodInfo, data);
        displayName.Should().Be("TestMethod1 (1,2,3)");
    }

    public void GetDisplayNameShouldReturnDisplayNameWithDynamicDataDisplayName()
    {
        var data = new object[] { 1, 2, 3 };

        _dynamicDataAttribute.DynamicDataDisplayName = "GetCustomDynamicDataDisplayName";
        var displayName = _dynamicDataAttribute.GetDisplayName(_testMethodInfo, data);
        displayName.Should().Be("DynamicDataTestWithDisplayName TestMethod1 with 3 parameters");
    }

    public void GetDisplayNameShouldReturnDisplayNameWithDynamicDataDisplayNameInDifferentClass()
    {
        var data = new object[] { 1, 2, 3 };

        _dynamicDataAttribute.DynamicDataDisplayName = "GetCustomDynamicDataDisplayName2";
        _dynamicDataAttribute.DynamicDataDisplayNameDeclaringType = typeof(DummyTestClass2);
        var displayName = _dynamicDataAttribute.GetDisplayName(_testMethodInfo, data);
        displayName.Should().Be("DynamicDataTestWithDisplayName TestMethod1 with 3 parameters");
    }

    public void GetDisplayNameShouldThrowExceptionWithDynamicDataDisplayNameMethodMissingParameters()
    {
        Action action = () =>
        {
            var data = new object[] { 1, 2, 3 };

            _dynamicDataAttribute.DynamicDataDisplayName = "GetDynamicDataDisplayNameWithMissingParameters";
            var displayName = _dynamicDataAttribute.GetDisplayName(_testMethodInfo, data);
        };

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    public void GetDisplayNameShouldThrowExceptionWithDynamicDataDisplayNameMethodInvalidReturnType()
    {
        Action action = () =>
        {
            var data = new object[] { 1, 2, 3 };

            _dynamicDataAttribute.DynamicDataDisplayName = "GetDynamicDataDisplayNameWithInvalidReturnType";
            var displayName = _dynamicDataAttribute.GetDisplayName(_testMethodInfo, data);
        };

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    public void GetDisplayNameShouldThrowExceptionWithDynamicDataDisplayNameMethodInvalidFirstParameterType()
    {
        Action action = () =>
        {
            var data = new object[] { 1, 2, 3 };

            _dynamicDataAttribute.DynamicDataDisplayName = "GetDynamicDataDisplayNameWithInvalidFirstParameterType";
            var displayName = _dynamicDataAttribute.GetDisplayName(_testMethodInfo, data);
        };

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    public void GetDisplayNameShouldThrowExceptionWithDynamicDataDisplayNameMethodInvalidSecondParameterType()
    {
        Action action = () =>
        {
            var data = new object[] { 1, 2, 3 };

            _dynamicDataAttribute.DynamicDataDisplayName = "GetDynamicDataDisplayNameWithInvalidSecondParameterType";
            var displayName = _dynamicDataAttribute.GetDisplayName(_testMethodInfo, data);
        };

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    public void GetDisplayNameShouldThrowExceptionWithDynamicDataDisplayNameMethodNonStatic()
    {
        Action action = () =>
        {
            var data = new object[] { 1, 2, 3 };

            _dynamicDataAttribute.DynamicDataDisplayName = "GetDynamicDataDisplayNameNonStatic";
            var displayName = _dynamicDataAttribute.GetDisplayName(_testMethodInfo, data);
        };

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    public void GetDisplayNameShouldThrowExceptionWithDynamicDataDisplayNameMethodPrivate()
    {
        Action action = () =>
        {
            var data = new object[] { 1, 2, 3 };

            _dynamicDataAttribute.DynamicDataDisplayName = "GetDynamicDataDisplayNamePrivate";
            var displayName = _dynamicDataAttribute.GetDisplayName(_testMethodInfo, data);
        };

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    public void GetDisplayNameShouldThrowExceptionWithMissingDynamicDataDisplayNameMethod()
    {
        Action action = () =>
        {
            var data = new object[] { 1, 2, 3 };

            _dynamicDataAttribute.DynamicDataDisplayName = "MissingCustomDynamicDataDisplayName";
            var displayName = _dynamicDataAttribute.GetDisplayName(_testMethodInfo, data);
        };

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    public void GetDisplayNameShouldReturnEmptyStringIfDataIsNull()
    {
        var displayName = _dynamicDataAttribute.GetDisplayName(_testMethodInfo, null);
        displayName.Should().BeNull();
    }

    public void GetDisplayNameHandlesNullValues()
    {
        var data = new string[] { "value1", "value2", null };
        var displayName = _dynamicDataAttribute.GetDisplayName(_testMethodInfo, data);
        displayName.Should().Be("TestMethod1 (value1,value2,)");

        var data1 = new string[] { null, "value1", "value2" };
        displayName = _dynamicDataAttribute.GetDisplayName(_testMethodInfo, data1);
        displayName.Should().Be("TestMethod1 (,value1,value2)");

        var data2 = new string[] { "value1", null, "value2" };
        displayName = _dynamicDataAttribute.GetDisplayName(_testMethodInfo, data2);
        displayName.Should().Be("TestMethod1 (value1,,value2)");
    }
}

/// <summary>
/// The dummy test class.
/// </summary>
[TestClass]
public class DummyTestClass
{
    /// <summary>
    /// Gets the reusable test data property.
    /// </summary>
    public static IEnumerable<object[]> ReusableTestDataProperty
    {
        get
        {
            return new[] { new object[] { 1, 2, 3 }, new object[] { 4, 5, 6 } };
        }
    }

    /// <summary>
    /// Gets the null test data property.
    /// </summary>
    public static IEnumerable<object[]> NullProperty
    {
        get
        {
            return null;
        }
    }

    /// <summary>
    /// Gets the empty test data property.
    /// </summary>
    public static IEnumerable<object[]> EmptyProperty
    {
        get
        {
            return Array.Empty<object[]>();
        }
    }

    /// <summary>
    /// Gets the wrong test data property i.e. Property returning something other than
    /// expected data type of <see cref="T:IEnumerable{object[]}"/>.
    /// </summary>
    public static string WrongDataTypeProperty
    {
        get
        {
            return "Dummy";
        }
    }

    /// <summary>
    /// The reusable test data method.
    /// </summary>
    /// <returns>
    /// The <see cref="IEnumerable{T}"/>.
    /// </returns>
    public static IEnumerable<object[]> ReusableTestDataMethod()
    {
        return new[] { new object[] { 1, 2, 3 }, new object[] { 4, 5, 6 } };
    }

    /// <summary>
    /// The custom display name method.
    /// </summary>
    /// <param name="methodInfo">
    /// The method info of test method.
    /// </param>
    /// <param name="data">
    /// The test data which is passed to test method.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetCustomDynamicDataDisplayName(MethodInfo methodInfo, object[] data)
    {
        return string.Format("DynamicDataTestWithDisplayName {0} with {1} parameters", methodInfo.Name, data.Length);
    }

    /// <summary>
    /// Custom display name method with missing parameters.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetDynamicDataDisplayNameWithMissingParameters()
    {
        throw new InvalidOperationException();
    }

    /// <summary>
    /// Custom display name method with invalid return type.
    /// </summary>
    public static void GetDynamicDataDisplayNameWithInvalidReturnType()
    {
        throw new InvalidOperationException();
    }

    /// <summary>
    /// Custom display name method with invalid first parameter type.
    /// </summary>
    /// <param name="methodInfo">
    /// The method info of test method.
    /// </param>
    /// <param name="data">
    /// The test data which is passed to test method.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetDynamicDataDisplayNameWithInvalidFirstParameterType(string methodInfo, object[] data)
    {
        throw new InvalidOperationException();
    }

    /// <summary>
    /// Custom display name method with invalid second parameter.
    /// </summary>
    /// <param name="methodInfo">
    /// The method info of test method.
    /// </param>
    /// <param name="data">
    /// The test data which is passed to test method.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetDynamicDataDisplayNameWithInvalidSecondParameterType(MethodInfo methodInfo, string data)
    {
        throw new InvalidOperationException();
    }

    /// <summary>
    /// Custom display name method that is not static.
    /// </summary>
    /// <param name="methodInfo">
    /// The method info of test method.
    /// </param>
    /// <param name="data">
    /// The test data which is passed to test method.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string GetDynamicDataDisplayNameNonStatic(MethodInfo methodInfo, object[] data)
    {
        throw new InvalidOperationException();
    }

    /// <summary>
    /// The test method 1.
    /// </summary>
    [TestMethod]
    [DynamicData("ReusableTestDataProperty")]
    public void TestMethod1()
    {
    }

    /// <summary>
    /// The test method 2.
    /// </summary>
    [TestMethod]
    [DynamicData("ReusableTestDataMethod")]
    public void TestMethod2()
    {
    }

    /// <summary>
    /// The test method 3.
    /// </summary>
    [TestMethod]
    [DynamicData("WrongDataTypeProperty")]
    public void TestMethod3()
    {
    }

    /// <summary>
    /// The test method 4.
    /// </summary>
    [TestMethod]
    [DynamicData("NullProperty")]
    public void TestMethod4()
    {
    }

    /// <summary>
    /// The test method 5.
    /// </summary>
    [TestMethod]
    [DynamicData("EmptyProperty")]
    public void TestMethod5()
    {
    }

    /// <summary>
    /// DataRow test method 1.
    /// </summary>
    [DataRow("First", "Second", null)]
    [DataRow(null, "First", "Second")]
    [DataRow("First", null, "Second")]
    [TestMethod]
    public void DataRowTestMethod()
    {
    }

    /// <summary>
    /// Custom display name method that is private.
    /// </summary>
    /// <param name="methodInfo">
    /// The method info of test method.
    /// </param>
    /// <param name="data">
    /// The test data which is passed to test method.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private static string GetDynamicDataDisplayNamePrivate(MethodInfo methodInfo, object[] data)
    {
        throw new InvalidOperationException();
    }
}

public class DummyTestClass2
{
    /// <summary>
    /// Gets the reusable test data property.
    /// </summary>
    public static IEnumerable<object[]> ReusableTestDataProperty2
    {
        get
        {
            return new[] { new object[] { 1, 2, 3 }, new object[] { 4, 5, 6 } };
        }
    }

    /// <summary>
    /// The reusable test data method.
    /// </summary>
    /// <returns>
    /// The <see cref="IEnumerable"/>.
    /// </returns>
    public static IEnumerable<object[]> ReusableTestDataMethod2()
    {
        return new[] { new object[] { 1, 2, 3 }, new object[] { 4, 5, 6 } };
    }

    /// <summary>
    /// The custom display name method.
    /// </summary>
    /// <param name="methodInfo">
    /// The method info of test method.
    /// </param>
    /// <param name="data">
    /// The test data which is passed to test method.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetCustomDynamicDataDisplayName2(MethodInfo methodInfo, object[] data)
    {
        return string.Format("DynamicDataTestWithDisplayName {0} with {1} parameters", methodInfo.Name, data.Length);
    }
}
