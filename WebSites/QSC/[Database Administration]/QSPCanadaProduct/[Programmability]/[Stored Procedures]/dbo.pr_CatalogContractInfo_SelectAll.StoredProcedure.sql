USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_CatalogContractInfo_SelectAll]    Script Date: 06/07/2017 09:17:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_CatalogContractInfo_SelectAll]

	@CatalogID				int = 110,
	@CatalogIDLastSeason	int = 95

AS


	/*declare @CatalogID int
	declare @CatalogIDLastSeason int
	SET	@CatalogID = 147
	SET	@CatalogIDLastSeason = 139*/


DECLARE	@zPreviousSeason	char(1),
		@iPreviousYear		int,
		@GSTTaxRate			numeric(4,2),
		@HSTTaxRate			numeric(4,2)

SELECT	@zPreviousSeason = s.Season,
		@iPreviousYear = s.FiscalYear
FROM	QSPCanadaCommon..Season s
JOIN	Program_Master pm
			ON	pm.Season = s.ID
WHERE	pm.Program_ID = @CatalogIDLastSeason

SELECT	@GSTTaxRate = tr.ConsolidatedRate / 100
FROM	QSPCanadaCommon..TaxRegion tr
WHERE	tr.ID = 1

SELECT	@HSTTaxRate = tr.ConsolidatedRate / 100
FROM	QSPCanadaCommon..TaxRegion tr
WHERE	tr.ID = 2

SELECT 			CASE pd.Status WHEN 30601 THEN 'N/A' ELSE CASE pd.ContractFormReceived WHEN 1 THEN 'Yes' ELSE 'No' END END AS ContractFormReceived,
				'No' AS Changes,
				ISNULL(p.Product_Sort_Name, '') AS Magazine_Title,
				ISNULL(pls.Product_Sort_Name, '') AS Magazine_Title_LastSeason,
				COALESCE(p.Product_Code, pls.Product_Code, '') AS Product_Code,
				--ISNULL(pls.Product_Code, '') AS Product_Code_LastSeason,
				ISNULL(p.RemitCode, '') AS Remit_Code,
				ISNULL(pls.RemitCode, '') AS Remit_Code_LastSeason,
				ISNULL(pscd.Description, '') AS Product_Status,
				ISNULL(pscdls.Description, '') AS Product_Status_LastSeason,
				ISNULL(cscd.Description, '') AS Contract_Status,
				ISNULL(cscdls.Description, '') AS Contract_Status_LastSeason,
				ISNULL(catcd.Description, '') AS Category_Code,
				ISNULL(catcdls.Description, '') AS Category_Code_LastSeason,
				ISNULL(CONVERT(varchar(10), pd.CatalogPageNumber), '') AS Catalog_PageNumber,
				ISNULL(CONVERT(varchar(10), pdls.CatalogPageNumber), '') AS Catalog_PageNumber_LastSeason,
				ISNULL(ll.Description, '') AS Listing_Level,
				ISNULL(llls.Description, '') AS Listing_Level_LastSeason,
				ISNULL(plcd.Description, '') AS PlacementLevel,
				ISNULL(plcdls.Description, '') AS PlacementLevel_LastSeason,
				ISNULL(CONVERT(varchar(10), pd.Remit_Rate), '') AS Remit_Rate_Percent,
				ISNULL(CONVERT(varchar(10), pdls.Remit_Rate), '') AS Remit_Rate_Percent_LastSeason,
				ISNULL(pd.MagazineCoverFilename, '') AS MagazineCover_Filename,
				ISNULL(pdls.MagazineCoverFilename, '') AS MagazineCover_Filename_LastSeason,
				ISNULL(aps.Description, '') AS Ad_PageSize,				
				ISNULL(apsls.Description, '') AS Ad_PageSize_LastSeason,		
				ISNULL(pd.CatalogAdFilename, '') AS Ad_Filename,
				ISNULL(pdls.CatalogAdFilename, '') AS Ad_Filename_LastSeason,
				ISNULL(pd.ListingCopyText, '') AS Listing_Copy_Text,
				ISNULL(pdls.ListingCopyText, '') AS Listing_Copy_Text_LastSeason,
				CASE
					WHEN pd.Status = 30601 THEN 'N/A'
					ELSE
						CASE p.RemitCode
							WHEN '3186' THEN '3' --Reader's Digest
							ELSE
								CASE pd.PlacementLevel
									WHEN 68000 THEN '2'
									WHEN 68001 THEN	'1'
									WHEN 68002 THEN '0.5'
									WHEN 68003 THEN '0'
									ELSE 'N/A'
								END
						END
					END AS Catalog_Spacing,
				CASE
					WHEN pdls.Status = 30601 THEN 'N/A'
					ELSE
						CASE pls.RemitCode
							WHEN '3186' THEN '3' --Reader's Digest
							ELSE
								CASE pdls.PlacementLevel
									WHEN 68000 THEN '2'
									WHEN 68001 THEN	'1'
									WHEN 68002 THEN '0.5'
									WHEN 68003 THEN '0'
									ELSE 'N/A'
								END
						END
					END AS Catalog_Spacing_LastSeason,
				ISNULL(CONVERT(varchar(10), p.Nbr_Of_Issues_Per_Year), '') AS Nbr_Of_Issues_Per_Year,
				ISNULL(CONVERT(varchar(10), pls.Nbr_Of_Issues_Per_Year), '') AS Nbr_Of_Issues_Per_Year_LastSeason,
				ISNULL(CONVERT(varchar(10), pd.Nbr_Of_Issues), '') AS Nbr_Of_Issues,
				ISNULL(CONVERT(varchar(10), pdls.Nbr_Of_Issues), '') AS Nbr_Of_Issues_LastSeason,
				ISNULL(CONVERT(varchar(10), pd.NewsStand_Price_Yr), '') AS NewsStand_Price_Yr_Price,
				ISNULL(CONVERT(varchar(10), pdls.NewsStand_Price_Yr), '') AS NewsStand_Price_Yr_Price_LastSeason,
				ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), pd.NewsStand_Price_Yr * (1 + @GSTTaxRate))), '') AS NewsStand_Price_Single_Yr_GST_Price,
				ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), pdls.NewsStand_Price_Yr * (1 + @GSTTaxRate))), '') AS NewsStand_Price_Single_Yr_GST_Price_LastSeason,
				ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), pd.NewsStand_Price_Yr * (1 + @HSTTaxRate))), '') AS NewsStand_Price_Single_Yr_HST_Price,
				ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), pdls.NewsStand_Price_Yr * (1 + @HSTTaxRate))), '') AS NewsStand_Price_Single_Yr_HST_Price_LastSeason,
				ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * (1 + @GSTTaxRate) * 100) / 100)), '') AS NewsStand_Price_Yr_GST_Price,
				ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), CEILING(pdls.Nbr_of_Issues * pdls.NewsStand_Price_Yr * (1 + @GSTTaxRate) * 100) / 100)), '') AS NewsStand_Price_Yr_GST_Price_LastSeason,
				ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * (1 + @HSTTaxRate) * 100) / 100)), '') AS NewsStand_Price_Yr_HST_Price,
				ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), CEILING(pdls.Nbr_of_Issues * pdls.NewsStand_Price_Yr * (1 + @HSTTaxRate) * 100) / 100)), '') AS NewsStand_Price_Yr_HST_Price_LastSeason,
				ISNULL(CONVERT(varchar(10), pd.ConversionRate), '') AS Conversion_Rate_Percent,
				ISNULL(CONVERT(varchar(10), pdls.ConversionRate), '') AS Conversion_Rate_Percent_LastSeason,
				CASE p.Currency
					WHEN 801 THEN ''
					ELSE ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), pd.Basic_Price_Yr)), '') END AS US_Price,
				CASE pls.Currency
					WHEN 801 THEN ''
					ELSE ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), pdls.Basic_Price_Yr)), '') END AS US_Price_LastSeason,
				ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END)), '') AS Canadian_Base_Price,
				ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), pdls.Basic_Price_Yr * CASE pls.Currency WHEN 802 THEN pdls.ConversionRate ELSE 1 END)), '') AS Canadian_Base_Price_LastSeason,
				ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * (1 + @GSTTaxRate))), '') AS GST_You_Pay_Price,
				ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), pdls.Basic_Price_Yr * CASE pls.Currency WHEN 802 THEN pdls.ConversionRate ELSE 1 END * (1 + @GSTTaxRate))), '') AS GST_You_Pay_Price_LastSeason,
				ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * (1 + @HSTTaxRate))), '') AS HST_You_Pay_Price,
				ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), pdls.Basic_Price_Yr * CASE pls.Currency WHEN 802 THEN pdls.ConversionRate ELSE 1 END * (1 + @HSTTaxRate))), '') AS HST_You_Pay_Price_LastSeason,
				ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), CEILING(ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * (1 + @GSTTaxRate), 2)))), '') AS Cat_GST_Price,
				ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), CEILING(ROUND(pdls.Basic_Price_Yr * CASE pls.Currency WHEN 802 THEN pdls.ConversionRate ELSE 1 END * (1 + @GSTTaxRate), 2)))), '') AS Cat_GST_Price_LastSeason,
				ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), CEILING(ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * (1 + @HSTTaxRate), 2)))), '') AS Cat_HST_Price,
				ISNULL(CONVERT(varchar(10), CONVERT(Numeric(10,2), CEILING(ROUND(pdls.Basic_Price_Yr * CASE pls.Currency WHEN 802 THEN pdls.ConversionRate ELSE 1 END * (1 + @HSTTaxRate), 2)))), '') AS Cat_HST_Price_LastSeason,
				CASE (CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * (1 + @GSTTaxRate) * 100) / 100)
					WHEN 0 THEN ''
					ELSE CASE
							WHEN ISNULL(FLOOR((((CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * (1 + @GSTTaxRate) * 100) / 100) - (CEILING(ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * (1 + @GSTTaxRate), 2)))) / (CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * (1 + @GSTTaxRate) * 100) / 100)) * 100) / 100, 0) < 0.15 THEN ''
							ELSE ISNULL(CONVERT(varchar(10), FLOOR((((CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * (1 + @GSTTaxRate) * 100) / 100) - (CEILING(ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * (1 + @GSTTaxRate), 2)))) / (CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * (1 + @GSTTaxRate) * 100) / 100)) * 100) / 100), '') END END AS Cat_GST_Percent,
				CASE (CEILING(pdls.Nbr_of_Issues * pdls.NewsStand_Price_Yr * (1 + @GSTTaxRate) * 100) / 100)
					WHEN 0 THEN ''
					ELSE CASE
							WHEN ISNULL(FLOOR((((CEILING(pdls.Nbr_of_Issues * pdls.NewsStand_Price_Yr * (1 + @GSTTaxRate) * 100) / 100) - (CEILING(ROUND(pdls.Basic_Price_Yr * CASE pls.Currency WHEN 802 THEN pdls.ConversionRate ELSE 1 END * (1 + @GSTTaxRate), 2)))) / (CEILING(pdls.Nbr_of_Issues * pdls.NewsStand_Price_Yr * (1 + @GSTTaxRate) * 100) / 100)) * 100) / 100, 0) < 0.15 THEN ''
							ELSE ISNULL(CONVERT(varchar(10), FLOOR((((CEILING(pdls.Nbr_of_Issues * pdls.NewsStand_Price_Yr * (1 + @GSTTaxRate) * 100) / 100) - (CEILING(ROUND(pdls.Basic_Price_Yr * CASE pls.Currency WHEN 802 THEN pdls.ConversionRate ELSE 1 END * (1 + @GSTTaxRate), 2)))) / (CEILING(pdls.Nbr_of_Issues * pdls.NewsStand_Price_Yr * (1 + @GSTTaxRate) * 100) / 100)) * 100) / 100), '') END END AS Cat_GST_Percent_LastSeason,
				CASE (CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * (1 + @HSTTaxRate) * 100) / 100)
					WHEN 0 THEN ''
					ELSE CASE
							WHEN ISNULL(FLOOR((((CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * (1 + @HSTTaxRate) * 100) / 100) - (CEILING(ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * (1 + @HSTTaxRate), 2)))) / (CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * (1 + @HSTTaxRate) * 100) / 100)) * 100) / 100, 0) < 0.15 THEN ''
							ELSE ISNULL(CONVERT(varchar(10), FLOOR((((CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * (1 + @HSTTaxRate) * 100) / 100) - (CEILING(ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * (1 + @HSTTaxRate), 2)))) / (CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * (1 + @HSTTaxRate) * 100) / 100)) * 100) / 100), '') END END AS Cat_HST_Percent,
				CASE (CEILING(pdls.Nbr_of_Issues * pdls.NewsStand_Price_Yr * (1 + @HSTTaxRate) * 100) / 100)
					WHEN 0 THEN ''
					ELSE CASE
							WHEN ISNULL(FLOOR((((CEILING(pdls.Nbr_of_Issues * pdls.NewsStand_Price_Yr * (1 + @HSTTaxRate) * 100) / 100) - (CEILING(ROUND(pdls.Basic_Price_Yr * CASE pls.Currency WHEN 802 THEN pdls.ConversionRate ELSE 1 END * (1 + @HSTTaxRate), 2)))) / (CEILING(pdls.Nbr_of_Issues * pdls.NewsStand_Price_Yr * (1 + @HSTTaxRate) * 100) / 100)) * 100) / 100, 0) < 0.15 THEN ''
							ELSE ISNULL(CONVERT(varchar(10), FLOOR((((CEILING(pdls.Nbr_of_Issues * pdls.NewsStand_Price_Yr * (1 + @HSTTaxRate) * 100) / 100) - (CEILING(ROUND(pdls.Basic_Price_Yr * CASE pls.Currency WHEN 802 THEN pdls.ConversionRate ELSE 1 END * (1 + @HSTTaxRate), 2)))) / (CEILING(pdls.Nbr_of_Issues * pdls.NewsStand_Price_Yr * (1 + @HSTTaxRate) * 100) / 100)) * 100) / 100), '') END END AS Cat_HST_Percent_LastSeason,
				CASE (CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * (1 + @HSTTaxRate) * 100) / 100)
					WHEN 0 THEN ''
					ELSE CONVERT(varchar(10), ISNULL(FLOOR((((CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * (1 + @GSTTaxRate) * 100) / 100) - (CEILING(ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * (1 + @GSTTaxRate), 2)))) / (CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * (1 + @GSTTaxRate) * 100) / 100)) * 100) / 100, 0) - (ISNULL(FLOOR((((CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * (1 + @HSTTaxRate) * 100) / 100) - (CEILING(ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * (1 + @HSTTaxRate), 2)))) / (CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * (1 + @HSTTaxRate) * 100) / 100)) * 100) / 100, 0))) END AS Cat_Difference_Percent,
				CASE (CEILING(pdls.Nbr_of_Issues * pdls.NewsStand_Price_Yr * (1 + @HSTTaxRate) * 100) / 100)
					WHEN 0 THEN ''
					ELSE CONVERT(varchar(10), ISNULL(FLOOR((((CEILING(pdls.Nbr_of_Issues * pdls.NewsStand_Price_Yr * (1 + @GSTTaxRate) * 100) / 100) - (CEILING(ROUND(pdls.Basic_Price_Yr * CASE pls.Currency WHEN 802 THEN pdls.ConversionRate ELSE 1 END * (1 + @GSTTaxRate), 2)))) / (CEILING(pdls.Nbr_of_Issues * pdls.NewsStand_Price_Yr * (1 + @GSTTaxRate) * 100) / 100)) * 100) / 100, 0) - (ISNULL(FLOOR((((CEILING(pdls.Nbr_of_Issues * pdls.NewsStand_Price_Yr * (1 + @HSTTaxRate) * 100) / 100) - (CEILING(ROUND(pdls.Basic_Price_Yr * CASE pls.Currency WHEN 802 THEN pdls.ConversionRate ELSE 1 END * (1 + @HSTTaxRate), 2)))) / (CEILING(pdls.Nbr_of_Issues * pdls.NewsStand_Price_Yr * (1 + @HSTTaxRate) * 100) / 100)) * 100) / 100, 0))) END AS Cat_Difference_Percent_LastSeason,
				ISNULL(CONVERT(varchar(10), pd.BasePriceSansPostage), '') AS BasePriceWithoutPostage_Price,
				ISNULL(CONVERT(varchar(10), pdls.BasePriceSansPostage), '') AS BasePriceWithoutPostage_Price_LastSeason,
				ISNULL(CONVERT(varchar(10), pd.PostageRemitRate), '') AS PostageRemitRate_Percent,
				ISNULL(CONVERT(varchar(10), pdls.PostageRemitRate), '') AS PostageRemitRate_Percent_LastSeason,
				ISNULL(CONVERT(varchar(10), pd.PostageAmount), '') AS PostageAmount_Price,
				ISNULL(CONVERT(varchar(10), pdls.PostageAmount), '') AS PostageAmount_Price_LastSeason,
				ISNULL(CONVERT(varchar(10), pd.BaseRemitRate), '') AS BaseRemitRate_Percent,
				ISNULL(CONVERT(varchar(10), pdls.BaseRemitRate), '') AS BaseRemitRate_Percent_LastSeason,
				ISNULL(pd.Effort_Key, '') AS EffortKey,
				ISNULL(pdls.Effort_Key, '') AS EffortKey_LastSeason,
				ISNULL(pd.ABCCode, '') AS Contract_ABCCode,
				ISNULL(pdls.ABCCode, '') AS Contract_ABCCode_LastSeason,
				CASE p.IsQSPExclusive WHEN '1' THEN 'YES' ELSE 'NO' END As QSP_Exclusive,
				CASE pls.IsQSPExclusive WHEN '1' THEN 'YES' ELSE 'NO' END As QSP_Exclusive_LastSeason,
				ISNULL(pub.Pub_Name, '') AS Publisher_Name,
				ISNULL(publs.Pub_Name, '') AS Publisher_Name_LastSeason,
				ISNULL(pub.Pub_Addr_1, '') AS Publisher_Address1,
				ISNULL(publs.Pub_Addr_1, '') AS Publisher_Address1_LastSeason,
				ISNULL(pub.Pub_Addr_2, '') AS Publisher_Address2,
				ISNULL(publs.Pub_Addr_2, '') AS Publisher_Address2_LastSeason,
				ISNULL(pub.Pub_City, '') AS Publisher_City,
				ISNULL(publs.Pub_City, '') AS Publisher_City_LastSeason,
				ISNULL(pub.Pub_State, '') AS Publisher_State,
				ISNULL(publs.Pub_State, '') AS Publisher_State_LastSeason,
				ISNULL(pub.Pub_Zip, '') AS Publisher_Zip,
				ISNULL(publs.Pub_Zip, '') AS Publisher_Zip_LastSeason,
				ISNULL(pub.Pub_Zip_Four, '') AS Publisher_Zip_Four,
				ISNULL(publs.Pub_Zip_Four, '') AS Publisher_Zip_Four_LastSeason,
				ISNULL(pub.Pub_CountryCode, '') AS Publisher_Country,
				ISNULL(publs.Pub_CountryCode, '') AS Publisher_Country_LastSeason,
				ISNULL(pcProduct.pcontact_fname, ISNULL(pcMain.pcontact_fname, '')) PublisherContact_FirstName,
				ISNULL(pcProductls.pcontact_fname, ISNULL(pcMainls.pcontact_fname, '')) PublisherContact_FirstName_LastSeason,
				ISNULL(pcProduct.pcontact_lname, ISNULL(pcMain.pcontact_lname, '')) PublisherContact_LastName,
				ISNULL(pcProductls.pcontact_lname, ISNULL(pcMainls.pcontact_lname, '')) PublisherContact_LastName_LastSeason,
				ISNULL(pcProduct.pcontact_title, ISNULL(pcMain.pcontact_title, '')) PublisherContact_Title,
				ISNULL(pcProductls.pcontact_title, ISNULL(pcMainls.pcontact_title, '')) PublisherContact_Title_LastSeason,
				ISNULL(phProduct.PhoneNumber, ISNULL(phMain.PhoneNumber, '')) PublisherContact_Phone,
				ISNULL(phProductls.PhoneNumber, ISNULL(phMainls.PhoneNumber, '')) PublisherContact_Phone_LastSeason,
				ISNULL(faxProduct.PhoneNumber, ISNULL(faxMain.PhoneNumber, '')) PublisherContact_Fax,
				ISNULL(faxProductls.PhoneNumber, ISNULL(faxMainls.PhoneNumber, '')) PublisherContact_Fax_LastSeason,
				ISNULL(pcProduct.pcontact_Email, ISNULL(pcMain.pcontact_Email, '')) PublisherContact_Email,
				ISNULL(pcProductls.pcontact_Email, ISNULL(pcMainls.pcontact_Email, '')) PublisherContact_Email_LastSeason,
				ISNULL(fh.Ful_Name, '') AS Fulfillment_House_Name,
				ISNULL(fhls.Ful_Name, '') AS Fulfillment_House_Name_LastSeason,
				ISNULL(fh.Ful_Addr_1, '') AS Fulfillment_House_Address1,
				ISNULL(fhls.Ful_Addr_1, '') AS Fulfillment_House_Address1_LastSeason,
				ISNULL(fh.Ful_Addr_2, '') AS Fulfillment_House_Address2,
				ISNULL(fhls.Ful_Addr_2, '') AS Fulfillment_House_Address2_LastSeason,
				ISNULL(fh.Ful_City, '') AS Fulfillment_House_City,
				ISNULL(fhls.Ful_City, '') AS Fulfillment_House_City_LastSeason,
				ISNULL(fh.Ful_State, '') AS Fulfillment_House_State,
				ISNULL(fhls.Ful_State, '') AS Fulfillment_House_State_LastSeason,
				ISNULL(fh.Ful_Zip, '') AS Fulfillment_House_Zip,
				ISNULL(fhls.Ful_Zip, '') AS Fulfillment_House_Zip_LastSeason,
				ISNULL(fh.Ful_Zip_Four, '') AS Fulfillment_House_Zip_Four,
				ISNULL(fhls.Ful_Zip_Four, '') AS Fulfillment_House_Zip_Four_LastSeason,
				ISNULL(fh.CountryCode, '') AS Fulfillment_House_Country,
				ISNULL(fhls.CountryCode, '') AS Fulfillment_House_Country_LastSeason,
				ISNULL(fh.QSPAgencyCode, '') AS QSPAgencyCode,
				ISNULL(fhls.QSPAgencyCode, '') AS QSPAgencyCode_LastSeason,
				ISNULL(fhcProduct.FirstName, ISNULL(fhcMain.FirstName, '')) Fulfillment_House_Contact_FirstName,
				ISNULL(fhcProductls.FirstName, ISNULL(fhcMainls.FirstName, '')) Fulfillment_House_Contact_FirstName_LastSeason,
				ISNULL(fhcProduct.LastName, ISNULL(fhcMain.LastName, '')) Fulfillment_House_Contact_LastName,
				ISNULL(fhcProductls.LastName, ISNULL(fhcMainls.LastName, '')) Fulfillment_House_Contact_LastName_LastSeason,
				ISNULL(fhcProduct.Title, ISNULL(fhcMain.Title, '')) Fulfillment_House_Contact_Title,
				ISNULL(fhcProductls.Title, ISNULL(fhcMainls.Title, '')) Fulfillment_House_Contact_Title_LastSeason,
				ISNULL(fhcProduct.Email, ISNULL(fhcMain.Email, '')) Fulfillment_House_Contact_Email,
				ISNULL(fhcProductls.Email, ISNULL(fhcMainls.Email, '')) Fulfillment_House_Contact_Email_LastSeason,
				ISNULL(fhcProduct.WorkPhone, ISNULL(fhcMain.WorkPhone, '')) Fulfillment_House_Contact_WorkPhone,
				ISNULL(fhcProductls.WorkPhone, ISNULL(fhcMainls.WorkPhone, '')) Fulfillment_House_Contact_WorkPhone_LastSeason,
				ISNULL(fhcProduct.Fax, ISNULL(fhcMain.Fax, '')) Fulfillment_House_Contact_Fax,
				ISNULL(fhcProductls.Fax, ISNULL(fhcMainls.Fax, '')) Fulfillment_House_Contact_Fax_LastSeason,
				ISNULL(p.VendorNumber, '') AS Vendor_Number,
				ISNULL(pls.VendorNumber, '') AS Vendor_Number_LastSeason,
				ISNULL(p.VendorSiteName, '') AS Vendor_Site_Name,
				ISNULL(pls.VendorSiteName, '') AS Vendor_Site_Name_LastSeason,
				ISNULL(p.PayGroupLookupCode, '') AS PayGroup_Lookup_Code,
				ISNULL(pls.PayGroupLookupCode, '') AS PayGroup_Lookup_Code_LastSeason,
				CASE pd.InternetApproval WHEN 1 THEN 'Yes' WHEN 0 THEN 'No' ELSE '' END AS Internet_Approval,
				CASE pdls.InternetApproval WHEN 1 THEN 'Yes' WHEN 0 THEN 'No' ELSE '' END AS Internet_Approval_LastSeason,
				ISNULL(pd.QSPCAListingCopyText, '') AS QSPCA_Listing_Copy_Text,
				ISNULL(pdls.QSPCAListingCopyText, '') AS QSPCA_Listing_Copy_Text_LastSeason,
				ISNULL(CONVERT(varchar(10), pd.AdCost), '') AS Ad_Cost,				
				ISNULL(CONVERT(varchar(10), pdls.AdCost), '') AS Ad_Cost_LastSeason,
				CASE pd.AdCostCurrency
					WHEN 801 THEN 'CAD'
					ELSE 'USD'
				END AS Ad_Cost_Currency,
				CASE pdls.AdCostCurrency
					WHEN 801 THEN 'CAD'
					ELSE 'USD'
				END AS Ad_Cost_Currency_LastSeason,
				ISNULL(CONVERT(varchar(10), pd.QSPPremiumID), '') AS QSP_Premium,				
				ISNULL(pd.prdPremiumInd, '') AS Premium_Indicator,
				ISNULL(pd.prdPremiumCode, '') AS Premium_Code,
				ISNULL(pd.prdPremiumCopy, '') AS Premium_Copy,
				ISNULL(tmgGST.tax_registration_number, '') AS GST_Registration_Number,
				ISNULL(tmgGSTls.tax_registration_number, '') AS GST_Registration_Number_LastSeason,
				ISNULL(tmgHST.tax_registration_number, '') AS HST_Registration_Number,
				ISNULL(tmgHSTls.tax_registration_number, '') AS HST_Registration_Number_LastSeason,
				ISNULL(tmgPST.tax_registration_number, '') AS PST_Registration_Number,
				ISNULL(tmgPSTls.tax_registration_number, '') AS PST_Registration_Number_LastSeason,
				ISNULL(ptcd.Description, '') AS Product_Type,
				ISNULL(ptcdls.Description, '') AS Product_Type_LastSeason,
				ISNULL(p.Lang, '') AS Product_Language,
				ISNULL(pls.Lang, '') AS Product_Language_LastSeason,
				ISNULL(pd.OracleCode, '') AS Oracle_Code,
				ISNULL(pdls.OracleCode, '') AS Oracle_Code_LastSeason,
				ISNULL(pd.Comment, '') AS Comment,
				ISNULL(pd.PrinterComment, '') AS Printer_Comment,
				ISNULL(pd.ContractComment, '') AS Contract_Comment,
				ISNULL(p.Comment, '') AS Product_Comment
FROM			Pricing_Details pd
JOIN			Product p
					ON	p.Product_Instance = pd.Product_Instance
					AND	pd.ProgramSectionID in (SELECT ID FROM ProgramSection WHERE Program_ID = @CatalogID)
LEFT JOIN		QSPCanadaCommon..CodeDetail pscd
					ON	pscd.Instance = p.Status
LEFT JOIN		QSPCanadaCommon..CodeDetail cscd
					ON	cscd.Instance = pd.Status
LEFT JOIN		AdPageSize aps
					ON	aps.ID = pd.AdPageSizeID
LEFT JOIN		QSPCanadaCommon..CodeDetail ptcd
					ON	ptcd.Instance = p.Type
LEFT JOIN		Publishers pub
					ON	pub.Pub_Nbr = p.Pub_Nbr
LEFT JOIN		(Publisher_Contacts pcProduct
					JOIN	PublisherContact_Product pcpProduct
								ON	pcpProduct.PublisherContactID = pcProduct.PContact_Instance)
					ON	pcProduct.Pub_Nbr = pub.Pub_Nbr
					AND	pcpProduct.Product_Code = p.Product_Code
LEFT JOIN		Phone phProduct
					ON	phProduct.PhoneListID = pcProduct.PhoneListID
					AND phProduct.Type = 1
LEFT JOIN		Phone faxProduct
					ON	faxProduct.PhoneListID = pcProduct.PhoneListID
					AND faxProduct.Type = 3
LEFT JOIN		(Publisher_Contacts pcMain
					LEFT JOIN	PublisherContact_Product pcpMain
									ON	pcpMain.PublisherContactID = pcMain.PContact_Instance)
					ON	pcMain.Pub_Nbr = pub.Pub_Nbr
					AND	pcpMain.ID IS NULL
LEFT JOIN		Phone phMain
					ON	phMain.PhoneListID = pcMain.PhoneListID
					AND	phMain.Type = 1
LEFT JOIN		Phone faxMain
					ON	faxMain.PhoneListID = pcMain.PhoneListID
					AND	faxMain.Type = 3
LEFT JOIN		Fulfillment_House fh
					ON	fh.Ful_Nbr = p.Fulfill_House_Nbr
LEFT JOIN		(Fulfillment_House_Contacts fhcProduct
					JOIN	FulfillmentHouseContact_Product fhcpProduct
								ON	fhcpProduct.FulfillmentHouseContactID = fhcProduct.Instance)
					ON	fhcProduct.Ful_Nbr = fh.Ful_Nbr
					AND	fhcpProduct.Product_Code = p.Product_Code
LEFT JOIN		(Fulfillment_House_Contacts fhcMain
					LEFT JOIN	FulfillmentHouseContact_Product fhcpMain
									ON	fhcpMain.FulfillmentHouseContactID = fhcMain.Instance)
					ON	fhcMain.Ful_Nbr = fh.Ful_Nbr
					AND	fhcpMain.ID IS NULL
LEFT JOIN		QSPCanadaCommon..CodeDetail catcd
					ON	catcd.Instance = p.Category_Code
LEFT JOIN		ListingLevel ll
					ON	ll.ID = pd.ListingLevelID
LEFT JOIN		QSPCanadaCommon..CodeDetail plcd
					ON	plcd.Instance = pd.PlacementLevel
LEFT JOIN		QSPCanadaCommon..TaxMagRegistration tmgGST
					ON	tmgGST.title_code = p.RemitCode
					AND	tmgGST.tax_ID = 1
LEFT JOIN		QSPCanadaCommon..TaxMagRegistration tmgHST
					ON	tmgHST.title_code = p.RemitCode
					AND	tmgHST.tax_ID = 2
LEFT JOIN		QSPCanadaCommon..TaxMagRegistration tmgPST
					ON	tmgPST.title_code = p.RemitCode
					AND	tmgPST.tax_ID = 3
FULL OUTER JOIN	(Product pls
					JOIN	Pricing_Details pdls
								ON	pdls.Product_Instance = pls.Product_Instance
								AND pdls.ProgramSectionID in (SELECT ID FROM ProgramSection WHERE Program_ID = @CatalogIDLastSeason)
					LEFT JOIN		QSPCanadaCommon..CodeDetail pscdls
										ON	pscdls.Instance = pls.Status
					LEFT JOIN		QSPCanadaCommon..CodeDetail cscdls
										ON	cscdls.Instance = pdls.Status
					LEFT JOIN		AdPageSize apsls
										ON	apsls.ID = pdls.AdPageSizeID
					LEFT JOIN		QSPCanadaCommon..CodeDetail ptcdls
										ON	ptcdls.Instance = pls.Type
					LEFT JOIN		Publishers publs
										ON	publs.Pub_Nbr = pls.Pub_Nbr
					LEFT JOIN		(Publisher_Contacts pcProductls
										JOIN	PublisherContact_Product pcpProductls
													ON	pcpProductls.PublisherContactID = pcProductls.PContact_Instance)
										ON	pcProductls.Pub_Nbr = publs.Pub_Nbr
										AND	pcpProductls.Product_Code = pls.Product_Code
					LEFT JOIN		Phone phProductls
										ON	phProductls.PhoneListID = pcProductls.PhoneListID
										AND phProductls.Type = 1
					LEFT JOIN		Phone faxProductls
										ON	faxProductls.PhoneListID = pcProductls.PhoneListID
										AND faxProductls.Type = 3
					LEFT JOIN		(Publisher_Contacts pcMainls
										LEFT JOIN	PublisherContact_Product pcpMainls
														ON	pcpMainls.PublisherContactID = pcMainls.PContact_Instance)
										ON	pcMainls.Pub_Nbr = publs.Pub_Nbr
										AND	pcpMainls.ID IS NULL
					LEFT JOIN		Phone phMainls
										ON	phMainls.PhoneListID = pcMainls.PhoneListID
										AND	phMainls.Type = 1
					LEFT JOIN		Phone faxMainls
										ON	faxMainls.PhoneListID = pcMainls.PhoneListID
										AND	faxMainls.Type = 3
					LEFT JOIN		Fulfillment_House fhls
										ON	fhls.Ful_Nbr = pls.Fulfill_House_Nbr
					LEFT JOIN		(Fulfillment_House_Contacts fhcProductls
										JOIN	FulfillmentHouseContact_Product fhcpProductls
													ON	fhcpProductls.FulfillmentHouseContactID = fhcProductls.Instance)
										ON	fhcProductls.Ful_Nbr = fhls.Ful_Nbr
										AND	fhcProductls.Product_Code = pls.Product_Code
					LEFT JOIN		(Fulfillment_House_Contacts fhcMainls
										LEFT JOIN	FulfillmentHouseContact_Product fhcpMainls
														ON	fhcpMainls.FulfillmentHouseContactID = fhcMainls.Instance)
										ON	fhcMainls.Ful_Nbr = fhls.Ful_Nbr
										AND	fhcpMainls.ID IS NULL
					LEFT JOIN		QSPCanadaCommon..CodeDetail catcdls
										ON	catcdls.Instance = pls.Category_Code
					LEFT JOIN		ListingLevel llls
										ON	llls.ID = pdls.ListingLevelID
					LEFT JOIN		QSPCanadaCommon..CodeDetail plcdls
										ON	plcdls.Instance = pdls.PlacementLevel
					LEFT JOIN		QSPCanadaCommon..TaxMagRegistration tmgGSTls
										ON	tmgGSTls.title_code = pls.RemitCode
										AND	tmgGSTls.tax_ID = 1
					LEFT JOIN		QSPCanadaCommon..TaxMagRegistration tmgHSTls
										ON	tmgHSTls.title_code = pls.RemitCode
										AND	tmgHSTls.tax_ID = 2
					LEFT JOIN		QSPCanadaCommon..TaxMagRegistration tmgPSTls
										ON	tmgPSTls.title_code = pls.RemitCode
										AND	tmgPSTls.tax_ID = 3)
					--ON	pls.Product_Sort_Name = p.Product_Sort_Name
					--AND	pls.Product_Code like 'S%'
					ON	pls.Product_Code = p.Product_Code
					AND	pls.Product_Year = @iPreviousYear
					AND	pls.Product_Season = @zPreviousSeason
					AND	pdls.TaxRegionID = pd.TaxRegionID
WHERE p.Status IN (30600, 30603)
--AND	p.Product_Code NOT LIKE 'G%'
--AND p.Product_Code NOT LIKE 'K%'
GROUP BY		pd.Status,
				pdls.Status,
				pd.ContractFormReceived,
				pscd.Description,
				pscdls.Description,
				cscd.Description,
				cscdls.Description,
				pd.MagazineCoverFilename,
				pdls.MagazineCoverFilename,
				pd.CatalogAdFilename,
				pdls.CatalogAdFilename,
				aps.Description,				
				apsls.Description,		
				pd.InternetApproval,
				pdls.InternetApproval,
				pd.ABCCode,
				pdls.ABCCode,
				pd.QSPPremiumID,
				pdls.QSPPremiumID,
				pd.prdPremiumInd,
				pdls.prdPremiumInd,
				pd.prdPremiumCode,
				pdls.prdPremiumCode,
				pd.prdPremiumCopy,
				pdls.prdPremiumCopy,
				pd.AdCost,
				pdls.AdCost,
				pd.AdCostCurrency,
				pdls.AdCostCurrency,
				pd.OracleCode,
				pdls.OracleCode,
				p.IsQSPExclusive,
				pls.IsQSPExclusive,
				pub.Pub_CountryCode,
				publs.Pub_CountryCode,
				pd.CatalogPageNumber,
				pdls.CatalogPageNumber,
				p.Product_Code,
				pls.Product_Code,
				p.RemitCode,
				pls.RemitCode,
				p.Product_Sort_Name,
				pls.Product_Sort_Name,
				ptcd.Description,
				ptcdls.Description,
				p.Type,
				pub.Pub_Name,
				publs.Pub_Name,
				pub.Pub_Addr_1,
				publs.Pub_Addr_1,
				pub.Pub_Addr_2,
				publs.Pub_Addr_2,
				pub.Pub_City,
				publs.Pub_City,
				pub.Pub_State,
				publs.Pub_State,
				pub.Pub_Zip,
				publs.Pub_Zip,
				pub.Pub_Zip_Four,
				publs.Pub_Zip_Four,
				pcProduct.pcontact_fname,
				pcMain.pcontact_fname,
				pcProductls.pcontact_fname, 
				pcMainls.pcontact_fname,
				pcProduct.pcontact_lname, 
				pcMain.pcontact_lname,
				pcProductls.pcontact_lname,
				pcMainls.pcontact_lname,
				pcProduct.pcontact_title,
				pcProductls.pcontact_title,
				pcMain.pcontact_title,
				pcMainls.pcontact_title,
				phProduct.PhoneNumber, 
				phMain.PhoneNumber,
				phProductls.PhoneNumber,
				phMainls.PhoneNumber,
				faxProduct.PhoneNumber,
				faxProductls.PhoneNumber,
				faxMain.PhoneNumber,
				faxMainls.PhoneNumber,
				pcProduct.pcontact_Email,
				pcMain.pcontact_Email,
				pcProductls.pcontact_Email,
				pcMainls.pcontact_Email,
				fh.Ful_Name,
				fhls.Ful_Name,
				fh.Ful_Addr_1,
				fhls.Ful_Addr_1,
				fh.Ful_Addr_2,
				fhls.Ful_Addr_2,
				fh.Ful_City,
				fhls.Ful_City,
				fh.Ful_State,
				fhls.Ful_State,
				fh.Ful_Zip,
				fhls.Ful_Zip,
				fh.Ful_Zip_Four,
				fhls.Ful_Zip_Four,
				fh.CountryCode,
				fhls.CountryCode,
				fh.QSPAgencyCode,
				fhls.QSPAgencyCode,
				fhcProduct.FirstName,
				fhcMain.FirstName,
				fhcProductls.FirstName,
				fhcMainls.FirstName,
				fhcProduct.LastName,
				fhcMain.LastName,
				fhcProductls.LastName,
				fhcMainls.LastName,
				fhcProduct.Title,
				fhcMain.Title,
				fhcProductls.Title,
				fhcMainls.Title,
				fhcProduct.Email,
				fhcMain.Email,
				fhcProductls.Email,
				fhcMainls.Email,
				fhcProduct.WorkPhone,
				fhcMain.WorkPhone,
				fhcProductls.WorkPhone,
				fhcMainls.WorkPhone,
				fhcProduct.Fax,
				fhcMain.Fax,
				fhcProductls.Fax,
				fhcMainls.Fax,
				pd.Effort_Key,
				pdls.Effort_Key,
				pd.ListingCopyText,
				pdls.ListingCopyText,
				catcd.Description,
				catcdls.Description,
				tmgGST.tax_registration_number,
				tmgGSTls.tax_registration_number,
				tmgHST.tax_registration_number,
				tmgHSTls.tax_registration_number,
				tmgPST.tax_registration_number,
				tmgPSTls.tax_registration_number,
				ll.Description,
				llls.Description,
				pd.PlacementLevel,
				pdls.PlacementLevel,
				plcd.Description,
				plcdls.Description,
				p.Lang,
				pls.Lang,
				p.VendorNumber,
				pls.VendorNumber,
				p.VendorSiteName,
				pls.VendorSiteName,
				p.PayGroupLookupCode,
				pls.PayGroupLookupCode,
				pd.Remit_Rate,
				pdls.Remit_Rate,
				p.Nbr_Of_Issues_Per_Year,
				pls.Nbr_Of_Issues_Per_Year,
				pd.Nbr_Of_Issues,
				pdls.Nbr_Of_Issues,
				pd.QSPCAListingCopyText,
				pdls.QSPCAListingCopyText,
				pd.NewsStand_Price_Yr,
				pdls.NewsStand_Price_Yr,
				pd.ConversionRate,
				pdls.ConversionRate,
				p.CountryCode,
				pd.Basic_Price_Yr,
				pls.CountryCode,
				pdls.Basic_Price_Yr,
				pd.ConversionRate,
				pdls.ConversionRate,
				p.Currency,
				pls.Currency,
				pd.BasePriceSansPostage,
				pdls.BasePriceSansPostage,
				pd.PostageRemitRate,
				pdls.PostageRemitRate,
				pd.PostageAmount,
				pdls.PostageAmount,
				pd.BaseRemitRate,
				pdls.BaseRemitRate,
				p.Comment,
				pd.Comment,
				pd.ContractComment,
				pd.PrinterComment
ORDER BY		p.Type,
				pub.Pub_Name,
				p.RemitCode
GO
