USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_next_business_day]    Script Date: 02/14/2014 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Brochures_images
CREATE   PROCEDURE [dbo].[efrcrm_get_next_business_day] --'2007-1-1', 89
                  @date datetime,
                  @duration int  
                    
 
AS
begin


DECLARE @return Datetime
--Modify by F6 18-05-2006
IF @duration = 0
	SET @return = @date
ELSE
	-- Added by Phil on June 27th 2005 to make use of the business_dates table
	BEGIN
		SET @duration = @duration
		-- Added by F6 -- Count + 1 when >= 12:00PM
		--//Remove time part
		SET @date = CONVERT(datetime, CONVERT(varchar(10), @date, 101))
		
		SELECT @return = business_date
		FROM (
		SELECT business_date, 
		    ( SELECT COUNT(*)
		      FROM business_calendar AS c
		      WHERE c.business_date < business_calendar.business_date 
			AND business_date >= @date
			  AND holiday = 0
			  AND weekend = 0
			) AS rowNumber
		FROM business_calendar
		WHERE business_date >= @date
		  AND holiday = 0
		  AND weekend = 0
		) c
		WHERE rowNumber = @Duration
	END

select @return



end
GO
