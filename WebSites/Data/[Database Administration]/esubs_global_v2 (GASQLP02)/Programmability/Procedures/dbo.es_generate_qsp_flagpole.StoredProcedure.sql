USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_generate_qsp_flagpole]    Script Date: 02/14/2014 13:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Philippe Girard
-- Create date: 2007/02/05
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[es_generate_qsp_flagpole]
AS
BEGIN
	SET NOCOUNT ON;
    
    CREATE TABLE #qsp_matching_code (
	    [account_id] [int] NOT NULL,
	    [cust_billing_matching_code] [varchar](10) NOT NULL,
	    [cust_shipping_matching_code] [varchar](10) NOT NULL,
	    [account_matching_code] [varchar](10) NOT NULL,
    )

    INSERT INTO #qsp_matching_code (
        account_id
        , cust_billing_matching_code
        , cust_shipping_matching_code
        , account_matching_code
    )
    select acc.Id as account_id
        , case when qspcommon.dbo.fct_get_zzzzz(arzpbl)  =  '00000'
                 or qspcommon.dbo.fct_get_aa99(aradb1)  = '****'
            then 'invalid'
            else isnull(qspcommon.dbo.fct_get_zzzzz(arzpbl),'') + isnull(qspcommon.dbo.fct_get_aa99(aradb1),'')
          end as qsp_cust_blling_matching_code
        , case when qspcommon.dbo.fct_get_zzzzz(arzpsh)  =  '00000'
                 or qspcommon.dbo.fct_get_aa99(arads1)  = '****'
            then 'invalid'
            else isnull(qspcommon.dbo.fct_get_zzzzz(arzpsh),'') + isnull(qspcommon.dbo.fct_get_aa99(arads1),'')
        end as qsp_cust_shipping_matching_code
        , case when qspcommon.dbo.fct_get_zzzzz(acc.zip)  =  '00000'
                 or qspcommon.dbo.fct_get_aa99(acc.address1)  = '****'
            then 'invalid'
            else isnull(qspcommon.dbo.fct_get_zzzzz(acc.zip),'') + isnull(qspcommon.dbo.fct_get_aa99(acc.address1),'')
        end as qsp_account_matching_code 		                       
    from qdsdata..armcusp ac
        inner join qspcommon..caccount acc
            on acc.id = ac.arcust
    where acc.id in (
        select AccountInstance
        from qspcommon..cca
        group by accountInstance
        having max(CAFiscal) >= Year(getdate()) - 2
    )

   
    -- Delete account not found

    DELETE FROM qsp_matching_code WHERE account_id NOT IN (
        SELECT account_id
        FROM #qsp_matching_code
    )


    -- Update account found

    UPDATE qsp_matching_code 
    SET cust_billing_matching_code = #qsp.cust_billing_matching_code
      , cust_shipping_matching_code = #qsp.cust_shipping_matching_code
      , account_matching_code = #qsp.account_matching_code
    FROM #qsp_matching_code #qsp
    WHERE #qsp.account_id = qsp_matching_code.account_id


    -- Insert new account
 
    INSERT INTO qsp_matching_code (
        account_id
        , cust_billing_matching_code
        , cust_shipping_matching_code
        , account_matching_code
    )
    SELECT account_id
         , cust_billing_matching_code
         , cust_shipping_matching_code
         , account_matching_code
    FROM #qsp_matching_code
    WHERE account_id NOT IN (
        SELECT account_id
        FROM qsp_matching_code
    )

END
GO
