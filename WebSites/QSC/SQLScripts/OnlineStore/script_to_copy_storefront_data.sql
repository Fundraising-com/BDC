
DECLARE @FromStorefrontID INT
DECLARE @ToStorefrontID INT
DECLARE @ParentID INT
DECLARE @NewParentIdentity INT
DECLARE @IncludeExclusions BIT /** 1 = Yes, 0 = No */
DECLARE @IncludePromotions BIT /** 1 = Yes, 0 = No */

DECLARE @Now datetime
SET @Now = GetDate()
DECLARE @iNow int
SET @iNow = (YEAR(@Now) * 10000) + (MONTH(@Now) * 100) + DAY(@Now)
   
DECLARE @CategoryID INT
DECLARE @IsMenu BIT
DECLARE @Rank INT
DECLARE @EffectiveBegin INT
DECLARE @EffectiveEnd INT

SET @FromStorefrontID = 1
SET @ToStorefrontID = 10
SET @IncludeExclusions = 0
SET @IncludePromotions = 1


--******************************************
/** Cleanup any existing rows */
--******************************************
	DELETE Store.StorefrontBrochure
	WHERE StorefrontID = @ToStorefrontID

	DELETE Store.StorefrontCategory
	WHERE StorefrontID = @ToStorefrontID

	IF @IncludeExclusions = 1 
	BEGIN
		DELETE Store.StorefrontItemExclusion
		WHERE StorefrontID = @ToStorefrontID
	END

	IF @IncludePromotions = 1 
	BEGIN

		DELETE Store.StorefrontPromotion
		WHERE StorefrontID = @ToStorefrontID

	END 

delete pse 
from store.PageSection ps 
join store.PageSectionEntity pse on pse.PageSectionID = ps.PageSectionID
where ps.StorefrontID = @ToStorefrontID

Delete store.PageSection where StorefrontID = @ToStorefrontID



--******************************************
-- INSERT THE BROCHURES
--******************************************

	INSERT INTO Store.StorefrontBrochure
	(StorefrontID, BrochureID, Created, Modified)
	SELECT @ToStorefrontID, BrochureID, GETDATE(), null
	FROM Store.StorefrontBrochure
	WHERE StorefrontID = @FromStorefrontID


--******************************************
-- Get the parent categories
--******************************************

DECLARE CursorCategoryParents CURSOR FOR
SELECT	ID, CategoryID, IsMenu, Rank, EffectiveBegin, EffectiveEnd
FROM	Store.StorefrontCategory
WHERE   ParentID is null
  AND   StorefrontID = @FromStorefrontID

OPEN CursorCategoryParents
FETCH NEXT FROM CursorCategoryParents INTO 
@ParentID, @CategoryID, @IsMenu, @Rank, @EffectiveBegin, @EffectiveEnd

WHILE @@FETCH_STATUS = 0
BEGIN


  --******************************************
  /** insert the parent category and save the id inserted for use during insert of children rows **/
  --******************************************
	
	INSERT INTO Store.StorefrontCategory
	(StorefrontID, CategoryID, IsMenu, Rank, EffectiveBegin, EffectiveEnd, Created)
	VALUES 
	(@ToStorefrontID, @CategoryID, @IsMenu, @Rank, @EffectiveBegin, @EffectiveEnd, getDate()) 


	SELECT @NewParentIdentity = SCOPE_IDENTITY()

	PRINT @ParentID
	PRINT @NewParentIdentity

  --******************************************
  -- Get the children of this category
  --******************************************
	

	DECLARE CursorCategoryChildren CURSOR FOR
	SELECT	CategoryID, IsMenu, Rank, EffectiveBegin, EffectiveEnd
	FROM	Store.StorefrontCategory
 	WHERE   ParentID = @ParentID
	  AND 	StoreFrontID = @FromStorefrontID
	  
	OPEN CursorCategoryChildren
	FETCH NEXT FROM CursorCategoryChildren INTO 
	@CategoryID, @IsMenu, @Rank, @EffectiveBegin, @EffectiveEnd


	WHILE @@FETCH_STATUS = 0
	BEGIN

	INSERT INTO Store.StorefrontCategory
	(StorefrontID, CategoryID, IsMenu, Rank, EffectiveBegin, EffectiveEnd, ParentID, Created)
	VALUES 
	(@ToStorefrontID, @CategoryID, @IsMenu, @Rank, @EffectiveBegin, @EffectiveEnd,@NewParentIdentity, getDate()) 
	
	
	FETCH NEXT FROM CursorCategoryChildren INTO 
	@CategoryID, @IsMenu, @Rank, @EffectiveBegin, @EffectiveEnd

	END
	CLOSE CursorCategoryChildren
	DEALLOCATE CursorCategoryChildren


FETCH NEXT FROM CursorCategoryParents INTO 
@ParentID, @CategoryID, @IsMenu, @Rank, @EffectiveBegin, @EffectiveEnd

END
CLOSE CursorCategoryParents
DEALLOCATE CursorCategoryParents


--******************************************
-- Get the children of this category
--******************************************

IF @IncludeExclusions = 1 
BEGIN

	INSERT INTO Store.StorefrontItemExclusion
	(StorefrontID, ItemID, Created)
	SELECT @ToStorefrontID, ItemID, GETDATE()
	FROM Store.StorefrontItemExclusion
	WHERE StorefrontID = @FromStorefrontID

END

IF @IncludePromotions = 1 
BEGIN

	INSERT INTO Store.StorefrontPromotion
	(StorefrontID, PromotionID, Created)
	SELECT @ToStorefrontID, PromotionID, GETDATE()
	FROM Store.StorefrontPromotion
	WHERE StorefrontID = @FromStorefrontID
	
END

insert store.PageSection(PageSectionTypeCode, StorefrontID, CategoryID, Created)
select PageSectionTypeCode, @ToStorefrontID, CategoryID, GETDATE()  from store.PageSection where StorefrontID = @FromStorefrontID

insert store.PageSectionEntity(PageSectionID, EntityID, [Rank], EffectiveBegin, EffectiveEnd,Created)
select ps.PageSectionID, pse.EntityID, pse.[rank], @iNow, pse.EffectiveEnd, GETDATE() 
from store.PageSection ps 
join store.PageSection psfrom on psfrom.PageSectionTypeCode = ps.PageSectionTypeCode and psfrom.StorefrontID = @FromStorefrontID
join store.PageSectionEntity pse on pse.PageSectionID = psfrom.PageSectionID
where ps.StorefrontID = @ToStorefrontID and pse.EffectiveEnd > @iNow



--exec store.StoreRollUpTable_POPULATE

--SELECT TOP 10 *
--FROM Store.EffectiveProduct 
--WHERE StorefrontID = 36

--SELECT *
--FROM Store.EffectiveCategory 
--WHERE StorefrontID = 36

