using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "ScriptableObjects/ObjectToSpawn", order = 1)]
public class ObjectData : ScriptableObject
{
    public string objectName;
    public int howManyObjectsAtFirst;

    public float objectActivationDeltaTime;
    public float firstActivationTime;
    public float minSpawnRate, maxSpawnRate;
    public float spawnRiseRate;
    public float spawnRateFrameCounterLimit;
    public float spawnRateFrameCounter = 0;

    public float _minX, _maxX, _minY, _maxY;
    public float verticalSpeed;

    public Queue<GameObject> objectsToDeactivate;

    public GameObject objectToSpawn;
    public Player player;
    public MainManager manager;
    //[SerializeField]
    //private GameObject minXObject, maxXObject,
    //minYObject, maxYObject;
}
