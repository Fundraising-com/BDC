USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[xf_create_member]    Script Date: 02/14/2014 13:08:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	
	Created by: Dat Nghiem
	Projects: Esubs v2
	Date: 12 July 2006
	
    Description: Add a member into a temporary table for partner
	
	
*/

CREATE PROC [dbo].[xf_create_member]
   	@external_member_id varchar(128) ,	
   	@external_group_id varchar(128) ,
   	@partner_id int = 0,
	@first_name varchar(100) = NULL,
	@middle_name varchar(100) = NULL,
	@last_name varchar(100) = NULL,
	@email_address varchar(100),
	@culture_code nvarchar(5) = 'en-US',
	@opt_status_id int = 2,
	@creation_channel_id int  = 20,
	@password varchar(100) = NULL,
	@comments varchar(1024) = NULL
AS
BEGIN

	DECLARE @errorCode int
  	DECLARE @member_type_id int
	DECLARE @return_code int
	--DECLARE @already_exists bit
	
	--SET @already_exists = 0
	SET @return_code = 0

	IF EXISTS(SELECT external_member_id 
                    FROM xfactor_member
                    WHERE external_member_id=@external_member_id
		AND external_group_id = @external_group_id
		AND partner_id = @partner_id
	)
	BEGIN
	BEGIN TRANSACTION
			
				UPDATE [xfactor_member]
				SET
				external_member_id = @external_member_id,
				external_group_id = @external_group_id,
			    partner_id = @partner_id,
				first_name = @first_name,
				middle_name = @middle_name,
				last_name = @last_name,
				email_address = @email_address,
				culture_code = @culture_code,
				opt_status_id = @opt_status_id,
				creation_channel_id = @creation_channel_id,
				password = @password,
				comments =@comments,
				created_date = GetDate(),
				deleted = 0
				WHERE external_member_id=@external_member_id
				AND external_group_id = @external_group_id
				AND partner_id = @partner_id				
				SET @errorCode = @@error
			
				IF (@errorCode <> 0)
				BEGIN
			  		ROLLBACK TRANSACTION
					RETURN -3
				END
				
	COMMIT TRANSACTION
	END
	ELSE
	BEGIN
		BEGIN TRANSACTION
			
				INSERT INTO [xfactor_member]
				(
					external_member_id ,	
				    	external_group_id ,
				    	partner_id ,
					first_name ,
					middle_name ,
					last_name ,
					email_address ,
					culture_code ,
					opt_status_id ,
					creation_channel_id ,
					password ,
					comments ,
					created_date
				)
				VALUES
				(
					@external_member_id ,	
				    @external_group_id,
				    @partner_id ,
					@first_name ,
					@middle_name ,
					@last_name ,
					@email_address ,
					@culture_code ,
					@opt_status_id ,
					@creation_channel_id ,
					@password ,
					@comments
					, GETDATE()
				)
				
				SET @errorCode = @@error
			
				IF (@errorCode <> 0)
				BEGIN
			  		ROLLBACK TRANSACTION
					RETURN -3
				END
				
		COMMIT TRANSACTION
	END
	
	RETURN @return_code
END
GO
