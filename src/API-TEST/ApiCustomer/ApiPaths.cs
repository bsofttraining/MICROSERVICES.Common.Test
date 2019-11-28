namespace ApiCustomer
{
    public class ApiPaths
    {
        public static class Order
        {
            public static string Get(string baseUri)
            {
                return $"{baseUri}/api/order";
            }
        }
    }
}
