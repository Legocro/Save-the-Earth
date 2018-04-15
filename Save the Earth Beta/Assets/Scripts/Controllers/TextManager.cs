using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;


namespace Main
{

    public class TextManager : MonoBehaviour
    {

        private enum Headers : int { CURRENCY, GENERATORS, PRODUCTION, COST, PREMIUM, MONEY, TIMER, TEMP };
        private FrameUpdate FrameScript;

        private string PowerText;
        private string PowerProductionText;
        private string MoneyText;
        private string PollutionText;
        private string PremiumCurrencyText;
        private string DoubleMoneyCostText;
        private string MoneyConversionRateText;
        private string TimerText;

        public Text Currencies;
        public Text EProduction;
        public Text Premium;
        public Text MoneyConversion;
        public Text TimerTextContainer;
        public Text PremiumConversion;
        // Use this for initialization
        private void Awake()
        {
            FrameScript = GetComponent<FrameUpdate>();
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
			GetValues();
			SetAllText();
        }

        private void GetValues()
        {
            PowerText = FrameScript.Power.ToString("F2");
            PowerProductionText = FrameScript.PowerProduction.ToString("F2");
            MoneyText = FrameScript.Money.ToString("F2");
            PremiumCurrencyText = FrameScript.PremiumCurrency.ToString();
            MoneyConversionRateText = FrameScript.MoneyConversionRate.ToString("F2");
            PollutionText = FrameScript.Pollution.ToString();
            TimerText = FrameScript.Timer.ToString();
        }

        private void SetAllText()
        {
            Currencies.text = formatText(Headers.CURRENCY, PowerText, MoneyText, PollutionText);
            Premium.text = formatText(Headers.PREMIUM, PremiumCurrencyText);
            MoneyConversion.text = formatText(Headers.MONEY, MoneyConversionRateText);
            TimerTextContainer.text = formatText(Headers.TIMER, TimerText);
            PremiumConversion.text = string.Format("Cost: {0} Power", (FrameScript.MinimumPowerFlag * FrameScript.PremiumConversionRate).ToString("F2"));
            EProduction.text = string.Format("Energy per second: {0}", FrameScript.PowerProduction);
        }
        string formatText(Headers header, params string[] values)
        {
            switch (header)
            {
                case Headers.CURRENCY:
                    return string.Format("Power: {0} \nMoney: ${1} \nPollution: {2}%", values[0], values[1], values[2]);
                case Headers.PREMIUM:
                    return string.Format("SuperMoney: ${0}", values[0]);
                case Headers.MONEY:
                    return string.Format("1 Power = {0} Money", values[0]);
                case Headers.TIMER:
                    return string.Format("Timer: {0}", values[0]);
            }
            return "SOMETHING WENT WRONG YO";

        }
    }

}