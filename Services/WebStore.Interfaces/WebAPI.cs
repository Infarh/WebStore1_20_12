namespace WebStore.Interfaces
{
    public static class WebAPI
    {
        private const string api = "api/";

        public const string Version = "v1";

        public const string Values = api + Version + "/values";
        public const string Employees = api + Version + "/employees";
        public const string Products = api + Version +  "/products";
        public const string Orders = api + Version +  "/orders";

        public static class Identity
        {
            public const string User = api + Version +  "/users";
            public const string Role = api + Version +  "/roles";
        }
    }
}
