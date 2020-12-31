USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_upd_CAccount_ContactInfoForAllCa]    Script Date: 06/07/2017 09:33:31 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_upd_CAccount_ContactInfoForAllCa]

	@iCAccountID			int,
	@iShiptoContactID		int

AS

DECLARE	@dSeasonStartDate	datetime,
		@iAddressID		int,
		@iPhoneListID		int,
		@iPhoneID		int,
		@zLang		varchar(2)

-- 04/11/2006 - Ben : Included the previous season
SELECT	@dSeasonStartDate = s.StartDate
FROM		Season s
WHERE	DATEADD(mm, -6, GetDate()) BETWEEN s.StartDate AND s.EndDate
AND		s.Season <> 'Y'

IF(@@rowcount > 0)
BEGIN
	-- Get Contacts to update
	SELECT	c.ID,
			c.AddressID,
			c.PhoneListID
	INTO		#ContactsToUpdate
	FROM		Contact c,
			Campaign ca
	WHERE	(ca.ShipToCampaignContactID = c.ID
	OR		ca.BillToCampaignContactID = c.ID)
	AND		ca.ShipToAccountID = @iCAccountID
	AND		ca.StartDate >= @dSeasonStartDate
	AND		c.DeletedTF = 0

	-- Check if the default contact needs to be inserted
	IF(@iShipToContactID <> -1)
	BEGIN
		-- Get Primary Contact information
		SELECT	@iAddressID = c.AddressID,
				@iPhoneListID = c.PhoneListID
		FROM		Contact c
		WHERE	c.ID = @iShipToContactID
	
		-- Check for Main
		SELECT	@iPhoneID = COALESCE(p.ID, 0)
		FROM		Phone p
		WHERE	p.PhoneListID = @iPhoneListID
		AND		p.Type = 30505
	
		IF(COALESCE(@iPhoneID, 0) = 0)
		BEGIN
			-- Check for Work
			SELECT	@iPhoneID = COALESCE(p.ID, 0)
			FROM		Phone p
			WHERE	p.PhoneListID = @iPhoneListID
			AND		p.Type = 30501
	
			IF(COALESCE(@iPhoneID, 0) = 0)
			BEGIN
				-- Use first
				SELECT	TOP 1
						@iPhoneID = p.ID
				FROM		Phone p
				WHERE	p.PhoneListID = @iPhoneListID
			END
		END
	
		-- Update Contact information
		UPDATE	cTo
		SET		cTo.ContactListID = 0,
				cTo.CAccountID = 0,
				cTo.Title = cFrom.Title,
				cTo.FirstName = cFrom.FirstName,
				cTo.LastName = cFrom.LastName,
				cTo.MiddleInitial = cFrom.MiddleInitial,
				cTo.[Function] = cFrom.[Function],
				cTo.Email = cFrom.Email,
				cTo.DateChanged = GetDate()
		FROM		Contact cTo,
				Contact cFrom
		WHERE	cFrom.ID = @iShiptoContactID
		AND		cTo.ID IN
				(SELECT	ID
				FROM		#ContactsToUpdate)
	
		-- Update Address information
		UPDATE	aTo
		SET		aTo.Street1 = aFrom.Street1,
				aTo.Street2 = aFrom.Street2,
				aTo.City = aFrom.City,
				aTo.StateProvince = aFrom.StateProvince,
				aTo.Postal_Code = aFrom.Postal_Code
		FROM		Address aTo,
				Address aFrom
		WHERE	aFrom.Address_ID = @iAddressID
		AND		aTo.Address_ID IN
				(SELECT	AddressID
				FROM		#ContactsToUpdate)
	
		-- Update Phone information
		UPDATE	pTo
		SET		pTo.PhoneNumber = pFrom.PhoneNumber,
				pTo.BestTimeToCall = pFrom.BestTimeToCall
		FROM		Phone pTo,
				Phone pFrom
		WHERE	pFrom.ID = @iPhoneID
		AND		pTo.PhoneListID IN
				(SELECT	PhoneListID
				FROM		#ContactsToUpdate)
	END
	ELSE
	BEGIN
		SELECT	@zLang = a.Lang
		FROM		CAccount a
		WHERE	a.ID = @iCAccountID

		-- Update Contact information with default info
		UPDATE	c
		SET		c.ContactListID = 0,
				c.CAccountID = 0,
				c.Title = '',
				c.FirstName = CASE @zLang WHEN 'FR' THEN 'Coordonnateur(trice)' ELSE 'Fundraising' END,
				c.LastName = CASE @zLang WHEN 'FR' THEN 'de levées de fonds' ELSE 'Coordinator' END,
				c.MiddleInitial = '',
				c.[Function] = '',
				c.Email = '',
				c.DateChanged = GetDate()
		FROM		Contact c
		WHERE	c.ID IN
				(SELECT	ID
				FROM		#ContactsToUpdate)
	
		-- Update Address information
		UPDATE	a
		SET		a.Street1 = '',
				a.Street2 = '',
				a.City = '',
				a.StateProvince = '',
				a.Postal_Code = ''
		FROM		Address a
		WHERE	a.Address_ID IN
				(SELECT	AddressID
				FROM		#ContactsToUpdate)
	
		-- Update Phone information
		UPDATE	p
		SET		p.PhoneNumber = '',
				p.BestTimeToCall = ''
		FROM		Phone p
		WHERE	p.PhoneListID IN
				(SELECT	PhoneListID
				FROM		#ContactsToUpdate)
	END

	DROP TABLE	#ContactsToUpdate
END
GO
