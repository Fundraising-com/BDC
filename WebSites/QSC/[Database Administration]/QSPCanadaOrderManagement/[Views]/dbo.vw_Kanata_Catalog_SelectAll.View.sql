USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_Kanata_Catalog_SelectAll]    Script Date: 06/07/2017 09:18:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vw_Kanata_Catalog_SelectAll]

AS
--temporary:
SELECT	*
FROM	QSPcanadaProduct..Program_Master master
WHERE	Program_ID in (97, 98)

/*Declare @start Datetime
Declare @end Datetime

SELECT	@Start=Startdate,@end=Enddate
FROM	[QSPCanadaCommon].[dbo].[Season]
WHERE	Convert(varchar(10),Getdate(),101) between StartDate and EndDate
AND		Season <> 'Y'

--todo: create a kanata field and select those with it
SELECT	DISTINCT
		master.Code,
		master.Program_Type,
		master.Status AS StatusID
FROM	QSPcanadaProduct..Program_Master master,
		QSPCanadaProduct..ProgramSection ps,
		QSPCanadaCommon..Season s
WHERE	s.ID = master.Season
AND		master.Program_ID = ps.Program_ID
AND		ps.type <> 2	--Magazines
AND		master.Status = 30403 --Approved
--and s.ID = 18
AND		s.startdate between @Start and @end
AND	*/
GO
