using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using ATMBankAPI.Repository;
namespace ATMBankAPI.Services
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiary;
        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository)
        {
            _beneficiary = beneficiaryRepository;
        }
        public async Task<BeneficiaryResponseDto> AddBeneficiary(BeneficiaryDto dto)
        {
            return await _beneficiary.AddBeneficiary(dto);
        }

        public async Task<string> DeleteBeneficiary(int beneficiaryId)
        {
           return await _beneficiary.DeleteBeneficiary(beneficiaryId);
        }

        public async Task<List<BeneficiaryResponseDto>> GetAllBeneficiaries(int accountId)
        {
           return await _beneficiary.GetAllBeneficiaries(accountId);
        }

        public async Task<BeneficiaryResponseDto> GetBeneficiary(int beneficiaryId)
        {
            return await _beneficiary.GetBeneficiary(beneficiaryId);
        }

        public async Task<BeneficiaryResponseDto> UpdateBeneficiary(int beneficiaryId, BeneficiaryDto dto)
        {
            return await _beneficiary.UpdateBeneficiary(beneficiaryId, dto);
        }
    }
}
