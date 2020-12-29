using System;
using System.Collections;

namespace GA.BDC.Core.ESubsGlobal.Reports
{
	/// <summary>
	/// Summary description for FeaturedGroupCollection.
	/// </summary>
	public class FeaturedGroupCollection
	{
		private ArrayList featuredGroupArrayList = new ArrayList();
		
        public FeaturedGroupCollection()
		{

		}

		public ArrayList GetRandomFeaturedGroups(int numberOfGroups) {
			ArrayList ids = new ArrayList(numberOfGroups);

			if(featuredGroupArrayList.Count < numberOfGroups) {
				numberOfGroups = featuredGroupArrayList.Count;
			}

			while(ids.Count < numberOfGroups) {
				Random random = new Random();
				int index = random.Next(featuredGroupArrayList.Count);
				if(!ids.Contains(index)) {
					ids.Add(index);
				}
			}

			ArrayList newGroups = new ArrayList(ids.Count);
			for(int i=0;i<ids.Count;i++) {
				newGroups.Add(featuredGroupArrayList[(int)ids[i]]);
			}

			return newGroups;
		}


		public ArrayList FeaturedGroups {
			get { return featuredGroupArrayList; }
			set { featuredGroupArrayList = value; }
		}

		public ArrayList GetFeaturedGroupArayList() {
			return featuredGroupArrayList;
		}

		public void AddFeaturedGroup(string s1, string s2, string s3, string s4, string s5, decimal d6) {
			featuredGroupArrayList.Add(new FeaturedGroup(s1,s2,s3,s4,s5,d6));
		}

	}
}
