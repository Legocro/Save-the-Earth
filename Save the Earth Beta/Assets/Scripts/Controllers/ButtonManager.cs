using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;

namespace Main
{

    public class ButtonManager : MonoBehaviour
    {
        public Button ConvertCurrencyButton;
        public Button SellPowerButton;
        public Button SellAllPowerButton;
        public Button DoubleMoneyButton;
        public Button LeftLightningButton;
        public Button RightLightningButton;
        private FrameUpdate FrameScript;
        private LightningPanel LightningPanelScript;
        public bool IsPremium = false;

        // Use this for initialization
        void Start()
        {
            FrameScript = GetComponent<FrameUpdate>();
            LightningPanelScript = GetComponent<LightningPanel>();
            InitButtons();
        }

        // Update is called once per frame
        void Update()
        {
           CheckCosts();
        }
        void InitButtons()
        {
            if (IsPremium)
            {
                SellAllPowerButton.onClick.AddListener(FrameScript.SellAllPower);
            }
            SellPowerButton.onClick.AddListener(FrameScript.SellPower);
            ConvertCurrencyButton.onClick.AddListener(FrameScript.ConvertCurrency);
            DoubleMoneyButton.onClick.AddListener(FrameScript.DoubleMoney);
            //LeftLightningButton.onClick.AddListener(delegate {LightningPanelScript.LeftClick(); FrameScript.UpdateScroll();});
            //RightLightningButton.onClick.AddListener(delegate {LightningPanelScript.RightClick(); FrameScript.UpdateScroll();});
            LeftLightningButton.onClick.AddListener(LightningPanelScript.LeftClick);
            RightLightningButton.onClick.AddListener(LightningPanelScript.RightClick);
        }
        void CheckCosts()
        {
            if (FrameScript.Power < FrameScript.MinimumPowerFlag * FrameScript.PremiumConversionRate || !FrameScript.ConvertUnlocked)
            {
                ConvertCurrencyButton.interactable = false;
            }
            else
            {
                ConvertCurrencyButton.interactable = true;
            }
            if (FrameScript.PremiumCurrency < FrameScript.DoubleMoneyCost)
            {
                DoubleMoneyButton.interactable = false;
            }
            else
            {
                DoubleMoneyButton.interactable = true;
            }
            if (FrameScript.Power > 0)
            {
                SellPowerButton.interactable = true;
            }
            else
            {
                SellPowerButton.interactable = false;
            }
        }
    }

}