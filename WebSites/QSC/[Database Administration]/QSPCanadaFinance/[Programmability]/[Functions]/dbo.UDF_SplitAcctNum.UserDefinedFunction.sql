USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_SplitAcctNum]    Script Date: 06/07/2017 09:17:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION  [dbo].[UDF_SplitAcctNum]
(
	@pGlAccountNum varchar(100),
	@section       varchar(25)
)
RETURNS Varchar(25)  AS  
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
BEGIN
	
declare @retval varchar(100)

	if @section = 'LegalEntity'
	begin
		select @retval = substring
					(
					@pGlAccountNum, 
					0, 
					dbo.UDF_instring(@pGlAccountNum, '.', 1) 
					);
	end
	else if @section = 'NaturalAccount'
	begin
		select @retval = substring
					(
					@pGlAccountNum, 
					dbo.UDF_instring(@pGlAccountNum, '.', 1) +1, 
					dbo.UDF_instring(@pGlAccountNum, '.', 2) 
					);
	end
	else if @section = 'SubAccount'
	begin
		select @retval = substring
					(
					@pGlAccountNum, 
					dbo.UDF_instring(@pGlAccountNum, '.', 2) + 1, 
					dbo.UDF_instring(@pGlAccountNum, '.', 3) 
					);
	end
	else if @section = 'ProductLineDept'
	begin
		select @retval = substring
					(
					@pGlAccountNum, 
					dbo.UDF_instring(@pGlAccountNum, '.', 3) + 1, 
					dbo.UDF_instring(@pGlAccountNum, '.', 4) 
					);
	end
	else if @section = 'LanguageMarket'
	begin
		select @retval = substring
					(
					@pGlAccountNum, 
					dbo.UDF_instring(@pGlAccountNum, '.', 4) + 1, 
					dbo.UDF_instring(@pGlAccountNum, '.', 5) 
					);
	end
	else if @section = 'Channel'
	begin
		select @retval = substring
					(
					@pGlAccountNum, 
					dbo.UDF_instring(@pGlAccountNum, '.', 5) + 1,
					dbo.UDF_instring(@pGlAccountNum, '.', 6) 
					);
	end
	else if @section = 'Segment7'
	begin
		select @retval = substring
					(
					@pGlAccountNum, 
					dbo.UDF_instring(@pGlAccountNum, '.', 6) + 1,
					len(@pGlAccountNum)
					);
	end
	else
	begin	
		select @retval = '';
	end



	Return @retval

END
GO
