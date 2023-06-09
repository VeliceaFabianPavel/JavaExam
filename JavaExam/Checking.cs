﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.IO;
using System.Security.Principal;
using System.Diagnostics;

namespace JavaExam
{
	public partial class Checking : Form
	{
		public static bool IsInternetConnected()
		{
			try
			{
				using (var client = new WebClient())
				{
					using (client.OpenRead("http://clients3.google.com/generate_204"))
					{
						return true;
					}
				}
			}
			catch
			{
				return false;
			}
		}
		public static bool FolderExistsInAppData(string folderName)
		{
			// Get the path to the current user's AppData folder
			string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

			// Combine the AppData path with the folder name
			string folderPath = Path.Combine(appDataPath, folderName);

			// Check if the folder exists
			return Directory.Exists(folderPath);
		}
		

	public static bool IsRunningWithAdminRights()
	{
		WindowsIdentity identity = WindowsIdentity.GetCurrent();
		WindowsPrincipal principal = new WindowsPrincipal(identity);

		return principal.IsInRole(WindowsBuiltInRole.Administrator);
	}
	public static bool AreTwoOrMoreScreensConnected()
		{
			return Screen.AllScreens.Length >= 2;
		}
		private bool IsIntelliJInstalled()
		{
			// Check the file system
			string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
			string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
			string[] possiblePaths = {
				Path.Combine(programFiles, "JetBrains"),
				Path.Combine(programFilesX86, "JetBrains")
			};

			foreach (string path in possiblePaths)
			{
				if (Directory.Exists(path))
				{
					string[] directories = Directory.GetDirectories(path);
					foreach (string directory in directories)
					{
						if (directory.Contains("IntelliJ"))
						{
							return true;
						}
					}
				}
			}

			return false;
		}

		public void Check()
		{
			////Initialising
			button6.Visible = false;
			button5.Visible = false;
			button4.Visible = false;
			button3.Visible = false;
			button2.Visible = false;
			button1.Visible = false;
			button7.Visible = false;
			////
			////Connectivity checking
			bool isConnected = IsInternetConnected();
			if (isConnected == true)
			{
				pictureBox1.Image = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("ok");
			}
			else
			{
				pictureBox1.Image = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("no");
				button6.Visible = true;
			}
			////
			////JavaExam folder checking
			string folderName = "JavaExam";
			bool folderExists = FolderExistsInAppData(folderName);
			if (folderExists == true)
			{
				pictureBox2.Image = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("ok");
			}
			else
			{
				pictureBox2.Image = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("no");
				button5.Visible = true;
			}
			////
			////JavaTemp folder checking
			string folderName2 = "JavaTemp";
			bool folderExists2 = FolderExistsInAppData(folderName2);
			if (folderExists2 == true)
			{
				pictureBox3.Image = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("ok");
			}
			else
			{
				pictureBox3.Image = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("no");
				button4.Visible = true;
			}
			////
			////Screens Count
			bool twoOrMoreScreens = AreTwoOrMoreScreensConnected();
			if (twoOrMoreScreens == false)
			{
				pictureBox4.Image = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("ok");
			}
			else
			{
				pictureBox4.Image = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("no");
				button3.Visible = true;
			}
			////
			////Administrative Rights
			bool isAdmin = IsRunningWithAdminRights();
			if(isAdmin==true)
			{
				pictureBox5.Image = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("ok");
			}
			else
			{
				pictureBox5.Image = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("no");
				button2.Visible = true;
			}
			////
			////IntelliJ Check
			bool IntelliJcheck = IsIntelliJInstalled();
			if (IntelliJcheck==true)
			{
				pictureBox6.Image = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("ok");
			}
			else
			{
				pictureBox6.Image = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("no");
				button1.Visible = true;
			}
			////Launch exam Check
			if (button1.Visible==false && button2.Visible==false && button3.Visible==false&&button4.Visible==false&&button5.Visible==false&&button6.Visible==false) 
			{ 
				button7.Visible = true;
			}
			else
			{
				button7.Visible=false;
			}
		}
		public Checking()
		{
			InitializeComponent();
			Check();
		}

		private void panel2_Paint(object sender, PaintEventArgs e)
		{

		}

		private void button6_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Make Sure you have a stable internet connection.\nTry closing the app, connecting to the internet and then try again pressing on \"Refresh\"", "Connectivity Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void button5_Click(object sender, EventArgs e)
		{
			MessageBox.Show("JavaExam folder (an important folder for running the exam) is missing!\nTry reinstalling the application, or checking your antivirus software", "Folder Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			MessageBox.Show("JavaTemp folder (an important folder for running the exam) is missing!\nTry reinstalling the application, or checking your antivirus software", "Folder Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Only one screen is allowed!\nDisconnect any external screens, then try again pressing on \"Refresh\"", "External Screens Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void button8_Click(object sender, EventArgs e)
		{
			Check();
		}

		private void button2_Click(object sender, EventArgs e)
		{


			if (MessageBox.Show("You need to run this app as an administrator!\nClick on OK, and the application will restart.\n\nYOU WILL HAVE TO LOG IN AGAIN!", "Administrative rights", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK && !IsRunningWithAdminRights())
				{
				// Start a new instance of the application with administrative rights
				var processStartInfo = new ProcessStartInfo
				{
					FileName = Application.ExecutablePath,
					Verb = "runas",
					UseShellExecute = true
				};

				try
				{
					Process.Start(processStartInfo);
					Application.Exit(); // Close the current instance of the application
				}
				catch (Exception ex)
				{
					// Log the exception or show a message to the user
					MessageBox.Show("Failed to restart the application with administrative rights: " + ex.Message);
				}
			}
			else
			{
				MessageBox.Show("The application is already running with administrative rights.");
			}
		}

		private void label7_Click(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			MessageBox.Show("IntelliJ IDEA is missing.\nIf you didn't installed it, download it from https://jetbrains.com and then try again by pressing on \"Refresh\"\nIf you however are sure that the app is intalled, try reinstalling it and then try again by pressing on \"Refresh\"", "IntelliJ Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void button7_Click(object sender, EventArgs e)
		{
			Tutorial tutorial = new Tutorial();
			tutorial.Show();
			Hide();
		}
	}

}

