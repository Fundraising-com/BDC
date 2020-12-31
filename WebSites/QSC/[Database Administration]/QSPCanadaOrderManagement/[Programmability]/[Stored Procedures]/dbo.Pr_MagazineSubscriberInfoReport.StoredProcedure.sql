USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[Pr_MagazineSubscriberInfoReport]    Script Date: 06/07/2017 09:20:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Pr_MagazineSubscriberInfoReport] 
@Publisher_ID	int,
@FH_ID int,
@From_Batch_ID int,
@To_Batch_ID int,
@bHardCopy bit = null


AS
SELECT  Product.Pub_Nbr,
	  pub.Pub_Name,
	  Product.Fulfill_House_Nbr,
	  fh.Ful_Name,
	  codrh.REmitCode as TitleCode,
	   codrh.MagazineTitle,rb.RunID,codrh.RemitBatchID,
	  CONVERT(varchar(10), rb.Date, 101) AS BatchDate,
	  codrh.NumberOfIssues,
	  codrh.BasePrice,
 	  CASE codrh.Renewal when 'N' then 'New' when 'R' then 'Renewal' END as Renewal,
	  crh.FirstName,crh.LastName,
	  crh.Address1,crh.Address2,
	  crh.City,crh.State,crh.Zip
FROM    QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrh,
	 QSPCanadaOrderManagement..RemitBatch rb,
	 QSPCanadaOrderManagement..CustomerOrderHeader coh,
	 QSPCanadaOrderManagement..CustomerOrderDetail cod,
 	 QSPCanadaOrderManagement..CustomerRemitHistory crh,
	 QSPCanadaProduct..Pricing_Details pd,
	 QSPCanadaProduct..Product Product,
	 QSPCanadaProduct..Fulfillment_House fh,
	 QSPCanadaProduct..Publishers pub
WHERE    codrh.RemitBatchID = rb.id
	 AND cod.customerorderheaderinstance = codrh.customerorderheaderinstance
	 AND cod.transid = codrh.transid
	 AND coh.Instance = cod.CustomerOrderHeaderInstance
	AND codrh.CustomerRemitHistoryInstance = crh.Instance
	 AND cod.PricingDetailsID = pd.MagPrice_Instance
	 and pd.Product_Instance = Product.Product_Instance 
--	 AND pd.Pricing_Year = Product.Product_Year
--	 AND pd.Pricing_Season = Product.Product_Season
--	 AND codrh.TitleCode = Product.Product_Code 
	 AND Product.Fulfill_House_Nbr = fh.Ful_Nbr   
	 AND Product.Pub_Nbr = pub.Pub_Nbr
	 AND Product.Pub_Nbr = ISNULL(@Publisher_ID,Product.Pub_Nbr)
	 AND Product.Fulfill_House_Nbr = ISNULL(@FH_ID,Product.Fulfill_House_Nbr)
	 AND rb.RunID   between ISNULL(@From_Batch_ID, codrh.RemitBatchID) and ISNULL(@To_Batch_ID, codrh.RemitBatchID)    
	 AND fh.HardCopy = ISNULL(@bHardCopy, fh.HardCopy)   
	 AND CODRH.STATUS IN (42000, 42001) --needs to be sent, sent
/*and (
    (cod.productname like 'UP here%Explore Canada%' and ( crh.FirstName +''+crh.LastName like 'Robert Parsons%' OR
						           crh.FirstName +''+crh.LastName like 'Lynne Wray%' OR
		                                           crh.FirstName +''+crh.LastName like 'Anne Cusworth%'))
    OR	
   (cod.productname like 'US Weekly%' and (crh.FirstName +''+crh.LastName like 'Aran Wilson%' ))
      	
   OR
   (cod.productname like 'Chatelaine%'and (crh.FirstName +''+crh.LastName like 'Ann Grant%' ))

   OR
   (cod.productname like 'Guideposts Large Print%' and (crh.FirstName +''+crh.LastName like 'Bea Mooney%' ))

   OR
   (cod.productname like 'Travel%Leisure%' and (crh.FirstName +''+crh.LastName like 'Pat Burden%' ))

   OR
   (cod.productname like 'Progressive Farmer%' and (crh.FirstName +''+crh.LastName like 'Charlie Robbins%' OR
						    crh.FirstName +''+crh.LastName like 'Mike Walhout%'))
   OR 
   (cod.productname like 'American Girl%' and (crh.FirstName +''+crh.LastName like 'Hana Ready%' ))

   OR
   (cod.productname like 'Ontario gardener Living%' and (crh.FirstName +''+crh.LastName like 'Tom Dickins%' OR
						           crh.FirstName +''+crh.LastName like 'Magda OLESEN%' OR
		                                           crh.FirstName +''+crh.LastName like 'Anne Comer%' OR
							   crh.FirstName +''+crh.LastName like 'Joan Thomas%'))
    OR	
   (cod.productname like 'Men%Health%' and (crh.FirstName +''+crh.LastName like 'Trevor Tope%' ))
      	
   OR
   (cod.productname like 'Utne Magazine%'and (crh.FirstName +''+crh.LastName like 'Richard Vos%' ))

   
   OR
   (cod.productname like 'Progressive Farmer%' and (crh.FirstName +''+crh.LastName like 'Paul Martin%' OR
						    crh.FirstName +''+crh.LastName like 'Tom Olson%'))

)
*/

--and ProductName ='MoneySense'
--and CODRH.RemitCode='9313'
/*
and fh.ful_nbr in
(
select Ful_Nbr from qspcanadaproduct..fulfillment_house
where ful_name in  (
'Canada Wide Magazine Ltd.',
'Canada''s National History Society ',
'Productions Ciel Variable',
'Specialty Publishing Company',
'Reader''s Digest ',
'WellnessOptions Publishing Inc. ',


'Animal Hebdo Inc. ',
'Dell Magazines',
'Carus Publishing',
'Canada''s National History Society',
'Chart Communications Inc. ',
'Dirt Rag Magazine ',
'Dynamique Messageries ',
'Evangelical Fellowship of Canada ',
'Faze Publications ',
'Formula Media Group',
'Gems Girls'' Clubs',
'Navigator Publishing',
'NAZCA Éditions ',
'Horse Community Journals Inc. ',
'Modern Dog Inc. ',
'Outpost - In House ',
'Postexperts Inc. ',
'Prestige Fulfillment',
'Publication Fulfillment Service ',
'Publisher''s Creative System',
'Rapid Media',
'Target Audience Management Inc. ',
'The Oyster Group ',
'Together Magazine (ASL) ',
'Trajan Publishing Corp. ',
'Watershed Sentinel Publications ',
'Western Standard',
'What''s Up Kids',
'Woodstock Media Services ',
'World of Wheels',
'Your Workplace')

)
*/
ORDER BY fh.Ful_Name,pub.Pub_Name, codrh.TitleCode,codrh.RemitBatchID, crh.FirstName
GO
