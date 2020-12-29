USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_control_type_by_id]    Script Date: 02/14/2014 13:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Control_type
CREATE PROCEDURE [dbo].[efrstore_get_control_type_by_id] @Control_type_id int AS
begin

select Control_type_id, Assembly_name, Namespace, Class_name, Display_attribute, Binding_name, Event_handler_name, Auto_post_back, Datestamp from Control_type where Control_type_id=@Control_type_id

end
GO
