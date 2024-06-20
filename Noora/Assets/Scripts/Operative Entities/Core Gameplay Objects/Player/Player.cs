using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : OperativeEntity
{
    [SerializeField] protected float lightFactor = 1.0f;

    [SerializeField] float direction;
    [SerializeField] public float speed;

    [SerializeField] MainManager manager;
    int allyScore = 10;

    [SerializeField] AudioSource audioSource, sidesAudio, fail, powerUpAudio;
    [SerializeField] bool isShrinkOn = false;

    [SerializeField] GameObject sides;
    [SerializeField] float sideRemainShrinkerTime;

    bool leftMouseBtn;
    bool leftArrowKey;
    bool rightArrowKey;

    float pitchValue = 0f;
    int pitchCounter = 0;
    [SerializeField] float pitchValueDelta;

    bool flagToMoveRight = true;
    bool flagToMoveLeft = true;

    protected override void Start()
    {
        base.Start();
        setVerticalSpeed(0);
    }

    public bool leftArrowEnable = true;
    public bool rightArrowEnable = true;

    private void Update()
    {
        GetInput();
    }

    public void GetInput()
    {
        leftMouseBtn = Input.GetKey(KeyCode.Mouse0);
        Vector3 leftMouseBtnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (leftMouseBtnPos.y > 1f) leftMouseBtn = false;

        leftArrowKey = (Input.GetKey(KeyCode.LeftArrow) || (leftMouseBtn && (leftMouseBtnPos.x < 0))) && flagToMoveLeft;
        rightArrowKey = (Input.GetKey(KeyCode.RightArrow) || (leftMouseBtn && (leftMouseBtnPos.x > 0))) && flagToMoveRight;

        if (leftArrowKey || rightArrowKey)
        {
            Movement(rightArrowKey, leftArrowKey);
        }
    }

    public void Movement(bool _rightKey, bool _leftKey)
    {
        int _direction = 0;

        if (_rightKey)
        {
            _direction = 1;
            flagToMoveLeft = true;
        }
        else if (_leftKey)
        {
            _direction = -1;
            flagToMoveRight = true;
        }

        transform.position += _direction * speed * Time.deltaTime * transform.right;
        if (transform.position.x >= _maxX)
        {
            flagToMoveRight = false;
        }
        else if (transform.position.x <= _minX)
        {
            flagToMoveLeft = false;
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minX, _maxX), transform.position.y, 5);
    }

    public bool IsShrinkOn()
    {
        return isShrinkOn;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
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
            playerCollidedWithAlly(gameObject, collision);
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

    public void PlayerCollidedWithSide(GameObject ally)
    {
        if (transform.position.x > 0)
        {
            ToMove(false, true);
        }
        else if (transform.position.x <= 0)
        {
            ToMove(true, false);
        }
        if (isShrinkOn)
        {
            MainManager.instance.setScore(10);
        }

        //AudioManager.instance.PlaySFX("PowerUp");
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
        allyToBeChild.transform.SetParent(gameObject.transform);
        allyToBeChild.GetComponent<ally>().GetComponent<Image>().color = new Color(255, 255, 255, 100);

        allyToBeChild.GetComponent<OperativeEntity>().DontMove();
        manager.setScore(allyScore);
        allyToBeChild.gameObject.tag = "player";

        allyToBeChild.GetComponent<ally>().activateTheLight();
        AudioManager.instance.PlaySFX("CollectAlly");

    }

    public void playerCollidedWithEnemy(GameObject enemy)
    {
        DontMove();
        enemy.GetComponent<enemy>().DontMove();
        AudioManager.instance.PlaySFX("Hit");
        manager.Lose();
    }

    public void ToMove(bool _flagToRight, bool _flagToLeft)
    {
        flagToMoveRight = _flagToRight;
        flagToMoveLeft = _flagToLeft;
    }

    public override void DontMove()
    {
        flagToMoveLeft = false;
        flagToMoveRight = false;
    }
}
