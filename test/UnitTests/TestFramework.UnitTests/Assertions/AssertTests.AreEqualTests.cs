// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TestFramework.ForTestingMSTest;

namespace Microsoft.VisualStudio.TestPlatform.TestFramework.UnitTests;
public partial class AssertTests : TestContainer
{
    public void AreNotEqualShouldFailWhenNotEqualType()
    {
        Action action = () => Assert.AreNotEqual(null, null);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreNotEqualShouldFailWhenNotEqualTypeWithMessage()
    {
        Action action = () => Assert.AreNotEqual(null, null, "A Message");

        action.Should().Throw<Exception>().And.Message.Should().Contain("A Message");
    }

    public void AreNotEqualShouldFailWhenNotEqualString()
    {
        Action action = () => Assert.AreNotEqual("A", "A");

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreNotEqualShouldFailWhenNotEqualStringWithMessage()
    {
        Action action = () => Assert.AreNotEqual("A", "A", "A Message");

        action.Should().Throw<Exception>().And.Message.Should().Contain("A Message");
    }

    public void AreNotEqualShouldFailWhenNotEqualStringAndCaseIgnored()
    {
        Action action = () => Assert.AreNotEqual("A", "a", true);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreNotEqualShouldFailWhenNotEqualInt()
    {
        Action action = () => Assert.AreNotEqual(1, 1);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreNotEqualShouldFailWhenNotEqualIntWithMessage()
    {
        Action action = () => Assert.AreNotEqual(1, 1, "A Message");

        action.Should().Throw<Exception>().And.Message.Should().Contain("A Message");
    }

    public void AreNotEqualShouldFailWhenNotEqualLong()
    {
        Action action = () => Assert.AreNotEqual(1L, 1L);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreNotEqualShouldFailWhenNotEqualLongWithMessage()
    {
        Action action = () => Assert.AreNotEqual(1L, 1L, "A Message");

        action.Should().Throw<Exception>().And.Message.Should().Contain("A Message");
    }

    public void AreNotEqualShouldFailWhenNotEqualLongWithDelta()
    {
        Action action = () => Assert.AreNotEqual(1L, 2L, 1L);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreNotEqualShouldFailWhenNotEqualDecimal()
    {
        Action action = () => Assert.AreNotEqual(0.1M, 0.1M);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreNotEqualShouldFailWhenNotEqualDecimalWithMessage()
    {
        Action action = () => Assert.AreNotEqual(0.1M, 0.1M, "A Message");

        action.Should().Throw<Exception>().And.Message.Should().Contain("A Message");
    }

    public void AreNotEqualShouldFailWhenNotEqualDecimalWithDelta()
    {
        Action action = () => Assert.AreNotEqual(0.1M, 0.2M, 0.1M);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreNotEqualShouldFailWhenNotEqualDouble()
    {
        Action action = () => Assert.AreNotEqual(0.1, 0.1);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreNotEqualShouldFailWhenNotEqualDoubleWithMessage()
    {
        Action action = () => Assert.AreNotEqual(0.1, 0.1, "A Message");

        action.Should().Throw<Exception>().And.Message.Should().Contain("A Message");
    }

    public void AreNotEqualShouldFailWhenNotEqualDoubleWithDelta()
    {
        Action action = () => Assert.AreNotEqual(0.1, 0.2, 0.1);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreNotEqualShouldFailWhenFloatDouble()
    {
        Action action = () => Assert.AreNotEqual(100E-2, 100E-2);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreNotEqualShouldFailWhenFloatDoubleWithMessage()
    {
        Action action = () => Assert.AreNotEqual(100E-2, 100E-2, "A Message");

        action.Should().Throw<Exception>().And.Message.Should().Contain("A Message");
    }

    public void AreNotEqualShouldFailWhenNotEqualFloatWithDelta()
    {
        Action action = () => Assert.AreNotEqual(100E-2, 200E-2, 100E-2);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreEqualShouldFailWhenNotEqualType()
    {
        Action action = () => Assert.AreEqual(null, "string");

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreEqualShouldFailWhenNotEqualTypeWithMessage()
    {
        Action action = () => Assert.AreEqual(null, "string", "A Message");

        action.Should().Throw<Exception>().And.Message.Should().Contain("A Message");
    }

    public void AreEqual_WithTurkishCultureAndIgnoreCase_Throws()
    {
        var expected = "i";
        var actual = "I";
        var turkishCulture = new CultureInfo("tr-TR");

        // In the tr-TR culture, "i" and "I" are not considered equal when doing a case-insensitive comparison.
        Action action = () => Assert.AreEqual(expected, actual, true, turkishCulture);
        action.Should().Throw<AssertFailedException>();
    }

    public void AreEqual_WithEnglishCultureAndIgnoreCase_DoesNotThrow()
    {
        var expected = "i";
        var actual = "I";
        var englishCulture = new CultureInfo("en-EN");

        // Will ignore case and won't make exeption.
        Assert.AreEqual(expected, actual, true, englishCulture);
    }

    public void AreEqual_WithEnglishCultureAndDoesNotIgnoreCase_Throws()
    {
        var expected = "i";
        var actual = "I";
        var englishCulture = new CultureInfo("en-EN");

        // Won't ignore case.
        Action action = () => Assert.AreEqual(expected, actual, false, englishCulture);
        action.Should().Throw<AssertFailedException>();
    }

    public void AreEqual_WithTurkishCultureAndDoesNotIgnoreCase_Throws()
    {
        var expected = "i";
        var actual = "I";
        var turkishCulture = new CultureInfo("tr-TR");

        // Won't ignore case.
        Action action = () => Assert.AreEqual(expected, actual, false, turkishCulture);
        action.Should().Throw<AssertFailedException>();
    }

    public void AreEqualShouldFailWhenNotEqualStringWithMessage()
    {
        Action action = () => Assert.AreEqual("A", "a", "A Message");

        action.Should().Throw<Exception>().And.Message.Should().Contain("A Message");
    }

    public void AreEqualShouldFailWhenNotEqualStringAndCaseIgnored()
    {
        Action action = () => Assert.AreEqual("A", "a", false);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreEqualShouldFailWhenNotEqualInt()
    {
        Action action = () => Assert.AreEqual(1, 2);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreEqualShouldFailWhenNotEqualIntWithMessage()
    {
        Action action = () => Assert.AreEqual(1, 2, "A Message");

        action.Should().Throw<Exception>().And.Message.Should().Contain("A Message");
    }

    public void AreEqualShouldFailWhenNotEqualLong()
    {
        Action action = () => Assert.AreEqual(1L, 2L);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreEqualShouldFailWhenNotEqualLongWithMessage()
    {
        Action action = () => Assert.AreEqual(1L, 2L, "A Message");

        action.Should().Throw<Exception>().And.Message.Should().Contain("A Message");
    }

    public void AreEqualShouldFailWhenNotEqualLongWithDelta()
    {
        Action action = () => Assert.AreEqual(10L, 20L, 5L);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreEqualShouldFailWhenNotEqualDouble()
    {
        Action action = () => Assert.AreEqual(0.1, 0.2);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreEqualShouldFailWhenNotEqualDoubleWithMessage()
    {
        Action action = () => Assert.AreEqual(0.1, 0.2, "A Message");

        action.Should().Throw<Exception>().And.Message.Should().Contain("A Message");
    }

    public void AreEqualShouldFailWhenNotEqualDoubleWithDelta()
    {
        Action action = () => Assert.AreEqual(0.1, 0.2, 0.05);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreEqualShouldFailWhenNotEqualDecimal()
    {
        Action action = () => Assert.AreEqual(0.1M, 0.2M);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreEqualShouldFailWhenNotEqualDecimalWithMessage()
    {
        Action action = () => Assert.AreEqual(0.1M, 0.2M, "A Message");

        action.Should().Throw<Exception>().And.Message.Should().Contain("A Message");
    }

    public void AreEqualShouldFailWhenNotEqualDecimalWithDelta()
    {
        Action action = () => Assert.AreEqual(0.1M, 0.2M, 0.05M);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreEqualShouldFailWhenFloatDouble()
    {
        Action action = () => Assert.AreEqual(100E-2, 200E-2);

        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void AreEqualShouldFailWhenFloatDoubleWithMessage()
    {
        Action action = () => Assert.AreEqual(100E-2, 200E-2, "A Message");

        action.Should().Throw<Exception>().And.Message.Should().Contain("A Message");
    }

    public void AreEqualShouldFailWhenNotEqualFloatWithDelta()
    {
        Action action = () => Assert.AreEqual(100E-2, 200E-2, 50E-2);

        action.Should().ThrowExactly<AssertFailedException>();
    }
}
