using System;
using System.Collections.Generic;
using System.Text;

namespace GA.BDC.Core.eFundraisingStore
{
    public class Survey : eFundraisingStoreDataObject
    {

        private int surveyId;
        private string pageName;
        private int display;

        public Survey() : this(int.MinValue) { }
        public Survey(int surveyId) : this(surveyId, null, int.MinValue) { }
        public Survey(int surveyId, string pageName) : this(surveyId, pageName, int.MinValue) { }
        public Survey(int surveyId, string pageName, int display){
            this.surveyId = surveyId;
            this.pageName = pageName;
            this.display = display;
        }


        #region Properties
        public int SurveyId
        {
            set { surveyId = value; }
            get { return surveyId; }
        }

        public string PageName
        {
            set { pageName = value; }
            get { return pageName; }
        }

        public int Display
        {
            set { display = value; }
            get { return display; }
        }
        #endregion

        #region Data Source Methods
        public static Survey GetSurveyBySurveyID(int id)
        {
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.GetSurveyBySurveyID(id);
        }

        public static Survey GetSurveyByPageName(string pageName)
        {
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.GetSurveyByPageName(pageName);
        }

       #endregion



    }
}
