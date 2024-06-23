using UnityEngine;

public class enemy : OperativeEntity
{
    protected override void Start()
    {
        base.Start();
        GetComponent<RectTransform>().sizeDelta.Set(50, 100);
        rgbd2D.gravityScale = 0;
    }

    protected void Update()
    {
        GetComponent<RectTransform>().sizeDelta.Set(50, 100);

        if (transform.position.y < _minY)
        {
            MainManager.instance.CallToSpawner(gameObject);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }


    // SPAWNED OBJECTS METHODS
    public override void setRelatives(GameObject myPlayer, GameObject myManager)
    {
        _player = myPlayer;
        _Manager = myManager;
    }

    // SPAWNED OBJECTS METHODS    

}
