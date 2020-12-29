using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Mappers
{
    public static class UserMapper
    {
        public static User Hydrate(user user, user_profile userProfile)
        {
            var result = new User
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = userProfile.BillingFirstName,
                LastName = userProfile.BillingLastName,
                Organization = userProfile.GroupName,
                Address = userProfile.BillingAddress,
                AddressZoneId = userProfile.BillingAddressType ?? 1,
                City = userProfile.BillingCity,
                Phone = userProfile.BillingPhone,
                State = userProfile.BillingState,
                ZIP = userProfile.BillingZIP
            };
            return result;
        }
    }
}
