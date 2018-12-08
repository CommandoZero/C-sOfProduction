using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{

    [SerializeField]
    int minValue = 0;
    [SerializeField]
    int maxValue = 0;

    int value = 0;

    bool owned = false;

    public void TakeBuilding()
    {
        owned = true;
        CitySpawnSystem.Instance.UpdateTerritory();
    }

    public void GetResource()
    {
        //Add value to player resource (health, money, ammo, etc)
        if (type == BuildingType.Bank)
        {
            //Add value to money
        }
        else if (type == BuildingType.Armory)
        {
            //Add value to ammo
        }
        else if (type == BuildingType.Store)
        {
            //Add value to health
        }
        else if (type == BuildingType.Warehouse)
        {
            //Add items?
        }
    }
    //The type of building
    enum BuildingType
    {
        Bank = 0,
        Store = 1,
        Armory = 2,
        Warehouse = 3
    }
    //Local enum
    [SerializeField]
    BuildingType type;
    //Is called when object spawns
    public void SetBuildingType(int aType)
    {
        type = (BuildingType)aType;
        value = Random.Range(minValue, maxValue);
    }

    Transform[] enemySpawnTransforms = new Transform[] { };

    public List<GameObject> enemies = new List<GameObject>();

    public void SpawnEnemies(GameObject aEnemy)
    {
        for (int i = 0; i < enemySpawnTransforms.Length; i++)
        {
            GameObject tempEnemy = Instantiate(aEnemy, enemySpawnTransforms[i].position, Quaternion.identity);
            enemies.Add(tempEnemy);
        }
    }


    public int numberOfEnemies = 0;

    public void UpdateAmountOfEnemies(int anAmount)
    {
        numberOfEnemies = anAmount;
        if (anAmount == 0)
        {
            TakeBuilding();
        }
    }
}
