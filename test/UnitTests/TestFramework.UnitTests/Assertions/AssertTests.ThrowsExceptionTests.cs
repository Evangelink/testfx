// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.VisualStudio.TestPlatform.TestFramework.UnitTests;
public partial class AssertTests
{
    #region ThrowAssertFailed tests
    // See https://github.com/dotnet/sdk/issues/25373
    public void ThrowAssertFailedDoesNotThrowIfMessageContainsInvalidStringFormatComposite()
    {
        Action action = () => Assert.ThrowAssertFailed("name", "{");

        action.Should().ThrowExactly<AssertFailedException>().And.Message.Should().Contain("name failed. {");
    }
    #endregion

    #region ThrowsException tests
    public void ThrowsExceptionWithLambdaExpressionsShouldThrowAssertionOnNoException()
    {
        Action action = () => Assert.ThrowsException<ArgumentException>(() => { });

        action.Should().ThrowExactly<AssertFailedException>().And.Message.Should().Contain("Assert.ThrowsException failed. No exception thrown. ArgumentException exception was expected.");
    }

    public void ThrowsExceptionWithLambdaExpressionsShouldThrowAssertionOnWrongException()
    {
        Action action = () => Assert.ThrowsException<ArgumentException>(() => throw new FormatException());

        action.Should().ThrowExactly<AssertFailedException>().And.Message.Should().Contain("Assert.ThrowsException failed. Threw exception FormatException, but exception ArgumentException was expected.");
    }
    #endregion

    #region ThrowsExceptionAsync tests.
    public async Task ThrowsExceptionAsyncShouldNotThrowAssertionOnRightException()
    {
        Task t = Assert.ThrowsExceptionAsync<ArgumentException>(
            async () =>
            {
                await Task.Delay(5).ConfigureAwait(false);
                throw new ArgumentException();
            });

        // Should not throw an exception.
        await t.ConfigureAwait(false);
    }

    public void ThrowsExceptionAsyncShouldThrowAssertionOnNoException()
    {
        Task t = Assert.ThrowsExceptionAsync<ArgumentException>(
            async () =>
            {
                await Task.Delay(5).ConfigureAwait(false);
            });
        Action action = t.Wait;

        action.Should().Throw<Exception>().And.InnerException.Should().BeOfType<AssertFailedException>().Which.Message.Should().Contain("Assert.ThrowsExceptionAsync failed. No exception thrown. ArgumentException exception was expected.");
    }

    public void ThrowsExceptionAsyncShouldThrowAssertionOnWrongException()
    {
        Task t = Assert.ThrowsExceptionAsync<ArgumentException>(
            async () =>
            {
                await Task.Delay(5).ConfigureAwait(false);
                throw new FormatException();
            });
        Action action = t.Wait;

        action.Should().Throw<Exception>().And.InnerException.Should().BeOfType<AssertFailedException>().Which.Message.Should().Contain("Assert.ThrowsExceptionAsync failed. Threw exception FormatException, but exception ArgumentException was expected.");
    }

    public void ThrowsExceptionAsyncWithMessageShouldThrowAssertionOnNoException()
    {
        Task t = Assert.ThrowsExceptionAsync<ArgumentException>(
            async () =>
            {
                await Task.Delay(5).ConfigureAwait(false);
            },
            "The world is not on fire.");
        Action action = t.Wait;

        action.Should().Throw<Exception>().And.InnerException.Should().BeOfType<AssertFailedException>().Which.Message.Should().Contain("Assert.ThrowsExceptionAsync failed. No exception thrown. ArgumentException exception was expected. The world is not on fire.");
    }

    public void ThrowsExceptionAsyncWithMessageShouldThrowAssertionOnWrongException()
    {
        Task t = Assert.ThrowsExceptionAsync<ArgumentException>(
            async () =>
            {
                await Task.Delay(5).ConfigureAwait(false);
                throw new FormatException();
            },
            "Happily ever after.");
        Action action = t.Wait;

        action.Should().Throw<Exception>().And.InnerException.Should().BeOfType<AssertFailedException>().Which.Message.Should().Contain("Assert.ThrowsExceptionAsync failed. Threw exception FormatException, but exception ArgumentException was expected. Happily ever after.");
    }

    public void ThrowsExceptionAsyncWithMessageAndParamsShouldThrowOnNullAction()
    {
        Action action = () =>
        {
            Task t = Assert.ThrowsExceptionAsync<ArgumentException>(null, null, null);
            t.Wait();
        };

        action.Should().Throw<Exception>().And.InnerException.Should().BeOfType<ArgumentNullException>();
    }

    public void ThrowsExceptionAsyncWithMessageAndParamsShouldThrowOnNullMessage()
    {
        Action action = () =>
        {
            Task t = Assert.ThrowsExceptionAsync<ArgumentException>(async () => { await Task.FromResult(true).ConfigureAwait(false); }, null, null);
            t.Wait();
        };

        action.Should().Throw<Exception>().And.InnerException.Should().BeOfType<ArgumentNullException>();
    }

    public void ThrowsExceptionAsyncWithMessageAndParamsShouldThrowAssertionOnNoException()
    {
        Task t = Assert.ThrowsExceptionAsync<ArgumentException>(
            async () =>
            {
                await Task.Delay(5).ConfigureAwait(false);
            },
            "The world is not on fire {0}.{1}-{2}.",
            "ta",
            "da",
            123);
        Action action = t.Wait;

        action.Should().Throw<Exception>().And.InnerException.Should().BeOfType<AssertFailedException>().Which.Message.Should().Contain("Assert.ThrowsExceptionAsync failed. No exception thrown. ArgumentException exception was expected. The world is not on fire ta.da-123.");
    }

    public void ThrowsExceptionAsyncWithMessageAndParamsShouldThrowAssertionOnWrongException()
    {
        Task t = Assert.ThrowsExceptionAsync<ArgumentException>(
            async () =>
            {
                await Task.Delay(5).ConfigureAwait(false);
                throw new FormatException();
            },
            "Happily ever after. {0} {1}.",
            "The",
            "End");
        Action action = t.Wait;

        action.Should().Throw<Exception>().And.InnerException.Should().BeOfType<AssertFailedException>().Which.Message.Should().Contain("Assert.ThrowsExceptionAsync failed. Threw exception FormatException, but exception ArgumentException was expected. Happily ever after. The End.");
    }
    #endregion
}
