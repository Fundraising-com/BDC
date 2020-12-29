USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_states]    Script Date: 02/14/2014 13:06:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for State
CREATE PROCEDURE [dbo].[efrcrm_get_states] AS
begin

select State_Code, State_Name, Avg_Delivery_Days, Time_Zone_Difference, Country_Code from State

end
GO
