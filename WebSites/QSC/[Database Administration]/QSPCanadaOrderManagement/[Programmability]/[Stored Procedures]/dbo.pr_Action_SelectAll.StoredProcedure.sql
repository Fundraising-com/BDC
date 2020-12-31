USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Action_SelectAll]    Script Date: 06/07/2017 09:19:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'Action'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Action_SelectAll]

@iCustomerOrderHeaderInstance 	int,
@iTransID				int,
@iCreditCardAction			int = 0

AS

DECLARE	@iActionInstance 	int,
	     	@iIsValid		int


CREATE TABLE #t (
Instance int,
Description varchar(50),
ReponsibleDeptInstance int,
IsNotifyPublisherPrint bit ,
IsActionUserUpdatable bit,
Message nvarchar(200),
CommentsIsRequired bit,
DisplayOrder int

)

if(@iCustomerOrderHeaderInstance <> 0)
BEGIN
	DECLARE c1 CURSOR FOR SELECT Instance FROM Action
	
		OPEN c1
		FETCH NEXT FROM c1 INTO @iActionInstance
			WHILE @@FETCH_STATUS = 0
			BEGIN
				
				EXEC pr_IsValidAction @iActionInstance, @iCustomerOrderHeaderInstance, @iTransID, @iCreditCardAction, @iIsValid OUTPUT
	
				IF @iIsValid = 1
					INSERT INTO #t  SELECT Instance, [Description], ReponsibleDeptInstance, IsNotifyPublisherPrint, IsActionUserUpdatable, Message, CommentsIsRequired, DisplayOrder FROM Action WHERE Instance = @iActionInstance
				
				print 'Action Instance' 
				print  @iActionInstance
				print 'IsValid' 
				print @iIsValid
				FETCH NEXT FROM c1 INTO @iActionInstance
			END
		CLOSE c1
	DEALLOCATE c1
	
	SELECT * FROM #t ORDER BY DisplayOrder
	DROP TABLE #t
END
else
BEGIN
	SELECT Instance, [Description], ReponsibleDeptInstance, IsNotifyPublisherPrint, IsActionUserUpdatable, Message, CommentsIsRequired, DisplayOrder FROM Action ORDER BY DisplayOrder
END
GO
