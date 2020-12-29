USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_question]    Script Date: 02/14/2014 13:06:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Question
CREATE PROCEDURE [dbo].[efrstore_insert_question] @Question_id int OUTPUT, @Name varchar(100), @Description varchar(600), @Control_type_id int, @Field_name varchar(100), @Default_value varchar(100), @Min_lenght int, @Max_lenght int, @Nbr_value int, @Datestamp datetime, @Stored_proc_to_call varchar(200), @Answer_type varchar(20), @Field_value varchar(100) AS
begin

insert into Question(Name, Description, Control_type_id, Field_name, Default_value, Min_lenght, Max_lenght, Nbr_value, Datestamp, Stored_proc_to_call, Answer_type, Field_value) values(@Name, @Description, @Control_type_id, @Field_name, @Default_value, @Min_lenght, @Max_lenght, @Nbr_value, @Datestamp, @Stored_proc_to_call, @Answer_type, @Field_value)

select @Question_id = SCOPE_IDENTITY()

end
GO
