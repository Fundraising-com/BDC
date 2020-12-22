using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Transactions;

using QSP.OrderExpress.Business.Context;
using QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Business.Validation;
using QSP.OrderExpress.Common.Comunication;
using QSP.OrderExpress.Common.Data;
using QSP.OrderExpress.Common.Enum;
using QSP.OrderExpress.Common.Search;

namespace QSP.OrderExpress.Business
{
    public class DocumentSystem
    {
        #region Version 2 code

        public List<DocumentSearchItem> Search(DocumentSearchParameters parameters)
        {
            List<DocumentSearchItem> result = new List<DocumentSearchItem>();

            OrderExpressDataContext db = new OrderExpressDataContext();

            #region Base query

            var query = from da in db.DocumentAccounts
                        where da.IsDeleted == false
                            && da.Document.IsDeleted == false
                            && (da.Account.Organization.BusinessDivisionId == (int)BusinessDivisionEnum.US
                                || da.Account.Organization.BusinessDivisionId == (int)BusinessDivisionEnum.EFR)
                        select new
                        {
                            QSPDocumentId = da.Document.DocumentId,
                            Name = da.Document.DocumentName,
                            DocumentType = (DocumentTypeEnum)(da.Document.DocumentTypeId ?? 1),
                            DocumentTypeName = da.Document.DocumentType.DocumentTypeName,
                            ReceivedDate = da.Document.ReceivedDate,
                            IsApproved = da.Document.IsApproved ?? false,
                            ApprovedDate = da.Document.ApprovedDate,
                            ApprovedById = da.Document.ApprovedUserId,
                            QSPAccountId = da.AccountId,
                            EDSAccountId = da.Account.FulfAccountId,
                            AccountName = da.Account.AccountName,
                            IsDeleted = da.Document.IsDeleted,  
                            CreateDate = da.Document.CreateDate,
                            CreateUserId = da.Document.CreateUserId,
                            UpdateDate = da.Document.UpdateDate,
                            UpdateUserId = da.Document.UpdateUserId, 
                            ExemptionNumber = da.Document.ExemptionNumber, 
                            ExemptionStartDate = da.Document.ExemptionStartDate, 
                            ExemptionEndDate = da.Document.ExemptionEndDate
                        };

            #endregion

            #region Filters

            if (parameters.SearchValue.Length > 0)
            {
                if (parameters.SearchField == DocumentSearchFieldEnum.Any)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.QSPDocumentId == number
                                || q.AccountName.Contains(parameters.SearchValue)
                                || q.QSPAccountId == number
                                || q.EDSAccountId == number
                                select q;
                    }
                    else
                    {
                        query = from q in query
                                where q.AccountName.Contains(parameters.SearchValue)
                                select q;
                    }
                }
                else if (parameters.SearchField == DocumentSearchFieldEnum.QSPDocumentId)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.QSPDocumentId == number
                                select q;
                    }
                }
                else if (parameters.SearchField == DocumentSearchFieldEnum.AccountName)
                {
                    query = from q in query
                            where q.AccountName.Contains(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == DocumentSearchFieldEnum.QSPAccountId)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.QSPAccountId == number
                                select q;
                    }
                }
                else if (parameters.SearchField == DocumentSearchFieldEnum.EDSAccountId)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.EDSAccountId == number
                                select q;
                    }
                }
                else if (parameters.SearchField == DocumentSearchFieldEnum.AccountNameBeginingWith)
                {
                    query = from q in query
                            where q.AccountName.StartsWith(parameters.SearchValue)
                            select q;
                }
            }

            if (parameters.DocumentStatusId.HasValue)
            {
                if (parameters.DocumentStatusId.Value == 0)
                {
                    query = from q in query
                            where q.IsApproved == false
                            select q;
                }
                else if (parameters.DocumentStatusId.Value == 1)
                {
                    query = from q in query
                            where q.IsApproved == true
                            select q;
                }
            }

            if (parameters.DocumentTypeId.HasValue)
            {
                query = from q in query
                        where q.DocumentType == (DocumentTypeEnum)parameters.DocumentTypeId
                        select q;
            }

            #endregion

            #region Left join to get approved by user

            var finalQuery = from q in query
                             join user in db.Users on q.ApprovedById equals user.UserId into temp
                             from userData in temp.DefaultIfEmpty()
                             select new
                             {
                                 QSPDocumentId = q.QSPDocumentId,
                                 Name = q.Name,
                                 DocumentType = q.DocumentType,
                                 DocumentTypeName = q.DocumentTypeName,
                                 ReceivedDate = q.ReceivedDate,
                                 IsApproved = q.IsApproved,
                                 ApprovedDate = q.ApprovedDate,
                                 ApprovedById = q.ApprovedById,
                                 QSPAccountId = q.QSPAccountId,
                                 EDSAccountId = q.EDSAccountId,
                                 AccountName = q.AccountName,
                                 IsDeleted = q.IsDeleted,
                                 CreateDate = q.CreateDate,
                                 CreateUserId = q.CreateUserId,
                                 UpdateDate = q.UpdateDate,
                                 UpdateUserId = q.UpdateUserId,
                                 ApprovedByFirstName = (userData.FirstName == null) ? "" : userData.FirstName,
                                 ApprovedByLastName = (userData.LastName == null) ? "" : userData.LastName,
                                 ExemptionNumber = q.ExemptionNumber,
                                 ExemptionStartDate = q.ExemptionStartDate,
                                 ExemptionEndDate = q.ExemptionEndDate
                             };

            #endregion

            #region Sort

            finalQuery = finalQuery.OrderBy(parameters.SortField);

            #endregion`

            //using (TransactionScope t = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            //{
                if (parameters.IsPagingEnabled)
                {
                    QSPForm.Business.Properties.Settings settings = new QSPForm.Business.Properties.Settings();

                    if (settings.UseDatabasePaging)
                    {
                        #region Paging

                        finalQuery = (
                            from q in finalQuery
                            select q
                            ).Skip((parameters.RequestedPage - 1) * parameters.ItemsPerPage).Take(parameters.ItemsPerPage);

                        #endregion

                        #region Get final result

                        result = (
                            from q in finalQuery
                            select new DocumentSearchItem
                            {
                                QSPDocumentId = q.QSPDocumentId,
                                Name = q.Name,
                                Type = q.DocumentType,
                                TypeName = q.DocumentTypeName,
                                ReceivedDate = q.ReceivedDate,
                                IsApproved = q.IsApproved,
                                ApprovedDate = q.ApprovedDate,
                                ApprovedById = q.ApprovedById,
                                ApprovedByFirstName = q.ApprovedByFirstName,
                                ApprovedByLastName = q.ApprovedByLastName,
                                QSPAccountId = q.QSPAccountId,
                                EDSAccountId = q.EDSAccountId,
                                AccountName = q.AccountName,
                                IsDeleted = q.IsDeleted,
                                CreateDate = q.CreateDate,
                                CreateUserId = q.CreateUserId,
                                UpdateDate = q.UpdateDate,
                                UpdateUserId = q.UpdateUserId,
                                ExemptionNumber = q.ExemptionNumber,
                                ExemptionStartDate = q.ExemptionStartDate,
                                ExemptionEndDate = q.ExemptionEndDate
                            }
                            ).ToList();

                        #endregion
                    }
                    else
                    {
                        #region Paging and final result

                        finalQuery = finalQuery.Take((parameters.RequestedPage + 1) * parameters.ItemsPerPage);

                        var temp = finalQuery.AsEnumerable().Skip(
                            (parameters.RequestedPage - 1) * parameters.ItemsPerPage).Select(q =>
                                new DocumentSearchItem
                                {
                                    QSPDocumentId = q.QSPDocumentId,
                                    Name = q.Name,
                                    Type = q.DocumentType,
                                    TypeName = q.DocumentTypeName,
                                    ReceivedDate = q.ReceivedDate,
                                    IsApproved = q.IsApproved,
                                    ApprovedDate = q.ApprovedDate,
                                    ApprovedById = q.ApprovedById,
                                    ApprovedByFirstName = q.ApprovedByFirstName,
                                    ApprovedByLastName = q.ApprovedByLastName,
                                    QSPAccountId = q.QSPAccountId,
                                    EDSAccountId = q.EDSAccountId,
                                    AccountName = q.AccountName,
                                    IsDeleted = q.IsDeleted,
                                    CreateDate = q.CreateDate,
                                    CreateUserId = q.CreateUserId,
                                    UpdateDate = q.UpdateDate,
                                    UpdateUserId = q.UpdateUserId,
                                    ExemptionNumber = q.ExemptionNumber,
                                    ExemptionStartDate = q.ExemptionStartDate,
                                    ExemptionEndDate = q.ExemptionEndDate
                                });

                        result = temp.ToList();

                        #endregion
                    }
                }
                else
                {
                    #region Get final result

                    result = (
                        from q in finalQuery
                        select new DocumentSearchItem
                        {
                            QSPDocumentId = q.QSPDocumentId,
                            Name = q.Name,
                            Type = q.DocumentType,
                            TypeName = q.DocumentTypeName,
                            ReceivedDate = q.ReceivedDate,
                            IsApproved = q.IsApproved,
                            ApprovedDate = q.ApprovedDate,
                            ApprovedById = q.ApprovedById,
                            ApprovedByFirstName = q.ApprovedByFirstName,
                            ApprovedByLastName = q.ApprovedByLastName,
                            QSPAccountId = q.QSPAccountId,
                            EDSAccountId = q.EDSAccountId,
                            AccountName = q.AccountName,
                            IsDeleted = q.IsDeleted,
                            CreateDate = q.CreateDate,
                            CreateUserId = q.CreateUserId,
                            UpdateDate = q.UpdateDate,
                            UpdateUserId = q.UpdateUserId,
                            ExemptionNumber = q.ExemptionNumber,
                            ExemptionStartDate = q.ExemptionStartDate,
                            ExemptionEndDate = q.ExemptionEndDate
                        }
                        ).ToList();

                    #endregion
                }
            //}
            return result;
        }
        public int SearchTotalRowCount(DocumentSearchParameters parameters)
        {
            int result = 0;

            OrderExpressDataContext db = new OrderExpressDataContext();

            #region Base query

            var query = from da in db.DocumentAccounts
                        where da.IsDeleted == false
                            && da.Document.IsDeleted == false
                            && (da.Account.Organization.BusinessDivisionId == (int)BusinessDivisionEnum.US
                                || da.Account.Organization.BusinessDivisionId == (int)BusinessDivisionEnum.EFR)
                        select new
                        {
                            QSPDocumentId = da.Document.DocumentId,
                            Name = da.Document.DocumentName,
                            DocumentType = (DocumentTypeEnum)(da.Document.DocumentTypeId ?? 1),
                            DocumentTypeName = da.Document.DocumentType.DocumentTypeName,
                            ReceivedDate = da.Document.ReceivedDate,
                            IsApproved = da.Document.IsApproved ?? false,
                            ApprovedDate = da.Document.ApprovedDate,
                            ApprovedById = da.Document.ApprovedUserId,
                            QSPAccountId = da.AccountId,
                            EDSAccountId = da.Account.FulfAccountId,
                            AccountName = da.Account.AccountName,
                            IsDeleted = da.Document.IsDeleted,
                            CreateDate = da.Document.CreateDate,
                            CreateUserId = da.Document.CreateUserId,
                            UpdateDate = da.Document.UpdateDate,
                            UpdateUserId = da.Document.UpdateUserId,
                            ExemptionNumber = da.Document.ExemptionNumber,
                            ExemptionStartDate = da.Document.ExemptionStartDate,
                            ExemptionEndDate = da.Document.ExemptionEndDate
                        };

            #endregion

            #region Filters

            if (parameters.SearchValue.Length > 0)
            {
                if (parameters.SearchField == DocumentSearchFieldEnum.Any)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.QSPDocumentId == number
                                || q.AccountName.Contains(parameters.SearchValue)
                                || q.QSPAccountId == number
                                || q.EDSAccountId == number
                                select q;
                    }
                    else
                    {
                        query = from q in query
                                where q.AccountName.Contains(parameters.SearchValue)
                                select q;
                    }
                }
                else if (parameters.SearchField == DocumentSearchFieldEnum.QSPDocumentId)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.QSPDocumentId == number
                                select q;
                    }
                }
                else if (parameters.SearchField == DocumentSearchFieldEnum.AccountName)
                {
                    query = from q in query
                            where q.AccountName.Contains(parameters.SearchValue)
                            select q;
                }
                else if (parameters.SearchField == DocumentSearchFieldEnum.QSPAccountId)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.QSPAccountId == number
                                select q;
                    }
                }
                else if (parameters.SearchField == DocumentSearchFieldEnum.EDSAccountId)
                {
                    int number = 0;
                    bool isNumber = int.TryParse(parameters.SearchValue, out number);

                    if (isNumber)
                    {
                        query = from q in query
                                where q.EDSAccountId == number
                                select q;
                    }
                }
                else if (parameters.SearchField == DocumentSearchFieldEnum.AccountNameBeginingWith)
                {
                    query = from q in query
                            where q.AccountName.StartsWith(parameters.SearchValue)
                            select q;
                }
            }

            if (parameters.DocumentStatusId.HasValue)
            {
                if (parameters.DocumentStatusId.Value == 0)
                {
                    query = from q in query
                            where q.IsApproved == false
                            select q;
                }
                else if (parameters.DocumentStatusId.Value == 1)
                {
                    query = from q in query
                            where q.IsApproved == true
                            select q;
                }
            }

            if (parameters.DocumentTypeId.HasValue)
            {
                query = from q in query
                        where q.DocumentType == (DocumentTypeEnum)parameters.DocumentTypeId
                        select q;
            }

            #endregion

            #region Get final count

            //using (TransactionScope t = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            //{
                result = query.Count();
            //}

            #endregion

            return result;
        }

        public DocumentData GetDocument(int documentId)
        {
            return this.GetDocument(documentId, false);
        }
        public DocumentData GetDocument(int documentId, bool loadChildrenObjects)
        {
            DocumentData result = new DocumentData();

            OrderExpressDataContext db = new OrderExpressDataContext();

            #region Load document data

            Document document =
                (from d in db.Documents
                 where d.DocumentId == documentId
                 select d
                 ).SingleOrDefault();

            result.Id = document.DocumentId;
            result.Name = document.DocumentName;
            result.IsApproved = document.IsApproved ?? false; 
            result.ApprovedById = document.ApprovedUserId;
            result.ApprovedDate = document.ApprovedDate;
            result.ReceivedDate = document.ReceivedDate;
            result.ExemptionNumber = document.ExemptionNumber;
            result.ExemptionStartDate = document.ExemptionStartDate;
            result.ExemptionEndDate = document.ExemptionEndDate;

            result.IsDeleted = document.IsDeleted;
            result.CreateDate = document.CreateDate;
            result.CreateUserId = document.CreateUserId;
            result.UpdateDate = document.UpdateDate;
            result.UpdateUserId = document.UpdateUserId;

            result.Type = new DocumentTypeData();
            result.Type.Id = document.DocumentTypeId;

            #endregion

            if (loadChildrenObjects)
            {
                #region Load extended document data

                if (document.ApprovedUserId.HasValue)
                {
                    User user =
                        (from u in db.Users
                         where u.UserId == document.ApprovedUserId.Value
                         select u
                         ).FirstOrDefault();

                    if (user != null)
                    {
                        result.ApprovedByFirstName = user.FirstName;
                        result.ApprovedByLastName = user.LastName;
                    }
                }

                #endregion

                #region Load account id

                List<DocumentAccount> documentAccountList =
                    (from da in document.DocumentAccounts
                     where da.IsDeleted == false
                     select da
                     ).ToList();

                if (documentAccountList.Count > 0)
                {
                    result.AccountId = documentAccountList[0].AccountId;
                }

                #endregion

                #region Load document type

                if (document.DocumentTypeId.HasValue)
                {
                    DocumentType documentType =
                        (from dt in db.DocumentTypes
                         where dt.DocumentTypeId == document.DocumentTypeId.Value
                         select dt
                         ).FirstOrDefault();

                    if (documentType != null)
                    {
                        result.Type.Name = documentType.DocumentTypeName;
                    }
                }

                #endregion
            }

            return result;
        }
        public MethodResult SaveDocument(DocumentData document)
        {
            MethodResult result = new MethodResult();

            DocumentValidation documentValidation = new DocumentValidation();
            MethodResult validationResult = documentValidation.ValidateDocument(document);

            if (validationResult.IsSuccessful)
            {
                OrderExpressDataContext db = new OrderExpressDataContext();
                db.Connection.Open();
                db.Transaction = db.Connection.BeginTransaction();

                try
                {
                    #region Save document data

                    Document existingDocument =
                        (from d in db.Documents
                         where d.DocumentId == document.Id
                         select d
                         ).SingleOrDefault();

                    if (existingDocument.IsApproved == false && document.IsApproved == true)
                    {
                        // We just approved
                        existingDocument.ApprovedUserId = document.ApprovedById;
                        existingDocument.ApprovedDate = DateTime.Now;
                    }

                    existingDocument.IsApproved = document.IsApproved;
                    existingDocument.ExemptionNumber = document.ExemptionNumber;
                    existingDocument.ExemptionStartDate = document.ExemptionStartDate;
                    existingDocument.ExemptionEndDate = document.ExemptionEndDate;
                    existingDocument.UpdateUserId = document.UpdateUserId;
                    existingDocument.UpdateDate = DateTime.Now;

                    db.SubmitChanges();

                    #endregion

                    #region Check results and commit transaction

                    result.Merge(validationResult, "Validation");

                    if (result.IsSuccessful)
                    {
                        db.Transaction.Commit();
                    }
                    else
                    {
                        db.Transaction.Rollback();
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    MethodResultNotification notification = new MethodResultNotification();

                    notification.Message = ex.Message;
                    notification.NotificationType = MethodResultNotificationTypeEnum.Error;
                    notification.DynamicValues = new Dictionary<string, object>();
                    notification.DynamicValues.Add("Exception", ex);

                    result.ResultNotifications.Add(notification);
                }
                finally
                {
                    db.Connection.Close();
                }
            }
            else
            {
                result = validationResult;
            }

            return result;
        }

        public List<DocumentType> GetDocumentTypes()
        {
            List<DocumentType> result = new List<DocumentType>();

            OrderExpressDataContext db = new OrderExpressDataContext();

            result = (from dt in db.DocumentTypes
                      select dt).ToList();

            return result;
        }
        public Dictionary<int, string> GetDocumentStatuses()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            result.Add(0, "Not approved");
            result.Add(1, "Approved");

            return result;
        }

        #endregion
    }
}
