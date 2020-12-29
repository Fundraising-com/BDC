USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_control_type]    Script Date: 02/14/2014 13:05:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Control_type
CREATE PROCEDURE [dbo].[efrstore_insert_control_type] @Control_type_id int OUTPUT, @Assembly_name varchar(200), @Namespace varchar(200), @Class_name varchar(100), @Display_attribute varchar(100), @Binding_name varchar(100), @Event_handler_name varchar(100), @Auto_post_back bit, @Datestamp datetime AS
begin

insert into Control_type(Assembly_name, Namespace, Class_name, Display_attribute, Binding_name, Event_handler_name, Auto_post_back, Datestamp) values(@Assembly_name, @Namespace, @Class_name, @Display_attribute, @Binding_name, @Event_handler_name, @Auto_post_back, @Datestamp)

select @Control_type_id = SCOPE_IDENTITY()

end
GO
