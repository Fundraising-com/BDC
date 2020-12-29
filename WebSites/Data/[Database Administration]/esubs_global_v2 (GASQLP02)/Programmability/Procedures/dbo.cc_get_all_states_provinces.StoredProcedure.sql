USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_get_all_states_provinces]    Script Date: 02/14/2014 13:04:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

*/
CREATE    PROCEDURE [dbo].[cc_get_all_states_provinces]
	@strCountryCode VARCHAR(10) = null
AS
BEGIN

if @strCountryCode is not null
SELECT     dbo.country.country_code,
           right(dbo.subdivision.subdivision_code,2) as state_code,
           dbo.subdivision.subdivision_code,
           dbo.subdivision.subdivision_name_1 as state_name
FROM       dbo.country INNER JOIN
           dbo.subdivision ON dbo.country.country_code = dbo.subdivision.country_code
where      lower(dbo.country.country_code) = lower(@strCountryCode)
order by   dbo.country.country_code desc,  right(dbo.subdivision.subdivision_code,2) asc

else


SELECT     dbo.country.country_code,
           right(dbo.subdivision.subdivision_code,2) as state_code,
           dbo.subdivision.subdivision_code,
           dbo.subdivision.subdivision_name_1 as state_name
FROM       dbo.country INNER JOIN
           dbo.subdivision ON dbo.country.country_code = dbo.subdivision.country_code
order by   dbo.country.country_code desc,  right(dbo.subdivision.subdivision_code,2) asc


end
GO
