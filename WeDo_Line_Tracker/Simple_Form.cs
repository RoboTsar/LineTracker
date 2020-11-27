/**
 ****************************** Module Header *****************************
    Project:      Line Tracker Application
    File:         Simple_Form.cs

    Copyright (c) Ivan Nikolovski
    Contact: <Website>

    Class with graphical elements and code for the application GUI

    All rights reserved.

    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
    EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
    WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
***************************************************************************
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wclBluetooth;
using wclCommon;
using wclWeDoFramework;

namespace WeDo_Line_Tracker
{
    public partial class Simple_Form : Form
    {

        LineTracker lineTracker;  // An instance of the line tracker class.
        wclBluetoothManager Manager;  // Bluetooth Manager for connecting to the hubs.
        wclWeDoWatcher Watcher;  // WeDoWatcher for finding the hubs.

        public Simple_Form()
        {
            InitializeComponent();
        }

        /**
        * <summary>When the Simple_Form loads</summary>
        * <param name="sender">Object on which the change happened</param>
        * <param name="e">Additional information about the event</param>
        */
        private void Simple_Form_Load(object sender, EventArgs e)
        {
            lineTracker = new LineTracker(this);
            Manager = new wclBluetoothManager();
            Watcher = new wclWeDoWatcher();
            listBox1.Items.Add("Please connect one of the hubs.");

            Watcher.OnHubFound += Watcher_OnHubFound;

            int res = Manager.Open();
            if (res != wclErrors.WCL_E_SUCCESS)
            {
                MessageBox.Show("Error opening the Manager.");
            }
            else
            {
                wclBluetoothRadio radio = null;
                for (int i = 0; i < Manager.Count; i++)
                {
                    if (Manager[i].Available)
                    {
                        radio = Manager[i];
                        break;
                    }
                }
                if (radio != null)
                {
                    res = Watcher.Start(radio);
                    if (res != wclErrors.WCL_E_SUCCESS)
                    {
                        MessageBox.Show("Can't start watching.");
                    }
                }
            }
        }

        /**
        * <summary>Once a hub has been found, connect to it</summary>
        * <param name="sender">Object on which the change happened</param>
        * <param name="Address">The address of the hub</param>
        * <param name="Name">Name of the found hub</param>
        */
        private void Watcher_OnHubFound(object Sender, long Address, string Name)
        {
            wclWeDoHub Hub = new wclWeDoHub();
            int res = Hub.Connect(Watcher.Radio, Address);
            if (res != wclErrors.WCL_E_SUCCESS)
            {
                MessageBox.Show("Can't connect to the Hub.");
            }
            else
            {
                lineTracker.AddHub(Hub);
            }
        }

        /**
        * <summary>Starts the line tracker and car</summary>
        * <param name="sender">Object on which the change happened</param>
        * <param name="e">Additional information about the event</param>
        */
        private void Button_Start_Click(object sender, EventArgs e)
        {
            lineTracker.Start();
        }

        /**
        * <summary>Stops the line tracker and car</summary>
        * <param name="sender">Object on which the change happened</param>
        * <param name="e">Additional information about the event</param>
        */
        private void Button_Stop_Click(object sender, EventArgs e)
        {
            lineTracker.Break();
        }
    }
}
