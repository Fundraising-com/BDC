using System;

namespace GA.BDC.Core.Flagpole.QSP
{
	/// <summary>
	/// Summary description for QspFlagPoleFactory.
	/// </summary>
	/// 


	public enum QspFlagPoleType: int
	{
		ESubGlobalWeb =0		
	}



	public class QspFlagPoleFactory
	{

		private static QspFlagPoleFactory instance = new QspFlagPoleFactory();

		private QspFlagPoleFactory()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		
		public static IQspFlagPole CreateMagazine(QspFlagPoleType qspType)
		{
			switch (qspType)
			{
				case QspFlagPoleType.ESubGlobalWeb:
					return new QspFlagPoleESubGlobal();
				default:
				break;
			}
			return null;
		}

	}




}
