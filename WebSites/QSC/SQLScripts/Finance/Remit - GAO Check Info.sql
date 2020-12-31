select AP_Cheque_Remit_ID, AP_Cheque_ID, RemitBatchID, RemitCode, CreationDate, FulfillmentHouseID,
		(SELECT TOP 1 p.Pub_Nbr FROM QSPCanadaProduct..Product p WHERE p.RemitCode = apcr.RemitCode ORDER BY Product_Year DESC) PublisherID,
		ProductSortName, NetAmount, GSTAmount,
		HSTAmount, PSTAmount, CurrencyCode, Address1, Address2, City, Province, PostalCode, CountryCode, Comment, PostageAmount
from qspcanadafinance..ap_cheque_remit apcr
where remitbatchid = 1600

select top 10 prodAud.AuditDate, prodAud.RemitCode OldRemitCode, prod.RemitCode NewRemitCode, prodAud.Product_Sort_Name OldProduct_Sort_Name, prod.Product_Sort_Name NewProduct_Sort_Name, prodAud.Fulfill_House_Nbr OldFulfill_House_Nbr, prod.Fulfill_House_Nbr NewFulfill_House_Nbr
from QSPCanadaProduct..ProductAudit prodAud
join qspcanadaproduct..Product prod on prod.Product_instance = prodAud.Product_Instance
where (prodAud.RemitCode <> prod.RemitCode
or prodAud.Product_Sort_Name <> prod.Product_Sort_Name
or prodAud.Fulfill_House_Nbr <> prod.Fulfill_House_Nbr)
and prod.Product_Year = 2019
and prod.Product_Season = 'F'
and prod.Type = 46001
order by auditinstance desc

select top 9 Log_Dt, *
from qspcanadaproduct..product
where product_season = 'F'
and product_year = 2019
and status in (30600, 30603)
and type = 46001
order by 1 desc

select DISTINCT RemitCode, Product_Sort_Name, Fulfill_House_Nbr
from qspcanadaproduct..product
where product_season = 'F'
and product_year = 2019
and status in (30600, 30603)
and type = 46001

select top 10 *
from QSPCanadaProduct..PUBLISHERS
order by Pub_Change_Dt desc

select *
from qspcanadaproduct..publishers

select top 10 AuditDate ModifyDate, a.Ful_Nbr, a.Ful_Status OldFul_Status, fh.Ful_Status NewFul_Status, a.Ful_Name OldFul_Name, fh.Ful_Name NewFul_Name, a.Ful_Addr_1 OldFul_Addr_1, fh.Ful_Addr_1 NewFul_Addr_1,
				a.Ful_Addr_2 OldFul_Addr_2, fh.Ful_Addr_2 NewFul_Addr_2, a.Ful_City OldFul_City, fh.Ful_City NewFul_City, a.Ful_State OldFul_State, fh.Ful_State NewFul_State, a.Ful_Zip OldFul_Zip, fh.Ful_Zip NewFul_Zip
from QSPCanadaProduct..FULFILLMENT_HOUSE_Audit a
join QSPCanadaProduct..FULFILLMENT_HOUSE fh on fh.Ful_Nbr = a.Ful_Nbr
where fh.Ful_Status <> a.Ful_Status
or fh.Ful_Name <> a.Ful_Name
or fh.Ful_Addr_1 <> a.Ful_Addr_1
or fh.Ful_Addr_2 <> a.Ful_Addr_2
or fh.Ful_City <> a.Ful_City
or fh.Ful_State <> a.Ful_State
or fh.Ful_Zip <> a.Ful_Zip
order by AuditDate desc

select *
from qspcanadaproduct..fulfillment_house