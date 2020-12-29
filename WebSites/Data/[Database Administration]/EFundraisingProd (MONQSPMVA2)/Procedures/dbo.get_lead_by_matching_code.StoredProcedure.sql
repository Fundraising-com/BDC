USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[get_lead_by_matching_code]    Script Date: 02/14/2014 13:08:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
--This proc will return the matching code for the address provided
--[dbo].[get_matching_code] '68 pine valley', 'h2k3a9'
CREATE  procedure [dbo].[get_lead_by_matching_code] --'68 pine valley', 'h2k3a9'
           @street_address as nvarchar(75),
           @zip_code as nvarchar(10) ,
           @lead_id as int = 0  output
          
as
SET NOCOUNT ON

declare @matching_code nvarchar(10)

/*declare @lead_id as int
declare @street_address as nvarchar(75)
declare @zip_code as nvarchar(10)
set @street_address = '1200 ChOALPEN'
set @zip_code = '24401'
*/


--DEV
/*
set @matching_code = case when  dbo.fct_get_zzzzz(@zip_code)  =  '00000'  or dbo.fct_get_aa99(@street_address)  = '****'  then 'invalid' 
			       else
			       isnull(dbo.fct_get_zzzzz(@zip_code),'')+isnull(dbo.fct_get_aa99(@street_address),'') end
*/	
--PROD
set @matching_code = case when  QSPCOMMON.dbo.fct_get_zzzzz(@zip_code)  =  '00000'  or QSPCOMMON.dbo.fct_get_aa99(@street_address)  = '****'  then 'invalid' 
			       else
			       isnull(QSPCOMMON.dbo.fct_get_zzzzz(@zip_code),'')+isnull(QSPCOMMON.dbo.fct_get_aa99(@street_address),'') end


select @lead_id = lead_id from lead where matching_code = @matching_code and @matching_code <> 'invalid'
if (@lead_id is null)
   set @lead_id = 0
        

--print @lead_id
return @lead_id
GO
