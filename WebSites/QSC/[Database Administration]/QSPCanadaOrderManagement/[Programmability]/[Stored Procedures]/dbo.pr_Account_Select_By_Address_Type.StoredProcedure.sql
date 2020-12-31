USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Account_Select_By_Address_Type]    Script Date: 06/07/2017 09:19:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Account_Select_By_Address_Type] 

@iCampaignID int,
@AddressType nvarchar(20),
@SubscriptionID int = 0,
@Query  nvarchar(4000) output
AS


declare @sqlStatement nvarchar(4000)




	set @sqlStatement = 'select qspcanadacommon..address.*,qspcanadacommon..caccount.Name,billtoaccountid as AccountID, '+
       			     '  qspcanadacommon..address.Street1 as Address1,'+
      	  ' qspcanadacommon..address.Street2 as Address2,'+
	   'qspcanadacommon..address.Country as Country,'+
     	 ' qspcanadacommon..address.city as City,'+
     	'  qspcanadacommon..address.stateprovince as State,'+
     	 '  qspcanadacommon..address.postal_code as Zip'+
				' from  qspcanadacommon..campaign inner join qspcanadacommon..caccount on '+
				' qspcanadacommon..campaign.billtoaccountid = qspcanadacommon..caccount.id '+
				' inner join qspcanadacommon..address on '+
 				' qspcanadacommon..caccount.AddressListID = qspcanadacommon..address.AddressListID '

	if(@AddressType = 'ShipTo')
		BEGIN
			
			set @sqlStatement = @sqlStatement +  ' and address_type =54001  where  qspcanadacommon..campaign.id = ' + convert(nvarchar,@iCampaignID)
		END
	if(@AddressType = 'BillTo')
		BEGIN
			set @sqlStatement = @sqlStatement +  ' and address_type =54002  where  qspcanadacommon..campaign.id = ' + convert(nvarchar,@iCampaignID)
		END
	

set @Query = @sqlStatement
exec (@sqlStatement)
GO
