USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[crm_all_leads]    Script Date: 02/14/2014 13:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[crm_all_leads]
AS
SELECT     l.consultant_id, l.lead_id, c.name AS Consultant, c.email_address AS consultant_email, l.lead_entry_date, la.lead_activity_type_id, la.lead_activity_id, 
                      la.lead_activity_date, la.completed_date, l.lead_assignment_date, l.salutation, l.first_name, l.last_name, l.organization, l.street_address, 
                      l.address_zone_id, l.city, l.state_code, l.zip_code, l.day_phone, l.day_time_call, l.evening_phone, l.fax, l.email, l.group_type_id, l.participant_count, 
                      l.fund_raising_goal, l.decision_maker, l.fund_raiser_start_date, l.comments, l.country_code, l.has_been_contacted, l.lead_priority_id, 
                      p.promotion_type_code, pt.promotion_type_name AS PromoType, p.promotion_name AS Promotion, l.fk_kit_type_id AS Kit_Type_ID, kt.Description AS KitType, 
                      l.day_phone_ext, l.evening_phone_ext, s.Time_Zone_Difference, l.promotion_id, l.kit_sent_date, pp.partner_id, l.title_id, l.campaign_reason_id, 
                      l.organization_type_id, l.group_web_site, l.other_phone, w.Web_Site_Name, l.interests, l.day_phone_is_good, l.evening_phone_is_good, 
                      l.account_number, l.activity_closing_reason_id, l.ext_consultant_id, l.customer_status_id, l.vif, l.wfc_customer_number, l.other_phone_is_good
FROM         dbo.lead AS l INNER JOIN
                      dbo.lead_activity AS la ON l.lead_id = la.lead_id INNER JOIN
                      dbo.Lead_Status AS ls ON l.lead_status_id = ls.Lead_Status_ID INNER JOIN
                      dbo.consultant AS c ON l.consultant_id = c.consultant_id INNER JOIN
                      efrcommon.dbo.promotion AS p ON l.promotion_id = p.promotion_id INNER JOIN
                      efrcommon.dbo.partner_promotion as pp ON pp.promotion_id = p.promotion_id INNER JOIN
                      efrcommon.dbo.Promotion_Type AS pt ON p.promotion_type_code = pt.Promotion_Type_Code INNER JOIN
                      dbo.Kit_Type AS kt ON l.fk_kit_type_id = kt.Kit_Type_ID LEFT OUTER JOIN
                      dbo.State AS s ON l.state_code = s.State_Code INNER JOIN
                      dbo.Lead_Activity_Type AS lat ON la.lead_activity_type_id = lat.Lead_Activity_Type_Id INNER JOIN
                      dbo.Web_Site AS w ON l.web_site_id = w.Web_Site_Id
WHERE     (c.is_active = 1) AND (c.department_id IN (7, 4, 18, 17))
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "l"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 262
            End
            DisplayFlags = 280
            TopColumn = 77
         End
         Begin Table = "la"
            Begin Extent = 
               Top = 114
               Left = 38
               Bottom = 222
               Right = 221
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ls"
            Begin Extent = 
               Top = 6
               Left = 300
               Bottom = 84
               Right = 457
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 222
               Left = 38
               Bottom = 330
               Right = 263
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 114
               Left = 259
               Bottom = 222
               Right = 449
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pt"
            Begin Extent = 
               Top = 330
               Left = 38
               Bottom = 438
               Right = 243
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "kt"
            Begin Extent = 
               Top = 222
               Left = 301
               Bottom = 330
               Right = 452
            End
            DisplayFlags = 280
            TopColumn = 0
         En' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'crm_all_leads'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'd
         Begin Table = "s"
            Begin Extent = 
               Top = 330
               Left = 281
               Bottom = 438
               Right = 469
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "lat"
            Begin Extent = 
               Top = 438
               Left = 38
               Bottom = 531
               Right = 229
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "w"
            Begin Extent = 
               Top = 438
               Left = 267
               Bottom = 516
               Right = 426
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'crm_all_leads'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'crm_all_leads'
GO
