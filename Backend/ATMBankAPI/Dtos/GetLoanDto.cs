namespace ATMBankAPI.Dtos
{
    public class GetLoanDto
    {
        public int LoanId { get; set; }
        public string CustomerName { get; set; }
        public string LoanType {  get; set; }
        public decimal LoanAmount { get; set; } 
        public decimal Emi { get; set; }
        public int DurationMonth { get; set; }
        public string LoanStatus { get; set; }
        public DateTime ApplyDate { get; set; }

    }
}
