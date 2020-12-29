USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_question]    Script Date: 02/14/2014 13:06:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Question
CREATE PROCEDURE [dbo].[efrstore_update_question] @Question_id int, @Name varchar(100), @Description varchar(600), @Control_type_id int, @Field_name varchar(100), @Default_value varchar(100), @Min_lenght int, @Max_lenght int, @Nbr_value int, @Datestamp datetime, @Stored_proc_to_call varchar(200), @Answer_type varchar(20), @Field_value varchar(100) AS
begin

update Question set Name=@Name, Description=@Description, Control_type_id=@Control_type_id, Field_name=@Field_name, Default_value=@Default_value, Min_lenght=@Min_lenght, Max_lenght=@Max_lenght, Nbr_value=@Nbr_value, Datestamp=@Datestamp, Stored_proc_to_call=@Stored_proc_to_call, Answer_type=@Answer_type, Field_value=@Field_value where Question_id=@Question_id

end
GO
