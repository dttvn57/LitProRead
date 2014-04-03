using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LitProRead.BusinessObjects
{
    public class StudentStatsBO
    {
        public string Status { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        
        public int Count_Asian { get; set; }
        public int Count_Black { get; set; }
        public int Count_Latino { get; set; }
        public int Count_NativeAmerican { get; set; }
        public int Count_PacificIslander { get; set; }
        public int Count_White { get; set; }
        public int Count_Other { get; set; }
        public int Count_Unknown { get; set; }
        public int Total_Ethnicity_Count { get; set; }

        public int Count_Age_Unknown { get; set; }
        public int Count_0_15 { get; set; }
        public int Count_16_19 { get; set; }
        public int Count_20_29 { get; set; }
        public int Count_30_39 { get; set; }
        public int Count_40_49 { get; set; }
        public int Count_50_59 { get; set; }
        public int Count_60_69 { get; set; }
        public int Count_70 { get; set; }
        public int Total_Age_Count { get; set; }

        public int Count_Male { get; set; }
        public int Count_Female { get; set; }
        public int Count_Gender_Unknown { get; set; }
        public int Total_Gender_Count { get; set; }

        public int Count_FirstActiveDateIsNull { get; set; }
    }
}