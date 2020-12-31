--Update the Catalogs to the new Year

select *
from program_master pm
join ProgramSection ps on ps.program_id = pm.program_id
join PRICING_DETAILS pd on pd.programsectionid = ps.id
join product p on p.product_instance = pd.product_instance
where pm.season = 45

begin tran
update pd
set pricing_year = 2016,
	pricing_season = 'F'
from program_master pm
join ProgramSection ps on ps.program_id = pm.program_id
join PRICING_DETAILS pd on pd.programsectionid = ps.id
join product p on p.product_instance = pd.product_instance
where pm.season = 45

update p
set product_year = 2016,
	product_season = 'F'
from program_master pm
join ProgramSection ps on ps.program_id = pm.program_id
join PRICING_DETAILS pd on pd.programsectionid = ps.id
join product p on p.product_instance = pd.product_instance
where pm.season = 45

--commit tran
