USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[NewID2]    Script Date: 02/14/2014 13:08:46 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     PROCEDURE [dbo].[NewID2] @sIDName CHAR(50), @sContext CHAR(50), @myNewID int OUTPUT 
AS/*
	DECLARE @IDNumber Integer;
	DECLARE @LastValue Integer;
	DECLARE rsID CURSOR FOR 
		SELECT Last_Value FROM IDGen_Table WHERE ID_Name = @sIDName AND Context = @sContext;
	
	OPEN rsID;
	FETCH NEXT FROM rsID INTO @LastValue;
	IF @@FETCH_STATUS = 0 
		SET @myNewID = @LastValue + 1;
	CLOSE rsID;
	DEALLOCATE rsID;

	UPDATE IDGen_Table SET Last_Value = @myNewID WHERE (ID_Name = @sIDName AND Context = @sContext);


	
--	SELECT Max(Last_Value) FROM IDGen_Table WHERE (ID_Name = @sIDName AND Context = @sContext);
	--SELECT @myNewID
        RETURN @@ERROR*/

BEGIN TRANSACTION
	DECLARE @LastValue Integer	
	SELECT 
		@LastValue = Last_Value+1 
	FROM 
		IDGen_Table with(xlock)--met un lock sur la ressource pour la durée de la transaction	Fblais
	WHERE 
		ID_Name = @sIDName 
	AND 	Context = @sContext
	
	if @@error = 0
	begin
		UPDATE 
			IDGen_Table 
		SET 
			Last_Value = @LastValue 
		WHERE 
			ID_Name = @sIDName 
		AND 	Context = @sContext
	end
	else
	begin
		rollback transaction
	end

	if @@error = 0
	begin
		commit transaction
		set  @myNewID = @LastValue 
		select @@error
		RETURN @@error
	end
	else
	begin
		rollback transaction
		select @@error
		RETURN @@error
	end
GO
