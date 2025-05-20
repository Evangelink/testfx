// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.VisualStudio.TestTools.UnitTesting;

public class FluentAssertion<TSubject>(TSubject actual)
{
    [MemberNotNull(nameof(actual))]
    public FluentAssertion<TSubject> IsNotNull()
    {
        Assert.IsNotNull(actual);
        return this;
    }

    public FluentAssertion<TSubject> IsNull()
    {
        Assert.IsNull(actual);
        return this;
    }

    public FluentAssertion<TSubject> IsOfType<TExpected>()
    {
        Assert.IsInstanceOfType<TExpected>(actual);
        return this;
    }

    public FluentAssertion<TSubject> And => this;
}
