using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySpawnSystem : MonoBehaviour
{

    public CitySpawnSystem Instance;

    [SerializeField]
    float maxBuildingSize = 0;
    [SerializeField, Range(0f, 5f)]
    float buildingToStreetRatio = 1;
    [SerializeField]
    int buildingAmount = 0;
    [SerializeField]
    Vector2 cityGrid;

    [SerializeField]
    GameObject streetObject;
    [SerializeField]
    GameObject streetObject2;
    [SerializeField]
    GameObject streetObject3;


    [SerializeField]
    List<GameObject> buildingObjects = new List<GameObject>();

    Dictionary<int, GameObject> building = new Dictionary<int, GameObject>();


    private void Start()
    {
        if (Instance == null)
            Instance = this;
        for (int i = 0; i < buildingObjects.Count; i++)
        {
            building.Add(i, buildingObjects[i]);
        }

        SpawnBuildings(maxBuildingSize, buildingToStreetRatio);
    }

    public void SpawnBuildings(float buildingSize, float aStreetRatio)
    {

        float distanceBetweenBuildings = buildingSize + (buildingSize * aStreetRatio);
        for (int i = 0; i < cityGrid.x; i++)
        {
            for (int j = 0; j < cityGrid.y; j++)
            {
                int buildingtype = Random.Range(0, buildingObjects.Count);

                GameObject tempArea = Instantiate(buildingObjects[buildingtype], new Vector3
                     (i * distanceBetweenBuildings, 0,
                      j * distanceBetweenBuildings), Quaternion.identity, gameObject.transform);

                Instantiate(streetObject, new Vector3(
                (i * distanceBetweenBuildings) + ((float)(buildingSize / 2) + ((float)(buildingSize / 2) * aStreetRatio)), 0,
                 tempArea.transform.position.z), Quaternion.identity, tempArea.transform);

                Instantiate(streetObject2, new Vector3(tempArea.transform.position.x, 0,
                (j * distanceBetweenBuildings) + ((float)(buildingSize / 2) + ((float)(buildingSize / 2) * aStreetRatio))),
                Quaternion.Euler(new Vector3(0, 90, 0)), tempArea.transform);

                Instantiate(streetObject3, new Vector3(
                    (i * distanceBetweenBuildings) + ((float)(buildingSize / 2) + ((float)(buildingSize / 2) * aStreetRatio)), 0,
                     (j * distanceBetweenBuildings) + ((float)(buildingSize / 2) + ((float)(buildingSize / 2) * aStreetRatio))

                    ), Quaternion.identity, tempArea.transform);
            }
        }

    }
}
