using GA.BDC.Shared.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Data.EzFund.EZMain.Mappers;
using Dapper;
using System.Text.RegularExpressions;
using Dapper.Contrib.Extensions;
using GA.BDC.Shared.Helpers;

namespace GA.BDC.Data.EzFund.EZMain.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly DataProvider _dataProvider;

        public OrganizationRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public int CreateNewOrganization(EzFundSale model)
        {
            var organization = new EzFundOrganization {
                OrganizationName = model.Client.Organization,
                DepartmentId = 0,
                OrganizationTypeId = 0,
                ISDId = 0,
                LocalCode = null,
                ZipLookUpCode = model.Client.Addresses[1].PostCode, //assigning the shipping zip code
                OrganizationMembersQuantity = 0,
                PrimaryAddressId = 0, //will be changed later in the proccess, when the Contact Address record is been created
                PrimaryContactId = 0, //will be changed later in the proccess,when the Contact record is been created
                Phone1TypeCode = null,
                Phone1 = null,
                Phone2TypeCode = null,
                Phone2 = null,
                Phone3TypeCode = null,
                Phone3 = null,
                FaxTypeCode = null,
                Fax = null,
                WebPage = null,
                SalesPersonCode = "EZ-WEB", //Todo: Define which code is going to be used
                SalesServicePersonCode = "EZ-WEB",
                LeadStatusCode = null,
                LeadReferralModificationDate = null,
                LeadReferralCode = null,
                LeadStatusModificationDate = null,
                PaymentTerminationCode = model.PaymentMethod.ToString(),
                SOLMAccountNumber = null,
                GMAccountNumber = null,
                CreatedDate = DateTime.Now,
                CreatedPersonCode = "EZ-WEB",
                LastModificationDate = DateTime.Now,
                LastModificationPersonCode = "EZ-WEB",
                OrganizationCanonicalName = StringHelper.CanonicalString(model.Client.Organization).ToUpper()
            };
            var organizationEntry = OrganizationMapper.DehydrateOrganization(organization);
            var organizationId = _dataProvider.Database.Connection.Insert(organizationEntry, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);

            var organizationContactAddress = new EzFundContactAddress
            {
                ContactId = 0,
                OrganizationId = (int)organizationId,
                AddressTypeCode = "ORG",
                Address1 = model.Client.Addresses[1].Address1,
                CityName = model.Client.Addresses[1].City,
                StateCode = model.Client.Addresses[1].Region.Code,
                ZipCode = model.Client.Addresses[1].PostCode,
                CountryName = "USA",
                CreatedDate = DateTime.Now,
                CreatedPersonCode = "EZ-WEB",
                LastModificationDate = DateTime.Now,
                LastModificationPersonCode = "EZ-WEB"
            };
            var organizationContact = new EzFundContact
            {
                OrganizationId = (int)organizationId,
                ContactSequencialNumber = 1,
                ContactName = $"{model.Client.FirstName} {model.Client.LastName}",
                ContactTitleId = 0,
                Phone1TypeCode = "UNKN",
                Phone1 = model.Client.Phone,
                Email = model.Client.Email,
                CreatedDate =  DateTime.Now,
                CreatedPersonCode = "EZ-WEB",
                LastModificationDate = DateTime.Now,
                LastModificationPersonCode = "EZ-WEB"
            };

            var organizationContactAddressEntry = OrganizationMapper.DehydrateOrganizationContactAddress(organizationContactAddress);
            var organizationContactAddressId = _dataProvider.Database.Connection.Insert(organizationContactAddressEntry, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            var organizationContactEntry = OrganizationMapper.DehydrateOrganizationContact(organizationContact);
            var organizationContactId = _dataProvider.Database.Connection.Insert(organizationContactEntry, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);

            /*Update the Organization with the new info*/
            organization.OrganizationId = (int)organizationId;
            organization.PrimaryContactId = (int)organizationContactId;
            organization.PrimaryAddressId = (int)organizationContactAddressId;
            var organizationUpdate = OrganizationMapper.DehydrateOrganization(organization);
	        _dataProvider.Database.Connection.Update(organizationUpdate, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);

            return (int)organizationId;
        }

        public void Delete(EzFundOrganization model)
        {
            throw new NotImplementedException();
        }

        public IList<EzFundOrganization> GetAll()
        {
            throw new NotImplementedException();
        }

        public EzFundOrganization GetById(int id)
        {
            const string sqlOrganization = "SELECT TOP 1 * FROM ORG_MSTR_TBL (NOLOCK) WHERE ORG_ID = @id";
            var orgRow = _dataProvider.Database.Connection.QueryFirstOrDefault<org_mstr_tbl>(sqlOrganization,
                  new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            var organization = OrganizationMapper.HydrateOrganization(orgRow);
            
            const string sqlContactAddress = @"SELECT * FROM ORG_CTCT_ADDR_TBL (NOLOCK) WHERE ORG_ID = @id";
            var ctcAddrRows = _dataProvider.Database.Connection.Query<org_ctct_addr_tbl>(sqlContactAddress, new { id },
              _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            foreach (var ctcAddr in ctcAddrRows) {
                organization.ContactAddresses.Add(OrganizationMapper.HydrateOrganizationContactAddress(ctcAddr));
            }

            const string sqlContact = @"SELECT * FROM ORG_CTCT_TBL (NOLOCK) WHERE ORG_ID = @id";
            var cttRows = _dataProvider.Database.Connection.Query<org_ctct_tbl>(sqlContact, new { id },
              _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            foreach (var ctc in cttRows)
            {
                organization.Contacts.Add(OrganizationMapper.HydrateOrganizationContact(ctc));
            }
            return organization;
        }

        public IList<int> GetOrganizationsByClientData(Client client)
        {
            const string sqlAddr1 = "SELECT m.ORG_ID FROM ORG_MSTR_TBL m WITH (NOLOCK) JOIN ORG_CTCT_ADDR_TBL c WITH (NOLOCK) ON m.ORG_ID = c.ORG_ID WHERE c.ZIP_CDE LIKE CONCAT(@zipCode, '%') OR m.ZIP_LKUP_CDE LIKE CONCAT(@zipCode, '%') GROUP BY m.ORG_ID";
            var ids = _dataProvider.Database.Connection.Query<int>(sqlAddr1,
                  new { zipCode = client.Addresses[0].PostCode}, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();

            const string sqlContact = "SELECT ORG_ID FROM ORG_CTCT_TBL (NOLOCK) WHERE lower(CTCT_NME) LIKE CONCAT('%',@firstName,'%',@lastName,'%') GROUP BY ORG_ID";
            ids.AddRange(_dataProvider.Database.Connection.Query<int>(sqlContact,
                  new { firstName = client.FirstName.ToLower(), lastName = client.LastName.ToLower() }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList());

            var regexPhone = String.Format("%{0}%", Regex.Replace(client.Phone, @"[^0-9]", "%"));
            const string sqlMainPhone = "SELECT ORG_ID FROM ORG_MSTR_TBL (NOLOCK) WHERE PH_1_NBR LIKE '@phone' OR PH_2_NBR LIKE '@phone' OR PH_3_NBR LIKE '@phone' GROUP BY ORG_ID";
            ids.AddRange(_dataProvider.Database.Connection.Query<int>(sqlMainPhone,
                  new { phone = regexPhone }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList());

            const string sqlContactPhone = "SELECT ORG_ID FROM ORG_CTCT_TBL (NOLOCK) WHERE PH_1_NBR LIKE '@phone' OR PH_2_NBR LIKE '@phone' OR PH_3_NBR LIKE '@phone' GROUP BY ORG_ID";
            ids.AddRange(_dataProvider.Database.Connection.Query<int>(sqlContactPhone,
                  new { phone = regexPhone }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList());

            const string sqlContactEmail = "SELECT ORG_ID FROM ORG_CTCT_TBL (NOLOCK) WHERE EML_TXT = @email GROUP BY ORG_ID";
            ids.AddRange(_dataProvider.Database.Connection.Query<int>(sqlContactEmail,
                  new { email = client.Email}, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList());

            /* If the shipping and billing address are different
             * We need to get the info from the secind address too
             */
            if (client.Addresses[0].PostCode != client.Addresses[1].PostCode
               || client.Addresses[0].Region != client.Addresses[1].Region
               || client.Addresses[0].City != client.Addresses[1].City
               || client.Addresses[0].Address1 != client.Addresses[1].Address1)
            {
                const string sqlAddr2 = "SELECT m.ORG_ID FROM ORG_MSTR_TBL m WITH (NOLOCK) JOIN ORG_CTCT_ADDR_TBL c WITH (NOLOCK) ON m.ORG_ID = c.ORG_ID WHERE c.ZIP_CDE LIKE CONCAT(@zipCode, '%') OR m.ZIP_LKUP_CDE LIKE CONCAT(@zipCode, '%') GROUP BY m.ORG_ID";
                ids.AddRange(_dataProvider.Database.Connection.Query<int>(sqlAddr2,
                      new { zipCode = client.Addresses[1].PostCode }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList());

                var name = Regex.Replace(client.Addresses[1].AttentionOf, @"\s+", "%");

                const string sqlContact2 = "SELECT ORG_ID FROM ORG_CTCT_TBL (NOLOCK) WHERE lower(CTCT_NME) LIKE @name GROUP BY ORG_ID";
                ids.AddRange(_dataProvider.Database.Connection.Query<int>(sqlContact2,
                      new { name }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList());
            }
            return ids.Distinct().ToList();
        }

		public IList<int> GetOrganizationsByClientDataNew(Client client)
		{
			const string sqlAddressBilling = "SELECT DISTINCT m.ORG_ID FROM ORG_MSTR_TBL m WITH (NOLOCK)"
				+ " JOIN ORG_CTCT_ADDR_TBL c WITH (NOLOCK) ON m.ORG_ID = c.ORG_ID"
				+ " WHERE ( REPLACE(c.ZIP_CDE,' ','') = @zipCode OR REPLACE(m.ZIP_LKUP_CDE,' ','') = @zipCode )"
				+ " AND REPLACE(c.ST_CDE,' ','') = @stateCode"
				+ " GROUP BY m.ORG_ID";
			var ids = _dataProvider.Database.Connection.Query<int>(sqlAddressBilling,
					new { zipCode = StringHelper.RemoveWhiteSpaces(client.Addresses[0].PostCode),
						stateCode = StringHelper.RemoveWhiteSpaces(client.Addresses[0].Region.Code) },
					_dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();


			const string sqlAddressWithCityBilling = "SELECT DISTINCT m.ORG_ID FROM ORG_MSTR_TBL m WITH (NOLOCK)"
				+ " JOIN ORG_CTCT_ADDR_TBL c WITH (NOLOCK) ON m.ORG_ID = c.ORG_ID"
				+ " WHERE (REPLACE(c.ZIP_CDE,' ','') = @zipCode OR REPLACE(m.ZIP_LKUP_CDE,' ','') = @zipCode)"
				+ " AND REPLACE(c.ST_CDE,' ','') = @stateCode AND REPLACE(c.CITY_NME,' ','') = @cityName";
			ids.AddRange(_dataProvider.Database.Connection.Query<int>(sqlAddressWithCityBilling,
					new { zipCode = StringHelper.RemoveWhiteSpaces(client.Addresses[0].PostCode),
							stateCode = StringHelper.RemoveWhiteSpaces(client.Addresses[0].Region.Code),
							cityName = StringHelper.RemoveWhiteSpaces(client.Addresses[0].City) },
					_dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList());


			/* If the shipping and billing address are different
			 * We need to get the info from the second address too
			 */
			if (client.Addresses[0].PostCode != client.Addresses[1].PostCode
				|| client.Addresses[0].Region != client.Addresses[1].Region
				|| client.Addresses[0].City != client.Addresses[1].City)
			{
				const string sqlAddressShipping = "SELECT DISTINCT m.ORG_ID FROM ORG_MSTR_TBL m WITH (NOLOCK)"
				+ " JOIN ORG_CTCT_ADDR_TBL c WITH (NOLOCK) ON m.ORG_ID = c.ORG_ID"
				+ " WHERE ( REPLACE(c.ZIP_CDE,' ','') = @zipCode OR REPLACE(m.ZIP_LKUP_CDE,' ','') = @zipCode )"
				+ " AND REPLACE(c.ST_CDE,' ','') = @stateCode"
				+ " GROUP BY m.ORG_ID";
				ids.AddRange(_dataProvider.Database.Connection.Query<int>(sqlAddressShipping,
						new
						{
							zipCode = StringHelper.RemoveWhiteSpaces(client.Addresses[1].PostCode),
							stateCode = StringHelper.RemoveWhiteSpaces(client.Addresses[1].Region.Code)
						},
						_dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList());

				const string sqlAddressWithCityShipping = "SELECT DISTINCT m.ORG_ID FROM ORG_MSTR_TBL m WITH (NOLOCK)"
				+ " JOIN ORG_CTCT_ADDR_TBL c WITH (NOLOCK) ON m.ORG_ID = c.ORG_ID"
				+ " WHERE (REPLACE(c.ZIP_CDE,' ','') = @zipCode OR REPLACE(m.ZIP_LKUP_CDE,' ','') = @zipCode)"
				+ " AND REPLACE(c.ST_CDE,' ','') = @stateCode AND REPLACE(c.CITY_NME,' ','') = @cityName";
				ids.AddRange(_dataProvider.Database.Connection.Query<int>(sqlAddressWithCityShipping,
						new
						{
							zipCode = StringHelper.RemoveWhiteSpaces(client.Addresses[1].PostCode),
							stateCode = StringHelper.RemoveWhiteSpaces(client.Addresses[1].Region.Code),
							cityName = StringHelper.RemoveWhiteSpaces(client.Addresses[1].City)
						},
						_dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList());
			}
			return ids.Distinct().ToList();
		}

		public IList<int> GetOrganizationsByClientDataNewOLD(Client client)
		{
			const string sqlAddressBilling = "SELECT DISTINCT m.ORG_ID FROM ORG_MSTR_TBL m WITH (NOLOCK)"
				+ " JOIN ORG_CTCT_ADDR_TBL c WITH (NOLOCK) ON m.ORG_ID = c.ORG_ID WHERE c.ZIP_CDE LIKE @zipCode AND c.ST_CDE LIKE @stateCode"
				+ " OR m.ZIP_LKUP_CDE LIKE @zipCode GROUP BY m.ORG_ID";
			var ids = _dataProvider.Database.Connection.Query<int>(sqlAddressBilling,
					new { zipCode = client.Addresses[0].PostCode+"%", stateCode = client.Addresses[0].Region.Code },
					_dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();


			const string sqlAddressWithCityBilling = "SELECT DISTINCT m.ORG_ID FROM ORG_MSTR_TBL m WITH (NOLOCK)"
				+ " JOIN ORG_CTCT_ADDR_TBL c WITH (NOLOCK) ON m.ORG_ID = c.ORG_ID WHERE c.ZIP_CDE LIKE @zipCode AND c.ST_CDE LIKE @stateCode AND CITY_NME LIKE @cityName";
			ids.AddRange(_dataProvider.Database.Connection.Query<int>(sqlAddressWithCityBilling,
					new { zipCode = client.Addresses[0].PostCode ="%", stateCode = client.Addresses[0].Region.Code, cityName = client.Addresses[0].City },
					_dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList());


			/* If the shipping and billing address are different
			 * We need to get the info from the second address too
			 */
			if (client.Addresses[0].PostCode != client.Addresses[1].PostCode
				|| client.Addresses[0].Region != client.Addresses[1].Region
				|| client.Addresses[0].City != client.Addresses[1].City)
			{
				const string sqlAddressShipping = "SELECT DISTINCT m.ORG_ID FROM ORG_MSTR_TBL m WITH (NOLOCK)"
				+ " JOIN ORG_CTCT_ADDR_TBL c WITH (NOLOCK) ON m.ORG_ID = c.ORG_ID WHERE c.ZIP_CDE LIKE CONCAT(@zipCode, '%') AND c.ST_CDE LIKE @stateCode"
				+ " OR m.ZIP_LKUP_CDE LIKE CONCAT(@zipCode, '%') GROUP BY m.ORG_ID";
				ids.AddRange(_dataProvider.Database.Connection.Query<int>(sqlAddressShipping,
						new { zipCode = client.Addresses[1].PostCode, stateCode = client.Addresses[1].Region.Code },
						_dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList());


				const string sqlAddressWithCityShipping = "SELECT DISTINCT m.ORG_ID FROM ORG_MSTR_TBL m WITH (NOLOCK)"
					+ " JOIN ORG_CTCT_ADDR_TBL c WITH (NOLOCK) ON m.ORG_ID = c.ORG_ID WHERE c.ZIP_CDE LIKE CONCAT(@zipCode, '%') AND c.ST_CDE LIKE @stateCode AND CITY_NME LIKE @cityName";
				ids.AddRange(_dataProvider.Database.Connection.Query<int>(sqlAddressWithCityShipping,
						new { zipCode = client.Addresses[1].PostCode, stateCode = client.Addresses[1].Region.Code, cityName = client.Addresses[1].City },
						_dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList());
			}
			return ids.Distinct().ToList();
		}


		public int Save(EzFundOrganization model)
        {
            throw new NotImplementedException();
        }

        public void Update(EzFundOrganization model)
        {
            throw new NotImplementedException();
        }
    }
}
