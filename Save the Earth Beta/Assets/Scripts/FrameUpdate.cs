using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;

namespace Main
{
    public class FrameUpdate : MonoBehaviour
    {


        // Constants
        public Building[] CleanGens;
        public Building[] PollutionGens;
        public Building[] PollutionCleaners;
        public GameObject Content;
        public GameObject BuildingPrefab;
        // Scripts
        private Menus Pause;
        private LightningPanel Panels;
        private GameData Data;
        private BuildingManager BuildingManager;
        private SaveManager SaveManager;
        //  ¯\_(ツ)_/¯
        private int IsPaused;
        private string Currency;
        private int Frames;
        private enum Headers : int { CURRENCY, GENERATORS, PRODUCTION, COST, PREMIUM, MONEY, TIMER, TEMP };

        // Currency
        public float Power;
        public float PowerProduction;
        public float Money;
        public float Pollution;
        public int PremiumCurrency;

        // Util
        public float PremiumConversionRate;
        public float MaxPowerEarned;
        public float MinimumPowerFlag;
        public int DoubleMoneyCost;
        private bool DoubleMoneyBought;
        public float MoneyConversionRate;
        public int Timer;
        public bool ConvertUnlocked;
        private int BuildingCounter = 0;
        private int PollutionCoeficient;


        private Dictionary<string, int> Counters = new Dictionary<string, int>()
            {
                {"Clean Panel" , 0},
                {"Pollution Panel" , 0},
                {"Cleaner Panel" , 0}
            };
        private Dictionary<string, int> IdkWhyThisHappens = new Dictionary<string, int>()
            {
                {"Clean Panel" , 0},
                {"Pollution Panel" , 0},
                {"Cleaner Panel" , 0}
            };

        private List<BuildingPanel> AllBuildings = new List<BuildingPanel>();

        // Initialization
        private void Awake()
        {
            InitValues();
        }
        void Start()
        {
            BuildBuildings();
        }

        //  Update is called once per frame
        void Update()
        {
            PerFrame();
            Frames++;
            if (Frames % 6 == 0)
            {
                PerTenthSecond();
            }
            if (Frames % 12 == 0)
            {
                PerFifthSecond();
            }
            if (Frames % 60 == 0)
            {
                EverySec();
            }
            if (Frames % 120 == 0)
            {
                EveryTwoSec();
            }
            if (Frames % 150 == 0)
            {
                EveryTwoAndAHalfSec();
            }
        }
        void InitValues()
        {
            Data = GetComponent<GameData>();
            Pause = GetComponent<Menus>();
            Panels = GetComponent<LightningPanel>();
            BuildingManager = GetComponent<BuildingManager>();
            SaveManager = GetComponent<SaveManager>();
            //Debug.Log(SaveManager);
            PollutionCoeficient = 25;
            PremiumConversionRate = 0.5f;
            MinimumPowerFlag = 100000f;
            DoubleMoneyCost = 10000;
            DoubleMoneyBought = false;
            Frames = 0;
            MoneyConversionRate = 0.05f;
            ConvertUnlocked = false;
            CleanGens = Data.CleanEnList;
            PollutionGens = Data.PollEnList;
            PollutionCleaners = Data.PollCleanerList;
            if (PlayerPrefs.GetInt("HasData") == 1)
            {
                SaveManager.Awake();
                SaveManager.Load();
                //Debug.Log("WE DID IT BOIS");
            }else
            {
                Power = 50f;
                Money = 0f;
                Pollution = 0f;
                PremiumCurrency = 0;
                MaxPowerEarned = 0;

            }
        }

        public float GetTotalPowerProduction(Building[] CGens, Building[] PGens)
        {
            float production = 0;
            foreach (Building b in CGens)
            {
                production += b.Energy * b.Quantity;
            }
            foreach (Building b in PGens)
            {
                production += b.Energy * b.Quantity;
            }
            return production;

        }

        public float GetTotalPollution(Building[] PGens, Building[] PCleaners)
        {
            float pollution = 0;
            foreach (Building b in PGens)
            {
                pollution += b.Pollution * b.Quantity;
            }
            foreach (Building b in PCleaners)
            {
                pollution += b.Pollution * b.Quantity;
            }
            return pollution;
        }

        void UpdatePowerFlag()
        {
            if (Power > MaxPowerEarned)
            {
                MaxPowerEarned = Power;
            }
            if (Power >= MinimumPowerFlag)
            {
                //MinimumPowerFlag = Power;
                ConvertUnlocked = true;
            }
        }
        public void DebugAll()
        {
        }

        public void UpdateScroll()
        {
            RectTransform ScrollTransform = Content.GetComponent<RectTransform>();
            Rect ScrollRect = ScrollTransform.rect;
            int BuildingCounter = Counters[Panels.CurrentPanel.name];
            int Height = (int)ScrollRect.height;
            int TotalBuildingPanelHeight = BuildingCounter * 130;
            if (640 < TotalBuildingPanelHeight)
            {
                // Panels.CEnPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0 , ScrollRect.height+200);
                IdkWhyThisHappens[Panels.CurrentPanel.name]++;
                Content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 640 + 200 * (float)Math.Ceiling((TotalBuildingPanelHeight - 630) / 200d));
            }
            else
            {
                Content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 640);
            }
        }

        void BuildBuildings()
        {
            foreach (Building b in BuildingManager.ConcatArrays(CleanGens, PollutionGens, PollutionCleaners))
            {
                if (b.Built)
                {
                    BuildBuilding(BuildingPrefab, b);
                }
            }
        }

        public void BuildBuilding(GameObject Prefab, Building Building)
        {
            int Index;
            if (Array.IndexOf(CleanGens, Building) >= 0)
            {
                Index = Array.IndexOf(CleanGens, Building);
                CleanGens[Index].Built = true;
            }
            else if (Array.IndexOf(PollutionGens, Building) >= 0)
            {
                Index = Array.IndexOf(PollutionGens, Building);
                PollutionGens[Index].Built = true;
            }
            else
            {
                Index = Array.IndexOf(PollutionCleaners, Building);
                PollutionCleaners[Index].Built = true;
            }

            GameObject temp = Instantiate(Prefab, Prefab.transform.position, Prefab.transform.rotation);
            GameObject BPanel = Building.Panel;
            temp.SetActive(true);
            temp.transform.SetParent(BPanel.transform);
            temp.transform.Find("NameText").GetComponent<Text>().text = Building.Name;
            temp.transform.Find("PropertyText").GetComponent<Text>().text = Building.Description;
            temp.transform.Find("QuantityText").GetComponent<Text>().text = string.Format("Amount: {0}", Building.Quantity);
            temp.transform.Find("CostText").GetComponent<Text>().text = string.Format("Cost: {0}", Building.Cost);
            temp.transform.Find("ProductionText").GetComponent<Text>().text = string.Format("Production: {0}", Building.Energy);

            BuildingPanel thisBuildingPanel = new BuildingPanel(temp, Building);

            temp.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(Building.AddOne);
            temp.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(delegate { BuyBuilding(Building); });
            temp.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(delegate { BuildingManager.UpdateBuilding(thisBuildingPanel); });
            int CounterThing = (int)Math.Ceiling((Counters[Building.Panel.name] * 130 - 630) / 200d) < 0 ? 0 : (int)Math.Ceiling((Counters[Building.Panel.name] * 130 - 630) / 200d);
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0f, 250 - Counters[Building.Panel.name] * 130 + 100 * CounterThing, 0);
            AllBuildings.Add(thisBuildingPanel);
            Counters[Building.Panel.name]++;
            UpdateScroll();
        }

        // Runs every frame
        void PerFrame()
        {
            Pollution = GetTotalPollution(PollutionGens, PollutionCleaners);
            PowerProduction = GetTotalPowerProduction(CleanGens, PollutionGens);
            PowerProduction = PowerProduction * (1 / ((Pollution + PollutionCoeficient) / PollutionCoeficient));
            UpdatePowerFlag();
        }

        // Runs ten times per second
        void PerTenthSecond()
        {
            IsPaused = Pause.GetState();
        }
        // Runs five times per second
        void PerFifthSecond()
        {
            AllBuildings.ForEach(BuildingManager.UpdateBuilding);
            SaveManager.Save();
        }

        // Runs every second
        void EverySec()
        {
            Timer++;
        }

        // Runs once every 2 seconds
        void EveryTwoSec()
        {
            DebugAll();
        }
        void EveryTwoAndAHalfSec()
        {
            Power += IsPaused * PowerProduction * 2.5f;
        }
        void BuyBuilding(Building building)
        {
            Money -= building.Cost / 1.1f;
        }
        public void SellPower()
        {
            Power--;
            Money += MoneyConversionRate;
        }

        public void SellAllPower()
        {
            int amount = (int)Power;
            Power = 0;
            Money += MoneyConversionRate * amount;
        }

        public void ConvertCurrency()
        {
            PremiumCurrency++;
            Power -= MinimumPowerFlag * PremiumConversionRate;

        }

        public void DoubleMoney()
        {
            Money *= 2;
            MoneyConversionRate *= 2;
            PremiumCurrency -= DoubleMoneyCost;
        }
    }
}
