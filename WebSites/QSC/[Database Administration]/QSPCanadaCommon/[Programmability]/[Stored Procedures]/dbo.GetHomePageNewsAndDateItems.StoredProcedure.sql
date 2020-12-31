USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[GetHomePageNewsAndDateItems]    Script Date: 06/07/2017 09:33:09 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[GetHomePageNewsAndDateItems]
	@Mode int,
	@ShowAll bit = 0
as

/*Modes (Designated by @Mode)
0 - Date Information
1 - News Information
*/
if @Mode = 0 -- Date information
begin
	if @ShowAll<> 0
	begin
		select * from QSPCanadaCommon.dbo.HomePageDateItems
			where DeletedTF <> 1
			order by id desc
	end
	else
	begin
		select startdate, enddate, text 
			from QSPCanadaCommon.dbo.HomePageDateItems 
			where
				getdate() between displayfrom and displayto 
				and DeletedTF <> 1 
			order by startdate desc
	end
end
else --News information
begin
	if @ShowAll <> 0
	begin
		select * from QSPCanadaCommon.dbo.HomePageNewsItems where DeletedTF <> 1 order by id desc
	end
	else
	begin
		select title, text, color from QSPCanadaCommon.dbo.HomePageNewsItems
			where
				getdate() between startdate and enddate and
				DeletedTF <> 1
			order by 
				weight desc,
				startdate desc,
				enddate asc
	end
end
GO
