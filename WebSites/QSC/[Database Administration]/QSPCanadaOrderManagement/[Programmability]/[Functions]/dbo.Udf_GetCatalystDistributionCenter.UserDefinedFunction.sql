USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[Udf_GetCatalystDistributionCenter]    Script Date: 06/07/2017 09:21:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[Udf_GetCatalystDistributionCenter] (@oraclecode varchar(50), @distcenter int)
							
							
RETURNS Varchar(50) AS  
BEGIN 


declare @distcentercode varchar(50)

if @distcenter=2
begin
	select @distcentercode ='CA-13'

end
else
begin
	if isnumeric(substring(@oraclecode, 1,4))=1
	begin
		select @distcentercode ='CA-10'
	end
	else if substring(@oraclecode, 1,2)  in ('PK', 'SA') 
	begin
		select @distcentercode ='CA-12'
	
	end 
	else if substring(@oraclecode, 1,2) not in ('PK', 'SA') 
				and isnumeric(substring(@oraclecode, 1,2)) = 0 
	begin
		select @distcentercode ='CA-11'
	
	end
end
return  IsNull(@distcentercode ,'')
end
GO
