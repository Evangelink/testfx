// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.VisualStudio.TestPlatform.TestFramework.UnitTests;
public partial class AssertTests
{
    // See https://github.com/dotnet/sdk/issues/25373
    public void InconclusiveDoesNotThrowWhenMessageContainsInvalidStringFormatCompositeAndNoArgumentsPassed()
    {
        Action action = () => Assert.Inconclusive("{");

        action.Should().ThrowExactly<AssertFailedException>().And.Message.Should().Contain("Assert.Inconclusive failed. {");
    }

    // See https://github.com/dotnet/sdk/issues/25373
    public void InconclusiveThrowsWhenMessageContainsInvalidStringFormatComposite()
    {
        Action action = () => Assert.Inconclusive("{", "arg");

        action.Should().ThrowExactly<FormatException>();
    }
}
