USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[NewID]    Script Date: 02/14/2014 13:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/****** Object:  Stored Procedure dbo.NewID    Script Date: 2003-02-22 20:34:54 ******/



CREATE   PROCEDURE [dbo].[NewID](@sIDName CHAR(50), @sContext CHAR(50)) 
AS
	/*DECLARE @IDNumber Integer;
	DECLARE @LastValue Integer;
	DECLARE @IDToReturn Integer;
	DECLARE rsID CURSOR FOR 
		SELECT Last_Value FROM IDGen_Table WHERE ID_Name = @sIDName AND Context = @sContext;
	
	OPEN rsID;
	FETCH NEXT FROM rsID INTO @LastValue;
	IF @@FETCH_STATUS = 0 
		SET @IDToReturn = @LastValue + 1;
	CLOSE rsID;
	DEALLOCATE rsID;

	UPDATE IDGen_Table SET Last_Value = @IDToReturn WHERE (ID_Name = @sIDName AND Context = @sContext);


	--RETURN(@IDToReturn)

--	SELECT Max(Last_Value) FROM IDGen_Table WHERE (ID_Name = @sIDName AND Context = @sContext);
	SELECT @IDToReturn*/

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
		SELECT @LastValue
		RETURN(@LastValue)	
	end
	else
	begin
		rollback transaction
		select -1
		RETURN( -1 )
	end
GO
