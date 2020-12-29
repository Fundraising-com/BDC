USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[fr_store_insert_representative]    Script Date: 02/14/2014 13:08:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: 29/11/2013
-- Description:	Inserts a new representative
-- =============================================
CREATE PROCEDURE [dbo].[fr_store_insert_representative]
	@Name VARCHAR(30),
	@Email VARCHAR(40),
	@Account INT,
	@External_ID INT
AS
BEGIN
	
	SET NOCOUNT ON;

    BEGIN TRAN
    
    INSERT INTO dbo.consultant	
        ( consultant_id ,
          division_id ,
          client_id ,
          client_sequence_code ,
          department_id ,
          partner_id ,
          consultant_transfer_status_id ,
          territory_id ,
          ext_consultant_id ,
          name ,
          email_address,
          is_agent ,
          is_active ,
          csr_consultant ,
          objectives ,
          is_available ,
          password ,
          kit_paid ,
          is_fm ,
          create_date)        
	VALUES 
	(@External_ID, 1, NULL, NULL, 9, 857, 1, NULL, @Account, @Name,@email, 0,1,0,0,0,'pass',0,1,GETDATE())
    
    update [eFundraisingProd].[dbo].[IDGen_Table]
	  set Last_Value = @External_ID
	  where context ='All' and ID_Name = 'Consultant_id' and Comment = 'none'
    COMMIT
END
GO
