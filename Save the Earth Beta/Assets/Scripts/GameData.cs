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
			foreach (Building b in PollCleanerList)
			{
				PollCleanerList[Index].Panel = Lightning.PCPanel;
				PollCleanerList[Index].PowerBreakpoint = b.Cost*0.7f;
				Index++;
			}
			CleanEnList[0].Built = true;
			PollEnList[0].Built = true;
			Index = 0;

		}

		// Update is called once per frame
		void Update () {

		}
		// Building (quantity pollution energy cost image name desc)
		public Building[] CleanEnList = new Building[]
		{
			new Building(0, 0, 1, 10, null, "Solar Generator" , "Uses sunlight to produce energy."),
			new Building(0, 0, 5, 150, null, "Solar Generator Mark 2" , "Uses sunlight to produce even more energy."),
			new Building(0, 0, 15, 500, null, "Wind Generator" , "Uses the wind to produce energy."),
			new Building(0, 0, 50, 2000, null, "River Dam" , "Uses the flow of rivers to produce energy."),
			new Building(0, 0, 300, 10000, null, "Hydroplant" , "Uses the force of the sea to produce energy.")
		};
		public Building[] PollEnList = new Building[]
		{
			new Building (0, 1, 7, 30, null, "Coal Generator" , "Burns coal to produce energy, creates pollution"),
			new Building (0, 4, 18, 250, null, "Coal Generator Mark 2" , "Burns coal to produce energy even more energy, creates pollution"),
			new Building (0, 13, 40, 1000, null, "Oil Generator" , "Burns oil to produce energy, creates pollution"),
			new Building (0, 20, 120, 3500, null, "Coal Power Plant" , "Burns coal to produce energy, creates pollution"),
			new Building (0, 37, 650, 15000, null, "Gas Generator" , "Burns natural gas to produce energy, creates pollution")
		};
        public Building[] PollCleanerList = new Building[]
        {
            new Building (0, -1, 0, 40, null, "Tree", "Produces fresh air and reduces pollution by 1"),
            new Building (0, -3, 0, 300, null, "Small Forest", "Produces fresh air and reduces pollution by 3"),
            new Building (0, -8, 0, 1200, null, "Emission filter", "A generator filter that reduces pollution by 8"),
            new Building (0, -14, 0, 4000, null, "Power Plant chimney", "A chimney that filters emission and reduces pollution by 14"),
            new Building (0, -22, 0, 25000, null, "Gas filter", "A filter that reduces pollution by 22")
        };


	}		
}