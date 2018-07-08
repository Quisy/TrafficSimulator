using System.Collections.Generic;
using TrafficSimulator.Enums;

namespace TrafficSimulator.Models.Maps
{
    public class SignDescription
    {
        public SignType SignType { get; set; }

        public char MapMark { get; set; }

        public Dictionary<string, string> Properties { get; set; }
    }
}
