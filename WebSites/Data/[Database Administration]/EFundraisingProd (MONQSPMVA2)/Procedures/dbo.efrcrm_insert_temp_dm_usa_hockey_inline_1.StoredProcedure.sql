USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_temp_dm_usa_hockey_inline_1]    Script Date: 02/14/2014 13:07:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Temp_dm_usa_hockey_inline_1
CREATE PROCEDURE [dbo].[efrcrm_insert_temp_dm_usa_hockey_inline_1] @Id int OUTPUT, @Compagnie varchar(200), @Contact varchar(200), @Address1 varchar(200), @Address2 varchar(200), @City varchar(200), @State varchar(200), @Zip varchar(200), @Phone varchar(200), @Ext varchar(200) AS
begin

insert into Temp_dm_usa_hockey_inline_1(Compagnie, Contact, Address1, Address2, City, State, Zip, Phone, Ext) values(@Compagnie, @Contact, @Address1, @Address2, @City, @State, @Zip, @Phone, @Ext)

select @Id = SCOPE_IDENTITY()

end
GO
