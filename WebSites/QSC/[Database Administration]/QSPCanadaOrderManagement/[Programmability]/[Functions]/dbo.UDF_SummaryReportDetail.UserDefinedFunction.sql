USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_SummaryReportDetail]    Script Date: 06/07/2017 09:21:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_SummaryReportDetail](@OrderId Int)  
/*********************************************************************************************************  
Created By MS March 27, 2006  
 Used in HomeRoom and Group Summary Reports  
MS April 20, 2006  
 Modified to remove Hardcoded prize code for DC ,and join to View for premiums  
MS June 01, 2006   
 Modified to get prize level from product instead of checking product Type and code  
MS July 05, 2006  
 Moved Udf to proc to reduce # of Reads  
MS Sept 12, 2006  
 NNNN Product code included in sales  
MS Sept 22 2006  
 Move Label etc to rdl.  
  
***********************************************************************************************************/  
RETURNS TABLE  
AS  
RETURN  
(  
SELECT  TOP 10000  
   acc.ID AS AccountId,  
   acc.Name AS AccountName,  
   cont.firstName,  
   cont.LastName,  
   SUBSTRING(adShip.Street1, 1, 50) AS AccountAddress1,   
   SUBSTRING(adShip.Street2, 1, 50) AS AccountAddress2,  
   adShip.City AS AccountCity,  
   adShip.StateProvince AS AccountProvince,  
   adShip.Postal_Code AS AccountPcode,  
   SUBSTRING(phone.PhoneNumber, 1, 15) AS PhoneNumber,  
   phone.Type,  
   Null AS ProgramList,  
   SUM(CASE b.orderQualifierID  
     WHEN 39009 THEN 0  
     ELSE  
      CASE cod.ProductType  
      WHEN 46001 THEN 1  
      WHEN 46006 THEN cod.Quantity  
      WHEN 46007 THEN cod.Quantity  
      WHEN 46012 THEN cod.Quantity  
      ELSE 0  
     END  
    END) AS MagQuantityReg,  
   SUM(CASE b.orderQualifierID  
     WHEN 39009 THEN 0  
     ELSE  
      CASE cod.ProductType  
      WHEN 46001 THEN cod.Price  
      WHEN 46006 THEN cod.Price  
      WHEN 46007 THEN cod.Price  
      WHEN 46012 THEN cod.Price  
      ELSE 0  
     END  
   END) AS MagAmountReg,  
   SUM(CASE WHEN b.OrderQualifierID <> 39009 AND cod.ProductType IN (46001, 46006, 46007, 46012) THEN cod.GroupProfitAmount ELSE 0 END) AS MagProfitReg,
   SUM(CASE b.orderQualifierID  
	 WHEN 39009 THEN   
	  CASE cod.ProductType  
	  WHEN 46001 THEN 1  
	  WHEN 46006 THEN cod.Quantity  
	  WHEN 46007 THEN cod.Quantity  
	  WHEN 46012 THEN cod.Quantity  
	  /*ELSE CASE cod.ProductType WHEN 46023 THEN CASE ISNULL(cod.IsVoucherRedemption,0) WHEN 0 THEN cod.Quantity ELSE 0 END 
		   ELSE CASE cod.ProductType WHEN 46024 THEN cod.Quantity 
				ELSE CASE cod.ProductType WHEN 46018 THEN cod.Quantity 
					ELSE CASE WHEN cod.ProductType IN (46002, 46020) THEN cod.Quantity ELSE 0 END
					END
				END
		   END*/
	  END
	 ELSE 0
   END) AS MagQuantityOnline,  
   SUM(CASE b.orderQualifierID  
     WHEN 39009 THEN     
      CASE cod.ProductType   
      WHEN 46001 THEN cod.Price  
      WHEN 46006 THEN cod.Price   
      WHEN 46007 THEN cod.Price  
      WHEN 46012 THEN cod.Price  
      /*ELSE CASE cod.ProductType WHEN 46023 THEN CASE ISNULL(cod.IsVoucherRedemption,0) WHEN 0 THEN cod.Price ELSE 0 END
		   ELSE CASE cod.ProductType WHEN 46024 THEN cod.Price 
				ELSE CASE cod.ProductType WHEN 46018 THEN cod.Price
					ELSE CASE WHEN cod.ProductType IN (46002, 46020) THEN cod.Price ELSE 0 END
					END
				END
		   END*/
	  END
     ELSE 0
   END) AS MagAmountOnLine, 
    SUM(CASE WHEN b.OrderQualifierID = 39009 AND cod.ProductType IN (46001, 46006, 46007, 46012) THEN cod.GroupProfitAmount ELSE 0 END) AS MagProfitOnline,

    SUM(CASE b.orderQualifierID  
		 WHEN 39009 THEN 0  
		 ELSE CASE cod.ProductType     
			 WHEN 46002 THEN cod.Quantity  
			 WHEN 46020 THEN cod.Quantity --jewelry
			 WHEN 46024 THEN cod.Quantity --Savings Pass
			 WHEN 46025 THEN cod.Quantity --Pretzel Rods
			 ELSE 0  
		 END
	END) AS GiftQuantityReg,  
   SUM(CASE b.orderQualifierID  
		 WHEN 39009 THEN 0  
		 ELSE CASE cod.ProductType     
			 WHEN 46002 THEN cod.Price   
			 WHEN 46020 THEN cod.Price   
			 WHEN 46024 THEN cod.Price --Savings Pass
			 WHEN 46025 THEN cod.Price --Pretzel Rods
			 ELSE 0   
		END
   END) AS GiftAmountReg,   
   SUM(CASE WHEN b.OrderQualifierID <> 39009 AND cod.ProductType IN (46002, 46020, 46024, 46025) THEN cod.GroupProfitAmount ELSE 0 END) AS GiftProfitReg,

    SUM(CASE b.orderQualifierID  
		 WHEN 39009 THEN CASE cod.ProductType     
							 WHEN 46002 THEN cod.Quantity  
							 WHEN 46020 THEN cod.Quantity --jewelry  
							 WHEN 46024 THEN cod.Quantity --Savings Pass  
							 WHEN 46025 THEN cod.Quantity --Pretzel Rods
							 ELSE 0
						 END
		 ELSE 0
	END) AS GiftQuantityOnline,  
   SUM(CASE b.orderQualifierID  
		 WHEN 39009 THEN CASE cod.ProductType     
							 WHEN 46002 THEN cod.Price   
							 WHEN 46020 THEN cod.Price   
							 WHEN 46024 THEN cod.Price --Savings Pass 
							 WHEN 46025 THEN cod.Price --Pretzel Rods
							 ELSE 0 
						END
		 ELSE 0
   END) AS GiftAmountOnline,   
   SUM(CASE WHEN b.OrderQualifierID = 39009 AND cod.ProductType IN (46002, 46020, 46024, 46025) THEN cod.GroupProfitAmount ELSE 0 END) AS GiftProfitOnline,

   SUM(CASE b.orderQualifierID  
		 WHEN 39009 THEN 0  
		 ELSE CASE cod.ProductType     
			 WHEN 46018 THEN cod.Quantity --cookie  
			 ELSE 0  
		 END
   END) AS CookieDoughQuantityReg,  
   SUM(CASE b.orderQualifierID  
		 WHEN 39009 THEN 0  
		 ELSE CASE cod.ProductType     
			 WHEN 46018 THEN cod.Price   
			 ELSE 0   
		 END
   END) AS CookieDoughAmountReg,
   SUM(CASE WHEN b.OrderQualifierID <> 39009 AND cod.ProductType IN (46018) THEN cod.GroupProfitAmount ELSE 0 END) AS CookieDoughProfitReg,
   
    SUM(CASE b.orderQualifierID  
		 WHEN 39009 THEN CASE cod.ProductType     
							 WHEN 46018 THEN cod.Quantity --cookie  
							 ELSE 0   
						END
		 ELSE 0
   END) AS CookieDoughQuantityOnline,  
   SUM(CASE b.orderQualifierID  
		 WHEN 39009 THEN CASE cod.ProductType     
							 WHEN 46018 THEN cod.Price   
							 ELSE 0  
						 END
		 ELSE 0
   END) AS CookieDoughAmountOnline,
   SUM(CASE WHEN b.OrderQualifierID = 39009 AND cod.ProductType IN (46018) THEN cod.GroupProfitAmount ELSE 0 END) AS CookieDoughProfitOnline,
  
   SUM(CASE b.orderQualifierID  
     WHEN 39009 THEN 0  
     ELSE  
      CASE cod.ProductType  
      WHEN 46023 THEN CASE ISNULL(cod.IsVoucherRedemption,0) WHEN 0 THEN  cod.Quantity ELSE 0 END
      ELSE 0
     END
    END) AS TrtQuantityReg,  
   SUM(CASE b.orderQualifierID  
     WHEN 39009 THEN 0  
     ELSE  
      CASE cod.ProductType  
      WHEN 46023 THEN CASE ISNULL(cod.IsVoucherRedemption,0) WHEN 0 THEN  cod.Price ELSE 0 END
      ELSE 0  
     END  
   END) AS TrtAmountReg, 
   SUM(CASE WHEN b.OrderQualifierID <> 39009 AND cod.ProductType IN (46023) AND ISNULL(cod.IsVoucherRedemption,0) = 0 THEN cod.GroupProfitAmount ELSE 0 END) AS TRTProfitReg,

   SUM(CASE b.orderQualifierID  
     WHEN 39009 THEN CASE cod.ProductType  
					  WHEN 46023 THEN CASE ISNULL(cod.IsVoucherRedemption,0) WHEN 0 THEN  cod.Quantity ELSE 0 END
					  ELSE 0 
					 END
	 ELSE 0 
    END) AS TrtQuantityOnline,  
   SUM(CASE b.orderQualifierID  
     WHEN 39009 THEN CASE cod.ProductType  
					  WHEN 46023 THEN CASE ISNULL(cod.IsVoucherRedemption,0) WHEN 0 THEN  cod.Price ELSE 0 END
					  ELSE 0   
					 END  
    ELSE 0
   END) AS TrtAmountOnline, 
   SUM(CASE WHEN b.OrderQualifierID = 39009 AND cod.ProductType IN (46023) AND ISNULL(cod.IsVoucherRedemption,0) = 0 THEN cod.GroupProfitAmount ELSE 0 END) AS TRTProfitOnline,
    
    SUM(CASE b.orderQualifierID  
     WHEN 39009 THEN 0
     ELSE
      CASE cod.ProductType     
      WHEN 46024 THEN cod.Quantity --Entertainment  
      ELSE 0
     END  
   END) AS EntertainmentQuantityReg,  
   SUM(CASE b.orderQualifierID  
     WHEN 39009 THEN 0
     ELSE
      CASE cod.ProductType     
      WHEN 46024 THEN cod.Price   
      ELSE 0
     END   
   END) AS EntertainmentAmountReg,
   SUM(CASE WHEN b.OrderQualifierID <> 39009 AND cod.ProductType IN (46024) THEN cod.GroupProfitAmount ELSE 0 END) AS EntertainmentProfitReg,

    SUM(CASE b.orderQualifierID  
     WHEN 39009 THEN CASE cod.ProductType     
							WHEN 46024 THEN cod.Quantity --Entertainment  
							ELSE 0
					 END  
      ELSE 0
   END) AS EntertainmentQuantityOnline,  
   SUM(CASE b.orderQualifierID  
     WHEN 39009 THEN CASE cod.ProductType     
					  WHEN 46024 THEN cod.Price   
					  ELSE 0
				     END   
     ELSE 0      
   END) AS EntertainmentAmountOnline,
   SUM(CASE WHEN b.OrderQualifierID = 39009 AND cod.ProductType IN (46024) THEN cod.GroupProfitAmount ELSE 0 END) AS EntertainmentProfitOnline,

   CASE pdLvlA.Prize_Level  
    WHEN 'A' THEN 1  
    ELSE 0  
   END AS LvlAQuantity,  
   CASE pdLvlB.Prize_Level  
    WHEN 'B' THEN 1  
    ELSE 0  
   END AS LvlBQuantity,  
   CASE pdLvlC.Prize_Level  
    WHEN 'C' THEN 1  
    ELSE 0  
   END AS LvlCQuantity,  
   CASE pdLvlD.Prize_Level  
    WHEN 'D' THEN 1  
    ELSE 0  
   END AS LvlDQuantity,  
   CASE pdLvlE.Prize_Level  
    WHEN 'E' THEN 1  
    ELSE 0  
   END AS LvlEQuantity,  
   CASE pdLvlF.Prize_Level  
    WHEN 'F' THEN 1  
    ELSE 0  
   END AS LvlFQuantity,  
   CASE pdLvlG.Prize_Level  
    WHEN 'G' THEN 1  
    ELSE 0  
   END AS LvlGQuantity,  
   CASE pdLvlH.Prize_Level  
    WHEN 'H' THEN 1  
    ELSE 0  
   END AS LvlHQuantity,  
   CASE pdLvlI.Prize_Level  
    WHEN 'I' THEN 1  
    ELSE 0  
   END AS LvlIQuantity,  
   CASE pdLvlJ.Prize_Level  
    WHEN 'J' THEN 1  
    ELSE 0  
   END AS LvlJQuantity,  
   CASE pdLvlK.Prize_Level  
    WHEN 'K' THEN 1  
    ELSE 0  
   END AS LvlKQuantity,  
  
   CASE ISNULL(teach.FirstName, '')  
    WHEN '' THEN ''  
    ELSE SUBSTRING(teach.FirstName, 1, 20) + ','  
   END AS TeacherFirstName,  
   CASE WHEN teach.LastName = 'ONLINE SALES' AND camp.Lang = 'FR' THEN 'COMMANDES EN LIGNE' ELSE SUBSTRING(dbo.[UDF_RemoveTitle](teach.LastName), 1, 30) END AS TeacherLastName,  
   teach.Instance AS TeacherInstance,   
   /*CASE b.OrderQualifierID  
    WHEN 39009 THEN 0  
    ELSE (SELECT TeacherCount FROM Batch WHERE OrderID = @OrderId)--dbo.UDF_GetHomeRoomCount(@OrderId,NULL,NULL,'CLASS')--b.TeacherCount  
   END AS RoomCount,*/
   (SELECT TeacherCount FROM Batch WHERE OrderID = @OrderId) AS RoomCount, 
   CASE teach.LastName   
    WHEN 'ONLINE SALES' THEN 'ZZZZZZZZZZ'   
    ELSE teach.Classroom  
   END AS Classroom,  
   b.CampaignID,  
   camp.Lang,   
   CASE teach.LastName  
    WHEN 'ONLINE SALES' THEN 'Y'   
    ELSE 'N'    
   END AS IsOnline,  
   b.OrderID,  
   b.ID AS BatchId,   
   b.[Date] AS BatchDate,  
   fm.FMID,  
   fm.FirstName AS FMFname,  
   fm.LastName AS FMLname,  
   CASE camp.IsStaffOrder  
    WHEN 0 THEN COUNT(CASE SIGN(LEN(ISNULL(rogers.Product_Code, '')))  
       WHEN 1 THEN  
        CASE ISNULL(rogers.Product_Instance, 0) - ISNULL(pd.Product_Instance, 0)  
        WHEN 0 THEN  
         CASE SIGN(ISNULL(pd.QSPPremiumID, 0))  
          WHEN 1 THEN 1  
           ELSE 0  
          END  
         ELSE 0  
        END  
       END)  
    ELSE 0  
   END AS RogersPremiumCount,  
   CASE camp.IsStaffOrder  
     WHEN 0 THEN COUNT(CASE SIGN(LEN(ISNULL(rd.Product_Code, '')))  
             WHEN 1 THEN CASE(ISNULL(rd.Product_Instance, 0) - ISNULL(pd.Product_Instance, 0))  
                       WHEN 0 THEN CASE SIGN(ISNULL(pd.QSPPremiumID, 0))  
                  WHEN 1 THEN 1  
                  ELSE 0  
                 END  
                    ELSE 0  
             END  
          END)  
     ELSE 0 END AS RDPremiumCount,  
   coh.Instance AS CustomerHeaderInstance,   
   stud.Instance AS StudentInstance,  
   stud.FirstName AS StudentFName,  
   stud.LastName AS StudentLName,  
   b.OrderQualifierID,  
	CASE teach.LastName   
		WHEN 'ONLINE SALES' THEN 'ZZZZZZZZZZ'   
		ELSE dbo.[UDF_RemoveTitle](teach.LastName)
	   END AS SortOrder
FROM  QSPCanadaOrderManagement..Batch b (NOLOCK)  
JOIN  QSPCanadaOrderManagement..Batch bShip (NOLOCK)  
    ON bShip.AccountID = b.ShipToAccountID  
    AND bShip.ID = b.ID  
    AND bShip.[Date] = b.[Date]  
JOIN  QSPCanadaCommon..Campaign camp (NOLOCK)  
    ON camp.ID = b.CampaignID  
JOIN  QSPCanadaCommon..FieldManager fm (NOLOCK)  
    ON fm.FMID = camp.FMID  
JOIN  QSPCanadaCommon..CAccount acc (NOLOCK)  
    ON acc.ID = b.AccountID  
LEFT JOIN QSPCanadaCommon.dbo.Phone phone (NOLOCK)  
    ON phone.PhoneListid = acc.PhoneListID  
    AND phone.Type = 30505  
LEFT JOIN QSPCanadaCommon..Contact cont (NOLOCK)  
    ON cont.ID = camp.ShipToCampaignContactID  
JOIN  QSPCanadaOrderManagement..CustomerorderHeader coh (NOLOCK)  
    ON coh.OrderBatchID = b.ID  
    AND coh.OrderBatchDate = b.[Date]  
JOIN  QSPCanadaOrderManagement..Student stud (NOLOCK)  
    ON stud.Instance = coh.StudentInstance  
JOIN      QSPCanadaOrderManagement..Teacher teach (NOLOCK)  
    ON teach.Instance = stud.TeacherInstance  
JOIN      QSPCanadaCommon..CAccount accShip (NOLOCK)  
    ON accShip.ID = b.ShipToAccountID  
JOIN      QSPCanadaCommon..Address adShip (NOLOCK)  
    ON adShip.AddressListID = accShip.AddressListID  
    AND adShip.Address_Type = 54001  
JOIN  QSPCanadaOrderManagement..CustomerOrderDetail cod (NOLOCK)  
    ON cod.CustomerOrderHeaderInstance = coh.Instance  
    AND cod.DelFlag = 0  
LEFT JOIN QSPCanadaProduct..Pricing_Details pd (NOLOCK)  
    ON pd.MagPrice_Instance = cod.PricingDetailsID   
LEFT JOIN QSPCanadaProduct..Product pdLvlA (NOLOCK)  
    ON pdLvlA.Product_Instance = pd.Product_Instance  
LEFT JOIN QSPCanadaProduct..Product pdLvlB (NOLOCK)  
    ON pdLvlB.Product_Instance = pd.product_Instance  
LEFT JOIN QSPCanadaProduct..Product pdLvlC (NOLOCK)  
    ON pdLvlC.Product_Instance = pd.Product_Instance  
LEFT JOIN QSPCanadaProduct..Product pdLvlD (NOLOCK)  
    ON pdLvlD.Product_Instance = pd.product_Instance  
LEFT JOIN QSPCanadaProduct..Product pdLvlE (NOLOCK)  
    ON pdLvlE.Product_Instance = pd.product_Instance  
LEFT JOIN QSPCanadaProduct..Product pdLvlF (NOLOCK)  
    ON pdLvlF.Product_Instance = pd.product_Instance  
LEFT JOIN QSPCanadaProduct..Product pdLvlG (NOLOCK)  
    ON pdLvlG.Product_Instance = pd.Product_Instance  
LEFT JOIN QSPCanadaProduct..Product pdLvlH (NOLOCK)  
    ON pdLvlH.Product_Instance = pd.Product_Instance      
LEFT JOIN QSPCanadaProduct..Product pdLvlI (NOLOCK)  
    ON pdLvlI.Product_Instance = pd.Product_Instance   
LEFT JOIN QSPCanadaProduct..Product pdLvlJ (NOLOCK)  
    ON pdLvlJ.Product_Instance = pd.Product_Instance       
LEFT JOIN QSPCanadaProduct..Product pdLvlK (NOLOCK)  
    ON pdLvlK.Product_Instance = pd.Product_Instance       
LEFT JOIN QSPCanadaProduct..Product rd (NOLOCK)  
    ON rd.Product_Instance = pd.Product_Instance  
    AND rd.Pub_Nbr = 39  
    AND pd.QSPPremiumID > 0   
LEFT JOIN QSPCanadaProduct..Product rogers (NOLOCK)  
    ON rogers.Product_Instance = pd.Product_Instance  
    AND rogers.Pub_Nbr = 43  
    AND pd.QSPPremiumID > 0  
WHERE  (cod.ProductType IN (46001, 46002, 46003, 46005, 46006, 46007, 46012, 46018, 46020, 46022, 46023, 46024, 46025)
OR   (pdLvlA.Prize_Level = 'A')  
OR   (pdLvlB.Prize_Level = 'B')  
OR   (pdLvlC.Prize_Level = 'C')  
OR   (pdLvlD.Prize_Level = 'D')  
OR   (pdLvlE.Prize_Level = 'E')  
OR   (pdLvlF.Prize_Level = 'F')  
OR   (pdLvlG.Prize_Level = 'G')  
OR   (pdLvlH.Prize_Level = 'H')  
OR   (pdLvlI.Prize_Level = 'I')  
OR   (pdLvlJ.Prize_Level = 'J')  
OR   (pdLvlK.Prize_Level = 'K'))  

AND   (b.OrderID = @OrderID  AND (b.OrderQualifierID NOT IN (39009) OR cod.IsShippedToAccount = 0)
OR		b.OrderID IN (SELECT DISTINCT OnlineOrderID  
					   FROM OnlineOrderMappingTable  
					   WHERE LandedOrderID = @OrderID))
		--AND (cod.IsShippedToAccount = 1 OR ISNULL(cod.DistributionCenterID, 0) = 0))
AND	cod.StatusInstance NOT IN (506)
GROUP BY b.OrderID,  
   b.ID,  
   b.[Date],       
   b.OrderQualifierID,  
   b.CampaignID,  
   camp.IsStaffOrder,  
   camp.Lang,  
   acc.ID,  
   acc.Name,  
   phone.PhoneNumber,  
   phone.Type,  
   adShip.Street1,  
   adShip.Street2,  
   adShip.City,  
   adShip.StateProvince,  
   adShip.Postal_Code,  
   cont.FirstName,  
   cont.LastName,  
   fm.FMID,  
   fm.FirstName,  
   fm.LastName,  
   teach.Instance,  
   teach.LastName,  
   teach.FirstName,  
   teach.Classroom,  
   b.TeacherCount,  
   stud.Instance,  
   stud.FirstName,  
   stud.LastName,    
   pd.PrdPremiumCode,   
   coh.Instance,  
   pdLvlA.Prize_Level,  
   pdLvlB.Prize_Level,  
   pdLvlC.Prize_Level,  
   pdLvlD.Prize_Level,  
   pdLvlE.Prize_Level,  
   pdLvlF.Prize_Level,  
   pdLvlG.Prize_Level,  
   pdLvlH.Prize_Level,  
   pdLvlI.Prize_Level,  
   pdLvlJ.Prize_Level,  
   pdLvlK.Prize_Level  
ORDER BY b.OrderID,  
   SortOrder   
)
GO
