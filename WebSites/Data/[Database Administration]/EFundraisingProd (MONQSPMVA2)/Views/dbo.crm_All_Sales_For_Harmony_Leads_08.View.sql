USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[crm_All_Sales_For_Harmony_Leads_08]    Script Date: 02/14/2014 13:01:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create      VIEW [dbo].[crm_All_Sales_For_Harmony_Leads_08]
AS
SELECT     fy.fm, c.name as fc, v.fc_com, la.lead_activity_date, L.lead_id, L.organization, p.description, fy.cafiscal, fy.mag_net_sales_f, fy.mus_net_sales_f, fy.book_net_sales_f, 
                      fy.gift_net_sales_f, fy.food_net_sales_f, fy.mag_net_sales_s, fy.mus_net_sales_s, fy.book_net_sales_s, fy.gift_net_sales_s, fy.food_net_sales_s, 
                      fy.choc_net_sales_s, fy.choc_net_sales_f
	
FROM         dbo.lead L left JOIN

             --join sur les activite pour avoir les transfered date (peut y avoir plusieurs active de transfer, on prend la derniere)
             (select lead_id, lead_activity_type_id, max(lead_activity_date) as lead_activity_date, max(completed_date) as completed_date
		from lead_activity where lead_activity_type_id = 9 group by lead_id,lead_activity_type_id  ) la
	        on la.lead_id = l.lead_id  
 left outer join

   --join sur les comments pour avoir le nom du consultant qui a fait le dernier comment
     --(peut avoir plusieurs comments le meme jour precedent le transfer par des consultants differents, on prend le dernier)
      (SELECT 
	   l.lead_id,
   	  max(case
          when c.Entry_Date Between a.completed_date-0.42 And a.completed_date then 
           cons.name  end) as fc_com
       FROM  Lead_Activity a INNER JOIN 
            Lead l ON a.lead_id = l.lead_id INNER JOIN
            Comments c ON l.lead_id = c.Lead_ID inner join
            consultant cons on cons.consultant_id = c.consultant_id
       WHERE a.lead_activity_type_id = 9
       group by l.lead_id
       having max(case
        when c.Entry_Date Between a.completed_date-0.42 And a.completed_date then 
        c.Consultant_ID end) is not null
) v
on v.lead_id = l.lead_id 
inner join
             dbo.consultant c on c.consultant_id = l.consultant_id inner join
             promotion p on l.promotion_id = p.promotion_id inner join 
              dbo.xHarmony x ON x.lead_id = L.lead_id INNER JOIN
                          --en rajoutant fm, environ 3000 nouveau record, si on rajoute account, le double --> un lead peut resultant en plusieurs account
                            (SELECT     l.lead_id AS Lead_id, cafiscal, acc.fm, SUM(Mag_Net_Sales_F) AS mag_net_sales_f, SUM(Mus_Net_Sales_F) AS mus_net_sales_f, 
                                                   SUM(Book_Net_Sales_F) AS book_net_sales_f, SUM(Gift_Net_Sales_F) AS gift_net_sales_f, SUM(Food_Net_Sales_F) 
                                                   AS food_net_sales_f, SUM(Mag_Net_Sales_S) AS mag_net_sales_s, SUM(Mus_Net_Sales_S) AS mus_net_sales_s, 
                                                   SUM(Book_Net_Sales_S) AS book_net_sales_s, SUM(Gift_Net_Sales_S) AS gift_net_sales_s, SUM(Food_Net_Sales_S) 
                                                   AS food_net_sales_s, SUM(Choc_Net_Sales_s) AS choc_net_sales_s, SUM(Choc_Net_Sales_F) AS choc_net_sales_f
                            FROM  lead l inner join (--je prend mes matching code de 3 colonnes et les mets sous la meme
                                  select account_no as id, matching_code,account_name from (  
                                            select account_no, qsp_account_matching_code as matching_code, account_Name from crm_static_all_accounts
                                            UNION ALL
                                            select account_no, qsp_cust_billing_matching_code as matching_code, account_Name from crm_static_all_accounts
                                            UNION ALL
                                            select account_no,  qsp_cust_shipping_matching_code as matching_code, account_Name from crm_static_all_accounts) a
                                            group by account_no, matching_code,account_Name)qsp
                                              ----------------------------------------                                              ---------------------------------------------------------------------
                                          ON l.matching_code = qsp.matching_code INNER JOIN
                                          QSPCommon.dbo.FY08_Account_byproduct acc ON acc.AccountInstance = qsp.id
                            WHERE      l.matching_code <> 'invalid'
                            GROUP BY l.lead_id, cafiscal, acc.fm) fy ON L.lead_id = fy.Lead_id
GO
