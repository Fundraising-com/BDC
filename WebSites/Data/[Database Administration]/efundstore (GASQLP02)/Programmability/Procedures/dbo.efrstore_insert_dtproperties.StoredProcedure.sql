USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_dtproperties]    Script Date: 02/14/2014 13:05:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Dtproperties
CREATE PROCEDURE [dbo].[efrstore_insert_dtproperties] @Id int OUTPUT, @Objectid int, @Property varchar(64), @Value varchar(255), @Uvalue nvarchar(510), @Lvalue image, @Version int AS
begin

insert into Dtproperties(Objectid, Property, Value, Uvalue, Lvalue, Version) values(@Objectid, @Property, @Value, @Uvalue, @Lvalue, @Version)

select @Id = SCOPE_IDENTITY()

end
GO
