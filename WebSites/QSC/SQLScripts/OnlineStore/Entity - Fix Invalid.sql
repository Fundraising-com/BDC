USE [GA]
GO

exec store.PrepareEntityValidationTables
exec store.ValidateEntityImages
exec store.ValidateEntityTexts
exec store.ValidateEntityCategories
exec store.UpdateValidEntities
select et.Literal, e.*
      from store.Entity e
      join store.EntityType et on e.EntityTypeCode=et.Code
      where e.IsValid=0 
      order by et.Literal, e.EntityID
exec Store.StoreRollUpTable_POPULATE

/*
select *
from store.InvalidText
where EntityID in (308117,308126)

select *
FROM Store.Entity e
JOIN Store.RequiredEntityTexttypes r on r.entitytypecode = e.entitytypecode
join store.text t on t.EntityID = e.EntityID
where e.EntityID in (308117,308126)
*/