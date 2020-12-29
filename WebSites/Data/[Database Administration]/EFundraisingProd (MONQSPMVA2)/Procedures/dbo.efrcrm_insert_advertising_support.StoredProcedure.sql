USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_advertising_support]    Script Date: 02/14/2014 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Advertising_Support
CREATE PROCEDURE [dbo].[efrcrm_insert_advertising_support] @Advertising_Support_ID int OUTPUT, @Advertising_Support_Type_ID int, @Title varchar(50), @Publishnig_Date smalldatetime, @Web_Site varchar(100), @Ordering_Phone_Number varchar(25), @Periodicity int, @Nb_Draw int, @Magazine_Price decimal, @Comments varchar(255) AS
begin

insert into Advertising_Support(Advertising_Support_Type_ID, Title, Publishnig_Date, Web_Site, Ordering_Phone_Number, Periodicity, Nb_Draw, Magazine_Price, Comments) values(@Advertising_Support_Type_ID, @Title, @Publishnig_Date, @Web_Site, @Ordering_Phone_Number, @Periodicity, @Nb_Draw, @Magazine_Price, @Comments)

select @Advertising_Support_ID = SCOPE_IDENTITY()

end
GO
