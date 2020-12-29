USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_crm_static_past3seasons_tbd]    Script Date: 02/14/2014 13:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Crm_static_past3seasons_tbd
CREATE PROCEDURE [dbo].[efrcrm_update_crm_static_past3seasons_tbd] @Crm_static_past3seasons_id int, @AccountInstance int, @AccountName varchar(50), @Total_Sold decimal, @Zzzzzaa99 varchar(9), @Zzzzz varchar(5), @Aa99 varchar(4), @FmID varchar(4), @Status int, @Email varchar(50), @FirstName varchar(20), @LastName varchar(30), @HomePhone varchar(20), @WorkPhone varchar(20), @MobilePhone varchar(20) AS
begin

update Crm_static_past3seasons_tbd set AccountInstance=@AccountInstance, AccountName=@AccountName, Total_Sold=@Total_Sold, Zzzzzaa99=@Zzzzzaa99, Zzzzz=@Zzzzz, Aa99=@Aa99, FmID=@FmID, Status=@Status, Email=@Email, FirstName=@FirstName, LastName=@LastName, HomePhone=@HomePhone, WorkPhone=@WorkPhone, MobilePhone=@MobilePhone where Crm_static_past3seasons_id=@Crm_static_past3seasons_id

end
GO
