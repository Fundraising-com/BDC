--SQLP08

USE SWCorporate_AppServices
Go

select		* 
FROM		Core.ExceptionLogEntryInfo 
WHERE		InstrumentationLogEntryCreated between '2017-10-19 11:34' and '2017-10-19 11:37'
ORDER BY	InstrumentationLogEntryId