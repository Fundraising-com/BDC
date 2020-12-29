using System;
using System.Collections.Generic;
using System.Text;

namespace GA.BDC.Core.eFundraisingStore
{
    public class Choice : eFundraisingStoreDataObject
    {


        private int choiceId;
        private String choiceDesc;
        private String location;
        private String image;

        public Choice() : this(int.MinValue) { }
        public Choice(int choiceId) : this(choiceId, null) { }
        public Choice(int choiceId, String choiceDesc): this(choiceId, choiceDesc ,null) { }
        public Choice(int choiceId, String choiceDesc, String location): this(choiceId, choiceDesc ,location,null){}
        public Choice(int choiceId, String choiceDesc, String location, String image)    
        {
            this.choiceId = choiceId;
            this.choiceDesc = choiceDesc;
            this.location = location;
            this.image = image;
        }

        #region Properties
        public int ChoiceId
        {
            set { choiceId = value; }
            get { return choiceId; }
        }

        public string ChoiceDesc
        {
            set { choiceDesc = value; }
            get { return choiceDesc; }
        }

        public string Location
        {
            set { location = value; }
            get { return location; }
        }

        public string Image
        {
            set { image = value; }
            get { return image; }
        }
        #endregion

        #region Data Source Methods
        public static Choice GetChoiceByChoiceID(int id)
        {
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.GetChoiceByChoiceID(id);
        }

        public static Choice[] GetChoices()
        {
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.GetChoices();
        }

        #endregion


    }
}
