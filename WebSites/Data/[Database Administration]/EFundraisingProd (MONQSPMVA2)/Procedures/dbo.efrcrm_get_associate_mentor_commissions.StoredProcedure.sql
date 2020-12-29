USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_associate_mentor_commissions]    Script Date: 02/14/2014 13:03:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Associate_mentor_commission
CREATE PROCEDURE [dbo].[efrcrm_get_associate_mentor_commissions] AS
begin

select Associate_id, Mentor_id, Product_class_id, Commission_rate, Comments from Associate_mentor_commission

end
GO
