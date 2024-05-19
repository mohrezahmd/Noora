using UnityEngine;

public class Border : MonoBehaviour
{
    //public static Border instance;
    public float myX, myY;
    // Start is called before the first frame update
    private void Awake()
    {
        Border[] tempLen = gameObject.GetComponents<Border>();
        if ( tempLen.Length> 1)
        {
            Destroy(tempLen[1], .2f );
        }
    }

    void Start()
    {
        myX = gameObject.transform.position.x;
        myY = gameObject.transform.position.y;
    }

}
