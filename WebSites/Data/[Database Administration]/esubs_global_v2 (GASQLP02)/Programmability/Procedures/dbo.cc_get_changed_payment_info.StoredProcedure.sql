USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_get_changed_payment_info]    Script Date: 02/14/2014 13:04:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--JF Lavigne
--Pour les cheques sort les event avec un changement d'addresse



--SI ON SORT LE 15 (une journee ppur faire les changements)
--Donc le 15 mars, on veut tout les changments de payment info depuis le 16 fevrier (leur cheque na pas ete fait encore)-->Poutr exu qui ont deja deja recu un paiemnt
--car ceux qui nont jamais recu de paiement vienne d'etre entre dans le systeme compatble dans les derniers jours

--***exceptions possible changement daddresse apres le 16 ou le 14 apres que le futurs chques ont ete entree....


CREATE     PROCEDURE [dbo].[cc_get_changed_payment_info] 
as
BEGIN



declare @start_date as datetime
declare @checkPeriod as int

SET @checkPeriod = (select max(payment_period_id) from payment_period)
    select 
	@start_date = dateadd(month,1,start_date)
    from 	
	payment_period 
    where
	payment_period_id = (@checkPeriod - 1)

--print @start_date


--ceux qui existe deja dans le system comptable (qui on un payment)
select pi.event_id,
       e.event_name,
       pi.payment_name,
       pa.address_1,
       pa.city,
       pa.zip_code, 
       pa.country_code,
       pa.subdivision_code
from payment_info pi
     inner join payment p on pi.payment_info_id = p.payment_info_id
     inner join event e on pi.event_id = e.event_id
     inner join postal_address pa on pi.postal_address_id = pa.postal_address_id
where pi.create_date > @start_date
      and pi.active = 1 
     
group by pi.event_id,
         e.event_name,
         pi.payment_name,
         pa.address_1,
         pa.city,
         pa.zip_code,
         pa.country_code,
         pa.subdivision_code

order by pi.event_id

--les addresses qui sont donnees du 5 au 15 pour la prochaine periode n'ont pas de cheques....
--mais leur addresses pourraient etre modifie
/*

select cd.event_id,
       pa.zip_code,
       pa.address_1,
       pa.city,
       pa.zip_code, 
       pa.country_code,
       pa.subdivision_code,
       pi.payment_name,
       max(pi.create_date)
from cc_check_diff cd
     inner join payment_info pi on cd.event_id = pi.event_id
     inner join postal_address pa on pi.postal_address_id = pa.postal_address_id
where pi.create_date > '2006-05-16'
group by cd.event_id, 
         pa.zip_code,
         pa.address_1,
         pa.city,
         pa.zip_code, 
         pa.country_code,
         pa.subdivision_code,
         pi.payment_name
having count(pi.payment_info_id) >1
order by max(pi.create_date) 


*/



end
GO
