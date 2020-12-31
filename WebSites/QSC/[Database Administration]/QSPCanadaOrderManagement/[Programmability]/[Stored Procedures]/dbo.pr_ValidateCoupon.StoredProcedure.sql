USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ValidateCoupon]    Script Date: 06/07/2017 09:20:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ValidateCoupon] 
   @CouponID nvarchar(50) = ''
 AS

DECLARE @iCouponSetID int

SET @iCouponSetID = -1

SELECT @iCouponSetID = CouponSetID
  FROM  Coupon
WHERE ID = @CouponID 
	 AND IsUsed = 0

if  @iCouponSetID = -1
begin

SELECT @iCouponSetID = CouponSetID
  FROM  Coupon
WHERE ID = @CouponID 
	 AND IsUsed = 1

if @iCouponSetID <> -1
	SET @iCouponSetID = 0

end

IF @CouponID='FREESUB'
	SET @iCouponSetID = 1

SELECT @iCouponSetID
GO
