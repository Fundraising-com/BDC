USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_lead_Comment_Advertising_Url]    Script Date: 02/14/2014 13:08:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- Author: Jason Farrell

-- Create date: May 11 2010

-- Description: Update lead comments with URL for admin advertising page

-- =============================================

Create PROCEDURE [dbo].[efrcrm_update_lead_Comment_Advertising_Url]

-- Add the parameters for the stored procedure here

@Lead_id as int

,@comments as varchar(1750)

AS

BEGIN

-- SET NOCOUNT ON added to prevent extra result sets from

-- interfering with SELECT statements.

SET NOCOUNT ON;

-- Insert statements for procedure here

update lead

set comments=@comments

where lead_id=@Lead_id

END
GO
