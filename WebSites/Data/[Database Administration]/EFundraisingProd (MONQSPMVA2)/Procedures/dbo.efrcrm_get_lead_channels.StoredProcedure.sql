USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_channels]    Script Date: 02/14/2014 13:04:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Lead_Channel
CREATE PROCEDURE [dbo].[efrcrm_get_lead_channels] AS
begin

select Channel_Code, Description from Lead_Channel

end
GO
