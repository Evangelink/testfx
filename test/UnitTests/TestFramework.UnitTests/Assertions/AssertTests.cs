// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.VisualStudio.TestPlatform.TestFramework.UnitTests;
public partial class AssertTests
{
    #region That tests
    public void ThatShouldReturnAnInstanceOfAssert()
    {
        Assert.That.Should().NotBeNull();
    }

    public void ThatShouldCacheAssertInstance()
    {
        Assert.That.Should().BeSameAs(Assert.That);
    }
    #endregion

    #region ReplaceNullChars tests
    public void ReplaceNullCharsShouldReturnStringIfNullOrEmpty()
    {
        Assert.ReplaceNullChars(null).Should().Be(null);
        Assert.ReplaceNullChars(string.Empty).Should().Be(string.Empty);
    }

    public void ReplaceNullCharsShouldReplaceNullCharsInAString()
    {
        Assert.ReplaceNullChars("The quick brown fox \0 jumped over the la\0zy dog\0").Should().Be("The quick brown fox \\0 jumped over the la\\0zy dog\\0");
    }
    #endregion

    #region BuildUserMessage tests

    // See https://github.com/dotnet/sdk/issues/25373
    public void BuildUserMessageThrowsWhenMessageContainsInvalidStringFormatComposite()
    {
        Action action = () => Assert.BuildUserMessage("{", "arg");

        action.Should().ThrowExactly<FormatException>();
    }

    // See https://github.com/dotnet/sdk/issues/25373
    public void BuildUserMessageDoesNotThrowWhenMessageContainsInvalidStringFormatCompositeAndNoArgumentsPassed()
    {
        string message = Assert.BuildUserMessage("{");
        message.Should().Be("{");
    }
    #endregion
}
