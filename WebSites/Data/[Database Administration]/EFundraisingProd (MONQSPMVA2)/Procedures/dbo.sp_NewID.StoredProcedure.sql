USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_NewID]    Script Date: 02/14/2014 13:09:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*================================================================
   Description: SP_NewID
		Cette procédure a été créée afin de complémenter
		la NewID existante afin d'avoir un return
   Créé le: 06/03/03
   Créé par: JF Brisebois
   Demandé par: Melissa
   Référence: C3	
   MOd: 	Fblais 2005-04-22 Add transaction, removed cursor
================================================================*/

CREATE  PROCEDURE [dbo].[sp_NewID](@sIDName CHAR(50), @sContext CHAR(50)) 
AS

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
