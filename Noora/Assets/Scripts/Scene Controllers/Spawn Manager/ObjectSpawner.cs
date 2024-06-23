using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] bool readDataFromSO = false;
    [SerializeField] public string objectName;

    [SerializeField] public ObjectData objectData;
    [SerializeField] int howManyObjectsAtFirst;
    [SerializeField] float objectActivationDeltaTime;
    [SerializeField] float firstActivationTime;
    [SerializeField] float minSpawnRate, maxSpawnRate;
    [SerializeField] float spawnRiseRate;
    [SerializeField] float spawnRateFrameCounterLimit;
    [SerializeField] float spawnRateFrameCounter = 0;

    /*public*/
    float _minX, _maxX, _minY, _maxY;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private Vector3 toSpawnLocalScale;

    Queue<GameObject> objectsToDeactivate;

    [SerializeField] GameObject borderContainer;
    Border[] borders;
    [SerializeField] Player player;
    SpawnManager spawnManager;
    [SerializeField] Canvas parentCanvas;
    [SerializeField] private GameObject objectToSpawn;


    private void Start()
    {
        if (readDataFromSO)
        {
            ReadDataFromSO();
        }

        spawnManager = transform.GetComponentInParent<SpawnManager>();
        objectName = objectToSpawn.tag;

        borders = borderContainer.GetComponentsInChildren<Border>();
        _minX = borders[0].gameObject.transform.position.x;
        _maxX = borders[1].gameObject.transform.position.x;
        _minY = borders[2].gameObject.transform.position.y;
        _maxY = borders[3].gameObject.transform.position.y;

        objectsToDeactivate = new Queue<GameObject>();
        for (int i = 0; i < howManyObjectsAtFirst; i++)
        {
            SpawnObject(objectToSpawn);
        }
        Invoke("activateObject", firstActivationTime);
    }

    private void FixedUpdate()
    {
        if (spawnRateFrameCounter >= spawnRateFrameCounterLimit)
        {
            objectActivationDeltaTime += spawnRiseRate;
            objectActivationDeltaTime = Mathf.Clamp(objectActivationDeltaTime, minSpawnRate, maxSpawnRate);
            spawnRateFrameCounter = 0;
        }
        else
        {
            spawnRateFrameCounter++;
        }
    }

    void ReadDataFromSO()
    {
        howManyObjectsAtFirst = objectData.howManyObjectsAtFirst;
        objectActivationDeltaTime = objectData.objectActivationDeltaTime;
        firstActivationTime = objectData.firstActivationTime;
        minSpawnRate = objectData.minSpawnRate;
        maxSpawnRate = objectData.maxSpawnRate;
        spawnRiseRate = objectData.spawnRiseRate;
        spawnRateFrameCounterLimit = objectData.spawnRateFrameCounterLimit;
        spawnRateFrameCounter = 0;
        verticalSpeed = objectData.verticalSpeed;
        objectsToDeactivate = new Queue<GameObject>();
        objectToSpawn = objectData.objectToSpawn;
        Player player = objectData.player;
        MainManager manager = objectData.manager;
    }

    void activateObject()
    {
        if (objectsToDeactivate != null)
        {
            if (objectsToDeactivate.Count != 0)
            {
                GameObject tempAlly = objectsToDeactivate.Dequeue();
                tempAlly.SetActive(true);
                initializeObject(tempAlly.GetComponent<OperativeEntity>());
            }
            else if (objectsToDeactivate.Count == 0)
            {
                for (int i = 0; i < howManyObjectsAtFirst; i++)
                {
                    SpawnObject(objectToSpawn);
                }
            }
        }
        Invoke("activateObject", objectActivationDeltaTime);
    }

    private void initializeObject(OperativeEntity toSpawn)
    {
        toSpawn.GetComponent<Image>().SetNativeSize();
        toSpawn.GetComponent<Transform>().SetParent(spawnManager.gameObject.transform);
        toSpawn.GetComponent<Transform>().position = new Vector2(Random.Range(_minX, _maxX), _maxY);
        toSpawn.setLimits(_minX, _maxX, _minY, _maxY);
        toSpawn.setRelatives(player.gameObject, spawnManager.gameObject);

        CallManagerForSpeedData(objectName);
        toSpawn.setVerticalSpeed(verticalSpeed);
        toSpawn.GetComponent<RectTransform>().localScale = toSpawnLocalScale;
    }

    GameObject SpawnObject(GameObject toSpawnObject)
    {
        Vector2 setPoint = new Vector2(Random.Range(_minX, _maxX), _maxY);

        GameObject toSpawnObjectInstance = Instantiate(toSpawnObject, setPoint, Quaternion.identity);
        initializeObject(toSpawnObjectInstance.GetComponent<OperativeEntity>());
        if (toSpawnObjectInstance.tag == "ally")
        {
            toSpawnObjectInstance.GetComponent<ally>().allyID = MainManager.instance.allyID;
            toSpawnObjectInstance.GetComponent<ally>().borderSide = borderSide.None;
            MainManager.instance.allyID += 1;
        }

        PushToDeactivate(toSpawnObjectInstance);
        return toSpawnObjectInstance;
    }

    public void PushToDeactivate(GameObject objectToDeactivate)
    {
        if (objectsToDeactivate == null) Debug.Log(true);
        objectsToDeactivate.Enqueue(objectToDeactivate);
        objectToDeactivate.SetActive(false);
    }

    public void CallManagerForSpeedData(string assignedObjTag)
    {
        //this.verticalSpeed = verticalSpeed;
        verticalSpeed = MainManager.instance.CallForData(assignedObjTag);
    }
}