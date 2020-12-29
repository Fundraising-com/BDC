USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_states_by_country_code]    Script Date: 02/14/2014 13:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for State
CREATE PROCEDURE [dbo].[efrcrm_get_states_by_country_code] @Country_Code varchar(10) AS
begin

select State_Code, State_Name, Avg_Delivery_Days, Time_Zone_Difference, Country_Code 
from State
where Country_Code= @Country_Code

end
GO
