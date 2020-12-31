use GA

--select * from core.programtype

--Gift BrochureContextBrochure (BrochureContextID=48)
select *
from core.Brochure b
left join core.BrochureContextBrochure bcb on bcb.BrochureID = b.BrochureID and bcb.BrochureContextID = 48
where MaterialGroup1 = '999'
and b.EffectiveEnd > 20181007
and bcb.BrochureContextID is null
and b.ProgramTypeID not in (40,104,105,106,110,118,150,153,157,148,171,209,211,220,222,224,231,232)

--Mag BrochureContextBrochure (BrochureContextID=49)
select *
from core.Brochure b
join core.ProgramType pt on pt.programtypeid = b.programtypeid
left join core.BrochureContextBrochure bcb on bcb.BrochureID = b.BrochureID and bcb.BrochureContextID = 49
where MaterialGroup1 = '999'
and b.EffectiveEnd > 20181007
and bcb.BrochureContextID is null
and b.ProgramTypeID not in (40,105,106,110,148,150,153,157,171,209,211,220,222,224,231,232)

begin tran

insert core.BrochureContextBrochure values (48, 2775)
insert core.BrochureContextBrochure values (48, 2819)
insert core.BrochureContextBrochure values (48, 2822)
insert core.BrochureContextBrochure values (48, 2849)
insert core.BrochureContextBrochure values (48, 2851)
insert core.BrochureContextBrochure values (48, 2853)
insert core.BrochureContextBrochure values (48, 3127)

insert core.BrochureContextBrochure values (49, 2775)
insert core.BrochureContextBrochure values (49, 2819)
insert core.BrochureContextBrochure values (49, 2821)
insert core.BrochureContextBrochure values (49, 2822)
insert core.BrochureContextBrochure values (49, 2849)
insert core.BrochureContextBrochure values (49, 2851)
insert core.BrochureContextBrochure values (49, 2853)
insert core.BrochureContextBrochure values (49, 3127)

--commit tran

begin tran

DECLARE @ContractID int
--- **  358890 = Canada Gift
--- **  358891 = Canada Mag
SET @ContractID = 358891

DECLARE @Now datetime
SET @Now = GetDate()
DECLARE @iNow int
SET @iNow = (YEAR(@Now) * 10000) + (MONTH(@Now) * 100) + DAY(@Now)

-- Remove any ToteContractBrochure records from our well-known contract.
DELETE core.ToteContractBrochure WHERE ContractBrochureID IN (SELECT ContractBrochureID FROM core.ContractBrochure WHERE ContractID = @ContractID)

-- Remove any ContractBrochure records from our well-known contract.
DELETE core.ContractBrochure WHERE ContractID = @ContractID

-- Update the ContractBrochures with the proper dates from the brochure.
--  Brochure is directly linked via BrochureID.
UPDATE core.ContractBrochure SET EffectiveBegin = b.EffectiveBegin, EffectiveEnd = b.EffectiveEnd
FROM core.ContractBrochure as cb 
INNER JOIN core.Brochure as b ON cb.BrochureID = b.BrochureID
WHERE cb.ContractID = @ContractID

-- Add any ContractBrochure records to our well-known contract
--  that are effective as of today's date but are not on the contract.
INSERT INTO core.ContractBrochure (ContractID, BrochureID, PriceCodeID, ProfitPercentage, EffectiveBegin, EffectiveEnd)
SELECT DISTINCT @ContractID AS 'ContractID', bcb.BrochureID, op.PriceCodeID, NULL AS 'ProfitPercentage', b.EffectiveBegin, b.EffectiveEnd 
FROM core.ArrivalType as at with(nolock)
INNER JOIN core.BrochureContextBrochure AS bcb WITH(NOLOCK) on bcb.BrochureContextID = at.BrochureContextID
INNER JOIN core.Brochure AS b WITH(NOLOCK) ON b.BrochureID = bcb.BrochureID
INNER JOIN core.BrochureOffer AS bo WITH(NOLOCK) ON bo.BrochureID = bcb.BrochureID
INNER JOIN core.Offer AS o WITH(NOLOCK) ON o.OfferID = bo.OfferID
INNER JOIN core.OfferPrice AS op WITH(NOLOCK) ON op.OfferID = o.OfferID
LEFT JOIN core.ContractBrochure as cb on @ContractID = cb.contractid AND b.brochureid = cb.Brochureid AND op.pricecodeid = cb.pricecodeid
WHERE /*@iNow >= b.EffectiveBegin AND*/ @iNow <= b.EffectiveEnd
  /*@AND @iNow >= o.EffectiveBegin*/ AND @iNow <= o.EffectiveEnd
  /*@AND @iNow >= op.EffectiveBegin*/ AND @iNow <= op.EffectiveEnd
  /*@AND @iNow >= bo.EffectiveBegin*/ AND @iNow <= bo.EffectiveEnd
  AND at.ContractID = @ContractID
  AND cb.ContractID IS NULL

-- Add any ToteContractBrochure records to our well-known contract
--  that are effective as of today's date but are not linked.
INSERT INTO core.ToteContractBrochure (ToteID, ContractBrochureID)
SELECT ToteID, ContractBrochureID
FROM core.Tote AS t WITH(NOLOCK)
INNER JOIN core.ContractBrochure AS cb WITH(NOLOCK) ON cb.ContractID = @ContractID
WHERE t.ContractID = @ContractID
  AND NOT EXISTS(SELECT ToteID FROM core.ToteContractBrochure
    WHERE ToteID = t.ToteID AND ContractBrochureID = cb.ContractBrochureID)

--commit tran

select *
from core.Contract c
join core.ContractBrochure cb on cb.ContractID = c.ContractID
join core.Brochure b on b.BrochureID = cb.BrochureID
where c.ContractID in (358890, 358891)
order by c.ContractID, cb.BrochureID

select *
from core.ContractBrochure cb
join core.Brochure b on b.BrochureID = cb.BrochureID
join core.ToteContractBrochure tcb on tcb.ContractBrochureID = cb.ContractBrochureID
where cb.ContractID in (358890, 358891)
order by cb.ContractID, cb.BrochureID
