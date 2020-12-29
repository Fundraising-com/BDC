USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_channel_by_ID]    Script Date: 02/14/2014 13:04:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Lead_Channel
create  PROCEDURE [dbo].[efrcrm_get_lead_channel_by_ID] 
                  @channel_code varchar(10) 
AS
begin

select Channel_Code, Description from Lead_Channel
where channel_code = @channel_code

end
GO
