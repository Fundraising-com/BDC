USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_advertising_supports]    Script Date: 02/14/2014 13:03:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Advertising_Support
CREATE PROCEDURE [dbo].[efrcrm_get_advertising_supports] AS
begin

select Advertising_Support_ID, Advertising_Support_Type_ID, Title, Publishnig_Date, Web_Site, Ordering_Phone_Number, Periodicity, Nb_Draw, Magazine_Price, Comments from Advertising_Support

end
GO
