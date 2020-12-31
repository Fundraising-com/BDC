USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_upd_UserPermissions]    Script Date: 06/07/2017 09:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_upd_UserPermissions]
	@ProfileID int = null,
	@PName varchar(30) = null,
	@PDesc varchar(50) = null,
	@Delete bit = 0
as

--Is this an update or a delete?
if @Delete <> 1 --Update
begin

	--Either update a User-Permission pair or update a Permission set
	if @ProfileID is not null --User-Permission pair
	begin
		--Check for previously deleted pair, update if present (auto-reviving deleted items), insert otherwise.
		if exists (select * from dbo.UserPermissions where ProfileId = @ProfileID and PName = @PName)
		begin
			update 
				dbo.UserPermissions
			set
				ModifiedDate = getdate(),
				ModifiedBy = 'FIXME',
				DeletedTF = 0
			where
				ProfileId = @ProfileID and PName = @PName;
		end
		else
		begin
			insert into dbo.UserPermissions(
				ProfileId, 
				PName, 
				CreatedDate, 
				CreatedBy, 
				ModifiedDate, 
				ModifiedBy
			) values (
				@ProfileID, 
				@PName, 
				getdate(), 
				'FIXME', 
				getdate(), 
				'FIXME'
			);
		end
	end
	else --Permission Set
	begin
		--Check for existing set, update if present (auto-reviving deleted items), insert otherwise.
		if exists (select * from dbo.UserPermissionsDef where PName = @PName)
		begin
			update 
				dbo.UserPermissionsDef
			set
				PDesc = @PDesc,
				ModifiedDate = getdate(),
				ModifiedBy = 'FIXME',
				DeletedTF = 0
			where
				PName = @PName
		end
		else
		begin
			insert into dbo.UserPermissionsDef(
				PName, 
				PDesc, 
				CreatedDate, 
				CreatedBy, 
				ModifiedDate, 
				ModifiedBy
			) values (
				@PName, 
				@PDesc, 
				getdate(), 
				'FIXME', 
				getdate(), 
				'FIXME'
			);
		end
	end
end
else --Delete entry
begin
	--Deleting a User-Permission pair or a Permission set?
	if @ProfileID is null --Permission set
	begin
		--Delete the permission set
		update 
			dbo.UserPermissionsDef
		set 
			DeletedTF = 1, 
			ModifiedDate = getdate()
		where 
			PName = @PName
		--Delete all referencing pairs
		update 
			dbo.UserPermissions
		set 
			DeletedTF = 1, 
			ModifiedDate = getdate()
		where 
			PName = @PName;
	end
	else --User-Permission pair
	begin
		update 
			dbo.UserPermissions
		set 
			DeletedTF = 1, 
			ModifiedDate = getdate()
		where 
			ProfileId = @ProfileID 
			and PName = @PName;
	end
end
GO
