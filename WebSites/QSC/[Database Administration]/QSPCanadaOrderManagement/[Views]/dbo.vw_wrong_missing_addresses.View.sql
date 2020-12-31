USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_wrong_missing_addresses]    Script Date: 06/07/2017 09:19:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_wrong_missing_addresses]
AS
SELECT     ca.ID AS CA, bth.OrderID, coh.Instance, cod.TransID, tch.Classroom, tch.LastName AS TeacherName, ISNULL(stu.FirstName, '') 
                      + ' ' + stu.LastName AS ParticipantName, cod.Recipient AS SubscriberName, ISNULL(Cust.FirstName, '') + ' ' + ISNULL(Cust.LastName, '') 
                      AS PurchaserName, Cust.Phone AS CustomerPhone, Cust.Address1 AS CustomerAddress1, Cust.Address2 AS CustomerAddress2, 
                      Cust.City AS CustomerCity, Cust.State AS CustomerState, Cust.Zip AS CustomerZip, 
                      CASE WHEN cod.ProductCode = 'NNNN' THEN 'Could not read Catalogue Code - Illegible Item' WHEN Isnull(cod.Recipient, '') 
                      = '' THEN 'Missing Recipient Name' WHEN (IsNull(Cust.Address1, '') = '' AND Isnull(Cust.City, '') = '') OR
                      (IsNull(Cust.State, '') = '' AND Isnull(Cust.Zip, '') = '') OR
                      (IsNull(Cust.Address1, '') = '' AND Isnull(Cust.State, '') = '') OR
                      (IsNull(Cust.City, '') = '' AND Isnull(Cust.Zip, '') = '') THEN 'Address Missing or Incomplete' WHEN IsNull(Cust.Address1, '') 
                      = '' THEN 'Missing Street Address' WHEN Isnull(Cust.City, '') = '' THEN 'Missing City' WHEN Isnull(Cust.State, '') 
                      = '' THEN 'Missing Province' WHEN Isnull(Cust.Zip, '') = '' THEN 'Missing Postal Code' ELSE 'Customer Status Error' END AS EN_Reason, 
                      cod.ProductCode AS TitleCode, cod.ProductName AS MagazineTitle, cod.Quantity AS Numberofissues, cod.CatalogPrice
FROM QSPCanadaCommon..Campaign ca
			INNER JOIN CustomerOrderHeader coh ON coh.CampaignID = ca.ID 
			INNER JOIN Customer Cust ON coh.CustomerBillToInstance = Cust.Instance 
			INNER JOIN Batch bth ON coh.OrderBatchID = bth.ID AND coh.OrderBatchDate = bth.[Date]
			INNER JOIN CustomerOrderDetail cod ON coh.Instance = cod.CustomerOrderHeaderInstance 
			INNER JOIN Student stu ON coh.StudentInstance = stu.Instance 
			INNER JOIN Teacher tch ON stu.TeacherInstance = tch.Instance          
WHERE (ca.StartDate >= '2005-07-01') AND (ca.EndDate <= '2006-06-30')
			AND (bth.OrderQualifierID <> 39014) 
			AND (cod.DelFlag <> 1) 
			AND (cod.ProductType IN (46001, 46006)) 
			AND (cod.Recipient IS NULL OR
                      cod.Recipient = '' OR
                      Cust.Address1 IS NULL OR
                      Cust.Address1 = '' OR
                      Cust.City IS NULL OR
                      Cust.City = '' OR
                      Cust.State IS NULL OR
                      Cust.State = '' OR
                      Cust.Zip IS NULL OR
                      Cust.Zip = '' OR
                      Cust.StatusInstance = 301)
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
         Begin Table = "sysobjects"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 206
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
      Begin ColumnWidths = 26
         Width = 284
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
         Width = 1440
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_wrong_missing_addresses'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_wrong_missing_addresses'
GO
