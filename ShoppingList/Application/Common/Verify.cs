using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;

namespace ShoppingList.Application.Common
{
    public class Verify
    {
        public Verify(bool assertion)
        {
            Assertion = assertion;
        }

        public bool Assertion { get; protected set; }
        public string ErrorMessageOnFailure { get; protected set; }
        public string Identifer { get; protected set; }

        public Verify OnFailureShowMessage(string errorMessageOnFailure)
        {
            ErrorMessageOnFailure = errorMessageOnFailure;
            return this;
        }

        public Verify IdentifyAs(string identifier)
        {
            Identifer = identifier;
            return this;
        }

        public void Immediately()
        {
            if (!Assertion)
            {
                throw new VerificationFailedException(this);
            }
        }

        public static void These(IList<Verify> verifications)
        {
            if (verifications.Any(verification => !verification.Assertion))
            {
                throw new VerificationFailedException(verifications);
            }
        }

        public static bool IfAnyWouldFail(IList<Verify> verifications)
        {
            if (verifications.Any(verification => !verification.Assertion))
            {
                return true;
            }

            return false;
        }
    }

    public class VerificationFailedException : Exception
    {
        public VerificationFailedException(Verify verification)
            : base("One or more validations have failed.")
        {
            FailedVerifications = new List<Verify>
            {
                verification
            };
        }

        public VerificationFailedException(IList<Verify> verifications)
            : base("One or more validations have failed.")
        {
            if (verifications == null)
            {
                throw new ArgumentNullException("verifications", "VerificationFailedException list cannot be null.");
            }

            if (verifications.All(verification => verification.Assertion))
            {
                throw new ArgumentException("There were no failed verifications in the list provided.");
            }
                
            FailedVerifications = new List<Verify>();

            foreach (var verification in verifications)
            {
                if (!verification.Assertion)
                {
                    FailedVerifications.Add(verification);
                }
            }
        }

        public IList<Verify> FailedVerifications { get; protected set; }
    }
}