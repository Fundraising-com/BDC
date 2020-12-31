USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[UpdateVendorPaygroup]    Script Date: 06/07/2017 09:17:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[UpdateVendorPaygroup] 
AS
Begin
Declare @TabVendor Table(
		      Tindex			Int Identity,
		      AccountId			Int, 
		      Vendor			Varchar(50), 
		      PaySite			Varchar(50)
		      
		    )	
Declare
	@AccountID		int,
	@Vendor		varchar(50) ,
	@PaySite		varchar(50) ,
	@MaxRowCount	int
	

				--GROUP		VENDOR		PAYSITE	
Insert Into @TabVendor Values(	9999	,	123453	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	9974	,	150301	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	14677	,	150303	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	8427	,	74442	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	8800	,	150304	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	1945	,	150305	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	16691	,	82825	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	8139	,	122807	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	8064	,	150306	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	7036	,	144242	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	6566	,	150222	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	16419	,	150217	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	2473	,	150307	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	2589	,	150308	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	2642	,	101981	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	2652	,	150309	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	3122	,	150310	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	3574	,	150311	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	3587	,	150312	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	3589	,	150314	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	3801	,	146040	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	4544	,	150315	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	4617	,	149609	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	4757	,	150316	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	5438	,	145069	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	6333	,	150317	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	6554	,	145965	,	'SUDBURY'	)
Insert Into @TabVendor Values(	6591	,	150318	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	7026	,	94089	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	7061	,	150319	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	7370	,	150320	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	7396	,	146099	,	'BRANTFORD'	)
Insert Into @TabVendor Values(	7592	,	150321	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	7635	,	150322	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	7648	,	146022	,	'CAMBRIDGE'	)
Insert Into @TabVendor Values(	7904	,	146092	,	'ELORA'	)
Insert Into @TabVendor Values(	8261	,	44237	,	'HAMILTON'	)
Insert Into @TabVendor Values(	8470	,	150323	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	8514	,	146090	,	'MILTON'	)
Insert Into @TabVendor Values(	8837	,	145985	,	'INGLEWOOD'	)
Insert Into @TabVendor Values(	9396	,	150324	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	9433	,	150326	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	9447	,	150327	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	9599	,	144911	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	9760	,	146134	,	'OSHAWA'	)
Insert Into @TabVendor Values(	9766	,	150328	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	9826	,	150329	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	10006	,	146144	,	'MARKHAM'	)
Insert Into @TabVendor Values(	10039	,	144910	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	10054	,	144912	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	10113	,	144914	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	10963	,	146085	,	'ARNPRIOR'	)
Insert Into @TabVendor Values(	11619	,	149611	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	13451	,	149580	,	'QC-LE GARDEUR'	)
Insert Into @TabVendor Values(	14126	,	146033	,	'GREENFIELD PARK'	)
Insert Into @TabVendor Values(	15431	,	145958	,	'REGINA'	)
Insert Into @TabVendor Values(	15817	,	146121	,	'MILESTONE'	)
Insert Into @TabVendor Values(	15884	,	145957	,	'WHITE CITY'	)
Insert Into @TabVendor Values(	16041	,	145981	,	'MILDEN'	)
Insert Into @TabVendor Values(	16042	,	146118	,	'PLENTY'	)
Insert Into @TabVendor Values(	16050	,	97775	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18302	,	145983	,	'THUNDER BAY'	)
Insert Into @TabVendor Values(	18349	,	144931	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18410	,	142021	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18416	,	125891	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18429	,	73792	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18473	,	93112	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18516	,	145984	,	'LONDON'	)
Insert Into @TabVendor Values(	18636	,	59805	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18665	,	116925	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18053	,	146125	,	'KAMLOOPS'	)
Insert Into @TabVendor Values(	18510	,	144384	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18519	,	20924	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	6920	,	113845	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	7339	,	118025	,	'ON-WINDSOR'	)
Insert Into @TabVendor Values(	8671	,	145599	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	749	,	146071	,	'CAMROSE'	)
Insert Into @TabVendor Values(	18592	,	94707	,	'AB-STONY PLAIN'	)
Insert Into @TabVendor Values(	18595	,	118032	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	8881	,	144382	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	8318	,	44237	,	'BOLTON'	)
Insert Into @TabVendor Values(	8857	,	99601	,	'ON-BRAMPTON'	)
Insert Into @TabVendor Values(	17095	,	144403	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	8148	,	146060	,	'WELLAND'	)
Insert Into @TabVendor Values(	8153	,	144392	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18196	,	5883	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	10173	,	144377	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	10182	,	146056	,	'KINGSTON'	)
Insert Into @TabVendor Values(	10214	,	146055	,	'KINGSTON'	)
Insert Into @TabVendor Values(	10710	,	146052	,	'GLOUCESTER'	)
Insert Into @TabVendor Values(	10831	,	146051	,	'KANATA'	)
Insert Into @TabVendor Values(	10840	,	117532	,	'ORLEANS'	)
Insert Into @TabVendor Values(	9165	,	146044	,	'TORONTO'	)
Insert Into @TabVendor Values(	9205	,	139642	,	'M6P3P8-TORONTO'	)
Insert Into @TabVendor Values(	16742	,	144353	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	8981	,	146019	,	'COLLINGWOOD'	)
Insert Into @TabVendor Values(	9003	,	146017	,	'BARRIE'	)
Insert Into @TabVendor Values(	9009	,	111224	,	'L0L1Y0-MINESING'	)
Insert Into @TabVendor Values(	9013	,	146016	,	'CREEMORE'	)
Insert Into @TabVendor Values(	9039	,	145994	,	'BARRIE'	)
Insert Into @TabVendor Values(	10003	,	145058	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	10101	,	145600	,	'ON-STOUFFVILLE'	)
Insert Into @TabVendor Values(	5177	,	41892	,	'QSP-MONCTON'	)
Insert Into @TabVendor Values(	13617	,	145070	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	14085	,	18514	,	'QC-PIERREFONDS'	)
Insert Into @TabVendor Values(	14112	,	111168	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	10720	,	150713	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	11056	,	150714	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	15532	,	150715	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	15567	,	150716	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	15741	,	150717	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	15951	,	150718	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	16563	,	150719	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	17220	,	150720	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	17741	,	150721	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18151	,	150722	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18556	,	150723	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18581	,	150724	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18673	,	150725	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18413	,	150726	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	7280	,	150727	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	493	,	150728	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	565	,	150729	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	600	,	150732	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	748	,	150733	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	352	,	150747	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	1414	,	150748	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18593	,	150749	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18597	,	150750	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18641	,	150751	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18211	,	150752	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	8093	,	150753	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18497	,	150754	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	10409	,	150755	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	10862	,	150756	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	9221	,	150757	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	9310	,	150758	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	9372	,	150759	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	16586	,	150760	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	7509	,	150761	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	7510	,	150762	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	7541	,	150763	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	7547	,	150764	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	9056	,	150765	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18527	,	150766	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	5169	,	150767	,	'ON-SCARBOROUGH'	)
Insert Into @TabVendor Values(	18334	,	150768	,	'ON-SCARBOROUGH'	)
					
						
Select @MaxRowCount= Count(*)  from @TabVendor


While @MaxRowCount > 0 
Begin

Select    @AccountID	= AccountId,
	@Vendor	=Vendor,	
	@PaySite	=PaySite
From    @TabVendor where Tindex = @MaxRowCount

Update QSPCanadaCommon..CAccount
Set VendorNumber = @Vendor,
      VendorSiteName=@PaySite
Where Id = @AccountID

select  Cast (@AccountID as varchar) + ' '+@Vendor+' '+@PaySite


Set @MAxRowcount =@MAxRowcount -1

End


End
GO
