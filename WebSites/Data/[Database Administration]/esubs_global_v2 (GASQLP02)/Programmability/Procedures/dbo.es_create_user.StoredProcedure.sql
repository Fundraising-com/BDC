USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_user]    Script Date: 02/14/2014 13:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	es_create_user
	
	Created by: JF Buist
	Projects: Esubs v2
	Date: May 2006
	
    Description: Insert member value to table member
*/

CREATE   PROC [dbo].[es_create_user]
	@culture_code nvarchar(5) = 'en-US',
	@opt_status_id int = 2,
	@first_name  varchar(50),
	@middle_name  varchar(50),
	@last_name  varchar(50),
	@gender char(1) = null,
	@email_address varchar(100),
	@password  varchar(100),
	@bounced bit,
	@external_member_id  varchar(100),
	@comments varchar(1024), 
	@create_date datetime,
	@parent_first_name varchar(100),
	@parent_last_name  varchar(100),
	@partner_id int,
	@lead_id int,
	@facebook_id int = NULL,
	@member_id int OUTPUT 
AS
BEGIN

-- error code declaration
DECLARE @memberErrorCode int
DECLARE @errorCode int

-- validate the member
--set @memberErrorCode = dbo.es_validate_member(null, @first_name, @last_name, @email_address, @partner_id, @external_member_id)

SELECT @memberErrorCode = validate_state
	FROM dbo.es_validate_member(null ,  @email_address , @partner_id, @external_member_id)


if( @memberErrorCode = 0) 	-- Validation of member is successfull (do not exists in the member table)
begin
	-- insert the new member into the member table
    	INSERT INTO member
    	(
    		culture_code
    		, opt_status_id
    		, first_name
    		, middle_name
    		, last_name
    		, gender
    		, email_address
    		, password
    		, bounced
			, parent_first_name
	    	, parent_last_name
    		, comments
    		, external_member_id
    		, create_date
            , partner_id
			, lead_id
			, facebook_id
    	)
    	VALUES
    	(
    		@culture_code
    		, @opt_status_id
    		, @first_name
    		, @middle_name
    		, @last_name
    		, @gender
    		, @email_address
    		, @password
    		, @bounced
			, @parent_first_name
			, @parent_last_name
    		, @comments
			, @external_member_id
    		, GETDATE()
           	, @partner_id
			, @lead_id
			, @facebook_id
    	)

	-- retreive the internal error code    	
    	SET @errorCode = @@error
    	
	-- rollback if any internal problem
    	IF (@errorCode <> 0)
    	BEGIN
    		RETURN @errorCode
    	END
    	
	-- set the member_id which will be inserted into member hierarchy table
    	SELECT @member_id = SCOPE_IDENTITY()
end

return @memberErrorCode

END
GO
