USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_personalization_image_validated]    Script Date: 02/14/2014 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jason Farrell
-- Create date: Sept 1, 2010
-- Description:	Update images status from Image Validator Tool
-- =============================================
CREATE PROCEDURE [dbo].[es_update_personalization_image_validated]
	-- Add the parameters for the stored procedure here
	@image_ids varchar(max)
	,@approver_name varchar(10)
	,@image_approval_status_id int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
DECLARE @script as nvarchar(max)

SET @script = 
	'UPDATE dbo.personalization_image
		SET image_approval_status_id = ' + CAST(@image_approval_status_id AS char(1))+
		  ',approved_date = ''' + rtrim(cast(datepart(month,GETDATE()) as char(2)))+'/'+ rtrim(cast(datepart(day,GETDATE()) as char(2)))+'/'+rtrim(cast(datepart(year,GETDATE()) as char(4))) + ' ' + cast(datepart(hh,GETDATE()) as char(2)) +':'+ cast(datepart(mi,GETDATE()) as char(2)) +':'+cast(datepart(ss,GETDATE()) as char(2)) + '''' +
		  ',approver_name =''' + @approver_name + '''' +
		' WHERE image_id in (' + @image_ids  + ')' 
EXECUTE sp_executesql @script 

END
GO
