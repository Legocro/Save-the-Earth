using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;


public class LightningPanel : MonoBehaviour {
	
	public Sprite Red;
	public Sprite Blue;
	public Sprite Purple;
	public GameObject CEnPanel;
	public GameObject PEnPanel;
	public GameObject PCPanel;
	public GameObject Panel;
	public Scrollbar Scroll;
	private int Index = 0;
	private Sprite[] Sprites;
	private GameObject[] Panels;
	public GameObject CurrentPanel;
	private FrameUpdate FrameScript;
	// Use this for initialization
	void Start () {
		Sprites = new Sprite[] {Blue, Red, Purple};
		Panels = new GameObject[] {CEnPanel, PEnPanel, PCPanel};
		CurrentPanel = Panels[0];
		FrameScript = GetComponent<FrameUpdate>();
	}

	// Update is called once per frame
	void Update () {
		
	}
	public void LeftClick()
	{
		Scroll.value = 1;
		Panels[Index].SetActive(false);
		Index = Index == 0 ? Sprites.Length - 1 : Index - 1;
		Panel.GetComponent<Image>().sprite = Sprites[Index];
		Panels[Index].SetActive(true);
		CurrentPanel = Panels[Index];
		FrameScript.UpdateScroll();
	}
	public void RightClick()
	{
		Scroll.value = 1;
		Panels[Index].SetActive(false);
		Index = Index == Sprites.Length - 1 ? 0 : Index + 1;
		Panel.GetComponent<Image>().sprite = Sprites[Index];
		Panels[Index].SetActive(true);
		CurrentPanel = Panels[Index];
		FrameScript.UpdateScroll();
	}
	public void GoToPanel(GameObject TargetPanel)
	{
		Panels[Index].SetActive(false);
		Index = Array.IndexOf(Panels, TargetPanel);
		Panel.GetComponent<Image>().sprite = Sprites[Index];
		Panels[Index].SetActive(true);
		CurrentPanel = Panels[Index];
		FrameScript.UpdateScroll();

	}
}
