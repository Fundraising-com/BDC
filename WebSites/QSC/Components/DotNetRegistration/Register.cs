using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Data;

namespace DotNetRegistration
{
	/// <summary>
	/// Summary description for Register.
	/// </summary>
	public class Register : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader colAction;
		private System.Windows.Forms.ColumnHeader colResult;
		private RegistrationConfig registrationConfig1;
		private static string configXMLName = string.Empty;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		static int Main(string[] args)
		{
			try
			{
				string dllName = string.Empty;
				string generatorName = string.Empty;
				string guid = string.Empty;
				string defaultKeyValue = string.Empty;
				if(args.Length > 0)
				{
					if(args.Length == 4)
					{
						//Accept all of the values as arguments
						dllName = args[0];
						generatorName = args[1];
						guid = args[2];
						defaultKeyValue = args[3];
						new Register(dllName,generatorName,guid,defaultKeyValue);

					}
					else
					{
						//Or just accept the name of the config file
						configXMLName = args[0];
						new Register();
					}
				}
				else
				{
					//Or assume the default config file name (DotNetRegistrationConfig.xml)
					new Register();
				}
				return 0;

			}
			catch(Exception e)
			{
				//This will catch any exceptions return a non-zero value (representing an error status)
				return 100;
			}
			
			
		}

		#region Constructors
		public Register(string dllName, string generatorName, string guid, string defaultKeyValue)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.Show();
			this.Activate();

			CompleteRegistration(dllName,generatorName,guid,defaultKeyValue);
		}

		public Register()
		{
			InitializeComponent();
			this.Show();
			this.Activate();
			if(configXMLName == string.Empty)
			{
				configXMLName = "DotNetRegistrationConfig.xml";
			}


			string currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
			try
			{
				registrationConfig1.ReadXml(currentDir+configXMLName);
				RegistrationConfig.dotNetRegistrationRow rowCfg = (RegistrationConfig.dotNetRegistrationRow)registrationConfig1.dotNetRegistration.Rows[0];
				CompleteRegistration(rowCfg.dllName,rowCfg.customToolName,rowCfg.guid,rowCfg.prodDesc);
			}
			catch(Exception e)
			{
				MakeListEntry("Unable to load DotNetRegistrationConfig.xml file",false,true);
				Thread.Sleep(new TimeSpan(0,0,3));
				throw new ApplicationException("Error");
			}
		}
		#endregion
	

		/// <summary>
		/// Actually performs the registration using regasm.exe and regedit.exe
		/// </summary>
		/// <param name="dllName">Name of the Dll being registered as a VS.NET Custom tool</param>
		/// <param name="generatorName">Name to use as the custom tool</param>
		/// <param name="guid">Guid of the assembly class that is the custom tool</param>
		/// <param name="defaultKeyValue">Description of the custom tool to be entered into the registry</param>
		/// <returns>0 (Zero) for successful registration</returns>
		public int CompleteRegistration(string dllName, string generatorName,string guid, string defaultKeyValue)
		{
			Color errorBack = Color.Red;
			Color errorFore = Color.Wheat;


			//This app must be run in the same directory as the Dll to be registered. 
			//Of course, this could be changed, but I haven't seen the need!
			string full = System.Reflection.Assembly.GetExecutingAssembly().Location;
			string dir = Path.GetDirectoryName(full);

			//Set the root path for the .NET Runtime (where you can find regasm.exe)
			string dotNetRoot = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
			string currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
			try
			{
				Process prc= new Process();
				ListViewItem item = new ListViewItem();

//Make sure regasm.exe is where it's supposed to be!
if(!File.Exists(dotNetRoot + "regasm.exe"))
{

	MakeListEntry("Unable to locate regasm.exe",false,true);
	Thread.Sleep(new TimeSpan(0,0,5));
	throw new ApplicationException("Install Failed");
}

//Make sure the Assembly to register is present and accounted for
if(!File.Exists(currentDir+dllName))
{
	MakeListEntry("Unable to locate "+currentDir+dllName,false,true);
	MakeListEntry("** Installation Failed **",true,true);
	MessageBox.Show(currentDir+dllName);
	Thread.Sleep(new TimeSpan(0,0,5));
	throw new ApplicationException("Install Failed");

}

				//Get started
				MakeListEntry("Registering Type Library",false,false);

				//Register the Type library for the Assembly
				prc.StartInfo.FileName = dotNetRoot + "regasm.exe";
				prc.StartInfo.Arguments = "/tlb \""+ currentDir+dllName+"\"";
				prc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				prc.StartInfo.WorkingDirectory = dir;
				prc.Start();
				prc.WaitForExit();
	
				if(prc.ExitCode == 0)
				{
					MakeListEntry("Success",true,false);
				}
				else
				{
					MakeListEntry("Failed. Error code:" + prc.ExitCode.ToString(),true,true);
					throw new ApplicationException("Install Failed");
				}
				Thread.Sleep(new TimeSpan(0,0,1));
			

				//Register the Assembly code base
				MakeListEntry("Registering Codebase",false,false);

				prc.StartInfo.FileName = dotNetRoot + "regasm.exe";
				prc.StartInfo.Arguments = "/codebase \"" +currentDir+dllName+"\"";
				prc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				prc.StartInfo.WorkingDirectory = dir;
				prc.Start();
				prc.WaitForExit();
				
				if(prc.ExitCode == 0)
				{
					MakeListEntry("Success",true,false);
				}
				else
				{
					MakeListEntry("Failed. Error code:" + prc.ExitCode.ToString(),true,true);
					throw new ApplicationException("Install Failed");
				}
				Thread.Sleep(new TimeSpan(0,0,1));

				//If we need to perform the registry entry, then do it, otherwise exit.
				if(generatorName != string.Empty && guid != string.Empty && defaultKeyValue != string.Empty)
				{
				
					//Register the Assembly as a VS.NET generator
					MakeListEntry("Making VS.NET Generator Registry Entry",false,false);
					string regFileName = ModifyRegistrationValue(currentDir,generatorName,guid,defaultKeyValue);
					if(regFileName.Length > 0)
					{

						prc.StartInfo.FileName = "regedit";
						prc.StartInfo.Arguments = "/s \"" +currentDir+regFileName+"\"";
						prc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
						prc.StartInfo.WorkingDirectory = dir;
						prc.Start();
						prc.WaitForExit();

						if(prc.ExitCode == 0)
						{
							MakeListEntry("Success",true,false);
						}
						else
						{
							MakeListEntry("Failed. Error code:" + prc.ExitCode.ToString(),true,true);
							throw new ApplicationException("Install Failed");
						}
					
					}
					else
					{
						MakeListEntry("Failed to retrieve registry file",false,true);
						throw new ApplicationException("Install Failed");
					}
				}
				Thread.Sleep(new TimeSpan(0,0,3));

				//Return 0 for a successful finish!
				return 0;
				
				
			}
			finally
			{
			}
		}
		
		/// <summary>
		/// Helper method to make the list view item entries
		/// </summary>
		/// <param name="verbage">Text to display</param>
		/// <param name="appendToLast">Whether or not to make a new list item or add this to the last item as a new sub-item</param>
		/// <param name="isError">Flag for an error message (makes the color Red)</param>
		private void MakeListEntry(string verbage, bool appendToLast, bool isError)
		{
			ListViewItem item;
			if(appendToLast)
			{
				item = listView1.Items[listView1.Items.Count-1];
				item.SubItems.Add(verbage);
			}
			else
			{
				item = new ListViewItem(verbage);
			}
			
			if(isError)
			{
				item.BackColor = Color.Red;
				item.ForeColor = Color.Wheat;
			}

			if(!appendToLast)
			{
				listView1.Items.Add(item);
			}
			listView1.Invalidate();
			this.Update();

		}
		/// <summary>
		/// Helper method to modify the template .reg file with the values specified.
		/// </summary>
		/// <param name="currentDirectory">Directory of the executing assembly (which should
		/// be the same directory as the template .reg file</param>
		/// <param name="generatorName">Name to use for the Custom Tool</param>
		/// <param name="guid">Class ID for the Custom Tool</param>
		/// <param name="defaultKeyValue">Description for the custom tool</param>
		/// <returns>The modified .reg file template as a string</returns>
		private string ModifyRegistrationValue(string currentDirectory,string generatorName, string guid, string defaultKeyValue)
		{
			try
			{
				string fullPath = currentDirectory + "DotNetRegistration.reg";
				StringBuilder fileContents = new StringBuilder();
				using(StreamReader streamReader = new StreamReader(fullPath))
				{
					fileContents.Append(streamReader.ReadToEnd());
				}

				fileContents =  fileContents.Replace("<<GeneratorName>>",generatorName);
				fileContents =  fileContents.Replace("<<guid>>",guid);
				fileContents =  fileContents.Replace("<<DefaultKeyValue>>",defaultKeyValue);
				
				StreamWriter writer =  File.CreateText(currentDirectory+generatorName+".reg");
				writer.Write(fileContents.ToString());
				writer.Flush();
				writer.Close();
				return generatorName+".reg";
			}
			catch(Exception e)
			{
				return "";
			}
		}
	
		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Register));
			this.listView1 = new System.Windows.Forms.ListView();
			this.colAction = new System.Windows.Forms.ColumnHeader();
			this.colResult = new System.Windows.Forms.ColumnHeader();
			this.registrationConfig1 = new RegistrationConfig();
			((System.ComponentModel.ISupportInitialize)(this.registrationConfig1)).BeginInit();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.colAction,
																						this.colResult});
			this.listView1.Location = new System.Drawing.Point(23, 12);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(555, 108);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// colAction
			// 
			this.colAction.Text = "Action Performed";
			this.colAction.Width = 311;
			// 
			// colResult
			// 
			this.colResult.Text = "Result";
			this.colResult.Width = 240;
			// 
			// registrationConfig1
			// 
			this.registrationConfig1.DataSetName = "RegistrationConfig";
			this.registrationConfig1.EnforceConstraints = false;
			this.registrationConfig1.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// Register
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(600, 133);
			this.Controls.Add(this.listView1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Register";
			this.Text = "Registering .NET Assembly as VS.NET Custom Tool";
			((System.ComponentModel.ISupportInitialize)(this.registrationConfig1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


	}
}
