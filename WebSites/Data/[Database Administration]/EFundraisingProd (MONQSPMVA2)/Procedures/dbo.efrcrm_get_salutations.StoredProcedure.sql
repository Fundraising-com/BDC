USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_salutations]    Script Date: 02/14/2014 13:06:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Salutation
CREATE PROCEDURE [dbo].[efrcrm_get_salutations] AS
begin

select Salutation_id, Salutation_desc from Salutation

end
GO
