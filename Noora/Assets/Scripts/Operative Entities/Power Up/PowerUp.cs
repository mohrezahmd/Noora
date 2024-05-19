using UnityEngine;

public class PowerUp : OperativeEntity
{
    private GameObject _manager;

    // Update is called once per frame
    void Update()
    {

        if (CompareTag("Shrinker") && gameObject.transform.position.y < _minY)
        {
            Destroy(gameObject, .1f);
        }
    }

    protected override void OnEnable()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        rgbd2D.velocity = new Vector2(0, -verticalSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Shrinker") && collision.gameObject.CompareTag("player"))
        {
            Destroy(gameObject, .1f);
            _player.GetComponent<Player>().PlayerCollidedWithPUP();
        }
        else if (CompareTag("Side") && collision.gameObject.CompareTag("player"))
        {
            _player.GetComponent<Player>().PlayerCollidedWithSide(collision.gameObject);
        }
    }

    // SPAWNED OBJECTS METHODS
    public override void setRelatives(GameObject myPlayer, GameObject myManager)
    {
        if (myPlayer.tag == "player") _player = myPlayer;
        if (myManager.tag == "manager") _manager = myManager;
    }

    // SPAWNED OBJECTS METHODS

}
