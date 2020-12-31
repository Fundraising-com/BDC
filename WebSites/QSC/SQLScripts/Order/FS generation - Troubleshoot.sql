USE [QSPCanadaOrderManagement]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[GenerateFieldSupplyOrder_V6]
		@CampaignID = 101539,
		@UserId = N'612',
		@DebugMode = 1,
		@GenerateOrder = 0

SELECT	'Return Value' = @return_value

GO

select campaignid, *
from qspcanadaordermanagement..batch
where orderid = 10193159

select *
from qspcanadacommon..campaigntocontentcatalog
where campaignid = 87921 

/*delete qspcanadacommon..campaigntocontentcatalog
where campaignid = 87921

exec  qspcanadacommon..pr_CreateCampaignToContentCatalog 87921
*/
select * from qspcanadacommon..campaignaudit
where id = 87921

select *
from qspcanadaproduct..product
where product_code in ( '1053981')

--catalogs
select pd.Language_Code, pd.FSIsBrochure, pd.TaxRegionId, ps.type, pd.Status, p.Status, ps.CatalogCode, pd.FSContent_Catalog_Code, pd.FSApplicabilityId, *
from qspcanadaproduct..pricing_details pd
join qspcanadaproduct..programsection ps on ps.id = pd.programsectionid
join qspcanadaproduct..program_master pm on pm.program_id = ps.program_id
join qspcanadaproduct..PROGRAM_MASTER pm2 on pm2.Code like pd.FSContent_Catalog_Code
JOIN QSPCanadaProduct..Product p ON p.Product_instance = pd.Product_instance
left join QSPCanadaProduct..ProgFSSectionMap map on map.Program_ID = pd.FSProgram_Id and map.Catalog_section_id = ps.Id
where pd.Product_Code in ( '1053993')

--non catalogs
select pd.FSProgram_Id, ps.ID, *
from qspcanadaproduct..pricing_details pd
join qspcanadaproduct..programsection ps on ps.id = pd.programsectionid
join qspcanadaproduct..program_master pm on pm.program_id = ps.program_id
left join QSPCanadaProduct..ProgFSSectionMap map on map.Program_ID = pd.FSProgram_Id and map.Catalog_section_id = ps.Id
where product_code in ( '1053981')

--Yearly Fix
SELECT *
from qspcanadaproduct..program_master

SELECT *
from qspcanadaproduct..programsection
where program_id in (397)

select pd.FSProgram_Id, p.Name, ps.*, *
from QSPCanadaProduct..PRICING_DETAILS pd
join qspcanadaproduct..programsection ps on ps.id = pd.programsectionid
join qspcanadaproduct..program_master pm on pm.program_id = ps.program_id
join qspcanadacommon..program p on p.id = pd.fsprogram_id
where pm.program_id in (397)
order by ps.name

select *
from QSPCanadaCommon..Program

begin tran
update qspcanadaproduct..pricing_details
set fsprogram_id = 53
where programsectionid in (1335)
and FSProgram_Id <> 53

update qspcanadaproduct..pricing_details
set fsprogram_id = 2
where programsectionid in (1333)
and FSProgram_Id <> 2

update qspcanadaproduct..pricing_details
set fsprogram_id = 44
where programsectionid in (1214)
and FSProgram_Id <> 44

update qspcanadaproduct..pricing_details
set fsprogram_id = 54
where programsectionid in (1251)
and FSProgram_Id <> 54

update qspcanadaproduct..pricing_details
set fsprogram_id = 56
where programsectionid in (1253)
and FSProgram_Id <> 56

update qspcanadaproduct..pricing_details
set fsprogram_id = 58
where programsectionid in (1254)
and FSProgram_Id <> 58

update qspcanadaproduct..pricing_details
set fsprogram_id = 59
where programsectionid in (1255)
and FSProgram_Id <> 59

update qspcanadaproduct..pricing_details
set fsprogram_id = 2
where programsectionid in (1250)
and FSProgram_Id <> 2

commit tran
