1. gasqlp02, for each DB, Tasks->Backup. Destination=Disk, S:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Backup\, copy-only backup
2. sqlt08, copy from \\gasqlp02\s$\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Backup to C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\Backup (DEV: Dev02, C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Backup)
3. sqlt08, bring DB offline
4. sqlt08 make backup copy of mdf file (C:\SQL_DATA\, copy to gasqlp02 if no space)
5. sqlt08, for each DB, Tasks->Restore DB (Option ->With Replace)
6. Delete backups
7. Fix users

USE QSPCanadaCommon
GO
EXEC sp_change_users_login 'Update_One', 'qspcafulfillment', 'qspcafulfillment'
USE QSPCanadaError
GO
EXEC sp_change_users_login 'Update_One', 'qspcafulfillment', 'qspcafulfillment'
USE QSPCanadaFinance
GO
EXEC sp_change_users_login 'Update_One', 'qspcafulfillment', 'qspcafulfillment'
USE QSPCanadaProduct
GO
EXEC sp_change_users_login 'Update_One', 'qspcafulfillment', 'qspcafulfillment'
USE QSPCanadaOrderManagement
GO
EXEC sp_change_users_login 'Update_One', 'qspcafulfillment', 'qspcafulfillment'

8. Verify the DBs
select top 99 *
from qspcanadaerror..tbl_error
order by id desc

select top 99 *
from qspcanadaproduct..product
order by product_instance desc

select top 99 *
from QSPCanadaFinance..invoice
order by invoice_id desc

select top 99 *
from qspcanadacommon..campaign
order by id desc

select top 99 *
from qspcanadaordermanagement..batch
order by date desc

9. Update environmental variables

update ReportRequestBatchType
set reportpath = REPLACE(reportpath, '\\Focus-fis\QSC', '\\GAFIST01\QSPC') --Dev: 'GAFISD01'