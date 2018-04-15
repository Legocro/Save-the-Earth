using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

public class SaveManager : MonoBehaviour
{
    private FrameUpdate FrameScript;
    // Use this for initialization
    public void Awake()
    {
        FrameScript = GetComponent<FrameUpdate>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Load()
    {
		Debug.Log("no");
		Debug.Log(FrameScript);
        FrameScript.Money = PlayerPrefs.GetFloat("Money");
        FrameScript.Power = PlayerPrefs.GetFloat("Power");
        FrameScript.Pollution = PlayerPrefs.GetFloat("Pollution");
        FrameScript.Timer = PlayerPrefs.GetInt("Timer");
        FrameScript.MaxPowerEarned = PlayerPrefs.GetInt("MaxPowerEarned");
        FrameScript.PremiumCurrency = PlayerPrefs.GetInt("PremiumCurrency");

		//Debug.Log(PlayerPrefs.GetFloat("Power"));
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("Money", FrameScript.Money);
        PlayerPrefs.SetFloat("Power", FrameScript.Power);
        PlayerPrefs.SetFloat("Pollution", FrameScript.Pollution);
        PlayerPrefs.SetFloat("MaxPowerEarned", FrameScript.MaxPowerEarned);
        PlayerPrefs.SetInt("Timer", FrameScript.Timer);
        PlayerPrefs.SetInt("PremiumCurrency", FrameScript.PremiumCurrency);
		PlayerPrefs.SetString("CleanGens","0"); //TODO: Serialization
        PlayerPrefs.SetInt("HasData", 1);
		//Debug.Log("9");
    }
}
