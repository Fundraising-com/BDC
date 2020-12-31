use qspcanadaproduct
go

select *
from program_master
--where status = 30403
order by program_id desc

select *
from program_master pm
join programsection ps on ps.program_id = pm.program_id
join pricing_details pd on pd.programsectionid = ps.id
join product p on p.product_instance = pd.product_instance
where pm.program_id in (412,413,416,417,421,424,426,427)
order by pm.program_id

begin tran
update p
set commentdate = getdate()
from program_master pm
join programsection ps on ps.program_id = pm.program_id
join pricing_details pd on pd.programsectionid = ps.id
join product p on p.product_instance = pd.product_instance
where pm.program_id in (412,413,416,417,421,424,426,427)
--commit tran
