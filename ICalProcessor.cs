using System;

namespace edomizil_functions
{
    public class ICalProcessor {

        private const int linesPreAmble = 4;
        private const int linesPerEvent = 8;
        private const int offsetName = 2;
        private const int offsetStartDate = 5;
        private const int offsetEndDate = 6;
        public static OccupancyList Parse(string input){

            var parts = input.Split(Environment.NewLine);
            int numOccuppancies = (int)((parts.Length - 5) / 8);  
            var occList = new OccupancyList();
            for(int i = 0; i < numOccuppancies; i++){
                var firstLineOfOccupancy = linesPreAmble + i * linesPerEvent;
                var lineName = parts[firstLineOfOccupancy + offsetName];
                var lineStartDate = parts[firstLineOfOccupancy + offsetStartDate];
                var lineEndDate = parts[firstLineOfOccupancy + offsetEndDate];
                var occ = new Occupancy(OccupancyType.EDomizil, lineName, convertDate(lineStartDate), convertDate(lineEndDate));
                occList.ListOfOccupancies.Add(occ);
            }
            return occList;
        }

        private static DateTime convertDate(string dateLine){
            var parts = dateLine.Split(':');
            
            var year = int.Parse(parts[1].Substring(0, 4));
            var month = int.Parse(parts[1].Substring(4, 2));
            var day = int.Parse(parts[1].Substring(6,2));
            
            return new DateTime(year, month, day);
        }

    }
}