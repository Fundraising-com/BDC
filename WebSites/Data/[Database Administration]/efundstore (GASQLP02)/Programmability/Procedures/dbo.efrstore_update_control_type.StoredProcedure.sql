USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_control_type]    Script Date: 02/14/2014 13:06:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Control_type
CREATE PROCEDURE [dbo].[efrstore_update_control_type] @Control_type_id int, @Assembly_name varchar(200), @Namespace varchar(200), @Class_name varchar(100), @Display_attribute varchar(100), @Binding_name varchar(100), @Event_handler_name varchar(100), @Auto_post_back bit, @Datestamp datetime AS
begin

update Control_type set Assembly_name=@Assembly_name, Namespace=@Namespace, Class_name=@Class_name, Display_attribute=@Display_attribute, Binding_name=@Binding_name, Event_handler_name=@Event_handler_name, Auto_post_back=@Auto_post_back, Datestamp=@Datestamp where Control_type_id=@Control_type_id

end
GO
