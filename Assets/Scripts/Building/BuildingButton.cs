using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    public BuildingPlacer BuildingPlacer;
    public GameObject BuildingPrefab;
    public AudioSource Gold;
    private int _buildingPrice;
    public Text BuildingPriceText;


    private void Start()
    {
        _buildingPrice = BuildingPrefab.GetComponent<Building>().Price;
        BuildingPriceText.text = _buildingPrice.ToString() ;
    }
    public void TryBuy()
    {
       
        if (FindObjectOfType<Resource>().Money >= _buildingPrice)
        {
            FindObjectOfType<Resource>().Money -= _buildingPrice;
            BuildingPlacer.CreatBuilding(BuildingPrefab);

        }
        else
        {
            Gold.Play();
        }
    }
}
