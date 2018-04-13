using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;
namespace Main
{
	
	public class GameData : MonoBehaviour 
	{
		private LightningPanel Lightning;
		private int[] Breakpoints = new int[] {0, 35, 170, 750, 4200};
		private int Index = 0;
		// Use this for initialization
		void Awake () {
			Lightning = GetComponent<LightningPanel>();
			foreach (Building b in CleanEnList)
			{
				CleanEnList[Index].Panel = Lightning.CEnPanel;
				CleanEnList[Index].PowerBreakpoint = Breakpoints[Index];
				Index++;
			}
			Index = 0;
			foreach (Building b in PollEnList)
			{
				PollEnList[Index].Panel = Lightning.PEnPanel;
				PollEnList[Index].PowerBreakpoint = Breakpoints[Index]*1.2f;
				Index++;
			}
			Index = 0;

		}

		// Update is called once per frame
		void Update () {

		}
		// Building (quantity pollution energy cost image name desc)
		public Building[] CleanEnList = new Building[]
		{
			new Building(0, 0, 1, 10, null, "Solar Generator" , "Uses sunlight to produce 1 energy/s"),
			new Building(0, 0, 5, 75, null, "Solar Generator Mark 2" , "Uses sunlight to produce 5 energy/s"),
			new Building(0, 0, 15, 250, null, "Wind Generator" , "Uses the wind to produce 15 energy/s"),
			new Building(0, 0, 50, 1000, null, "River Dam" , "Uses the flow of water to produce 50 energy/s"),
			new Building(0, 0, 300, 5000, null, "Hydroplant" , "Uses the force of the sea to produce 300 energy/s")
		};
		public Building[] PollEnList = new Building[]
		{
			new Building (0, 1, 7, 20, null, "Coal Generator" , "Burns coal to produce 7 energy/s, increases pollution by 1"),
			new Building (0, 4, 18, 150, null, "Coal Generator Mark 2" , "Burns coal to produce 18 energy/s, increases pollution by 4"),
			new Building (0, 13, 40, 600, null, "Oil Generator" , "Burns oil to produce 40 energy/s, increases pollution by 13"),
			new Building (0, 20, 120, 2000, null, "Coal Power Plant" , "Burns coal to produce 120 energy/s, increases pollution by 20"),
			new Building (0, 37, 650, 10000, null, "Gas Generator" , "Burns natural gas to produce 650 energy/s, increases pollution by 37"),
		};
        public Building[] PollClList = new Building[]
        {
            new Building (0, -1, 0, 40, null, "Tree", "Produces fresh air and reduces pollution by 1"),
            new Building (0, -3, 0, 300, null, "Small Forest", "Produces fresh air and reduces pollution by 3"),
            new Building (0, -8, 0, 1200, null, "Emission filter", "A generator filter that reduces pollution by 8"),
            new Building (0, -14, 0, 4000, null, "Power Plant chimney", "A chimney that filters emission and reduces pollution by 14"),
            new Building (0, -22, 0, 25000, null, "Gas filter", "A filter that reduces pollution by 22")
        };

	}		
}