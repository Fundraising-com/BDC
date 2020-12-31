select *
from store.Effectiveproduct
where StorefrontID = 226

select *
from store.Category

select *
from store.storefront

select top 9 *
from core.contract c
where DivisionCode = 40
and ContractTypeID = 3
order by c.ContractID desc

select *
from core.contract c
left join Store.contractstorefront csf on csf.ContractID = c.ContractID
--left join core.contractbrochure cb on cb.ContractID = c.ContractID
--left join core.Brochure b on b.BrochureID = cb.BrochureID
--left join core.ContractAddress ca on ca.ContractID = c.ContractID
where c.SAPContractNo in ('101062', '101186')--, '101187','101151','101570')
order by c.ContractID, b.BrochureID

begin tran
insert store.ContractStorefront values (453808, 226, null, GETDATE(), null, null)
commit tran


select top 9 *
from core.ContractBrochure
order by ContractBrochureID desc

Magazine English Family Reading Program Fall 2015
To Remember This Catalogue FY2016
Entertainment Catalogue FY2015
Organic Edibles Catalogue Spring 2016
Tasty Batter Cookie Dough Spring 2016
Festival Catalogue Spring FY16

select top 19 *
from Integration.ETLLog
order by ETLLogID desc

select *
from core.Brochure

begin tran
insert core.Contract values (3, 1, 2, null, '101062', null, null, null, null, null, null, null, 20160101, 20160630, null, GETDATE(), null, null, null, null, null, null, 0, 0, 0, 0, null, 40, 1, 1, 0, 2, 14, null, NEWID(), 1)
insert core.Contract values (3, 1, 2, null, '101186', null, null, null, null, null, null, null, 20160101, 20160630, null, GETDATE(), null, null, null, null, null, null, 0, 0, 0, 0, null, 40, 1, 1, 0, 2, 14, null, NEWID(), 1)
--commit tran

select top 22 *
from core.ContractBrochure
order by ContractID desc

insert core.ContractBrochure values (453808, 1014, 1, 0.00, 20150716, 99991231, GETDATE(), null, null, NEWID())
insert core.ContractBrochure values (453808, 1304, 1, 0.00, 20150716, 99991231, GETDATE(), null, null, NEWID())
insert core.ContractBrochure values (453808, 1626, 1, 0.00, 20150716, 99991231, GETDATE(), null, null, NEWID())
insert core.ContractBrochure values (453808, 1919, 1, 0.00, 20150716, 99991231, GETDATE(), null, null, NEWID())
insert core.ContractBrochure values (453808, 1920, 1, 0.00, 20150716, 99991231, GETDATE(), null, null, NEWID())
insert core.ContractBrochure values (453808, 1921, 1, 0.00, 20150716, 99991231, GETDATE(), null, null, NEWID())

insert core.ContractBrochure values (453809, 1014, 1, 0.00, 20150716, 99991231, GETDATE(), null, null, NEWID())
insert core.ContractBrochure values (453809, 1304, 1, 0.00, 20150716, 99991231, GETDATE(), null, null, NEWID())
insert core.ContractBrochure values (453809, 1626, 1, 0.00, 20150716, 99991231, GETDATE(), null, null, NEWID())
insert core.ContractBrochure values (453809, 1919, 1, 0.00, 20150716, 99991231, GETDATE(), null, null, NEWID())
insert core.ContractBrochure values (453809, 1920, 1, 0.00, 20150716, 99991231, GETDATE(), null, null, NEWID())
insert core.ContractBrochure values (453809, 1921, 1, 0.00, 20150716, 99991231, GETDATE(), null, null, NEWID())

begin tran

insert core.ContractAddress values (453808, null, null, '9288', '', '', null, null, '', '', 'ON', 'CA', '', null, null, null, null, 0, 0, 0, 0, 1, 0, 0, 0, null, '', '', '', GETDATE(), null, null, NEWID())
insert core.ContractAddress values (453808, null, null, '9288', '', '', null, null, '', '', 'ON', 'CA', '', null, null, null, null, 1, 0, 0, 0, 0, 0, 0, 0, null, '', '', '', GETDATE(), null, null, NEWID())
insert core.ContractAddress values (453808, null, null, '9288', '', '', null, null, '', '', 'ON', 'CA', '', null, null, null, null, 0, 0, 0, 1, 0, 0, 0, 0, null, '', '', '', GETDATE(), null, null, NEWID())
insert core.ContractAddress values (453808, null, null, '9288', '', '', null, null, '', '', 'ON', 'CA', '', null, null, null, null, 0, 0, 0, 0, 0, 0, 1, 0, null, '', '', '', GETDATE(), null, null, NEWID())
insert core.ContractAddress values (453808, null, null, '9288', '', '', null, null, '', '', 'ON', 'CA', '', null, null, null, null, 0, 1, 0, 0, 0, 0, 0, 0, null, '', '', '', GETDATE(), null, null, NEWID())
insert core.ContractAddress values (453808, null, null, '9288', '', '', null, null, '', '', 'ON', 'CA', '', null, null, null, null, 0, 0, 1, 0, 0, 0, 0, 0, null, '', '', '', GETDATE(), null, null, NEWID())

insert core.ContractAddress values (453809, null, null, '9205', '', '', null, null, '', '', 'ON', 'CA', '', null, null, null, null, 0, 0, 0, 0, 1, 0, 0, 0, null, '', '', '', GETDATE(), null, null, NEWID())
insert core.ContractAddress values (453809, null, null, '9205', '', '', null, null, '', '', 'ON', 'CA', '', null, null, null, null, 1, 0, 0, 0, 0, 0, 0, 0, null, '', '', '', GETDATE(), null, null, NEWID())
insert core.ContractAddress values (453809, null, null, '9205', '', '', null, null, '', '', 'ON', 'CA', '', null, null, null, null, 0, 0, 0, 1, 0, 0, 0, 0, null, '', '', '', GETDATE(), null, null, NEWID())
insert core.ContractAddress values (453809, null, null, '9205', '', '', null, null, '', '', 'ON', 'CA', '', null, null, null, null, 0, 0, 0, 0, 0, 0, 1, 0, null, '', '', '', GETDATE(), null, null, NEWID())
insert core.ContractAddress values (453809, null, null, '9205', '', '', null, null, '', '', 'ON', 'CA', '', null, null, null, null, 0, 1, 0, 0, 0, 0, 0, 0, null, '', '', '', GETDATE(), null, null, NEWID())
insert core.ContractAddress values (453809, null, null, '9205', '', '', null, null, '', '', 'ON', 'CA', '', null, null, null, null, 0, 0, 1, 0, 0, 0, 0, 0, null, '', '', '', GETDATE(), null, null, NEWID())

select top 9 *
from core.ContractBrochure
order by ContractBrochureID desc

begin tran
update core.ContractBrochure
set ReplicationID = NEWID()
where ContractBrochureID in (1618278,1618279,1618280)
--commit tran

begin tran
delete core.ContractAddress where ContractID = 453809
delete core.ContractBrochure where ContractID = 453809
delete core.Contract where ContractID = 453809
commit tran

begin tran
update core.ContractAddress
set name1 = 'BROADACRES JUNIOR SCHOOL', Name2 = 'JEANNE MACFARLANE', Address = '45 CRENDON DR', City = 'ETOBICOKE', PostalCode = 'M9C3G6', EmailAddress = 'JEANNE.MACFARLANE@TDSB.ON.CA', DisplayName = 'Jeanne Macfarlane', PhoneNumber = '416-394-7030'
where ContractAddressID in 
(
2446902,
2446903,
2446904)

update core.ContractAddress
set name1 = 'JILL MASLANKA', Name2 = null, Address = '38 BLAKETON RD', City = 'ETOBICOKE', PostalCode = 'M9B4W3', EmailAddress = 'JILL.MASLANKA@QSP.CA', DisplayName = 'Jill Maslanka', PhoneNumber = '(416) 646-2441'
where ContractAddressID in 
(2446895) --fm

update core.ContractAddress
set name1 = 'JEANNE MACFARLANE', Name2 = 'BROADACRES JUNIOR SCHOOL', Address = '45 CRENDON DR', City = 'ETOBICOKE', PostalCode = 'M9C3G6', EmailAddress = 'JEANNE.MACFARLANE@TDSB.ON.CA', DisplayName = 'Jeanne Macfarlane', PhoneNumber = '416-394-7030'
where ContractAddressID in 
(2446905,2446906)
commit tran
