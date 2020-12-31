select distinct cd.Description ProductLine, p.OracleCode SAPMaterialNumber, p.Product_Sort_Name ProductName, p.Product_Code, cd2.Description Program
from product p
join QSPCanadaCommon..codedetail cd on cd.instance = p.type
join PRICING_DETAILS pd on pd.Product_Instance = p.Product_Instance
join ProgramSection ps on ps.id = pd.ProgramSectionID
join PROGRAM_MASTER pm on pm.Program_ID = ps.Program_ID
join QSPCanadaCommon..CodeDetail cd2 on cd2.Instance = pm.SubType
where p.product_year in (2019)
and p.Type not in (46001,46023)
and p.oraclecode not like 'DO%'
and p.Status = 30600
order by cd2.Description, p.OracleCode, p.Product_Sort_Name