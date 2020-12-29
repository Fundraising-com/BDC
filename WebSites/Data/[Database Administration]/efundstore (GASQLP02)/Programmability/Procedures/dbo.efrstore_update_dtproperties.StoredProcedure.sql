USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_dtproperties]    Script Date: 02/14/2014 13:06:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Dtproperties
CREATE PROCEDURE [dbo].[efrstore_update_dtproperties] @Id int, @Objectid int, @Property varchar(64), @Value varchar(255), @Uvalue nvarchar(510), @Lvalue image, @Version int AS
begin

update Dtproperties set Objectid=@Objectid, Property=@Property, Value=@Value, Uvalue=@Uvalue, Lvalue=@Lvalue, Version=@Version where Id=@Id

end
GO
