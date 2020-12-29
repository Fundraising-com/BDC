USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_validate_member]    Script Date: 02/14/2014 13:08:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION 
[dbo].[es_validate_member] (@member_id int
,@email_address varchar(256), @partner_id int, @external_member_id   varchar(128))

RETURNS @T TABLE
   (
   	member_id	int,
	validate_state	int
   )
AS

/*
	return validate_state are
	0 = OK
	1 = Email Address and Partner id already exists into the table member
	2 = External member id already exists
*/

BEGIN

DECLARE @lcmember_id int
DECLARE @lcstatus int
DECLARE @oldEmail varchar(256)

set @lcstatus = 0


	if(@member_id = null) 
	begin
		-- INSERT MEMBER VALIDATION SECTION
		-- if member id, it means this operation is an insert

		-- check if the external member id already exists in the member table
		if(@external_member_id != null) 
		begin
			set @lcmember_id = null
			select @lcmember_id = member_id 
			from member 
			where 
				external_member_id = @external_member_id
				and 
				@partner_id is not null and partner_id = @partner_id
			if (@lcmember_id is not null)
			begin
				-- there is another member using the same partner id and external_member_id
				set @lcstatus = 2
				INSERT INTO @T (member_id, validate_state)
				VALUES (@lcmember_id, @lcstatus )
				return
			end
		end

		-- check if the member is already in the table		
		set @lcmember_id = null
		select @lcmember_id = member_id 
		from member 
		where 
			@email_address is not null and email_address = @email_address 
			and @partner_id is not null and partner_id = @partner_id
			--and deleted = 0
			and user_id is not null
		
		if (@lcmember_id IS NOT NULL)
		begin
			-- there is another member using the same first name, last name, email address and partner id in the table member			
			set @lcstatus = 1
			INSERT INTO @T (member_id, validate_state)
			VALUES (@lcmember_id,@lcstatus )
			return
		end
	end
	else
	begin
		-- UPDATE MEMBER VALIDATION SECTION
		-- if member id is not null, it means this operation is an update

		-- check if the external member id already exists in the member table
		if(@external_member_id != null) 
		begin
			set @lcmember_id = @member_id
			if(exists(select member_id 
			from member 
			where 
				external_member_id = @external_member_id
				and @partner_id is not null and partner_id = @partner_id
				and member_id != @member_id
			))
			begin
				-- there is another member using the same partner id and external_member_id
				set @lcstatus = 2
				INSERT INTO @T (member_id, validate_state)
				VALUES (@lcmember_id,@lcstatus )
				return
			end
		end

		-- check if the member is already in the table
		set @lcmember_id = @member_id

		select @oldEmail = email_address from member where member_id = @member_id
		if(@oldEmail <> @email_address)
		begin
			-- Does email_address will be modified ?
			if(exists (select email_address from member 
			           where member_id != @member_id 
			             and email_address = @email_address
			             and @partner_id is not null and partner_id = @partner_id
			             and user_id is not null
			             --and deleted = 0 
			             /*and create_date > '2006-12-11'*/) )
			begin
				set @lcstatus = 1
				INSERT INTO @T (member_id, validate_state)
				VALUES (@lcmember_id,@lcstatus )
				return
			end
		end
	end


INSERT INTO @T (member_id, validate_state)
VALUES (@lcmember_id, @lcstatus )

return
	
END
GO
