// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using static System.Collections.Specialized.BitVector32;

namespace Microsoft.VisualStudio.TestPlatform.TestFramework.UnitTests;
public partial class AssertTests
{
    public void InstanceOfTypeShouldFailWhenValueIsNull()
    {
        Action action = () => Assert.IsInstanceOfType(null, typeof(AssertTests));
        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void InstanceOfTypeShouldFailWhenTypeIsNull()
    {
        Action action = () => Assert.IsInstanceOfType(5, null);
        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void InstanceOfTypeShouldPassOnSameInstance()
    {
        Action action = () => Assert.IsInstanceOfType(5, typeof(int));
        action.Should().NotThrow();
    }

    public void InstanceOfTypeShouldPassOnHigherInstance()
    {
        Action action = () => Assert.IsInstanceOfType(5, typeof(object));
        action.Should().NotThrow();
    }

    public void InstanceNotOfTypeShouldFailWhenValueIsNull()
    {
        Action action = () => Assert.IsNotInstanceOfType(null, typeof(object));
        action.Should().NotThrow();
    }

    public void InstanceNotOfTypeShouldFailWhenTypeIsNull()
    {
        Action action = () => Assert.IsNotInstanceOfType(5, null);
        action.Should().ThrowExactly<AssertFailedException>();
    }

    public void InstanceNotOfTypeShouldPassOnWrongInstance()
    {
        Action action = () => Assert.IsNotInstanceOfType(5L, typeof(int));
        action.Should().NotThrow();
    }

    public void InstanceNotOfTypeShouldPassOnSubInstance()
    {
        Action action = () => Assert.IsNotInstanceOfType(new object(), typeof(int));
        action.Should().NotThrow();
    }

    [TestMethod]
    public void IsInstanceOfTypeUsingGenericType_WhenValueIsNull_Fails()
    {
        Action action = () => Assert.IsInstanceOfType<AssertTests>(null);
        action.Should().ThrowExactly<AssertFailedException>();
    }

    [TestMethod]
    public void IsInstanceOfTypeUsingGenericType_OnSameInstance_DoesNotThrow()
    {
        Action action = () => Assert.IsInstanceOfType<int>(5);
        action.Should().NotThrow();
    }

    [TestMethod]
    public void IsInstanceOfTypeUsingGenericType_OnHigherInstance_DoesNotThrow()
    {
        Action action = () => Assert.IsInstanceOfType<object>(5);
        action.Should().NotThrow();
    }

    [TestMethod]
    public void IsNotInstanceOfTypeUsingGenericType_WhenValueIsNull_DoesNotThrow()
    {
        Action action = () => Assert.IsNotInstanceOfType<object>(null);
        action.Should().NotThrow();
    }

    [TestMethod]
    public void IsNotInstanceOfType_OnWrongInstanceUsingGenericType_DoesNotThrow()
    {
        Action action = () => Assert.IsNotInstanceOfType<int>(5L);
        action.Should().NotThrow();
    }

    [TestMethod]
    public void IsNotInstanceOfTypeUsingGenericType_OnSubInstance_DoesNotThrow()
    {
        Action action = () => Assert.IsNotInstanceOfType<int>(new object());
        action.Should().NotThrow();
    }
}
