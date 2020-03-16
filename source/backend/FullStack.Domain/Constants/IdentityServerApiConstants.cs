namespace FullStack.Domain.Constants
{
    public static class IdentityServerApiConstants
    {
        public const string ResourceName = "full-stack.api";
        public const string ResourceDisplayName = "API - Test - FullStack";

        public static class SecurityConstants
        {
            public const string PfxName = "certificate.pfx";
        }

        public static class StandardClaimsConstants
        {
            public const string RoleId = "urn:platform:roleid";
        }
    }
}
