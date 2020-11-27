/**
 ****************************** Module Header *****************************
    Project:      Line Tracker Application
    File:         LineTracker.cs

    Copyright (c) Ivan Nikolovski
    Contact: <Website>

    Main class of the line tracker application

    All rights reserved.

    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
    EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
    WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
***************************************************************************
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using wclWeDoFramework;

namespace WeDo_Line_Tracker
{
    public class LineTracker
    {
        private wclWeDoHub hub1;    //  Top Hub
        private wclWeDoHub hub2;    // Bottom Hub
        private wclWeDoMotionSensor ms1;    // Right Sensor
        private wclWeDoMotionSensor ms2;    // Left Sensor
        private wclWeDoMotor motor1;    // Right Motor
        private wclWeDoMotor motor2;    // Left Motor
        private Form DForm;         // Form on which relevant information should be shown.
        
        // These are the addresses of the 2 hubs the line tracker is made out of
        // *** Please replace with the addresses of your hubs. ***
        private string Hub1Address = "13614235101830";
        private string Hub2Address = "558230286332";

        private bool Running = false;  // Represents the current state of the line tracker.


        public LineTracker(Form DForm)
        {
            this.DForm = DForm;
        }

        public wclWeDoHub Hub1 { get => hub1; set => hub1 = value; }
        public wclWeDoHub Hub2 { get => hub2; set => hub2 = value; }
        public wclWeDoMotionSensor Ms1 { get => ms1; set => ms1 = value; }
        public wclWeDoMotionSensor Ms2 { get => ms2; set => ms2 = value; }
        public wclWeDoMotor Motor1 { get => motor1; set => motor1 = value; }
        public wclWeDoMotor Motor2 { get => motor2; set => motor2 = value; }

        /**
        * <summary>Add found hubs are added as variables, triggers events Hub_OnDeviceAttached,
        * finds the listbox by its tag, so it can make all the listbox text changes.</summary>
        * <param name="Hub">The hub to be added to the line tracker</param>
        * <returns>
        * Integer 1 if hub 1 was added,
        * Integer 2 if hub 2 was added,
        * Integer 0 if no hubs were added.
        * </returns>
        */
        public int AddHub(wclWeDoHub Hub)
        {
            if (Hub.Address.ToString().Equals(Hub1Address))
            {
                Hub1 = Hub;
                Hub1.OnDeviceAttached += Hub_OnDeviceAttached;
                ListBox listBox = (ListBox)Find_Element_By_Tag("listbox");
                listBox.Items.Add("Connected to hub:" + Hub1.Address);
                if (Hub2 != null)
                {
                    listBox.Items.Add("Both hubs connected successfully.");
                    listBox.Items.Add("Press start to start the Line Tracker");
                }
                else
                {
                    listBox.Items.Add("Waiting to connect to the other hub");
                }
                return 1;
            }
            if (Hub.Address.ToString().Equals(Hub2Address))
            {
                Hub2 = Hub;
                Hub2.OnDeviceAttached += Hub_OnDeviceAttached;
                ListBox listBox = (ListBox)Find_Element_By_Tag("listbox");
                listBox.Items.Add("Connected to hub:" + Hub2.Address);
                if (Hub1 != null)
                {
                    listBox.Items.Add("Both hubs connected successfully.");
                    listBox.Items.Add("Press start to start the Line Tracker");
                }
                else
                {
                    listBox.Items.Add("Waiting to connect to the other hub");
                }
                return 2;
            }
            return 0;
        }

        /**
        * <summary>All found motion sensors are added as variables.</summary>
        * <param name="MS">The MS to be added to the line tracker</param>
        * <returns>
        * Integer 1 if Sensor 1 was added,
        * Integer 2 if Sensor 2 was added,
        * Integer 0 if no Sensors were added.
        * </returns>
        */
        public int AddMS(wclWeDoMotionSensor MS)
        {
            if (MS.Hub.Address.ToString().Equals(Hub1Address))
            {
                Ms1 = MS;
                    Ms1.OnDistanceChanged += Ms_OnDistanceChanged;
                return 1;
            }
            if (MS.Hub.Address.ToString().Equals(Hub2Address))
            {
                Ms2 = MS;
                    Ms2.OnDistanceChanged += Ms_OnDistanceChanged;
                return 2;
            }
            return 0;
        }

        /**
        * <summary> Stops all motors and triggers event Motor_OnPowerChanged, 
        * also adds one line of text to the listbox.</summary>
        */
        internal void Break()
        {
            if (Motor1 != null && Motor2 != null)
            {
                Running = false;
                Motor1.Brake();
                Motor2.Brake();
                Motor_OnPowerChanged();
                ListBox listBox = (ListBox)Find_Element_By_Tag("listbox");
                listBox.Items.Add("Line Tracker stopped running");
            }
        }

        /**
        * <summary> Starts all motors on full power, triggers event Motor_OnPowerChanged, 
        * also adds one line of text to the listbox.</summary>
        */
        internal void Start()
        {
            if (Motor1 != null && Motor2 != null)
            {
                Running = true;
                Motor1.Run(wclWeDoMotorDirection.mdLeft, 100);
                Motor2.Run(wclWeDoMotorDirection.mdRight, 100);
                Motor_OnPowerChanged();
                ListBox listBox = (ListBox)Find_Element_By_Tag("listbox");
                listBox.Items.Add("Line Tracker started running");
            }
        }

        /**
        * <summary>Changes the colors of the arrows on the form, depends if each motor is on or off.</summary>
        */
        public void Motor_OnPowerChanged()
        {
            Control Arrow_Left = Find_Element_By_Tag("Arrow_Left");
            Control Arrow_Right = Find_Element_By_Tag("Arrow_Right");
            Control Arrow_Up = Find_Element_By_Tag("Arrow_Up");
            if (Motor1 != null && Motor2 != null)
            {
                if (Motor1.Power == 100 && Motor2.Power < 100)
                {
                    //Moving Right
                    ((Label)Arrow_Left).ForeColor = Color.Gray;
                    ((Label)Arrow_Right).ForeColor = Color.Green;
                    ((Label)Arrow_Up).ForeColor = Color.Gray;
                }
                else if (Motor1.Power < 100 && Motor2.Power == 100)
                {
                    //Moving Left
                    ((Label)Arrow_Left).ForeColor = Color.Green;
                    ((Label)Arrow_Right).ForeColor = Color.Gray;
                    ((Label)Arrow_Up).ForeColor = Color.Gray;
                }
                else if (Motor1.Power == 100 && Motor2.Power == 100)
                {
                    //Moving Up
                    ((Label)Arrow_Left).ForeColor = Color.Gray;
                    ((Label)Arrow_Right).ForeColor = Color.Gray;
                    ((Label)Arrow_Up).ForeColor = Color.Green;
                }
                else
                {
                    //Stopped
                    ((Label)Arrow_Left).ForeColor = Color.Gray;
                    ((Label)Arrow_Right).ForeColor = Color.Gray;
                    ((Label)Arrow_Up).ForeColor = Color.Gray;
                }
            }
        
        }

        /**
        * <summary>When a motion sensor's distance is changed.</summary>
        * <param name="sender">Object on which the change happened</param>
        * <param name="e">Additional information about the event</param>
        */
        public void Ms_OnDistanceChanged(object sender, EventArgs e)
        {
            if (Running == false)
            {
                return;
            }
            //Motion sensors detect values over 3 when they are over the black line otherwise the value is 0 or close to 0.
            if (Ms1 != null && Ms2 != null)
            {
                if (Motor1 != null && Motor2 != null)
                {
                    LineTrackerEvent Event = LineTrackerEvent.None;

                    if (Ms1.Distance > 0 && Motor2.Power != 0)
                    {
                        Event = LineTrackerEvent.Motor2_HitLine;
                    }
                    else if (Ms1.Distance == 0 && Motor2.Power < 100)
                    {
                        Event = LineTrackerEvent.Motor2_LeftLine;
                    }
                    else if (Ms2.Distance > 0 && Motor1.Power != 0)
                    {
                        Event = LineTrackerEvent.Motor1_HitLine;
                    }
                    else if (Ms2.Distance == 0 && Motor1.Power < 100)
                    {
                        Event = LineTrackerEvent.Motor1_LeftLine;
                    }
                    

                    switch (Event)
                    {
                        case LineTrackerEvent.Motor2_HitLine:
                            Motor2.Brake();
                            Motor_OnPowerChanged();
                            break;

                        case LineTrackerEvent.Motor2_LeftLine:
                            Motor2.Run(wclWeDoMotorDirection.mdRight, 100);
                            Motor_OnPowerChanged();
                            break;

                        case LineTrackerEvent.Motor1_HitLine:
                            Motor1.Brake();
                            Motor_OnPowerChanged();
                            break;

                        case LineTrackerEvent.Motor1_LeftLine:
                            Motor1.Run(wclWeDoMotorDirection.mdLeft, 100);
                            Motor_OnPowerChanged();
                            break;
                    }
                }
            }
            
        }

        /**
        * <summary>Any found motors will be set as a wclWeDoMotor, also checks to see which hub the motors belong to.</summary>
        * <param name="Motor">The Motor to be added to the line tracker</param>
        * <returns>
        * Integer 1 if Motor 1 was added,
        * Integer 2 if Motor 2 was added,
        * Integer 0 if no Motors were added.
        * </returns>
        */
        public int AddMotor(wclWeDoMotor Motor)
        {
            if (Motor.Hub.Address.ToString().Equals(Hub1Address))
            {
                Motor1 = Motor;
                Motor.Brake();
                Motor_OnPowerChanged();
                return 1;
            }
            if (Motor.Hub.Address.ToString().Equals(Hub2Address))
            {
                Motor2 = Motor;
                Motor.Brake();
                Motor_OnPowerChanged();
                return 2;
            }
            return 0;
        }

        /**
        * <summary>Checks to see which devices are attached.</summary>
        * <param name="Sender">Object on which the change happened</param>
        * <param name="Device">Device that's been attached to the hub</param>
        */
        private void Hub_OnDeviceAttached(object Sender, wclWeDoIo Device)
        {
            if (Device.DeviceType == wclWeDoIoDeviceType.iodMotionSensor)
            {
                wclWeDoMotionSensor MS = Device as wclWeDoMotionSensor;
                int index = AddMS(MS);
                if (index == 1)
                {
                    //MotionSensor1_lbl.ForeColor = Color.Green;
                    Control C = Find_Element_By_Tag("MS1");
                    if (C != null)
                    {
                        C.ForeColor = Color.Green;
                    }
                }
                else if (index == 2)
                {
                    //MotionSensor2_lbl.ForeColor = Color.Green;
                    Control C = Find_Element_By_Tag("MS2");
                    if (C != null)
                    {
                        C.ForeColor = Color.Green;
                    }
                }
            }
            else if (Device.DeviceType == wclWeDoIoDeviceType.iodMotor)
            {
                wclWeDoMotor Motor = Device as wclWeDoMotor;
                int index = AddMotor(Motor);
                if (index == 1)
                {
                    //Motor1_lbl.ForeColor = Color.Green;
                    Control C = Find_Element_By_Tag("Motor1");
                    if (C != null)
                    {
                        C.ForeColor = Color.Green;
                    }
                }
                else if (index == 2)
                {
                    //Motor2_lbl.ForeColor = Color.Green;
                    Control C = Find_Element_By_Tag("Motor2");
                    if (C != null)
                    {
                        C.ForeColor = Color.Green;
                    }
                }
                Motor.Brake();
            }
        }

        /**
        * <summary>Finds an element/component by it's given tag</summary>
        * <param name="Tag">Tag of control that we're looking for</param>
        * <returns>
        * Control with given tag or null if one doesn't exist.
        * </returns>
        */
        private Control Find_Element_By_Tag(string Tag)
        {
            Control C = null;
            if(DForm == null)
            {
                return C;
            }

           foreach (Control control in DForm.Controls)
            {
                if (control.Tag != null && control.Tag.ToString().Equals(Tag))
                {
                    C = control;
                    break;
                }
            }

            return C;
        }
    }
}
