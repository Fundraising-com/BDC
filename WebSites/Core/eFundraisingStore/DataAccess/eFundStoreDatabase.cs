using System;
using System.Xml;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using GA.BDC.Core.Data.Sql;
using GA.BDC.Core.Configuration;
using System.Collections.Generic;
using GA.BDC.Core.BusinessBase;

namespace GA.BDC.Core.eFundraisingStore.DataAccess {
	/// <summary>
	/// Summary description for eFundWebDatabase.
	/// </summary>
	internal class eFundraisingStoreDatabase : GA.BDC.Core.Data.Sql.DatabaseObject {
		public eFundraisingStoreDatabase() {
			   if(AppConfig.IsEFundraisingStoreProduction) {
				SetConnectionString(AppConfig.EFundraisingStoreConnectionStringRelease);
				SetDataProvider(AppConfig.EFundraisingStoreDataProviderRelease);
			} 
			else {
				SetConnectionString(AppConfig.EFundraisingStoreConnectionStringDebug);
				SetDataProvider(AppConfig.EFundraisingStoreDataProviderDebug);
			}
		}

		#region Countries Methods
		/*
		private Countries LoadCountries(DataRow row) {
			Countries Countries = new Countries();

			// Store database values into our business object
			Countries.CountryCode = DBValue.ToString(row["country_code"]);
			Countries.CountryName = DBValue.ToString(row["country_name"]);
			Countries.LongCountryCode = DBValue.ToString(row["long_country_code"]);
			Countries.NumericCode = DBValue.ToString(row["numeric_code"]);
			Countries.CurrencyCode = DBValue.ToString(row["currency_code"]);

			// return the filled object
			return Countries;
		}

		public Country[] GetCountriess() {
			return GetCountriess(null);}

		private Country[] GetCountriess(SqlInterface si) {
			Country[] countriess = null;

			string storedProcName = "efrstore_get__countriess";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					countriess = new Country[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							countriess[i] = LoadCountry(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return Countriess;
		}


		*/
		#endregion

		#region Country Methods

		/*
		private Country LoadCountry(DataRow row) {
			Country Country = new Country();

			// Store database values into our business object
			Country.CountryCode = DBValue.ToString(row["country_code"]);
			Country.Name = DBValue.ToString(row["country_name"]);
			Country.CurrencyCode = DBValue.ToString(row["currency_code"]);

			// return the filled object
			return Country;
		}

		public Country[] GetCountrys() {
			return GetCountrys(null);}

		private Country[] GetCountrys(SqlInterface si) {
			Country[] Countrys = null;

			string storedProcName = "efrstore_get__countrys";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
		}	

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					Countrys = new Country[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							Countrys[i] = LoadCountry(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return Countrys;
		}

		*/

		#endregion

		#region Languages Methods
		/*
		private Languages LoadLanguages(DataRow row) {
			Languages Languages = new Languages();

			// Store database values into our business object
			Languages.LanguageId = DBValue.ToInt16(row["language_id"]);
			Languages.LanguageName = DBValue.ToString(row["language_name"]);
			Languages.LongLanguageCode = DBValue.ToString(row["long_language_code"]);
			Languages.ShortLanguageCode = DBValue.ToString(row["short_language_code"]);

			// return the filled object
			return Languages;
		}

		public Languages[] GetLanguagess() {
			return GetLanguagess(null);}

		private Languages[] GetLanguagess(SqlInterface si) {
			Languages[] Languagess = null;

			string storedProcName = "efrstore_get__languagess";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					Languagess = new Languages[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							Languagess[i] = LoadLanguages(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return Languagess;
		}


		public Languages GetLanguagesByID(int id) {
			return GetLanguagesByID(id, null);}

		private Languages GetLanguagesByID(int id, SqlInterface si) {
			Languages Languages = null;

			string storedProcName = "efrstore_get__languages_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Language_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						Languages = LoadLanguages(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return Languages;
		}


		public int InsertLanguages(Languages Languages) {
			return InsertLanguages(Languages, null);}

		private int InsertLanguages(Languages Languages, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert__languages";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Language_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Language_name", DbType.String, DBValue.ToDBString(Languages.LanguageName)));
				paramCol.Add(new SqlDataParameter("@Long_language_code", DbType.String, DBValue.ToDBString(Languages.LongLanguageCode)));
				paramCol.Add(new SqlDataParameter("@Short_language_code", DbType.String, DBValue.ToDBString(Languages.ShortLanguageCode)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					Languages.LanguageId = DBValue.ToInt32(paramCol["@Language_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateLanguages(Languages Languages) {
			return UpdateLanguages(Languages, null);}

		private int UpdateLanguages(Languages Languages, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update__languages";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Language_id", DbType.Int16, DBValue.ToDBInt16(Languages.LanguageId)));
				paramCol.Add(new SqlDataParameter("@Language_name", DbType.String, DBValue.ToDBString(Languages.LanguageName)));
				paramCol.Add(new SqlDataParameter("@Long_language_code", DbType.String, DBValue.ToDBString(Languages.LongLanguageCode)));
				paramCol.Add(new SqlDataParameter("@Short_language_code", DbType.String, DBValue.ToDBString(Languages.ShortLanguageCode)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}
*/


		#endregion

		#region State Methods
		/*
		private State LoadState(DataRow row) {
			State State = new State();

			// Store database values into our business object
			State.StateCode = DBValue.ToString(row["state_code"]);
			State.StateName = DBValue.ToString(row["state_name"]);
			State.AvgDeliveryDays = DBValue.ToInt16(row["avg_delivery_days"]);
			State.TimeZoneDifference = DBValue.ToInt32(row["time_zone_difference"]);
			State.CountryCode = DBValue.ToString(row["country_code"]);

			// return the filled object
			return State;
		}

		public State[] GetStates() {
			return GetStates(null);}

		private State[] GetStates(SqlInterface si) {
			State[] States = null;

			string storedProcName = "efrstore_get__states";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					States = new State[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							States[i] = LoadState(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return States;
		}
*/


		#endregion

		#region AccountingClass Methods

		private AccountingClass LoadAccountingClass(DataRow row) {
			AccountingClass accountingClass = new AccountingClass();

			// Store database values into our business object
			accountingClass.AccountingClassId = DBValue.ToInt16(row["accounting_class_id"]);
			accountingClass.CarrierId = DBValue.ToInt16(row["carrier_id"]);
			accountingClass.ShippingOptionId = DBValue.ToInt16(row["shipping_option_id"]);
			accountingClass.Description = DBValue.ToString(row["description"]);
			accountingClass.Rank = DBValue.ToInt32(row["rank"]);
			accountingClass.DeliveryDays = DBValue.ToInt16(row["delivery_days"]);
			accountingClass.ShippingFees = DBValue.ToInt16(row["shipping_fees"]);
			accountingClass.FreeShippingAmount = DBValue.ToInt32(row["free_shipping_amount"]);

			// return the filled object
			return accountingClass;
		}

		public AccountingClass[] GetAccountingClasss() {
			return GetAccountingClasss(null);}

		private AccountingClass[] GetAccountingClasss(SqlInterface si) {
			AccountingClass[] accountingClasss = null;

			string storedProcName = "efrstore_get_accounting_classs";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					accountingClasss = new AccountingClass[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							accountingClasss[i] = LoadAccountingClass(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return accountingClasss;
		}


		public AccountingClass GetAccountingClassByID(int id) {
			return GetAccountingClassByID(id, null);}

		private AccountingClass GetAccountingClassByID(int id, SqlInterface si) {
			AccountingClass accountingClass = null;

			string storedProcName = "efrstore_get_accounting_class_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Accounting_class_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						accountingClass = LoadAccountingClass(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return accountingClass;
		}


		public int InsertAccountingClass(AccountingClass accountingClass) {
			return InsertAccountingClass(accountingClass, null);}

		private int InsertAccountingClass(AccountingClass accountingClass, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_accounting_class";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Accounting_class_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Carrier_id", DbType.Int16, DBValue.ToDBInt16(accountingClass.CarrierId)));
				paramCol.Add(new SqlDataParameter("@Shipping_option_id", DbType.Int16, DBValue.ToDBInt16(accountingClass.ShippingOptionId)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(accountingClass.Description)));
				paramCol.Add(new SqlDataParameter("@Rank", DbType.Int32, DBValue.ToDBInt32(accountingClass.Rank)));
				paramCol.Add(new SqlDataParameter("@Delivery_days", DbType.Int16, DBValue.ToDBInt16(accountingClass.DeliveryDays)));
				paramCol.Add(new SqlDataParameter("@Shipping_fees", DbType.Int16, DBValue.ToDBInt16(accountingClass.ShippingFees)));
				paramCol.Add(new SqlDataParameter("@Free_shipping_amount", DbType.Int32, DBValue.ToDBInt32(accountingClass.FreeShippingAmount)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					accountingClass.AccountingClassId = DBValue.ToInt16(paramCol["@Accounting_class_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateAccountingClass(AccountingClass accountingClass) {
			return UpdateAccountingClass(accountingClass, null);}

		private int UpdateAccountingClass(AccountingClass accountingClass, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_accounting_class";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Accounting_class_id", DbType.Int16, DBValue.ToDBInt16(accountingClass.AccountingClassId)));
				paramCol.Add(new SqlDataParameter("@Carrier_id", DbType.Int16, DBValue.ToDBInt16(accountingClass.CarrierId)));
				paramCol.Add(new SqlDataParameter("@Shipping_option_id", DbType.Int16, DBValue.ToDBInt16(accountingClass.ShippingOptionId)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(accountingClass.Description)));
				paramCol.Add(new SqlDataParameter("@Rank", DbType.Int32, DBValue.ToDBInt32(accountingClass.Rank)));
				paramCol.Add(new SqlDataParameter("@Delivery_days", DbType.Int16, DBValue.ToDBInt16(accountingClass.DeliveryDays)));
				paramCol.Add(new SqlDataParameter("@Shipping_fees", DbType.Int16, DBValue.ToDBInt16(accountingClass.ShippingFees)));
				paramCol.Add(new SqlDataParameter("@Free_shipping_amount", DbType.Int32, DBValue.ToDBInt32(accountingClass.FreeShippingAmount)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region AccountingClassShippingFee Methods

		private AccountingClassShippingFee LoadAccountingClassShippingFee(DataRow row) {
			AccountingClassShippingFee accountingClassShippingFee = new AccountingClassShippingFee();

			// Store database values into our business object
			accountingClassShippingFee.AccountingClassId = DBValue.ToInt16(row["accounting_class_id"]);
			accountingClassShippingFee.MinAmount = DBValue.ToDouble(row["min_amount"]);
			accountingClassShippingFee.MaxAmount = DBValue.ToDouble(row["max_amount"]);
			accountingClassShippingFee.ShippingFee = DBValue.ToInt16(row["shipping_fee"]);

			// return the filled object
			return accountingClassShippingFee;
		}

		public AccountingClassShippingFee[] GetAccountingClassShippingFees() {
			return GetAccountingClassShippingFees(null);}

		private AccountingClassShippingFee[] GetAccountingClassShippingFees(SqlInterface si) {
			AccountingClassShippingFee[] accountingClassShippingFees = null;

			string storedProcName = "efrstore_get_accounting_class_shipping_fees";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					accountingClassShippingFees = new AccountingClassShippingFee[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							accountingClassShippingFees[i] = LoadAccountingClassShippingFee(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return accountingClassShippingFees;
		}


		public AccountingClassShippingFee GetAccountingClassShippingFeeByID(int id) {
			return GetAccountingClassShippingFeeByID(id, null);}

		private AccountingClassShippingFee GetAccountingClassShippingFeeByID(int id, SqlInterface si) {
			AccountingClassShippingFee accountingClassShippingFee = null;

			string storedProcName = "efrstore_get_accounting_class_shipping_fee_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Accounting_class_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						accountingClassShippingFee = LoadAccountingClassShippingFee(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return accountingClassShippingFee;
		}


		public int InsertAccountingClassShippingFee(AccountingClassShippingFee accountingClassShippingFee) {
			return InsertAccountingClassShippingFee(accountingClassShippingFee, null);}

		private int InsertAccountingClassShippingFee(AccountingClassShippingFee accountingClassShippingFee, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_accounting_class_shipping_fee";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Accounting_class_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Min_amount", DbType.Double, DBValue.ToDBDouble(accountingClassShippingFee.MinAmount)));
				paramCol.Add(new SqlDataParameter("@Max_amount", DbType.Double, DBValue.ToDBDouble(accountingClassShippingFee.MaxAmount)));
				paramCol.Add(new SqlDataParameter("@Shipping_fee", DbType.Int16, DBValue.ToDBInt16(accountingClassShippingFee.ShippingFee)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					accountingClassShippingFee.AccountingClassId = DBValue.ToInt16(paramCol["@Accounting_class_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateAccountingClassShippingFee(AccountingClassShippingFee accountingClassShippingFee) {
			return UpdateAccountingClassShippingFee(accountingClassShippingFee, null);}

		private int UpdateAccountingClassShippingFee(AccountingClassShippingFee accountingClassShippingFee, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_accounting_class_shipping_fee";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Accounting_class_id", DbType.Int16, DBValue.ToDBInt16(accountingClassShippingFee.AccountingClassId)));
				paramCol.Add(new SqlDataParameter("@Min_amount", DbType.Double, DBValue.ToDBDouble(accountingClassShippingFee.MinAmount)));
				paramCol.Add(new SqlDataParameter("@Max_amount", DbType.Double, DBValue.ToDBDouble(accountingClassShippingFee.MaxAmount)));
				paramCol.Add(new SqlDataParameter("@Shipping_fee", DbType.Int16, DBValue.ToDBInt16(accountingClassShippingFee.ShippingFee)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region BestTimeCall Methods

		private BestTimeCall LoadBestTimeCall(DataRow row) {
			BestTimeCall bestTimeCall = new BestTimeCall();

			// Store database values into our business object
			bestTimeCall.BestTimeCallId = DBValue.ToInt16(row["best_time_call_id"]);
			bestTimeCall.Description = DBValue.ToString(row["description"]);

			// return the filled object
			return bestTimeCall;
		}

		public BestTimeCall[] GetBestTimeCalls() {
			return GetBestTimeCalls(null);}

		private BestTimeCall[] GetBestTimeCalls(SqlInterface si) {
			BestTimeCall[] bestTimeCalls = null;

			string storedProcName = "efrstore_get_best_time_calls";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					bestTimeCalls = new BestTimeCall[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							bestTimeCalls[i] = LoadBestTimeCall(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return bestTimeCalls;
		}


		public BestTimeCall GetBestTimeCallByID(int id) {
			return GetBestTimeCallByID(id, null);}

		private BestTimeCall GetBestTimeCallByID(int id, SqlInterface si) {
			BestTimeCall bestTimeCall = null;

			string storedProcName = "efrstore_get_best_time_call_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Best_time_call_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						bestTimeCall = LoadBestTimeCall(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return bestTimeCall;
		}


		public int InsertBestTimeCall(BestTimeCall bestTimeCall) {
			return InsertBestTimeCall(bestTimeCall, null);}

		private int InsertBestTimeCall(BestTimeCall bestTimeCall, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_best_time_call";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Best_time_call_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(bestTimeCall.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					bestTimeCall.BestTimeCallId = DBValue.ToInt16(paramCol["@Best_time_call_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateBestTimeCall(BestTimeCall bestTimeCall) {
			return UpdateBestTimeCall(bestTimeCall, null);}

		private int UpdateBestTimeCall(BestTimeCall bestTimeCall, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_best_time_call";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Best_time_call_id", DbType.Int16, DBValue.ToDBInt16(bestTimeCall.BestTimeCallId)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(bestTimeCall.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region BestTimeCallDesc Methods

		private BestTimeCallDesc LoadBestTimeCallDesc(DataRow row) {
			BestTimeCallDesc bestTimeCallDesc = new BestTimeCallDesc();

			// Store database values into our business object
			bestTimeCallDesc.BestTimeCallId = DBValue.ToInt16(row["best_time_call_id"]);
			bestTimeCallDesc.CultureCode = DBValue.ToString(row["culture_code"]);
			bestTimeCallDesc.Description = DBValue.ToString(row["description"]);

			// return the filled object
			return bestTimeCallDesc;
		}

		public BestTimeCallDesc[] GetBestTimeCallDescs() {
			return GetBestTimeCallDescs(null);}

		private BestTimeCallDesc[] GetBestTimeCallDescs(SqlInterface si) {
			BestTimeCallDesc[] bestTimeCallDescs = null;

			string storedProcName = "efrstore_get_best_time_call_descs";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					bestTimeCallDescs = new BestTimeCallDesc[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							bestTimeCallDescs[i] = LoadBestTimeCallDesc(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return bestTimeCallDescs;
		}


		public BestTimeCallDesc GetBestTimeCallDescByID(int id) {
			return GetBestTimeCallDescByID(id, null);}

		private BestTimeCallDesc GetBestTimeCallDescByID(int id, SqlInterface si) {
			BestTimeCallDesc bestTimeCallDesc = null;

			string storedProcName = "efrstore_get_best_time_call_desc_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Best_time_call_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						bestTimeCallDesc = LoadBestTimeCallDesc(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return bestTimeCallDesc;
		}


		public int InsertBestTimeCallDesc(BestTimeCallDesc bestTimeCallDesc) {
			return InsertBestTimeCallDesc(bestTimeCallDesc, null);}

		private int InsertBestTimeCallDesc(BestTimeCallDesc bestTimeCallDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_best_time_call_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Best_time_call_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(bestTimeCallDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(bestTimeCallDesc.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					bestTimeCallDesc.BestTimeCallId = DBValue.ToInt16(paramCol["@Best_time_call_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateBestTimeCallDesc(BestTimeCallDesc bestTimeCallDesc) {
			return UpdateBestTimeCallDesc(bestTimeCallDesc, null);}

		private int UpdateBestTimeCallDesc(BestTimeCallDesc bestTimeCallDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_best_time_call_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Best_time_call_id", DbType.Int16, DBValue.ToDBInt16(bestTimeCallDesc.BestTimeCallId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(bestTimeCallDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(bestTimeCallDesc.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region BrochureImage Methods

		private BrochureImage LoadBrochureImage(DataRow row) {
			BrochureImage brochureImage = new BrochureImage();

			// Store database values into our business object
			brochureImage.BrochureImageId = DBValue.ToInt16(row["brochure_image_id"]);
			brochureImage.ProductId = DBValue.ToInt32(row["product_id"]);
			brochureImage.BaseFilename = DBValue.ToString(row["base_filename"]);
			brochureImage.FileExt = DBValue.ToString(row["file_ext"]);
			brochureImage.NumberPage = DBValue.ToInt16(row["number_page"]);

			// return the filled object
			return brochureImage;
		}

		public BrochureImage[] GetBrochureImages() {
			return GetBrochureImages(null);}

		private BrochureImage[] GetBrochureImages(SqlInterface si) {
			BrochureImage[] brochureImages = null;

			string storedProcName = "efrstore_get_brochure_images";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					brochureImages = new BrochureImage[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							brochureImages[i] = LoadBrochureImage(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return brochureImages;
		}


		public BrochureImage GetBrochureImageByID(int id) {
			return GetBrochureImageByID(id, null);}

		private BrochureImage GetBrochureImageByID(int id, SqlInterface si) {
			BrochureImage brochureImage = null;

			string storedProcName = "efrstore_get_brochure_image_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Brochure_image_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						brochureImage = LoadBrochureImage(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return brochureImage;
		}


		public int InsertBrochureImage(BrochureImage brochureImage) {
			return InsertBrochureImage(brochureImage, null);}

		private int InsertBrochureImage(BrochureImage brochureImage, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_brochure_image";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Brochure_image_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, DBValue.ToDBInt32(brochureImage.ProductId)));
				paramCol.Add(new SqlDataParameter("@Base_filename", DbType.String, DBValue.ToDBString(brochureImage.BaseFilename)));
				paramCol.Add(new SqlDataParameter("@File_ext", DbType.String, DBValue.ToDBString(brochureImage.FileExt)));
				paramCol.Add(new SqlDataParameter("@Number_page", DbType.Int16, DBValue.ToDBInt16(brochureImage.NumberPage)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					brochureImage.BrochureImageId = DBValue.ToInt16(paramCol["@Brochure_image_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateBrochureImage(BrochureImage brochureImage) {
			return UpdateBrochureImage(brochureImage, null);}

		private int UpdateBrochureImage(BrochureImage brochureImage, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_brochure_image";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Brochure_image_id", DbType.Int16, DBValue.ToDBInt16(brochureImage.BrochureImageId)));
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, DBValue.ToDBInt32(brochureImage.ProductId)));
				paramCol.Add(new SqlDataParameter("@Base_filename", DbType.String, DBValue.ToDBString(brochureImage.BaseFilename)));
				paramCol.Add(new SqlDataParameter("@File_ext", DbType.String, DBValue.ToDBString(brochureImage.FileExt)));
				paramCol.Add(new SqlDataParameter("@Number_page", DbType.Int16, DBValue.ToDBInt16(brochureImage.NumberPage)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region CampaignReason Methods

		private CampaignReason LoadCampaignReason(DataRow row) {
			CampaignReason campaignReason = new CampaignReason();

			// Store database values into our business object
			campaignReason.CampaignReasonId = DBValue.ToInt16(row["campaign_reason_id"]);
			campaignReason.PartyTypeId = DBValue.ToInt16(row["party_type_id"]);
			campaignReason.Description = DBValue.ToString(row["description"]);

			// return the filled object
			return campaignReason;
		}

		public CampaignReason[] GetCampaignReasons() {
			return GetCampaignReasons(null);}

		private CampaignReason[] GetCampaignReasons(SqlInterface si) {
			CampaignReason[] campaignReasons = null;

			string storedProcName = "efrstore_get_campaign_reasons";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					campaignReasons = new CampaignReason[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							campaignReasons[i] = LoadCampaignReason(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return campaignReasons;
		}


		public CampaignReason GetCampaignReasonByID(int id) {
			return GetCampaignReasonByID(id, null);}

		private CampaignReason GetCampaignReasonByID(int id, SqlInterface si) {
			CampaignReason campaignReason = null;

			string storedProcName = "efrstore_get_campaign_reason_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Campaign_reason_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						campaignReason = LoadCampaignReason(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return campaignReason;
		}


		public int InsertCampaignReason(CampaignReason campaignReason) {
			return InsertCampaignReason(campaignReason, null);}

		private int InsertCampaignReason(CampaignReason campaignReason, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_campaign_reason";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Campaign_reason_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Party_type_id", DbType.Int16, DBValue.ToDBInt16(campaignReason.PartyTypeId)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(campaignReason.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					campaignReason.CampaignReasonId = DBValue.ToInt16(paramCol["@Campaign_reason_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateCampaignReason(CampaignReason campaignReason) {
			return UpdateCampaignReason(campaignReason, null);}

		private int UpdateCampaignReason(CampaignReason campaignReason, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_campaign_reason";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Campaign_reason_id", DbType.Int16, DBValue.ToDBInt16(campaignReason.CampaignReasonId)));
				paramCol.Add(new SqlDataParameter("@Party_type_id", DbType.Int16, DBValue.ToDBInt16(campaignReason.PartyTypeId)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(campaignReason.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region CampaignReasonDesc Methods

		private CampaignReasonDesc LoadCampaignReasonDesc(DataRow row) {
			CampaignReasonDesc campaignReasonDesc = new CampaignReasonDesc();

			// Store database values into our business object
			campaignReasonDesc.CampaignReasonId = DBValue.ToInt16(row["campaign_reason_id"]);
			campaignReasonDesc.CultureCode = DBValue.ToString(row["culture_code"]);
			campaignReasonDesc.Description = DBValue.ToString(row["description"]);

			// return the filled object
			return campaignReasonDesc;
		}

		public CampaignReasonDesc[] GetCampaignReasonDescs() {
			return GetCampaignReasonDescs(null);}

		private CampaignReasonDesc[] GetCampaignReasonDescs(SqlInterface si) {
			CampaignReasonDesc[] campaignReasonDescs = null;

			string storedProcName = "efrstore_get_campaign_reason_descs";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					campaignReasonDescs = new CampaignReasonDesc[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							campaignReasonDescs[i] = LoadCampaignReasonDesc(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return campaignReasonDescs;
		}


		public CampaignReasonDesc GetCampaignReasonDescByID(int id) {
			return GetCampaignReasonDescByID(id, null);}

		private CampaignReasonDesc GetCampaignReasonDescByID(int id, SqlInterface si) {
			CampaignReasonDesc campaignReasonDesc = null;

			string storedProcName = "efrstore_get_campaign_reason_desc_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Campaign_reason_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						campaignReasonDesc = LoadCampaignReasonDesc(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return campaignReasonDesc;
		}


		public int InsertCampaignReasonDesc(CampaignReasonDesc campaignReasonDesc) {
			return InsertCampaignReasonDesc(campaignReasonDesc, null);}

		private int InsertCampaignReasonDesc(CampaignReasonDesc campaignReasonDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_campaign_reason_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Campaign_reason_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(campaignReasonDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(campaignReasonDesc.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					campaignReasonDesc.CampaignReasonId = DBValue.ToInt16(paramCol["@Campaign_reason_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateCampaignReasonDesc(CampaignReasonDesc campaignReasonDesc) {
			return UpdateCampaignReasonDesc(campaignReasonDesc, null);}

		private int UpdateCampaignReasonDesc(CampaignReasonDesc campaignReasonDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_campaign_reason_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Campaign_reason_id", DbType.Int16, DBValue.ToDBInt16(campaignReasonDesc.CampaignReasonId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(campaignReasonDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(campaignReasonDesc.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region Carrier Methods

		private Carrier LoadCarrier(DataRow row) {
			Carrier carrier = new Carrier();

			// Store database values into our business object
			carrier.CarrierId = DBValue.ToInt16(row["carrier_id"]);
			carrier.Description = DBValue.ToString(row["description"]);

			// return the filled object
			return carrier;
		}

		public Carrier[] GetCarriers() {
			return GetCarriers(null);}

		private Carrier[] GetCarriers(SqlInterface si) {
			Carrier[] carriers = null;

			string storedProcName = "efrstore_get_carriers";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					carriers = new Carrier[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							carriers[i] = LoadCarrier(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return carriers;
		}


		public Carrier GetCarrierByID(int id) {
			return GetCarrierByID(id, null);}

		private Carrier GetCarrierByID(int id, SqlInterface si) {
			Carrier carrier = null;

			string storedProcName = "efrstore_get_carrier_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Carrier_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						carrier = LoadCarrier(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return carrier;
		}


		public int InsertCarrier(Carrier carrier) {
			return InsertCarrier(carrier, null);}

		private int InsertCarrier(Carrier carrier, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_carrier";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Carrier_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(carrier.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					carrier.CarrierId = DBValue.ToInt16(paramCol["@Carrier_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateCarrier(Carrier carrier) {
			return UpdateCarrier(carrier, null);}

		private int UpdateCarrier(Carrier carrier, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_carrier";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Carrier_id", DbType.Int16, DBValue.ToDBInt16(carrier.CarrierId)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(carrier.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region CarrierShippingOption Methods

		private CarrierShippingOption LoadCarrierShippingOption(DataRow row) {
			CarrierShippingOption carrierShippingOption = new CarrierShippingOption();

			// Store database values into our business object
			carrierShippingOption.ShippingOptionId = DBValue.ToInt16(row["shipping_option_id"]);
			carrierShippingOption.Description = DBValue.ToString(row["description"]);

			// return the filled object
			return carrierShippingOption;
		}

		public CarrierShippingOption[] GetCarrierShippingOptions() {
			return GetCarrierShippingOptions(null);}

		private CarrierShippingOption[] GetCarrierShippingOptions(SqlInterface si) {
			CarrierShippingOption[] carrierShippingOptions = null;

			string storedProcName = "efrstore_get_carrier_shipping_options";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					carrierShippingOptions = new CarrierShippingOption[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							carrierShippingOptions[i] = LoadCarrierShippingOption(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return carrierShippingOptions;
		}


		public CarrierShippingOption GetCarrierShippingOptionByID(int id) {
			return GetCarrierShippingOptionByID(id, null);}

		private CarrierShippingOption GetCarrierShippingOptionByID(int id, SqlInterface si) {
			CarrierShippingOption carrierShippingOption = null;

			string storedProcName = "efrstore_get_carrier_shipping_option_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Shipping_option_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						carrierShippingOption = LoadCarrierShippingOption(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return carrierShippingOption;
		}


		public int InsertCarrierShippingOption(CarrierShippingOption carrierShippingOption) {
			return InsertCarrierShippingOption(carrierShippingOption, null);}

		private int InsertCarrierShippingOption(CarrierShippingOption carrierShippingOption, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_carrier_shipping_option";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Shipping_option_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(carrierShippingOption.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					carrierShippingOption.ShippingOptionId = DBValue.ToInt16(paramCol["@Shipping_option_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateCarrierShippingOption(CarrierShippingOption carrierShippingOption) {
			return UpdateCarrierShippingOption(carrierShippingOption, null);}

		private int UpdateCarrierShippingOption(CarrierShippingOption carrierShippingOption, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_carrier_shipping_option";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Shipping_option_id", DbType.Int16, DBValue.ToDBInt16(carrierShippingOption.ShippingOptionId)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(carrierShippingOption.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

        #region Choice Methods

        private Choice LoadChoice(DataRow row)
        {
            Choice choice = new Choice();

            // Store database values into our business object
            choice.ChoiceId = DBValue.ToInt32(row["choice_id"]);
            choice.ChoiceDesc = DBValue.ToString(row["choice_desc"]);
            choice.Location = DBValue.ToString(row["location"]);
            choice.Image = DBValue.ToString(row["image"]);
           
            // return the filled object
            return choice;
        }

        public Choice[] GetChoices()
        {
            return GetChoices(null);
        }

        private Choice[] GetChoices(SqlInterface si)
        {
            Choice[] choices = null;

            string storedProcName = "efrstore_get_choices";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    choices = new Choice[dt.Rows.Count];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            choices[i] = LoadChoice(dt.Rows[i]);
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return choices;
        }

        public Choice GetChoiceByChoiceID(int choiceId)
        {
            return GetChoiceByChoiceID(choiceId, null);
        }

        private Choice GetChoiceByChoiceID(int choiceId, SqlInterface si)
        {
            Choice choice = null;

            string storedProcName = "efrstore_get_choice_by_choice_id";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@Choice_id", DbType.Int32, DBValue.ToDBInt32(choiceId)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // fill our objects
                    try
                    {
                        choice = LoadChoice(dt.Rows[0]);
                    }
                    catch (Exception ex)
                    {
                        throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return choice;
        }

        #endregion

		#region Client Methods

		private Client LoadClient(DataRow row) {
			Client client = new Client();

			// Store database values into our business object
			client.ClientSequenceCode = DBValue.ToString(row["client_sequence_code"]);
			client.ClientId = DBValue.ToInt32(row["client_id"]);
			client.OrganizationClassCode = DBValue.ToString(row["organization_class_code"]);
			client.GroupTypeId = DBValue.ToInt32(row["group_type_id"]);
			client.ChannelCode = DBValue.ToString(row["channel_code"]);
			client.PromotionId = DBValue.ToInt32(row["promotion_id"]);
			client.LeadId = DBValue.ToInt32(row["lead_id"]);
			client.DivisionId = DBValue.ToInt32(row["division_id"]);
			client.CsrConsultantId = DBValue.ToInt32(row["csr_consultant_id"]);
			client.TitleId = DBValue.ToInt32(row["title_id"]);
			client.Salutation = DBValue.ToString(row["salutation"]);
			client.FirstName = DBValue.ToString(row["first_name"]);
			client.LastName = DBValue.ToString(row["last_name"]);
			client.Title = DBValue.ToString(row["title"]);
			client.Organization = DBValue.ToString(row["organization"]);
			client.DayPhone = DBValue.ToString(row["day_phone"]);
			client.DayTimeCall = DBValue.ToString(row["day_time_call"]);
			client.EveningPhone = DBValue.ToString(row["evening_phone"]);
			client.EveningTimeCall = DBValue.ToString(row["evening_time_call"]);
			client.Fax = DBValue.ToString(row["fax"]);
			client.Email = DBValue.ToString(row["email"]);
			client.ExtraComment = DBValue.ToString(row["extra_comment"]);
			client.InterestedInAgent = DBValue.ToInt16(row["interested_in_agent"]);
			client.InterestedInOnline = DBValue.ToInt16(row["interested_in_online"]);
			client.DayPhoneExt = DBValue.ToString(row["day_phone_ext"]);
			client.EveningPhoneExt = DBValue.ToString(row["evening_phone_ext"]);
			client.OtherPhone = DBValue.ToString(row["other_phone"]);
			client.OtherPhoneExt = DBValue.ToString(row["other_phone_ext"]);

			// return the filled object
			return client;
		}

		public Client[] GetClients() {
			return GetClients(null);}

		private Client[] GetClients(SqlInterface si) {
			Client[] clients = null;

			string storedProcName = "efrstore_get_clients";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					clients = new Client[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							clients[i] = LoadClient(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return clients;
		}



		#endregion

		#region ClientAddress Methods

		private ClientAddress LoadClientAddress(DataRow row) {
			ClientAddress clientAddress = new ClientAddress();

			// Store database values into our business object
			clientAddress.AddressId = DBValue.ToInt32(row["address_id"]);
			clientAddress.ClientSequenceCode = DBValue.ToString(row["client_sequence_code"]);
			clientAddress.ClientId = DBValue.ToInt32(row["client_id"]);
			clientAddress.AddressType = DBValue.ToString(row["address_type"]);
			clientAddress.StreetAddress = DBValue.ToString(row["street_address"]);
			clientAddress.StateCode = DBValue.ToString(row["state_code"]);
			clientAddress.City = DBValue.ToString(row["city"]);
			clientAddress.ZipCode = DBValue.ToString(row["zip_code"]);
			clientAddress.CountryCode = DBValue.ToString(row["country_code"]);
			clientAddress.AttentionOf = DBValue.ToString(row["attention_of"]);

			// return the filled object
			return clientAddress;
		}

		public ClientAddress[] GetClientAddresss() {
			return GetClientAddresss(null);}

		private ClientAddress[] GetClientAddresss(SqlInterface si) {
			ClientAddress[] clientAddresss = null;

			string storedProcName = "efrstore_get_client_addresss";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					clientAddresss = new ClientAddress[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							clientAddresss[i] = LoadClientAddress(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return clientAddresss;
		}


		public ClientAddress GetClientAddressByID(int id) {
			return GetClientAddressByID(id, null);}

		private ClientAddress GetClientAddressByID(int id, SqlInterface si) {
			ClientAddress clientAddress = null;

			string storedProcName = "efrstore_get_client_address_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Address_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						clientAddress = LoadClientAddress(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return clientAddress;
		}


		public int InsertClientAddress(ClientAddress clientAddress) {
			return InsertClientAddress(clientAddress, null);}

		private int InsertClientAddress(ClientAddress clientAddress, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_client_address";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Address_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Client_sequence_code", DbType.String, DBValue.ToDBString(clientAddress.ClientSequenceCode)));
				paramCol.Add(new SqlDataParameter("@Client_id", DbType.Int32, DBValue.ToDBInt32(clientAddress.ClientId)));
				paramCol.Add(new SqlDataParameter("@Address_type", DbType.String, DBValue.ToDBString(clientAddress.AddressType)));
				paramCol.Add(new SqlDataParameter("@Street_address", DbType.String, DBValue.ToDBString(clientAddress.StreetAddress)));
				paramCol.Add(new SqlDataParameter("@State_code", DbType.String, DBValue.ToDBString(clientAddress.StateCode)));
				paramCol.Add(new SqlDataParameter("@City", DbType.String, DBValue.ToDBString(clientAddress.City)));
				paramCol.Add(new SqlDataParameter("@Zip_code", DbType.String, DBValue.ToDBString(clientAddress.ZipCode)));
				paramCol.Add(new SqlDataParameter("@Country_code", DbType.String, DBValue.ToDBString(clientAddress.CountryCode)));
				paramCol.Add(new SqlDataParameter("@Attention_of", DbType.String, DBValue.ToDBString(clientAddress.AttentionOf)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					clientAddress.AddressId = DBValue.ToInt32(paramCol["@Address_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateClientAddress(ClientAddress clientAddress) {
			return UpdateClientAddress(clientAddress, null);}

		private int UpdateClientAddress(ClientAddress clientAddress, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_client_address";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Address_id", DbType.Int32, DBValue.ToDBInt32(clientAddress.AddressId)));
				paramCol.Add(new SqlDataParameter("@Client_sequence_code", DbType.String, DBValue.ToDBString(clientAddress.ClientSequenceCode)));
				paramCol.Add(new SqlDataParameter("@Client_id", DbType.Int32, DBValue.ToDBInt32(clientAddress.ClientId)));
				paramCol.Add(new SqlDataParameter("@Address_type", DbType.String, DBValue.ToDBString(clientAddress.AddressType)));
				paramCol.Add(new SqlDataParameter("@Street_address", DbType.String, DBValue.ToDBString(clientAddress.StreetAddress)));
				paramCol.Add(new SqlDataParameter("@State_code", DbType.String, DBValue.ToDBString(clientAddress.StateCode)));
				paramCol.Add(new SqlDataParameter("@City", DbType.String, DBValue.ToDBString(clientAddress.City)));
				paramCol.Add(new SqlDataParameter("@Zip_code", DbType.String, DBValue.ToDBString(clientAddress.ZipCode)));
				paramCol.Add(new SqlDataParameter("@Country_code", DbType.String, DBValue.ToDBString(clientAddress.CountryCode)));
				paramCol.Add(new SqlDataParameter("@Attention_of", DbType.String, DBValue.ToDBString(clientAddress.AttentionOf)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region ClientAddressType Methods

		private ClientAddressType LoadClientAddressType(DataRow row) {
			ClientAddressType clientAddressType = new ClientAddressType();

			// Store database values into our business object
			clientAddressType.ClientAddressTypeId = DBValue.ToString(row["client_address_type_id"]);
			clientAddressType.Description = DBValue.ToString(row["description"]);

			// return the filled object
			return clientAddressType;
		}

		public ClientAddressType[] GetClientAddressTypes() {
			return GetClientAddressTypes(null);}

		private ClientAddressType[] GetClientAddressTypes(SqlInterface si) {
			ClientAddressType[] clientAddressTypes = null;

			string storedProcName = "efrstore_get_client_address_types";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					clientAddressTypes = new ClientAddressType[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							clientAddressTypes[i] = LoadClientAddressType(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return clientAddressTypes;
		}



		#endregion

		#region ClientSequence Methods

		private ClientSequence LoadClientSequence(DataRow row) {
			ClientSequence clientSequence = new ClientSequence();

			// Store database values into our business object
			clientSequence.ClientSequenceCode = DBValue.ToString(row["client_sequence_code"]);
			clientSequence.Description = DBValue.ToString(row["description"]);
			clientSequence.IsActive = DBValue.ToInt16(row["is_active"]);

			// return the filled object
			return clientSequence;
		}

		public ClientSequence[] GetClientSequences() {
			return GetClientSequences(null);}

		private ClientSequence[] GetClientSequences(SqlInterface si) {
			ClientSequence[] clientSequences = null;

			string storedProcName = "efrstore_get_client_sequences";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					clientSequences = new ClientSequence[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							clientSequences[i] = LoadClientSequence(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return clientSequences;
		}



		#endregion

		#region ControlType Methods

		private ControlType LoadControlType(DataRow row) {
			ControlType controlType = new ControlType();

			// Store database values into our business object
			controlType.ControlTypeId = DBValue.ToInt32(row["control_type_id"]);
			controlType.AssemblyName = DBValue.ToString(row["assembly_name"]);
			controlType.Namespace = DBValue.ToString(row["namespace"]);
			controlType.ClassName = DBValue.ToString(row["class_name"]);
			controlType.DisplayAttribute = DBValue.ToString(row["display_attribute"]);
			controlType.BindingName = DBValue.ToString(row["binding_name"]);
			controlType.EventHandlerName = DBValue.ToString(row["event_handler_name"]);
			controlType.AutoPostBack = DBValue.ToInt16(row["auto_post_back"]);
			controlType.Datestamp = DBValue.ToDateTime(row["datestamp"]);

			// return the filled object
			return controlType;
		}

		public ControlType[] GetControlTypes() {
			return GetControlTypes(null);}

		private ControlType[] GetControlTypes(SqlInterface si) {
			ControlType[] controlTypes = null;

			string storedProcName = "efrstore_get_control_types";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					controlTypes = new ControlType[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							controlTypes[i] = LoadControlType(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return controlTypes;
		}


		public ControlType GetControlTypeByID(int id) {
			return GetControlTypeByID(id, null);}

		private ControlType GetControlTypeByID(int id, SqlInterface si) {
			ControlType controlType = null;

			string storedProcName = "efrstore_get_control_type_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Control_type_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						controlType = LoadControlType(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return controlType;
		}


		public int InsertControlType(ControlType controlType) {
			return InsertControlType(controlType, null);}

		private int InsertControlType(ControlType controlType, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_control_type";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Control_type_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Assembly_name", DbType.String, DBValue.ToDBString(controlType.AssemblyName)));
				paramCol.Add(new SqlDataParameter("@Namespace", DbType.String, DBValue.ToDBString(controlType.Namespace)));
				paramCol.Add(new SqlDataParameter("@Class_name", DbType.String, DBValue.ToDBString(controlType.ClassName)));
				paramCol.Add(new SqlDataParameter("@Display_attribute", DbType.String, DBValue.ToDBString(controlType.DisplayAttribute)));
				paramCol.Add(new SqlDataParameter("@Binding_name", DbType.String, DBValue.ToDBString(controlType.BindingName)));
				paramCol.Add(new SqlDataParameter("@Event_handler_name", DbType.String, DBValue.ToDBString(controlType.EventHandlerName)));
				paramCol.Add(new SqlDataParameter("@Auto_post_back", DbType.Int16, DBValue.ToDBInt16(controlType.AutoPostBack)));
				paramCol.Add(new SqlDataParameter("@Datestamp", DbType.DateTime, DBValue.ToDBDateTime(controlType.Datestamp)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					controlType.ControlTypeId = DBValue.ToInt32(paramCol["@Control_type_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateControlType(ControlType controlType) {
			return UpdateControlType(controlType, null);}

		private int UpdateControlType(ControlType controlType, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_control_type";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Control_type_id", DbType.Int32, DBValue.ToDBInt32(controlType.ControlTypeId)));
				paramCol.Add(new SqlDataParameter("@Assembly_name", DbType.String, DBValue.ToDBString(controlType.AssemblyName)));
				paramCol.Add(new SqlDataParameter("@Namespace", DbType.String, DBValue.ToDBString(controlType.Namespace)));
				paramCol.Add(new SqlDataParameter("@Class_name", DbType.String, DBValue.ToDBString(controlType.ClassName)));
				paramCol.Add(new SqlDataParameter("@Display_attribute", DbType.String, DBValue.ToDBString(controlType.DisplayAttribute)));
				paramCol.Add(new SqlDataParameter("@Binding_name", DbType.String, DBValue.ToDBString(controlType.BindingName)));
				paramCol.Add(new SqlDataParameter("@Event_handler_name", DbType.String, DBValue.ToDBString(controlType.EventHandlerName)));
				paramCol.Add(new SqlDataParameter("@Auto_post_back", DbType.Int16, DBValue.ToDBInt16(controlType.AutoPostBack)));
				paramCol.Add(new SqlDataParameter("@Datestamp", DbType.DateTime, DBValue.ToDBDateTime(controlType.Datestamp)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region Country Methods
		

		private Country LoadCountry(DataRow row) {
			Country country = new Country();

			// Store database values into our business object
			country.CountryCode = DBValue.ToString(row["country_code"]);
			country.Name = DBValue.ToString(row["Counry_Name"]);
			//country.DescriptiveInformation = DBValue.ToString(row["descriptive_information"]);
			country.Display = DBValue.ToInt16(row["display"]);
			country.CurrencyCode = DBValue.ToString(row["Currency_Code"]);

			// return the filled object
			return country;
		}

		public Country[] GetCountrys() {
			return GetCountrys(null);}

		private Country[] GetCountrys(SqlInterface si) {
			Country[] countrys = null;

			string storedProcName = "efr_get_country";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					countrys = new Country[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							countrys[i] = LoadCountry(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return countrys;
		}

		
		#endregion

		#region CreditCard Methods

		private CreditCard LoadCreditCard(DataRow row) {
			CreditCard creditCard = new CreditCard();

			// Store database values into our business object
			creditCard.CreditCardId = DBValue.ToInt32(row["credit_card_id"]);
			creditCard.OnlineUserId = DBValue.ToInt32(row["online_user_id"]);
			creditCard.CreditCardTypeId = DBValue.ToInt16(row["credit_card_type_id"]);
			creditCard.CreditCardNumber = DBValue.ToString(row["credit_card"]);
			creditCard.LastDigits = DBValue.ToString(row["last_digits"]);
			creditCard.YearExpire = DBValue.ToInt16(row["year_expire"]);
			creditCard.MonthExpire = DBValue.ToInt16(row["month_expire"]);
			creditCard.DateCreated = DBValue.ToDateTime(row["date_created"]);
			creditCard.Removed = DBValue.ToInt16(row["removed"]);

			// return the filled object
			return creditCard;
		}

		public CreditCard[] GetCreditCards() {
			return GetCreditCards(null);}

		private CreditCard[] GetCreditCards(SqlInterface si) {
			CreditCard[] creditCards = null;

			string storedProcName = "efrstore_get_credit_cards";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					creditCards = new CreditCard[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							creditCards[i] = LoadCreditCard(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return creditCards;
		}


		public CreditCard GetCreditCardByID(int id) {
			return GetCreditCardByID(id, null);}

		private CreditCard GetCreditCardByID(int id, SqlInterface si) {
			CreditCard creditCard = null;

			string storedProcName = "efrstore_get_credit_card_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Credit_card_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						creditCard = LoadCreditCard(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return creditCard;
		}


		public int InsertCreditCard(CreditCard creditCard) {
			return InsertCreditCard(creditCard, null);}

		private int InsertCreditCard(CreditCard creditCard, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_credit_card";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Credit_card_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Online_user_id", DbType.Int32, DBValue.ToDBInt32(creditCard.OnlineUserId)));
				paramCol.Add(new SqlDataParameter("@Credit_card_type_id", DbType.Int16, DBValue.ToDBInt16(creditCard.CreditCardTypeId)));
				paramCol.Add(new SqlDataParameter("@Credit_card", DbType.String, DBValue.ToDBString(creditCard.CreditCardNumber)));
				paramCol.Add(new SqlDataParameter("@Last_digits", DbType.String, DBValue.ToDBString(creditCard.LastDigits)));
				paramCol.Add(new SqlDataParameter("@Year_expire", DbType.Int16, DBValue.ToDBInt16(creditCard.YearExpire)));
				paramCol.Add(new SqlDataParameter("@Month_expire", DbType.Int16, DBValue.ToDBInt16(creditCard.MonthExpire)));
				paramCol.Add(new SqlDataParameter("@Date_created", DbType.DateTime, DBValue.ToDBDateTime(creditCard.DateCreated)));
				paramCol.Add(new SqlDataParameter("@Removed", DbType.Int16, DBValue.ToDBInt16(creditCard.Removed)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					creditCard.CreditCardId = DBValue.ToInt32(paramCol["@Credit_card_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateCreditCard(CreditCard creditCard) {
			return UpdateCreditCard(creditCard, null);}

		private int UpdateCreditCard(CreditCard creditCard, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_credit_card";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Credit_card_id", DbType.Int32, DBValue.ToDBInt32(creditCard.CreditCardId)));
				paramCol.Add(new SqlDataParameter("@Online_user_id", DbType.Int32, DBValue.ToDBInt32(creditCard.OnlineUserId)));
				paramCol.Add(new SqlDataParameter("@Credit_card_type_id", DbType.Int16, DBValue.ToDBInt16(creditCard.CreditCardTypeId)));
				paramCol.Add(new SqlDataParameter("@Credit_card", DbType.String, DBValue.ToDBString(creditCard.CreditCardNumber)));
				paramCol.Add(new SqlDataParameter("@Last_digits", DbType.String, DBValue.ToDBString(creditCard.LastDigits)));
				paramCol.Add(new SqlDataParameter("@Year_expire", DbType.Int16, DBValue.ToDBInt16(creditCard.YearExpire)));
				paramCol.Add(new SqlDataParameter("@Month_expire", DbType.Int16, DBValue.ToDBInt16(creditCard.MonthExpire)));
				paramCol.Add(new SqlDataParameter("@Date_created", DbType.DateTime, DBValue.ToDBDateTime(creditCard.DateCreated)));
				paramCol.Add(new SqlDataParameter("@Removed", DbType.Int16, DBValue.ToDBInt16(creditCard.Removed)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region CreditCardType Methods

		private CreditCardType LoadCreditCardType(DataRow row) {
			CreditCardType creditCardType = new CreditCardType();

			// Store database values into our business object
			creditCardType.CreditCardTypeId = DBValue.ToInt16(row["credit_card_type_id"]);
			creditCardType.PaymentMethodId = DBValue.ToInt16(row["payment_method_id"]);
			creditCardType.CreditCardTypeName = DBValue.ToString(row["credit_card_type_name"]);
			creditCardType.CreditCardImage = DBValue.ToString(row["credit_card_image"]);
			creditCardType.DisplayOrder = DBValue.ToInt16(row["display_order"]);
			creditCardType.Displayable = DBValue.ToInt16(row["displayable"]);

			// return the filled object
			return creditCardType;
		}

		public CreditCardType[] GetCreditCardTypes() {
			return GetCreditCardTypes(null);}

		private CreditCardType[] GetCreditCardTypes(SqlInterface si) {
			CreditCardType[] creditCardTypes = null;

			string storedProcName = "efrstore_get_credit_card_types";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					creditCardTypes = new CreditCardType[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							creditCardTypes[i] = LoadCreditCardType(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return creditCardTypes;
		}


		public CreditCardType GetCreditCardTypeByID(int id) {
			return GetCreditCardTypeByID(id, null);}

		private CreditCardType GetCreditCardTypeByID(int id, SqlInterface si) {
			CreditCardType creditCardType = null;

			string storedProcName = "efrstore_get_credit_card_type_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Credit_card_type_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						creditCardType = LoadCreditCardType(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return creditCardType;
		}


		public int InsertCreditCardType(CreditCardType creditCardType) {
			return InsertCreditCardType(creditCardType, null);}

		private int InsertCreditCardType(CreditCardType creditCardType, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_credit_card_type";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Credit_card_type_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Payment_method_id", DbType.Int16, DBValue.ToDBInt16(creditCardType.PaymentMethodId)));
				paramCol.Add(new SqlDataParameter("@Credit_card_type_name", DbType.String, DBValue.ToDBString(creditCardType.CreditCardTypeName)));
				paramCol.Add(new SqlDataParameter("@Credit_card_image", DbType.String, DBValue.ToDBString(creditCardType.CreditCardImage)));
				paramCol.Add(new SqlDataParameter("@Display_order", DbType.Int16, DBValue.ToDBInt16(creditCardType.DisplayOrder)));
				paramCol.Add(new SqlDataParameter("@Displayable", DbType.Int16, DBValue.ToDBInt16(creditCardType.Displayable)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					creditCardType.CreditCardTypeId = DBValue.ToInt16(paramCol["@Credit_card_type_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateCreditCardType(CreditCardType creditCardType) {
			return UpdateCreditCardType(creditCardType, null);}

		private int UpdateCreditCardType(CreditCardType creditCardType, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_credit_card_type";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Credit_card_type_id", DbType.Int16, DBValue.ToDBInt16(creditCardType.CreditCardTypeId)));
				paramCol.Add(new SqlDataParameter("@Payment_method_id", DbType.Int16, DBValue.ToDBInt16(creditCardType.PaymentMethodId)));
				paramCol.Add(new SqlDataParameter("@Credit_card_type_name", DbType.String, DBValue.ToDBString(creditCardType.CreditCardTypeName)));
				paramCol.Add(new SqlDataParameter("@Credit_card_image", DbType.String, DBValue.ToDBString(creditCardType.CreditCardImage)));
				paramCol.Add(new SqlDataParameter("@Display_order", DbType.Int16, DBValue.ToDBInt16(creditCardType.DisplayOrder)));
				paramCol.Add(new SqlDataParameter("@Displayable", DbType.Int16, DBValue.ToDBInt16(creditCardType.Displayable)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region Culture Methods

		private Culture LoadCulture(DataRow row) {
			Culture culture = new Culture();

			// Store database values into our business object
			culture.CultureCode = DBValue.ToString(row["culture_code"]);
			culture.CountryCode = DBValue.ToString(row["country_code"]);
			culture.LanguageCode = DBValue.ToString(row["language_code"]);
			culture.CultureName = DBValue.ToString(row["culture_name"]);

			// return the filled object
			return culture;
		}

		public Culture[] GetCultures() {
			return GetCultures(null);}

		private Culture[] GetCultures(SqlInterface si) {
			Culture[] cultures = null;

			string storedProcName = "efrstore_get_cultures";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					cultures = new Culture[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							cultures[i] = LoadCulture(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return cultures;
		}



		#endregion

		#region CultureCountry Methods

		private CultureCountry LoadCultureCountry(DataRow row) {
			CultureCountry cultureCountry = new CultureCountry();

			// Store database values into our business object
			cultureCountry.CultureCode = DBValue.ToString(row["culture_code"]);
			cultureCountry.CountryCode = DBValue.ToString(row["country_code"]);
			cultureCountry.Name = DBValue.ToString(row["name"]);

			// return the filled object
			return cultureCountry;
		}

		public CultureCountry[] GetCultureCountrys() {
			return GetCultureCountrys(null);}

		private CultureCountry[] GetCultureCountrys(SqlInterface si) {
			CultureCountry[] cultureCountrys = null;

			string storedProcName = "efrstore_get_culture_countrys";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					cultureCountrys = new CultureCountry[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							cultureCountrys[i] = LoadCultureCountry(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return cultureCountrys;
		}



		#endregion

		#region CultureSubdivision Methods

		private CultureSubdivision LoadCultureSubdivision(DataRow row) {
			CultureSubdivision cultureSubdivision = new CultureSubdivision();

			// Store database values into our business object
			cultureSubdivision.CultureCode = DBValue.ToString(row["culture_code"]);
			cultureSubdivision.SubdivisionCode = DBValue.ToString(row["subdivision_code"]);
			cultureSubdivision.Name = DBValue.ToString(row["name"]);

			// return the filled object
			return cultureSubdivision;
		}

		public CultureSubdivision[] GetCultureSubdivisions() {
			return GetCultureSubdivisions(null);}

		private CultureSubdivision[] GetCultureSubdivisions(SqlInterface si) {
			CultureSubdivision[] cultureSubdivisions = null;

			string storedProcName = "efrstore_get_culture_subdivisions";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					cultureSubdivisions = new CultureSubdivision[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							cultureSubdivisions[i] = LoadCultureSubdivision(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return cultureSubdivisions;
		}



		#endregion

		#region Division Methods

		private Division LoadDivision(DataRow row) {
			Division division = new Division();

			// Store database values into our business object
			division.DivisionId = DBValue.ToInt32(row["division_id"]);
			division.Name = DBValue.ToString(row["name"]);
			division.Logo = DBValue.ToString(row["logo"]);
			division.ShortName = DBValue.ToString(row["short_name"]);

			// return the filled object
			return division;
		}

		public Division[] GetDivisions() {
			return GetDivisions(null);}

		private Division[] GetDivisions(SqlInterface si) {
			Division[] divisions = null;

			string storedProcName = "efrstore_get_divisions";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					divisions = new Division[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							divisions[i] = LoadDivision(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return divisions;
		}


		public Division GetDivisionByID(int id) {
			return GetDivisionByID(id, null);}

		private Division GetDivisionByID(int id, SqlInterface si) {
			Division division = null;

			string storedProcName = "efrstore_get_division_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Division_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						division = LoadDivision(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return division;
		}


		public int InsertDivision(Division division) {
			return InsertDivision(division, null);}

		private int InsertDivision(Division division, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_division";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Division_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(division.Name)));
				paramCol.Add(new SqlDataParameter("@Logo", DbType.String, DBValue.ToDBString(division.Logo)));
				paramCol.Add(new SqlDataParameter("@Short_name", DbType.String, DBValue.ToDBString(division.ShortName)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					division.DivisionId = DBValue.ToInt32(paramCol["@Division_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateDivision(Division division) {
			return UpdateDivision(division, null);}

		private int UpdateDivision(Division division, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_division";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Division_id", DbType.Int32, DBValue.ToDBInt32(division.DivisionId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(division.Name)));
				paramCol.Add(new SqlDataParameter("@Logo", DbType.String, DBValue.ToDBString(division.Logo)));
				paramCol.Add(new SqlDataParameter("@Short_name", DbType.String, DBValue.ToDBString(division.ShortName)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region GroupType Methods

		private GroupType LoadGroupType(DataRow row) {
			GroupType groupType = new GroupType();

			// Store database values into our business object
			groupType.GroupTypeId = DBValue.ToInt16(row["group_type_id"]);
			groupType.PartyTypeId = DBValue.ToInt16(row["party_type_id"]);
			groupType.Description = DBValue.ToString(row["description"]);

			// return the filled object
			return groupType;
		}

		public GroupType[] GetGroupTypes() {
			return GetGroupTypes(null);}

		private GroupType[] GetGroupTypes(SqlInterface si) {
			GroupType[] groupTypes = null;

			string storedProcName = "efrstore_get_group_types";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					groupTypes = new GroupType[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							groupTypes[i] = LoadGroupType(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return groupTypes;
		}


		public GroupType GetGroupTypeByID(int id) {
			return GetGroupTypeByID(id, null);}

		private GroupType GetGroupTypeByID(int id, SqlInterface si) {
			GroupType groupType = null;

			string storedProcName = "efrstore_get_group_type_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Group_type_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						groupType = LoadGroupType(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return groupType;
		}


		public int InsertGroupType(GroupType groupType) {
			return InsertGroupType(groupType, null);}

		private int InsertGroupType(GroupType groupType, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_group_type";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Group_type_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Party_type_id", DbType.Int16, DBValue.ToDBInt16(groupType.PartyTypeId)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(groupType.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					groupType.GroupTypeId = DBValue.ToInt16(paramCol["@Group_type_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateGroupType(GroupType groupType) {
			return UpdateGroupType(groupType, null);}

		private int UpdateGroupType(GroupType groupType, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_group_type";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Group_type_id", DbType.Int16, DBValue.ToDBInt16(groupType.GroupTypeId)));
				paramCol.Add(new SqlDataParameter("@Party_type_id", DbType.Int16, DBValue.ToDBInt16(groupType.PartyTypeId)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(groupType.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region GroupTypeDesc Methods

		private GroupTypeDesc LoadGroupTypeDesc(DataRow row) {
			GroupTypeDesc groupTypeDesc = new GroupTypeDesc();

			// Store database values into our business object
			groupTypeDesc.GroupTypeId = DBValue.ToInt16(row["group_type_id"]);
			groupTypeDesc.CultureCode = DBValue.ToString(row["culture_code"]);
			groupTypeDesc.Description = DBValue.ToString(row["description"]);

			// return the filled object
			return groupTypeDesc;
		}

		public GroupTypeDesc[] GetGroupTypeDescs() {
			return GetGroupTypeDescs(null);}

		private GroupTypeDesc[] GetGroupTypeDescs(SqlInterface si) {
			GroupTypeDesc[] groupTypeDescs = null;

			string storedProcName = "efrstore_get_group_type_descs";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					groupTypeDescs = new GroupTypeDesc[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							groupTypeDescs[i] = LoadGroupTypeDesc(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return groupTypeDescs;
		}


		public GroupTypeDesc GetGroupTypeDescByID(int id) {
			return GetGroupTypeDescByID(id, null);}

		private GroupTypeDesc GetGroupTypeDescByID(int id, SqlInterface si) {
			GroupTypeDesc groupTypeDesc = null;

			string storedProcName = "efrstore_get_group_type_desc_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Group_type_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						groupTypeDesc = LoadGroupTypeDesc(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return groupTypeDesc;
		}


		public int InsertGroupTypeDesc(GroupTypeDesc groupTypeDesc) {
			return InsertGroupTypeDesc(groupTypeDesc, null);}

		private int InsertGroupTypeDesc(GroupTypeDesc groupTypeDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_group_type_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Group_type_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(groupTypeDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(groupTypeDesc.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					groupTypeDesc.GroupTypeId = DBValue.ToInt16(paramCol["@Group_type_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateGroupTypeDesc(GroupTypeDesc groupTypeDesc) {
			return UpdateGroupTypeDesc(groupTypeDesc, null);}

		private int UpdateGroupTypeDesc(GroupTypeDesc groupTypeDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_group_type_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Group_type_id", DbType.Int16, DBValue.ToDBInt16(groupTypeDesc.GroupTypeId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(groupTypeDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(groupTypeDesc.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region HearAboutUs Methods

		private HearAboutUs LoadHearAboutUs(DataRow row) {
			HearAboutUs hearAboutUs = new HearAboutUs();

			// Store database values into our business object
			hearAboutUs.HearAboutUsId = DBValue.ToInt16(row["hear_about_us_id"]);
			hearAboutUs.PartyTypeId = DBValue.ToInt16(row["party_type_id"]);
			hearAboutUs.Name = DBValue.ToString(row["name"]);
			hearAboutUs.OrderOnWeb = DBValue.ToInt16(row["order_on_web"]);
			hearAboutUs.IsActive = DBValue.ToInt16(row["is_active"]);

			// return the filled object
			return hearAboutUs;
		}

		public HearAboutUs[] GetHearAboutUss() {
			return GetHearAboutUss(null);}

		private HearAboutUs[] GetHearAboutUss(SqlInterface si) {
			HearAboutUs[] hearAboutUss = null;

			string storedProcName = "efrstore_get_hear_about_uss";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					hearAboutUss = new HearAboutUs[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							hearAboutUss[i] = LoadHearAboutUs(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return hearAboutUss;
		}


		public HearAboutUs GetHearAboutUsByID(int id) {
			return GetHearAboutUsByID(id, null);}

		private HearAboutUs GetHearAboutUsByID(int id, SqlInterface si) {
			HearAboutUs hearAboutUs = null;

			string storedProcName = "efrstore_get_hear_about_us_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Hear_about_us_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						hearAboutUs = LoadHearAboutUs(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return hearAboutUs;
		}


		public int InsertHearAboutUs(HearAboutUs hearAboutUs) {
			return InsertHearAboutUs(hearAboutUs, null);}

		private int InsertHearAboutUs(HearAboutUs hearAboutUs, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_hear_about_us";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Hear_about_us_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Party_type_id", DbType.Int16, DBValue.ToDBInt16(hearAboutUs.PartyTypeId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(hearAboutUs.Name)));
				paramCol.Add(new SqlDataParameter("@Order_on_web", DbType.Int16, DBValue.ToDBInt16(hearAboutUs.OrderOnWeb)));
				paramCol.Add(new SqlDataParameter("@Is_active", DbType.Int16, DBValue.ToDBInt16(hearAboutUs.IsActive)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					hearAboutUs.HearAboutUsId = DBValue.ToInt16(paramCol["@Hear_about_us_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateHearAboutUs(HearAboutUs hearAboutUs) {
			return UpdateHearAboutUs(hearAboutUs, null);}

		private int UpdateHearAboutUs(HearAboutUs hearAboutUs, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_hear_about_us";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Hear_about_us_id", DbType.Int16, DBValue.ToDBInt16(hearAboutUs.HearAboutUsId)));
				paramCol.Add(new SqlDataParameter("@Party_type_id", DbType.Int16, DBValue.ToDBInt16(hearAboutUs.PartyTypeId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(hearAboutUs.Name)));
				paramCol.Add(new SqlDataParameter("@Order_on_web", DbType.Int16, DBValue.ToDBInt16(hearAboutUs.OrderOnWeb)));
				paramCol.Add(new SqlDataParameter("@Is_active", DbType.Int16, DBValue.ToDBInt16(hearAboutUs.IsActive)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region HearAboutUsDesc Methods

		private HearAboutUsDesc LoadHearAboutUsDesc(DataRow row) {
			HearAboutUsDesc hearAboutUsDesc = new HearAboutUsDesc();

			// Store database values into our business object
			hearAboutUsDesc.HearAboutUsId = DBValue.ToInt16(row["hear_about_us_id"]);
			hearAboutUsDesc.CultureCode = DBValue.ToString(row["culture_code"]);
			hearAboutUsDesc.Description = DBValue.ToString(row["description"]);

			// return the filled object
			return hearAboutUsDesc;
		}

		public HearAboutUsDesc[] GetHearAboutUsDescs() {
			return GetHearAboutUsDescs(null);}

		private HearAboutUsDesc[] GetHearAboutUsDescs(SqlInterface si) {
			HearAboutUsDesc[] hearAboutUsDescs = null;

			string storedProcName = "efrstore_get_hear_about_us_descs";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					hearAboutUsDescs = new HearAboutUsDesc[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							hearAboutUsDescs[i] = LoadHearAboutUsDesc(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return hearAboutUsDescs;
		}

        //TODO Example
        public HearAboutUsDesc GetHearAboutUsDescByID(int id) {
			return GetHearAboutUsDescByID(id, null);}

		private HearAboutUsDesc GetHearAboutUsDescByID(int id, SqlInterface si) {
			HearAboutUsDesc hearAboutUsDesc = null;

			string storedProcName = "efrstore_get_hear_about_us_desc_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Hear_about_us_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						hearAboutUsDesc = LoadHearAboutUsDesc(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return hearAboutUsDesc;
		}


		public int InsertHearAboutUsDesc(HearAboutUsDesc hearAboutUsDesc) {
			return InsertHearAboutUsDesc(hearAboutUsDesc, null);}

		private int InsertHearAboutUsDesc(HearAboutUsDesc hearAboutUsDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_hear_about_us_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Hear_about_us_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(hearAboutUsDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(hearAboutUsDesc.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					hearAboutUsDesc.HearAboutUsId = DBValue.ToInt16(paramCol["@Hear_about_us_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateHearAboutUsDesc(HearAboutUsDesc hearAboutUsDesc) {
			return UpdateHearAboutUsDesc(hearAboutUsDesc, null);}

		private int UpdateHearAboutUsDesc(HearAboutUsDesc hearAboutUsDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_hear_about_us_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Hear_about_us_id", DbType.Int16, DBValue.ToDBInt16(hearAboutUsDesc.HearAboutUsId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(hearAboutUsDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(hearAboutUsDesc.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region Language Methods

		private Language LoadLanguage(DataRow row) {
			Language language = new Language();

			// Store database values into our business object
			language.LanguageCode = DBValue.ToString(row["language_code"]);
			language.Name = DBValue.ToString(row["name"]);

			// return the filled object
			return language;
		}

		public Language[] GetLanguages() {
			return GetLanguages(null);}

		private Language[] GetLanguages(SqlInterface si) {
			Language[] languages = null;

			string storedProcName = "efrstore_get_languages";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					languages = new Language[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							languages[i] = LoadLanguage(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return languages;
		}



		#endregion

		#region Newsletter Methods

		private Newsletter LoadNewsletter(DataRow row) {
			Newsletter newsletter = new Newsletter();

			// Store database values into our business object
			newsletter.NewsletterId = DBValue.ToInt32(row["newsletter_id"]);
			newsletter.CultureCode = DBValue.ToString(row["culture_code"]);
			newsletter.PartnerId = DBValue.ToInt32(row["partner_id"]);
			newsletter.NewsMonth = DBValue.ToString(row["news_month"]);
			newsletter.Url = DBValue.ToString(row["url"]);
			newsletter.DisplayOrder = DBValue.ToInt16(row["display_order"]);
			newsletter.Enabled = DBValue.ToInt16(row["enabled"]);

			// return the filled object
			return newsletter;
		}

		public Newsletter[] GetNewsletters() {
			return GetNewsletters(null);}

		private Newsletter[] GetNewsletters(SqlInterface si) {
			Newsletter[] newsletters = null;

			string storedProcName = "efrstore_get_newsletters";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					newsletters = new Newsletter[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							newsletters[i] = LoadNewsletter(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return newsletters;
		}


		public Newsletter GetNewsletterByID(int id) {
			return GetNewsletterByID(id, null);}

		private Newsletter GetNewsletterByID(int id, SqlInterface si) {
			Newsletter newsletter = null;

			string storedProcName = "efrstore_get_newsletter_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Newsletter_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						newsletter = LoadNewsletter(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return newsletter;
		}


		public int InsertNewsletter(Newsletter newsletter) {
			return InsertNewsletter(newsletter, null);}

		private int InsertNewsletter(Newsletter newsletter, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_newsletter";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Newsletter_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(newsletter.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(newsletter.PartnerId)));
				paramCol.Add(new SqlDataParameter("@News_month", DbType.String, DBValue.ToDBString(newsletter.NewsMonth)));
				paramCol.Add(new SqlDataParameter("@Url", DbType.String, DBValue.ToDBString(newsletter.Url)));
				paramCol.Add(new SqlDataParameter("@Display_order", DbType.Int16, DBValue.ToDBInt16(newsletter.DisplayOrder)));
				paramCol.Add(new SqlDataParameter("@Enabled", DbType.Int16, DBValue.ToDBInt16(newsletter.Enabled)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					newsletter.NewsletterId = DBValue.ToInt32(paramCol["@Newsletter_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateNewsletter(Newsletter newsletter) {
			return UpdateNewsletter(newsletter, null);}

		private int UpdateNewsletter(Newsletter newsletter, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_newsletter";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Newsletter_id", DbType.Int32, DBValue.ToDBInt32(newsletter.NewsletterId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(newsletter.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(newsletter.PartnerId)));
				paramCol.Add(new SqlDataParameter("@News_month", DbType.String, DBValue.ToDBString(newsletter.NewsMonth)));
				paramCol.Add(new SqlDataParameter("@Url", DbType.String, DBValue.ToDBString(newsletter.Url)));
				paramCol.Add(new SqlDataParameter("@Display_order", DbType.Int16, DBValue.ToDBInt16(newsletter.DisplayOrder)));
				paramCol.Add(new SqlDataParameter("@Enabled", DbType.Int16, DBValue.ToDBInt16(newsletter.Enabled)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;


		}


        public void InsertToTempTable(System.Data.DataTable dt)
        {

            System.Data.SqlClient.SqlBulkCopy bulkCopy = new System.Data.SqlClient.SqlBulkCopy(connectionString, System.Data.SqlClient.SqlBulkCopyOptions.TableLock);
            bulkCopy.BulkCopyTimeout = 120;

            bulkCopy.DestinationTableName = "Temp_Newsletter_Unsub";
            bulkCopy.WriteToServer(dt);

        }


        public DataTable RetrieveDataFromFRTables()
        {
            return RetrieveDataFromFRTables(null);        
        }

        public DataTable RetrieveDataFromFRTables(SqlInterface si)
        {
            string storedProcName = "efrcrm_get_fr_subscription_list";

            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }
            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }
                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);
                if (dt != null)
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return null;
        }


        public DataTable RetrieveDataFromFRMailListTables()
        {
            return RetrieveDataFromFRMailListTables(null);        
        }

        public DataTable RetrieveDataFromFRMailListTables(SqlInterface si)
        {
            string storedProcName = "fr_get_Customers";

            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }
            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);
                if (dt != null)
                {
                    return dt;
                }
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return null;
        }


        //TODO RetrieveDataFromEFRMailList
        public DataTable RetrieveDataFromEFRMailListTables(int partnerID, string beginDate, string endDate)
        {
            return RetrieveDataFromEFRMailListTables(partnerID, beginDate, endDate, null);
        }

        public DataTable RetrieveDataFromEFRMailListTables(int partnerID, string beginDate, string endDate, SqlInterface si)
        {
            string storedProcName = "efr_get_Customers";

            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }
            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(partnerID)));
                paramCol.Add(new SqlDataParameter("@date_from", DbType.String, DBValue.ToDBString(beginDate)));
                paramCol.Add(new SqlDataParameter("@date_to", DbType.String, DBValue.ToDBString(endDate)));


                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);
                if (dt != null)
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return null;
        
        
        
        }
        public DataTable RetrieveDataFromEFRTables()
        {

            return RetrieveDataFromEFRTables(null);
        
        }
         public DataTable RetrieveDataFromEFRTables(SqlInterface si)
         {

            string storedProcName = "efrcrm_get_efr_subscription_list";

            bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}
			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);
				if (dt != null) 
				{
					return dt;
				}
			}
            catch (Exception ex)
            {
                throw ex;

            }
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return null;
		}


        public int UpdateLeadNewsletterTables()
        {
            return UpdateLeadNewsletterTables(null);
        }

        private int UpdateLeadNewsletterTables(SqlInterface si)
        {
            int result = int.MinValue;

            string storedProcName = "efr_update_lead_news_unsubscribe_new";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                
                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

                if (result > 0)
                {
                    return result;
                }
                else
                {
                    throw new SqlDataException("Error updating database calling " + storedProcName);
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return result;
        }








		#endregion

		#region NewsletterSubscription Methods

		private NewsletterSubscription LoadNewsletterSubscription(DataRow row) {
			NewsletterSubscription newsletterSubscription = new NewsletterSubscription();

			// Store database values into our business object
			newsletterSubscription.SubscriptionId = DBValue.ToInt32(row["subscription_id"]);
			newsletterSubscription.PartnerId = DBValue.ToInt32(row["partner_id"]);
			newsletterSubscription.CultureCode = DBValue.ToString(row["culture_code"]);
			newsletterSubscription.Referrer = DBValue.ToString(row["referrer"]);
			newsletterSubscription.Email = DBValue.ToString(row["email"]);
			newsletterSubscription.Fullname = DBValue.ToString(row["fullname"]);
			newsletterSubscription.Unsubscribed = DBValue.ToInt16(row["unsubscribed"]);
			newsletterSubscription.SubscribeDate = DBValue.ToDateTime(row["subscribe_date"]);
			newsletterSubscription.UnsubscribeDate = DBValue.ToDateTime(row["unsubscribe_date"]);

			// return the filled object
			return newsletterSubscription;
		}

		public NewsletterSubscription[] GetNewsletterSubscriptions() {
			return GetNewsletterSubscriptions(null);}

		private NewsletterSubscription[] GetNewsletterSubscriptions(SqlInterface si) {
			NewsletterSubscription[] newsletterSubscriptions = null;

			string storedProcName = "efrstore_get_newsletter_subscriptions";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					newsletterSubscriptions = new NewsletterSubscription[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							newsletterSubscriptions[i] = LoadNewsletterSubscription(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return newsletterSubscriptions;
		}


		public NewsletterSubscription GetNewsletterSubscriptionByID(int id) {
			return GetNewsletterSubscriptionByID(id, null);}

		private NewsletterSubscription GetNewsletterSubscriptionByID(int id, SqlInterface si) {
			NewsletterSubscription newsletterSubscription = null;

			string storedProcName = "efrstore_get_newsletter_subscription_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Subscription_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						newsletterSubscription = LoadNewsletterSubscription(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return newsletterSubscription;
		}

		public NewsletterSubscriptionCollection GetNewsletterSubscriptionByEmailAndParnterId(string email, int partnerId) {
			return GetNewsletterSubscriptionByEmailAndParnterId(email, partnerId, null);}

		private NewsletterSubscriptionCollection GetNewsletterSubscriptionByEmailAndParnterId(string email, int partnerId, SqlInterface si) {
			NewsletterSubscriptionCollection newsletterSubscriptions = null;

			string storedProcName = "efrstore_get_newsletter_subscriptions_by_email_and_patner_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(partnerId)));
				paramCol.Add(new SqlDataParameter("@Email", DbType.String, DBValue.ToDBString(email)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					newsletterSubscriptions = new NewsletterSubscriptionCollection();

					for (int i = 0; i < dt.Rows.Count; i++) {
						// fill our objects
						try {
							newsletterSubscriptions.Add(LoadNewsletterSubscription(dt.Rows[i]));
						} 
						catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return newsletterSubscriptions;
		}

		public int InsertNewsletterSubscription(NewsletterSubscription newsletterSubscription) {
			return InsertNewsletterSubscription(newsletterSubscription, null);}

		private int InsertNewsletterSubscription(NewsletterSubscription newsletterSubscription, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_newsletter_subscription";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Subscription_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(newsletterSubscription.PartnerId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(newsletterSubscription.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Referrer", DbType.String, DBValue.ToDBString(newsletterSubscription.Referrer)));
				paramCol.Add(new SqlDataParameter("@Email", DbType.String, DBValue.ToDBString(newsletterSubscription.Email)));
				paramCol.Add(new SqlDataParameter("@Fullname", DbType.String, DBValue.ToDBString(newsletterSubscription.Fullname)));
				paramCol.Add(new SqlDataParameter("@Unsubscribed", DbType.Int16, DBValue.ToDBInt16(newsletterSubscription.Unsubscribed)));
				paramCol.Add(new SqlDataParameter("@Subscribe_date", DbType.DateTime, DBValue.ToDBDateTime(newsletterSubscription.SubscribeDate)));
				paramCol.Add(new SqlDataParameter("@Unsubscribe_date", DbType.DateTime, DBValue.ToDBDateTime(newsletterSubscription.UnsubscribeDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					newsletterSubscription.SubscriptionId = DBValue.ToInt32(paramCol["@Subscription_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateNewsletterSubscription(NewsletterSubscription newsletterSubscription) {
			return UpdateNewsletterSubscription(newsletterSubscription, null);}

		private int UpdateNewsletterSubscription(NewsletterSubscription newsletterSubscription, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_newsletter_subscription";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Subscription_id", DbType.Int32, DBValue.ToDBInt32(newsletterSubscription.SubscriptionId)));
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(newsletterSubscription.PartnerId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(newsletterSubscription.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Referrer", DbType.String, DBValue.ToDBString(newsletterSubscription.Referrer)));
				paramCol.Add(new SqlDataParameter("@Email", DbType.String, DBValue.ToDBString(newsletterSubscription.Email)));
				paramCol.Add(new SqlDataParameter("@Fullname", DbType.String, DBValue.ToDBString(newsletterSubscription.Fullname)));
				paramCol.Add(new SqlDataParameter("@Unsubscribed", DbType.Int16, DBValue.ToDBInt16(newsletterSubscription.Unsubscribed)));
				paramCol.Add(new SqlDataParameter("@Subscribe_date", DbType.DateTime, DBValue.ToDBDateTime(newsletterSubscription.SubscribeDate)));
				paramCol.Add(new SqlDataParameter("@Unsubscribe_date", DbType.DateTime, DBValue.ToDBDateTime(newsletterSubscription.UnsubscribeDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region OnlineUser Methods

		private OnlineUser LoadOnlineUser(DataRow row) {
			OnlineUser onlineUser = new OnlineUser();

			// Store database values into our business object
			onlineUser.OnlineUserId = DBValue.ToInt32(row["online_user_id"]);
			onlineUser.ClientSequenceCode = DBValue.ToString(row["client_sequence_code"]);
			onlineUser.ClientId = DBValue.ToInt32(row["client_id"]);
			onlineUser.Email = DBValue.ToString(row["email"]);
			onlineUser.OnlineUserPwd = DBValue.ToString(row["online_user_pwd"]);
			onlineUser.DateCreated = DBValue.ToDateTime(row["date_created"]);

			// return the filled object
			return onlineUser;
		}

		public OnlineUser[] GetOnlineUsers() {
			return GetOnlineUsers(null);}

		private OnlineUser[] GetOnlineUsers(SqlInterface si) {
			OnlineUser[] onlineUsers = null;

			string storedProcName = "efrstore_get_online_users";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					onlineUsers = new OnlineUser[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							onlineUsers[i] = LoadOnlineUser(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return onlineUsers;
		}


		public OnlineUser GetOnlineUserByID(int id) {
			return GetOnlineUserByID(id, null);}

		private OnlineUser GetOnlineUserByID(int id, SqlInterface si) {
			OnlineUser onlineUser = null;

			string storedProcName = "efrstore_get_online_user_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Online_user_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						onlineUser = LoadOnlineUser(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return onlineUser;
		}


		public int InsertOnlineUser(OnlineUser onlineUser) {
			return InsertOnlineUser(onlineUser, null);}

		private int InsertOnlineUser(OnlineUser onlineUser, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_online_user";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Online_user_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Client_sequence_code", DbType.String, DBValue.ToDBString(onlineUser.ClientSequenceCode)));
				paramCol.Add(new SqlDataParameter("@Client_id", DbType.Int32, DBValue.ToDBInt32(onlineUser.ClientId)));
				paramCol.Add(new SqlDataParameter("@Email", DbType.String, DBValue.ToDBString(onlineUser.Email)));
				paramCol.Add(new SqlDataParameter("@Online_user_pwd", DbType.String, DBValue.ToDBString(onlineUser.OnlineUserPwd)));
				paramCol.Add(new SqlDataParameter("@Date_created", DbType.DateTime, DBValue.ToDBDateTime(onlineUser.DateCreated)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					onlineUser.OnlineUserId = DBValue.ToInt32(paramCol["@Online_user_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateOnlineUser(OnlineUser onlineUser) {
			return UpdateOnlineUser(onlineUser, null);}

		private int UpdateOnlineUser(OnlineUser onlineUser, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_online_user";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Online_user_id", DbType.Int32, DBValue.ToDBInt32(onlineUser.OnlineUserId)));
				paramCol.Add(new SqlDataParameter("@Client_sequence_code", DbType.String, DBValue.ToDBString(onlineUser.ClientSequenceCode)));
				paramCol.Add(new SqlDataParameter("@Client_id", DbType.Int32, DBValue.ToDBInt32(onlineUser.ClientId)));
				paramCol.Add(new SqlDataParameter("@Email", DbType.String, DBValue.ToDBString(onlineUser.Email)));
				paramCol.Add(new SqlDataParameter("@Online_user_pwd", DbType.String, DBValue.ToDBString(onlineUser.OnlineUserPwd)));
				paramCol.Add(new SqlDataParameter("@Date_created", DbType.DateTime, DBValue.ToDBDateTime(onlineUser.DateCreated)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region Order Methods

		private Order LoadOrder(DataRow row) {
			Order order = new Order();

			// Store database values into our business object
			order.OrderId = DBValue.ToInt32(row["order_id"]);
			order.ShoppingCartId = DBValue.ToInt32(row["shopping_cart_id"]);
			order.OnlineUserId = DBValue.ToInt32(row["online_user_id"]);
			order.CreditCardId = DBValue.ToInt32(row["credit_card_id"]);
			order.CultureCode = DBValue.ToString(row["culture_code"]);
			order.RandomNumber = DBValue.ToInt32(row["random_number"]);
			//order.OrderNumber = DBValue.ToString(row["order_number"]);
			order.OrderTotal = DBValue.ToDouble(row["order_total"]);
			order.ShippingTotal = DBValue.ToDouble(row["shipping_total"]);
			order.TaxTotal = DBValue.ToDouble(row["tax_total"]);
			order.OrderSubmitted = DBValue.ToInt16(row["order_submitted"]);
			order.DateCreated = DBValue.ToDateTime(row["date_created"]);
			order.ScheduledDeliveryDate = DBValue.ToDateTime(row["scheduled_delivery_date"]);

			// return the filled object
			return order;
		}

		public Order[] GetOrders() {
			return GetOrders(null);}

		private Order[] GetOrders(SqlInterface si) {
			Order[] orders = null;

			string storedProcName = "efrstore_get_orders";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					orders = new Order[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							orders[i] = LoadOrder(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return orders;
		}


		public Order GetOrderByID(int id) {
			return GetOrderByID(id, null);}

		private Order GetOrderByID(int id, SqlInterface si) {
			Order order = null;

			string storedProcName = "efrstore_get_order_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Order_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						order = LoadOrder(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return order;
		}


		public int InsertOrder(Order order) {
			return InsertOrder(order, null);}

		private int InsertOrder(Order order, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_order";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Order_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Shopping_cart_id", DbType.Int32, DBValue.ToDBInt32(order.ShoppingCartId)));
				paramCol.Add(new SqlDataParameter("@Online_user_id", DbType.Int32, DBValue.ToDBInt32(order.OnlineUserId)));
				paramCol.Add(new SqlDataParameter("@Credit_card_id", DbType.Int32, DBValue.ToDBInt32(order.CreditCardId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(order.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Random_number", DbType.Int32, DBValue.ToDBInt32(order.RandomNumber)));
				paramCol.Add(new SqlDataParameter("@Order_number", DbType.String, DBValue.ToDBString(order.OrderNumber)));
				paramCol.Add(new SqlDataParameter("@Order_total", DbType.Double, DBValue.ToDBDouble(order.OrderTotal)));
				paramCol.Add(new SqlDataParameter("@Shipping_total", DbType.Double, DBValue.ToDBDouble(order.ShippingTotal)));
				paramCol.Add(new SqlDataParameter("@Tax_total", DbType.Double, DBValue.ToDBDouble(order.TaxTotal)));
				paramCol.Add(new SqlDataParameter("@Order_submitted", DbType.Int16, DBValue.ToDBInt16(order.OrderSubmitted)));
				paramCol.Add(new SqlDataParameter("@Date_created", DbType.DateTime, DBValue.ToDBDateTime(order.DateCreated)));
				paramCol.Add(new SqlDataParameter("@Scheduled_delivery_date", DbType.DateTime, DBValue.ToDBDateTime(order.ScheduledDeliveryDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					order.OrderId = DBValue.ToInt32(paramCol["@Order_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateOrder(Order order) {
			return UpdateOrder(order, null);}

		private int UpdateOrder(Order order, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_order";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Order_id", DbType.Int32, DBValue.ToDBInt32(order.OrderId)));
				paramCol.Add(new SqlDataParameter("@Shopping_cart_id", DbType.Int32, DBValue.ToDBInt32(order.ShoppingCartId)));
				paramCol.Add(new SqlDataParameter("@Online_user_id", DbType.Int32, DBValue.ToDBInt32(order.OnlineUserId)));
				paramCol.Add(new SqlDataParameter("@Credit_card_id", DbType.Int32, DBValue.ToDBInt32(order.CreditCardId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(order.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Random_number", DbType.Int32, DBValue.ToDBInt32(order.RandomNumber)));
				paramCol.Add(new SqlDataParameter("@Order_number", DbType.String, DBValue.ToDBString(order.OrderNumber)));
				paramCol.Add(new SqlDataParameter("@Order_total", DbType.Double, DBValue.ToDBDouble(order.OrderTotal)));
				paramCol.Add(new SqlDataParameter("@Shipping_total", DbType.Double, DBValue.ToDBDouble(order.ShippingTotal)));
				paramCol.Add(new SqlDataParameter("@Tax_total", DbType.Double, DBValue.ToDBDouble(order.TaxTotal)));
				paramCol.Add(new SqlDataParameter("@Order_submitted", DbType.Int16, DBValue.ToDBInt16(order.OrderSubmitted)));
				paramCol.Add(new SqlDataParameter("@Date_created", DbType.DateTime, DBValue.ToDBDateTime(order.DateCreated)));
				paramCol.Add(new SqlDataParameter("@Scheduled_delivery_date", DbType.DateTime, DBValue.ToDBDateTime(order.ScheduledDeliveryDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region OrderCoupon Methods

		private OrderCoupon LoadOrderCoupon(DataRow row) {
			OrderCoupon orderCoupon = new OrderCoupon();

			// Store database values into our business object
			orderCoupon.OrderId = DBValue.ToInt32(row["order_id"]);
			orderCoupon.CouponId = DBValue.ToInt32(row["coupon_id"]);

			// return the filled object
			return orderCoupon;
		}

		public OrderCoupon[] GetOrderCoupons() {
			return GetOrderCoupons(null);}

		private OrderCoupon[] GetOrderCoupons(SqlInterface si) {
			OrderCoupon[] orderCoupons = null;

			string storedProcName = "efrstore_get_order_coupons";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					orderCoupons = new OrderCoupon[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							orderCoupons[i] = LoadOrderCoupon(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return orderCoupons;
		}


		public OrderCoupon GetOrderCouponByID(int id) {
			return GetOrderCouponByID(id, null);}

		private OrderCoupon GetOrderCouponByID(int id, SqlInterface si) {
			OrderCoupon orderCoupon = null;

			string storedProcName = "efrstore_get_order_coupon_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Order_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						orderCoupon = LoadOrderCoupon(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return orderCoupon;
		}


		public int InsertOrderCoupon(OrderCoupon orderCoupon) {
			return InsertOrderCoupon(orderCoupon, null);}

		private int InsertOrderCoupon(OrderCoupon orderCoupon, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_order_coupon";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Order_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Coupon_id", DbType.Int32, DBValue.ToDBInt32(orderCoupon.CouponId)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					orderCoupon.OrderId = DBValue.ToInt32(paramCol["@Order_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateOrderCoupon(OrderCoupon orderCoupon) {
			return UpdateOrderCoupon(orderCoupon, null);}

		private int UpdateOrderCoupon(OrderCoupon orderCoupon, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_order_coupon";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Order_id", DbType.Int32, DBValue.ToDBInt32(orderCoupon.OrderId)));
				paramCol.Add(new SqlDataParameter("@Coupon_id", DbType.Int32, DBValue.ToDBInt32(orderCoupon.CouponId)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region OrderSale Methods

		private OrderSale LoadOrderSale(DataRow row) {
			OrderSale orderSale = new OrderSale();

			// Store database values into our business object
			orderSale.OrderId = DBValue.ToInt32(row["order_id"]);
			orderSale.SaleId = DBValue.ToInt32(row["sale_id"]);
			orderSale.DateCreated = DBValue.ToDateTime(row["date_created"]);

			// return the filled object
			return orderSale;
		}

		public OrderSale[] GetOrderSales() {
			return GetOrderSales(null);}

		private OrderSale[] GetOrderSales(SqlInterface si) {
			OrderSale[] orderSales = null;

			string storedProcName = "efrstore_get_order_sales";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					orderSales = new OrderSale[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							orderSales[i] = LoadOrderSale(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return orderSales;
		}


		public OrderSale GetOrderSaleByID(int id) {
			return GetOrderSaleByID(id, null);}

		private OrderSale GetOrderSaleByID(int id, SqlInterface si) {
			OrderSale orderSale = null;

			string storedProcName = "efrstore_get_order_sale_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Order_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						orderSale = LoadOrderSale(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return orderSale;
		}


		public int InsertOrderSale(OrderSale orderSale) {
			return InsertOrderSale(orderSale, null);}

		private int InsertOrderSale(OrderSale orderSale, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_order_sale";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Order_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Sale_id", DbType.Int32, DBValue.ToDBInt32(orderSale.SaleId)));
				paramCol.Add(new SqlDataParameter("@Date_created", DbType.DateTime, DBValue.ToDBDateTime(orderSale.DateCreated)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					orderSale.OrderId = DBValue.ToInt32(paramCol["@Order_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateOrderSale(OrderSale orderSale) {
			return UpdateOrderSale(orderSale, null);}

		private int UpdateOrderSale(OrderSale orderSale, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_order_sale";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Order_id", DbType.Int32, DBValue.ToDBInt32(orderSale.OrderId)));
				paramCol.Add(new SqlDataParameter("@Sale_id", DbType.Int32, DBValue.ToDBInt32(orderSale.SaleId)));
				paramCol.Add(new SqlDataParameter("@Date_created", DbType.DateTime, DBValue.ToDBDateTime(orderSale.DateCreated)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region OrganizationType Methods

		private OrganizationType LoadOrganizationType(DataRow row) {
			OrganizationType organizationType = new OrganizationType();

			// Store database values into our business object
			organizationType.OrganizationTypeId = DBValue.ToInt16(row["organization_type_id"]);
			organizationType.PartyTypeId = DBValue.ToInt16(row["party_type_id"]);
			organizationType.Description = DBValue.ToString(row["description"]);

			// return the filled object
			return organizationType;
		}

		public OrganizationType[] GetOrganizationTypes() {
			return GetOrganizationTypes(null);}

		private OrganizationType[] GetOrganizationTypes(SqlInterface si) {
			OrganizationType[] organizationTypes = null;

			string storedProcName = "efrstore_get_organization_types";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					organizationTypes = new OrganizationType[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							organizationTypes[i] = LoadOrganizationType(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return organizationTypes;
		}


		public OrganizationType GetOrganizationTypeByID(int id) {
			return GetOrganizationTypeByID(id, null);}

		private OrganizationType GetOrganizationTypeByID(int id, SqlInterface si) {
			OrganizationType organizationType = null;

			string storedProcName = "efrstore_get_organization_type_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Organization_type_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						organizationType = LoadOrganizationType(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return organizationType;
		}


		public int InsertOrganizationType(OrganizationType organizationType) {
			return InsertOrganizationType(organizationType, null);}

		private int InsertOrganizationType(OrganizationType organizationType, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_organization_type";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Organization_type_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Party_type_id", DbType.Int16, DBValue.ToDBInt16(organizationType.PartyTypeId)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(organizationType.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					organizationType.OrganizationTypeId = DBValue.ToInt16(paramCol["@Organization_type_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateOrganizationType(OrganizationType organizationType) {
			return UpdateOrganizationType(organizationType, null);}

		private int UpdateOrganizationType(OrganizationType organizationType, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_organization_type";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Organization_type_id", DbType.Int16, DBValue.ToDBInt16(organizationType.OrganizationTypeId)));
				paramCol.Add(new SqlDataParameter("@Party_type_id", DbType.Int16, DBValue.ToDBInt16(organizationType.PartyTypeId)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(organizationType.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region OrganizationTypeDesc Methods

		private OrganizationTypeDesc LoadOrganizationTypeDesc(DataRow row) {
			OrganizationTypeDesc organizationTypeDesc = new OrganizationTypeDesc();

			// Store database values into our business object
			organizationTypeDesc.OrganizationTypeId = DBValue.ToInt16(row["organization_type_id"]);
			organizationTypeDesc.CultureCode = DBValue.ToString(row["culture_code"]);
			organizationTypeDesc.Description = DBValue.ToString(row["description"]);

			// return the filled object
			return organizationTypeDesc;
		}

		public OrganizationTypeDesc[] GetOrganizationTypeDescs() {
			return GetOrganizationTypeDescs(null);}

		private OrganizationTypeDesc[] GetOrganizationTypeDescs(SqlInterface si) {
			OrganizationTypeDesc[] organizationTypeDescs = null;

			string storedProcName = "efrstore_get_organization_type_descs";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					organizationTypeDescs = new OrganizationTypeDesc[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							organizationTypeDescs[i] = LoadOrganizationTypeDesc(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return organizationTypeDescs;
		}


		public OrganizationTypeDesc GetOrganizationTypeDescByID(int id) {
			return GetOrganizationTypeDescByID(id, null);}

		private OrganizationTypeDesc GetOrganizationTypeDescByID(int id, SqlInterface si) {
			OrganizationTypeDesc organizationTypeDesc = null;

			string storedProcName = "efrstore_get_organization_type_desc_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Organization_type_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						organizationTypeDesc = LoadOrganizationTypeDesc(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return organizationTypeDesc;
		}


		public int InsertOrganizationTypeDesc(OrganizationTypeDesc organizationTypeDesc) {
			return InsertOrganizationTypeDesc(organizationTypeDesc, null);}

		private int InsertOrganizationTypeDesc(OrganizationTypeDesc organizationTypeDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_organization_type_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Organization_type_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(organizationTypeDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(organizationTypeDesc.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					organizationTypeDesc.OrganizationTypeId = DBValue.ToInt16(paramCol["@Organization_type_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateOrganizationTypeDesc(OrganizationTypeDesc organizationTypeDesc) {
			return UpdateOrganizationTypeDesc(organizationTypeDesc, null);}

		private int UpdateOrganizationTypeDesc(OrganizationTypeDesc organizationTypeDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_organization_type_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Organization_type_id", DbType.Int16, DBValue.ToDBInt16(organizationTypeDesc.OrganizationTypeId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(organizationTypeDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(organizationTypeDesc.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

        #region PackageInterest

        private packageInterest LoadPackageInterest(DataRow row)
        {
            packageInterest package_int = new packageInterest();

            // Store database values into our business object
            package_int.PackageId = DBValue.ToInt16(row["package_id"]);
            package_int.Package_interest_id = DBValue.ToInt16(row["Package_interest_id"]);
            

            // return the filled object
            return package_int;
        }

        public List<packageInterest> GetPackageInterestByPackageID(int id)
        {
            SqlInterface si =null;
            List<packageInterest> list = null;

            string storedProcName = "efrstore_get_package_interest";
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int32, DBValue.ToDBInt32(id)));


                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    list = new List<packageInterest>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            packageInterest packageInte = LoadPackageInterest(dt.Rows[i]);


                            list.Add(packageInte);
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            

            return list;
        }


        #endregion

        #region Package Methods

        private Package LoadPackage(DataRow row) {
			Package package = new Package();

			// Store database values into our business object
			package.PackageId = DBValue.ToInt16(row["package_id"]);
			package.ParentPackageId = DBValue.ToInt16(row["parent_package_id"]);
			package.Name = DBValue.ToString(row["name"]);
			package.ProfitPercentage = DBValue.ToInt16(row["profit_percentage"]);
			package.Enabled = DBValue.ToInt16(row["enabled"]);
			package.CreateDate = DBValue.ToDateTime(row["create_date"]);

			// return the filled object
			return package;
		}

		public Package[] GetPackages() {
			return GetPackages(null);}

		private Package[] GetPackages(SqlInterface si) {
			Package[] packages = null;

			string storedProcName = "efrstore_get_packages";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					packages = new Package[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							packages[i] = LoadPackage(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return packages;
		}

		public DataTable GetPackagesInDataTable() 
		{
			return GetPackagesInDataTable(null);
		}

		private DataTable GetPackagesInDataTable(SqlInterface si) 
		{
			

			string storedProcName = "efrstore_get_packages";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) 
				{
					return dt;
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return null;
		}

		public PackageCollection GetPackagesRoot(int packageRootID) 
		{
			return GetPackagesRoot(packageRootID,null);}

		private PackageCollection GetPackagesRoot(int packageRootID, SqlInterface si) {
			PackageCollection packages = null;

			string storedProcName = "efrstore_get_packages_root";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					packages = new PackageCollection();

					for (int i = 0; i < dt.Rows.Count; i++) {
						// fill our objects
						try {

							Package package = LoadPackage(dt.Rows[i]);
							if (package.PackageId == packageRootID || packageRootID == int.MinValue)
							{
								//packages = GetPackagesByParentPackageID(package.PackageId);
								package.PackageDescription = GetPackageDescByID(package.PackageId);
								package.LoadChildrenPackages();
								package.LoadProducts();
								packages.Add(package);
								

							}
						} 
						catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return packages;
		}

        
        
        
        
        public PackageCollection GetPackagesByParentPackageID(int id) {
			return GetPackagesByParentPackageID(id, null);}

		private PackageCollection GetPackagesByParentPackageID(int id, SqlInterface si) {
			PackageCollection packages = null;

			string storedProcName = "efrstore_get_packages_by_parent_package_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Parent_package_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					packages = new PackageCollection();

					for (int i = 0; i < dt.Rows.Count; i++) {
						// fill our objects
						try {
							Package package = LoadPackage(dt.Rows[i]);

							package.PackageDescription = GetPackageDescByID(package.PackageId);
							package.LoadProducts();
							packages.Add(package);
						} 
						catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return packages;
		}

		public PackageCollection GetPackagesByName(string name) 
		{
			return GetPackagesByName(name, null);}

		private PackageCollection GetPackagesByName(string name, SqlInterface si) 
		{
			PackageCollection packages = null;

			string storedProcName = "efrstore_get_packages_by_name";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@name", DbType.String, DBValue.ToString(name)));
		
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) 
				{
					packages = new PackageCollection();

					for (int i = 0; i < dt.Rows.Count; i++) 
					{
						// fill our objects
						try 
						{
							Package package = LoadPackage(dt.Rows[i]);
							package.PackageDescription = GetPackageDescByID(package.PackageId);
							package.LoadProducts();
							packages.Add(package);
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return packages;
		}

		public Package GetPackageByID(int id) {
			return GetPackageByID(id, null);}

		public Package GetPackageByTopParentPackageIDAndPageName(int topParentPackageId, string pageName)
		{
			return GetPackageByTopParentPackageIDAndPageName(topParentPackageId, pageName, null);
		}


		private Package GetPackageByTopParentPackageIDAndPageName(int topParentPackageId, string pageName, SqlInterface si) 
		{
			Package package = null;

			string storedProcName = "efrstore_get_package_by_top_parent_package_id_and_page_name";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Top_Parent_Package_id", DbType.Int32, DBValue.ToDBInt32(topParentPackageId)));
				paramCol.Add(new SqlDataParameter("@Page_Name", DbType.String, DBValue.ToDBString(pageName)));

				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) 
				{
					// fill our objects
					try 
					{
						package = LoadPackage(dt.Rows[0]);
						package.PackageDescription = GetPackageDescByID(package.PackageId);

					} 
					catch(Exception ex) 
					{
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}

			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return package;
		}



		private Package GetPackageByID(int id, SqlInterface si) 
		{
			Package package = null;

			string storedProcName = "efrstore_get_package_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						package = LoadPackage(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return package;
		}


		public int GetPackageRootIDByID(int id) 
		{
			return GetPackageRootIDByID(id, null);}

		private int GetPackageRootIDByID(int id, SqlInterface si) 
		{
			int packageID = 0;

			string storedProcName = "efrstore_get_package_parent_by_package_id";


			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{

				SqlDataParameterCollection paramCol;

				bool parentFound = true;
				while (parentFound)
				{

					// declare stored procedure parameters
					paramCol = new SqlDataParameterCollection();
					paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
					if (newConnection) 
					{
						// open the connection
						si.Open();
					}

					DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

					if(dt != null && dt.Rows.Count > 0) 
					{
						// fill our objects
						try 
						{
							id = DBValue.ToInt32(dt.Rows[0]["parent_package_id"]);
							if (id == 0 || id == int.MinValue)
							{
								parentFound = false;
							}
							else
							{
								packageID = id;
							}

							
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
					else
					{
						parentFound = false;
					}
					if(newConnection) 
					{
						// Always close connection.
						si.Close();
					}
					
				}

			}
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
		
			return packageID;
		}


		public Package GetPackageRootByID(int id) 
		{
			return GetPackageRootByID(id, null);}

		private Package GetPackageRootByID(int id, SqlInterface si) 
		{
			Package package = null;

			string storedProcName = "efrstore_get_package_parent_by_package_id";


			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{

				SqlDataParameterCollection paramCol;

				bool parentFound = true;
				while (parentFound)
				{

					// declare stored procedure parameters
					paramCol = new SqlDataParameterCollection();
					paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
					if (newConnection) 
					{
						// open the connection
						si.Open();
					}

					DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

					if(dt != null && dt.Rows.Count > 0) 
					{
						// fill our objects
						try 
						{
							id = DBValue.ToInt32(dt.Rows[0]["parent_package_id"]);
							if (id == 0 || id == int.MinValue)
							{
								parentFound = false;
							}
							else
							{
								package = LoadPackage(dt.Rows[0]);
							}

							
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
					else
					{
						parentFound = false;
					}
					if(newConnection) 
					{
						// Always close connection.
						si.Close();
					}
					
				}

			}
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
		
			return package;
		}
		

		public Package GetPackageByProductID(int id)
		{
			return GetPackageByProductID(id,null);
		}
		private Package GetPackageByProductID(int id, SqlInterface si)
		{
			Package package = null;

			string storedProcName = "efrstore_get_package_by_product_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) 
				{
					// fill our objects
					try 
					{
						package = LoadPackage(dt.Rows[0]);
					} 
					catch(Exception ex) 
					{
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return package;
		}

		public int InsertPackage(Package package) {
			return InsertPackage(package, null);}

		private int InsertPackage(Package package, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_package";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Parent_package_id", DbType.Int16, DBValue.ToDBInt16(package.ParentPackageId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(package.Name)));
				paramCol.Add(new SqlDataParameter("@Profit_percentage", DbType.Int16, DBValue.ToDBInt16(package.ProfitPercentage)));
				paramCol.Add(new SqlDataParameter("@Enabled", DbType.Int16, DBValue.ToDBInt16(package.Enabled)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(package.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					package.PackageId = DBValue.ToInt16(paramCol["@Package_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdatePackage(Package package) {
			return UpdatePackage(package, null);}

		private int UpdatePackage(Package package, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_package";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int16, DBValue.ToDBInt16(package.PackageId)));
				paramCol.Add(new SqlDataParameter("@Parent_package_id", DbType.Int16, DBValue.ToDBInt16(package.ParentPackageId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(package.Name)));
				paramCol.Add(new SqlDataParameter("@Profit_percentage", DbType.Int16, DBValue.ToDBInt16(package.ProfitPercentage)));
				paramCol.Add(new SqlDataParameter("@Enabled", DbType.Int16, DBValue.ToDBInt16(package.Enabled)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(package.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region PackageDesc Methods

		private PackageDesc LoadPackageDesc(DataRow row) {
			PackageDesc packageDesc = new PackageDesc();

			// Store database values into our business object
			packageDesc.PackageId = DBValue.ToInt32(row["package_id"]);
			packageDesc.CultureCode = DBValue.ToString(row["culture_code"]);
			packageDesc.TemplateId = DBValue.ToInt32(row["template_id"]);
			packageDesc.Name = DBValue.ToString(row["name"]);
			packageDesc.ShortDesc = DBValue.ToString(row["short_desc"]);
			packageDesc.LongDesc = DBValue.ToString(row["long_desc"]);
			packageDesc.ExtraDesc = DBValue.ToString(row["extra_desc"]);
			packageDesc.PageName = DBValue.ToString(row["page_name"]);
			packageDesc.PageTitle = DBValue.ToString(row["page_title"]);
			packageDesc.ImageName = DBValue.ToString(row["image_name"]);
			packageDesc.ImageAltText = DBValue.ToString(row["image_alt_text"]);
			packageDesc.DisplayOrder = DBValue.ToInt32(row["display_order"]);
			packageDesc.Enabled = DBValue.ToInt16(row["enabled"]);
			packageDesc.Configuration = DBValue.ToString(row["configuration"]);
			packageDesc.CreateDate = DBValue.ToDateTime(row["create_date"]);

			// return the filled object
			return packageDesc;
		}

		public PackageDesc[] GetPackageDescs() {
			return GetPackageDescs(null);}

		private PackageDesc[] GetPackageDescs(SqlInterface si) {
			PackageDesc[] packageDescs = null;

			string storedProcName = "efrstore_get_package_descs";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					packageDescs = new PackageDesc[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							packageDescs[i] = LoadPackageDesc(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return packageDescs;
		}

		public PackageDescCollection GetPackageDescsByPackageID(short packageID) 
		{
			return GetPackageDescsByPackageID(packageID, null);}

		private PackageDescCollection GetPackageDescsByPackageID(short packageID, SqlInterface si) 
		{
			PackageDescCollection packageDescs = null;

			string storedProcName = "efrstore_get_package_descs_by_package_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int16,  DBValue.ToDBInt16(packageID)));
		
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);
			
				if (dt != null) 
				{
					packageDescs = new PackageDescCollection();
					for (int i = 0; i < dt.Rows.Count; i++)	
					{
						// fill our objects
						try 
						{
							packageDescs.Add(LoadPackageDesc(dt.Rows[i]));
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return packageDescs;
		}
		public PackageDescCollection GetChildPackageDescByPackageName(string packageName)
		{
			return GetChildPackageDescByPackageName(packageName, null);
		}
		private PackageDescCollection GetChildPackageDescByPackageName(string packageName, SqlInterface si)
		{
			PackageDescCollection packageDescs = null;

			string storedProcName = "efrstore_get_child_packages_by_package_name";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Package_name", DbType.String,  DBValue.ToDBString(packageName)));
		
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);
			
				if (dt != null) 
				{
					packageDescs = new PackageDescCollection();
					for (int i = 0; i < dt.Rows.Count; i++)	
					{
						// fill our objects
						try 
						{
							packageDescs.Add(LoadPackageDesc(dt.Rows[i]));
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return packageDescs;
		}
		public PackageDescCollection GetPackageDescsByPackageIDAndPageName(int packageID, string pageName) 
		{
			return GetPackageDescsByPackageIDAndPageName(packageID, pageName, null);}

		private PackageDescCollection GetPackageDescsByPackageIDAndPageName(int packageID, string pageName, SqlInterface si) 
		{
			PackageDescCollection packageDescs = null;

			string storedProcName = "efrstore_get_package_descs_by_package_id_and_page_name";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int16,  DBValue.ToDBInt16(packageID)));
				paramCol.Add(new SqlDataParameter("@Page_Name", DbType.String,  DBValue.ToDBString(pageName)));
		
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);
			
				if (dt != null) 
				{
					packageDescs = new PackageDescCollection();
					for (int i = 0; i < dt.Rows.Count; i++)	
					{
						// fill our objects
						try 
						{
							packageDescs.Add(LoadPackageDesc(dt.Rows[i]));
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return packageDescs;
		}

		public PackageDescCollection GetPackageDescsByPageName(string pageName) 
		{
			return GetPackageDescsByPageName(pageName, null);}

		private PackageDescCollection GetPackageDescsByPageName(string pageName, SqlInterface si) 
		{
			PackageDescCollection packageDescs = null;

			string storedProcName = "efrstore_get_package_descs_by_page_name";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Page_Name", DbType.String,  DBValue.ToDBString(pageName)));
		
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);
			
				if (dt != null) 
				{
					packageDescs = new PackageDescCollection();
					for (int i = 0; i < dt.Rows.Count; i++)	
					{
						// fill our objects
						try 
						{
							packageDescs.Add(LoadPackageDesc(dt.Rows[i]));
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return packageDescs;
		}


		//private PackageDesc GetPackageDescByPageName(
		public PackageDesc GetPackageDescByID(int id) 
		{
			return GetPackageDescByID(id, null);}

		private PackageDesc GetPackageDescByID(int id, SqlInterface si) {
			PackageDesc packageDesc = null;

			string storedProcName = "efrstore_get_package_desc_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						packageDesc = LoadPackageDesc(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return packageDesc;
		}

		
		public PackageDescCollection GetPackageDescsByImageName(string imageName) 
		{
			return GetPackageDescsByImageName(imageName, null);}

		private PackageDescCollection GetPackageDescsByImageName(string imageName, SqlInterface si) 
		{
			PackageDescCollection packageDescs = null;

			string storedProcName = "efrstore_get_package_descs_by_image_name";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Image_Name", DbType.String, DBValue.ToDBString(imageName)));
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) 
				{
					packageDescs = new PackageDescCollection();
					for (int i = 0; i < dt.Rows.Count; i++)	
					{
						// fill our objects
						try 
						{
							packageDescs.Add(LoadPackageDesc(dt.Rows[0]));
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return packageDescs;
		}
		public PackageDesc GetPackageDescByPageNameAndTemplateExists(string pageName)
		{
			return GetPackageDescByPageNameAndTemplateExists(pageName, null);
		}
		private PackageDesc GetPackageDescByPageNameAndTemplateExists( string pageName, SqlInterface si)
		{
			PackageDesc packageDesc = null;

			string storedProcName = "efrstore_get_package_descs_by_page_name_and_template_exist";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@page_name ", DbType.String, DBValue.ToString(pageName)));
				if (newConnection)	
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) 
				{
					// fill our objects
					try 
					{
						packageDesc = LoadPackageDesc(dt.Rows[0]);
					} 
					catch(Exception ex) 
					{
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return packageDesc;
		}

		
		public PackageDesc GetPackageDescByPageNameAndPackageRootID(string pageName, int rootID)
		{
			return GetPackageDescByPageNameAndPackageRootID(pageName, rootID, null);
		}
		private PackageDesc GetPackageDescByPageNameAndPackageRootID( string pageName, int rootID, SqlInterface si)
		{
			PackageDesc packageDesc = null;

			string storedProcName = "efrstore_get_package_descs_by_page_name_and_root_package_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@page_name ", DbType.String, DBValue.ToString(pageName)));
				paramCol.Add(new SqlDataParameter("@root_package_id", DbType.Int32, DBValue.ToDBInt32(rootID)));
				if (newConnection)	
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) 
				{
					// fill our objects
					try 
					{
						packageDesc = LoadPackageDesc(dt.Rows[0]);
					} 
					catch(Exception ex) 
					{
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return packageDesc;
		}

		public PackageDesc GetPackageDescByPackageIDAndCultureCode(int id, string cultureCode) 
		{
			return GetPackageDescByPackageIDAndCultureCode(id, cultureCode, null);}

		private PackageDesc GetPackageDescByPackageIDAndCultureCode(int id, string cultureCode, SqlInterface si) 
		{
			PackageDesc packageDesc = null;

			string storedProcName = "efrstore_get_package_desc_by_package_id_and_culture_code";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int32, DBValue.ToDBInt32(id)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(cultureCode)));
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) 
				{
					// fill our objects
					try 
					{
						packageDesc = LoadPackageDesc(dt.Rows[0]);
					} 
					catch(Exception ex) 
					{
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return packageDesc;
		}

		public int InsertPackageDesc(PackageDesc packageDesc) {
			return InsertPackageDesc(packageDesc, null);}

		private int InsertPackageDesc(PackageDesc packageDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_package_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int16, DBValue.ToDBInt16(packageDesc.PackageId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(packageDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Template_id", DbType.Int32, DBValue.ToDBInt32(packageDesc.TemplateId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(packageDesc.Name)));
				paramCol.Add(new SqlDataParameter("@Short_desc", DbType.String, DBValue.ToDBString(packageDesc.ShortDesc)));
				paramCol.Add(new SqlDataParameter("@Long_desc", DbType.String, DBValue.ToDBString(packageDesc.LongDesc)));
				paramCol.Add(new SqlDataParameter("@Extra_desc", DbType.String, DBValue.ToDBString(packageDesc.ExtraDesc)));
				paramCol.Add(new SqlDataParameter("@Page_name", DbType.String, DBValue.ToDBString(packageDesc.PageName)));
				paramCol.Add(new SqlDataParameter("@Page_title", DbType.String, DBValue.ToDBString(packageDesc.PageTitle)));
				paramCol.Add(new SqlDataParameter("@Image_name", DbType.String, DBValue.ToDBString(packageDesc.ImageName)));
				paramCol.Add(new SqlDataParameter("@Image_alt_text", DbType.String, DBValue.ToDBString(packageDesc.ImageAltText)));
				paramCol.Add(new SqlDataParameter("@Display_order", DbType.Int32, DBValue.ToDBInt32(packageDesc.DisplayOrder)));
				paramCol.Add(new SqlDataParameter("@Enabled", DbType.Int16, DBValue.ToDBInt16(packageDesc.Enabled)));
				paramCol.Add(new SqlDataParameter("@Configuration", DbType.String, DBValue.ToDBString(packageDesc.Configuration)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(packageDesc.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					packageDesc.PackageId = DBValue.ToInt32(paramCol["@Package_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdatePackageDesc(PackageDesc packageDesc) {
			return UpdatePackageDesc(packageDesc, null);}

		private int UpdatePackageDesc(PackageDesc packageDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_package_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int32, DBValue.ToDBInt32(packageDesc.PackageId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(packageDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Template_id", DbType.Int32, DBValue.ToDBInt32(packageDesc.TemplateId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(packageDesc.Name)));
				paramCol.Add(new SqlDataParameter("@Short_desc", DbType.String, DBValue.ToDBString(packageDesc.ShortDesc)));
				paramCol.Add(new SqlDataParameter("@Long_desc", DbType.String, DBValue.ToDBString(packageDesc.LongDesc)));
				paramCol.Add(new SqlDataParameter("@Extra_desc", DbType.String, DBValue.ToDBString(packageDesc.ExtraDesc)));
				paramCol.Add(new SqlDataParameter("@Page_name", DbType.String, DBValue.ToDBString(packageDesc.PageName)));
				paramCol.Add(new SqlDataParameter("@Page_title", DbType.String, DBValue.ToDBString(packageDesc.PageTitle)));
				paramCol.Add(new SqlDataParameter("@Image_name", DbType.String, DBValue.ToDBString(packageDesc.ImageName)));
				paramCol.Add(new SqlDataParameter("@Image_alt_text", DbType.String, DBValue.ToDBString(packageDesc.ImageAltText)));
				paramCol.Add(new SqlDataParameter("@Display_order", DbType.Int32, DBValue.ToDBInt32(packageDesc.DisplayOrder)));
				paramCol.Add(new SqlDataParameter("@Enabled", DbType.Int16, DBValue.ToDBInt16(packageDesc.Enabled)));
				paramCol.Add(new SqlDataParameter("@Configuration", DbType.String, DBValue.ToDBString(packageDesc.Configuration)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(packageDesc.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region Program Methods

		private Program LoadProgram(DataRow row) {
			Program program = new Program();

			// Store database values into our business object
			program.ProgramId = DBValue.ToInt32(row["program_id"]);
			program.Name = DBValue.ToString(row["name"]);
			program.CreateDate = DBValue.ToDateTime(row["create_date"]);

			// return the filled object
			return program;
		}

		public Program[] GetPrograms() {
			return GetPrograms(null);}

		private Program[] GetPrograms(SqlInterface si) {
			Program[] programs = null;

			string storedProcName = "efrstore_get_programs";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					programs = new Program[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							programs[i] = LoadProgram(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return programs;
		}


		public Program GetProgramByID(int id) {
			return GetProgramByID(id, null);}

		private Program GetProgramByID(int id, SqlInterface si) {
			Program program = null;

			string storedProcName = "efrstore_get_program_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Program_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						program = LoadProgram(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return program;
		}


		public int InsertProgram(Program program) {
			return InsertProgram(program, null);}

		private int InsertProgram(Program program, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_program";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Program_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(program.Name)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(program.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					program.ProgramId = DBValue.ToInt32(paramCol["@Program_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateProgram(Program program) {
			return UpdateProgram(program, null);}

		private int UpdateProgram(Program program, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_program";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Program_id", DbType.Int32, DBValue.ToDBInt32(program.ProgramId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(program.Name)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(program.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region ProgramPartner Methods

		private ProgramPartner LoadProgramPartner(DataRow row) {
			ProgramPartner programPartner = new ProgramPartner();

			// Store database values into our business object
			programPartner.ProgramId = DBValue.ToInt32(row["program_id"]);
			programPartner.PartnerId = DBValue.ToInt32(row["partner_id"]);
			programPartner.ProgramUrl = DBValue.ToString(row["program_url"]);
			programPartner.CreateDate = DBValue.ToDateTime(row["create_date"]);

			// return the filled object
			return programPartner;
		}

		public ProgramPartner[] GetProgramPartners() {
			return GetProgramPartners(null);}

		private ProgramPartner[] GetProgramPartners(SqlInterface si) {
			ProgramPartner[] programPartners = null;

			string storedProcName = "efrstore_get_program_partners";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					programPartners = new ProgramPartner[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							programPartners[i] = LoadProgramPartner(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return programPartners;
		}


		public ProgramPartner GetProgramPartnerByID(int id) {
			return GetProgramPartnerByID(id, null);}

		private ProgramPartner GetProgramPartnerByID(int id, SqlInterface si) {
			ProgramPartner programPartner = null;

			string storedProcName = "efrstore_get_program_partner_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Program_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						programPartner = LoadProgramPartner(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return programPartner;
		}


		public int InsertProgramPartner(ProgramPartner programPartner) {
			return InsertProgramPartner(programPartner, null);}

		private int InsertProgramPartner(ProgramPartner programPartner, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_program_partner";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Program_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(programPartner.PartnerId)));
				paramCol.Add(new SqlDataParameter("@Program_url", DbType.String, DBValue.ToDBString(programPartner.ProgramUrl)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(programPartner.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					programPartner.ProgramId = DBValue.ToInt32(paramCol["@Program_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateProgramPartner(ProgramPartner programPartner) {
			return UpdateProgramPartner(programPartner, null);}

		private int UpdateProgramPartner(ProgramPartner programPartner, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_program_partner";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Program_id", DbType.Int32, DBValue.ToDBInt32(programPartner.ProgramId)));
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(programPartner.PartnerId)));
				paramCol.Add(new SqlDataParameter("@Program_url", DbType.String, DBValue.ToDBString(programPartner.ProgramUrl)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(programPartner.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region Promotion Methods

		private Promotion LoadPromotion(DataRow row) {
			Promotion promotion = new Promotion();

			// Store database values into our business object
			promotion.PromotionId = DBValue.ToInt32(row["promotion_id"]);
			promotion.PromotionTypeCode = DBValue.ToString(row["promotion_type_code"]);
			promotion.PromotionDestinationId = DBValue.ToInt32(row["promotion_destination_id"]);
			promotion.Name = DBValue.ToString(row["name"]);
			promotion.ScriptName = DBValue.ToString(row["script_name"]);
			promotion.Active = DBValue.ToInt16(row["active"]);
			promotion.CreateDate = DBValue.ToDateTime(row["create_date"]);

			// return the filled object
			return promotion;
		}

		public Promotion[] GetPromotions() {
			return GetPromotions(null);}

		private Promotion[] GetPromotions(SqlInterface si) {
			Promotion[] promotions = null;

			string storedProcName = "efrstore_get_promotions";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					promotions = new Promotion[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							promotions[i] = LoadPromotion(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return promotions;
		}


		public Promotion GetPromotionByID(int id) {
			return GetPromotionByID(id, null);}

		private Promotion GetPromotionByID(int id, SqlInterface si) {
			Promotion promotion = null;

			string storedProcName = "efrstore_get_promotion_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Promotion_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						promotion = LoadPromotion(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return promotion;
		}


		public int InsertPromotion(Promotion promotion) {
			return InsertPromotion(promotion, null);}

		private int InsertPromotion(Promotion promotion, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_promotion";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Promotion_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Promotion_type_code", DbType.String, DBValue.ToDBString(promotion.PromotionTypeCode)));
				paramCol.Add(new SqlDataParameter("@Promotion_destination_id", DbType.Int32, DBValue.ToDBInt32(promotion.PromotionDestinationId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(promotion.Name)));
				paramCol.Add(new SqlDataParameter("@Script_name", DbType.String, DBValue.ToDBString(promotion.ScriptName)));
				paramCol.Add(new SqlDataParameter("@Active", DbType.Int16, DBValue.ToDBInt16(promotion.Active)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(promotion.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					promotion.PromotionId = DBValue.ToInt32(paramCol["@Promotion_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdatePromotion(Promotion promotion) {
			return UpdatePromotion(promotion, null);}

		private int UpdatePromotion(Promotion promotion, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_promotion";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Promotion_id", DbType.Int32, DBValue.ToDBInt32(promotion.PromotionId)));
				paramCol.Add(new SqlDataParameter("@Promotion_type_code", DbType.String, DBValue.ToDBString(promotion.PromotionTypeCode)));
				paramCol.Add(new SqlDataParameter("@Promotion_destination_id", DbType.Int32, DBValue.ToDBInt32(promotion.PromotionDestinationId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(promotion.Name)));
				paramCol.Add(new SqlDataParameter("@Script_name", DbType.String, DBValue.ToDBString(promotion.ScriptName)));
				paramCol.Add(new SqlDataParameter("@Active", DbType.Int16, DBValue.ToDBInt16(promotion.Active)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(promotion.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region PromotionDestination Methods

		private PromotionDestination LoadPromotionDestination(DataRow row) {
			PromotionDestination promotionDestination = new PromotionDestination();

			// Store database values into our business object
			promotionDestination.PromotionDestinationId = DBValue.ToInt32(row["promotion_destination_id"]);
			promotionDestination.Url = DBValue.ToString(row["url"]);
			promotionDestination.CreateDate = DBValue.ToDateTime(row["create_date"]);

			// return the filled object
			return promotionDestination;
		}

		public PromotionDestination[] GetPromotionDestinations() {
			return GetPromotionDestinations(null);}

		private PromotionDestination[] GetPromotionDestinations(SqlInterface si) {
			PromotionDestination[] promotionDestinations = null;

			string storedProcName = "efrstore_get_promotion_destinations";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					promotionDestinations = new PromotionDestination[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							promotionDestinations[i] = LoadPromotionDestination(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return promotionDestinations;
		}


		public PromotionDestination GetPromotionDestinationByID(int id) {
			return GetPromotionDestinationByID(id, null);}

		private PromotionDestination GetPromotionDestinationByID(int id, SqlInterface si) {
			PromotionDestination promotionDestination = null;

			string storedProcName = "efrstore_get_promotion_destination_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Promotion_destination_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						promotionDestination = LoadPromotionDestination(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return promotionDestination;
		}


		public int InsertPromotionDestination(PromotionDestination promotionDestination) {
			return InsertPromotionDestination(promotionDestination, null);}

		private int InsertPromotionDestination(PromotionDestination promotionDestination, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_promotion_destination";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Promotion_destination_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Url", DbType.String, DBValue.ToDBString(promotionDestination.Url)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(promotionDestination.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					promotionDestination.PromotionDestinationId = DBValue.ToInt32(paramCol["@Promotion_destination_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdatePromotionDestination(PromotionDestination promotionDestination) {
			return UpdatePromotionDestination(promotionDestination, null);}

		private int UpdatePromotionDestination(PromotionDestination promotionDestination, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_promotion_destination";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Promotion_destination_id", DbType.Int32, DBValue.ToDBInt32(promotionDestination.PromotionDestinationId)));
				paramCol.Add(new SqlDataParameter("@Url", DbType.String, DBValue.ToDBString(promotionDestination.Url)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(promotionDestination.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region PromotionType Methods

		private PromotionType LoadPromotionType(DataRow row) {
			PromotionType promotionType = new PromotionType();

			// Store database values into our business object
			promotionType.PromotionTypeCode = DBValue.ToString(row["promotion_type_code"]);
			promotionType.Name = DBValue.ToString(row["name"]);
			promotionType.CreateDate = DBValue.ToDateTime(row["create_date"]);

			// return the filled object
			return promotionType;
		}

		public PromotionType[] GetPromotionTypes() {
			return GetPromotionTypes(null);}

		private PromotionType[] GetPromotionTypes(SqlInterface si) {
			PromotionType[] promotionTypes = null;

			string storedProcName = "efrstore_get_promotion_types";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					promotionTypes = new PromotionType[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							promotionTypes[i] = LoadPromotionType(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return promotionTypes;
		}



		#endregion

		#region Partner Methods

		private Partner LoadPartner(DataRow row) {
			Partner partner = new Partner();

			// Store database values into our business object
			partner.PartnerId = DBValue.ToInt32(row["partner_id"]);
			partner.PartnerTypeId = DBValue.ToInt32(row["partner_type_id"]);
			partner.PartnerName = DBValue.ToString(row["partner_name"]);
			partner.HasCollectionSite = DBValue.ToInt16(row["has_collection_site"]);
			partner.Guid = DBValue.ToString(row["guid"]);
			partner.CreateDate = DBValue.ToDateTime(row["create_date"]);

			// return the filled object
			return partner;
		}

		public Partner[] GetPartners() {
			return GetPartners(null);}

		private Partner[] GetPartners(SqlInterface si) {
			Partner[] partners = null;

			string storedProcName = "efrstore_get_partners";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					partners = new Partner[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							partners[i] = LoadPartner(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return partners;
		}


		public Partner GetPartnerByID(int id) {
			return GetPartnerByID(id, null);}

		private Partner GetPartnerByID(int id, SqlInterface si) {
			Partner partner = null;

			string storedProcName = "efrstore_get_partner_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						partner = LoadPartner(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return partner;
		}


		public int InsertPartner(Partner partner) {
			return InsertPartner(partner, null);}

		private int InsertPartner(Partner partner, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_partner";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Partner_type_id", DbType.Int32, DBValue.ToDBInt32(partner.PartnerTypeId)));
				paramCol.Add(new SqlDataParameter("@Partner_name", DbType.String, DBValue.ToDBString(partner.PartnerName)));
				paramCol.Add(new SqlDataParameter("@Has_collection_site", DbType.Int16, DBValue.ToDBInt16(partner.HasCollectionSite)));
				paramCol.Add(new SqlDataParameter("@Guid", DbType.String, DBValue.ToDBString(partner.Guid)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(partner.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					partner.PartnerId = DBValue.ToInt32(paramCol["@Partner_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdatePartner(Partner partner) {
			return UpdatePartner(partner, null);}

		private int UpdatePartner(Partner partner, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_partner";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(partner.PartnerId)));
				paramCol.Add(new SqlDataParameter("@Partner_type_id", DbType.Int32, DBValue.ToDBInt32(partner.PartnerTypeId)));
				paramCol.Add(new SqlDataParameter("@Partner_name", DbType.String, DBValue.ToDBString(partner.PartnerName)));
				paramCol.Add(new SqlDataParameter("@Has_collection_site", DbType.Int16, DBValue.ToDBInt16(partner.HasCollectionSite)));
				paramCol.Add(new SqlDataParameter("@Guid", DbType.String, DBValue.ToDBString(partner.Guid)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(partner.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region PartnerAttribute Methods

		private PartnerAttribute LoadPartnerAttribute(DataRow row) {
			PartnerAttribute partnerAttribute = new PartnerAttribute();

			// Store database values into our business object
			partnerAttribute.PartnerAttributeId = DBValue.ToInt32(row["partner_attribute_id"]);
			partnerAttribute.Name = DBValue.ToString(row["name"]);
			partnerAttribute.CreateDate = DBValue.ToDateTime(row["create_date"]);

			// return the filled object
			return partnerAttribute;
		}

		public PartnerAttribute[] GetPartnerAttributes() {
			return GetPartnerAttributes(null);}

		private PartnerAttribute[] GetPartnerAttributes(SqlInterface si) {
			PartnerAttribute[] partnerAttributes = null;

			string storedProcName = "efrstore_get_partner_attributes";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					partnerAttributes = new PartnerAttribute[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							partnerAttributes[i] = LoadPartnerAttribute(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return partnerAttributes;
		}


		public PartnerAttribute GetPartnerAttributeByID(int id) {
			return GetPartnerAttributeByID(id, null);}

		private PartnerAttribute GetPartnerAttributeByID(int id, SqlInterface si) {
			PartnerAttribute partnerAttribute = null;

			string storedProcName = "efrstore_get_partner_attribute_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_attribute_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						partnerAttribute = LoadPartnerAttribute(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return partnerAttribute;
		}


		public int InsertPartnerAttribute(PartnerAttribute partnerAttribute) {
			return InsertPartnerAttribute(partnerAttribute, null);}

		private int InsertPartnerAttribute(PartnerAttribute partnerAttribute, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_partner_attribute";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_attribute_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(partnerAttribute.Name)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(partnerAttribute.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					partnerAttribute.PartnerAttributeId = DBValue.ToInt32(paramCol["@Partner_attribute_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdatePartnerAttribute(PartnerAttribute partnerAttribute) {
			return UpdatePartnerAttribute(partnerAttribute, null);}

		private int UpdatePartnerAttribute(PartnerAttribute partnerAttribute, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_partner_attribute";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_attribute_id", DbType.Int32, DBValue.ToDBInt32(partnerAttribute.PartnerAttributeId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(partnerAttribute.Name)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(partnerAttribute.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region PartnerAttributeValue Methods

		private PartnerAttributeValue LoadPartnerAttributeValue(DataRow row) {
			PartnerAttributeValue partnerAttributeValue = new PartnerAttributeValue();

			// Store database values into our business object
			partnerAttributeValue.PartnerId = DBValue.ToInt32(row["partner_id"]);
			partnerAttributeValue.PartnerAttributeId = DBValue.ToInt32(row["partner_attribute_id"]);
			partnerAttributeValue.CultureCode = DBValue.ToString(row["culture_code"]);
			partnerAttributeValue.Value = DBValue.ToString(row["value"]);
			partnerAttributeValue.CreateDate = DBValue.ToDateTime(row["create_date"]);

			// return the filled object
			return partnerAttributeValue;
		}

		public PartnerAttributeValue[] GetPartnerAttributeValues() {
			return GetPartnerAttributeValues(null);}

		private PartnerAttributeValue[] GetPartnerAttributeValues(SqlInterface si) {
			PartnerAttributeValue[] partnerAttributeValues = null;

			string storedProcName = "efrstore_get_partner_attribute_values";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					partnerAttributeValues = new PartnerAttributeValue[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							partnerAttributeValues[i] = LoadPartnerAttributeValue(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return partnerAttributeValues;
		}


		public PartnerAttributeValue GetPartnerAttributeValueByID(int id) {
			return GetPartnerAttributeValueByID(id, null);}

		private PartnerAttributeValue GetPartnerAttributeValueByID(int id, SqlInterface si) {
			PartnerAttributeValue partnerAttributeValue = null;

			string storedProcName = "efrstore_get_partner_attribute_value_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						partnerAttributeValue = LoadPartnerAttributeValue(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return partnerAttributeValue;
		}


		public int InsertPartnerAttributeValue(PartnerAttributeValue partnerAttributeValue) {
			return InsertPartnerAttributeValue(partnerAttributeValue, null);}

		private int InsertPartnerAttributeValue(PartnerAttributeValue partnerAttributeValue, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_partner_attribute_value";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Partner_attribute_id", DbType.Int32, DBValue.ToDBInt32(partnerAttributeValue.PartnerAttributeId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(partnerAttributeValue.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Value", DbType.String, DBValue.ToDBString(partnerAttributeValue.Value)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(partnerAttributeValue.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					partnerAttributeValue.PartnerId = DBValue.ToInt32(paramCol["@Partner_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdatePartnerAttributeValue(PartnerAttributeValue partnerAttributeValue) {
			return UpdatePartnerAttributeValue(partnerAttributeValue, null);}

		private int UpdatePartnerAttributeValue(PartnerAttributeValue partnerAttributeValue, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_partner_attribute_value";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(partnerAttributeValue.PartnerId)));
				paramCol.Add(new SqlDataParameter("@Partner_attribute_id", DbType.Int32, DBValue.ToDBInt32(partnerAttributeValue.PartnerAttributeId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(partnerAttributeValue.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Value", DbType.String, DBValue.ToDBString(partnerAttributeValue.Value)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(partnerAttributeValue.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region PartnerContact Methods

		private PartnerContact LoadPartnerContact(DataRow row) {
			PartnerContact partnerContact = new PartnerContact();

			// Store database values into our business object
			partnerContact.PartnerContactId = DBValue.ToInt16(row["partner_contact_id"]);
			partnerContact.PartnerId = DBValue.ToInt32(row["partner_id"]);
			partnerContact.CultureCode = DBValue.ToString(row["culture_code"]);
			partnerContact.SectionName = DBValue.ToString(row["section_name"]);
			partnerContact.SectionValue = DBValue.ToString(row["section_value"]);
			partnerContact.DisplayOrder = DBValue.ToInt16(row["display_order"]);

			// return the filled object
			return partnerContact;
		}

		public PartnerContact[] GetPartnerContacts() {
			return GetPartnerContacts(null);}

		private PartnerContact[] GetPartnerContacts(SqlInterface si) {
			PartnerContact[] partnerContacts = null;

			string storedProcName = "efrstore_get_partner_contacts";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					partnerContacts = new PartnerContact[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							partnerContacts[i] = LoadPartnerContact(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return partnerContacts;
		}


		public PartnerContact GetPartnerContactByID(int id) {
			return GetPartnerContactByID(id, null);}

		private PartnerContact GetPartnerContactByID(int id, SqlInterface si) {
			PartnerContact partnerContact = null;

			string storedProcName = "efrstore_get_partner_contact_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_contact_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						partnerContact = LoadPartnerContact(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return partnerContact;
		}


		public int InsertPartnerContact(PartnerContact partnerContact) {
			return InsertPartnerContact(partnerContact, null);}

		private int InsertPartnerContact(PartnerContact partnerContact, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_partner_contact";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_contact_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(partnerContact.PartnerId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(partnerContact.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Section_name", DbType.String, DBValue.ToDBString(partnerContact.SectionName)));
				paramCol.Add(new SqlDataParameter("@Section_value", DbType.String, DBValue.ToDBString(partnerContact.SectionValue)));
				paramCol.Add(new SqlDataParameter("@Display_order", DbType.Int16, DBValue.ToDBInt16(partnerContact.DisplayOrder)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					partnerContact.PartnerContactId = DBValue.ToInt16(paramCol["@Partner_contact_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdatePartnerContact(PartnerContact partnerContact) {
			return UpdatePartnerContact(partnerContact, null);}

		private int UpdatePartnerContact(PartnerContact partnerContact, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_partner_contact";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_contact_id", DbType.Int16, DBValue.ToDBInt16(partnerContact.PartnerContactId)));
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(partnerContact.PartnerId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(partnerContact.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Section_name", DbType.String, DBValue.ToDBString(partnerContact.SectionName)));
				paramCol.Add(new SqlDataParameter("@Section_value", DbType.String, DBValue.ToDBString(partnerContact.SectionValue)));
				paramCol.Add(new SqlDataParameter("@Display_order", DbType.Int16, DBValue.ToDBInt16(partnerContact.DisplayOrder)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region PartnerGroupType Methods

		private PartnerGroupType LoadPartnerGroupType(DataRow row) {
			PartnerGroupType partnerGroupType = new PartnerGroupType();

			// Store database values into our business object
			partnerGroupType.PartnerGroupTypeId = DBValue.ToInt16(row["partner_group_type_id"]);
			partnerGroupType.Description = DBValue.ToString(row["description"]);

			// return the filled object
			return partnerGroupType;
		}

		public PartnerGroupType[] GetPartnerGroupTypes() {
			return GetPartnerGroupTypes(null);}

		private PartnerGroupType[] GetPartnerGroupTypes(SqlInterface si) {
			PartnerGroupType[] partnerGroupTypes = null;

			string storedProcName = "efrstore_get_partner_group_types";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					partnerGroupTypes = new PartnerGroupType[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							partnerGroupTypes[i] = LoadPartnerGroupType(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return partnerGroupTypes;
		}


		public PartnerGroupType GetPartnerGroupTypeByID(int id) {
			return GetPartnerGroupTypeByID(id, null);}

		private PartnerGroupType GetPartnerGroupTypeByID(int id, SqlInterface si) {
			PartnerGroupType partnerGroupType = null;

			string storedProcName = "efrstore_get_partner_group_type_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_group_type_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						partnerGroupType = LoadPartnerGroupType(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return partnerGroupType;
		}


		public int InsertPartnerGroupType(PartnerGroupType partnerGroupType) {
			return InsertPartnerGroupType(partnerGroupType, null);}

		private int InsertPartnerGroupType(PartnerGroupType partnerGroupType, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_partner_group_type";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_group_type_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(partnerGroupType.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					partnerGroupType.PartnerGroupTypeId = DBValue.ToInt16(paramCol["@Partner_group_type_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdatePartnerGroupType(PartnerGroupType partnerGroupType) {
			return UpdatePartnerGroupType(partnerGroupType, null);}

		private int UpdatePartnerGroupType(PartnerGroupType partnerGroupType, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_partner_group_type";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_group_type_id", DbType.Int16, DBValue.ToDBInt16(partnerGroupType.PartnerGroupTypeId)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(partnerGroupType.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region PartnerPackage Methods

		private PartnerPackage LoadPartnerPackage(DataRow row) {
			PartnerPackage partnerPackage = new PartnerPackage();

			// Store database values into our business object
			partnerPackage.PartnerId = DBValue.ToInt32(row["partner_id"]);
			partnerPackage.PackageId = DBValue.ToInt16(row["package_id"]);

			// return the filled object
			return partnerPackage;
		}

		public PartnerPackage[] GetPartnerPackages() {
			return GetPartnerPackages(null);}

		private PartnerPackage[] GetPartnerPackages(SqlInterface si) {
			PartnerPackage[] partnerPackages = null;

			string storedProcName = "efrstore_get_partner_packages";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					partnerPackages = new PartnerPackage[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							partnerPackages[i] = LoadPartnerPackage(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return partnerPackages;
		}


		public PartnerPackage GetPartnerPackageByID(int id) {
			return GetPartnerPackageByID(id, null);}

		private PartnerPackage GetPartnerPackageByID(int id, SqlInterface si) {
			PartnerPackage partnerPackage = null;

			string storedProcName = "efrstore_get_partner_package_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						partnerPackage = LoadPartnerPackage(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return partnerPackage;
		}


		public int InsertPartnerPackage(PartnerPackage partnerPackage) {
			return InsertPartnerPackage(partnerPackage, null);}

		private int InsertPartnerPackage(PartnerPackage partnerPackage, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_partner_package";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int16, DBValue.ToDBInt16(partnerPackage.PackageId)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					partnerPackage.PartnerId = DBValue.ToInt32(paramCol["@Partner_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdatePartnerPackage(PartnerPackage partnerPackage) {
			return UpdatePartnerPackage(partnerPackage, null);}

		private int UpdatePartnerPackage(PartnerPackage partnerPackage, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_partner_package";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(partnerPackage.PartnerId)));
				paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int16, DBValue.ToDBInt16(partnerPackage.PackageId)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region PartnerPromotion Methods

		private PartnerPromotion LoadPartnerPromotion(DataRow row) {
			PartnerPromotion partnerPromotion = new PartnerPromotion();

			// Store database values into our business object
			partnerPromotion.PartnerPromotionId = DBValue.ToInt32(row["partner_promotion_id"]);
			partnerPromotion.PartnerId = DBValue.ToInt32(row["partner_id"]);
			partnerPromotion.PromotionId = DBValue.ToInt32(row["promotion_id"]);
			partnerPromotion.CreateDate = DBValue.ToDateTime(row["create_date"]);

			// return the filled object
			return partnerPromotion;
		}

		public PartnerPromotion[] GetPartnerPromotions() {
			return GetPartnerPromotions(null);}

		private PartnerPromotion[] GetPartnerPromotions(SqlInterface si) {
			PartnerPromotion[] partnerPromotions = null;

			string storedProcName = "efrstore_get_partner_promotions";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					partnerPromotions = new PartnerPromotion[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							partnerPromotions[i] = LoadPartnerPromotion(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return partnerPromotions;
		}


		public PartnerPromotion GetPartnerPromotionByID(int id) {
			return GetPartnerPromotionByID(id, null);}

		private PartnerPromotion GetPartnerPromotionByID(int id, SqlInterface si) {
			PartnerPromotion partnerPromotion = null;

			string storedProcName = "efrstore_get_partner_promotion_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_promotion_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						partnerPromotion = LoadPartnerPromotion(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return partnerPromotion;
		}


		public int InsertPartnerPromotion(PartnerPromotion partnerPromotion) {
			return InsertPartnerPromotion(partnerPromotion, null);}

		private int InsertPartnerPromotion(PartnerPromotion partnerPromotion, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_partner_promotion";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_promotion_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(partnerPromotion.PartnerId)));
				paramCol.Add(new SqlDataParameter("@Promotion_id", DbType.Int32, DBValue.ToDBInt32(partnerPromotion.PromotionId)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(partnerPromotion.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					partnerPromotion.PartnerPromotionId = DBValue.ToInt32(paramCol["@Partner_promotion_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdatePartnerPromotion(PartnerPromotion partnerPromotion) {
			return UpdatePartnerPromotion(partnerPromotion, null);}

		private int UpdatePartnerPromotion(PartnerPromotion partnerPromotion, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_partner_promotion";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_promotion_id", DbType.Int32, DBValue.ToDBInt32(partnerPromotion.PartnerPromotionId)));
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(partnerPromotion.PartnerId)));
				paramCol.Add(new SqlDataParameter("@Promotion_id", DbType.Int32, DBValue.ToDBInt32(partnerPromotion.PromotionId)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(partnerPromotion.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region PartnerType Methods

		private PartnerType LoadPartnerType(DataRow row) {
			PartnerType partnerType = new PartnerType();

			// Store database values into our business object
			partnerType.PartnerTypeId = DBValue.ToInt32(row["partner_type_id"]);
			partnerType.Name = DBValue.ToString(row["name"]);
			partnerType.CreateDate = DBValue.ToDateTime(row["create_date"]);

			// return the filled object
			return partnerType;
		}

		public PartnerType[] GetPartnerTypes() {
			return GetPartnerTypes(null);}

		private PartnerType[] GetPartnerTypes(SqlInterface si) {
			PartnerType[] partnerTypes = null;

			string storedProcName = "efrstore_get_partner_types";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					partnerTypes = new PartnerType[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							partnerTypes[i] = LoadPartnerType(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return partnerTypes;
		}


		public PartnerType GetPartnerTypeByID(int id) {
			return GetPartnerTypeByID(id, null);}

		private PartnerType GetPartnerTypeByID(int id, SqlInterface si) {
			PartnerType partnerType = null;

			string storedProcName = "efrstore_get_partner_type_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_type_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						partnerType = LoadPartnerType(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return partnerType;
		}


		public int InsertPartnerType(PartnerType partnerType) {
			return InsertPartnerType(partnerType, null);}

		private int InsertPartnerType(PartnerType partnerType, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_partner_type";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_type_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(partnerType.Name)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(partnerType.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					partnerType.PartnerTypeId = DBValue.ToInt32(paramCol["@Partner_type_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdatePartnerType(PartnerType partnerType) {
			return UpdatePartnerType(partnerType, null);}

		private int UpdatePartnerType(PartnerType partnerType, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_partner_type";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_type_id", DbType.Int32, DBValue.ToDBInt32(partnerType.PartnerTypeId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(partnerType.Name)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(partnerType.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region PartnerTypeCulture Methods

		private PartnerTypeCulture LoadPartnerTypeCulture(DataRow row) {
			PartnerTypeCulture partnerTypeCulture = new PartnerTypeCulture();

			// Store database values into our business object
			partnerTypeCulture.PartnerTypeId = DBValue.ToInt32(row["partner_type_id"]);
			partnerTypeCulture.CultureCode = DBValue.ToString(row["culture_code"]);
			partnerTypeCulture.Name = DBValue.ToString(row["name"]);
			partnerTypeCulture.CreateDate = DBValue.ToDateTime(row["create_date"]);

			// return the filled object
			return partnerTypeCulture;
		}

		public PartnerTypeCulture[] GetPartnerTypeCultures() {
			return GetPartnerTypeCultures(null);}

		private PartnerTypeCulture[] GetPartnerTypeCultures(SqlInterface si) {
			PartnerTypeCulture[] partnerTypeCultures = null;

			string storedProcName = "efrstore_get_partner_type_cultures";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					partnerTypeCultures = new PartnerTypeCulture[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							partnerTypeCultures[i] = LoadPartnerTypeCulture(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return partnerTypeCultures;
		}


		public PartnerTypeCulture GetPartnerTypeCultureByID(int id) {
			return GetPartnerTypeCultureByID(id, null);}

		private PartnerTypeCulture GetPartnerTypeCultureByID(int id, SqlInterface si) {
			PartnerTypeCulture partnerTypeCulture = null;

			string storedProcName = "efrstore_get_partner_type_culture_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_type_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						partnerTypeCulture = LoadPartnerTypeCulture(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return partnerTypeCulture;
		}


		public int InsertPartnerTypeCulture(PartnerTypeCulture partnerTypeCulture) {
			return InsertPartnerTypeCulture(partnerTypeCulture, null);}

		private int InsertPartnerTypeCulture(PartnerTypeCulture partnerTypeCulture, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_partner_type_culture";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_type_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(partnerTypeCulture.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(partnerTypeCulture.Name)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(partnerTypeCulture.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					partnerTypeCulture.PartnerTypeId = DBValue.ToInt32(paramCol["@Partner_type_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdatePartnerTypeCulture(PartnerTypeCulture partnerTypeCulture) {
			return UpdatePartnerTypeCulture(partnerTypeCulture, null);}

		private int UpdatePartnerTypeCulture(PartnerTypeCulture partnerTypeCulture, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_partner_type_culture";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Partner_type_id", DbType.Int32, DBValue.ToDBInt32(partnerTypeCulture.PartnerTypeId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(partnerTypeCulture.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(partnerTypeCulture.Name)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(partnerTypeCulture.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region Product Methods

		private Product LoadProduct(DataRow row) {
			Product product = new Product();

			// Store database values into our business object
			product.ProductId = DBValue.ToInt32(row["product_id"]);
			product.ParentProductId = DBValue.ToInt32(row["parent_product_id"]);
			product.ScratchBookId = DBValue.ToInt32(row["scratch_book_id"]);
			product.Name = DBValue.ToString(row["name"]);
			product.RaisingPotential = DBValue.ToDecimal(row["raising_potential"]);
			product.ProductCode = DBValue.ToString(row["product_code"]);
			product.Enabled = DBValue.ToInt16(row["enabled"]);
			product.IsInner = DBValue.ToInt16(row["is_inner"]);
			product.CreateDate = DBValue.ToDateTime(row["create_date"]);

			// return the filled object
			return product;
		}

		public Product[] GetProducts() {
			return GetProducts(null);}

		private Product[] GetProducts(SqlInterface si) {
			Product[] products = null;

			string storedProcName = "efrstore_get_products";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					products = new Product[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							products[i] = LoadProduct(dt.Rows[i]);
							
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return products;
		}

		public ProductCollection GetProductsByName(string name) 
		{
			return GetProductsByName(name, null);}

		private ProductCollection GetProductsByName(string name, SqlInterface si) 
		{
			ProductCollection products = null;

			string storedProcName = "efrstore_get_products_by_name";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@name", DbType.String, DBValue.ToString(name)));
		
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) 
				{
					products = new ProductCollection();

					for (int i = 0; i < dt.Rows.Count; i++) 
					{
						// fill our objects
						try 
						{
							Product product = LoadProduct(dt.Rows[i]);
							product.ProductDescription = GetProductDescByID(product.ProductId);
							product.LoadChildrenProducts();
							products.Add(product);
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return products;
		}

		public ProductCollection GetProductsByNameSimilar(string name, int packageRootID) 
		{
			return GetProductsByNameSimilar(name, packageRootID, null);}

		private ProductCollection GetProductsByNameSimilar(string name, int packageRootID, SqlInterface si) 
		{
			ProductCollection products = null;

			string storedProcName = "efrstore_get_products_by_name_similar";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@name", DbType.String, DBValue.ToString(name)));
				paramCol.Add(new SqlDataParameter("@package_root_id", DbType.Int32, DBValue.ToInt32(packageRootID)));
		
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) 
				{
					products = new ProductCollection();

					for (int i = 0; i < dt.Rows.Count; i++) 
					{
						// fill our objects
						try 
						{
							Product product = LoadProduct(dt.Rows[i]);
							product.ProductDescription = GetProductDescByID(product.ProductId);
							product.LoadChildrenProducts();
							products.Add(product);
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return products;
		}

		public ProductCollection GetProductsByProductCode(string code, int packageRootID) 
		{
			return GetProductsByProductCode(code, packageRootID, null);}

		private ProductCollection GetProductsByProductCode(string code, int packageRootID, SqlInterface si) 
		{
			ProductCollection products = null;

			string storedProcName = "efrstore_get_products_by_product_code";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@product_code", DbType.String, DBValue.ToString(code)));
				paramCol.Add(new SqlDataParameter("@package_root_id", DbType.Int32, DBValue.ToInt32(packageRootID)));
		
		
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) 
				{
					products = new ProductCollection();

					for (int i = 0; i < dt.Rows.Count; i++) 
					{
						// fill our objects
						try 
						{
							Product product = LoadProduct(dt.Rows[i]);
							product.ProductDescription = GetProductDescByID(product.ProductId);
							product.LoadChildrenProducts();
							products.Add(product);
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return products;
		}
		
        public ProductCollection GetProductsRoot() {
			return GetProductsRoot(null);
		}

		private ProductCollection GetProductsRoot(SqlInterface si) {
			ProductCollection products = null;

			string storedProcName = "efrstore_get_products_root";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					products = new ProductCollection();

					for (int i = 0; i < dt.Rows.Count; i++) {
						// fill our objects
						try {
							Product product = LoadProduct(dt.Rows[i]);
							product.ProductDescription = GetProductDescByID(product.ProductId);
							product.LoadChildrenProducts();
							products.Add(product);
						} 
						catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return products;
		}

		public ProductCollection GetProductsByScratchBookID(int scratchBookID, int packageRootID) 
		{
			return GetProductsByScratchBookID(scratchBookID, packageRootID, null);
		}

		private ProductCollection GetProductsByScratchBookID(int scratchBookID, int packageRootID, SqlInterface si) 
		{
			ProductCollection products = null;

			string storedProcName = "efrstore_get_products_By_Scratch_Book_ID";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@scratch_book_id", DbType.Int32, DBValue.ToInt32(scratchBookID)));
				paramCol.Add(new SqlDataParameter("@package_root_id", DbType.Int32, DBValue.ToInt32(packageRootID)));
		
				
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) 
				{
					products = new ProductCollection();

					for (int i = 0; i < dt.Rows.Count; i++) 
					{
						// fill our objects
						try 
						{
							Product product = LoadProduct(dt.Rows[i]);
							product.ProductDescription = GetProductDescByID(product.ProductId);
							product.LoadChildrenProducts();
							products.Add(product);
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return products;
		}

		public ProductDescCollection GetProductDescsByImageName(string imageName) 
		{
			return GetProductDescsByImageName(imageName, null);}

		private ProductDescCollection GetProductDescsByImageName(string imageName, SqlInterface si) 
		{
			ProductDescCollection productDescs = null;

			string storedProcName = "efrstore_get_product_descs_by_image_name";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Image_Name", DbType.String, DBValue.ToDBString(imageName)));
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) 
				{
					productDescs = new ProductDescCollection();
					for (int i = 0; i < dt.Rows.Count; i++)	
					{
						// fill our objects
						try 
						{
							productDescs.Add(LoadProductDesc(dt.Rows[0]));
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return productDescs;
		}


		public int GetProductRootIDByID(int id) 
		{
			return GetProductRootIDByID(id, null);}

		private int GetProductRootIDByID(int id, SqlInterface si) 
		{
			int productID = 0;

			string storedProcName = "efrstore_get_product_parent_by_product_id";


			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{

				SqlDataParameterCollection paramCol;

				bool parentFound = true;
				while (parentFound)
				{

					// declare stored procedure parameters
					paramCol = new SqlDataParameterCollection();
					paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
					if (newConnection) 
					{
						// open the connection
						si.Open();
					}

					DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

					if(dt != null && dt.Rows.Count > 0) 
					{
						// fill our objects
						try 
						{
							id = DBValue.ToInt32(dt.Rows[0]["parent_product_id"]);
							if (id == 0 || id == int.MinValue)
							{
								parentFound = false;
							}
							else
							{
								productID = id;
							}

							
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
					else
					{
						parentFound = false;
					}
					if(newConnection) 
					{
						// Always close connection.
						si.Close();
					}
					
				}

			}
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
		
			return productID;
		}

		public Product GetProductByID(int id) {
			return GetProductByID(id, null);}

		public Product GetProductByTopParentPackageIDAndPageName(int topParentPackageId, string pageName)
		{
			return GetProductByTopParentPackageIDAndPageName(topParentPackageId, pageName, null);
		}

		
		private Product GetProductByTopParentPackageIDAndPageName(int topParentPackageId, string pageName, SqlInterface si) 
		{
			Product product = null;

			string storedProcName = "efrstore_get_product_by_top_parent_package_id_and_page_name";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Top_Parent_Package_id", DbType.Int32, DBValue.ToDBInt32(topParentPackageId)));
				paramCol.Add(new SqlDataParameter("@Page_Name", DbType.String, DBValue.ToDBString(pageName)));

				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) 
				{
					// fill our objects
					try 
					{
						product = LoadProduct(dt.Rows[0]);
						product.ProductDescription = GetProductDescByID(product.ProductId);
					} 
					catch(Exception ex) 
					{
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return product;
		}
		

		private Product GetProductByID(int id, SqlInterface si) 
		{
			Product product = null;

			string storedProcName = "efrstore_get_product_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						product = LoadProduct(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}

			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return product;
		}

        public ProductCollection GetProductsByPackageIDWebsite(int id)
        {
            return GetProductsByPackageIDWebsite(id, null);
        }

        private ProductCollection GetProductsByPackageIDWebsite(int id, SqlInterface si)
        {
            ProductCollection products = null;

            string storedProcName = "efrstore_get_products_by_package_id_website";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int32, DBValue.ToDBInt32(id)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null && dt.Rows.Count > 0)
                {

                    products = new ProductCollection();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            bool enabled = Convert.ToBoolean(dt.Rows[i]["Enabled"]);
                            if (enabled)
                            {
                                Product product = LoadProduct(dt.Rows[i]);
                                product.LoadChildrenProducts();
                                product.ProductDescription = GetProductDescByID(product.ProductId);
                                products.Add(product);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return products;
        }
       
        
        public ProductCollection GetProductsByPackageID(int id) 
		{
			return GetProductsByPackageID(id, null);}

		private ProductCollection GetProductsByPackageID(int id, SqlInterface si) 
		{
			ProductCollection products = null;

            string storedProcName = "efrstore_get_products_by_package_id_website";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) 
				{
					
					products = new ProductCollection();

					for (int i = 0; i < dt.Rows.Count; i++)	
					{
						// fill our objects
						try 
						{
							bool enabled = Convert.ToBoolean(dt.Rows[i]["Enabled"]);
							if (enabled)
							{
								Product product = LoadProduct(dt.Rows[i]);
								product.LoadChildrenProducts();
								product.ProductDescription = GetProductDescByID(product.ProductId);
								products.Add(product);
							}
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return products;
		}

		public ProductCollection GetProductsByParentId(int id) 
		{
			return GetProductsByParentId(id, null);}

		private ProductCollection GetProductsByParentId(int id, SqlInterface si) 
		{
			ProductCollection products = null;

			string storedProcName = "efrstore_get_products_by_parent_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null) 
				{
					products = new ProductCollection();

					for (int i = 0; i < dt.Rows.Count; i++)	
					{
						// fill our objects
						try 
						{
							Product p = LoadProduct(dt.Rows[i]);
							p.ProductDescription = GetProductDescByID(p.ProductId);
							products.Add(p);
						}
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					} 
					
					
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return products;
		}

		public int InsertProduct(Product product) {
			return InsertProduct(product, null);}

		private int InsertProduct(Product product, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_product";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Parent_product_id", DbType.Int32, DBValue.ToDBInt32(product.ParentProductId)));
				paramCol.Add(new SqlDataParameter("@Scratch_book_id", DbType.Int32, DBValue.ToDBInt32(product.ScratchBookId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(product.Name)));
				paramCol.Add(new SqlDataParameter("@Raising_potential", DbType.Decimal, DBValue.ToDBDecimal(product.RaisingPotential)));
				paramCol.Add(new SqlDataParameter("@Product_code", DbType.String, DBValue.ToDBString(product.ProductCode)));
				paramCol.Add(new SqlDataParameter("@Enabled", DbType.Int16, DBValue.ToDBInt16(product.Enabled)));
				paramCol.Add(new SqlDataParameter("@Is_inner", DbType.Int16, DBValue.ToDBInt16(product.IsInner)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(product.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					product.ProductId = DBValue.ToInt32(paramCol["@Product_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateProduct(Product product) {
			return UpdateProduct(product, null);}

		private int UpdateProduct(Product product, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_product";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, DBValue.ToDBInt32(product.ProductId)));
				paramCol.Add(new SqlDataParameter("@Parent_product_id", DbType.Int32, DBValue.ToDBInt32(product.ParentProductId)));
				paramCol.Add(new SqlDataParameter("@Scratch_book_id", DbType.Int32, DBValue.ToDBInt32(product.ScratchBookId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(product.Name)));
				paramCol.Add(new SqlDataParameter("@Raising_potential", DbType.Decimal, DBValue.ToDBDecimal(product.RaisingPotential)));
				paramCol.Add(new SqlDataParameter("@Product_code", DbType.String, DBValue.ToDBString(product.ProductCode)));
				paramCol.Add(new SqlDataParameter("@Enabled", DbType.Int16, DBValue.ToDBInt16(product.Enabled)));
				paramCol.Add(new SqlDataParameter("@Is_inner", DbType.Int16, DBValue.ToDBInt16(product.IsInner)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(product.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region ProductClass Methods

		private ProductClass LoadProductClass(DataRow row) {
			ProductClass productClass = new ProductClass();

			// Store database values into our business object
			productClass.ProductClassId = DBValue.ToInt32(row["product_class_id"]);
			productClass.DivisionId = DBValue.ToInt32(row["division_id"]);
			productClass.AccountingClassId = DBValue.ToInt16(row["accounting_class_id"]);
			productClass.Description = DBValue.ToString(row["description"]);
			productClass.Display = DBValue.ToInt16(row["display"]);
			productClass.MinimumOrderQty = DBValue.ToInt16(row["minimum_order_qty"]);

			// return the filled object
			return productClass;
		}

		public ProductClass[] GetProductClasss() {
			return GetProductClasss(null);}

		private ProductClass[] GetProductClasss(SqlInterface si) {
			ProductClass[] productClasss = null;

			string storedProcName = "efrstore_get_product_classs";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					productClasss = new ProductClass[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							productClasss[i] = LoadProductClass(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return productClasss;
		}


		public ProductClass GetProductClassByID(int id) {
			return GetProductClassByID(id, null);}

		private ProductClass GetProductClassByID(int id, SqlInterface si) {
			ProductClass productClass = null;

			string storedProcName = "efrstore_get_product_class_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_class_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						productClass = LoadProductClass(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return productClass;
		}


		public int InsertProductClass(ProductClass productClass) {
			return InsertProductClass(productClass, null);}

		private int InsertProductClass(ProductClass productClass, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_product_class";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_class_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Division_id", DbType.Int32, DBValue.ToDBInt32(productClass.DivisionId)));
				paramCol.Add(new SqlDataParameter("@Accounting_class_id", DbType.Int16, DBValue.ToDBInt16(productClass.AccountingClassId)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(productClass.Description)));
				paramCol.Add(new SqlDataParameter("@Display", DbType.Int16, DBValue.ToDBInt16(productClass.Display)));
				paramCol.Add(new SqlDataParameter("@Minimum_order_qty", DbType.Int16, DBValue.ToDBInt16(productClass.MinimumOrderQty)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					productClass.ProductClassId = DBValue.ToInt32(paramCol["@Product_class_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateProductClass(ProductClass productClass) {
			return UpdateProductClass(productClass, null);}

		private int UpdateProductClass(ProductClass productClass, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_product_class";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_class_id", DbType.Int32, DBValue.ToDBInt32(productClass.ProductClassId)));
				paramCol.Add(new SqlDataParameter("@Division_id", DbType.Int32, DBValue.ToDBInt32(productClass.DivisionId)));
				paramCol.Add(new SqlDataParameter("@Accounting_class_id", DbType.Int16, DBValue.ToDBInt16(productClass.AccountingClassId)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(productClass.Description)));
				paramCol.Add(new SqlDataParameter("@Display", DbType.Int16, DBValue.ToDBInt16(productClass.Display)));
				paramCol.Add(new SqlDataParameter("@Minimum_order_qty", DbType.Int16, DBValue.ToDBInt16(productClass.MinimumOrderQty)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region ProductClassDesc Methods

		private ProductClassDesc LoadProductClassDesc(DataRow row) {
			ProductClassDesc productClassDesc = new ProductClassDesc();

			// Store database values into our business object
			productClassDesc.ProductClassId = DBValue.ToInt32(row["product_class_id"]);
			productClassDesc.LanguageId = DBValue.ToInt16(row["language_id"]);
			productClassDesc.ProductClassDescription = DBValue.ToString(row["product_class_desc"]);
			productClassDesc.MinRequirement = DBValue.ToString(row["min_requirement"]);

			// return the filled object
			return productClassDesc;
		}

		public ProductClassDesc[] GetProductClassDescs() {
			return GetProductClassDescs(null);}

		private ProductClassDesc[] GetProductClassDescs(SqlInterface si) {
			ProductClassDesc[] productClassDescs = null;

			string storedProcName = "efrstore_get_product_class_descs";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					productClassDescs = new ProductClassDesc[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							productClassDescs[i] = LoadProductClassDesc(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return productClassDescs;
		}


		public ProductClassDesc GetProductClassDescByID(int id) {
			return GetProductClassDescByID(id, null);}

		private ProductClassDesc GetProductClassDescByID(int id, SqlInterface si) {
			ProductClassDesc productClassDesc = null;

			string storedProcName = "efrstore_get_product_class_desc_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_class_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						productClassDesc = LoadProductClassDesc(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return productClassDesc;
		}


		public int InsertProductClassDesc(ProductClassDesc productClassDesc) {
			return InsertProductClassDesc(productClassDesc, null);}

		private int InsertProductClassDesc(ProductClassDesc productClassDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_product_class_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_class_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Language_id", DbType.Int16, DBValue.ToDBInt16(productClassDesc.LanguageId)));
				paramCol.Add(new SqlDataParameter("@Product_class_desc", DbType.String, DBValue.ToDBString(productClassDesc.ProductClassDescription)));
				paramCol.Add(new SqlDataParameter("@Min_requirement", DbType.String, DBValue.ToDBString(productClassDesc.MinRequirement)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					productClassDesc.ProductClassId = DBValue.ToInt32(paramCol["@Product_class_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateProductClassDesc(ProductClassDesc productClassDesc) {
			return UpdateProductClassDesc(productClassDesc, null);}

		private int UpdateProductClassDesc(ProductClassDesc productClassDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_product_class_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_class_id", DbType.Int32, DBValue.ToDBInt32(productClassDesc.ProductClassId)));
				paramCol.Add(new SqlDataParameter("@Language_id", DbType.Int16, DBValue.ToDBInt16(productClassDesc.LanguageId)));
				paramCol.Add(new SqlDataParameter("@Product_class_desc", DbType.String, DBValue.ToDBString(productClassDesc.ProductClassDescription)));
				paramCol.Add(new SqlDataParameter("@Min_requirement", DbType.String, DBValue.ToDBString(productClassDesc.MinRequirement)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region ProductDesc Methods

		private ProductDesc LoadProductDesc(DataRow row) {
			ProductDesc productDesc = new ProductDesc();

			// Store database values into our business object
			productDesc.ProductId = DBValue.ToInt32(row["product_id"]);
			productDesc.CultureCode = DBValue.ToString(row["culture_code"]);
			productDesc.TemplateId = DBValue.ToInt32(row["template_id"]);
			productDesc.Name = DBValue.ToString(row["name"]);
			productDesc.ShortDesc = DBValue.ToString(row["short_desc"]);
			productDesc.LongDesc = DBValue.ToString(row["long_desc"]);
			productDesc.ExtraDesc = DBValue.ToString(row["extra_desc"]);
			productDesc.PageName = DBValue.ToString(row["page_name"]);
			productDesc.PageTitle = DBValue.ToString(row["page_title"]);
			productDesc.ImageName = DBValue.ToString(row["image_name"]);
			productDesc.ImageAltText = DBValue.ToString(row["image_alt_text"]);
			productDesc.DisplayOrder = DBValue.ToInt32(row["display_order"]);
			productDesc.Enabled = DBValue.ToInt16(row["enabled"]);
			productDesc.Configuration = DBValue.ToString(row["configuration"]);
			productDesc.CreateDate = DBValue.ToDateTime(row["create_date"]);

			// return the filled object
			return productDesc;
		}

		public ProductDesc[] GetProductDescs() {
			return GetProductDescs(null);}

		private ProductDesc[] GetProductDescs(SqlInterface si) {
			ProductDesc[] productDescs = null;

			string storedProcName = "efrstore_get_product_descs";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					productDescs = new ProductDesc[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							productDescs[i] = LoadProductDesc(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return productDescs;
		}

		public ProductDesc GetProductDescByProductIDAndCultureCode(int id, string cultureCode) 
		{
			return GetProductDescByProductIDAndCultureCode(id, cultureCode, null);}

		private ProductDesc GetProductDescByProductIDAndCultureCode(int id, string cultureCode, SqlInterface si) 
		{
			ProductDesc productDesc = null;

			string storedProcName = "efrstore_get_product_desc_by_product_id_and_culture_code";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, DBValue.ToDBInt32(id)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(cultureCode)));
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) 
				{
					// fill our objects
					try 
					{
						productDesc = LoadProductDesc(dt.Rows[0]);
					} 
					catch(Exception ex) 
					{
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return productDesc;
		}

		public ProductDescCollection GetProductDescsByProductID(int productID) 
		{
			return GetProductDescsByProductID(productID, null);}

		private ProductDescCollection GetProductDescsByProductID(int productID, SqlInterface si) 
		{
			ProductDescCollection productDescs = null;

			string storedProcName = "efrstore_get_product_descs_by_product_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
			    paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32,  DBValue.ToDBInt32(productID)));
			
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);
			  
				if (dt != null) 
				{
					productDescs = new ProductDescCollection();
					for (int i = 0; i < dt.Rows.Count; i++)	
					{
						// fill our objects
						try 
						{
							productDescs.Add(LoadProductDesc(dt.Rows[i]));
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return productDescs;
		}

		public ProductDescCollection GetProductDescsByPageName(string pageName) 
		{
			return GetProductDescsByPageName(pageName, null);}

		private ProductDescCollection GetProductDescsByPageName(string pageName, SqlInterface si) 
		{
			ProductDescCollection productDescs = null;

			string storedProcName = "efrstore_get_product_descs_by_page_name";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Page_name", DbType.String,  DBValue.ToDBString(pageName)));
			
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);
			  
				if (dt != null) 
				{
					productDescs = new ProductDescCollection();
					for (int i = 0; i < dt.Rows.Count; i++)	
					{
						// fill our objects
						try 
						{
							productDescs.Add(LoadProductDesc(dt.Rows[i]));
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return productDescs;
		}


		public ProductDesc GetProductDescByID(int id) 
		{
			return GetProductDescByID(id, null);}

		private ProductDesc GetProductDescByID(int id, SqlInterface si) {
			ProductDesc productDesc = null;

			string storedProcName = "efrstore_get_product_desc_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						productDesc = LoadProductDesc(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return productDesc;
		}
		public ProductDesc GetProductDescByPageNameAndTemplateExists( string pageName)
		{
			return GetProductDescByPageNameAndTemplateExists(pageName,null);
		}
		private ProductDesc GetProductDescByPageNameAndTemplateExists(string pageName, SqlInterface si)
		{
			ProductDesc productDesc = null;

			string storedProcName = "efrstore_get_product_descs_by_page_name_and_template_exist";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@page_name", DbType.String, DBValue.ToString(pageName)));
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) 
				{
					// fill our objects
					try 
					{
						productDesc = LoadProductDesc(dt.Rows[0]);
					} 
					catch(Exception ex) 
					{
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return productDesc;
		}
		public ProductDesc GetProductDescByPageNameAndPackageRootID( string pageName)
		{
			return GetProductDescByPageNameAndPackageRootID(pageName,null);
		}
		private ProductDesc GetProductDescByPageNameAndPackageRootID(string pageName, SqlInterface si)
		{
			ProductDesc productDesc = null;

			string storedProcName = "efrstore_get_product_descs_by_page_name";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@page_name", DbType.String, DBValue.ToString(pageName)));
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) 
				{
					// fill our objects
					try 
					{
						productDesc = LoadProductDesc(dt.Rows[0]);
					} 
					catch(Exception ex) 
					{
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return productDesc;
		}
		public int InsertProductDesc(ProductDesc productDesc) {
			return InsertProductDesc(productDesc, null);}

		private int InsertProductDesc(ProductDesc productDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_product_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, DBValue.ToDBInt32(productDesc.ProductId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(productDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Template_id", DbType.Int32, DBValue.ToDBInt32(productDesc.TemplateId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(productDesc.Name)));
				paramCol.Add(new SqlDataParameter("@Short_desc", DbType.String, DBValue.ToDBString(productDesc.ShortDesc)));
				paramCol.Add(new SqlDataParameter("@Long_desc", DbType.String, DBValue.ToDBString(productDesc.LongDesc)));
				paramCol.Add(new SqlDataParameter("@Extra_desc", DbType.String, DBValue.ToDBString(productDesc.ExtraDesc)));
				paramCol.Add(new SqlDataParameter("@Page_name", DbType.String, DBValue.ToDBString(productDesc.PageName)));
				paramCol.Add(new SqlDataParameter("@Page_title", DbType.String, DBValue.ToDBString(productDesc.PageTitle)));
				paramCol.Add(new SqlDataParameter("@Image_name", DbType.String, DBValue.ToDBString(productDesc.ImageName)));
				paramCol.Add(new SqlDataParameter("@Image_alt_text", DbType.String, DBValue.ToDBString(productDesc.ImageAltText)));
				paramCol.Add(new SqlDataParameter("@Display_order", DbType.Int32, DBValue.ToDBInt32(productDesc.DisplayOrder)));
				paramCol.Add(new SqlDataParameter("@Enabled", DbType.Int16, DBValue.ToDBInt16(productDesc.Enabled)));
				paramCol.Add(new SqlDataParameter("@Configuration", DbType.String, DBValue.ToDBString(productDesc.Configuration)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(productDesc.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					productDesc.ProductId = DBValue.ToInt32(paramCol["@Product_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateProductDesc(ProductDesc productDesc) {
			return UpdateProductDesc(productDesc, null);}

		private int UpdateProductDesc(ProductDesc productDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_product_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, DBValue.ToDBInt32(productDesc.ProductId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(productDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Template_id", DbType.Int32, DBValue.ToDBInt32(productDesc.TemplateId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(productDesc.Name)));
				paramCol.Add(new SqlDataParameter("@Short_desc", DbType.String, DBValue.ToDBString(productDesc.ShortDesc)));
				paramCol.Add(new SqlDataParameter("@Long_desc", DbType.String, DBValue.ToDBString(productDesc.LongDesc)));
				paramCol.Add(new SqlDataParameter("@Extra_desc", DbType.String, DBValue.ToDBString(productDesc.ExtraDesc)));
				paramCol.Add(new SqlDataParameter("@Page_name", DbType.String, DBValue.ToDBString(productDesc.PageName)));
				paramCol.Add(new SqlDataParameter("@Page_title", DbType.String, DBValue.ToDBString(productDesc.PageTitle)));
				paramCol.Add(new SqlDataParameter("@Image_name", DbType.String, DBValue.ToDBString(productDesc.ImageName)));
				paramCol.Add(new SqlDataParameter("@Image_alt_text", DbType.String, DBValue.ToDBString(productDesc.ImageAltText)));
				paramCol.Add(new SqlDataParameter("@Display_order", DbType.Int32, DBValue.ToDBInt32(productDesc.DisplayOrder)));
				paramCol.Add(new SqlDataParameter("@Enabled", DbType.Int16, DBValue.ToDBInt16(productDesc.Enabled)));
				paramCol.Add(new SqlDataParameter("@Configuration", DbType.String, DBValue.ToDBString(productDesc.Configuration)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(productDesc.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region ProductPackage Methods

		private ProductPackage LoadProductPackage(DataRow row) {
			ProductPackage productPackage = new ProductPackage();

			// Store database values into our business object
			productPackage.ProductId = DBValue.ToInt32(row["product_id"]);
			productPackage.PackageId = DBValue.ToInt16(row["package_id"]);
			productPackage.DisplayOrder = DBValue.ToInt16(row["display_order"]);
			productPackage.Display = DBValue.ToInt16(row["display"]);
			productPackage.CreateDate = DBValue.ToDateTime(row["create_date"]);

			// return the filled object
			return productPackage;
		}

		public ProductPackage[] GetProductPackages() {
			return GetProductPackages(null);}

		private ProductPackage[] GetProductPackages(SqlInterface si) {
			ProductPackage[] productPackages = null;

			string storedProcName = "efrstore_get_product_packages";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					productPackages = new ProductPackage[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							productPackages[i] = LoadProductPackage(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return productPackages;
		}

		public ProductPackage GetProductPackageByPackageIDAndProductID(int packageID, int productID) {
			return GetProductPackageByPackageIDAndProductID(packageID, productID, null);}

		private ProductPackage GetProductPackageByPackageIDAndProductID(int packageID, int productID, SqlInterface si) {
			ProductPackage productPackage = null;

			string storedProcName = "efrstore_get_product_package_by_package_id_and_product_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, DBValue.ToDBInt32(productID)));
				paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int32, DBValue.ToDBInt32(packageID)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						productPackage = LoadProductPackage(dt.Rows[0]);
					} 
					catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} 
			finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return productPackage;
		}

		public ProductPackageCollection GetProductPackageByProductID(int productID) 
		{
			return GetProductPackageByProductID(productID, null);}

		private ProductPackageCollection GetProductPackageByProductID(int productID, SqlInterface si) 
		{
			ProductPackageCollection productPackages = null;

			string storedProcName = "efrstore_get_product_package_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, DBValue.ToDBInt32(productID)));
		
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) 
				{
					productPackages = new ProductPackageCollection();

					for (int i = 0; i < dt.Rows.Count; i++)	
					{
						// fill our objects
						try 
						{
							ProductPackage pp = LoadProductPackage(dt.Rows[i]);
                            productPackages.Add(pp);
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}
			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return productPackages;
		}

		public ProductPackageCollection GetProductPackageByPackageID(short packageID) {
			return GetProductPackageByPackageID(packageID, null);}

		private ProductPackageCollection GetProductPackageByPackageID(short packageID, SqlInterface si) {
			ProductPackageCollection productPackages = null;

			string storedProcName = "efrstore_get_product_package_by_package_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int16, DBValue.ToDBInt16(packageID)));
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					productPackages = new ProductPackageCollection();

					for (int i = 0; i < dt.Rows.Count; i++) {
						// fill our objects
						try {
							ProductPackage pp = LoadProductPackage(dt.Rows[i]);
							productPackages.Add(pp);
						} 
						catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}
			} 
			finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return productPackages;
		}

		public ProductPackage GetProductPackageByID(int id) {
			return GetProductPackageByID(id, null);}

		private ProductPackage GetProductPackageByID(int id, SqlInterface si) {
			ProductPackage productPackage = null;

			string storedProcName = "efrstore_get_product_package_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						productPackage = LoadProductPackage(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return productPackage;
		}


		public int InsertProductPackage(ProductPackage productPackage) {
			return InsertProductPackage(productPackage, null);}

		private int InsertProductPackage(ProductPackage productPackage, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_product_package";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int16, DBValue.ToDBInt16(productPackage.ProductId)));
				paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int16, DBValue.ToDBInt16(productPackage.PackageId)));
				paramCol.Add(new SqlDataParameter("@Display_order", DbType.Int16, DBValue.ToDBInt16(productPackage.DisplayOrder)));
				paramCol.Add(new SqlDataParameter("@Display", DbType.Int16, DBValue.ToDBInt16(productPackage.Display)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(productPackage.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					productPackage.ProductId = DBValue.ToInt32(paramCol["@Product_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateProductPackage(ProductPackage productPackage) {
			return UpdateProductPackage(productPackage, null);}

		private int UpdateProductPackage(ProductPackage productPackage, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_product_package";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, DBValue.ToDBInt32(productPackage.ProductId)));
				paramCol.Add(new SqlDataParameter("@Package_id", DbType.Int16, DBValue.ToDBInt16(productPackage.PackageId)));
				paramCol.Add(new SqlDataParameter("@Display_order", DbType.Int16, DBValue.ToDBInt16(productPackage.DisplayOrder)));
				paramCol.Add(new SqlDataParameter("@Display", DbType.Int16, DBValue.ToDBInt16(productPackage.Display)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(productPackage.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region ProductPriceInfo Methods

		private ProductPriceInfo LoadProductPriceInfo(DataRow row) {
			ProductPriceInfo productPriceInfo = new ProductPriceInfo();

			// Store database values into our business object
			productPriceInfo.ProductId = DBValue.ToInt32(row["product_id"]);
			productPriceInfo.CountryCode = DBValue.ToString(row["country_code"]);
			productPriceInfo.EffectiveDate = DBValue.ToDateTime(row["effective_date"]);
			productPriceInfo.ProductClassId = DBValue.ToInt32(row["product_class_id"]);
			productPriceInfo.UnitPrice = DBValue.ToDecimal(row["unit_price"]);

			// return the filled object
			return productPriceInfo;
		}

		public ProductPriceInfo[] GetProductPriceInfos() {
			return GetProductPriceInfos(null);}

		private ProductPriceInfo[] GetProductPriceInfos(SqlInterface si) {
			ProductPriceInfo[] productPriceInfos = null;

			string storedProcName = "efrstore_get_product_price_infos";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					productPriceInfos = new ProductPriceInfo[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							productPriceInfos[i] = LoadProductPriceInfo(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return productPriceInfos;
		}


		public ProductPriceInfo GetProductPriceInfoByID(int id) {
			return GetProductPriceInfoByID(id, null);}

		private ProductPriceInfo GetProductPriceInfoByID(int id, SqlInterface si) {
			ProductPriceInfo productPriceInfo = null;

			string storedProcName = "efrstore_get_product_price_info_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						productPriceInfo = LoadProductPriceInfo(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return productPriceInfo;
		}


		public int InsertProductPriceInfo(ProductPriceInfo productPriceInfo) {
			return InsertProductPriceInfo(productPriceInfo, null);}

		private int InsertProductPriceInfo(ProductPriceInfo productPriceInfo, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_product_price_info";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Country_code", DbType.String, DBValue.ToDBString(productPriceInfo.CountryCode)));
				paramCol.Add(new SqlDataParameter("@Effective_date", DbType.DateTime, DBValue.ToDBDateTime(productPriceInfo.EffectiveDate)));
				paramCol.Add(new SqlDataParameter("@Product_class_id", DbType.Int32, DBValue.ToDBInt32(productPriceInfo.ProductClassId)));
				paramCol.Add(new SqlDataParameter("@Unit_price", DbType.Decimal, DBValue.ToDBDecimal(productPriceInfo.UnitPrice)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					productPriceInfo.ProductId = DBValue.ToInt32(paramCol["@Product_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateProductPriceInfo(ProductPriceInfo productPriceInfo) {
			return UpdateProductPriceInfo(productPriceInfo, null);}

		private int UpdateProductPriceInfo(ProductPriceInfo productPriceInfo, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_product_price_info";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Product_id", DbType.Int32, DBValue.ToDBInt32(productPriceInfo.ProductId)));
				paramCol.Add(new SqlDataParameter("@Country_code", DbType.String, DBValue.ToDBString(productPriceInfo.CountryCode)));
				paramCol.Add(new SqlDataParameter("@Effective_date", DbType.DateTime, DBValue.ToDBDateTime(productPriceInfo.EffectiveDate)));
				paramCol.Add(new SqlDataParameter("@Product_class_id", DbType.Int32, DBValue.ToDBInt32(productPriceInfo.ProductClassId)));
				paramCol.Add(new SqlDataParameter("@Unit_price", DbType.Decimal, DBValue.ToDBDecimal(productPriceInfo.UnitPrice)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion
        
		#region Question Methods

		private Question LoadQuestion(DataRow row) {
			Question question = new Question();

			// Store database values into our business object
			question.QuestionId = DBValue.ToInt32(row["question_id"]);
			question.Name = DBValue.ToString(row["name"]);
			question.Description = DBValue.ToString(row["description"]);
			question.ControlTypeId = DBValue.ToInt32(row["control_type_id"]);
			question.FieldName = DBValue.ToString(row["field_name"]);
			question.DefaultValue = DBValue.ToString(row["default_value"]);
			question.MinLenght = DBValue.ToInt32(row["min_lenght"]);
			question.MaxLenght = DBValue.ToInt32(row["max_lenght"]);
			question.NbrValue = DBValue.ToInt32(row["nbr_value"]);
			question.Datestamp = DBValue.ToDateTime(row["datestamp"]);
			question.StoredProcToCall = DBValue.ToString(row["stored_proc_to_call"]);
			question.AnswerType = DBValue.ToString(row["answer_type"]);
			question.FieldValue = DBValue.ToString(row["field_value"]);

			// return the filled object
			return question;
		}

		public Question[] GetQuestions() {
			return GetQuestions(null);}

		private Question[] GetQuestions(SqlInterface si) {
			Question[] questions = null;

			string storedProcName = "efrstore_get_questions";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					questions = new Question[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							questions[i] = LoadQuestion(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return questions;
		}


		public Question GetQuestionByID(int id) {
			return GetQuestionByID(id, null);}

		private Question GetQuestionByID(int id, SqlInterface si) {
			Question question = null;

			string storedProcName = "efrstore_get_question_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Question_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						question = LoadQuestion(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return question;
		}


		public int InsertQuestion(Question question) {
			return InsertQuestion(question, null);}

		private int InsertQuestion(Question question, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_question";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Question_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(question.Name)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(question.Description)));
				paramCol.Add(new SqlDataParameter("@Control_type_id", DbType.Int32, DBValue.ToDBInt32(question.ControlTypeId)));
				paramCol.Add(new SqlDataParameter("@Field_name", DbType.String, DBValue.ToDBString(question.FieldName)));
				paramCol.Add(new SqlDataParameter("@Default_value", DbType.String, DBValue.ToDBString(question.DefaultValue)));
				paramCol.Add(new SqlDataParameter("@Min_lenght", DbType.Int32, DBValue.ToDBInt32(question.MinLenght)));
				paramCol.Add(new SqlDataParameter("@Max_lenght", DbType.Int32, DBValue.ToDBInt32(question.MaxLenght)));
				paramCol.Add(new SqlDataParameter("@Nbr_value", DbType.Int32, DBValue.ToDBInt32(question.NbrValue)));
				paramCol.Add(new SqlDataParameter("@Datestamp", DbType.DateTime, DBValue.ToDBDateTime(question.Datestamp)));
				paramCol.Add(new SqlDataParameter("@Stored_proc_to_call", DbType.String, DBValue.ToDBString(question.StoredProcToCall)));
				paramCol.Add(new SqlDataParameter("@Answer_type", DbType.String, DBValue.ToDBString(question.AnswerType)));
				paramCol.Add(new SqlDataParameter("@Field_value", DbType.String, DBValue.ToDBString(question.FieldValue)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					question.QuestionId = DBValue.ToInt32(paramCol["@Question_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateQuestion(Question question) {
			return UpdateQuestion(question, null);}

		private int UpdateQuestion(Question question, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_question";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Question_id", DbType.Int32, DBValue.ToDBInt32(question.QuestionId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(question.Name)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(question.Description)));
				paramCol.Add(new SqlDataParameter("@Control_type_id", DbType.Int32, DBValue.ToDBInt32(question.ControlTypeId)));
				paramCol.Add(new SqlDataParameter("@Field_name", DbType.String, DBValue.ToDBString(question.FieldName)));
				paramCol.Add(new SqlDataParameter("@Default_value", DbType.String, DBValue.ToDBString(question.DefaultValue)));
				paramCol.Add(new SqlDataParameter("@Min_lenght", DbType.Int32, DBValue.ToDBInt32(question.MinLenght)));
				paramCol.Add(new SqlDataParameter("@Max_lenght", DbType.Int32, DBValue.ToDBInt32(question.MaxLenght)));
				paramCol.Add(new SqlDataParameter("@Nbr_value", DbType.Int32, DBValue.ToDBInt32(question.NbrValue)));
				paramCol.Add(new SqlDataParameter("@Datestamp", DbType.DateTime, DBValue.ToDBDateTime(question.Datestamp)));
				paramCol.Add(new SqlDataParameter("@Stored_proc_to_call", DbType.String, DBValue.ToDBString(question.StoredProcToCall)));
				paramCol.Add(new SqlDataParameter("@Answer_type", DbType.String, DBValue.ToDBString(question.AnswerType)));
				paramCol.Add(new SqlDataParameter("@Field_value", DbType.String, DBValue.ToDBString(question.FieldValue)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region QuestionParamTarget Methods

		private QuestionParamTarget LoadQuestionParamTarget(DataRow row) {
			QuestionParamTarget questionParamTarget = new QuestionParamTarget();

			// Store database values into our business object
			questionParamTarget.QuestionId = DBValue.ToInt32(row["question_id"]);
			questionParamTarget.WebFormId = DBValue.ToInt32(row["web_form_id"]);
			questionParamTarget.ParameterTarget = DBValue.ToString(row["parameter_target"]);

			// return the filled object
			return questionParamTarget;
		}

		public QuestionParamTarget[] GetQuestionParamTargets() {
			return GetQuestionParamTargets(null);}

		private QuestionParamTarget[] GetQuestionParamTargets(SqlInterface si) {
			QuestionParamTarget[] questionParamTargets = null;

			string storedProcName = "efrstore_get_question_param_targets";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					questionParamTargets = new QuestionParamTarget[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							questionParamTargets[i] = LoadQuestionParamTarget(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return questionParamTargets;
		}


		public QuestionParamTarget GetQuestionParamTargetByID(int id) {
			return GetQuestionParamTargetByID(id, null);}

		private QuestionParamTarget GetQuestionParamTargetByID(int id, SqlInterface si) {
			QuestionParamTarget questionParamTarget = null;

			string storedProcName = "efrstore_get_question_param_target_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Question_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						questionParamTarget = LoadQuestionParamTarget(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return questionParamTarget;
		}


		public int InsertQuestionParamTarget(QuestionParamTarget questionParamTarget) {
			return InsertQuestionParamTarget(questionParamTarget, null);}

		private int InsertQuestionParamTarget(QuestionParamTarget questionParamTarget, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_question_param_target";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Question_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Web_form_id", DbType.Int32, DBValue.ToDBInt32(questionParamTarget.WebFormId)));
				paramCol.Add(new SqlDataParameter("@Parameter_target", DbType.String, DBValue.ToDBString(questionParamTarget.ParameterTarget)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					questionParamTarget.QuestionId = DBValue.ToInt32(paramCol["@Question_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateQuestionParamTarget(QuestionParamTarget questionParamTarget) {
			return UpdateQuestionParamTarget(questionParamTarget, null);}

		private int UpdateQuestionParamTarget(QuestionParamTarget questionParamTarget, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_question_param_target";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Question_id", DbType.Int32, DBValue.ToDBInt32(questionParamTarget.QuestionId)));
				paramCol.Add(new SqlDataParameter("@Web_form_id", DbType.Int32, DBValue.ToDBInt32(questionParamTarget.WebFormId)));
				paramCol.Add(new SqlDataParameter("@Parameter_target", DbType.String, DBValue.ToDBString(questionParamTarget.ParameterTarget)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region QuestionsEntryForm Methods

		private QuestionsEntryForm LoadQuestionsEntryForm(DataRow row) {
			QuestionsEntryForm questionsEntryForm = new QuestionsEntryForm();

			// Store database values into our business object
			questionsEntryForm.QuestionId = DBValue.ToInt32(row["question_id"]);
			questionsEntryForm.WebFormId = DBValue.ToInt32(row["web_form_id"]);
			questionsEntryForm.Required = DBValue.ToInt16(row["required"]);
			questionsEntryForm.QuestionOrder = DBValue.ToInt32(row["question_order"]);
			questionsEntryForm.InsertTable = DBValue.ToString(row["insert_table"]);
			questionsEntryForm.InsertColumn = DBValue.ToString(row["insert_column"]);
			questionsEntryForm.DefaultValue = DBValue.ToString(row["default_value"]);

			// return the filled object
			return questionsEntryForm;
		}

		public QuestionsEntryForm[] GetQuestionsEntryForms() {
			return GetQuestionsEntryForms(null);}

		private QuestionsEntryForm[] GetQuestionsEntryForms(SqlInterface si) {
			QuestionsEntryForm[] questionsEntryForms = null;

			string storedProcName = "efrstore_get_questions_entry_forms";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					questionsEntryForms = new QuestionsEntryForm[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							questionsEntryForms[i] = LoadQuestionsEntryForm(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return questionsEntryForms;
		}


		public QuestionsEntryForm GetQuestionsEntryFormByID(int id) {
			return GetQuestionsEntryFormByID(id, null);}

		private QuestionsEntryForm GetQuestionsEntryFormByID(int id, SqlInterface si) {
			QuestionsEntryForm questionsEntryForm = null;

			string storedProcName = "efrstore_get_questions_entry_form_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Question_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						questionsEntryForm = LoadQuestionsEntryForm(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return questionsEntryForm;
		}


		public int InsertQuestionsEntryForm(QuestionsEntryForm questionsEntryForm) {
			return InsertQuestionsEntryForm(questionsEntryForm, null);}

		private int InsertQuestionsEntryForm(QuestionsEntryForm questionsEntryForm, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_questions_entry_form";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Question_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Web_form_id", DbType.Int32, DBValue.ToDBInt32(questionsEntryForm.WebFormId)));
				paramCol.Add(new SqlDataParameter("@Required", DbType.Int16, DBValue.ToDBInt16(questionsEntryForm.Required)));
				paramCol.Add(new SqlDataParameter("@Question_order", DbType.Int32, DBValue.ToDBInt32(questionsEntryForm.QuestionOrder)));
				paramCol.Add(new SqlDataParameter("@Insert_table", DbType.String, DBValue.ToDBString(questionsEntryForm.InsertTable)));
				paramCol.Add(new SqlDataParameter("@Insert_column", DbType.String, DBValue.ToDBString(questionsEntryForm.InsertColumn)));
				paramCol.Add(new SqlDataParameter("@Default_value", DbType.String, DBValue.ToDBString(questionsEntryForm.DefaultValue)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					questionsEntryForm.QuestionId = DBValue.ToInt32(paramCol["@Question_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateQuestionsEntryForm(QuestionsEntryForm questionsEntryForm) {
			return UpdateQuestionsEntryForm(questionsEntryForm, null);}

		private int UpdateQuestionsEntryForm(QuestionsEntryForm questionsEntryForm, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_questions_entry_form";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Question_id", DbType.Int32, DBValue.ToDBInt32(questionsEntryForm.QuestionId)));
				paramCol.Add(new SqlDataParameter("@Web_form_id", DbType.Int32, DBValue.ToDBInt32(questionsEntryForm.WebFormId)));
				paramCol.Add(new SqlDataParameter("@Required", DbType.Int16, DBValue.ToDBInt16(questionsEntryForm.Required)));
				paramCol.Add(new SqlDataParameter("@Question_order", DbType.Int32, DBValue.ToDBInt32(questionsEntryForm.QuestionOrder)));
				paramCol.Add(new SqlDataParameter("@Insert_table", DbType.String, DBValue.ToDBString(questionsEntryForm.InsertTable)));
				paramCol.Add(new SqlDataParameter("@Insert_column", DbType.String, DBValue.ToDBString(questionsEntryForm.InsertColumn)));
				paramCol.Add(new SqlDataParameter("@Default_value", DbType.String, DBValue.ToDBString(questionsEntryForm.DefaultValue)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region Salutation Methods

		private Salutation LoadSalutation(DataRow row) {
			Salutation salutation = new Salutation();

			// Store database values into our business object
			salutation.SalutationId = DBValue.ToInt16(row["salutation_id"]);
			salutation.Description = DBValue.ToString(row["description"]);

			// return the filled object
			return salutation;
		}

		public Salutation[] GetSalutations() {
			return GetSalutations(null);}

		private Salutation[] GetSalutations(SqlInterface si) {
			Salutation[] salutations = null;

			string storedProcName = "efrstore_get_salutations";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					salutations = new Salutation[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							salutations[i] = LoadSalutation(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return salutations;
		}


		public Salutation GetSalutationByID(int id) {
			return GetSalutationByID(id, null);}

		private Salutation GetSalutationByID(int id, SqlInterface si) {
			Salutation salutation = null;

			string storedProcName = "efrstore_get_salutation_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Salutation_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						salutation = LoadSalutation(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return salutation;
		}


		public int InsertSalutation(Salutation salutation) {
			return InsertSalutation(salutation, null);}

		private int InsertSalutation(Salutation salutation, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_salutation";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Salutation_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(salutation.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					salutation.SalutationId = DBValue.ToInt16(paramCol["@Salutation_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateSalutation(Salutation salutation) {
			return UpdateSalutation(salutation, null);}

		private int UpdateSalutation(Salutation salutation, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_salutation";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Salutation_id", DbType.Int16, DBValue.ToDBInt16(salutation.SalutationId)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(salutation.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region SalutationDesc Methods

		private SalutationDesc LoadSalutationDesc(DataRow row) {
			SalutationDesc salutationDesc = new SalutationDesc();

			// Store database values into our business object
			salutationDesc.SalutationId = DBValue.ToInt16(row["salutation_id"]);
			salutationDesc.CultureCode = DBValue.ToString(row["culture_code"]);
			salutationDesc.Description = DBValue.ToString(row["description"]);

			// return the filled object
			return salutationDesc;
		}

		public SalutationDesc[] GetSalutationDescs() {
			return GetSalutationDescs(null);}

		private SalutationDesc[] GetSalutationDescs(SqlInterface si) {
			SalutationDesc[] salutationDescs = null;

			string storedProcName = "efrstore_get_salutation_descs";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					salutationDescs = new SalutationDesc[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							salutationDescs[i] = LoadSalutationDesc(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return salutationDescs;
		}


		public SalutationDesc GetSalutationDescByID(int id) {
			return GetSalutationDescByID(id, null);}

		private SalutationDesc GetSalutationDescByID(int id, SqlInterface si) {
			SalutationDesc salutationDesc = null;

			string storedProcName = "efrstore_get_salutation_desc_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Salutation_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						salutationDesc = LoadSalutationDesc(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return salutationDesc;
		}


		public int InsertSalutationDesc(SalutationDesc salutationDesc) {
			return InsertSalutationDesc(salutationDesc, null);}

		private int InsertSalutationDesc(SalutationDesc salutationDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_salutation_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Salutation_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(salutationDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(salutationDesc.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					salutationDesc.SalutationId = DBValue.ToInt16(paramCol["@Salutation_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateSalutationDesc(SalutationDesc salutationDesc) {
			return UpdateSalutationDesc(salutationDesc, null);}

		private int UpdateSalutationDesc(SalutationDesc salutationDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_salutation_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Salutation_id", DbType.Int16, DBValue.ToDBInt16(salutationDesc.SalutationId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(salutationDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(salutationDesc.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region Session Methods

		private Session LoadSession(DataRow row) {
			Session session = new Session();

			// Store database values into our business object
			session.SessionId = DBValue.ToInt32(row["session_id"]);
			session.VisitorsLogId = DBValue.ToInt32(row["visitors_log_id"]);
			session.DateCreated = DBValue.ToDateTime(row["date_created"]);

			// return the filled object
			return session;
		}

		public Session[] GetSessions() {
			return GetSessions(null);}

		private Session[] GetSessions(SqlInterface si) {
			Session[] sessions = null;

			string storedProcName = "efrstore_get_sessions";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					sessions = new Session[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							sessions[i] = LoadSession(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return sessions;
		}


		public Session GetSessionByID(int id) {
			return GetSessionByID(id, null);}

		private Session GetSessionByID(int id, SqlInterface si) {
			Session session = null;

			string storedProcName = "efrstore_get_session_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Session_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						session = LoadSession(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return session;
		}


		public int InsertSession(Session session) {
			return InsertSession(session, null);}

		private int InsertSession(Session session, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_session";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Session_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Visitors_log_id", DbType.Int32, DBValue.ToDBInt32(session.VisitorsLogId)));
				paramCol.Add(new SqlDataParameter("@Date_created", DbType.DateTime, DBValue.ToDBDateTime(session.DateCreated)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					session.SessionId = DBValue.ToInt32(paramCol["@Session_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateSession(Session session) {
			return UpdateSession(session, null);}

		private int UpdateSession(Session session, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_session";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Session_id", DbType.Int32, DBValue.ToDBInt32(session.SessionId)));
				paramCol.Add(new SqlDataParameter("@Visitors_log_id", DbType.Int32, DBValue.ToDBInt32(session.VisitorsLogId)));
				paramCol.Add(new SqlDataParameter("@Date_created", DbType.DateTime, DBValue.ToDBDateTime(session.DateCreated)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region SessionItem Methods

		private SessionItem LoadSessionItem(DataRow row) {
			SessionItem sessionItem = new SessionItem();

			// Store database values into our business object
			sessionItem.SessionItemId = DBValue.ToInt32(row["session_item_id"]);
			sessionItem.SessionId = DBValue.ToInt32(row["session_id"]);
			sessionItem.Name = DBValue.ToString(row["name"]);
			sessionItem.Value = DBValue.ToString(row["value"]);

			// return the filled object
			return sessionItem;
		}

		public SessionItem[] GetSessionItems() {
			return GetSessionItems(null);}

		private SessionItem[] GetSessionItems(SqlInterface si) {
			SessionItem[] sessionItems = null;

			string storedProcName = "efrstore_get_session_items";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					sessionItems = new SessionItem[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							sessionItems[i] = LoadSessionItem(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return sessionItems;
		}


		public SessionItem GetSessionItemByID(int id) {
			return GetSessionItemByID(id, null);}

		private SessionItem GetSessionItemByID(int id, SqlInterface si) {
			SessionItem sessionItem = null;

			string storedProcName = "efrstore_get_session_item_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Session_item_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						sessionItem = LoadSessionItem(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return sessionItem;
		}


		public int InsertSessionItem(SessionItem sessionItem) {
			return InsertSessionItem(sessionItem, null);}

		private int InsertSessionItem(SessionItem sessionItem, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_session_item";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Session_item_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Session_id", DbType.Int32, DBValue.ToDBInt32(sessionItem.SessionId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(sessionItem.Name)));
				paramCol.Add(new SqlDataParameter("@Value", DbType.String, DBValue.ToDBString(sessionItem.Value)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					sessionItem.SessionItemId = DBValue.ToInt32(paramCol["@Session_item_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateSessionItem(SessionItem sessionItem) {
			return UpdateSessionItem(sessionItem, null);}

		private int UpdateSessionItem(SessionItem sessionItem, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_session_item";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Session_item_id", DbType.Int32, DBValue.ToDBInt32(sessionItem.SessionItemId)));
				paramCol.Add(new SqlDataParameter("@Session_id", DbType.Int32, DBValue.ToDBInt32(sessionItem.SessionId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(sessionItem.Name)));
				paramCol.Add(new SqlDataParameter("@Value", DbType.String, DBValue.ToDBString(sessionItem.Value)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region ShoppingCart Methods

		private ShoppingCart LoadShoppingCart(DataRow row) {
			ShoppingCart shoppingCart = new ShoppingCart();

			// Store database values into our business object
			shoppingCart.ShoppingCartId = DBValue.ToInt32(row["shopping_cart_id"]);
			shoppingCart.VisitorLogId = DBValue.ToInt32(row["visitor_log_id"]);
			shoppingCart.OnlineUserId = DBValue.ToInt32(row["online_user_id"]);
			shoppingCart.ShoppingCartCode = DBValue.ToString(row["shopping_cart_code"]);
			shoppingCart.DateCreated = DBValue.ToDateTime(row["date_created"]);

			// return the filled object
			return shoppingCart;
		}

		public ShoppingCart[] GetShoppingCarts() {
			return GetShoppingCarts(null);}

		private ShoppingCart[] GetShoppingCarts(SqlInterface si) {
			ShoppingCart[] shoppingCarts = null;

			string storedProcName = "efrstore_get_shopping_carts";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					shoppingCarts = new ShoppingCart[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							shoppingCarts[i] = LoadShoppingCart(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return shoppingCarts;
		}


		public ShoppingCart GetShoppingCartByID(int id) {
			return GetShoppingCartByID(id, null);}

		private ShoppingCart GetShoppingCartByID(int id, SqlInterface si) {
			ShoppingCart shoppingCart = null;

			string storedProcName = "efrstore_get_shopping_cart_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Shopping_cart_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						shoppingCart = LoadShoppingCart(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return shoppingCart;
		}


		public int InsertShoppingCart(ShoppingCart shoppingCart) {
			return InsertShoppingCart(shoppingCart, null);}

		private int InsertShoppingCart(ShoppingCart shoppingCart, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_shopping_cart";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Shopping_cart_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Visitor_log_id", DbType.Int32, DBValue.ToDBInt32(shoppingCart.VisitorLogId)));
				paramCol.Add(new SqlDataParameter("@Online_user_id", DbType.Int32, DBValue.ToDBInt32(shoppingCart.OnlineUserId)));
				paramCol.Add(new SqlDataParameter("@Shopping_cart_code", DbType.String, DBValue.ToDBString(shoppingCart.ShoppingCartCode)));
				paramCol.Add(new SqlDataParameter("@Date_created", DbType.DateTime, DBValue.ToDBDateTime(shoppingCart.DateCreated)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					shoppingCart.ShoppingCartId = DBValue.ToInt32(paramCol["@Shopping_cart_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateShoppingCart(ShoppingCart shoppingCart) {
			return UpdateShoppingCart(shoppingCart, null);}

		private int UpdateShoppingCart(ShoppingCart shoppingCart, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_shopping_cart";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Shopping_cart_id", DbType.Int32, DBValue.ToDBInt32(shoppingCart.ShoppingCartId)));
				paramCol.Add(new SqlDataParameter("@Visitor_log_id", DbType.Int32, DBValue.ToDBInt32(shoppingCart.VisitorLogId)));
				paramCol.Add(new SqlDataParameter("@Online_user_id", DbType.Int32, DBValue.ToDBInt32(shoppingCart.OnlineUserId)));
				paramCol.Add(new SqlDataParameter("@Shopping_cart_code", DbType.String, DBValue.ToDBString(shoppingCart.ShoppingCartCode)));
				paramCol.Add(new SqlDataParameter("@Date_created", DbType.DateTime, DBValue.ToDBDateTime(shoppingCart.DateCreated)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region ShoppingCartCode Methods

		private ShoppingCartCode LoadShoppingCartCode(DataRow row) {
			ShoppingCartCode shoppingCartCode = new ShoppingCartCode();

			// Store database values into our business object
			shoppingCartCode.ShoppingCartCodeID = DBValue.ToString(row["shopping_cart_code"]);
			shoppingCartCode.Description = DBValue.ToString(row["description"]);

			// return the filled object
			return shoppingCartCode;
		}

		public ShoppingCartCode[] GetShoppingCartCodes() {
			return GetShoppingCartCodes(null);}

		private ShoppingCartCode[] GetShoppingCartCodes(SqlInterface si) {
			ShoppingCartCode[] shoppingCartCodes = null;

			string storedProcName = "efrstore_get_shopping_cart_codes";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					shoppingCartCodes = new ShoppingCartCode[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							shoppingCartCodes[i] = LoadShoppingCartCode(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return shoppingCartCodes;
		}



		#endregion

		#region ShoppingCartItem Methods

		private ShoppingCartItem LoadShoppingCartItem(DataRow row) {
			ShoppingCartItem shoppingCartItem = new ShoppingCartItem();

			// Store database values into our business object
			shoppingCartItem.ShoppingCartId = DBValue.ToInt32(row["shopping_cart_id"]);
			shoppingCartItem.ScratchBookId = DBValue.ToInt32(row["scratch_book_id"]);
			shoppingCartItem.CarrierId = DBValue.ToInt16(row["carrier_id"]);
			shoppingCartItem.ShippingOptionId = DBValue.ToInt16(row["shipping_option_id"]);
			shoppingCartItem.Quantity = DBValue.ToInt16(row["quantity"]);
			shoppingCartItem.ClientUploadedImg = DBValue.ToString(row["client_uploaded_img"]);
			shoppingCartItem.GroupName = DBValue.ToString(row["group_name"]);

			// return the filled object
			return shoppingCartItem;
		}

		public ShoppingCartItem[] GetShoppingCartItems() {
			return GetShoppingCartItems(null);}

		private ShoppingCartItem[] GetShoppingCartItems(SqlInterface si) {
			ShoppingCartItem[] shoppingCartItems = null;

			string storedProcName = "efrstore_get_shopping_cart_items";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					shoppingCartItems = new ShoppingCartItem[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							shoppingCartItems[i] = LoadShoppingCartItem(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return shoppingCartItems;
		}


		public ShoppingCartItem GetShoppingCartItemByID(int id) {
			return GetShoppingCartItemByID(id, null);}

		private ShoppingCartItem GetShoppingCartItemByID(int id, SqlInterface si) {
			ShoppingCartItem shoppingCartItem = null;

			string storedProcName = "efrstore_get_shopping_cart_item_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Shopping_cart_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						shoppingCartItem = LoadShoppingCartItem(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return shoppingCartItem;
		}


		public int InsertShoppingCartItem(ShoppingCartItem shoppingCartItem) {
			return InsertShoppingCartItem(shoppingCartItem, null);}

		private int InsertShoppingCartItem(ShoppingCartItem shoppingCartItem, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_shopping_cart_item";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Shopping_cart_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Scratch_book_id", DbType.Int32, DBValue.ToDBInt32(shoppingCartItem.ScratchBookId)));
				paramCol.Add(new SqlDataParameter("@Carrier_id", DbType.Int16, DBValue.ToDBInt16(shoppingCartItem.CarrierId)));
				paramCol.Add(new SqlDataParameter("@Shipping_option_id", DbType.Int16, DBValue.ToDBInt16(shoppingCartItem.ShippingOptionId)));
				paramCol.Add(new SqlDataParameter("@Quantity", DbType.Int16, DBValue.ToDBInt16(shoppingCartItem.Quantity)));
				paramCol.Add(new SqlDataParameter("@Client_uploaded_img", DbType.String, DBValue.ToDBString(shoppingCartItem.ClientUploadedImg)));
				paramCol.Add(new SqlDataParameter("@Group_name", DbType.String, DBValue.ToDBString(shoppingCartItem.GroupName)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					shoppingCartItem.ShoppingCartId = DBValue.ToInt32(paramCol["@Shopping_cart_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateShoppingCartItem(ShoppingCartItem shoppingCartItem) {
			return UpdateShoppingCartItem(shoppingCartItem, null);}

		private int UpdateShoppingCartItem(ShoppingCartItem shoppingCartItem, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_shopping_cart_item";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Shopping_cart_id", DbType.Int32, DBValue.ToDBInt32(shoppingCartItem.ShoppingCartId)));
				paramCol.Add(new SqlDataParameter("@Scratch_book_id", DbType.Int32, DBValue.ToDBInt32(shoppingCartItem.ScratchBookId)));
				paramCol.Add(new SqlDataParameter("@Carrier_id", DbType.Int16, DBValue.ToDBInt16(shoppingCartItem.CarrierId)));
				paramCol.Add(new SqlDataParameter("@Shipping_option_id", DbType.Int16, DBValue.ToDBInt16(shoppingCartItem.ShippingOptionId)));
				paramCol.Add(new SqlDataParameter("@Quantity", DbType.Int16, DBValue.ToDBInt16(shoppingCartItem.Quantity)));
				paramCol.Add(new SqlDataParameter("@Client_uploaded_img", DbType.String, DBValue.ToDBString(shoppingCartItem.ClientUploadedImg)));
				paramCol.Add(new SqlDataParameter("@Group_name", DbType.String, DBValue.ToDBString(shoppingCartItem.GroupName)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region Story Methods

		private Story LoadStory(DataRow row) {
			Story story = new Story();

			// Store database values into our business object
			story.StoryId = DBValue.ToInt32(row["story_id"]);
			story.StoryTypeId = DBValue.ToInt32(row["story_type_id"]);
			story.GroupTypeId = DBValue.ToInt32(row["group_type_id"]);
			story.StoryText = DBValue.ToString(row["story_text"]);

			// return the filled object
			return story;
		}

		public Story[] GetStorys() {
			return GetStorys(null);}

		private Story[] GetStorys(SqlInterface si) {
			Story[] storys = null;

			string storedProcName = "efrstore_get_storys";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					storys = new Story[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							storys[i] = LoadStory(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return storys;
		}


		public Story GetStoryByID(int id) {
			return GetStoryByID(id, null);}

		private Story GetStoryByID(int id, SqlInterface si) {
			Story story = null;

			string storedProcName = "efrstore_get_story_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Story_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						story = LoadStory(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return story;
		}


		public int InsertStory(Story story) {
			return InsertStory(story, null);}

		private int InsertStory(Story story, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_story";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Story_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Story_type_id", DbType.Int32, DBValue.ToDBInt32(story.StoryTypeId)));
				paramCol.Add(new SqlDataParameter("@Group_type_id", DbType.Int32, DBValue.ToDBInt32(story.GroupTypeId)));
				paramCol.Add(new SqlDataParameter("@Story_text", DbType.String, DBValue.ToDBString(story.StoryText)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					story.StoryId = DBValue.ToInt32(paramCol["@Story_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateStory(Story story) {
			return UpdateStory(story, null);}

		private int UpdateStory(Story story, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_story";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Story_id", DbType.Int32, DBValue.ToDBInt32(story.StoryId)));
				paramCol.Add(new SqlDataParameter("@Story_type_id", DbType.Int32, DBValue.ToDBInt32(story.StoryTypeId)));
				paramCol.Add(new SqlDataParameter("@Group_type_id", DbType.Int32, DBValue.ToDBInt32(story.GroupTypeId)));
				paramCol.Add(new SqlDataParameter("@Story_text", DbType.String, DBValue.ToDBString(story.StoryText)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region StoryType Methods

		private StoryType LoadStoryType(DataRow row) {
			StoryType storyType = new StoryType();

			// Store database values into our business object
			storyType.StoryTypeId = DBValue.ToInt32(row["story_type_id"]);
			storyType.Name = DBValue.ToString(row["name"]);

			// return the filled object
			return storyType;
		}

		public StoryType[] GetStoryTypes() {
			return GetStoryTypes(null);}

		private StoryType[] GetStoryTypes(SqlInterface si) {
			StoryType[] storyTypes = null;

			string storedProcName = "efrstore_get_story_types";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					storyTypes = new StoryType[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							storyTypes[i] = LoadStoryType(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return storyTypes;
		}


		public StoryType GetStoryTypeByID(int id) {
			return GetStoryTypeByID(id, null);}

		private StoryType GetStoryTypeByID(int id, SqlInterface si) {
			StoryType storyType = null;

			string storedProcName = "efrstore_get_story_type_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Story_type_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						storyType = LoadStoryType(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return storyType;
		}


		public int InsertStoryType(StoryType storyType) {
			return InsertStoryType(storyType, null);}

		private int InsertStoryType(StoryType storyType, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_story_type";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Story_type_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(storyType.Name)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					storyType.StoryTypeId = DBValue.ToInt32(paramCol["@Story_type_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateStoryType(StoryType storyType) {
			return UpdateStoryType(storyType, null);}

		private int UpdateStoryType(StoryType storyType, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_story_type";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Story_type_id", DbType.Int32, DBValue.ToDBInt32(storyType.StoryTypeId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(storyType.Name)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region Subdivision Methods

		private Subdivision LoadSubdivision(DataRow row) {
			Subdivision subdivision = new Subdivision();

			// Store database values into our business object
			subdivision.SubdivisionCode = DBValue.ToString(row["subdivision_code"]);
			subdivision.CountryCode = DBValue.ToString(row["country_code"]);
			subdivision.SubdivisionName1 = DBValue.ToString(row["subdivision_name_1"]);
			subdivision.SubdivisionName2 = DBValue.ToString(row["subdivision_name_2"]);
			subdivision.SubdivisionName3 = DBValue.ToString(row["subdivision_name_3"]);
			subdivision.RegionalDivision = DBValue.ToString(row["regional_division"]);
			subdivision.SubdivisionCategory = DBValue.ToString(row["subdivision_category"]);
			subdivision.Display = DBValue.ToInt16(row["display"]);

			// return the filled object
			return subdivision;
		}

		public Subdivision[] GetSubdivisions() {
			return GetSubdivisions(null);}

		private Subdivision[] GetSubdivisions(SqlInterface si) {
			Subdivision[] subdivisions = null;

			string storedProcName = "efrstore_get_subdivisions";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					subdivisions = new Subdivision[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							subdivisions[i] = LoadSubdivision(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return subdivisions;
		}



		#endregion

		#region Supplier Methods

		private Supplier LoadSupplier(DataRow row) {
			Supplier supplier = new Supplier();

			// Store database values into our business object
			supplier.SupplierId = DBValue.ToInt32(row["supplier_id"]);
			supplier.Name = DBValue.ToString(row["name"]);
			supplier.StreetAdress = DBValue.ToString(row["street_adress"]);
			supplier.City = DBValue.ToString(row["city"]);
			supplier.Zip = DBValue.ToString(row["zip"]);
			supplier.ContactName = DBValue.ToString(row["contact_name"]);
			supplier.Phone = DBValue.ToString(row["phone"]);
			supplier.Fax = DBValue.ToString(row["fax"]);
			supplier.AccountNo = DBValue.ToString(row["account_no"]);
			supplier.CreditMargin = DBValue.ToDecimal(row["credit_margin"]);
			supplier.StateCode = DBValue.ToString(row["state_code"]);
			supplier.CountryCode = DBValue.ToString(row["country_code"]);
			supplier.Comment = DBValue.ToString(row["comment"]);

			// return the filled object
			return supplier;
		}

		public Supplier[] GetSuppliers() {
			return GetSuppliers(null);}

		private Supplier[] GetSuppliers(SqlInterface si) {
			Supplier[] suppliers = null;

			string storedProcName = "efrstore_get_suppliers";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					suppliers = new Supplier[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							suppliers[i] = LoadSupplier(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return suppliers;
		}


		public Supplier GetSupplierByID(int id) {
			return GetSupplierByID(id, null);}

		private Supplier GetSupplierByID(int id, SqlInterface si) {
			Supplier supplier = null;

			string storedProcName = "efrstore_get_supplier_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Supplier_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						supplier = LoadSupplier(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return supplier;
		}


		public int InsertSupplier(Supplier supplier) {
			return InsertSupplier(supplier, null);}

		private int InsertSupplier(Supplier supplier, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_supplier";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Supplier_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(supplier.Name)));
				paramCol.Add(new SqlDataParameter("@Street_adress", DbType.String, DBValue.ToDBString(supplier.StreetAdress)));
				paramCol.Add(new SqlDataParameter("@City", DbType.String, DBValue.ToDBString(supplier.City)));
				paramCol.Add(new SqlDataParameter("@Zip", DbType.String, DBValue.ToDBString(supplier.Zip)));
				paramCol.Add(new SqlDataParameter("@Contact_name", DbType.String, DBValue.ToDBString(supplier.ContactName)));
				paramCol.Add(new SqlDataParameter("@Phone", DbType.String, DBValue.ToDBString(supplier.Phone)));
				paramCol.Add(new SqlDataParameter("@Fax", DbType.String, DBValue.ToDBString(supplier.Fax)));
				paramCol.Add(new SqlDataParameter("@Account_no", DbType.String, DBValue.ToDBString(supplier.AccountNo)));
				paramCol.Add(new SqlDataParameter("@Credit_margin", DbType.Decimal, DBValue.ToDBDecimal(supplier.CreditMargin)));
				paramCol.Add(new SqlDataParameter("@State_code", DbType.String, DBValue.ToDBString(supplier.StateCode)));
				paramCol.Add(new SqlDataParameter("@Country_code", DbType.String, DBValue.ToDBString(supplier.CountryCode)));
				paramCol.Add(new SqlDataParameter("@Comment", DbType.String, DBValue.ToDBString(supplier.Comment)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					supplier.SupplierId = DBValue.ToInt32(paramCol["@Supplier_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateSupplier(Supplier supplier) {
			return UpdateSupplier(supplier, null);}

		private int UpdateSupplier(Supplier supplier, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_supplier";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Supplier_id", DbType.Int32, DBValue.ToDBInt32(supplier.SupplierId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(supplier.Name)));
				paramCol.Add(new SqlDataParameter("@Street_adress", DbType.String, DBValue.ToDBString(supplier.StreetAdress)));
				paramCol.Add(new SqlDataParameter("@City", DbType.String, DBValue.ToDBString(supplier.City)));
				paramCol.Add(new SqlDataParameter("@Zip", DbType.String, DBValue.ToDBString(supplier.Zip)));
				paramCol.Add(new SqlDataParameter("@Contact_name", DbType.String, DBValue.ToDBString(supplier.ContactName)));
				paramCol.Add(new SqlDataParameter("@Phone", DbType.String, DBValue.ToDBString(supplier.Phone)));
				paramCol.Add(new SqlDataParameter("@Fax", DbType.String, DBValue.ToDBString(supplier.Fax)));
				paramCol.Add(new SqlDataParameter("@Account_no", DbType.String, DBValue.ToDBString(supplier.AccountNo)));
				paramCol.Add(new SqlDataParameter("@Credit_margin", DbType.Decimal, DBValue.ToDBDecimal(supplier.CreditMargin)));
				paramCol.Add(new SqlDataParameter("@State_code", DbType.String, DBValue.ToDBString(supplier.StateCode)));
				paramCol.Add(new SqlDataParameter("@Country_code", DbType.String, DBValue.ToDBString(supplier.CountryCode)));
				paramCol.Add(new SqlDataParameter("@Comment", DbType.String, DBValue.ToDBString(supplier.Comment)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

        #region Survey Methods

        private Survey LoadSurvey(DataRow row)
        {
            Survey survey = new Survey();

            // Store database values into our business object
            survey.SurveyId = DBValue.ToInt32(row["survey_id"]);
            survey.PageName = DBValue.ToString(row["page_name"]);
            survey.Display = DBValue.ToInt32(row["display"]);
           
            // return the filled object
            return survey;
        }

        public Survey GetSurveyByPageName(String pageName)
        {
            return GetSurveyByPageName(pageName, null);
        }

        private Survey GetSurveyByPageName(String pageName, SqlInterface si)
        {
            Survey survey = null;
            
            string storedProcName = "efrstore_get_survey_by_page_name";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@Page_name", DbType.String, DBValue.ToDBString(pageName)));
                
                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // fill our objects
                    try
                    {
                        survey = LoadSurvey(dt.Rows[0]);
                    }
                    catch (Exception ex)
                    {
                        throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                    }
                }


            } finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return survey;
        }

        public Survey GetSurveyBySurveyID(int surveyId)
        {
            return GetSurveyBySurveyID(surveyId, null);
        }

        private Survey GetSurveyBySurveyID(int surveyId, SqlInterface si)
        {
            Survey survey = null;

            string storedProcName = "efrstore_get_survey_by_survey_id";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@Survey_id", DbType.Int32, DBValue.ToDBInt32(surveyId)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // fill our objects
                    try
                    {
                        survey = LoadSurvey(dt.Rows[0]);
                    }
                    catch (Exception ex)
                    {
                        throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return survey;
        }
        
        #endregion

        #region SurveyChoice Methods

        private Survey_choice LoadSurveyChoice(DataRow row)
        {
            Survey_choice choice = new Survey_choice();

            // Store database values into our business object
            choice.SurveyId = DBValue.ToInt32(row["survey_id"]);
            choice.ChoiceId= DBValue.ToInt32(row["choice_id"]);
            
            // return the filled object
            return choice;
        }

        public Survey_choice[] GetSurveyChoices()
        {
            return GetSurveyChoices(null);
        }

        private Survey_choice[] GetSurveyChoices(SqlInterface si)
        {
            Survey_choice[] surveyChoices = null;

            string storedProcName = "efrstore_get_survey_choices";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();


                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    surveyChoices = new Survey_choice[dt.Rows.Count];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            surveyChoices[i] = LoadSurveyChoice(dt.Rows[i]);
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return surveyChoices;
        }

        public SurveyChoiceCollection GetSurveyChoicesBySurveyID(int surveyId)
        {
            return GetSurveyChoicesBySurveyID(surveyId, null);
        }

        private SurveyChoiceCollection GetSurveyChoicesBySurveyID(int surveyId, SqlInterface si)
        {
            SurveyChoiceCollection surveyChoices = null;

            string storedProcName = "efrstore_get_survey_choices_by_survey_id";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@Survey_id", DbType.Int32, DBValue.ToDBInt32(surveyId)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    surveyChoices = new SurveyChoiceCollection();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            Survey_choice choice = LoadSurveyChoice(dt.Rows[i]);
                            surveyChoices.Add(choice);
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return surveyChoices;
        }

        #endregion

        #region TempLead Methods

        private TempLead LoadTempLead(DataRow row) {
			TempLead tempLead = new TempLead();

			// Store database values into our business object
			tempLead.TempLeadId = DBValue.ToInt32(row["temp_lead_id"]);
			tempLead.DivisionId = DBValue.ToInt32(row["division_id"]);
			tempLead.PromotionId = DBValue.ToInt32(row["promotion_id"]);
			tempLead.ChannelCode = DBValue.ToString(row["channel_code"]);
			tempLead.LeadStatusId = DBValue.ToInt32(row["lead_status_id"]);
			tempLead.OrganizationTypeId = DBValue.ToInt16(row["organization_type_id"]);
			tempLead.CampaignReasonId = DBValue.ToInt16(row["campaign_reason_id"]);
			tempLead.WebSiteId = DBValue.ToInt16(row["web_site_id"]);
			tempLead.GroupTypeId = DBValue.ToInt16(row["group_type_id"]);
			tempLead.Salutation = DBValue.ToString(row["salutation"]);
			tempLead.TitleId = DBValue.ToInt16(row["title_id"]);
			tempLead.HearId = DBValue.ToInt16(row["hear_id"]);
			tempLead.LeadEntryDate = DBValue.ToDateTime(row["lead_entry_date"]);
			tempLead.FirstName = DBValue.ToString(row["first_name"]);
			tempLead.LastName = DBValue.ToString(row["last_name"]);
			tempLead.Organization = DBValue.ToString(row["organization"]);
			tempLead.StreetAddress = DBValue.ToString(row["street_address"]);
			tempLead.City = DBValue.ToString(row["city"]);
			tempLead.StateCode = DBValue.ToString(row["state_code"]);
			tempLead.CountryCode = DBValue.ToString(row["country_code"]);
			tempLead.ZipCode = DBValue.ToString(row["zip_code"]);
			tempLead.DayPhone = DBValue.ToString(row["day_phone"]);
			tempLead.DayTimeCall = DBValue.ToString(row["day_time_call"]);
			tempLead.EveningPhone = DBValue.ToString(row["evening_phone"]);
			tempLead.Fax = DBValue.ToString(row["fax"]);
			tempLead.Email = DBValue.ToString(row["email"]);
			tempLead.ParticipantCount = DBValue.ToInt32(row["participant_count"]);
			tempLead.FundRaisingGoal = DBValue.ToInt32(row["fund_raising_goal"]);
			tempLead.DecisionDate = DBValue.ToDateTime(row["decision_date"]);
			tempLead.DecisionMaker = DBValue.ToInt16(row["decision_maker"]);
			tempLead.FundRaiserStartDate = DBValue.ToDateTime(row["fund_raiser_start_date"]);
			tempLead.Onemaillist = DBValue.ToInt16(row["onemaillist"]);
			tempLead.Comments = DBValue.ToString(row["comments"]);
			tempLead.DayPhoneExt = DBValue.ToString(row["day_phone_ext"]);
			tempLead.EveningPhoneExt = DBValue.ToString(row["evening_phone_ext"]);
			tempLead.OtherPhone = DBValue.ToString(row["other_phone"]);
			tempLead.CookieContent = DBValue.ToString(row["cookie_content"]);
			tempLead.GroupWebSite = DBValue.ToString(row["group_web_site"]);
			tempLead.OtherPhoneExt = DBValue.ToString(row["other_phone_ext"]);
			tempLead.Isnew = DBValue.ToInt16(row["isnew"]);
			tempLead.OptInSweepstakes = DBValue.ToInt16(row["opt_in_sweepstakes"]);

			// return the filled object
			return tempLead;
		}

		public TempLead[] GetTempLeads() {
			return GetTempLeads(null);}

		private TempLead[] GetTempLeads(SqlInterface si) {
			TempLead[] tempLeads = null;

			string storedProcName = "efrstore_get_temp_leads";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					tempLeads = new TempLead[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							tempLeads[i] = LoadTempLead(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return tempLeads;
		}


		public TempLead GetTempLeadByID(int id) {
			return GetTempLeadByID(id, null);}

		private TempLead GetTempLeadByID(int id, SqlInterface si) {
			TempLead tempLead = null;

			string storedProcName = "efrstore_get_temp_lead_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Temp_lead_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						tempLead = LoadTempLead(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return tempLead;
		}


		public int InsertTempLead(TempLead tempLead) {
			return InsertTempLead(tempLead, null);}

		private int InsertTempLead(TempLead tempLead, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_temp_lead";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Temp_lead_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Division_id", DbType.Int32, DBValue.ToDBInt32(tempLead.DivisionId)));
				paramCol.Add(new SqlDataParameter("@Promotion_id", DbType.Int32, DBValue.ToDBInt32(tempLead.PromotionId)));
				paramCol.Add(new SqlDataParameter("@Channel_code", DbType.String, DBValue.ToDBString(tempLead.ChannelCode)));
				paramCol.Add(new SqlDataParameter("@Lead_status_id", DbType.Int32, DBValue.ToDBInt32(tempLead.LeadStatusId)));
				paramCol.Add(new SqlDataParameter("@Organization_type_id", DbType.Int16, DBValue.ToDBInt16(tempLead.OrganizationTypeId)));
				paramCol.Add(new SqlDataParameter("@Campaign_reason_id", DbType.Int16, DBValue.ToDBInt16(tempLead.CampaignReasonId)));
				paramCol.Add(new SqlDataParameter("@Web_site_id", DbType.Int16, DBValue.ToDBInt16(tempLead.WebSiteId)));
				paramCol.Add(new SqlDataParameter("@Group_type_id", DbType.Int16, DBValue.ToDBInt16(tempLead.GroupTypeId)));
				paramCol.Add(new SqlDataParameter("@Salutation", DbType.String, DBValue.ToDBString(tempLead.Salutation)));
				paramCol.Add(new SqlDataParameter("@Title_id", DbType.Int16, DBValue.ToDBInt16(tempLead.TitleId)));
				paramCol.Add(new SqlDataParameter("@Hear_id", DbType.Int16, DBValue.ToDBInt16(tempLead.HearId)));
				paramCol.Add(new SqlDataParameter("@Lead_entry_date", DbType.DateTime, DBValue.ToDBDateTime(tempLead.LeadEntryDate)));
				paramCol.Add(new SqlDataParameter("@First_name", DbType.String, DBValue.ToDBString(tempLead.FirstName)));
				paramCol.Add(new SqlDataParameter("@Last_name", DbType.String, DBValue.ToDBString(tempLead.LastName)));
				paramCol.Add(new SqlDataParameter("@Organization", DbType.String, DBValue.ToDBString(tempLead.Organization)));
				paramCol.Add(new SqlDataParameter("@Street_address", DbType.String, DBValue.ToDBString(tempLead.StreetAddress)));
				paramCol.Add(new SqlDataParameter("@City", DbType.String, DBValue.ToDBString(tempLead.City)));
				paramCol.Add(new SqlDataParameter("@State_code", DbType.String, DBValue.ToDBString(tempLead.StateCode)));
				paramCol.Add(new SqlDataParameter("@Country_code", DbType.String, DBValue.ToDBString(tempLead.CountryCode)));
				paramCol.Add(new SqlDataParameter("@Zip_code", DbType.String, DBValue.ToDBString(tempLead.ZipCode)));
				paramCol.Add(new SqlDataParameter("@Day_phone", DbType.String, DBValue.ToDBString(tempLead.DayPhone)));
				paramCol.Add(new SqlDataParameter("@Day_time_call", DbType.String, DBValue.ToDBString(tempLead.DayTimeCall)));
				paramCol.Add(new SqlDataParameter("@Evening_phone", DbType.String, DBValue.ToDBString(tempLead.EveningPhone)));
				paramCol.Add(new SqlDataParameter("@Fax", DbType.String, DBValue.ToDBString(tempLead.Fax)));
				paramCol.Add(new SqlDataParameter("@Email", DbType.String, DBValue.ToDBString(tempLead.Email)));
				paramCol.Add(new SqlDataParameter("@Participant_count", DbType.Int32, DBValue.ToDBInt32(tempLead.ParticipantCount)));
				paramCol.Add(new SqlDataParameter("@Fund_raising_goal", DbType.Int32, DBValue.ToDBInt32(tempLead.FundRaisingGoal)));
				paramCol.Add(new SqlDataParameter("@Decision_date", DbType.DateTime, DBValue.ToDBDateTime(tempLead.DecisionDate)));
				paramCol.Add(new SqlDataParameter("@Decision_maker", DbType.Int16, DBValue.ToDBInt16(tempLead.DecisionMaker)));
				paramCol.Add(new SqlDataParameter("@Fund_raiser_start_date", DbType.DateTime, DBValue.ToDBDateTime(tempLead.FundRaiserStartDate)));
				paramCol.Add(new SqlDataParameter("@Onemaillist", DbType.Int16, DBValue.ToDBInt16(tempLead.Onemaillist)));
				paramCol.Add(new SqlDataParameter("@Comments", DbType.String, DBValue.ToDBString(tempLead.Comments)));
				paramCol.Add(new SqlDataParameter("@Day_phone_ext", DbType.String, DBValue.ToDBString(tempLead.DayPhoneExt)));
				paramCol.Add(new SqlDataParameter("@Evening_phone_ext", DbType.String, DBValue.ToDBString(tempLead.EveningPhoneExt)));
				paramCol.Add(new SqlDataParameter("@Other_phone", DbType.String, DBValue.ToDBString(tempLead.OtherPhone)));
				paramCol.Add(new SqlDataParameter("@Cookie_content", DbType.String, DBValue.ToDBString(tempLead.CookieContent)));
				paramCol.Add(new SqlDataParameter("@Group_web_site", DbType.String, DBValue.ToDBString(tempLead.GroupWebSite)));
				paramCol.Add(new SqlDataParameter("@Other_phone_ext", DbType.String, DBValue.ToDBString(tempLead.OtherPhoneExt)));
				paramCol.Add(new SqlDataParameter("@Isnew", DbType.Int16, DBValue.ToDBInt16(tempLead.Isnew)));
				paramCol.Add(new SqlDataParameter("@Opt_in_sweepstakes", DbType.Int16, DBValue.ToDBInt16(tempLead.OptInSweepstakes)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					tempLead.TempLeadId = DBValue.ToInt32(paramCol["@Temp_lead_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateTempLead(TempLead tempLead) {
			return UpdateTempLead(tempLead, null);}

		private int UpdateTempLead(TempLead tempLead, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_temp_lead";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Temp_lead_id", DbType.Int32, DBValue.ToDBInt32(tempLead.TempLeadId)));
				paramCol.Add(new SqlDataParameter("@Division_id", DbType.Int32, DBValue.ToDBInt32(tempLead.DivisionId)));
				paramCol.Add(new SqlDataParameter("@Promotion_id", DbType.Int32, DBValue.ToDBInt32(tempLead.PromotionId)));
				paramCol.Add(new SqlDataParameter("@Channel_code", DbType.String, DBValue.ToDBString(tempLead.ChannelCode)));
				paramCol.Add(new SqlDataParameter("@Lead_status_id", DbType.Int32, DBValue.ToDBInt32(tempLead.LeadStatusId)));
				paramCol.Add(new SqlDataParameter("@Organization_type_id", DbType.Int16, DBValue.ToDBInt16(tempLead.OrganizationTypeId)));
				paramCol.Add(new SqlDataParameter("@Campaign_reason_id", DbType.Int16, DBValue.ToDBInt16(tempLead.CampaignReasonId)));
				paramCol.Add(new SqlDataParameter("@Web_site_id", DbType.Int16, DBValue.ToDBInt16(tempLead.WebSiteId)));
				paramCol.Add(new SqlDataParameter("@Group_type_id", DbType.Int16, DBValue.ToDBInt16(tempLead.GroupTypeId)));
				paramCol.Add(new SqlDataParameter("@Salutation", DbType.String, DBValue.ToDBString(tempLead.Salutation)));
				paramCol.Add(new SqlDataParameter("@Title_id", DbType.Int16, DBValue.ToDBInt16(tempLead.TitleId)));
				paramCol.Add(new SqlDataParameter("@Hear_id", DbType.Int16, DBValue.ToDBInt16(tempLead.HearId)));
				paramCol.Add(new SqlDataParameter("@Lead_entry_date", DbType.DateTime, DBValue.ToDBDateTime(tempLead.LeadEntryDate)));
				paramCol.Add(new SqlDataParameter("@First_name", DbType.String, DBValue.ToDBString(tempLead.FirstName)));
				paramCol.Add(new SqlDataParameter("@Last_name", DbType.String, DBValue.ToDBString(tempLead.LastName)));
				paramCol.Add(new SqlDataParameter("@Organization", DbType.String, DBValue.ToDBString(tempLead.Organization)));
				paramCol.Add(new SqlDataParameter("@Street_address", DbType.String, DBValue.ToDBString(tempLead.StreetAddress)));
				paramCol.Add(new SqlDataParameter("@City", DbType.String, DBValue.ToDBString(tempLead.City)));
				paramCol.Add(new SqlDataParameter("@State_code", DbType.String, DBValue.ToDBString(tempLead.StateCode)));
				paramCol.Add(new SqlDataParameter("@Country_code", DbType.String, DBValue.ToDBString(tempLead.CountryCode)));
				paramCol.Add(new SqlDataParameter("@Zip_code", DbType.String, DBValue.ToDBString(tempLead.ZipCode)));
				paramCol.Add(new SqlDataParameter("@Day_phone", DbType.String, DBValue.ToDBString(tempLead.DayPhone)));
				paramCol.Add(new SqlDataParameter("@Day_time_call", DbType.String, DBValue.ToDBString(tempLead.DayTimeCall)));
				paramCol.Add(new SqlDataParameter("@Evening_phone", DbType.String, DBValue.ToDBString(tempLead.EveningPhone)));
				paramCol.Add(new SqlDataParameter("@Fax", DbType.String, DBValue.ToDBString(tempLead.Fax)));
				paramCol.Add(new SqlDataParameter("@Email", DbType.String, DBValue.ToDBString(tempLead.Email)));
				paramCol.Add(new SqlDataParameter("@Participant_count", DbType.Int32, DBValue.ToDBInt32(tempLead.ParticipantCount)));
				paramCol.Add(new SqlDataParameter("@Fund_raising_goal", DbType.Int32, DBValue.ToDBInt32(tempLead.FundRaisingGoal)));
				paramCol.Add(new SqlDataParameter("@Decision_date", DbType.DateTime, DBValue.ToDBDateTime(tempLead.DecisionDate)));
				paramCol.Add(new SqlDataParameter("@Decision_maker", DbType.Int16, DBValue.ToDBInt16(tempLead.DecisionMaker)));
				paramCol.Add(new SqlDataParameter("@Fund_raiser_start_date", DbType.DateTime, DBValue.ToDBDateTime(tempLead.FundRaiserStartDate)));
				paramCol.Add(new SqlDataParameter("@Onemaillist", DbType.Int16, DBValue.ToDBInt16(tempLead.Onemaillist)));
				paramCol.Add(new SqlDataParameter("@Comments", DbType.String, DBValue.ToDBString(tempLead.Comments)));
				paramCol.Add(new SqlDataParameter("@Day_phone_ext", DbType.String, DBValue.ToDBString(tempLead.DayPhoneExt)));
				paramCol.Add(new SqlDataParameter("@Evening_phone_ext", DbType.String, DBValue.ToDBString(tempLead.EveningPhoneExt)));
				paramCol.Add(new SqlDataParameter("@Other_phone", DbType.String, DBValue.ToDBString(tempLead.OtherPhone)));
				paramCol.Add(new SqlDataParameter("@Cookie_content", DbType.String, DBValue.ToDBString(tempLead.CookieContent)));
				paramCol.Add(new SqlDataParameter("@Group_web_site", DbType.String, DBValue.ToDBString(tempLead.GroupWebSite)));
				paramCol.Add(new SqlDataParameter("@Other_phone_ext", DbType.String, DBValue.ToDBString(tempLead.OtherPhoneExt)));
				paramCol.Add(new SqlDataParameter("@Isnew", DbType.Int16, DBValue.ToDBInt16(tempLead.Isnew)));
				paramCol.Add(new SqlDataParameter("@Opt_in_sweepstakes", DbType.Int16, DBValue.ToDBInt16(tempLead.OptInSweepstakes)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region Template Methods

		private Template LoadTemplate(DataRow row) {
			Template template = new Template();

			// Store database values into our business object
			template.TemplateId = DBValue.ToInt32(row["template_id"]);
			template.Name = DBValue.ToString(row["name"]);
			template.Path = DBValue.ToString(row["path"]);
			template.CreateDate = DBValue.ToDateTime(row["create_date"]);

			// return the filled object
			return template;
		}

		public Template[] GetTemplates() {
			return GetTemplates(null);}

		private Template[] GetTemplates(SqlInterface si) {
			Template[] templates = null;

			string storedProcName = "efrstore_get_templates";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					templates = new Template[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							templates[i] = LoadTemplate(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return templates;
		}


		public Template GetTemplateByID(int id) {
			return GetTemplateByID(id, null);}

		private Template GetTemplateByID(int id, SqlInterface si) {
			Template template = null;

			string storedProcName = "efrstore_get_template_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Template_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						template = LoadTemplate(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return template;
		}
		public Template GetTemplateByProductPageName(string pageName)
		{
			return GetTemplateByProductPageName(pageName, null);
		}
		private Template GetTemplateByProductPageName(string pageName, SqlInterface si)
		{
			Template template = null;

			string storedProcName = "efrstore_get_template_by_product_page_name";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@page_name", DbType.String, DBValue.ToString(pageName)));
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) 
				{
					// fill our objects
					try 
					{
						template = LoadTemplate(dt.Rows[0]);
					} 
					catch(Exception ex) 
					{
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} 
			finally 
			{
				
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return template;
		}
		public Template GetTemplateByPackagePageNameAndRootPackageID(string pageName, int rootID)
		{
			return GetTemplateByPackagePageNameAndRootPackageID(pageName, rootID, null);
		}
		private Template GetTemplateByPackagePageNameAndRootPackageID(string pageName, int rootID, SqlInterface si)
		{
			Template template = null;

			string storedProcName = "efrstore_get_template_by_package_page_name_and_root_package_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@page_name", DbType.String, DBValue.ToString(pageName)));
				paramCol.Add(new SqlDataParameter("@root_package_id",DbType.Int32, DBValue.ToInt32(rootID)));
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) 
				{
					// fill our objects
					try 
					{
						template = LoadTemplate(dt.Rows[0]);
					} 
					catch(Exception ex) 
					{
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return template;
		}
		public int InsertTemplate(Template template) {
			return InsertTemplate(template, null);}

		private int InsertTemplate(Template template, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_template";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Template_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(template.Name)));
				paramCol.Add(new SqlDataParameter("@Path", DbType.String, DBValue.ToDBString(template.Path)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(template.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					template.TemplateId = DBValue.ToInt32(paramCol["@Template_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateTemplate(Template template) {
			return UpdateTemplate(template, null);}

		private int UpdateTemplate(Template template, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_template";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Template_id", DbType.Int32, DBValue.ToDBInt32(template.TemplateId)));
				paramCol.Add(new SqlDataParameter("@Name", DbType.String, DBValue.ToDBString(template.Name)));
				paramCol.Add(new SqlDataParameter("@Path", DbType.String, DBValue.ToDBString(template.Path)));
				paramCol.Add(new SqlDataParameter("@Create_date", DbType.DateTime, DBValue.ToDBDateTime(template.CreateDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region Title Methods

		private Title LoadTitle(DataRow row) {
			Title title = new Title();

			// Store database values into our business object
			title.TitleId = DBValue.ToInt16(row["title_id"]);
			title.PartyTypeId = DBValue.ToInt16(row["party_type_id"]);
			title.TitleDesc = DBValue.ToString(row["title_desc"]);

			// return the filled object
			return title;
		}

		public Title[] GetTitles() {
			return GetTitles(null);}

		private Title[] GetTitles(SqlInterface si) {
			Title[] titles = null;

			string storedProcName = "efrstore_get_titles";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					titles = new Title[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							titles[i] = LoadTitle(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return titles;
		}


		public Title GetTitleByID(int id) {
			return GetTitleByID(id, null);}

		private Title GetTitleByID(int id, SqlInterface si) {
			Title title = null;

			string storedProcName = "efrstore_get_title_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Title_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						title = LoadTitle(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return title;
		}


		public int InsertTitle(Title title) {
			return InsertTitle(title, null);}

		private int InsertTitle(Title title, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_title";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Title_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Party_type_id", DbType.Int16, DBValue.ToDBInt16(title.PartyTypeId)));
				paramCol.Add(new SqlDataParameter("@Title_desc", DbType.String, DBValue.ToDBString(title.TitleDesc)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					title.TitleId = DBValue.ToInt16(paramCol["@Title_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateTitle(Title title) {
			return UpdateTitle(title, null);}

		private int UpdateTitle(Title title, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_title";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Title_id", DbType.Int16, DBValue.ToDBInt16(title.TitleId)));
				paramCol.Add(new SqlDataParameter("@Party_type_id", DbType.Int16, DBValue.ToDBInt16(title.PartyTypeId)));
				paramCol.Add(new SqlDataParameter("@Title_desc", DbType.String, DBValue.ToDBString(title.TitleDesc)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region TitleDesc Methods

		private TitleDesc LoadTitleDesc(DataRow row) {
			TitleDesc titleDesc = new TitleDesc();

			// Store database values into our business object
			titleDesc.TitleId = DBValue.ToInt16(row["title_id"]);
			titleDesc.CultureCode = DBValue.ToString(row["culture_code"]);
			titleDesc.Description = DBValue.ToString(row["description"]);

			// return the filled object
			return titleDesc;
		}

		public TitleDesc[] GetTitleDescs() {
			return GetTitleDescs(null);}

		private TitleDesc[] GetTitleDescs(SqlInterface si) {
			TitleDesc[] titleDescs = null;

			string storedProcName = "efrstore_get_title_descs";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					titleDescs = new TitleDesc[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							titleDescs[i] = LoadTitleDesc(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return titleDescs;
		}


		public TitleDesc GetTitleDescByID(int id) {
			return GetTitleDescByID(id, null);}

		private TitleDesc GetTitleDescByID(int id, SqlInterface si) {
			TitleDesc titleDesc = null;

			string storedProcName = "efrstore_get_title_desc_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Title_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						titleDesc = LoadTitleDesc(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return titleDesc;
		}


		public int InsertTitleDesc(TitleDesc titleDesc) {
			return InsertTitleDesc(titleDesc, null);}

		private int InsertTitleDesc(TitleDesc titleDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_title_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Title_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(titleDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(titleDesc.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					titleDesc.TitleId = DBValue.ToInt16(paramCol["@Title_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateTitleDesc(TitleDesc titleDesc) {
			return UpdateTitleDesc(titleDesc, null);}

		private int UpdateTitleDesc(TitleDesc titleDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_title_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Title_id", DbType.Int16, DBValue.ToDBInt16(titleDesc.TitleId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(titleDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(titleDesc.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region Unsubscribe Methods

		private Unsubscribe LoadUnsubscribe(DataRow row) {
			Unsubscribe unsubscribe = new Unsubscribe();

			// Store database values into our business object
			unsubscribe.UnsubscribeId = DBValue.ToInt32(row["unsubscribe_id"]);
			unsubscribe.Email = DBValue.ToString(row["email"]);
			unsubscribe.Unsubscribed = DBValue.ToInt16(row["unsubscribed"]);
			unsubscribe.UnsubscribedDate = DBValue.ToDateTime(row["unsubscribed_date"]);

			// return the filled object
			return unsubscribe;
		}

		public Unsubscribe[] GetUnsubscribes() {
			return GetUnsubscribes(null);}

		private Unsubscribe[] GetUnsubscribes(SqlInterface si) {
			Unsubscribe[] unsubscribes = null;

			string storedProcName = "efrstore_get_unsubscribes";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					unsubscribes = new Unsubscribe[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							unsubscribes[i] = LoadUnsubscribe(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return unsubscribes;
		}


		public Unsubscribe GetUnsubscribeByID(int id) {
			return GetUnsubscribeByID(id, null);}

		private Unsubscribe GetUnsubscribeByID(int id, SqlInterface si) {
			Unsubscribe unsubscribe = null;

			string storedProcName = "efrstore_get_unsubscribe_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Unsubscribe_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						unsubscribe = LoadUnsubscribe(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return unsubscribe;
		}


		public int InsertUnsubscribe(Unsubscribe unsubscribe) {
			return InsertUnsubscribe(unsubscribe, null);}

		private int InsertUnsubscribe(Unsubscribe unsubscribe, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_unsubscribe";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Unsubscribe_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Email", DbType.String, DBValue.ToDBString(unsubscribe.Email)));
				paramCol.Add(new SqlDataParameter("@Unsubscribed", DbType.Int16, DBValue.ToDBInt16(unsubscribe.Unsubscribed)));
				paramCol.Add(new SqlDataParameter("@Unsubscribed_date", DbType.DateTime, DBValue.ToDBDateTime(unsubscribe.UnsubscribedDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					unsubscribe.UnsubscribeId = DBValue.ToInt32(paramCol["@Unsubscribe_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateUnsubscribe(Unsubscribe unsubscribe) {
			return UpdateUnsubscribe(unsubscribe, null);}

		private int UpdateUnsubscribe(Unsubscribe unsubscribe, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_unsubscribe";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Unsubscribe_id", DbType.Int32, DBValue.ToDBInt32(unsubscribe.UnsubscribeId)));
				paramCol.Add(new SqlDataParameter("@Email", DbType.String, DBValue.ToDBString(unsubscribe.Email)));
				paramCol.Add(new SqlDataParameter("@Unsubscribed", DbType.Int16, DBValue.ToDBInt16(unsubscribe.Unsubscribed)));
				paramCol.Add(new SqlDataParameter("@Unsubscribed_date", DbType.DateTime, DBValue.ToDBDateTime(unsubscribe.UnsubscribedDate)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

        #region UserVote

        private User_vote LoadUserVote(DataRow row)
        {
            User_vote user_vote = new User_vote();

            // Store database values into our business object
            user_vote.SessionId = DBValue.ToString(row["session_id"]);
            user_vote.ChoiceId = DBValue.ToInt32(row["choice_id"]);
            user_vote.SurveyId = DBValue.ToInt32(row["survey_id"]);

            // return the filled object
            return user_vote;
        }

        public User_vote[] GetUserVotes()
        {
            return GetUserVotes(null);
        }

        private User_vote[] GetUserVotes(SqlInterface si)
        {
            User_vote[] userVotes = null;

            string storedProcName = "efrstore_get_user_votes";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();


                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    userVotes = new User_vote[dt.Rows.Count];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            userVotes[i] = LoadUserVote(dt.Rows[i]);
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return userVotes;
        }

        public UserVoteCollection GetUserVotesBySessionID(string sessionId)
        {
            return GetUserVotesBySessionID(sessionId, null);
        }

        private UserVoteCollection GetUserVotesBySessionID(string sessionId, SqlInterface si)
        {
            UserVoteCollection userVotes = null;

            string storedProcName = "efrstore_get_user_votes_by_session_id";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@Session_id", DbType.String, DBValue.ToDBString(sessionId)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    userVotes = new UserVoteCollection();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            if (dt.Rows.Count > 0)
                            {
                                User_vote userVote = LoadUserVote(dt.Rows[i]);
                                userVotes.Add(userVote);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return userVotes;
        }

        public UserVoteCollection GetUserVotesByChoiceID(int choiceId)
        {
            return GetUserVotesByChoiceID(choiceId, null);
        }

        private UserVoteCollection GetUserVotesByChoiceID(int choiceId, SqlInterface si)
        {
            UserVoteCollection userVotes = null;

            string storedProcName = "efrstore_get_user_votes_by_choice_id";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@Choice_id", DbType.Int32, DBValue.ToInt32(choiceId)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    userVotes = new UserVoteCollection();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            User_vote userVote = LoadUserVote(dt.Rows[i]);
                            userVotes.Add(userVote);
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return userVotes;
        }

        public UserVoteCollection GetUserVotesBySurveyID(int surveyID)
        {
            return GetUserVotesBySurveyID(surveyID, null);
        }

        private UserVoteCollection GetUserVotesBySurveyID(int surveyID, SqlInterface si)
        {
            UserVoteCollection userVotes = null;

            string storedProcName = "efrstore_get_user_votes_by_survey_id";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@Survey_id", DbType.Int32, DBValue.ToInt32(surveyID)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    userVotes = new UserVoteCollection();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            User_vote userVote = LoadUserVote(dt.Rows[i]);
                            userVotes.Add(userVote);
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return userVotes;
        }

        public UserVoteCollection GetUserVotesByChoiceIDAndSurveyID(int choiceID,int surveyID)
        {
            return GetUserVotesByChoiceIDAndSurveyID(choiceID,surveyID, null);
        }

        private UserVoteCollection GetUserVotesByChoiceIDAndSurveyID(int choiceID, int surveyID, SqlInterface si)
        {
            UserVoteCollection userVotes = null;

            string storedProcName = "efrstore_get_user_votes_by_choice_id_survey_id";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@Choice_id", DbType.Int32, DBValue.ToInt32(choiceID)));
                paramCol.Add(new SqlDataParameter("@Survey_id", DbType.Int32, DBValue.ToInt32(surveyID)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    userVotes = new UserVoteCollection();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            User_vote userVote = LoadUserVote(dt.Rows[i]);
                            userVotes.Add(userVote);
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return userVotes;
        }

        public int InsertUserVote(User_vote vote)
        {
            return InsertUserVote(vote, null);
        }

        private int InsertUserVote(User_vote vote, SqlInterface si)
        {
            int result = int.MinValue;

            string storedProcName = "efrstore_insert_user_vote";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@Session_id", DbType.String, DBValue.ToDBString(vote.SessionId)));
                paramCol.Add(new SqlDataParameter("@Choice_id", DbType.Int32, DBValue.ToDBInt32(vote.ChoiceId)));
                paramCol.Add(new SqlDataParameter("@Survey_id", DbType.Int32, DBValue.ToDBInt32(vote.SurveyId)));
                
                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

                if (result < 1)
                {
                    throw new SqlDataException("Error inserting into database calling " + storedProcName);
                }


            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return result;
        }


        #endregion

        #region WebForm Methods

        private WebForm LoadWebForm(DataRow row) {
			WebForm webForm = new WebForm();

			// Store database values into our business object
			webForm.WebFormId = DBValue.ToInt32(row["web_form_id"]);
			webForm.WebFormDesc = DBValue.ToString(row["web_form_desc"]);
			webForm.WebFormTypeId = DBValue.ToInt32(row["web_form_type_id"]);
			webForm.LeadStatusId = DBValue.ToInt32(row["lead_status_id"]);
			webForm.StoredProcToCall = DBValue.ToString(row["stored_proc_to_call"]);
			webForm.Datestamp = DBValue.ToDateTime(row["datestamp"]);

			// return the filled object
			return webForm;
		}

		public WebForm[] GetWebForms() {
			return GetWebForms(null);}

		private WebForm[] GetWebForms(SqlInterface si) {
			WebForm[] webForms = null;

			string storedProcName = "efrstore_get_web_forms";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					webForms = new WebForm[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							webForms[i] = LoadWebForm(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return webForms;
		}


		public WebForm GetWebFormByID(int id) {
			return GetWebFormByID(id, null);}

		private WebForm GetWebFormByID(int id, SqlInterface si) {
			WebForm webForm = null;

			string storedProcName = "efrstore_get_web_form_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Web_form_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						webForm = LoadWebForm(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return webForm;
		}


		public int InsertWebForm(WebForm webForm) {
			return InsertWebForm(webForm, null);}

		private int InsertWebForm(WebForm webForm, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_web_form";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Web_form_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Web_form_desc", DbType.String, DBValue.ToDBString(webForm.WebFormDesc)));
				paramCol.Add(new SqlDataParameter("@Web_form_type_id", DbType.Int32, DBValue.ToDBInt32(webForm.WebFormTypeId)));
				paramCol.Add(new SqlDataParameter("@Lead_status_id", DbType.Int32, DBValue.ToDBInt32(webForm.LeadStatusId)));
				paramCol.Add(new SqlDataParameter("@Stored_proc_to_call", DbType.String, DBValue.ToDBString(webForm.StoredProcToCall)));
				paramCol.Add(new SqlDataParameter("@Datestamp", DbType.DateTime, DBValue.ToDBDateTime(webForm.Datestamp)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					webForm.WebFormId = DBValue.ToInt32(paramCol["@Web_form_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateWebForm(WebForm webForm) {
			return UpdateWebForm(webForm, null);}

		private int UpdateWebForm(WebForm webForm, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_web_form";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Web_form_id", DbType.Int32, DBValue.ToDBInt32(webForm.WebFormId)));
				paramCol.Add(new SqlDataParameter("@Web_form_desc", DbType.String, DBValue.ToDBString(webForm.WebFormDesc)));
				paramCol.Add(new SqlDataParameter("@Web_form_type_id", DbType.Int32, DBValue.ToDBInt32(webForm.WebFormTypeId)));
				paramCol.Add(new SqlDataParameter("@Lead_status_id", DbType.Int32, DBValue.ToDBInt32(webForm.LeadStatusId)));
				paramCol.Add(new SqlDataParameter("@Stored_proc_to_call", DbType.String, DBValue.ToDBString(webForm.StoredProcToCall)));
				paramCol.Add(new SqlDataParameter("@Datestamp", DbType.DateTime, DBValue.ToDBDateTime(webForm.Datestamp)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region WebFormQuestion Methods

		private WebFormQuestion LoadWebFormQuestion(DataRow row) {
			WebFormQuestion webFormQuestion = new WebFormQuestion();

			// Store database values into our business object
			webFormQuestion.QuestionId = DBValue.ToInt32(row["question_id"]);
			webFormQuestion.WebFormId = DBValue.ToInt32(row["web_form_id"]);
			webFormQuestion.Required = DBValue.ToInt16(row["required"]);
			webFormQuestion.QuestionOrder = DBValue.ToInt32(row["question_order"]);
			webFormQuestion.Datestamp = DBValue.ToDateTime(row["datestamp"]);

			// return the filled object
			return webFormQuestion;
		}

		public WebFormQuestion[] GetWebFormQuestions() {
			return GetWebFormQuestions(null);}

		private WebFormQuestion[] GetWebFormQuestions(SqlInterface si) {
			WebFormQuestion[] webFormQuestions = null;

			string storedProcName = "efrstore_get_web_form_questions";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					webFormQuestions = new WebFormQuestion[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							webFormQuestions[i] = LoadWebFormQuestion(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return webFormQuestions;
		}


		public WebFormQuestion GetWebFormQuestionByID(int id) {
			return GetWebFormQuestionByID(id, null);}

		private WebFormQuestion GetWebFormQuestionByID(int id, SqlInterface si) {
			WebFormQuestion webFormQuestion = null;

			string storedProcName = "efrstore_get_web_form_question_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Question_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						webFormQuestion = LoadWebFormQuestion(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return webFormQuestion;
		}


		public int InsertWebFormQuestion(WebFormQuestion webFormQuestion) {
			return InsertWebFormQuestion(webFormQuestion, null);}

		private int InsertWebFormQuestion(WebFormQuestion webFormQuestion, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_web_form_question";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Question_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Web_form_id", DbType.Int32, DBValue.ToDBInt32(webFormQuestion.WebFormId)));
				paramCol.Add(new SqlDataParameter("@Required", DbType.Int16, DBValue.ToDBInt16(webFormQuestion.Required)));
				paramCol.Add(new SqlDataParameter("@Question_order", DbType.Int32, DBValue.ToDBInt32(webFormQuestion.QuestionOrder)));
				paramCol.Add(new SqlDataParameter("@Datestamp", DbType.DateTime, DBValue.ToDBDateTime(webFormQuestion.Datestamp)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					webFormQuestion.QuestionId = DBValue.ToInt32(paramCol["@Question_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateWebFormQuestion(WebFormQuestion webFormQuestion) {
			return UpdateWebFormQuestion(webFormQuestion, null);}

		private int UpdateWebFormQuestion(WebFormQuestion webFormQuestion, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_web_form_question";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Question_id", DbType.Int32, DBValue.ToDBInt32(webFormQuestion.QuestionId)));
				paramCol.Add(new SqlDataParameter("@Web_form_id", DbType.Int32, DBValue.ToDBInt32(webFormQuestion.WebFormId)));
				paramCol.Add(new SqlDataParameter("@Required", DbType.Int16, DBValue.ToDBInt16(webFormQuestion.Required)));
				paramCol.Add(new SqlDataParameter("@Question_order", DbType.Int32, DBValue.ToDBInt32(webFormQuestion.QuestionOrder)));
				paramCol.Add(new SqlDataParameter("@Datestamp", DbType.DateTime, DBValue.ToDBDateTime(webFormQuestion.Datestamp)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region WebFormTypeDesc Methods

		private WebFormTypeDesc LoadWebFormTypeDesc(DataRow row) {
			WebFormTypeDesc webFormTypeDesc = new WebFormTypeDesc();

			// Store database values into our business object
			webFormTypeDesc.WebFormTypeId = DBValue.ToInt32(row["web_form_type_id"]);
			webFormTypeDesc.CultureCode = DBValue.ToString(row["culture_code"]);
			webFormTypeDesc.Description = DBValue.ToString(row["description"]);

			// return the filled object
			return webFormTypeDesc;
		}

		public WebFormTypeDesc[] GetWebFormTypeDescs() {
			return GetWebFormTypeDescs(null);}

		private WebFormTypeDesc[] GetWebFormTypeDescs(SqlInterface si) {
			WebFormTypeDesc[] webFormTypeDescs = null;

			string storedProcName = "efrstore_get_web_form_type_descs";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					webFormTypeDescs = new WebFormTypeDesc[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							webFormTypeDescs[i] = LoadWebFormTypeDesc(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return webFormTypeDescs;
		}


		public WebFormTypeDesc GetWebFormTypeDescByID(int id) {
			return GetWebFormTypeDescByID(id, null);}

		private WebFormTypeDesc GetWebFormTypeDescByID(int id, SqlInterface si) {
			WebFormTypeDesc webFormTypeDesc = null;

			string storedProcName = "efrstore_get_web_form_type_desc_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Web_form_type_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						webFormTypeDesc = LoadWebFormTypeDesc(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return webFormTypeDesc;
		}


		public int InsertWebFormTypeDesc(WebFormTypeDesc webFormTypeDesc) {
			return InsertWebFormTypeDesc(webFormTypeDesc, null);}

		private int InsertWebFormTypeDesc(WebFormTypeDesc webFormTypeDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_web_form_type_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Web_form_type_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(webFormTypeDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(webFormTypeDesc.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					webFormTypeDesc.WebFormTypeId = DBValue.ToInt32(paramCol["@Web_form_type_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateWebFormTypeDesc(WebFormTypeDesc webFormTypeDesc) {
			return UpdateWebFormTypeDesc(webFormTypeDesc, null);}

		private int UpdateWebFormTypeDesc(WebFormTypeDesc webFormTypeDesc, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_web_form_type_desc";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Web_form_type_id", DbType.Int32, DBValue.ToDBInt32(webFormTypeDesc.WebFormTypeId)));
				paramCol.Add(new SqlDataParameter("@Culture_code", DbType.String, DBValue.ToDBString(webFormTypeDesc.CultureCode)));
				paramCol.Add(new SqlDataParameter("@Description", DbType.String, DBValue.ToDBString(webFormTypeDesc.Description)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion

		#region Website Methods

		private Website LoadWebsite(DataRow row) {
			Website website = new Website();

			// Store database values into our business object
			website.WebsiteId = DBValue.ToInt16(row["website_id"]);
			website.PartnerId = DBValue.ToInt32(row["partner_id"]);
			website.WebprojectId = DBValue.ToInt16(row["webproject_id"]);
			website.WebsiteDns = DBValue.ToString(row["website_dns"]);

			// return the filled object
			return website;
		}

		public Website[] GetWebsites() {
			return GetWebsites(null);}

		private Website[] GetWebsites(SqlInterface si) {
			Website[] websites = null;

			string storedProcName = "efrstore_get_websites";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) {
					websites = new Website[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++)	{
						// fill our objects
						try {
							websites[i] = LoadWebsite(dt.Rows[i]);
						} catch(Exception ex) {
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return websites;
		}


		public Website GetWebsiteByID(int id) {
			return GetWebsiteByID(id, null);}

		private Website GetWebsiteByID(int id, SqlInterface si) {
			Website website = null;

			string storedProcName = "efrstore_get_website_by_id";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Website_id", DbType.Int32, DBValue.ToDBInt32(id)));
		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					// fill our objects
					try {
						website = LoadWebsite(dt.Rows[0]);
					} catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return website;
		}


		public int InsertWebsite(Website website) {
			return InsertWebsite(website, null);}

		private int InsertWebsite(Website website, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_insert_website";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Website_id", DbType.Int16, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(website.PartnerId)));
				paramCol.Add(new SqlDataParameter("@Webproject_id", DbType.Int16, DBValue.ToDBInt16(website.WebprojectId)));
				paramCol.Add(new SqlDataParameter("@Website_dns", DbType.String, DBValue.ToDBString(website.WebsiteDns)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					// Get generated id
					website.WebsiteId = DBValue.ToInt16(paramCol["@Website_id"].Value);

				} else {
					throw new SqlDataException("Error inserting into database calling " + storedProcName);
				}	


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}


		public int UpdateWebsite(Website website) {
			return UpdateWebsite(website, null);}

		private int UpdateWebsite(Website website, SqlInterface si) {
			int result = int.MinValue;

			string storedProcName = "efrstore_update_website";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) {
				si = new SqlInterface(dataProvider, connectionString);
			} else {
				newConnection = false;
			}

			try {
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Website_id", DbType.Int16, DBValue.ToDBInt16(website.WebsiteId)));
				paramCol.Add(new SqlDataParameter("@Partner_id", DbType.Int32, DBValue.ToDBInt32(website.PartnerId)));
				paramCol.Add(new SqlDataParameter("@Webproject_id", DbType.Int16, DBValue.ToDBInt16(website.WebprojectId)));
				paramCol.Add(new SqlDataParameter("@Website_dns", DbType.String, DBValue.ToDBString(website.WebsiteDns)));

		
				if (newConnection) {
					// open the connection
					si.Open();
				}

				result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (result > 0) {
					return result;
				}
				else {
					throw new SqlDataException("Error updating database calling " + storedProcName);
				}


			} finally {
				if(newConnection) {
					// Always close connection.
					si.Close();
				}
			}
			return result;
		}



		#endregion


        #region Homepage Methods

        private HomePageSpecialProducts LoadProductsList(DataRow row)
        {

            HomePageSpecialProducts homepageproductslist = new HomePageSpecialProducts();

            // Store database values into our business object
            homepageproductslist.PackageId = DBValue.ToInt32(row["package_id"]);
            homepageproductslist.PackageCategoryId = DBValue.ToInt32(row["package_category_id"]);
            homepageproductslist.DisplayOrder = DBValue.ToInt32(row["display_order"]);
            homepageproductslist.Name = DBValue.ToString(row["name"]);
            homepageproductslist.PageName = DBValue.ToString(row["page_name"]);
          
            // return the filled object
            return homepageproductslist;

        }


        public System.Collections.Generic.List<HomePageSpecialProducts> GetProductsListLinks(int catid)
        {
            return GetProductsListLinks(catid, null);
        }

        public System.Collections.Generic.List<HomePageSpecialProducts> GetProductsListLinks(int catid, SqlInterface si)
        {
            List<HomePageSpecialProducts> productsList = new List<HomePageSpecialProducts>();

            string storedProcName = "efrstore_get_homepagelists";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@package_category_id", DbType.Int32, DBValue.ToDBInt32(catid)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            productsList.Add(LoadProductsList(dt.Rows[i]));
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }
            }
            catch
            {
                // throw exception
                throw;
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
        }
        return productsList;

    }
   

        private HomePageSpecialProductsHeader LoadProductsListHeader(DataRow row)
        {

            HomePageSpecialProductsHeader homepageproductsheader = new HomePageSpecialProductsHeader();

            // Store database values into our business object
            homepageproductsheader.ImageUrl = DBValue.ToString(row["image_url"]);
            homepageproductsheader.CatagoryTitle = DBValue.ToString(row["package_category_title"]);
            homepageproductsheader.CatagoryDescription = DBValue.ToString(row["package_category_desc"]);
            homepageproductsheader.Catagoryid = DBValue.ToInt32(row["package_category_id"]);
            homepageproductsheader.ProductUrl = DBValue.ToString(row["product_url"]);


            // return the filled object
            return homepageproductsheader;

        }


        public System.Collections.Generic.List<HomePageSpecialProductsHeader> GetProductsListHeader()
        {
            return GetProductsListHeader(null);
        }

        public System.Collections.Generic.List<HomePageSpecialProductsHeader> GetProductsListHeader(SqlInterface si)
        {
            List<HomePageSpecialProductsHeader> productsListHeader = new List<HomePageSpecialProductsHeader>();

            string storedProcName = "efrstore_get_homepagelistheader";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            productsListHeader.Add(LoadProductsListHeader(dt.Rows[i]));
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }
            }
            catch (Exception ex1)
            {
                // throw exception
                throw;
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return productsListHeader;

        }

        #endregion
    }
}