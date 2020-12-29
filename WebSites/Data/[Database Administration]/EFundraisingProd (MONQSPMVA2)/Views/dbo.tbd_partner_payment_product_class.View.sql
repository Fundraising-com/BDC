USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[tbd_partner_payment_product_class]    Script Date: 02/14/2014 13:02:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[tbd_partner_payment_product_class]
AS
SELECT TOP 100 PERCENT YEAR(dbo.payment.payment_entry_date) AS [Year], MONTH(dbo.payment.payment_entry_date) AS [Month], 
               SUM(dbo.payment.payment_amount) AS SumPayment, dbo.partner.partner_name, dbo.product_class.description, dbo.sales_item.sales_item_no, 
               dbo.promotion.promotion_type_code
FROM  dbo.lead INNER JOIN
               dbo.client ON dbo.lead.lead_id = dbo.client.lead_id INNER JOIN
               dbo.sale ON dbo.client.client_sequence_code = dbo.sale.client_sequence_code AND dbo.client.client_id = dbo.sale.client_id INNER JOIN
               dbo.payment ON dbo.sale.sales_id = dbo.payment.sales_id INNER JOIN
               dbo.promotion ON dbo.lead.promotion_id = dbo.promotion.promotion_id INNER JOIN
               dbo.partner ON dbo.promotion.partner_id = dbo.partner.partner_id INNER JOIN
               dbo.sales_item ON dbo.sale.sales_id = dbo.sales_item.sales_id INNER JOIN
               dbo.scratch_book ON dbo.sales_item.scratch_book_id = dbo.scratch_book.scratch_book_id INNER JOIN
               dbo.product_class ON dbo.scratch_book.product_class_id = dbo.product_class.product_class_id
GROUP BY MONTH(dbo.payment.payment_entry_date), YEAR(dbo.payment.payment_entry_date), dbo.partner.partner_name, dbo.product_class.description, 
               dbo.sales_item.sales_item_no, dbo.promotion.promotion_type_code
HAVING (YEAR(dbo.payment.payment_entry_date) = 2007) AND (MONTH(dbo.payment.payment_entry_date) = 8) AND (dbo.sales_item.sales_item_no = 1) AND 
               (dbo.promotion.promotion_type_code IN ('AF', 'im'))
ORDER BY YEAR(dbo.payment.payment_entry_date), MONTH(dbo.payment.payment_entry_date)
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
         Configuration = "(H (1[50] 2[25] 3) )"
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
         Configuration = "(H (1 [56] 4 [18] 2))"
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
         Begin Table = "lead (dbo)"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 121
               Right = 279
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "client (dbo)"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 241
               Right = 252
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "sale (dbo)"
            Begin Extent = 
               Top = 246
               Left = 38
               Bottom = 361
               Right = 275
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "payment (dbo)"
            Begin Extent = 
               Top = 366
               Left = 38
               Bottom = 481
               Right = 240
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "promotion (dbo)"
            Begin Extent = 
               Top = 486
               Left = 38
               Bottom = 601
               Right = 245
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "partner (dbo)"
            Begin Extent = 
               Top = 606
               Left = 38
               Bottom = 721
               Right = 264
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "sales_item (dbo)"
            Begin Extent = 
               Top = 726
               Left = 38
               Bottom = 841
               Right = 274
          ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'tbd_partner_payment_product_class'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'  End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "scratch_book (dbo)"
            Begin Extent = 
               Top = 366
               Left = 278
               Bottom = 481
               Right = 468
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "product_class (dbo)"
            Begin Extent = 
               Top = 486
               Left = 283
               Bottom = 601
               Right = 475
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
      RowHeights = 220
      Begin ColumnWidths = 8
         Width = 284
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'tbd_partner_payment_product_class'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'tbd_partner_payment_product_class'
GO
