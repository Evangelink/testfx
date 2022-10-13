// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using System.Reflection;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using TestFramework.ForTestingMSTest;

namespace Microsoft.VisualStudio.TestPlatform.TestFramework.UnitTests.Attributes;
public class DataRowAttributeTests : TestContainer
{
    public void DefaultConstructorSetsEmptyArrayPassed()
    {
        var dataRow = new DataRowAttribute();

        dataRow.Data.Should().BeEmpty();
    }

    public void ConstructorShouldSetDataPassed()
    {
        var dataRow = new DataRowAttribute("mercury");

        dataRow.Data.Should().Equal(new object[] { "mercury" });
    }

    public void ConstructorShouldSetNullDataPassed()
    {
        var dataRow = new DataRowAttribute(null);

        dataRow.Data.Should().Equal(new object[] { null });
    }

    public void ConstructorShouldSetMultipleDataValuesPassed()
    {
        var dataRow = new DataRowAttribute("mercury", "venus", "earth");

        dataRow.Data.Should().Equal(new object[] { "mercury", "venus", "earth" });
    }

    public void ConstructorShouldSetANullDataValuePassedInParams()
    {
        var dataRow = new DataRowAttribute("neptune", null);

        dataRow.Data.Should().Equal(new object[] { "neptune", null });
    }

    public void ConstructorShouldSetANullDataValuePassedInAsADataArg()
    {
        var dataRow = new DataRowAttribute(null, "logos");

        dataRow.Data.Should().Equal(new object[] { null, "logos" });
    }

    public void ConstructorShouldSetMultipleDataArrays()
    {
        // Fixes https://github.com/microsoft/testfx/issues/1180
        var dataRow = new DataRowAttribute(new[] { "a" }, new[] { "b" });

        dataRow.Data.Should().HaveCount(2);
        dataRow.Data[0].As<string[]>().Should().Equal(new[] { "a" });
        dataRow.Data[1].As<string[]>().Should().Equal(new[] { "b" });
    }

    public void GetDataShouldReturnDataPassed()
    {
        var dataRow = new DataRowAttribute("mercury");

        dataRow.GetData(null).Single().Should().Equal(new object[] { "mercury" });
    }

    public void GetDisplayNameShouldReturnAppropriateName()
    {
        var dataRowAttribute = new DataRowAttribute(null);

        var dummyTestClass = new DummyTestClass();
        var testMethodInfo = dummyTestClass.GetType().GetTypeInfo().GetDeclaredMethod("DataRowTestMethod");

        var data = new string[] { "First", "Second", null };
        var data1 = new string[] { null, "First", "Second" };
        var data2 = new string[] { "First", null, "Second" };

        var displayName = dataRowAttribute.GetDisplayName(testMethodInfo, data);
        displayName.Should().Be("DataRowTestMethod (First,Second,)");

        displayName = dataRowAttribute.GetDisplayName(testMethodInfo, data1);
        displayName.Should().Be("DataRowTestMethod (,First,Second)");

        displayName = dataRowAttribute.GetDisplayName(testMethodInfo, data2);
        displayName.Should().Be("DataRowTestMethod (First,,Second)");
    }

    public void GetDisplayNameShouldReturnSpecifiedDisplayName()
    {
        var dataRowAttribute = new DataRowAttribute(null)
        {
            DisplayName = "DataRowTestWithDisplayName",
        };

        var dummyTestClass = new DummyTestClass();
        var testMethodInfo = dummyTestClass.GetType().GetTypeInfo().GetDeclaredMethod("DataRowTestMethod");

        var data = new string[] { "First", "Second", null };

        var displayName = dataRowAttribute.GetDisplayName(testMethodInfo, data);
        displayName.Should().Be("DataRowTestWithDisplayName");
    }

    public void GetDisplayNameForArrayOfOneItem()
    {
        // Arrange
        var dataRow = new DataRowAttribute(new[] { "a" });
        var methodInfoMock = new Mock<MethodInfo>();
        methodInfoMock.SetupGet(x => x.Name).Returns("MyMethod");

        // Act
        var displayName = dataRow.GetDisplayName(methodInfoMock.Object, dataRow.Data);

        // Assert
        displayName.Should().Be("MyMethod (a)");
    }

    public void GetDisplayNameForArrayOfMultipleItems()
    {
        // Arrange
        var dataRow = new DataRowAttribute(new[] { "a", "b", "c" });
        var methodInfoMock = new Mock<MethodInfo>();
        methodInfoMock.SetupGet(x => x.Name).Returns("MyMethod");

        // Act
        var displayName = dataRow.GetDisplayName(methodInfoMock.Object, dataRow.Data);

        // Assert
        displayName.Should().Be("MyMethod (a,b,c)");
    }

    public void GetDisplayNameForMultipleArraysOfOneItem()
    {
        // Arrange
        var dataRow = new DataRowAttribute(new[] { "a" }, new[] { "1" });
        var methodInfoMock = new Mock<MethodInfo>();
        methodInfoMock.SetupGet(x => x.Name).Returns("MyMethod");

        // Act
        var displayName = dataRow.GetDisplayName(methodInfoMock.Object, dataRow.Data);

        // Assert
        displayName.Should().Be("MyMethod (System.String[],System.String[])");
    }

    public void GetDisplayNameForMultipleArraysOfMultipleItems()
    {
        // Arrange
        var dataRow = new DataRowAttribute(new[] { "a", "b", "c" }, new[] { "1", "2", "3" });
        var methodInfoMock = new Mock<MethodInfo>();
        methodInfoMock.SetupGet(x => x.Name).Returns("MyMethod");

        // Act
        var displayName = dataRow.GetDisplayName(methodInfoMock.Object, dataRow.Data);

        // Assert
        displayName.Should().Be("MyMethod (System.String[],System.String[])");
    }
}
