using GA.BDC.Web.MGP.Helpers.Extensions;
namespace GA.BDC.Web.MGP.Models.Branding
{
    public class User
    {
        public int Id { get; set; }
        public int HierarchyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompleteName
        {
            get
            {
                return FirstName.IsNotEmpty()
                        ? string.Concat(FirstName.Trim(), LastName.IsNotEmpty() ? " " + LastName.Trim() : string.Empty)
                        : string.Empty;
            }
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public int EventParticipationId { get; set; }
        public bool IsLoggedIn { get; set; }
        public int MemberTypeId { get; set; }
        public UserType UserTypeFromMemberType
        {
            get
            {
                switch (MemberTypeId)
                {
                    case (int)UserType.UNKNOWN:
                        return UserType.UNKNOWN;
                    case (int)UserType.SPONSOR:
                        return UserType.SPONSOR;
                    case (int)UserType.PARTICIPANT:
                        return UserType.PARTICIPANT;
                    case (int)UserType.SUPPORTER:
                        return UserType.SUPPORTER;
                    default:
                        return UserType.UNKNOWN;
                }
            }
        }
        public UserType UserTypeInfo { get; set; }
        public bool IsSponsor
        {
            get
            {
                return MemberTypeId > 0
                    ? (UserTypeFromMemberType == UserType.SPONSOR)
                    : (UserTypeInfo == UserType.SPONSOR);
            }
        }

        public bool IsParticipant
        {
            get
            {
                return MemberTypeId > 0
                    ? (UserTypeFromMemberType == UserType.PARTICIPANT)
                    : (UserTypeInfo == UserType.PARTICIPANT);
            }
        }

        public bool IsSupporter
        {
            get
            {
                return MemberTypeId > 0
                    ? (UserTypeFromMemberType == UserType.SUPPORTER)
                    : (UserTypeInfo == UserType.SUPPORTER);
            }
        }

        public bool IsUnknown
        {
            get
            {
                return MemberTypeId > 0
                    ? (UserTypeFromMemberType == UserType.UNKNOWN)
                    : (UserTypeInfo == UserType.UNKNOWN);
            }
        }
    }

    public enum UserType : int
    {
        UNKNOWN = 0,
        SPONSOR = 1,
        PARTICIPANT = 2,
        SUPPORTER = 3
    }
}