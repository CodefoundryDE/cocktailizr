using Cocktailizr.Model.Service;
using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Principal;
using System.Web;
using CocktailizrTypes.Security;

namespace Cocktailizr.Security
{
    public class AuthPolicy : IAuthorizationPolicy
    {
        public string Id { get { return Guid.NewGuid().ToString(); } }

        public ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }

        private readonly BenutzerDbService _benutzerService;

        public AuthPolicy()
        {
            _benutzerService = CocktailizrServiceLocator.BenutzerDbService;
        }

        // this method gets called after the authentication stage
        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            // get the authenticated client identity
            IIdentity client = GetClientIdentity(evaluationContext);

            UserRole role = _benutzerService.GetUserRole(client.Name).Result;

            evaluationContext.Properties["Principal"] = new AuthPrincipal(client, role);
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