USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_stats_personalization_section]    Script Date: 02/14/2014 13:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Stats_personalization_section
CREATE PROCEDURE [dbo].[es_insert_stats_personalization_section] @Stats_personalization_section_id int OUTPUT, @Description varchar(50), @Create_date datetime AS
begin

insert into Stats_personalization_section(Description, Create_date) values(@Description, @Create_date)

select @Stats_personalization_section_id = SCOPE_IDENTITY()

end
GO
