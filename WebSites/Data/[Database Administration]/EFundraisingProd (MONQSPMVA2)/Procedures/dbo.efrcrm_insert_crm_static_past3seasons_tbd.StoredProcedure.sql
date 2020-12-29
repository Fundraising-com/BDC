USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_crm_static_past3seasons_tbd]    Script Date: 02/14/2014 13:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Crm_static_past3seasons_tbd
CREATE PROCEDURE [dbo].[efrcrm_insert_crm_static_past3seasons_tbd] @Crm_static_past3seasons_id int OUTPUT, @AccountInstance int, @AccountName varchar(50), @Total_Sold decimal, @Zzzzzaa99 varchar(9), @Zzzzz varchar(5), @Aa99 varchar(4), @FmID varchar(4), @Status int, @Email varchar(50), @FirstName varchar(20), @LastName varchar(30), @HomePhone varchar(20), @WorkPhone varchar(20), @MobilePhone varchar(20) AS
begin

insert into Crm_static_past3seasons_tbd(AccountInstance, AccountName, Total_Sold, Zzzzzaa99, Zzzzz, Aa99, FmID, Status, Email, FirstName, LastName, HomePhone, WorkPhone, MobilePhone) values(@AccountInstance, @AccountName, @Total_Sold, @Zzzzzaa99, @Zzzzz, @Aa99, @FmID, @Status, @Email, @FirstName, @LastName, @HomePhone, @WorkPhone, @MobilePhone)

select @Crm_static_past3seasons_id = SCOPE_IDENTITY()

end
GO
