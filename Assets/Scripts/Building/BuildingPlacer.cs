using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public float CellSize = 1;
    public Camera RaycastCamera;
    private Plane _plane;
    public Building CurrentBulding;
    public AudioSource YouCantBuildHere;

    public Dictionary<Vector2Int, Building> BuildingsDictionary = new Dictionary<Vector2Int, Building>();
    void Start()
    {
        _plane = new Plane(Vector3.up, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentBulding == null)
        {
            return;
        }

        Ray ray = RaycastCamera.ScreenPointToRay(Input.mousePosition);
        float distance;
        _plane.Raycast(ray, out distance);
        Vector3 point = ray.GetPoint(distance) / CellSize;

        int x = Mathf.RoundToInt(point.x);
        int z = Mathf.RoundToInt(point.z);
        CurrentBulding.transform.position = new Vector3(x, 0, z) * CellSize;

        if(CheckAllow(x,z, CurrentBulding))
        {
            CurrentBulding.DisplayAcceptablePosition();
            if (Input.GetMouseButtonDown(0))
            {
                InstallBulding(x, z, CurrentBulding);
                CurrentBulding = null;
            }
        }
        else
        {
            CurrentBulding.DisplayUnacceptablePosition();
        }
    }
    bool CheckAllow(int XPosition, int ZPosition, Building Building)
    {
        for (int x = 0; x < Building.XSize; x++)
        {
            for (int z = 0; z < Building.ZSize; z++)
            {
                Vector2Int coordinate = new Vector2Int(XPosition + x, ZPosition + z);
                if (BuildingsDictionary.ContainsKey(coordinate))
                {   
                    if(Input.GetMouseButtonUp(0))
                    {
                        YouCantBuildHere.Play();
                    }
                    
                    return false;
                }
            }
        }
        return true;
    }
    void InstallBulding(int XPosition, int ZPosition, Building Building)
    {
        for (int x = 0; x < Building.XSize; x++)
        {
            for (int z = 0; z < Building.ZSize; z++)
            {
                Vector2Int coordinate = new Vector2Int(XPosition + x, ZPosition + z);
                BuildingsDictionary.Add(coordinate, CurrentBulding);
            }
        }
    }
    public void CreatBuilding(GameObject buildingPrefab)
    {
        GameObject newBuilding = Instantiate(buildingPrefab);
        CurrentBulding = newBuilding.GetComponent<Building>();
    }
}
