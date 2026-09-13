using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface IBeneficiaryService
    {
        Task<BeneficiaryResponseDto> AddBeneficiary(BeneficiaryDto dto);
        Task<BeneficiaryResponseDto> GetBeneficiary(int beneficiaryId);

        Task<List<BeneficiaryResponseDto>> GetAllBeneficiaries(int accountId);

        Task<BeneficiaryResponseDto> UpdateBeneficiary(int beneficiaryId,BeneficiaryDto dto);

        Task<string> DeleteBeneficiary(int beneficiaryId);
        Task<BeneficiaryTransferResponseDto> TransferToBeneficiary(BeneficiaryTransferDto dto);
    }
}
