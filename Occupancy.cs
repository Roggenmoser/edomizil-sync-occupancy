using System;
using System.Collections.Generic;

namespace edomizil_functions
{
    public enum OccupancyType { EDomizil, Private }
    public class Occupancy {

        public string VisitorName { get; }
        public OccupancyType OccupancyType { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate  { get; }
        public Occupancy(OccupancyType occType, string name, DateTime startDate, DateTime endTime){

            this.OccupancyType = occType;
            this.VisitorName = name;
            this.StartDate = startDate;
            this.EndDate = endTime;

        }

    }

    public class OccupancyList {
        public List<Occupancy> ListOfOccupancies { get; }

        public OccupancyList() {
            ListOfOccupancies = new List<Occupancy>();
        }
    }    
}