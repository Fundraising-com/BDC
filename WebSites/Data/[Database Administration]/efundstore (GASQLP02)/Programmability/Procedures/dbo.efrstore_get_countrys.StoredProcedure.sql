USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_countrys]    Script Date: 02/14/2014 13:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Country
CREATE PROCEDURE [dbo].[efrstore_get_countrys] AS
begin

select Country_code, Name, Descriptive_information, Display from Country

end
GO
