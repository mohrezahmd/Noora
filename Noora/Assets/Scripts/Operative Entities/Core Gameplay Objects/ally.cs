using UnityEngine;
using UnityEngine.UI;
public enum borderSide { Left, Right, Above, Bellow, None }

public class ally : OperativeEntity
{
    int selfCollisionCounter = 0;
    bool notReachedBorder = true;
    public int allyID = 0;

    [SerializeField] public GameObject myLight;

    public borderSide borderSide;

    protected override void Start()
    {
        base.Start();
        rgbd2D.gravityScale = 0;
        borderSide = borderSide.None;
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

    private void FixedUpdate()
    {
        if (player.IsShrinkOn())
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, _minX, _maxX), transform.position.y);
        }

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
            MainManager.instance.setScore(10);
            //player.PlayerMyAudio();
            selfCollisionCounter++;
        }
        else if (collision.CompareTag("Side") && transform.parent != null )
        {
            //player.PlayerMyAudio();
            player.playerCollidedWithSth(collision);
        }
    }

    public void activateTheLight()
    {
        myLight.SetActive(true);
    }
}

