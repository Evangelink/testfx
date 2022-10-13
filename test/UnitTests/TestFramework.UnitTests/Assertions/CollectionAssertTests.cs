// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TestFramework.ForTestingMSTest;

namespace Microsoft.VisualStudio.TestPlatform.TestFramework.UnitTests.Assertions;
public class CollectionAssertTests : TestContainer
{
    public void ThatShouldReturnAnInstanceOfCollectionAssert()
    {
        CollectionAssert.That.Should().NotBeNull();
    }

    public void ThatShouldCacheCollectionAssertInstance()
    {
        CollectionAssert.That.Should().BeSameAs(CollectionAssert.That);
    }
}
