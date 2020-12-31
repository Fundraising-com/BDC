select ps.*, psm.*, pd.FSProgram_Id, *
from ProgramSection ps
join ProgFSSectionMap psm on psm.CATALOG_SECTION_ID = ps.ID
join PRICING_DETAILS pd on pd.ProgramSectionID = ps.ID
where psm.PROGRAM_ID <> pd.FSProgram_Id
and pd.Pricing_Year = 2013
and pd.Pricing_Season = 'S'

commit tran
update pd
set FSProgram_ID = psm.Program_ID
from ProgramSection ps
join ProgFSSectionMap psm on psm.CATALOG_SECTION_ID = ps.ID
join PRICING_DETAILS pd on pd.ProgramSectionID = ps.ID
where psm.PROGRAM_ID <> pd.FSProgram_Id
and pd.Pricing_Year = 2013
and pd.Pricing_Season = 'S'
