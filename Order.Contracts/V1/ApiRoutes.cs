namespace Order.Contracts
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Product
        {
            public const string GetAll = Base + "/product/getallproducts";
            public const string GetById = Base + "/product/getproductbyid/{id}";
            public const string AddAsync = Base + "/product/createproduct";
            public const string UpdateAsync = Base + "/product/updateproduct";
            public const string DeleteAsync = Base + "/product/deleteproduct/{id}";
        }
        public static class Order
        {
            public const string GetAll = Base + "/order/getallorders";
            public const string GetById = Base + "/order/getorderbyid/{id}";
            public const string AddAsync = Base + "/order/createorder";
            public const string CancelOrderAsync = Base + "/order/cancelorder";
        }
    }
}
