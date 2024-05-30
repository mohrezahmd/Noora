using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ally : OperativeEntity
{
    int selfCollisionCounter = 0;
    bool notReachedBorder = true;

    [SerializeField] public GameObject myLight;

    protected override void Start()
    {
        base.Start();
        rgbd2D.gravityScale = 0;
    }

    protected override void OnEnable()
    {
        //lightFactor = 0;
        base.OnEnable();

        if (_maxY > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, _maxY);
        }

    }

    protected override void FixedUpdate()
    {
        base.Start ();
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 34f);
        if (transform.parent.CompareTag("player"))
        {
            if ((transform.position.x >= _maxX || transform.position.x <= _minX) && notReachedBorder)
            {
                notReachedBorder = false;
                if (!player.IsShrinkOn())
                {
                    transform.parent.gameObject.GetComponent<OperativeEntity>().DontMove();
                }
            }
            else if (!(transform.position.x >= _maxX) && !(transform.position.x <= _minX))
            {
                notReachedBorder = true;
            }

        }

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, _minX, _maxX), transform.position.y);

        if (transform.position.y < _minY)
        {
            MainManager.instance.CallToSpawner(gameObject);
        }
    }

    public override void setRelatives(GameObject myPlayer, GameObject myManager)
    {
        if (myPlayer.tag == "player")
        {
            player = myPlayer.GetComponent<Player>();
            mainManager = myManager.GetComponent<MainManager>();
            _Manager = myManager;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        playerCollidedWithSth(collision.gameObject);
        //base.OnTriggerEnter2D(collision);
    }

    public void playerCollidedWithSth(GameObject collision)
    {
        if (collision.CompareTag("enemy") && transform.parent != null && transform.parent.CompareTag("player"))
        {
            player.playerCollidedWithEnemy(collision);
        }
        else if (collision.CompareTag("ally") && transform.parent != null && transform.parent.CompareTag("player"))
        {
            //player.PlayerMyAudio();
            player.playerCollidedWithAlly(gameObject, collision.gameObject);
            selfCollisionCounter++;
        }
        else if (collision.CompareTag("player") && selfCollisionCounter == 1)
        {
            mainManager.setScore(10);
            //player.PlayerMyAudio();
            selfCollisionCounter++;
        }
        else if (collision.CompareTag("Side"))
        {
            //player.PlayerMyAudio();

            player.PlayerCollidedWithSide(collision);
        }

    }

    public void activateTheLight()
    {
        myLight.SetActive(true);
    }
}

