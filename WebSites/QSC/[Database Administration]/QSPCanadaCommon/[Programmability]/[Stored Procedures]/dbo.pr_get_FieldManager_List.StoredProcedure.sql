USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_FieldManager_List]    Script Date: 06/07/2017 09:33:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_get_FieldManager_List] 
  @mode int
AS

if (@mode = 1) or (@mode = 4)
begin
		--from pr_get_FieldManagerNameList 
	SELECT
		[FMID],
		[FirstName],
		[LastName],
		[MiddleInitial],
		[Email]
	  FROM 
		[dbo].[FieldManager]
	 WHERE
		[DeletedTF] <> 1
	ORDER BY
		[LastName] ASC
		,[FirstName] ASC
		,[MiddleInitial] ASC
		,[FMID] ASC
end
else if @mode = 2
begin
	--show all FMs
	--used in the account/campaign transfer screen
	SELECT
		[FMID],
		[FirstName],
		[LastName],
		[MiddleInitial],
		[Email]
	  FROM 
		[dbo].[FieldManager]
	ORDER BY
		 [DeletedTF]		ASC
		,[LastName] 		ASC
		,[FirstName] 		ASC
		,[MiddleInitial] 	ASC
		,[FMID] 		ASC
end
else if @mode = 3
begin
	--selected FMs for Estimated Sales Trial
	SELECT
		[FMID],
		[FirstName],
		[LastName],
		[MiddleInitial],
		[Email]
	  FROM 
		[dbo].[FieldManager]
	 WHERE
		[DeletedTF] <> 1
		AND FMID <> '0514' --Louise Whitlock - removed by request from Carmine 2005.04.21
		AND FMID <> '0018' --Trent Wehrhahn

		--AND FMID IN('0013','0041','0046','0088','0094')
		--(Beth Snelgrove, Jeff May-Melin, Sandy McCarty, 
		--Jill Maslanka, Linda Hartnett)
		--test group

		--AND FMID IN('0017', '0037')
		--('Sheri	Greene', ' 'Warren Galenzoski')--2004.10.28
		--AND FMID = '0509' -- Thierry 2004.10.28 email addr fix

		--AND FMID IN('0005','0013','0094')
		--Garry Montgomery, Beth Snelgrove and Linda Hartnett(2004/12/12 test)

		--AND FMID = '0041' -- Jeff May-Melin (2004/12/19 test)

		--AND FMID = '0035' --Daryl Beamish

		--AND FMID = '0095' --Trish deWitt

		--AND FMID = '0003' --Jim Cougle


		--WFC Exclusion List (they dont want emails since they only sell Chocolate)
		AND FMID NOT IN (
			'0028' --Ken Gardiner
		)

		

	ORDER BY
		[LastName] ASC
		,[FirstName] ASC
		,[MiddleInitial] ASC
		,[FMID] ASC
end
else if @mode = 5
begin
	--selected FMs for QSP.ca Sponsor Report password spreadsheet
	SELECT
		  [FMID]
		, [FirstName]
		, [LastName]
		, [MiddleInitial]
		, [Email]
		, 'fm'
			+ FMID 
			+ '_' 
			+ replace(LastName, ' ', '')
			+ '_'
			+ replace(convert(varchar(30), getdate(), 102), '.','_')
			+ '.xls'
		  as [xlsFileName]
	  FROM 
		[dbo].[FieldManager]
	 WHERE
		[DeletedTF] <> 1
		AND FMID <> '0514' --Louise Whitlock
		AND FMID <> '0018' --Trent Wehrhahn

--		--Exclusion List (they dont want emails for whatever reason)
--		AND FMID NOT IN (
--			'' --FM Name
--		)
	ORDER BY
		[LastName] ASC
		,[FirstName] ASC
		,[MiddleInitial] ASC
		,[FMID] ASC
end
GO
