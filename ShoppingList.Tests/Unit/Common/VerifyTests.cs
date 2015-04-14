using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using ShoppingList.Application.Common;

namespace ShoppingList.Tests.Unit.Common
{
    [TestClass]
    public class VerifyTests
    {
        [Test]
        public void VerifyWhenFalseShouldThrowErrorMessage()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var verifications = new List<Verify>();

            verifications.Add(new Verify(false).IdentifyAs("1").OnFailureShowMessage("The error message"));

            Action action = () => Verify.These(verifications);

            action.ShouldThrow<VerificationFailedException>().WithMessage("One or more validations have failed.");
        }

        [Test]
        public void VerifyWhenTrueShouldNotThrowErrorMessage()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var verifications = new List<Verify>();

            verifications.Add(new Verify(true).IdentifyAs("2").OnFailureShowMessage("The error message"));

            Action action = () => Verify.These(verifications);

            action.ShouldNotThrow<VerificationFailedException>();
        }

        [Test]
        public void SingleVerifyWhenFalseShouldThrowErrorMessage()
        {
            Action action = () => new Verify(false).OnFailureShowMessage("The error message").Immediately();

            action.ShouldThrow<VerificationFailedException>().WithMessage("One or more validations have failed.");
        }
    }
}
