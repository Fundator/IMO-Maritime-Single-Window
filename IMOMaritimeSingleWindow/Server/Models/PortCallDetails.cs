﻿using System;
using System.Collections.Generic;

namespace IMOMaritimeSingleWindow.Models
{
    public partial class PortCallDetails
    {
        public int PortCallDetailsId { get; set; }
        public int PortCallId { get; set; }
        public int? NumberOfCrew { get; set; }
        public int? NumberOfPassengers { get; set; }
        public float? ActualDraught { get; set; }
        public float? AirDraught { get; set; }
        public bool? ReportingHazmat { get; set; }
        public bool? ReportingCargo { get; set; }
        public bool? ReportingShipStores { get; set; }
        public bool? ReportingCrew { get; set; }
        public bool? ReportingPax { get; set; }

        public PortCall PortCall { get; set; }
    }
}
