using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class BuildingPanel
    {
        public GameObject gameObject;
        public Building building;
        public int BuildingCount = 0;
        public BuildingPanel(GameObject g, Building b)
        {
            gameObject = g;
            building = b;
        }

    }
    public class Building
    {

        public Image Thumbnail;
        public string Name;
        public string Description;
        public int Quantity;
        public int Pollution;
        public int Energy;
        public float Cost;
        public float PowerBreakpoint {get; set;}
        public GameObject Panel {get; set;}
        public bool Built {get; set;}
        public Building(int quantity, int pollution, int energy, int cost, Image thumbnail, string name, string Desc)
        {
            Thumbnail = thumbnail;
            Name = name;
            Description = Desc;
            Quantity = quantity;
            Pollution = pollution;
            Energy = energy;
            Cost = cost;
        }
        public virtual void AddOne()
        {
            Quantity++;
            Cost *= 1.1f;
        }
    }
}