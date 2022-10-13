// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Text.RegularExpressions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TestFramework.ForTestingMSTest;

namespace Microsoft.VisualStudio.TestPlatform.TestFramework.UnitTests.Assertions;
public class StringAssertTests : TestContainer
{
    public void ThatShouldReturnAnInstanceOfStringAssert()
    {
        StringAssert.That.Should().NotBeNull();
    }

    public void ThatShouldCacheStringAssertInstance()
    {
        StringAssert.That.Should().BeSameAs(StringAssert.That);
    }

    public void StringAssertContains()
    {
        string actual = "The quick brown fox jumps over the lazy dog.";
        string notInString = "I'm not in the string above";
        Action action = () => StringAssert.Contains(actual, notInString);

        action.Should().ThrowExactly<AssertFailedException>().And.Message.Should().Contain("StringAssert.Contains failed");
    }

    public void StringAssertStartsWith()
    {
        string actual = "The quick brown fox jumps over the lazy dog.";
        string notInString = "I'm not in the string above";
        Action action = () => StringAssert.StartsWith(actual, notInString);

        action.Should().ThrowExactly<AssertFailedException>().And.Message.Should().Contain("StringAssert.StartsWith failed");
    }

    public void StringAssertEndsWith()
    {
        string actual = "The quick brown fox jumps over the lazy dog.";
        string notInString = "I'm not in the string above";
        Action action = () => StringAssert.EndsWith(actual, notInString);

        action.Should().ThrowExactly<AssertFailedException>().And.Message.Should().Contain("StringAssert.EndsWith failed");
    }

    public void StringAssertDoesNotMatch()
    {
        string actual = "The quick brown fox jumps over the lazy dog.";
        Regex doesMatch = new("quick brown fox");
        Action action = () => StringAssert.DoesNotMatch(actual, doesMatch);

        action.Should().ThrowExactly<AssertFailedException>().And.Message.Should().Contain("StringAssert.DoesNotMatch failed");
    }

    public void StringAssertContainsIgnoreCase_DoesNotThrow()
    {
        string actual = "The quick brown fox jumps over the lazy dog.";
        string inString = "THE QUICK BROWN FOX JUMPS OVER THE LAZY DOG.";
        StringAssert.Contains(actual, inString, StringComparison.OrdinalIgnoreCase);
    }

    public void StringAssertStartsWithIgnoreCase_DoesNotThrow()
    {
        string actual = "The quick brown fox jumps over the lazy dog.";
        string inString = "THE QUICK";
        StringAssert.StartsWith(actual, inString, StringComparison.OrdinalIgnoreCase);
    }

    public void StringAssertEndsWithIgnoreCase_DoesNotThrow()
    {
        string actual = "The quick brown fox jumps over the lazy dog.";
        string inString = "LAZY DOG.";
        StringAssert.EndsWith(actual, inString, StringComparison.OrdinalIgnoreCase);
    }

    // See https://github.com/dotnet/sdk/issues/25373
    public void StringAssertContainsDoesNotThrowFormatException()
    {
        Action action = () => StringAssert.Contains(":-{", "x");

        action.Should().ThrowExactly<AssertFailedException>().And.Message.Should().Contain("StringAssert.Contains failed");
    }

    // See https://github.com/dotnet/sdk/issues/25373
    public void StringAssertContainsDoesNotThrowFormatExceptionWithArguments()
    {
        Action action = () => StringAssert.Contains("{", "x", "message {0}", "arg");

        action.Should().ThrowExactly<AssertFailedException>().And.Message.Should().Contain("StringAssert.Contains failed");
    }

    // See https://github.com/dotnet/sdk/issues/25373
    public void StringAssertContainsFailsIfMessageIsInvalidStringFormatComposite()
    {
        Action action = () => StringAssert.Contains("a", "b", "message {{0}", "arg");

        action.Should().ThrowExactly<FormatException>();
    }
}
