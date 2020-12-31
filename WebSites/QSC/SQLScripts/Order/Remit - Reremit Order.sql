USE QSPCanadaOrderManagement


DECLARE @CustomerOrderHeaderInstance	INT,
		@TransID						INT

SET		@CustomerOrderHeaderInstance = 9872029
SET		@TransID = 2

SELECT	*
FROM	CustomerOrderDetailRemitHistory
WHERE	CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance

EXEC	pr_Remit_ReRemitSubsByCOD
		@CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance,
		@TransID = @TransID

SELECT	*
FROM	CustomerOrderDetailRemitHistory
WHERE	CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
AND		TransID = @TransID
