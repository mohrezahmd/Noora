using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCollider : MonoBehaviour //PowerUp
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject player, manager;
    [SerializeField] GameObject sideLight;
    [SerializeField] GameObject[] sideLightsPlus1;

    private void Start()
    {
        //StartCoroutine(FlipLight());
    }

    protected void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        //rgbd2D = GetComponent<Rigidbody2D>();
        //rgbd2D.velocity = new Vector2(0, -verticalSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            //AudioManager.instance.PlaySFX("CollectAlly");
            //if(!(audioSource.pitch < 2f)) audioSource.pitch = 1.5f;
            audioSource.Play();
            //Debug.Log("1.pitch: " + audioSource.pitch);

        }
    }

    //public IEnumerator FlipLight()
    //{
    //    Debug.Log("flip");
    //    sideLight.transform.Rotate(new Vector3(0, 0,180));
    //    yield return new WaitForSeconds(.1f);
    //    StartCoroutine(FlipLight());
    //}

}