USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[fr_store_insert_representative]    Script Date: 02/14/2014 13:06:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: 29/11/213
-- Description:	Inserts a new Rep into the FC table, returns the External ID generated on the transaction
-- =============================================
CREATE PROCEDURE [dbo].[fr_store_insert_representative]
	@Name VARCHAR(30), 
	@Email VARCHAR(40),
	@Redirect VARCHAR(30),
	@City VARCHAR(20),
	@STATE VARCHAR(20),
	@ImageUrl VARCHAR(40),
	@Phone VARCHAR(40),
	@Account INT,
	@Ammount FLOAT
AS
BEGIN
	
	SET NOCOUNT ON;
	BEGIN TRAN
	DECLARE @ext_id INT
	SET @ext_id = (SELECT MAX(ext_id) + 1 FROM FC)
	
    INSERT INTO dbo.FC	
        ( name ,
          ext_id ,
          email_address ,
          active ,
          login ,
          url ,
          city ,
          state ,
          image_url ,
          phone ,
          esubs_parnter_id ,
          SAPAccountNo,
          profit_raised
        )
		VALUES(        
		@Name
		,@ext_id
		,@email
		,1
		,@redirect
		,null
		,@City
		,@State
		,@imageUrl
		,@phone
		,857
		,@Account,
		@Ammount)
	
	COMMIT
		
	SELECT @ext_id AS external_id
END
GO
