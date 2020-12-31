USE [QSPCanadaOrderManagement]

DECLARE	@CustomerOrderHeaderInstance	INT
DECLARE @TransID						INT

SET	@CustomerOrderHeaderInstance = 10571753
SET @TransID = 3

SELECT	*
FROM	CustomerOrderDetail cod
WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance

BEGIN TRAN

UPDATE	cod
SET		cod.DelFlag = 1
FROM	CustomerOrderDetail cod
WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
AND		cod.TransID = @TransID

--Temporary, reremit the rest of the subs
DECLARE @RemitStatus INT
EXEC spRemitIndividualItem @CustomerOrderHeaderInstance, 1, @RemitStatus OUT
EXEC spRemitIndividualItem @CustomerOrderHeaderInstance, 2, @RemitStatus OUT
EXEC spRemitIndividualItem @CustomerOrderHeaderInstance, 3, @RemitStatus OUT
EXEC spRemitIndividualItem @CustomerOrderHeaderInstance, 4, @RemitStatus OUT
EXEC spRemitIndividualItem @CustomerOrderHeaderInstance, 5, @RemitStatus OUT
EXEC spRemitIndividualItem @CustomerOrderHeaderInstance, 6, @RemitStatus OUT
EXEC spRemitIndividualItem @CustomerOrderHeaderInstance, 7, @RemitStatus OUT
EXEC spRemitIndividualItem @CustomerOrderHeaderInstance, 8, @RemitStatus OUT
EXEC spRemitIndividualItem @CustomerOrderHeaderInstance, 9, @RemitStatus OUT
EXEC spRemitIndividualItem @CustomerOrderHeaderInstance, 10, @RemitStatus OUT
EXEC spRemitIndividualItem @CustomerOrderHeaderInstance, 11, @RemitStatus OUT
EXEC spRemitIndividualItem @CustomerOrderHeaderInstance, 12, @RemitStatus OUT
EXEC spRemitIndividualItem @CustomerOrderHeaderInstance, 13, @RemitStatus OUT
EXEC spRemitIndividualItem @CustomerOrderHeaderInstance, 14, @RemitStatus OUT
EXEC spRemitIndividualItem @CustomerOrderHeaderInstance, 15, @RemitStatus OUT
EXEC spRemitIndividualItem @CustomerOrderHeaderInstance, 16, @RemitStatus OUT
EXEC spRemitIndividualItem @CustomerOrderHeaderInstance, 17, @RemitStatus OUT

SELECT	*
FROM	CustomerOrderDetail cod
WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance

COMMIT TRAN