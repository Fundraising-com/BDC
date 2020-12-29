USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_advertising_support_by_id]    Script Date: 02/14/2014 13:03:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Advertising_Support
CREATE PROCEDURE [dbo].[efrcrm_get_advertising_support_by_id] @Advertising_Support_ID int AS
begin

select Advertising_Support_ID, Advertising_Support_Type_ID, Title, Publishnig_Date, Web_Site, Ordering_Phone_Number, Periodicity, Nb_Draw, Magazine_Price, Comments from Advertising_Support where Advertising_Support_ID=@Advertising_Support_ID

end
GO
