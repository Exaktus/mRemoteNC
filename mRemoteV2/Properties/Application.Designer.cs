using System.Collections.Generic;
using System;
using AxWFICALib;
using System.Drawing;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Windows.Forms;
using mRemoteNC.App;
using My;


//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



namespace mRemoteNC
{
	namespace My
	{
		
		//NOTE: This file is auto-generated; do not modify it directly.  To make changes,
		// or if you encounter build errors in this file, go to the Project Designer
		// (go to Project Properties or double-click the My Project node in
		// Solution Explorer), and make changes on the Application tab.
		//
		public partial class MyApplication : global::Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase
		{
			[STAThread]
			static void Main()
			{
			    Application.EnableVisualStyles();
				(new MyApplication()).Run(new string[] {});
			}
			
			[global::System.Diagnostics.DebuggerStepThrough()]public MyApplication() : base(global::Microsoft.VisualBasic.ApplicationServices.AuthenticationMode.Windows)
			{
				this.IsSingleInstance = false;
				this.EnableVisualStyles = true;
				this.SaveMySettingsOnExit = true;
				this.ShutdownStyle = global::Microsoft.VisualBasic.ApplicationServices.ShutdownMode.AfterMainFormCloses;
			}
			
			[DebuggerStepThrough()]protected override void OnCreateMainForm()
			{
				this.MainForm = frmMain.Default;
			}
		}
	}
	
}
