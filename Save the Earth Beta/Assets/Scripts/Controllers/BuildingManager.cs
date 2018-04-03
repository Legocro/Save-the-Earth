using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;


namespace Main
{
    public class BuildingManager : MonoBehaviour
    {
        private GameData GameData;
        private FrameUpdate FrameScript;
        private LightningPanel Lightning;
        private float Money;
        private Building[] Buildings;
        // Use this for initialization
		private void Awake() {
			FrameScript = GetComponent<FrameUpdate>();
		}
        void Start()
        {
            GameData = GetComponent<GameData>();
            Lightning = GetComponent<LightningPanel>();
        }

        // Update is called once per frame
        void Update()
        {
			Buildings = FrameScript.CleanGens;
            Money = FrameScript.Money;
            UpdateBuildings();
        }
        void UpdateBuildings()
        {
            foreach (Building b in Buildings)
            {
                if (Money >= b.PowerBreakpoint && !b.Built)
                {
                    FrameScript.BuildBuilding(FrameScript.BuildingPrefab, b);
                }
            }
        }

        void ConcatenateArrays(Building[][] Arrays)
        {

        }
    }
}