namespace ATMBankAPI.Dtos
{
    public class BeneficiaryResponseDto
    {
        public string Message { get; set; }
        public int BeneficiaryId { get; set; }
        public string BeneficiaryName { get; set; }
        public long BeneficiaryAccount { get; set; }
        public string NickName { get; set; }
    }
}
