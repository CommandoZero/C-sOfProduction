using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySpawnSystem : MonoBehaviour
{

    public static CitySpawnSystem Instance;
    //The size of our building
    [SerializeField]
    float maxBuildingSize = 0;
    //The ratio of how big our streets are to the buildings
    [SerializeField, Range(0f, 1f)]
    float buildingToStreetRatio = 1;
    //x and y amounts of our city grid
    [SerializeField]
    Vector2 cityGrid;
    //Our streets
    [SerializeField]
    GameObject streetObject;
    [SerializeField]
    GameObject Crosswalk;

    //Our buildings
    [SerializeField]
    List<GameObject> buildingObjects = new List<GameObject>();
    //How we link our buildings
    Dictionary<int, GameObject> building = new Dictionary<int, GameObject>();

    int buildingsLeftToTake = 0;
    bool won = false;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        for (int i = 0; i < buildingObjects.Count; i++)
        {
            building.Add(i, buildingObjects[i]);
        }

        SpawnBuildings(maxBuildingSize, buildingToStreetRatio);
        buildingsLeftToTake = (int)cityGrid.x * (int)cityGrid.y;
    }
    //Function that we use to spawn our buildings
    public void SpawnBuildings(float buildingSize, float aStreetRatio)
    {
        //The distance between buildings 
        float distanceBetweenBuildings = buildingSize + (buildingSize * aStreetRatio);
        for (int i = 0; i < cityGrid.x; i++)
        {
            for (int j = 0; j < cityGrid.y; j++)
            {
                //What kind of building are we placing
                int buildingtype = Random.Range(0, buildingObjects.Count);
                //Spawn that building
                GameObject tempArea = Instantiate(buildingObjects[buildingtype], new Vector3
                                      (i * distanceBetweenBuildings, 0,
                                       j * distanceBetweenBuildings), Quaternion.identity, gameObject.transform);
                tempArea.GetComponent<BuildingScript>().SetBuildingType(buildingtype);

                //Spawn the streets in the accurate places
                Instantiate(streetObject, new Vector3(
                (i * distanceBetweenBuildings) + ((float)(buildingSize / 2) + ((float)(buildingSize / 2) * aStreetRatio)), 0,
                tempArea.transform.position.z), Quaternion.Euler(new Vector3(0, 90, 0)), tempArea.transform);

                Instantiate(streetObject, new Vector3(tempArea.transform.position.x, 0,
                (j * distanceBetweenBuildings) + ((float)(buildingSize / 2) + ((float)(buildingSize / 2) * aStreetRatio))),
                Quaternion.identity, tempArea.transform);

                Instantiate(Crosswalk, new Vector3(
                (i * distanceBetweenBuildings) + ((float)(buildingSize / 2) + ((float)(buildingSize / 2) * aStreetRatio)), 0,
                (j * distanceBetweenBuildings) + ((float)(buildingSize / 2) + ((float)(buildingSize / 2) * aStreetRatio))),
                Quaternion.identity, tempArea.transform);
            }
        }

    }

    public void UpdateTerritory()
    {
        buildingsLeftToTake--;
        if (buildingsLeftToTake <= 0)
        {
            won = true;
        }
    }
}
