// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Security.Principal;
using System.ServiceModel;

namespace Server.Implementation
{
    /// <summary>
    /// Authorizes users that call service operations.
    /// </summary>
    public class AuthorizationPolicy : IAuthorizationPolicy 
    {
        private Guid _id = Guid.NewGuid();

        /// <summary>
        /// Gets an id.
        /// </summary>
        public string Id
        {
            get { return _id.ToString(); }
        }

        /// <summary>
        /// Gets a claim set that represents the issuer of the authorization policy.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "I just don't understend wath is this property for. TODO: try get it.")]
        public ClaimSet Issuer { get; private set; }

        /// <summary>
        /// Evaluates whether a user meets the requirements for this authorization policy.
        /// </summary>
        /// <param name="evaluationContext">An System.IdentityModel.Policy.EvaluationContext that contains the claim set that the authorization policy evaluates.</param>
        /// <param name="state">A System.Object, passed by reference that represents the custom state for this authorization policy.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "TODO")]
        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            var client = GetClientIdentity(evaluationContext);

            // set the custom principal
            evaluationContext.Properties["Principal"] = new CustomPrincipal(client);
            return true;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = "TODO")]
        private static IIdentity GetClientIdentity(EvaluationContext evaluationContext)
        {
            object obj;
            if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
            {
                return ServiceSecurityContext.Anonymous.PrimaryIdentity;
            }

            var identities = obj as IList<IIdentity>;
            if (identities == null || identities.Count <= 0)
            {
                return ServiceSecurityContext.Anonymous.PrimaryIdentity;
            }

            if (identities[0].Name.Equals("Anonymous", StringComparison.OrdinalIgnoreCase))
            {
                return ServiceSecurityContext.Anonymous.PrimaryIdentity;
            }

            return identities[0];
        } 
    }
}