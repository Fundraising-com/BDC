USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[GetCurrentSeasonStartAndEndDate]    Script Date: 06/07/2017 09:33:08 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetCurrentSeasonStartAndEndDate] 
					--@startDate datetime OUTPUT,
					--@endDate datetime OUTPUT
AS

Select *
--@startDate=Startdate,@endDate=Enddate
From [QSPCanadaCommon].[dbo].[Season]
Where (	Convert(varchar(10),Getdate(),101) between StartDate and EndDate) 
And Season <>'Y'

--Select @startDate,@endDate
GO
