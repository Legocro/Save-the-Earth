using System;
using System.Linq;
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
            Buildings = ConcatArrays(FrameScript.CleanGens , FrameScript.PollutionGens);
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
    }
}