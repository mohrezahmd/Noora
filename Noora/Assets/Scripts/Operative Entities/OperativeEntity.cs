using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OperativeEntity : MonoBehaviour
{
    [SerializeField] protected float influenceFactor = 0f;
    [SerializeField] protected float collisionNubmer;
    public List<UnityEvent> onEntityCollision;

    [SerializeField] protected GameObject _Manager, _player;
    protected Player player;
    protected MainManager mainManager;
    protected Rigidbody2D rgbd2D;
    public float _maxX, _minX, _minY, _maxY;
    [SerializeField] protected float verticalSpeed = 2f;
    [SerializeField] protected GameObject bordersContainer;
    protected Border[] borders;

    [SerializeField] protected bool flagIsActive = true;
    [SerializeField] protected bool flagToMove = true;


    protected virtual void Start()
    {      
        rgbd2D = GetComponent<Rigidbody2D>();
        if (gameObject.tag != "player")
        {
            verticalSpeed = MainManager.instance.CallToAssignerForData(gameObject.tag);
        }
        if (bordersContainer != null)
        {
            borders = bordersContainer.GetComponentsInChildren<Border>();
            _minX = borders[0].transform.position.x;
            _maxX = borders[1].transform.position.x;
            _minY = borders[2].transform.position.y;
            _maxY = borders[3].transform.position.y;
        }
    }

    protected virtual void OnEnable()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        rgbd2D.velocity = new Vector2(0, -verticalSpeed);

    }

    protected virtual void FixedUpdate()
    {

    }

    public virtual void setLimits(float minX, float maxX, float minY, float maxY)
    {
        _minX = minX; _maxX = maxX; _minY = minY; _maxY = maxY;
    }

    public virtual void setRelatives(GameObject myPlayer, GameObject myManager)
    {
        _player = myPlayer;
        _Manager = myManager;
    }

    public void setVerticalSpeed(float vSpeed)
    {
        verticalSpeed = vSpeed;
    }

    public float GetVerticalSpeed()
    {
        return verticalSpeed;
    }

    public void DontMove()
    {
        flagToMove = false;
        SetSpeed(0, 0);
    }

    public virtual void SetSpeed(float x, float y)
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        rgbd2D.velocity = new Vector2(x, y);
    }

}
