--Old Offers
select p.Product_Code, p.RemitCode, p.Product_Sort_Name, pd.ListAgentCode
from product p
join pricing_details pd on pd.product_instance = p.product_instance
where product_year = 2015
and product_season = 'F'
and pd.listagentcode is not null
order by p.RemitCode, p.Product_Code

--Old and New Offers
select pOld.Product_Code, pOld.RemitCode, pOld.Product_Sort_Name, pOld.Status, pdOld.ListAgentCode, pNew.Product_Code, pNew.RemitCode, pNew.Product_Sort_Name, pdNew.ListAgentCode
from product pOld
join pricing_details pdOld on pdOld.product_instance = pOld.product_instance
left join Product pNew on pNew.Product_Code = pOld.Product_Code 
						and pNew.Product_Year = 2016
						and pnew.Product_Season = 'F'
left join PRICING_DETAILS pdNew on pdNew.Product_Instance = pnew.Product_Instance
where pOld.Product_Year = 2015
and pOld.Product_Season = 'F'
and pdOld.ListAgentCode <> ISNULL(pdNew.listagentcode, 'aaa')
and pdOld.listagentcode is not null
and pdNew.MagPrice_Instance is not null
order by pOld.RemitCode, pOld.Product_Code

--No New Offers
select pOld.Product_Code, pOld.RemitCode, pOld.Product_Sort_Name, pdOld.ListAgentCode, pNew.Product_Code, pNew.RemitCode, pNew.Product_Sort_Name, pdNew.ListAgentCode
from product pOld
join pricing_details pdOld on pdOld.product_instance = pOld.product_instance
left join Product pNew on pNew.Product_Code = pOld.Product_Code 
						and pNew.Product_Year = 2016
						and pnew.Product_Season = 'F'
left join PRICING_DETAILS pdNew on pdNew.Product_Instance = pnew.Product_Instance
where pOld.Product_Year = 2015
and pOld.Product_Season = 'F'
and pdOld.listagentcode is not null
and pdNew.MagPrice_Instance is null
order by pOld.RemitCode, pOld.Product_Code

--Update
begin tran
update pdNew
set ListAgentCode = pdOld.ListAgentCode
from product pOld
join pricing_details pdOld on pdOld.product_instance = pOld.product_instance
join Product pNew on pNew.Product_Code = pOld.Product_Code 
						and pNew.Product_Year = 2016
						and pnew.Product_Season = 'F'
join PRICING_DETAILS pdNew on pdNew.Product_Instance = pnew.Product_Instance
where pOld.Product_Year = 2015
and pOld.Product_Season = 'F'
and pdOld.listagentcode is not null
and pdNew.listagentcode is null
--commit tran

--New Offers Needing List Agent Codes
select distinct p.Product_Code, p.RemitCode, p.Product_Sort_Name, p.product_year, p.Product_Season, fh.Ful_Nbr, fh.Ful_Name
from product p
join pricing_details pd on pd.product_instance = p.product_instance
join FULFILLMENT_HOUSE fh on fh.Ful_Nbr = p.Fulfill_House_Nbr
where (product_year = 2016 and product_season = 'F')
and p.Fulfill_House_Nbr in (7,11,207,313)
and pd.listagentcode is null
--and p.Status <> 30601
order by fh.Ful_Name, p.RemitCode, p.Product_Code, p.Product_Year