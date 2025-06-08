namespace Tutorial5.DTOs
{
    public class CustomerResponse
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string? phoneNumber { get; set; }
        public List<PurchaseCustomerResponse> purchases { get; set; }

    }

    public class PurchaseCustomerResponse
    {
        public DateTime date { get; set; }
        public int? rating { get; set; }
        public decimal? price { get; set; }
        public PurchaseCustomerWashingMachineResponse washingMachine { get; set; }
        public PurchaseCustomerProgramResponse program { get; set; }
    }

    public class PurchaseCustomerWashingMachineResponse
    {
        public string serial { get; set; }
        public decimal maxWeight { get; set; }
    }

    public class PurchaseCustomerProgramResponse
    {
        public string name { get; set; }
        public int duration { get; set; }
    }
}
