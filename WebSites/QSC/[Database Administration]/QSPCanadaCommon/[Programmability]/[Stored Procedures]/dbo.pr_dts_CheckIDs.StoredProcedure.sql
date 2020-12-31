USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_dts_CheckIDs]    Script Date: 06/07/2017 09:33:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_dts_CheckIDs] AS

DECLARE
	 @MaxAccount1 int
	, @MaxAccount2 int 
	, @MaxCampaign1 int 
	, @MaxCampaign2 int 
	, @MaxPhoneList int 
	, @MaxPhone int 
	, @MaxAddressList int 
	, @MaxAddress int 
	, @MaxContact1 int 
	, @MaxContact2 int   

SET NOCOUNT ON
select @MaxAccount1 = max(id) 		from dbo.CAccount
select @MaxAccount2 = max(id) 		from dbo.CAccount where id < 30000
select @MaxCampaign1 = max(id) 	from dbo.Campaign
select @MaxCampaign2 = max(id) 	from dbo.Campaign where id < 30000
select @MaxPhoneList = max(id) 		from dbo.PhoneList
select @MaxPhone = max(id) 		from dbo.Phone
select @MaxAddressList = max(id) 	from dbo.AddressList
select @MaxAddress = max(address_id) 	from dbo.Address
select @MaxContact1 = max(id) 		from dbo.Contact
select @MaxContact2 = max(id) 		from dbo.Contact where id < 11000
SET NOCOUNT OFF

SELECT
	   @@SERVERNAME 			as [ServerName]
	 , cast(@MaxAccount1 as varchar) 	as [MaxAccount1]
	 , cast(@MaxAccount2 as varchar) 	as [MaxAccount2]
	 , cast(@MaxCampaign1 as varchar) 	as [MaxCampaign1]
	 , cast(@MaxCampaign2 as varchar) 	as [MaxCampaign2]
	 , cast(@MaxPhoneList as varchar) 	as [MaxPhoneList]
	 , cast(@MaxPhone as varchar) 		as [MaxPhone]
	 , cast(@MaxAddressList as varchar) 	as [MaxAddressList]
	 , cast(@MaxAddress as varchar) 	 	as [MaxAddress]
	 , cast(@MaxContact1 as varchar) 	as [MaxContact1]
	 , cast(@MaxContact2 as varchar) 	as [MaxContact2]
GO
