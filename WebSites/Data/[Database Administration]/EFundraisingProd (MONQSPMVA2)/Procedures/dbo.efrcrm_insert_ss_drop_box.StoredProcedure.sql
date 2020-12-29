USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_ss_drop_box]    Script Date: 02/14/2014 13:07:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for SS_Drop_Box
CREATE PROCEDURE [dbo].[efrcrm_insert_ss_drop_box] @SS_Drop_Box_Id int OUTPUT, @SS_Drop_Box_Name varchar(50), @Display_Order int AS
begin

insert into SS_Drop_Box(SS_Drop_Box_Name, Display_Order) values(@SS_Drop_Box_Name, @Display_Order)

select @SS_Drop_Box_Id = SCOPE_IDENTITY()

end
GO
