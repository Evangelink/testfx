// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TestFramework.ForTestingMSTest;

namespace UnitTestFramework.Tests;

/// <summary>
/// Tests for class GenericParameterHelper.
/// </summary>
public class GenericParameterHelperTests : TestContainer
{
    private GenericParameterHelper _sut = null;

    public GenericParameterHelperTests()
    {
        _sut = new GenericParameterHelper(10);
    }

    public void EqualsShouldReturnFalseIfEachObjectHasDefaultDataValue()
    {
        GenericParameterHelper firstObject = new();
        GenericParameterHelper secondObject = new();

        firstObject.Should().NotBe(secondObject);
    }

    public void EqualsShouldReturnTrueIfTwoObjectHasSameDataValue()
    {
        GenericParameterHelper objectToCompare = new(10);

        _sut.Should().Be(objectToCompare);
    }

    public void EqualsShouldReturnFalseIfTwoObjectDoesNotHaveSameDataValue()
    {
        GenericParameterHelper objectToCompare = new(5);

        _sut.Should().NotBe(objectToCompare);
    }

    public void CompareToShouldReturnZeroIfTwoObjectHasSameDataValue()
    {
        GenericParameterHelper objectToCompare = new(10);

        _sut.CompareTo(objectToCompare).Should().Be(0);
    }

    public void CompareToShouldThrowExceptionIfSpecifiedObjectIsNotOfTypeGenericParameterHelper()
    {
        Action action = () => _sut.CompareTo(5);

        action.Should().ThrowExactly<NotSupportedException>();
    }

    public void GenericParameterHelperShouldImplementIEnumerator()
    {
        _sut = new GenericParameterHelper(15);

        int expectedLengthOfList = 5;  // (15%10)
        int result = 0;

        foreach (var x in _sut)
        {
            result++;
        }

        result.Should().Be(expectedLengthOfList);
    }
}
