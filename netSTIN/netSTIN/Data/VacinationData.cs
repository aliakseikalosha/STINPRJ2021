using System;
using System.Collections.Generic;
namespace netSTIN.Data
{
    public class VacinationData
    {
        public List<VacinationStateData> data = new List<VacinationStateData>();
        public DateTime day;
    }
    public class VacinationStateData
    {
        public float Percent = 0;
        public int Population = 0;
        public string StateName = "";
    }
}