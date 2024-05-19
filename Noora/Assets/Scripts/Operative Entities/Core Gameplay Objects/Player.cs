using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : OperativeEntity
{
    [SerializeField] protected float lightFactor = 1.0f;

    [SerializeField] float direction;
    [SerializeField] public float speed;

    [SerializeField] MainManager manager;
    int allyScore = 10;

    //[SerializeField] AudioSource audioSource, sidesAudio, fail, powerUpAudio;
    [SerializeField] bool isShrinkOn = false;

    [SerializeField] GameObject sides;
    [SerializeField] float sideRemainShrinkerTime;

    bool leftMouseBtn;
    bool leftArrowKey;
    bool rightArrowKey;



    protected override void Start()
    {
        base.Start();
        setVerticalSpeed(0);

    }

    private void Update()
    {
        leftMouseBtn = Input.GetKey(KeyCode.Mouse0);
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y > 1f) leftMouseBtn = false;

        leftArrowKey = Input.GetKey(KeyCode.LeftArrow);
        rightArrowKey = Input.GetKey(KeyCode.RightArrow);
        //Debug.Log(11);
        if (flagToMove && (leftArrowKey || rightArrowKey || leftMouseBtn))
        {
            movement();
            //Debug.Log(10);
        }
        else
        {
            //Debug.Log(9);
            if (leftMouseBtn || leftArrowKey || rightArrowKey)
            {
                if (rightArrowKey)
                {
                    direction = 1f;
                }
                else if (leftArrowKey)
                {
                    direction = -1f;
                }
                else if (leftMouseBtn)
                {
                    direction = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                    //if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y > 100f)
                    //{
                    //    direction = 0;
                    //}
                }
                if ((direction > 0 && transform.position.x <= 0) || (direction < 0 && transform.position.x >= 0))
                {
                    flagToMove = true;
                }
            }
        }
    }

    protected override void OnEnable() { /*base.OnEnable();*/ }

    public void movement()
    {
        if (rightArrowKey)
        {
            direction = 1f;
        }
        else if (leftArrowKey)
        {
            direction = -1f;
        }
        else if (leftMouseBtn)
        {
            //Debug.Log(" Y cam poin: " + Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        }

        //Debug.Log("dir: " + direction);

        if (direction != 0)
        {
            transform.position += ((direction / Mathf.Abs(direction)) * speed * Time.deltaTime * transform.right);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minX, _maxX), transform.position.y, 5);
        }
        else if (direction == 0)
        {
            transform.position += 0 * speed * Time.deltaTime * transform.right;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minX, _maxX), transform.position.y, 5);
        }

    }

    public bool IsShrinkOn()
    {
        return isShrinkOn;
    }
    //public void PlayerMyAudio()
    //{
    //    //audioSource.Play();
    //    AudioManager.instance.PlaySFX("PowerUP");
    //}

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //base.OnTriggerEnter2D(collision);
        playerCollidedWithSth(collision.gameObject);
    }

    public virtual void playerCollidedWithSth(GameObject collision)
    {
        if (collision.CompareTag("enemy"))
        {
            playerCollidedWithEnemy(collision);
        }
        else if (collision.CompareTag("ally"))
        {
            playerCollidedWithAlly(gameObject, collision.gameObject);
        }
        else if (collision.CompareTag("Shrinker"))
        {
            PlayerCollidedWithPUP();
        }
        else if (collision.CompareTag("Side"))
        {
            PlayerCollidedWithSide(collision);
        }
    }
    public void PlayerCollidedWithPUP()
    {
        //powerUpAudio.Play();
        AudioManager.instance.PlaySFX("PowerUp");
        StartCoroutine(RemainEffect(sideRemainShrinkerTime));
    }

    public void PlayerCollidedWithSide(GameObject collision)
    {
        manager.setScore(10);
        AudioManager.instance.PlaySFX("PowerUp", 2.3f);
        //sidesAudio.Play();

    }

    IEnumerator RemainEffect(float t)
    {
        isShrinkOn = true;
        sides.SetActive(true);
        yield return new WaitForSeconds(t);
        isShrinkOn = false;
        sides.SetActive(false);
    }

    public void playerCollidedWithAlly(GameObject allyToBeParent, GameObject allyToBeChild)
    {
        if (isShrinkOn)
        {
            allyToBeChild.transform.SetParent(allyToBeParent.transform);
        }
        else
        {
            allyToBeChild.transform.SetParent(gameObject.transform);
        }


        allyToBeChild.GetComponent<OperativeEntity>().DontMove();
        allyToBeChild.GetComponent<ally>().GetComponent<Image>().color = new Color(255, 255, 255, 100);
        manager.setScore(allyScore);
        allyToBeChild.gameObject.tag = "player";

        allyToBeChild.GetComponent<ally>().activateTheLight();
        //audioSource.Play();
        AudioManager.instance.PlaySFX("CollectAlly");
    }

    public void playerCollidedWithEnemy(GameObject enemy)
    {
        DontMove();
        enemy.GetComponent<enemy>().DontMove();
        //fail.Play();
        AudioManager.instance.PlaySFX("Hit");
        StartCoroutine(justWait());
        manager.Lose();
    }

    IEnumerator justWait()
    {
        yield return new WaitForSeconds(1f);
    }
}
