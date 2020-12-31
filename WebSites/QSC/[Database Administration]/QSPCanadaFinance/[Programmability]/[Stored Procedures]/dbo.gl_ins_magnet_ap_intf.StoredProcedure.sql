USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[gl_ins_magnet_ap_intf]    Script Date: 06/07/2017 09:17:22 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[gl_ins_magnet_ap_intf]
  @pAdjustmentID	int,
  @pAdjustmentTypeID	int,
  @pAccountID		int,
  @pEffectiveDate	datetime,
  @pAdjustmentAmount	numeric(10,2),
  @pCountryCode	varchar(2),
  @pDescription		varchar(140),
  @pDistGLAccountNum	varchar(50),
  @pOrderID		int
AS

-- =~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~
-- Description: 
-- This procedure automatically inserts MAGNET Profit adjustments 
-- into the 3 QSPOracleInterface A/P interface tables;
-- 1) om_tbl_ap_invoices_interface 
-- 2) om_tbl_ap_inv_lines_interface
-- 3) om_tbl_po_vendors_interface
-- 
-- It also verifies that the flag is_cheque_required is set to true for 
-- the adjustment type ID of the adjustment.
--              
-- This procedure assumes that one adjustment will generate 
-- only ONE pair of G/L entries (1 debit and 1 credit)
-- 
-- Revision History:
-- June 2004 - Joshua Caesar 
-- Inital Revision based upon om_proc_ins_magnet_ap_intf previous Oracle system.
-- =~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~
/* need to check that these values don't have defaults that get appened to 
 v_vendor_number    om_tbl_group.vendor_number%TYPE;
 v_vendor_site_name om_tbl_group.vendor_site_name%TYPE;
 v_vendor_pay_group om_tbl_group.vendor_pay_group%TYPE;
 v_is_cheque_required om_tbl_adjustment_type.is_cheque_required%TYPE;
 v_currency_code    om_tbl_ap_invoices_interface.invoice_currency_code%TYPE;
 v_invoice_num      om_tbl_ap_invoices_interface.invoice_num%TYPE;
 v_ap_description         om_tbl_ap_invoices_interface.description%TYPE;
*/

/* See if a check/cheque is required */
DECLARE @bCheckRequired bit
SELECT 
	@bCheckRequired = 
	case upper(is_cheque_required)
		when 'Y' then cast(1 as bit)
		when 'N' then cast(0 as bit)
		else cast(0 as bit) --should be an error condition else is_cheque_required
	end
FROM 
	ADJUSTMENT_TYPE
WHERE
	ADJUSTMENT_TYPE.adjustment_type_id =  @pAdjustmentTypeID

IF @bCheckRequired <> 1
BEGIN
	--this is an error, a check/cheque isnt needed
	return(-1);
END

/* Make sure that Mandatory fields have good values */
IF @pAdjustmentID IS NULL
begin
	return(-1);
end --RAISE_APPLICATION_ERROR(-20011, 'Adjustment ID CANNOT be NULL!');
IF @pAccountID IS NULL
begin
	return(-1);
end --RAISE_APPLICATION_ERROR(-20012, 'Account ID CANNOT be NULL!');
IF @pAdjustmentAmount IS NULL
begin
	return(-1);
end --RAISE_APPLICATION_ERROR(-20013, 'Adjustment Amount CANNOT be NULL!');
IF @pAdjustmentAmount <= 0
begin
	return(-1);
end--RAISE_APPLICATION_ERROR(-20014, 'Adjustment Amount must be greater than zero!')
IF @pCountryCode IS NULL
begin
	return(-1);
end --RAISE_APPLICATION_ERROR(-20015, 'Country Code CANNOT be NULL!');

/*** Get the Vendor information ***/
DECLARE @vVendorNumber		varchar(30)
DECLARE @vVendorSiteName	varchar(15)
DECLARE @vVendorPayGroup	varchar(25)
SELECT 
	@vVendorNumber		= VendorNumber, 
	@vVendorSiteName	= VendorSiteName, 
	@vVendorPayGroup	= VendorPayGroup
 FROM 
	QSPCanadaCommon.dbo.CAccount
WHERE
	QSPCanadaCommon.dbo.CAccount.[Id] = @pAccountID
	
IF @vVendorNumber IS NULL
begin
	return(-1);
end
IF @vVendorSiteName IS NULL
begin
	return(-1);
end
IF @vVendorPayGroup IS NULL
begin
	return(-1);
end

/*** Fetching Canadian Currency Description ***/
-- still working on this one, see gl_ins_magnet_overpay_ap.prc.sql
DECLARE @iCurrencyCode int
SELECT @iCurrencyCode = 1--should be a varchar ? 

/*** Set the invoice number ***/
DECLARE @vInvoiceNum varchar(50)
SET @vInvoiceNum = cast(@pAccountID as varchar) + '-' + cast(@pAdjustmentID as varchar)

/*** Fetching the A/P description from the Order ID and Campaign ID of the order ***/
DECLARE @vCheckDescription varchar(240)--the interface allows for 240.
select 
	@vCheckDescription = @pDescription
	+ ' CA ' 
	+ cast(isnull(CampaignID, '') as varchar)
	+ ' ORDER ' 
	+ cast(OrderID as varchar)
from 
	QSPCanadaOrderManagement.dbo.Batch
where 
	QSPCanadaOrderManagement.dbo.Batch.[OrderID] = @pOrderID


/*** Inserting into the A/P Interface Tables ***/
INSERT INTO QSPOracleInterface.dbo.OM_TBL_AP_INVOICES_INTERFACE (
	country_code, 
	invoice_num, 
	invoice_type,
	invoice_date, 
	invoice_amount, 
	invoice_currency_code,
	terms_name, 
	pay_group_lookup_code, 
	[description]
) VALUES (
	@pCountryCode, 
	@vInvoiceNum, 
	'STANDARD', --constant_invoice_type,
	@pEffectiveDate, 
	@pAdjustmentAmount, 
	@iCurrencyCode,
	'IMMEDIATE', --constant_terms_name,
	@vVendorPayGroup, 
	@vCheckDescription
);

--Split the account #
DECLARE @vLegalEntity varchar(50)
DECLARE @vNaturalAccount varchar(50)
DECLARE @vSubAccount varchar(50)
DECLARE @vProductLineDept varchar(50)
DECLARE @vLanguageMarket varchar(50)
DECLARE @vChannel varchar(50)
DECLARE @vSegment7 varchar(50)
EXEC QSPCanadaFinance.dbo.gl_split_gl_account_num 
	@pGlAccountNum	= @pDistGLAccountNum, 
	@pLegalEntity		= @vLegalEntity		OUTPUT , 
	@pNaturalAccount	= @vNaturalAccount	OUTPUT , 
	@pSubAccount		= @vSubAccount	OUTPUT , 
	@pProductLineDept	= @vProductLineDept	OUTPUT , 
	@pLanguageMarket	= @vLanguageMarket	OUTPUT , 
	@pChannel		= @vChannel		OUTPUT , 
	@pSegment7		= @vSegment7		OUTPUT 
		
INSERT INTO QSPOracleInterface.dbo.OM_TBL_AP_INV_LINES_INTERFACE (
	country_code, 
	invoice_num, 
	line_number,
	[description], 
	amount, 
	dist_legal_entity,
	dist_natural_account, 
	dist_sub_account, 
	dist_product_line_dept,
	dist_language_market, 
	dist_channel, 
	dist_segment7,
	prepay_legal_entity, 
	prepay_natural_account, 
	prepay_sub_account,
	prepay_product_line_dept, 
	prepay_language_market, 
	prepay_channel,
	prepay_segment7
) VALUES (
	@pCountryCode, 
	@vInvoiceNum, 
	1, --c_line_number CONSTANT 
	   --om_tbl_ap_inv_lines_interface.line_number%TYPE := 1;,
	@vCheckDescription, 
	@pAdjustmentAmount, 
	@vLegalEntity,
	@vNaturalAccount, 
	@vSubAccount, 
	@vProductLineDept,
	@vLanguageMarket, 
	@vChannel, 
	@vSegment7,
	'062', --constant_prepay_legal_entity,
	'1656',--constant_prepay_natural_account, 
	'0000',--constant_prepay_sub_account,
	'0000',--constant_prepay_product_line_dept, 
	'00',--constant_prepay_language_market, 
	'00',--constant_prepay_channel,
	'000'--constant_prepay_segment7
);

INSERT INTO QSPOracleInterface.dbo.OM_TBL_PO_VENDORS_INTERFACE (
	country_code, 
	invoice_num, 
	vendor_number,
	vendor_site_name
) VALUES (
	@pCountryCode, 
	@vInvoiceNum, 
	@vVendorNumber,
	@vVendorSiteName
);
GO
