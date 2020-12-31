USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[gl_ins_magnet_adj]    Script Date: 06/07/2017 09:17:22 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[gl_ins_magnet_adj]
  @pAdjustmentID	int,
  @pAdjustmentAmount	decimal(10,2),
  @pTransactionTypeID	int,
  @pEntityID		int, 
  @Retval		int output
AS

-- =~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~
-- Description:
-- This procedure calls the om_pack gl layer procedure to insert 
-- the details of an adjustment into the General Ledger tables.
-- 
-- Revision History:
-- June 2004 - Joshua Caesar 
-- Inital Revision based upon om_proc_ins_magnet_adj_gl previous Oracle system.
-- =~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~

DECLARE @sGl_Trans_entries varchar(50)
DECLARE @iRetVal int
SELECT @sGl_Trans_entries = '', @iRetVal = 0
DECLARE @iReturnCode int

--how do i format this to a varchar(50)
--c_prod_line_magazine CONSTANT OM_TBL_QSP_PRODUCT_LINE.qsp_product_line_id%TYPE := 1;
--v_pl_table          om_pack_gl.pl_tabletype;
--v_pl_table.DELETE;
--v_pl_table(1).pl_id := om_pack_constant.c_prod_line_magazine;
--v_pl_table(1).pl_amount := p_adjustment_amount;

--these two are both null, thats easy enough
--v_tax_table         om_pack_gl.tax_tabletype;
--v_tax_table.DELETE;
--v_misc_charge_table om_pack_gl.misc_charge_tabletype;
--v_misc_charge_table.DELETE;

exec  dbo.GL_Function  0,			0,
			 @pAdjustmentId,
					 @pTransactionTypeID,
					 @pEntityID,
					 @pAdjustmentAmount,
					 @Retval 		 Output
GO
