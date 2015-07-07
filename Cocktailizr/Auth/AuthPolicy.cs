using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Cocktailizr.Auth
{
    public class AuthPolicy : IAuthorizationPolicy
    {
        public string Id { get { return Guid.NewGuid().ToString(); } }

        public ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }

        // this method gets called after the authentication stage
        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            // get the authenticated client identity
            IIdentity client = GetClientIdentity(evaluationContext);

            var roles = new List<string>();
            if (client.Name.Equals("frank")) roles.Add("ADMIN");

            evaluationContext.Properties["Principal"] = new AuthPrincipal(client, roles.ToArray());

            return true;
        }

        private IIdentity GetClientIdentity(EvaluationContext evaluationContext)
        {
            object obj;
            if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
                throw new Exception("No Identity found");

            IList<IIdentity> identities = obj as IList<IIdentity>;
            if (identities == null || identities.Count <= 0)
                throw new Exception("No Identity found");

            return identities[0];
        }

    }
}