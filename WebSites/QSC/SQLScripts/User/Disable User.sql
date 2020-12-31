USE [QSPCanadaCommon]
GO

SELECT	*
FROM	CUserProfile
WHERE	LastName = 'Greenberg'

BEGIN TRAN t1
UPDATE	CUserProfile
SET		Locked = 1
WHERE	Instance = 168
COMMIT TRAN t1