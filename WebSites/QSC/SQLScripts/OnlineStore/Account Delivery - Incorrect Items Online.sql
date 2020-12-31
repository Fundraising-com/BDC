CREATE TABLE #CategoryBrochure (CategoryID INT, BrochureID INT)

INSERT INTO #CategoryBrochure VALUES (395, 3248)
INSERT INTO #CategoryBrochure VALUES (395, 2868)
INSERT INTO #CategoryBrochure VALUES (418, 2821)
INSERT INTO #CategoryBrochure VALUES (505, 3246)
INSERT INTO #CategoryBrochure VALUES (505, 2857)
--INSERT INTO #CategoryBrochure VALUES (524, 2512)
INSERT INTO #CategoryBrochure VALUES (525, 2861)
--INSERT INTO #CategoryBrochure VALUES (527, 2860)
INSERT INTO #CategoryBrochure VALUES (532, 3242)
INSERT INTO #CategoryBrochure VALUES (532, 3152)
--INSERT INTO #CategoryBrochure VALUES (534, 2866)
--INSERT INTO #CategoryBrochure VALUES (535, 2863)
--INSERT INTO #CategoryBrochure VALUES (539, 2862)
INSERT INTO #CategoryBrochure VALUES (550, 3247)
INSERT INTO #CategoryBrochure VALUES (550, 2878)
INSERT INTO #CategoryBrochure VALUES (551, 2851)
INSERT INTO #CategoryBrochure VALUES (553, 2849)
--INSERT INTO #CategoryBrochure VALUES (557, 2098)
INSERT INTO #CategoryBrochure VALUES (559, 2853)
--INSERT INTO #CategoryBrochure VALUES (466, 2856)
INSERT INTO #CategoryBrochure VALUES (586, 3251)
INSERT INTO #CategoryBrochure VALUES (587, 3250)
INSERT INTO #CategoryBrochure VALUES (517, 3244)
INSERT INTO #CategoryBrochure VALUES (585, 3241)
INSERT INTO #CategoryBrochure VALUES (413, 3239)
INSERT INTO #CategoryBrochure VALUES (413, 2858)
INSERT INTO #CategoryBrochure VALUES (583, 3238)
INSERT INTO #CategoryBrochure VALUES (582, 3237)
INSERT INTO #CategoryBrochure VALUES (584, 3236)
INSERT INTO #CategoryBrochure VALUES (584, 3235)
INSERT INTO #CategoryBrochure VALUES (504, 3234)
INSERT INTO #CategoryBrochure VALUES (461, 3233)
INSERT INTO #CategoryBrochure VALUES (519, 3229)

DECLARE @Today int;
SET @Today = CAST(CONVERT(CHAR(8), GETDATE(), 112) as int);

SELECT		DISTINCT
			'Item in Category but not in Landed Brochure' Issue,
			cat.CategoryID,
			cat.InternalName CategoryName,
			NULL BrochureID,
			NULL BrochureName,
			i.ItemID,
			i.SAPID ItemSAPID,
			i.UPCCode,
			i.ItemDescShort ItemName
FROM		#CategoryBrochure cb
JOIN		store.Category cat ON cat.CategoryID = cb.CategoryID
JOIN		store.EffectiveProduct ep ON ep.CategoryID = cat.CategoryID
JOIN		core.Item i ON i.EntityID = ep.EntityID AND i.UPCCode IS NOT NULL

LEFT JOIN (
SELECT		cb.CategoryID, iL.ItemID
FROM		#CategoryBrochure cb
JOIN		core.Brochure bL ON bL.BrochureID = cb.BrochureID
JOIN		core.BrochureOffer boL ON boL.BrochureID = bL.BrochureID
JOIN		core.Item iL ON iL.ItemID = boL.ItemID
WHERE		@Today BETWEEN bL.EffectiveBegin AND bL.EffectiveEnd
AND			@Today BETWEEN boL.EffectiveBegin AND boL.EffectiveEnd
) l ON	l.ItemID = i.ItemID
	AND	l.CategoryID = cb.CategoryID
WHERE		l.ItemID IS NULL

UNION ALL

SELECT		'Item in Landed Brochure but not in Category' Issue,
			cat.CategoryID,
			cat.InternalName CategoryName,
			b.BrochureID,
			b.BrochureName,
			i.ItemID,
			i.SAPID ItemSAPID,
			i.UPCCode,
			i.ItemDescShort ItemName
FROM		#CategoryBrochure cb
JOIN		core.Brochure b ON b.BrochureID = cb.BrochureID
JOIN		core.BrochureOffer bo ON bo.BrochureID = b.BrochureID
JOIN		core.Item i ON i.ItemID = bo.ItemID
JOIN		store.Category cat ON cat.CategoryID = cb.CategoryID

LEFT JOIN (
SELECT		DISTINCT cat.CategoryID, i.ItemID
FROM		store.Category cat
JOIN		store.EffectiveProduct ep ON ep.CategoryID = cat.CategoryID
JOIN		core.Item i ON i.EntityID = ep.EntityID
) c ON	c.ItemID = i.ItemID
	AND	c.CategoryID = cb.CategoryID

WHERE		c.ItemID IS NULL
AND			bo.EffectiveBegin <= @Today
AND			bo.EffectiveEnd >= @Today
AND			i.UPCCode IS NOT NULL
AND			i.UPCCode NOT LIKE 'D0%'
AND			i.UPCCode NOT LIKE 'M0%'
AND			i.UPCCode NOT IN ('990')
ORDER BY	Issue, cat.CategoryID, BrochureID, i.ItemID

--DROP TABLE #CategoryBrochure

/*
select  c.InternalName CategoryName, b.BrochureName
from #categorybrochure cb
join core.brochure b on b.BrochureID = cb.brochureid
join store.category c on c.categoryid = cb.categoryid
*/

--select * from store.Category where InternalName like '%account%' order by CategoryID
--select * from core.brochure where BrochureName like '%popcorn%' and EffectiveEnd > 20180906 order by BrochureID

/*
select *
from core.item
where entityid = 2996559

select DISTINCT cat.CategoryID, i.ItemID, cat.InternalName
FROM		store.Category cat
JOIN		store.EffectiveProduct ep ON ep.CategoryID = cat.CategoryID
JOIN		core.Item i ON i.EntityID = ep.EntityID
where i.itemid = 21167
*/