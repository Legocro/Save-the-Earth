using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;


namespace Main
{
    public class PlayerData
    {

        public Building[] CleanGens;
        public Building[] PollutionGens;
        public Building[] PollutionCleaners;
        public Dictionary<string, int> Counters = new Dictionary<string, int>()
            {
                {"Clean Panel" , 0},
                {"Pollution Panel" , 0},
                {"Cleaner Panel" , 0}
            };
        public Dictionary<string, int> IdkWhyThisHappens = new Dictionary<string, int>()
            {
                {"Clean Panel" , 0},
                {"Pollution Panel" , 0},
                {"Cleaner Panel" , 0}
            };


    }
}