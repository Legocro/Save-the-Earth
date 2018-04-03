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
        public GameObject ScrollView;
        public Text Currencies;
        public Text EProduction;
        public Text Premium;
        public Text MoneyConversion;
        public Text TimerTextContainer;
        public Text PremiumConversion;
    /*  public Button SellPowerButton;
        public Button ConvertCurrencyButton;
        public Button DoubleMoneyButton;
        public Button SellAllPowerButton;*/
        public GameObject BuildingPrefab;
        private Menus Pause;
        private LightningPanel Panels;
        private int IsPaused;
        private string Currency;
        private int Frames;
        private enum Headers : int { CURRENCY, GENERATORS, PRODUCTION, COST, PREMIUM, MONEY, TIMER, TEMP };

        // Currency
        public float Power;
        private float PowerProduction;
        public float Money;
        private float Pollution;
        public int PremiumCurrency;

        // Util
        public float PremiumConversionRate;
        private float MaxPowerEarned;
        public float MinimumPowerFlag;
        public int DoubleMoneyCost;
        private bool DoubleMoneyBought;
        private float MoneyConversionRate;
        private int Timer;
        public bool ConvertUnlocked;
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
        private int BuildingCounter = 0;

        // Buildings
        private List<BuildingPanel> AllBuildings = new List<BuildingPanel>();
        private GameData Data;
        // UI stuff
        private string PowerText;
        private string PowerProductionText;
        private string MoneyText;
        private string SolarGeneratorsText;
        private string SolarGeneratorCostText;
        private string CoalGeneratorsText;
        private string CoalGeneratorCostText;
        private string PollutionText;
        private string PollutionReducersText;
        private string PollutionReducersCostText;
        private string PremiumCurrencyText;
        private string DoubleMoneyCostText;
        private string MoneyConversionRateText;
        private string TimerText;
        private string Temporary;

        // Initialization
        private void Awake() {
            InitValues();
        }
        void Start()
        {
            //InitButtons();
            //InitValues();
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

        /*void FixedUpdate()
        {
            if (Frames % 6 == 0)
            {
                PerTenthSecond();
            }
        }*/



        void ConvertToText()
        {
            PowerText = Power.ToString("F2");
            PowerProductionText = PowerProduction.ToString("F2");
            MoneyText = Money.ToString("F2");
            PremiumCurrencyText = PremiumCurrency.ToString();
            MoneyConversionRateText = MoneyConversionRate.ToString("F2");
            PollutionText = Pollution.ToString();
            TimerText = Timer.ToString();
        }

        string formatText(Headers header, params string[] values)
        {
            switch (header)
            {
                case Headers.CURRENCY:
                    return string.Format("Power: {0} Money: ${1} Pollution: {2}%", values[0], values[1], values[2]);
                case Headers.PREMIUM:
                    return string.Format("SuperMoney: ${0}", values[0]);
                case Headers.MONEY:
                    return string.Format("1 Power = {0} Money", values[0]);
                case Headers.TIMER:
                    return string.Format("Timer: {0}", values[0]);
            }
            return "SOMETHING WENT WRONG YO";

        }

        void InitValues()
        {
            Data = GetComponent<GameData>();
            Pause = GetComponent<Menus>();
            Panels = GetComponent<LightningPanel>();
            Frames = 0;
            Power = 50f;
            Money = 0f;
            Pollution = 0f;
            PollutionCoeficient = 25;
            PremiumCurrency = 0;
            MaxPowerEarned = 0;
            PremiumConversionRate = 0.5f;
            MinimumPowerFlag = 100f;
            DoubleMoneyCost = 100;
            DoubleMoneyBought = false;
            MoneyConversionRate = 0.5f;
            ConvertUnlocked = false;
            CleanGens = Data.CleanEnList;
            PollutionGens = Data.PollEnList;
            BuildBuilding(BuildingPrefab, CleanGens[0]);
      }

        void SetAllText()
        {
            Currencies.text = formatText(Headers.CURRENCY, PowerText, MoneyText, PollutionText);
            Premium.text = formatText(Headers.PREMIUM, PremiumCurrencyText);
            MoneyConversion.text = formatText(Headers.MONEY, MoneyConversionRateText);
            TimerTextContainer.text = formatText(Headers.TIMER, TimerText);
            PremiumConversion.text = string.Format("Cost: {0} Power", (MinimumPowerFlag * PremiumConversionRate).ToString("F2"));
            EProduction.text = string.Format("Energy per second: {0}", PowerProduction);
        }

        /*void CheckCosts()
        {
            if (Power < MinimumPowerFlag * PremiumConversionRate || !ConvertUnlocked)
            {
                ConvertCurrencyButton.interactable = false;
            }
            else
            {
                ConvertCurrencyButton.interactable = true;
            }
            if (PremiumCurrency < DoubleMoneyCost)
            {
                DoubleMoneyButton.interactable = false;
            }
            else
            {
                DoubleMoneyButton.interactable = true;
            }
        }*/
        float GetTotalPowerProduction(Building[] CGens, Building[] PGens) 
        {
            /*float production = 0;
            foreach (Building b in CGens)
            {
                production += b.Energy * b.Quantity;
            }*/
            return 10; //production;

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
           // Debug.Log(Panels.CEnPanel.name);
           // Debug.Log(IdkWhyThisHappens["Pollution Panel"]);
        }

        public void UpdateScroll()
        {
            RectTransform ScrollTransform = ScrollView.GetComponent<RectTransform>();
            Rect ScrollRect = ScrollTransform.rect;
            int BuildingCounter = Counters[Panels.CurrentPanel.name];
            int Height = (int)ScrollRect.height;
            int TotalBuildingPanelHeight = BuildingCounter * 135;
            if (Height < TotalBuildingPanelHeight)
            {
               // Panels.CEnPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0 , ScrollRect.height+200);
                IdkWhyThisHappens[Panels.CurrentPanel.name]++;
                ScrollView.GetComponent<RectTransform>().sizeDelta = new Vector2(0, ScrollRect.height + 150);
            }
            else
            {
                ScrollView.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 400 + 150 * IdkWhyThisHappens[Panels.CurrentPanel.name]);
            }
        }

        void UpdateBuilding(BuildingPanel BuildingPanel)
        {
            Building BuildingPanelBuilding = BuildingPanel.building;
            GameObject BuildingPanelGameObject = BuildingPanel.gameObject;
            BuildingPanelGameObject.transform.Find("QuantityText").GetComponent<Text>().text = string.Format("Amount: {0}", BuildingPanelBuilding.Quantity);
            if (Money < BuildingPanelBuilding.Cost)
            {
                BuildingPanelGameObject.transform.Find("BuyButton").GetComponent<Button>().interactable = false;
                return;
            }
            BuildingPanelGameObject.transform.Find("BuyButton").GetComponent<Button>().interactable = true;
        }

        void BuildBuildings(params Building[] buildings)
        {
            foreach (Building b in buildings)
            {
               // BuildBuilding(BuildingPrefab, b);
            }
        }

        public void BuildBuilding(GameObject b , Building Building)
        {
            //UpdateScroll();
            CleanGens[Array.IndexOf(CleanGens,Building)].Built = true;
            GameObject temp = Instantiate(b, b.transform.position, b.transform.rotation);
            GameObject BPanel = Building.Panel;
            temp.SetActive(true);
            temp.transform.SetParent(BPanel.transform);
            temp.transform.Find("NameText").GetComponent<Text>().text = Building.Name;
            temp.transform.Find("PropertyText").GetComponent<Text>().text = Building.Description;
            temp.transform.Find("QuantityText").GetComponent<Text>().text = string.Format("Amount: {0}", Building.Quantity);

            BuildingPanel thisBuildingPanel = new BuildingPanel(temp, Building);

            temp.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(Building.AddOne);
            temp.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(delegate { BuyBuilding(Building); });
            temp.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(delegate { UpdateBuilding(thisBuildingPanel); });
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0f,130 - Counters[Panels.CurrentPanel.name]*130 + 75*IdkWhyThisHappens[Panels.CurrentPanel.name], 0);
            AllBuildings.Add(thisBuildingPanel);
            Counters[Panels.CurrentPanel.name]++;
            UpdateScroll();
        }

        // Runs every frame
        void PerFrame()
        {
            Pollution =  0;//(float)CoalGen.Quantity * CoalGen.Pollution;
            PowerProduction = GetTotalPowerProduction(CleanGens, PollutionGens); //SolarGen.Quantity * SolarGen.Energy + CoalGen.Quantity * CoalGen.Energy;
            PowerProduction = PowerProduction * (1 / ((Pollution + PollutionCoeficient) / PollutionCoeficient));
            ConvertToText();
            SetAllText();
            //CheckCosts();
            UpdatePowerFlag();
        }

        // Runs ten times per second
        void PerTenthSecond()
        {
            IsPaused = Pause.GetState();
            //Power += IsPaused * PowerProduction / 10;

        }
        // Runs five times per second
        void PerFifthSecond()
        {
            AllBuildings.ForEach(UpdateBuilding);
        }

        // Runs every second
        void EverySec()
        {
            Timer++;
            // BuildBuilding(BuildingPrefab, SolarGen);
            // BuildBuilding(BuildingPrefab, CoalGen);

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

        // Buttons
        /*void BuyOneSolar()
        {
            SolarGenerators++;
            Money -= SolarGeneratorCost;
        }

        void BuyOneCoal()
        {
            CoalGenerators++;
            Pollution++;
            Money -= CoalGeneratorCost;
        }*/
        void BuyBuilding(Building building)
        {
            Money -= building.Cost;
        }
        public void SellPower()
        {
            Power--;
            Money += MoneyConversionRate;
        }

        public void SellAllPower()
        {
            UpdateScroll();
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
