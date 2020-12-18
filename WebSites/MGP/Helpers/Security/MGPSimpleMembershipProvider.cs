using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web.Security;
using GA.BDC.Data.MGP.esubs_global_v2.Models;
using WebMatrix.WebData;

namespace GA.BDC.Web.MGP.Helpers.Security
{
   // ReSharper disable once InconsistentNaming
   public class MGPSimpleMembershipProvider : SimpleMembershipProvider
   {
      public override bool ChangePassword(string username, string oldPassword, string newPassword)
      {
         using (var dataProvider = new DataProvider())
         {
            var users = (from u in dataProvider.users
                         where u.username == username && u.password == oldPassword
                         select u);
            if (!users.Any())
            {
               return false; //user doesn't exists or meet the criteria
            }
            var user = users.First();
            user.password = newPassword;
            dataProvider.SaveChanges();
            return true;
         }
      }

      public override bool ConfirmAccount(string accountConfirmationToken)
      {
         return true; //we don't have account confirmation
      }

      public override string CreateAccount(string userName, string password)
      {
         return string.Empty; //we dont have accounts
      }

      public override void CreateOrUpdateOAuthAccount(string provider, string providerUserId, string userName)
      {
	      var extraData = userName.Split('|');
	      var emailAddress = extraData[0];
	      var partnerId = int.Parse(extraData[1]);
         using (var dataProvider = new DataProvider())
         {
	         var userId = (from u in dataProvider.users
		         where u.username == emailAddress && u.partner_id == partnerId
                          select u.user_id).First();
            var userOAuthMembershipAlreadyExists = (from u in dataProvider.user_oauthmemberships
                                                    where u.provider == provider && u.providerUserId == providerUserId && u.userId == userId
                                                    select u).Any();
            if (userOAuthMembershipAlreadyExists) return;
            var userOAuthMembership = new user_oauthmembership
            {
               provider = provider,
               providerUserId = providerUserId,
               userId = userId
            };
            dataProvider.user_oauthmemberships.Add(userOAuthMembership);
            dataProvider.SaveChanges();
         }
      }

      public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
          string newPasswordAnswer)
      {
         return true; // we don't have password questions
      }

      public override bool ConfirmAccount(string userName, string accountConfirmationToken)
      {
         return true; // we don't have account confirmation
      }

      public override string CreateAccount(string userName, string password, bool requireConfirmationToken)
      {
         throw new NotImplementedException();
      }

      public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
          bool isApproved, object providerUserKey, out MembershipCreateStatus status)
      {
         throw new NotImplementedException();
      }

      public override string CreateUserAndAccount(string userName, string password)
      {
         throw new NotImplementedException();
      }

      public override string CreateUserAndAccount(string userName, string password, IDictionary<string, object> values)
      {
         using (var dataProvider = new DataProvider())
         {
            using (var transactionScope = new TransactionScope())
            {
               var partnerId = Convert.ToInt32(values["PartnerId"]);

               var user = new user
               {
                  username = userName,
                  agree_term_services = Convert.ToBoolean(values["Terms"]),
                  create_date = DateTime.Now,
                  culture_code = "en-US",
                  email_address = userName,
                  first_name = WebUtility.HtmlDecode(WebUtility.HtmlDecode(Convert.ToString(values["FirstName"]))),
                  last_name = WebUtility.HtmlDecode(WebUtility.HtmlDecode(Convert.ToString(values["LastName"]))),
                  password = password,
                  unsubscribe = !Convert.ToBoolean(values["Newsletter"]),
                  partner_id = Convert.ToInt32(values["PartnerId"]),
                  opt_status_id = false,
                  is_first_login = true,
                  is_disabled = false
               };
               dataProvider.users.Add(user);
               dataProvider.SaveChanges();

               var member = new member
               {
                  create_date = DateTime.Now,
                  password = password,
                  partner_id = partnerId,
                  user_id = user.user_id,
                  email_address = userName,
                  last_name = user.last_name,
                  first_name = user.first_name,
                  culture_code = "en-US",
                  opt_status_id = 1,
                  unsubscribe = !Convert.ToBoolean(values["Newsletter"]),
                  comments = "Member created automatically by MGP Website after registration",
               };

               dataProvider.members.Add(member);
               dataProvider.SaveChanges();
               var memberHierarchy = new member_hierarchy
               {
                  create_date = DateTime.Now,
                  active = true,
                  creation_channel_id = Convert.ToInt32(values["ChannelId"]),
                  member_id = member.member_id,
                  parent_member_hierarchy_id = values.Keys.Any(p => p == "ParentMemberHierarchyId")
                                                  ? Convert.ToInt32(values["ParentMemberHierarchyId"])
                                                  : (int?)null,
                  unsubscribe = !Convert.ToBoolean(values["Newsletter"])
               };
               dataProvider.member_hierarchy.Add(memberHierarchy);
               dataProvider.SaveChanges();

               transactionScope.Complete();
            }
         }
         return string.Empty; //we don't have account confirmation
      }

      public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation)
      {
         return CreateUserAndAccount(userName, password, false, null);
      }

      public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation, IDictionary<string, object> values)
      {
         return CreateUserAndAccount(userName, password, values);
      }

      public override bool DeleteAccount(string userName)
      {
         throw new NotImplementedException();
      }

      public override void DeleteOAuthAccount(string provider, string providerUserId)
      {
         throw new NotImplementedException();
      }

      public override void DeleteOAuthToken(string token)
      {
         throw new NotImplementedException();
      }

      public override bool DeleteUser(string username, bool deleteAllRelatedData)
      {
         throw new NotImplementedException();
      }

      public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
      {
         throw new NotImplementedException();
      }

      public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
      {
         throw new NotImplementedException();
      }

      public override string GeneratePasswordResetToken(string userName)
      {
         throw new NotImplementedException();
      }

      public override string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow)
      {
         throw new NotImplementedException();
      }

      public override ICollection<OAuthAccountData> GetAccountsForUser(string userName)
      {
         throw new NotImplementedException();
      }

      public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
      {
         throw new NotImplementedException();
      }

      public override DateTime GetCreateDate(string userName)
      {
         using (var dataProvider = new DataProvider())
         {
            return (from u in dataProvider.users
                    where u.username == userName
                    select u.create_date).First();
         }
      }

      public override DateTime GetLastPasswordFailureDate(string userName)
      {
         return DateTime.Now; //we don't have this functionality
      }

      public override int GetNumberOfUsersOnline()
      {
         return 0; //we don't have this functionality
      }

      public override string GetOAuthTokenSecret(string token)
      {
         throw new NotImplementedException();
      }

      public override string GetPassword(string username, string answer)
      {
         throw new NotImplementedException();
      }

      public override DateTime GetPasswordChangedDate(string userName)
      {
         throw new NotImplementedException();
      }

      public override int GetPasswordFailuresSinceLastSuccess(string userName)
      {
         throw new NotImplementedException();
      }

      public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
      {
         throw new NotImplementedException();
      }

      public override MembershipUser GetUser(string username, bool userIsOnline)
      {
         var email = username.Split('|')[0];
         var partnerId = int.Parse(username.Split('|')[1]);
         using (var dataProvider = new DataProvider())
         {
            var user = (from u in dataProvider.users
                        where u.email_address == email
                        && u.partner_id == partnerId
                        select u);

            return user.Any() ? new MembershipUser(
                Name,
                email,
                null,
                email,
                string.Empty,
                string.Empty,
                true,
                false,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now) : null;
         }
      }

      public override int GetUserIdFromOAuth(string provider, string providerUserId)
      {
         using (var dataProvider = new DataProvider())
         {
            var user = (from u in dataProvider.user_oauthmemberships
                        where u.provider == provider && u.providerUserId == providerUserId
                        select u);
            return user.Any() ? user.First().userId : -1;
         }
      }

      public override int GetUserIdFromPasswordResetToken(string token)
      {
         throw new NotImplementedException();
      }

      public override string GetUserNameByEmail(string email)
      {
         using (var dataProvider = new DataProvider())
         {
            return (from u in dataProvider.users
                    where u.email_address == email
                    select u.username).First();
         }
      }

      public override string GetUserNameFromId(int userId)
      {
         using (var dataProvider = new DataProvider())
         {
            return (from u in dataProvider.users
                    where u.user_id == userId
                    select u.username).First();
         }
      }

      public override bool HasLocalAccount(int userId)
      {
         using (var dataProvider = new DataProvider())
         {
            return (from u in dataProvider.users
                    where u.user_id == userId
                    select u).Any();
         }
      }

      public override bool IsConfirmed(string userName)
      {
         return true; //we don't have this functionlity
      }

      public override void ReplaceOAuthRequestTokenWithAccessToken(string requestToken, string accessToken, string accessTokenSecret)
      {
         throw new NotImplementedException();
      }

      public override string ResetPassword(string username, string answer)
      {
         throw new NotImplementedException();
      }

      public override bool ResetPasswordWithToken(string token, string newPassword)
      {
         throw new NotImplementedException();
      }

      public override void StoreOAuthRequestToken(string requestToken, string requestTokenSecret)
      {
         throw new NotImplementedException();
      }

      public override bool UnlockUser(string userName)
      {
         return true; //we don't have lock functionlity
      }

      public override void UpdateUser(MembershipUser user)
      {
         throw new NotImplementedException();
      }

      public override bool ValidateUser(string userName, string password)
      {
         var email = userName.Split('|')[0];
         var partnerId = int.Parse(userName.Split('|')[1]);
         using (var dataProvider = new DataProvider())
         {
            dataProvider.Configuration.LazyLoadingEnabled = false;
            dataProvider.Configuration.AutoDetectChangesEnabled = false;
            var users = dataProvider.users.Where(u => u.email_address == email && u.password == password && u.partner_id == partnerId).ToList();
            return users.Any(p => p.is_disabled == null || (bool)!p.is_disabled);
         }
      }
   }
}