USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_advertising_support]    Script Date: 02/14/2014 13:07:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Advertising_Support
CREATE PROCEDURE [dbo].[efrcrm_update_advertising_support] @Advertising_Support_ID int, @Advertising_Support_Type_ID int, @Title varchar(50), @Publishnig_Date smalldatetime, @Web_Site varchar(100), @Ordering_Phone_Number varchar(25), @Periodicity int, @Nb_Draw int, @Magazine_Price decimal, @Comments varchar(255) AS
begin

update Advertising_Support set Advertising_Support_Type_ID=@Advertising_Support_Type_ID, Title=@Title, Publishnig_Date=@Publishnig_Date, Web_Site=@Web_Site, Ordering_Phone_Number=@Ordering_Phone_Number, Periodicity=@Periodicity, Nb_Draw=@Nb_Draw, Magazine_Price=@Magazine_Price, Comments=@Comments where Advertising_Support_ID=@Advertising_Support_ID

end
GO
