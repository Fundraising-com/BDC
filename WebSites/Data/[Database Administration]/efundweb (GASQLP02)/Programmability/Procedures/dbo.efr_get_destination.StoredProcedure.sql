USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_get_destination]    Script Date: 02/14/2014 13:04:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efr_get_destination]
	@destination_id int
AS
SELECT
	[Destination_ID],
	[Web_Site_ID],
	[URL]

FROM
	efundweb.dbo.destinations
WHERE 
	destination_id = @destination_id
GO
