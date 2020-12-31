USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[GetUserPermissions]    Script Date: 06/07/2017 09:33:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserPermissions]
	@ProfileID int = null,
	@PName varchar(30) = null
as

--Decide action based on parameters
if @ProfileID is null and @PName is null
begin
	--Get a list of permission names
	  select * 
	    from dbo.UserPermissionsDef
	   where DeletedTF <> 1
	order by PName asc
end
else if @ProfileID is null
begin
	 --Get a list of users with permission for PName
	  select * 
	    from dbo.UserPermissions
	   where DeletedTF <> 1 and PName = @PName
	order by ProfileId asc
end
else if @PName is null
begin
	 --Get a list of permissions held by user with ProfileID
	  select * 
	    from dbo.UserPermissions
	   where DeletedTF <> 1 and ProfileId = @ProfileID
	order by PName asc
end
else
begin
	 --Check whether user with ProfileID holds permission PName
	if @PName in (
		 --Do we have permission?
		select PName 
		  from dbo.UserPermissions
		 where DeletedTF <> 1 
			and ProfileId = @ProfileID
		)
	begin
		return 1
	end
	else
	begin
		return 0
	end
end
GO
