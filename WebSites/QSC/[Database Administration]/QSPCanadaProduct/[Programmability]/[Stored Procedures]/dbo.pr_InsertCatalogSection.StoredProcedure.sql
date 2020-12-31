USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_InsertCatalogSection]    Script Date: 06/07/2017 09:17:54 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_InsertCatalogSection]
	@iCatalogID		int,
	@zCatalogCode		varchar(50),
	@iType			int,
	@zName		varchar(50),
	@iFSProgramID		int,
	@zUserID		varchar(30)
AS

DECLARE	@iCatalogSectionID	int

create table #temp
(
	 NextInstance int
)
insert into #temp exec qspcanadaordermanagement..InsertNextInstance 21 -- ProgramSectionNext
select @iCatalogSectionID = nextinstance from #temp
truncate table #temp
			
drop table #temp

INSERT INTO	ProgramSection
		(ID,
		Program_ID,
		Type,
		CatalogCode,
		Name,
		DateCreated,
		UserIDCreated,
		DateChanged,
		UserIDChanged)
		VALUES
		(@iCatalogSectionID,
		@iCatalogID,
		@iType,
		@zCatalogCode,
		@zName,
		getdate(),
		@zUserID,
		getdate(),
		@zUserID)

IF(@iType = 3 AND @iFSProgramID <> 0)
BEGIN
	INSERT INTO	ProgFSSectionMap
			(Program_ID,
			Catalog_Section_ID)
	VALUES	(@iFSProgramID,
			@iCatalogSectionID)
END

SELECT @iCatalogSectionID
GO
