USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_temp_dm_usa_hockey_inline_1]    Script Date: 02/14/2014 13:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Temp_dm_usa_hockey_inline_1
CREATE PROCEDURE [dbo].[efrcrm_update_temp_dm_usa_hockey_inline_1] @Id int, @Compagnie varchar(200), @Contact varchar(200), @Address1 varchar(200), @Address2 varchar(200), @City varchar(200), @State varchar(200), @Zip varchar(200), @Phone varchar(200), @Ext varchar(200) AS
begin

update Temp_dm_usa_hockey_inline_1 set Compagnie=@Compagnie, Contact=@Contact, Address1=@Address1, Address2=@Address2, City=@City, State=@State, Zip=@Zip, Phone=@Phone, Ext=@Ext where Id=@Id

end
GO
