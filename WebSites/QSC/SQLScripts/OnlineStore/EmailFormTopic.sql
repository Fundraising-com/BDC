select * from store.SiteGoalType
select * from portal.GoalType

select * 
from store.EmailFormTopic t
join store.Page p on p.PageID = t.PageID
join store.pagetype pt on pt.code = p.pagetypecode
join store.Site s on s.siteid = p.siteid
order by s.siteid

select * from store.Page
select * from store.PageType

--test: PageID 37=qsp.ca store, 38=faculty store, 39=bdc store, 40=sponsor, 41=student

begin tran
insert store.emailformtopic values (37, 'General Question or Comment', 'General Question or Comment', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (38, 'General Question or Comment', 'General Question or Comment', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (39, 'General Question or Comment', 'General Question or Comment', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (40, 'General Question', 'General Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (41, 'General Question', 'General Question', 1, GETDATE(), NULL, NULL)

insert store.emailformtopic values (37, 'Fundraising With QSP Inquiry', 'Fundraising With QSP Inquiry', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (37, 'Magazine Question', 'Magazine Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (37, 'Online Store Support', 'Online Store Support', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (37, 'Order Question', 'Order Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (37, 'Product Information', 'Product Information', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (37, 'Organization/Student ID Inquiry', 'Organization/Student ID Inquiry', 1, GETDATE(), NULL, NULL)

insert store.emailformtopic values (38, 'Fundraising With QSP Inquiry', 'Fundraising With QSP Inquiry', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (38, 'Magazine Question', 'Magazine Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (38, 'Online Store Support', 'Online Store Support', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (38, 'Order Question', 'Order Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (38, 'Product Information', 'Product Information', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (38, 'Organization ID Inquiry', 'Organization ID Inquiry', 1, GETDATE(), NULL, NULL)

insert store.emailformtopic values (39, 'Fundraising With EFR Inquiry', 'Fundraising With QSP Inquiry', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (39, 'Magazine Question', 'Magazine Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (39, 'Online Store Support', 'Online Store Support', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (39, 'Order Question', 'Order Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (39, 'Product Information', 'Product Information', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (39, 'Organization ID Inquiry', 'Organization ID Inquiry', 1, GETDATE(), NULL, NULL)

insert store.emailformtopic values (40, 'Report Question', 'Report Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (40, 'Image Upload Question', 'Image Upload Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (40, 'Document Upload Question', 'Document Upload Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (40, 'Login/Account Information Question', 'Login/Account Information Question', 1, GETDATE(), NULL, NULL)

insert store.emailformtopic values (41, 'Report Question', 'Report Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (41, 'Image Upload Question', 'Image Upload Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (41, 'Login/Account Information Question', 'Login/Account Information Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (41, 'Registration Inquiry', 'Registration Inquiry', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (41, 'Student Email Issues', 'Student Email Issues', 1, GETDATE(), NULL, NULL)


--prod: PageID 37=qsp.ca store, 38=sponsor, 39=student, 40=faculty, 41=bdc

begin tran

insert store.emailformtopic values (37, 'General Question or Comment', 'General Question or Comment', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (37, 'Fundraising With QSP Inquiry', 'Fundraising With QSP Inquiry', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (37, 'Magazine Question', 'Magazine Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (37, 'Online Store Support', 'Online Store Support', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (37, 'Order Question', 'Order Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (37, 'Product Information', 'Product Information', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (37, 'Organization/Student ID Inquiry', 'Organization/Student ID Inquiry', 1, GETDATE(), NULL, NULL)

insert store.emailformtopic values (38, 'General Question', 'General Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (38, 'Report Question', 'Report Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (38, 'Image Upload Question', 'Image Upload Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (38, 'Document Upload Question', 'Document Upload Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (38, 'Login/Account Information Question', 'Login/Account Information Question', 1, GETDATE(), NULL, NULL)

insert store.emailformtopic values (39, 'General Question', 'General Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (39, 'Report Question', 'Report Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (39, 'Image Upload Question', 'Image Upload Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (39, 'Login/Account Information Question', 'Login/Account Information Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (39, 'Registration Inquiry', 'Registration Inquiry', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (39, 'Student Email Issues', 'Student Email Issues', 1, GETDATE(), NULL, NULL)

insert store.emailformtopic values (40, 'General Question or Comment', 'General Question or Comment', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (40, 'Fundraising With QSP Inquiry', 'Fundraising With QSP Inquiry', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (40, 'Magazine Question', 'Magazine Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (40, 'Online Store Support', 'Online Store Support', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (40, 'Order Question', 'Order Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (40, 'Product Information', 'Product Information', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (40, 'Organization ID Inquiry', 'Organization ID Inquiry', 1, GETDATE(), NULL, NULL)

insert store.emailformtopic values (41, 'General Question or Comment', 'General Question or Comment', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (41, 'Fundraising With EFR Inquiry', 'Fundraising With QSP Inquiry', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (41, 'Magazine Question', 'Magazine Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (41, 'Online Store Support', 'Online Store Support', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (41, 'Order Question', 'Order Question', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (41, 'Product Information', 'Product Information', 1, GETDATE(), NULL, NULL)
insert store.emailformtopic values (41, 'Organization ID Inquiry', 'Organization ID Inquiry', 1, GETDATE(), NULL, NULL)



select top 99 * from Messaging.Email
order by EmailID desc

select Distinct DisplayText AS EnglishText
from store.EmailFormTopic
where PageID between 37 and 41


select *
from store.EmailFormTopic
where PageID between 37 and 41

--test: PageID 37=qsp.ca store, 38=faculty store, 39=bdc store, 40=sponsor, 41=student
--prod: PageID 37=qsp.ca store, 38=sponsor, 39=student, 40=faculty, 41=bdc

--test
begin tran
insert store.EmailFormTopic values (37, 'Question ou commentaire d’ordre général', 'Question ou commentaire d’ordre général', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (37, 'Question sur le financement avec QSP', 'Question sur le financement avec QSP', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (37, 'Question sur les magazines', 'Question sur les magazines', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (37, 'Aide avec le magasin en ligne', 'Aide avec le magasin en ligne', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (37, 'Question sur une commande', 'Question sur une commande', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (37, 'Renseignements sur les produits', 'Renseignements sur les produits', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (37, 'Question sur l’id. de l’élève/l’organisation', 'Question sur l’id. de l’élève/l’organisation', 2, GETDATE(), null, null)

insert store.EmailFormTopic values (40, 'Question d’ordre général', 'Question d’ordre général', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (40, 'Question sur les rapports', 'Question sur les rapports', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (40, 'Question sur le téléchargement d’image', 'Question sur le téléchargement d’image', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (40, 'Question sur le téléchargement de document', 'Question sur le téléchargement de document', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (40, 'Question sur la connexion/les détails du compte', 'Question sur la connexion/les détails du compte', 2, GETDATE(), null, null)

insert store.EmailFormTopic values (41, 'Question d’ordre général', 'Question d’ordre général', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (41, 'Question sur les rapports', 'Question sur les rapports', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (41, 'Question sur le téléchargement d’image', 'Question sur le téléchargement d’image', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (41, 'Question sur la connexion/les détails du compte', 'Question sur la connexion/les détails du compte', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (41, 'Question sur l’inscription', 'Question sur l’inscription', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (41, 'Problème avec le courriel de l’élève', 'Problème avec le courriel de l’élève', 2, GETDATE(), null, null)

insert store.EmailFormTopic values (38, 'Question ou commentaire d’ordre général', 'Question ou commentaire d’ordre général', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (38, 'Question sur le financement avec QSP', 'Question sur le financement avec QSP', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (38, 'Question sur les magazines', 'Question sur les magazines', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (38, 'Aide avec le magasin en ligne', 'Aide avec le magasin en ligne', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (38, 'Question sur une commande', 'Question sur une commande', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (38, 'Renseignements sur les produits', 'Renseignements sur les produits', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (38, 'Question sur l’identifiant de l’organisation', 'Question sur l’identifiant de l’organisation', 2, GETDATE(), null, null)

insert store.EmailFormTopic values (39, 'Question ou commentaire d’ordre général', 'Question ou commentaire d’ordre général', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (39, 'Question sur le financement avec EFR', 'Question sur le financement avec EFR', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (39, 'Question sur les magazines', 'Question sur les magazines', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (39, 'Aide avec le magasin en ligne', 'Aide avec le magasin en ligne', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (39, 'Question sur une commande', 'Question sur une commande', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (39, 'Renseignements sur les produits', 'Renseignements sur les produits', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (39, 'Question sur l’identifiant de l’organisation', 'Question sur l’identifiant de l’organisation', 2, GETDATE(), null, null)

--prod
commit tran
insert store.EmailFormTopic values (37, 'Question ou commentaire d’ordre général', 'Question ou commentaire d’ordre général', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (37, 'Question sur le financement avec QSP', 'Question sur le financement avec QSP', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (37, 'Question sur les magazines', 'Question sur les magazines', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (37, 'Aide avec le magasin en ligne', 'Aide avec le magasin en ligne', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (37, 'Question sur une commande', 'Question sur une commande', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (37, 'Renseignements sur les produits', 'Renseignements sur les produits', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (37, 'Question sur l’id. de l’élève/l’organisation', 'Question sur l’id. de l’élève/l’organisation', 2, GETDATE(), null, null)

insert store.EmailFormTopic values (38, 'Question d’ordre général', 'Question d’ordre général', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (38, 'Question sur les rapports', 'Question sur les rapports', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (38, 'Question sur le téléchargement d’image', 'Question sur le téléchargement d’image', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (38, 'Question sur le téléchargement de document', 'Question sur le téléchargement de document', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (38, 'Question sur la connexion/les détails du compte', 'Question sur la connexion/les détails du compte', 2, GETDATE(), null, null)

insert store.EmailFormTopic values (39, 'Question d’ordre général', 'Question d’ordre général', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (39, 'Question sur les rapports', 'Question sur les rapports', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (39, 'Question sur le téléchargement d’image', 'Question sur le téléchargement d’image', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (39, 'Question sur la connexion/les détails du compte', 'Question sur la connexion/les détails du compte', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (39, 'Question sur l’inscription', 'Question sur l’inscription', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (39, 'Problème avec le courriel de l’élève', 'Problème avec le courriel de l’élève', 2, GETDATE(), null, null)

insert store.EmailFormTopic values (40, 'Question ou commentaire d’ordre général', 'Question ou commentaire d’ordre général', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (40, 'Question sur le financement avec QSP', 'Question sur le financement avec QSP', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (40, 'Question sur les magazines', 'Question sur les magazines', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (40, 'Aide avec le magasin en ligne', 'Aide avec le magasin en ligne', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (40, 'Question sur une commande', 'Question sur une commande', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (40, 'Renseignements sur les produits', 'Renseignements sur les produits', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (40, 'Question sur l’identifiant de l’organisation', 'Question sur l’identifiant de l’organisation', 2, GETDATE(), null, null)

insert store.EmailFormTopic values (41, 'Question ou commentaire d’ordre général', 'Question ou commentaire d’ordre général', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (41, 'Question sur le financement avec EFR', 'Question sur le financement avec EFR', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (41, 'Question sur les magazines', 'Question sur les magazines', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (41, 'Aide avec le magasin en ligne', 'Aide avec le magasin en ligne', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (41, 'Question sur une commande', 'Question sur une commande', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (41, 'Renseignements sur les produits', 'Renseignements sur les produits', 2, GETDATE(), null, null)
insert store.EmailFormTopic values (41, 'Question sur l’identifiant de l’organisation', 'Question sur l’identifiant de l’organisation', 2, GETDATE(), null, null)
