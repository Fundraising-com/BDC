using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Business.ReportExecution;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.CommonWeb;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for GenerateOnlineStatements.
	/// </summary>
	public class GenerateOnlineStatements : CustomerService.CustomerServicePage
	{
		protected System.Web.UI.WebControls.Button btnGenerate;
		private DataTable Table = new DataTable("CampaignTable");

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
		}
		#endregion

		private void btnGenerate_Click(object sender, System.EventArgs e)
		{
			GenerateOnlineProgramProfitStatements();
		}

		private void GenerateOnlineProgramProfitStatements() 
		{
			byte[] results;
			FileStream fs;

			LoadData();

			foreach(DataRow row in Table.Rows) 
			{
                if (!File.Exists("D:\\Statements\\" + row["FMID"].ToString() + "\\" + row["FMID"].ToString() + "_" + row["Account_ID"].ToString().PadLeft(6, '0') + "_" + row["Campaign_ID"].ToString().PadLeft(7, '0') + "_2.pdf"))
                {
                    results = CallReportServicesDirect(row["Campaign_ID"].ToString());

				    if(!Directory.Exists("D:\\Statements\\" + row["FMID"].ToString())) 
				    {
					    Directory.CreateDirectory("D:\\Statements\\" + row["FMID"].ToString());
				    }

				    fs = new FileStream("D:\\Statements\\" + row["FMID"].ToString() + "\\" + row["FMID"].ToString() + "_" + row["Account_ID"].ToString().PadLeft(6, '0') + "_" + row["Campaign_ID"].ToString().PadLeft(7, '0') + "_2.pdf", FileMode.Create);
				    fs.Write(results, 0, results.Length);
				    fs.Close();
                }
			}
		}

		private void LoadData() 
		{
			int adjustmentBatchID = 4;

			this.BusSearch.SelectCampaignsForStatementsByAdjustmentBatchID(Table, adjustmentBatchID);

            RemoveAccounts(Table);
            RemoveFM(Table);
		}

		private byte[] CallReportServicesDirect(string CampaignID)
		{
			IAsyncResult asyncResult;
			byte[] result = null;

			string RSInstallation = System.Configuration.ConfigurationSettings.AppSettings["RSInstallation"];
			string sRSUsername    = System.Configuration.ConfigurationSettings.AppSettings["RSPowerUsername"];
			string sRSPassword    = System.Configuration.ConfigurationSettings.AppSettings["RSPowerPassword"];
			RSClient oRS = new RSClient();
			try 
			{
				oRS.LogonUser(sRSUsername, sRSPassword, null);
			} 
			catch 
			{
				oRS.LogonUser(sRSUsername, sRSPassword, null);
			}
			

			string encoding;
			string mimetype;
			QSPFulfillment.RDReportServer.ParameterValue[] inputParams = new QSPFulfillment.RDReportServer.ParameterValue[4];
			inputParams[0] = new QSPFulfillment.RDReportServer.ParameterValue();
			inputParams[0].Name = "CampaignID";
			inputParams[0].Value = CampaignID;
			inputParams[1] = new QSPFulfillment.RDReportServer.ParameterValue();
			inputParams[1].Name = "Over100";
			inputParams[1].Value = "0";
			inputParams[2] = new QSPFulfillment.RDReportServer.ParameterValue();
			inputParams[2].Name = "DateFrom";
			inputParams[2].Value = new DateTime(2009, 07, 01).ToString();
			inputParams[3] = new QSPFulfillment.RDReportServer.ParameterValue();
			inputParams[3].Name = "DateTo";
			inputParams[3].Value = new DateTime(2009, 11, 20).ToString();

			QSPFulfillment.RDReportServer.ParameterValue[] parametersUsed;
			Warning[] warnings;
			string[] streamids;

			try 
			{
				asyncResult = oRS.BeginRender(
					"OnlineProgramProfitStatement"
//					"/QSP CA SystemsOnlineProgramProfitStatement"
					, "PDF"
					, null
					, null
					, inputParams
					, null
					, null
					, null
					, null);

				if(asyncResult.AsyncWaitHandle.WaitOne(-1, false)) 
				{
					result = oRS.EndRender(
						asyncResult
						, out encoding
						, out mimetype
						, out parametersUsed
						, out warnings
						, out streamids);
				} 
				else 
				{
					asyncResult = oRS.BeginRender(
						"OnlineProgramProfitStatement"
//						"/QSP CA SystemsOnlineProgramProfitStatement"
						, "PDF"
						, null
						, null
						, inputParams
						, null
						, null
						, null
						, null);

					if(asyncResult.AsyncWaitHandle.WaitOne(-1, false)) 
					{
						result = oRS.EndRender(
							asyncResult
							, out encoding
							, out mimetype
							, out parametersUsed
							, out warnings
							, out streamids);
					}
				}
			}
			catch 
			{
				asyncResult = oRS.BeginRender(
					"OnlineProgramProfitStatement"
//					"/QSP CA SystemsOnlineProgramProfitStatement"
					, "PDF"
					, null
					, null
					, inputParams
					, null
					, null
					, null
					, null);

				if(asyncResult.AsyncWaitHandle.WaitOne(-1, false)) 
				{
					result = oRS.EndRender(
						asyncResult
						, out encoding
						, out mimetype
						, out parametersUsed
						, out warnings
						, out streamids);
				} 
				else 
				{
					asyncResult = oRS.BeginRender(
						"OnlineProgramProfitStatement"
//						"/QSP CA SystemsOnlineProgramProfitStatement"
						, "PDF"
						, null
						, null
						, inputParams
						, null
						, null
						, null
						, null);

					if(asyncResult.AsyncWaitHandle.WaitOne(-1, false)) 
					{
						result = oRS.EndRender(
							asyncResult
							, out encoding
							, out mimetype
							, out parametersUsed
							, out warnings
							, out streamids);
					}
				}
			}

			return result;
		}

        private void RemoveAccounts(DataTable table)
        {
            List<int> list = new List<int>();

            list.Add(60232);
            list.Add(62247);
            list.Add(62147);
            list.Add(62665);
            list.Add(62765);
            list.Add(60893);
            list.Add(66595);
            list.Add(65098);
            list.Add(64812);
            list.Add(62104);
            list.Add(61354);
            list.Add(62926);
            list.Add(64898);
            list.Add(63687);
            list.Add(65087);
            list.Add(66584);
            list.Add(66541);
            list.Add(64437);
            list.Add(65473);
            list.Add(63040);
            list.Add(64855);
            list.Add(61297);
            list.Add(60518);
            list.Add(64798);
            list.Add(64019);
            list.Add(65991);
            list.Add(60836);
            list.Add(64237);
            list.Add(64912);
            list.Add(66209);
            list.Add(62204);
            list.Add(63776);
            list.Add(66123);
            list.Add(64998);
            list.Add(62047);
            list.Add(64305);
            list.Add(65273);
            list.Add(66484);
            list.Add(65230);
            list.Add(63140);
            list.Add(64930);
            list.Add(62915);
            list.Add(65391);
            list.Add(66227);
            list.Add(60193);
            list.Add(62308);
            list.Add(60293);
            list.Add(64887);
            list.Add(64655);
            list.Add(65248);
            list.Add(62640);
            list.Add(64730);
            list.Add(63115);
            list.Add(62958);
            list.Add(61143);
            list.Add(65216);
            list.Add(64773);
            list.Add(63619);
            list.Add(65680);
            list.Add(65105);
            list.Add(60207);
            list.Add(66152);
            list.Add(62265);
            list.Add(63344);
            list.Add(64787);
            list.Add(63751);
            list.Add(60811);
            list.Add(64873);
            list.Add(66041);
            list.Add(64830);
            list.Add(66241);
            list.Add(61161);
            list.Add(64101);
            list.Add(66216);
            list.Add(64212);
            list.Add(65191);
            list.Add(66534);
            list.Add(50790);
            list.Add(66577);
            list.Add(59664);
            list.Add(61722);
            list.Add(65741);
            list.Add(62154);
            list.Add(61997);
            list.Add(66016);
            list.Add(65898);
            list.Add(62801);
            list.Add(64330);
            list.Add(64387);
            list.Add(60311);
            list.Add(64905);
            list.Add(65523);
            list.Add(65984);
            list.Add(64848);
            list.Add(64805);
            list.Add(65423);
            list.Add(65223);
            list.Add(61536);
            list.Add(65366);
            list.Add(61965);
            list.Add(66591);
            list.Add(64862);
            list.Add(63133);
            list.Add(65641);
            list.Add(61736);
            list.Add(63219);
            list.Add(66127);
            list.Add(65798);
            list.Add(60082);
            list.Add(64630);
            list.Add(66159);
            list.Add(64026);
            list.Add(64430);
            list.Add(66477);
            list.Add(63326);
            list.Add(66177);
            list.Add(64780);
            list.Add(61740);
            list.Add(64419);
            list.Add(64880);
            list.Add(61522);
            list.Add(64823);
            list.Add(62908);
            list.Add(64923);
            list.Add(64319);
            list.Add(66059);
            list.Add(63240);
            list.Add(65255);
            list.Add(60386);
            list.Add(62315);
            list.Add(60300);
            list.Add(63887);
            list.Add(65573);
            list.Add(64694);
            list.Add(62733);
            list.Add(65673);
            list.Add(65684);
            list.Add(62833);
            list.Add(61179);
            list.Add(61897);
            list.Add(64837);
            list.Add(66234);
            list.Add(63701);
            list.Add(62129);
            list.Add(64219);
            list.Add(66566);
            list.Add(64187);
            list.Add(65398);
            list.Add(60243);
            list.Add(62258);
            list.Add(60400);
            list.Add(65759);
            list.Add(65951);
            list.Add(64815);
            list.Add(63318);
            list.Add(64354);
            list.Add(62539);
            list.Add(65256);
            list.Add(66558);
            list.Add(65817);
            list.Add(64915);
            list.Add(62516);
            list.Add(65413);
            list.Add(62943);
            list.Add(66192);
            list.Add(64311);
            list.Add(62582);
            list.Add(65090);
            list.Add(60567);
            list.Add(62405);
            list.Add(61437);
            list.Add(65851);
            list.Add(64377);
            list.Add(66149);
            list.Add(64786);
            list.Add(64245);
            list.Add(61646);
            list.Add(64686);
            list.Add(62814);
            list.Add(64881);
            list.Add(60976);
            list.Add(65983);
            list.Add(66524);
            list.Add(62239);
            list.Add(66226);
            list.Add(65751);
            list.Add(65081);
            list.Add(66492);
            list.Add(62505);
            list.Add(63107);
            list.Add(65617);
            list.Add(60067);
            list.Add(64243);
            list.Add(66224);
            list.Add(66519);
            list.Add(63141);
            list.Add(64286);
            list.Add(65958);
            list.Add(62116);
            list.Add(64086);
            list.Add(63745);
            list.Add(65056);
            list.Add(64856);
            list.Add(62248);
            list.Add(64252);
            list.Add(64847);
            list.Add(60367);
            list.Add(65749);
            list.Add(64781);
            list.Add(66553);
            list.Add(61037);
            list.Add(64581);
            list.Add(65949);
            list.Add(64790);
            list.Add(64813);
            list.Add(65792);
            list.Add(64452);
            list.Add(63811);
            list.Add(60576);
            list.Add(64890);
            list.Add(61973);
            list.Add(64913);
            list.Add(62414);
            list.Add(66115);
            list.Add(60333);
            list.Add(63877);
            list.Add(62148);
            list.Add(65179);
            list.Add(66049);
            list.Add(65383);
            list.Add(66024);
            list.Add(64888);
            list.Add(64788);
            list.Add(65924);
            list.Add(64988);
            list.Add(60912);
            list.Add(65088);
            list.Add(61971);
            list.Add(65858);
            list.Add(62014);
            list.Add(60903);
            list.Add(64413);
            list.Add(62684);
            list.Add(60076);
            list.Add(65890);
            list.Add(64118);
            list.Add(64879);
            list.Add(61135);
            list.Add(64922);
            list.Add(66494);
            list.Add(62707);
            list.Add(63082);
            list.Add(62180);
            list.Add(62541);
            list.Add(64656);
            list.Add(65254);
            list.Add(60242);
            list.Add(65615);
            list.Add(66190);
            list.Add(65054);
            list.Add(60408);
            list.Add(64547);
            list.Add(66528);
            list.Add(61937);
            list.Add(63720);
            list.Add(62784);
            list.Add(64845);
            list.Add(64779);
            list.Add(61078);
            list.Add(65724);
            list.Add(65990);
            list.Add(66551);
            list.Add(65649);
            list.Add(63007);
            list.Add(64579);
            list.Add(66594);
            list.Add(64822);
            list.Add(64811);
            list.Add(66185);
            list.Add(63752);
            list.Add(63575);
            list.Add(64688);
            list.Add(62816);
            list.Add(63177);
            list.Add(62112);
            list.Add(63409);
            list.Add(60108);
            list.Add(64820);
            list.Add(66051);
            list.Add(60403);
            list.Add(63343);
            list.Add(65358);
            list.Add(65381);
            list.Add(62046);
            list.Add(65481);
            list.Add(66560);
            list.Add(66583);
            list.Add(63486);
            list.Add(64720);
            list.Add(64854);
            list.Add(66217);
            list.Add(63618);
            list.Add(61980);
            list.Add(65747);
            list.Add(62650);
            list.Add(60835);
            list.Add(66151);
            list.Add(64920);
            list.Add(64861);
            list.Add(62685);
            list.Add(63172);
            list.Add(61549);
            list.Add(64423);
            list.Add(63106);
            list.Add(65027);
            list.Add(64557);
            list.Add(65244);
            list.Add(60813);
            list.Add(61766);
            list.Add(62668);
            list.Add(66129);
            list.Add(66063);
            list.Add(64640);
            list.Add(63538);
            list.Add(60198);
            list.Add(65078);
            list.Add(63704);
            list.Add(65293);
            list.Add(66229);
            list.Add(65980);
            list.Add(65227);
            list.Add(60630);
            list.Add(64225);
            list.Add(57773);
            list.Add(64108);
            list.Add(63321);
            list.Add(65061);
            list.Add(64191);
            list.Add(60447);
            list.Add(65410);
            list.Add(65763);
            list.Add(62251);
            list.Add(64827);
            list.Add(62002);
            list.Add(63353);
            list.Add(65225);
            list.Add(64927);
            list.Add(64876);
            list.Add(63774);
            list.Add(64910);
            list.Add(66161);
            list.Add(65342);
            list.Add(62185);
            list.Add(64793);
            list.Add(65059);
            list.Add(64240);
            list.Add(65646);
            list.Add(64844);
            list.Add(64993);
            list.Add(64306);
            list.Add(65895);
            list.Add(65946);
            list.Add(65995);
            list.Add(61532);
            list.Add(60047);
            list.Add(64223);
            list.Add(64810);
            list.Add(62302);
            list.Add(66029);
            list.Add(60513);
            list.Add(64206);
            list.Add(64893);
            list.Add(62434);
            list.Add(61149);
            list.Add(66178);
            list.Add(66582);
            list.Add(65429);
            list.Add(65744);
            list.Add(62151);
            list.Add(64842);
            list.Add(63019);
            list.Add(60328);
            list.Add(65529);
            list.Add(66565);
            list.Add(65778);
            list.Add(65091);
            list.Add(65944);
            list.Add(65844);
            list.Add(62804);
            list.Add(64776);
            list.Add(61181);
            list.Add(62904);
            list.Add(65008);
            list.Add(60162);
            list.Add(64808);
            list.Add(64891);
            list.Add(65495);
            list.Add(61951);
            list.Add(66027);
            list.Add(63119);
            list.Add(66482);
            list.Add(65910);
            list.Add(58473);
            list.Add(60245);
            list.Add(65412);
            list.Add(63691);
            list.Add(65761);
            list.Add(64859);
            list.Add(62721);
            list.Add(64510);
            list.Add(63972);
            list.Add(64825);
            list.Add(63757);
            list.Add(64072);
            list.Add(61132);
            list.Add(64925);
            list.Add(66548);
            list.Add(62987);
            list.Add(64908);
            list.Add(60949);
            list.Add(62200);
            list.Add(60279);
            list.Add(66614);
            list.Add(10945);
            list.Add(64991);
            list.Add(63070);
            list.Add(64610);
            list.Add(65712);
            list.Add(65512);
            list.Add(63740);
            list.Add(66546);
            list.Add(62919);
            list.Add(62470);
            list.Add(64857);
            list.Add(66231);
            list.Add(65295);
            list.Add(63472);
            list.Add(65095);
            list.Add(59192);
            list.Add(62404);
            list.Add(63755);
            list.Add(61883);
            list.Add(64574);
            list.Add(66197);
            list.Add(63440);
            list.Add(61734);
            list.Add(63655);
            list.Add(60515);
            list.Add(64691);
            list.Add(66612);
            list.Add(62836);
            list.Add(66529);
            list.Add(66580);
            list.Add(65710);
            list.Add(61966);
            list.Add(66214);
            list.Add(66114);
            list.Add(61700);
            list.Add(64874);
            list.Add(65278);
            list.Add(64774);
            list.Add(64625);
            list.Add(64791);
            list.Add(65693);
            list.Add(63555);
            list.Add(66480);
            list.Add(64359);
            list.Add(62736);
            list.Add(63423);
            list.Add(64708);
            list.Add(60698);
            list.Add(66131);
            list.Add(62521);
            list.Add(66143);
            list.Add(64846);
            list.Add(61506);
            list.Add(60109);
            list.Add(65164);
            list.Add(64789);
            list.Add(65714);
            list.Add(60684);
            list.Add(61563);
            list.Add(60527);
            list.Add(65396);
            list.Add(65914);
            list.Add(63942);
            list.Add(65957);
            list.Add(66157);
            list.Add(64932);
            list.Add(66575);
            list.Add(64778);
            list.Add(64878);
            list.Add(64921);
            list.Add(63392);
            list.Add(60066);
            list.Add(64171);
            list.Add(62081);
            list.Add(64889);
            list.Add(65296);
            list.Add(61995);
            list.Add(66618);
            list.Add(61102);
            list.Add(60627);
            list.Add(62642);
            list.Add(66057);
            list.Add(65814);
            list.Add(62960);
            list.Add(62013);
            list.Add(64907);
            list.Add(62531);
            list.Add(64853);
            list.Add(65932);
            list.Add(64896);
            list.Add(66132);
            list.Add(55361);
            list.Add(63310);
            list.Add(64335);
            list.Add(64864);
            list.Add(63553);
            list.Add(60920);
            list.Add(65196);
            list.Add(66075);
            list.Add(60041);
            list.Add(63742);
            list.Add(65757);
            list.Add(62188);
            list.Add(66018);
            list.Add(63324);
            list.Add(60027);
            list.Add(66207);
            list.Add(61581);
            list.Add(64821);
            list.Add(64578);
            list.Add(66593);
            list.Add(65975);
            list.Add(64764);
            list.Add(63728);
            list.Add(65096);
            list.Add(64160);
            list.Add(62431);
            list.Add(65700);
            list.Add(63928);
            list.Add(62517);
            list.Add(63399);
            list.Add(65657);
            list.Add(64696);
            list.Add(63817);
            list.Add(61756);
            list.Add(64796);
            list.Add(64596);
            list.Add(63660);
            list.Add(61645);
            list.Add(62581);
            list.Add(66511);
            list.Add(65993);
            list.Add(64135);
            list.Add(64235);
            list.Add(62263);
            list.Add(64839);
            list.Add(62824);
            list.Add(62131);
            list.Add(61613);
            list.Add(65589);
            list.Add(65157);
            list.Add(64828);
            list.Add(65400);
            list.Add(61974);
            list.Add(61370);
            list.Add(63299);
            list.Add(63099);
            list.Add(64871);
            list.Add(62967);
            list.Add(62249);
            list.Add(65389);
            list.Add(66525);
            list.Add(60534);
            list.Add(61052);
            list.Add(63067);
            list.Add(62306);
            list.Add(65864);
            list.Add(65807);
            list.Add(65907);
            list.Add(65171);
            list.Add(65689);
            list.Add(60216);
            list.Add(62724);
            list.Add(66468);
            list.Add(66182);
            list.Add(61945);
            list.Add(64928);
            list.Add(62956);
            list.Add(65882);
            list.Add(63867);
            list.Add(66500);
            list.Add(61806);
            list.Add(62424);
            list.Add(64903);
            list.Add(61545);
            list.Add(60466);
            list.Add(64803);
            list.Add(62381);
            list.Add(64021);
            list.Add(63778);
            list.Add(64539);
            list.Add(61688);
            list.Add(64989);
            list.Add(65925);
            list.Add(62931);
            list.Add(64110);
            list.Add(66125);
            list.Add(66486);
            list.Add(62699);
            list.Add(66600);
            list.Add(64914);
            list.Add(64428);
            list.Add(62656);
            list.Add(65550);
            list.Add(66168);
            list.Add(66068);
            list.Add(65032);
            list.Add(62138);
            list.Add(65089);
            list.Add(62049);
            list.Add(65968);
            list.Add(64096);
            list.Add(60595);
            list.Add(64814);
            list.Add(65046);
            list.Add(62281);
            list.Add(63317);
            list.Add(62438);
            list.Add(66025);
            list.Add(66586);
            list.Add(64771);
            list.Add(66543);
            list.Add(64838);
            list.Add(65974);
            list.Add(61898);
            list.Add(60060);
            list.Add(64772);
            list.Add(64872);
            list.Add(61085);
            list.Add(62966);
            list.Add(64895);
            list.Add(64397);
            list.Add(61151);
            list.Add(65665);
            list.Add(65765);
            list.Add(65631);
            list.Add(64068);
            list.Add(63661);
            list.Add(61128);
            list.Add(66601);
            list.Add(62030);
            list.Add(64829);
            list.Add(64795);
            list.Add(64806);
            list.Add(65931);
            list.Add(62525);
            list.Add(63332);
            list.Add(66535);
            list.Add(61964);
            list.Add(65908);
            list.Add(60190);
            list.Add(65099);
            list.Add(66235);
            list.Add(64738);
            list.Add(64761);
            list.Add(65640);
            list.Add(65299);
            list.Add(61730);
            list.Add(63327);
            list.Add(63659);
            list.Add(61555);
            list.Add(66533);
            list.Add(66035);
            list.Add(66510);
            list.Add(64804);
            list.Add(62789);
            list.Add(60751);
            list.Add(62523);
            list.Add(64529);
            list.Add(65408);
            list.Add(65531);
            list.Add(60917);
            list.Add(62998);
            list.Add(66244);
            list.Add(64870);
            list.Add(64229);
            list.Add(64770);
            list.Add(62866);
            list.Add(65497);
            list.Add(66610);
            list.Add(64904);
            list.Add(60619);
            list.Add(62689);
            list.Add(64661);
            list.Add(65806);
            list.Add(65406);
            list.Add(60528);
            list.Add(64504);
            list.Add(65472);
            list.Add(63302);
            list.Add(66542);
            list.Add(65901);
            list.Add(64427);
            list.Add(64527);
            list.Add(62632);
            list.Add(62323);
            list.Add(64338);
            list.Add(66133);
            list.Add(66110);
            list.Add(66608);
            list.Add(61762);
            list.Add(64095);
            list.Add(62993);
            list.Add(65097);
            list.Add(66233);
            list.Add(65197);
            list.Add(65231);
            list.Add(62498);
            list.Add(65174);
            list.Add(63600);
            list.Add(62257);
            list.Add(64129);
            list.Add(66199);
            list.Add(64670);
            list.Add(61187);
            list.Add(60926);
            list.Add(64931);
            list.Add(66508);
            list.Add(61121);
            list.Add(63634);
            list.Add(62223);
            list.Add(64865);
            list.Add(65606);
            list.Add(65065);
            list.Add(65667);
            list.Add(64531);
            list.Add(65767);
            list.Add(61119);
            list.Add(61153);
            list.Add(65229);
            list.Add(64731);
            list.Add(65372);
            list.Add(64070);
            list.Add(66603);
            list.Add(63357);
            list.Add(62055);
            list.Add(64831);
            list.Add(66503);
            list.Add(61130);
            list.Add(66142);
            list.Add(66165);
            list.Add(62398);
            list.Add(64906);
            list.Add(64929);
            list.Add(64797);
            list.Add(65063);
            list.Add(65040);
            list.Add(66008);
            list.Add(64236);
            list.Add(62264);
            list.Add(65999);
            list.Add(64863);
            list.Add(64840);
            list.Add(60921);
            list.Add(60526);
            list.Add(63466);
            list.Add(64897);
            list.Add(65799);
            list.Add(63804);
            list.Add(65942);
            list.Add(64027);
            list.Add(61528);
            list.Add(65238);
            list.Add(64697);
            list.Add(63125);
            list.Add(66108);
            list.Add(64093);
            list.Add(66189);
            list.Add(63295);
            list.Add(64500);
            list.Add(63564);
            list.Add(60624);
            list.Add(62528);
            list.Add(66054);
            list.Add(65785);
            list.Add(61609);
            list.Add(64835);
            list.Add(63432);
            list.Add(65150);
            list.Add(65236);
            list.Add(61406);
            list.Add(66604);
            list.Add(61460);
            list.Add(61692);
            list.Add(61809);
            list.Add(62946);
            list.Add(64832);
            list.Add(63146);
            list.Add(61927);
            list.Add(64918);
            list.Add(65067);
            list.Add(66521);
            list.Add(62963);
            list.Add(64099);
            list.Add(63063);
            list.Add(59969);
            list.Add(64517);
            list.Add(65250);
            list.Add(60387);
            list.Add(63696);
            list.Add(66103);
            list.Add(65336);
            list.Add(62728);
            list.Add(66186);
            list.Add(61709);
            list.Add(62227);
            list.Add(65971);
            list.Add(64849);
            list.Add(61274);
            list.Add(66504);
            list.Add(60455);
            list.Add(64818);
            list.Add(60923);
            list.Add(61641);
            list.Add(64850);
            list.Add(64017);
            list.Add(60256);
            list.Add(64718);
            list.Add(63064);
            list.Add(63613);
            list.Add(66121);
            list.Add(64867);
            list.Add(66490);
            list.Add(62660);
            list.Add(61209);
            list.Add(66072);
            list.Add(64767);
            list.Add(60138);
            list.Add(62245);
            list.Add(65185);
            list.Add(65972);
            list.Add(65385);
            list.Add(62142);
            list.Add(62978);
            list.Add(65703);
            list.Add(65872);
            list.Add(60038);
            list.Add(61524);
            list.Add(60991);
            list.Add(62663);
            list.Add(64031);
            list.Add(60373);
            list.Add(66221);
            list.Add(65085);
            list.Add(62646);
            list.Add(66539);
            list.Add(61192);
            list.Add(60588);
            list.Add(62727);
            list.Add(60305);
            list.Add(60405);
            list.Add(65621);
            list.Add(64899);
            list.Add(63763);
            list.Add(64650);
            list.Add(65821);
            list.Add(66036);
            list.Add(64868);
            list.Add(63296);
            list.Add(66522);
            list.Add(65718);
            list.Add(64785);
            list.Add(65921);
            list.Add(65635);
            list.Add(65286);
            list.Add(64699);
            list.Add(65804);
            list.Add(66004);
            list.Add(64882);
            list.Add(63631);
            list.Add(64836);
            list.Add(66471);
            list.Add(66222);
            list.Add(64782);
            list.Add(64885);
            list.Add(65100);
            list.Add(60070);
            list.Add(64799);
            list.Add(65704);
            list.Add(65818);
            list.Add(65904);
            list.Add(64485);
            list.Add(62378);
            list.Add(64800);
            list.Add(65636);
            list.Add(61173);
            list.Add(63214);
            list.Add(63045);
            list.Add(62596);
            list.Add(61342);
            list.Add(64382);
            list.Add(60838);
            list.Add(62845);
            list.Add(60924);
            list.Add(62095);
            list.Add(60841);
            list.Add(65467);
            list.Add(60606);
            list.Add(63950);
            list.Add(62209);
            list.Add(65753);
            list.Add(61992);
            list.Add(64886);
            list.Add(66022);
            list.Add(65404);
            list.Add(65235);
            list.Add(63128);
            list.Add(62192);
            list.Add(65086);
            list.Add(64582);
            list.Add(64817);
            list.Add(64917);
            list.Add(62510);
            list.Add(64768);
            list.Add(60137);
            list.Add(66171);
            list.Add(64900);
            list.Add(65418);
            list.Add(60655);
            list.Add(61960);
            list.Add(65939);
            list.Add(63881);
            list.Add(61474);
            list.Add(63446);
            list.Add(66020);
            list.Add(65402);
            list.Add(65387);
            list.Add(61122);
            list.Add(60289);
            list.Add(63833);
            list.Add(61726);
            list.Add(63298);
            list.Add(61626);
            list.Add(64766);
            list.Add(64852);
            list.Add(66223);
            list.Add(62193);
            list.Add(61958);
            list.Add(64769);
            list.Add(60590);
            list.Add(64901);
            list.Add(60607);
            list.Add(62379);
            list.Add(65519);
            list.Add(63112);
            list.Add(66587);
            list.Add(60172);
            list.Add(66037);
            list.Add(65920);
            list.Add(62176);
            list.Add(62662);
            list.Add(62897);
            list.Add(64869);
            list.Add(64234);
            list.Add(66473);
            list.Add(63579);
            list.Add(64884);
            list.Add(63679);
            list.Add(60908);
            list.Add(66573);
            list.Add(64166);
            list.Add(62812);
            list.Add(64784);
            list.Add(59454);
            list.Add(62394);
            list.Add(58049);
            list.Add(66470);
            list.Add(65820);
            list.Add(63648);
            list.Add(61140);
            list.Add(66187);
            list.Add(60622);
            list.Add(64498);
            list.Add(62829);
            list.Add(63616);
            list.Add(64801);
            list.Add(66487);
            list.Add(61289);
            list.Add(66038);
            list.Add(65938);
            list.Add(66556);
            list.Add(63198);
            list.Add(65219);
            list.Add(60940);
            list.Add(62208);
            list.Add(64833);
            list.Add(62325);
            list.Add(62494);
            list.Add(66570);
            list.Add(66155);
            list.Add(63144);
            list.Add(65351);
            list.Add(64916);
            list.Add(61976);
            list.Add(65955);
            list.Add(62262);
            list.Add(64183);
            list.Add(64765);
            list.Add(60136);
            list.Add(66170);
            list.Add(60271);
            list.Add(60889);
            list.Add(62312);
            list.Add(64819);
            list.Add(64919);
            list.Add(62512);
            list.Add(64816);
            list.Add(60139);
            list.Add(64098);
            list.Add(65887);
            list.Add(60657);
            list.Add(64902);
            list.Add(60571);
            list.Add(64851);
            list.Add(66588);
            list.Add(65220);
            list.Add(61493);
            list.Add(60975);
            list.Add(65151);
            list.Add(65838);
            list.Add(66505);
            list.Add(62947);
            list.Add(66056);
            list.Add(64284);
            list.Add(66119);
            list.Add(63580);
            list.Add(63465);
            list.Add(64802);
            list.Add(66488);
            list.Add(61811);
            list.Add(61290);
            list.Add(62744);
            list.Add(62011);
            list.Add(63548);
            list.Add(65755);
            list.Add(63698);
            list.Add(66523);
            list.Add(64147);
            list.Add(64834);
            list.Add(63629);
            list.Add(59971);
            list.Add(61794);
            list.Add(65252);
            list.Add(63729);
            list.Add(65601);
            list.Add(66205);
            list.Add(65169);
            list.Add(62444);
            list.Add(66188);
            list.Add(64365);
            list.Add(64883);
            list.Add(62793);
            list.Add(62244);
            list.Add(65988);
            list.Add(60704);
            list.Add(66537);
            list.Add(66102);
            list.Add(66237);
            list.Add(64233);
            list.Add(61508);
            list.Add(65470);
            list.Add(64866);
            list.Add(65066);
            list.Add(62261);
            list.Add(63062);
            list.Add(64751);
            list.Add(66606);
            list.Add(64783);
            list.Add(61239);
            list.Add(62862);
            list.Add(65802);
            list.Add(66002);
            list.Add(60616);
            list.Add(66615);
            list.Add(62654);
            list.Add(64792);
            list.Add(64692);
            list.Add(65379);
            list.Add(60029);
            list.Add(66515);
            list.Add(64892);
            list.Add(61165);
            list.Add(61852);
            list.Add(65041);
            list.Add(61652);
            list.Add(64992);
            list.Add(64443);
            list.Add(66581);
            list.Add(62167);
            list.Add(65075);
            list.Add(62150);
            list.Add(64643);
            list.Add(66564);
            list.Add(63624);
            list.Add(63169);
            list.Add(65777);
            list.Add(65894);
            list.Add(64875);
            list.Add(64726);
            list.Add(62954);
            list.Add(66498);
            list.Add(62184);
            list.Add(64809);
            list.Add(64909);
            list.Add(64560);
            list.Add(60295);
            list.Add(64609);
            list.Add(65058);
            list.Add(66532);
            list.Add(65960);
            list.Add(65860);
            list.Add(64743);
            list.Add(64858);
            list.Add(61901);
            list.Add(63956);
            list.Add(65877);
            list.Add(65845);
            list.Add(64826);
            list.Add(63756);
            list.Add(66598);
            list.Add(65994);
            list.Add(64775);
            list.Add(62705);
            list.Add(63003);
            list.Add(64458);
            list.Add(62769);
            list.Add(63556);
            list.Add(63871);
            list.Add(64807);
            list.Add(66181);
            list.Add(64707);
            list.Add(64209);
            list.Add(64658);
            list.Add(66530);
            list.Add(64592);
            list.Add(61567);
            list.Add(60997);
            list.Add(63605);
            list.Add(62033);
            list.Add(62935);
            list.Add(61080);
            list.Add(65726);
            list.Add(65843);
            list.Add(65228);
            list.Add(66596);
            list.Add(64824);
            list.Add(62537);
            list.Add(66479);
            list.Add(66513);
            list.Add(66613);
            list.Add(62786);
            list.Add(63539);
            list.Add(64841);
            list.Add(66562);
            list.Add(65394);
            list.Add(65677);
            list.Add(60682);
            list.Add(59663);
            list.Add(66198);
            list.Add(64924);
            list.Add(66496);
            list.Add(64575);
            list.Add(64911);
            list.Add(66162);
            list.Add(63571);
            list.Add(61599);
            list.Add(63322);
            list.Add(60646);
            list.Add(65260);
            list.Add(63637);
            list.Add(65509);
            list.Add(60348);
            list.Add(65294);
            list.Add(63471);
            list.Add(65392);
            list.Add(62237);
            list.Add(65094);
            list.Add(64158);
            list.Add(66196);
            list.Add(62601);
            list.Add(65377);
            list.Add(61167);
            list.Add(66030);
            list.Add(64690);
            list.Add(60514);
            list.Add(64141);
            list.Add(62718);
            list.Add(64590);
            list.Add(66213);
            list.Add(65194);
            list.Add(65077);
            list.Add(64058);
            list.Add(64407);
            list.Add(62486);
            list.Add(63371);
            list.Add(62552);
            list.Add(63454);
            list.Add(65277);
            list.Add(66147);
            list.Add(66245);
            list.Add(65026);
            list.Add(62301);
            list.Add(60850);
            list.Add(66062);
            list.Add(64173);
            list.Add(60429);
            list.Add(64860);
            list.Add(66211);
            list.Add(62252);
            list.Add(62484);
            list.Add(64843);
            list.Add(64777);
            list.Add(65275);
            list.Add(63875);
            list.Add(61382);
            list.Add(64926);
            list.Add(65996);
            list.Add(66145);
            list.Add(60031);
            list.Add(65730);
            list.Add(64894);
            list.Add(64794);
            list.Add(60246);
            list.Add(64156);
            list.Add(65979);
            list.Add(65092);
            list.Add(62501);
            list.Add(64677);
            list.Add(66194);
            list.Add(64877);

            for (int i = table.Rows.Count - 1; i >= 0; i--)
            {
                if (list.Contains(Convert.ToInt32(table.Rows[i]["Campaign_ID"].ToString())))
                    table.Rows.RemoveAt(i);
            }
        }

        private void RemoveFM(DataTable table)
        {
            List<string> list = new List<string>();

            list.Add("0508"); //EFR

            for (int i = table.Rows.Count - 1; i >= 0; i--)
            {
                if (list.Contains((table.Rows[i]["FMID"].ToString())))
                    table.Rows.RemoveAt(i);
            }
        } 
	}
}
