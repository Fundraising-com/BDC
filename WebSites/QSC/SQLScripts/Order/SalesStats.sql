USE QSPCanadaOrderManagement

--1.	Number of students who have sold 1+ unit, fall ’11 and fall ‘10
select COUNT(DISTINCT coh.studentinstance)
from customerorderdetail cod
join customerorderheader coh
		on coh.instance = cod.customerorderheaderinstance
join batch b
		on b.id = coh.orderbatchid and b.date = coh.orderbatchdate
join qspcanadafinance..invoice inv
		on inv.invoice_id = cod.invoicenumber
join qspcanadacommon..campaign camp
		on camp.ID = b.CampaignID
where (inv.invoice_date between '2009-07-01' and '2010-01-01')
--where (inv.invoice_date between '2010-07-01' and '2011-01-01')
and b.OrderQualifierID IN (39001, 39002, 39009, 39015, 39020)
and camp.IsStaffOrder = 0

--2.	Average number of units/student, fall ’11 and fall ‘10
select avg(convert(numeric, NumUnits))
from (select coh.studentinstance, COUNT(CASE cod.ProductType WHEN 46001 THEN 1 ELSE cod.Quantity END) NumUnits
from customerorderdetail cod
join customerorderheader coh
		on coh.instance = cod.customerorderheaderinstance
join batch b
		on b.id = coh.orderbatchid and b.date = coh.orderbatchdate
join qspcanadafinance..invoice inv
		on inv.invoice_id = cod.invoicenumber
join qspcanadacommon..campaign camp
		on camp.ID = b.CampaignID
where (inv.invoice_date between '2009-07-01' and '2010-01-01')
--where (inv.invoice_date between '2010-07-01' and '2011-01-01')
and b.OrderQualifierID IN (39001, 39002, 39009, 39015, 39020)
and camp.IsStaffOrder = 0
group by coh.studentinstance) NumPerStudent

--3.	Average dollars/student, fall ’11 and fall ‘10
select avg(Price)
from (select coh.studentinstance, SUM(cod.Price) Price
from customerorderdetail cod
join customerorderheader coh
		on coh.instance = cod.customerorderheaderinstance
join batch b
		on b.id = coh.orderbatchid and b.date = coh.orderbatchdate
join qspcanadafinance..invoice inv
		on inv.invoice_id = cod.invoicenumber
join qspcanadacommon..campaign camp
		on camp.ID = b.CampaignID
where (inv.invoice_date between '2009-07-01' and '2010-01-01')
--where (inv.invoice_date between '2010-07-01' and '2011-01-01')
and b.OrderQualifierID IN (39001, 39002, 39009, 39015, 39020)
and camp.IsStaffOrder = 0
group by coh.studentinstance) NumPerStudent

--4.	Average customers per student, fall ’11 and fall ‘10
select avg(convert(numeric, NumCust))
from (select coh.studentinstance, COUNT(DISTINCT(cust.Instance)) NumCust
from customerorderdetail cod
join customerorderheader coh
		on coh.instance = cod.customerorderheaderinstance
join batch b
		on b.id = coh.orderbatchid and b.date = coh.orderbatchdate
join qspcanadafinance..invoice inv
		on inv.invoice_id = cod.invoicenumber
JOIN Customer cust
		ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
								WHEN 0 THEN coh.CustomerBillToInstance
								ELSE		cod.CustomerShipToInstance
							END
join qspcanadacommon..campaign camp
		on camp.ID = b.CampaignID
where (inv.invoice_date between '2009-07-01' and '2010-01-01')
--where (inv.invoice_date between '2010-07-01' and '2011-01-01')
and b.OrderQualifierID IN (39001, 39002, 39009, 39015, 39020)
and camp.IsStaffOrder = 0
group by coh.studentinstance) NumPerStudent

--5.	Average units per customer, fall ’11 and fall ‘10
select avg(convert(numeric, NumUnits))
from (select cust.Instance, COUNT(CASE cod.ProductType WHEN 46001 THEN 1 ELSE cod.Quantity END) NumUnits
from customerorderdetail cod
join customerorderheader coh
		on coh.instance = cod.customerorderheaderinstance
join batch b
		on b.id = coh.orderbatchid and b.date = coh.orderbatchdate
join qspcanadafinance..invoice inv
		on inv.invoice_id = cod.invoicenumber
JOIN Customer cust
		ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
								WHEN 0 THEN coh.CustomerBillToInstance
								ELSE		cod.CustomerShipToInstance
							END
join qspcanadacommon..campaign camp
		on camp.ID = b.CampaignID
where (inv.invoice_date between '2009-07-01' and '2010-01-01')
--where (inv.invoice_date between '2010-07-01' and '2011-01-01')
and b.OrderQualifierID IN (39001, 39002, 39009, 39015, 39020)
and camp.IsStaffOrder = 0
group by cust.Instance) NumPerCustomer

--6.	Average revenue/customer, fall ’11 and fall ‘10
select avg(Price)
from (select cust.Instance, SUM(cod.Price) Price
from customerorderdetail cod
join customerorderheader coh
		on coh.instance = cod.customerorderheaderinstance
join batch b
		on b.id = coh.orderbatchid and b.date = coh.orderbatchdate
join qspcanadafinance..invoice inv
		on inv.invoice_id = cod.invoicenumber
JOIN Customer cust
		ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
								WHEN 0 THEN coh.CustomerBillToInstance
								ELSE		cod.CustomerShipToInstance
							END
join qspcanadacommon..campaign camp
		on camp.ID = b.CampaignID
where (inv.invoice_date between '2009-07-01' and '2010-01-01')
--where (inv.invoice_date between '2010-07-01' and '2011-01-01')
and b.OrderQualifierID IN (39001, 39002, 39009, 39015, 39020)
and camp.IsStaffOrder = 0
group by cust.Instance) NumPerCustomer