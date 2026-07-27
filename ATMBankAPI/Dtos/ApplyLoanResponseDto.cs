namespace ATMBankAPI.Dtos
{
    public class ApplyLoanResponseDto
    {
        public string Message { get; set; }
        public int LoanId { get; set; }
        public string CustomerName { get; set; }
        public string LoanType { get; set; }
        public decimal LoanAmount { get; set; }
        public int DurationMonths { get; set; }
        public string LoanStatus { get; set; }


    }
}
