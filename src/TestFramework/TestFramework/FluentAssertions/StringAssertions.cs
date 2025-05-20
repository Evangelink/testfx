// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.VisualStudio.TestTools.UnitTesting;

public static class FluentAssertionsExtensions
{
    public static FluentAssertion<string> StartsWith(this FluentAssertion<string> actual, string expected)
    {
        StringAssert.StartsWith(expected, actual);
        return actual;
    }
}
