USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_llu_group_imports]    Script Date: 02/14/2014 13:08:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_llu_group_imports] AS
begin

select External_group_id, Group_name, Sponsor_name, Address, City, State, Zip, Password, Email, Country, Phone, Member_count from Llu_group_import

end
GO
