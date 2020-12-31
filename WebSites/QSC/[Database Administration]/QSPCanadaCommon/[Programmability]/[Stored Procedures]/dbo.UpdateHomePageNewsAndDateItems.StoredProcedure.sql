USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[UpdateHomePageNewsAndDateItems]    Script Date: 06/07/2017 09:33:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[UpdateHomePageNewsAndDateItems]
	@Mode int,
	@id int = null,
	@displayfrom datetime = null,
	@displayto datetime = null,
	@startdate datetime = null,
	@enddate datetime = null,
	@title varchar(100) = null,
	@text varchar(400) = null,
	@color varchar(10) = null,
	@weight smallint = 0,
	@Delete bit = 0
as
/*Modes (Designated by @Mode)
0 - Date Information
1 - News Information
*/
if @Mode = 0 -- Date information
begin
	--Update or delete?
	if @Delete <> 0 --Delete
	begin
		update QSPCanadaCommon.dbo.HomePageDateItems
			set DeletedTF = 1
			where id = @id
	end
	else --Update
	begin
		if @id <> null
		begin
			update QSPCanadaCommon.dbo.HomePageDateItems
				SET
					displayfrom = @displayfrom,
					displayto = @displayto,
					startdate = @startdate,
					enddate = @enddate,
					text = @text
				WHERE id = @id
		end
		else
		begin
			insert into QSPCanadaCommon.dbo.HomePageDateItems
				(displayfrom, displayto, startdate, enddate, text)
				values
				(@displayfrom, @displayto, @startdate, @enddate, @text)
		end
	end
end
else --News information
begin
	--Update or delete?
	if @Delete <> 0 --Delete
	begin
		update QSPCanadaCommon.dbo.HomePageNewsItems
			set DeletedTF = 1
			where id = @id
	end
	else --Update
	begin
		if @id is not null
		begin
			update QSPCanadaCommon.dbo.HomePageNewsItems
				SET
					startdate = @startdate,
					enddate = @enddate, 
					title = @title,
					text = @text,
					color = @color,
					weight = @weight
				WHERE id = @id
		end
		else
		begin
			insert into QSPCanadaCommon.dbo.HomePageNewsItems
				(startdate, enddate, title, text, color, weight)
				values
				(@startdate, @enddate, @title, @text, @color, @weight)
		end
	end
end
GO
