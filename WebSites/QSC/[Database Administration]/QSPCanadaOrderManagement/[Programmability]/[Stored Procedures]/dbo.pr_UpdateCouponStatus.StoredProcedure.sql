USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdateCouponStatus]    Script Date: 06/07/2017 09:20:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_UpdateCouponStatus] 
   @sCouponID nvarchar(50) = ''
 AS


UPDATE Coupon
        SET IsUsed=1 
 WHERE ID = @sCouponID
GO
