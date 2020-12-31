USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[PrepStudentStagingForUnigistix]    Script Date: 06/07/2017 09:20:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE      procedure [dbo].[PrepStudentStagingForUnigistix]
	 @orderid int
as

	select distinct p.product_code,qsppremiumid into #premiums 
			from  QSPCanadaProduct..Product as P,
				QSPCanadaProduct..Pricing_Details as PD,
				QSPCanadaProduct..Program_Details as PGD
			where 		
				p.type=46001
				and p.product_code=pgd.product_code
				and p.product_year = pd.pricing_year
				and p.product_season = pd.pricing_season
				and pd.product_code=pgd.product_code
				and pgd.program_year = pd.pricing_year
				and pgd.program_season = pd.pricing_season
				and pgd.taxregionid = pd.taxregionid
				and pd.programsectionid= pgd.programsectionid
				and product_year=2005
				--and premium_code in ('1','3','6')    
				and IsNull(QSppremiumId,0) in ('1','3','6')  
				and premium_code in ('1','2','3','4','5','6') 
				
				

			select distinct p.product_code,qsppremiumid into #rpremiums from  QSPCanadaProduct..Product as P,
					QSPCanadaProduct..Pricing_Details as PD,
					QSPCanadaProduct..Program_Details as PGD
				where 					
					p.type=46001
					and p.product_code=pgd.product_code
					and p.product_year = pd.pricing_year
					and p.product_season = pd.pricing_season
					and pd.product_code=pgd.product_code
					and pgd.program_year = pd.pricing_year
					and pgd.program_season = pd.pricing_season
					and pgd.taxregionid = pd.taxregionid
					and pd.programsectionid= pgd.programsectionid
					and product_year=2005
					--and premium_code in ('2','4','5')    
					and IsNull(QSppremiumId,0) in ('2','4','5') 
					and premium_code in ('1','2','3','4','5','6')   
					
					

			select s.instance,productcode,count(*)as qspremiumcount into #rstudent from
				student s, customerorderheader coh,customerorderdetail cod,batch,		
					#premiums
				where 
					coh.orderbatchdate=date and coh.orderbatchid=id
					and coh.instance= cod.customerorderheaderinstance
					and coh.studentinstance = s.instance
					and orderid=@orderid
					and #premiums.product_code=cod.productcode
					group by s.instance,productcode
			
			select s.instance,productcode,count(*)as otherpremiumcount into #ostudent from
				student s, customerorderheader coh,customerorderdetail cod,batch,		
					#rpremiums
				where 
					coh.orderbatchdate=date and coh.orderbatchid=id
					and coh.instance= cod.customerorderheaderinstance
					and coh.studentinstance = s.instance
					and orderid=@orderid
					and #rpremiums.product_code=cod.productcode
					group by s.instance,productcode
--sp_columns 'UnigistixStudentStaging'
--delete from UnigistixStudentStaging
			insert UnigistixStudentStaging 
			(StudentInstance,
				StudentLastName,
				StudentFirstName
				)
			select distinct s.instance, lastname,Firstname from
				student s, customerorderheader coh,batch
				where 
					coh.orderbatchdate=date and coh.orderbatchid=id
					and coh.studentinstance = s.instance
					and orderid=@orderid
				and s.instance not in
					(select distinct studentinstance from UnigistixStudentStaging)

			update 	UnigistixStudentStaging set ReadersPremiums = isnull(qspremiumcount,0) from
						#rstudent,UnigistixStudentStaging where
							#rstudent.instance = StudentInstance	
	
			update 	UnigistixStudentStaging set OtherPremiums = isnull(otherpremiumcount,0) from
						#ostudent,UnigistixStudentStaging where
							#ostudent.instance = StudentInstance		



drop table  #premiums
drop table  #rpremiums
drop table  #rstudent
drop table #ostudent
GO
