Use QSPFulfillment
go

ALTER PROC [dbo].[pr_rpt_duplicate_campaign]    
AS    
BEGIN    
 SELECT 
		 c.account_id AS qsp_account_id
	   , c.campaign_id AS qsp_campaign_id   
	   , a.fulf_account_id AS eds_account_id    
	   , accs.account_status_name    
	   , a.fm_id    
	   , fsm.first_name    
	   , fsm.last_name    
	   , c.campaign_name    
	   , c.start_date    
	   , c.end_date    
	   , c.fiscal_year    
	   , c.deleted
	   , pt.program_type_name  
	   , c.create_date 
	   , c.update_date   
	   , c.create_user_id
	   , c.update_user_id
	   , (select count(*) from dbo.[order] where campaign_id = c.campaign_id and deleted = 0 ) as [Campaign_Orders]
	   , (select count(*) from dbo.[program_agreement_campaign] where campaign_id = c.campaign_id and deleted = 0) as [Campaign_PAs]
	   , fsm.deleted as [FSM_Deleted]
 FROM campaign c    
	  INNER JOIN (    
	  SELECT account_id    
	   , fiscal_year    
	   , program_type_id    
	   , COUNT(*) AS number_of_duplicate    
	  FROM campaign c    
	  WHERE c.program_type_id IN (7,11)    
		AND c.deleted = 0     
		AND c.fiscal_year = dbo.fnc_GetDateFiscalYR(GETDATE())    
	  GROUP BY c.account_id, c.fiscal_year, c.program_type_id    
	  HAVING COUNT(*) > 1    
	  ) t     
	   ON t.account_id = c.account_id    
	   AND t.fiscal_year = c.fiscal_year    
	   AND t.program_type_id = c.program_type_id
	   AND c.deleted = 0
	  LEFT JOIN program_type pt    
	   ON pt.program_type_id = c.program_type_id    
	  LEFT JOIN account a    
	   ON a.account_id = c.account_id    
	  LEFT JOIN account_status accs    
	   ON accs.account_status_id = a.account_status_id    
	  LEFT JOIN field_sales_manager fsm    
	   ON fsm.fm_id = a.fm_id
END 