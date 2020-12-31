USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_InsertProductCategory]    Script Date: 06/07/2017 09:17:55 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_InsertProductCategory]

	@zDescription	varchar(64)

AS

	DECLARE @iNewCategoryID	int

	create table #temp
	(
		 NextInstance int
	)
	
	insert into #temp exec qspcanadaordermanagement..InsertNextInstance 27 -- CategoryNext
	select @iNewCategoryID = nextinstance from #temp
	truncate table #temp
			
	drop table #temp


	INSERT INTO QSPCanadaCommon..CodeDetail
	(Instance,
	CodeHeaderInstance,
	Description,
	Gross,
	ADPCode)
	VALUES
	(@iNewCategoryID,
	30700,
	@zDescription,
	0,
	'')

	SELECT @iNewCategoryID
GO
