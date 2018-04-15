using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        private void Awake()
        {
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
            Buildings = ConcatArrays(FrameScript.CleanGens , FrameScript.PollutionGens, FrameScript.PollutionCleaners);
            Money = FrameScript.Money;
            UpdateBuildings();
        }
        void UpdateBuildings()
        {
            foreach (Building b in Buildings)
            {
                if (Money >= b.PowerBreakpoint && !b.Built)
                {
                    Lightning.GoToPanel(b.Panel);
                    FrameScript.BuildBuilding(FrameScript.BuildingPrefab, b);
                }
            }
        }
        public static T[] ConcatArrays<T>(params T[][] list)
        {
            var result = new T[list.Sum(a => a.Length)];
            int offset = 0;
            for (int x = 0; x < list.Length; x++)
            {
                list[x].CopyTo(result, offset);
                offset += list[x].Length;
            }
            return result;
        }
        public void UpdateBuilding(BuildingPanel BuildingPanel)
        {
            Building BuildingPanelBuilding = BuildingPanel.building;
            GameObject BuildingPanelGameObject = BuildingPanel.gameObject;
            BuildingPanelGameObject.transform.Find("QuantityText").GetComponent<Text>().text = string.Format("Amount: {0}", BuildingPanelBuilding.Quantity);
            BuildingPanelGameObject.transform.Find("CostText").GetComponent<Text>().text = string.Format("Cost: {0}", BuildingPanelBuilding.Cost);
            BuildingPanelGameObject.transform.Find("ProductionText").GetComponent<Text>().text = string.Format("Production: {0}", BuildingPanelBuilding.Energy * BuildingPanelBuilding.Quantity);
            if (FrameScript.Money < BuildingPanelBuilding.Cost)
            {
                BuildingPanelGameObject.transform.Find("BuyButton").GetComponent<Button>().interactable = false;
                return;
            }
            BuildingPanelGameObject.transform.Find("BuyButton").GetComponent<Button>().interactable = true;
        }
    }
}