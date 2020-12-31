USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_CampaignProgram_ContentCatalogCode]    Script Date: 06/07/2017 09:33:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE       PROCEDURE [dbo].[pr_get_CampaignProgram_ContentCatalogCode]
  @CampaignID int,
  @Country varchar(2) = null,
  @ShowUnSelected bit = 1
AS

--get the default
IF ( (@Country IS NULL) OR ( ltrim(rtrim(@Country)) = '') )
BEGIN
	SELECT @Country = Country FROM Campaign WHERE ID = @CampaignID
END

SET NOCOUNT ON
DECLARE @PreviousProgram int
SELECT @PreviousProgram = -5
DECLARE @ProgramID int
DECLARE @Content_Catalog_Code varchar(20)

DECLARE @FSOrderRecCreated bit
SELECT @FSOrderRecCreated = isnull(FSOrderRecCreated, 0)
FROM Campaign 
WHERE Campaign.[ID] = @CampaignID

CREATE TABLE #Catalogs
(
  ProgramID int NOT NULL,
  Content_Catalog_Code varchar(500) NOT NULL
);


DECLARE CCCode_Cursor CURSOR FOR 
SELECT 
	Content_Catalog_Code + ';' as Content_Catalog_Code,
	ProgramId
  FROM 
	CampaignToContentCatalog 
 WHERE 
	CampaignID = @CampaignID
	AND DeletedTF <> 1
ORDER BY 
	ProgramId ASC

OPEN CCCode_Cursor
FETCH NEXT FROM CCCode_Cursor INTO @Content_Catalog_Code, @ProgramID

WHILE(@@fetch_status <> -1)
BEGIN
	IF @ProgramID <> @PreviousProgram
	BEGIN
		SELECT @PreviousProgram = @ProgramID;
		INSERT INTO #Catalogs VALUES(@ProgramID, @Content_Catalog_Code);
	END
	ELSE
	BEGIN
		UPDATE #Catalogs
		SET Content_Catalog_Code = Content_Catalog_Code + @Content_Catalog_Code
		WHERE ProgramID = @ProgramID;
	END

	--GET THE NEXT ONE
	FETCH NEXT FROM CCCode_Cursor INTO @Content_Catalog_Code, @ProgramID	
END
CLOSE CCCode_Cursor
DEALLOCATE CCCode_Cursor
SET NOCOUNT OFF


SELECT * FROM (
select 
	CAST(1 as bit) AS Selected,
	a.ProgramID,
	c.Name AS ProgramName, 
	case upper(a.IsPreCollect)
		when 'Y' then cast(1 as bit)
		when 'N' then cast(0 as bit)
		else a.IsPreCollect
	end AS IsPreCollect,
	a.GroupProfit,
	ISNULL(b.Content_Catalog_Code,'') AS Content_Catalog_Code,
	@FSOrderRecCreated AS FSOrderRecCreated
  from 
	CampaignProgram a left join #Catalogs b 
		on a.ProgramID = b.ProgramID
	left join Program c
		on a.ProgramID = c.ID
 WHERE 
 	a.CampaignID = @CampaignID
	AND DeletedTF <> 1
union
select 
	CAST(0 as bit) AS Selected,
	ID     AS ProgramID,
	[Name] AS ProgramName,
	CAST(0 AS bit) AS IsPreCollect,
	DefaultProfit AS GroupProfit,
	''   AS Content_Catalog_Code,
	@FSOrderRecCreated AS FSOrderRecCreated
from 
	program
where 
	@ShowUnSelected = 1
	AND country = @Country
	AND ID NOT IN ( SELECT distinct(ProgramID) FROM CampaignProgram WHERE CampaignID = @CampaignID AND DeletedTF <> 1)
	AND ActiveForFiscal_TF <> 0
) TotalTable
ORDER BY TotalTable.Selected DESC

drop table #Catalogs
GO
