USE [QSPCanadaCommon]

select c.lang, CASE tr.ID WHEN 2 THEN 'HST' ELSE 'GST' END AS TaxRegion,
CASE c.IsStaffOrder WHEN 0 THEN 'NO' ELSE 'YES' END AS StaffCampaign,
CASE c.OnlineOnlyPrograms WHEN 0 THEN 'NO' ELSE 'YES' END AS OnlineOnlyCampaign,
CASE (SELECT 1 FROM CampaignProgram cp WHERE cp.CampaignID = c.ID AND cp.ProgramID IN (4) AND cp.DeletedTF = 0) WHEN 1 THEN 'YES' ELSE 'NO' END AS ComboCampaign,
c.ID CampaignID, acc.ID AccountID, acc.Name AccountName, ad.stateProvince AccountProvince, fm.FMID, 
fm.Firstname + ' ' + fm.Lastname FMName, c.SuppliesDeliveryDate AS RequestedDeliveryDate,
CASE isnull(sro.shipmentrequestorderid,0) WHEN 0 THEN 'NO' ELSE 'YES' END AS SentToUnigistix,
CASE b.statusinstance WHEN 40013 THEN 'YES' ELSE 'NO' END AS OrderShipped,
Round((c.NumberOfParticipants + c.NumberOfStaff) * (110/100), 0) + IsNull(c.Extra_MagBrochure,0) NumCatalogs
from campaign c
join caccount acc on acc.id = c.shiptoaccountid
join [address] ad on ad.addresslistid = acc.addresslistid and ad.address_type = 54001
join (select distinct Province, TaxRegionID from TaxRegionProvince) trp on trp.Province = ad.StateProvince
join fieldmanager fm on fm.fmid = c.fmid
join TaxRegion tr on trp.TaxRegionID = tr.id
left join qspcanadaordermanagement..batch b on b.campaignid = c.id and b.orderqualifierid = 39007 and b.statusinstance <> 40005
left join qspcanadaordermanagement..shipmentrequestorder sro on sro.orderid = b.orderid
where c.id in (select campaignid from campaignprogram where programID in (1,2))
and c.FSRequired = 1
and c.[Status] = 37002
--and FSOrderRecCreated = 0 --Not sure if they want to see ones already sent to UTI
and c.StartDate BETWEEN '2011-07-01' AND '2012-06-30'
ORDER BY tr.ID, c.lang, c.IsStaffOrder, c.OnlineOnlyPrograms, ComboCampaign, SentToUnigistix, OrderShipped