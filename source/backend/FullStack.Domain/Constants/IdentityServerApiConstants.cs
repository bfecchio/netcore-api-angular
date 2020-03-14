namespace FullStack.Domain.Constants
{
    public static class IdentityServerApiConstants
    {
        public const string ResourceName = "full-stack.api";
        public const string ResourceDisplayName = "API - Test - FullStack";

        public static class SecurityConstants
        {
            public const string PfxName = "identity.pfx";
        }

        public static class StandardScopesConstants
        { }

        public static class GrantResultMessagesConstants
        {
            public const string UserDoesNotExists = "Usuário e/ou senha inválidos.";
            public const string UnauthorizedUser = "Usuário não autorizado na plataforma.";
        }

        public static class StandardClaimsConstants
        {
            public const string RoleId = "urn:platform:roleid";
        }
    }
}
