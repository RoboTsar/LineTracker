/**
 ****************************** Module Header *****************************
    Project:      Line Tracker Application
    File:         LineTrackerEvent.cs

    Copyright (c) Ivan Nikolovski
    Contact: <Website>

    Enumeration representing different line tracker events

    All rights reserved.

    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
    EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
    WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
***************************************************************************
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDo_Line_Tracker
{
    public enum LineTrackerEvent
    {
        None,
        Motor1_HitLine, 
        Motor1_LeftLine,
        Motor2_HitLine,
        Motor2_LeftLine
    }
}
