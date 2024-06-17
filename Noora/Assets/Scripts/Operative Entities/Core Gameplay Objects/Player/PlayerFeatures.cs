using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeatures : MonoBehaviour
{
// {    if (Input.GetKeyDown(KeyCode.A))
//        {
//            if (leftAlly != null)
//            {
//                ally _leftAlly = leftAlly.GetComponent<ally>();
//    Debug.Log("left ally: " + _leftAlly.allyID + " , " + _leftAlly.borderSide);
//            }
//            else
//{
//    Debug.Log("there is no left!");
//}
//        }

//        if (Input.GetKeyDown(KeyCode.D))
//{
//    if (rightAlly != null)
//    {
//        ally _rightAlly = rightAlly.GetComponent<ally>();
//        Debug.Log("right ally: " + _rightAlly.allyID + " , " + _rightAlly.borderSide);
//    }
//    else
//    {
//        Debug.Log("there is no right!");

//    }
//}

//if (Input.GetKeyDown(KeyCode.W))
//{
//    if (aboveAlly != null)
//    {
//        ally _aboveAlly = aboveAlly.GetComponent<ally>();
//        Debug.Log("above ally: " + _aboveAlly.allyID + " , " + _aboveAlly.borderSide);
//    }
//    else
//    {
//        Debug.Log("there is no above!");
//    }
//}

//if (Input.GetKeyDown(KeyCode.S))
//{
//    if (bellowAlly != null)
//    {
//        ally _bellowAlly = bellowAlly.GetComponent<ally>();
//        Debug.Log("bellow ally: " + _bellowAlly.allyID + " , " + _bellowAlly.borderSide);
//    }
//    else
//    {
//        Debug.Log("there is no bellow!");
//    }
//}

    //public void NextBorderAlly(GameObject nextAlly, GameObject lastAlly)
    //{
    //    float distance1 = -1, distance2;
    //    distance2 = Vector2.Distance(nextAlly.transform.position, transform.position);

    //    if (lastAlly != null)
    //    {

    //        distance1 = Vector2.Distance(lastAlly.transform.position, transform.position);

    //        if (distance1 < distance2)
    //        {
    //            lastAlly.GetComponent<Image>().color = Color.white;
    //            lastAlly = nextAlly;
    //            lastAlly.GetComponent<Image>().color = Color.red;
    //        }
    //    }
    //    else
    //    {
    //        lastAlly = nextAlly;
    //        lastAlly.GetComponent<Image>().color = Color.white;
    //    }
    //}



    //public void SetBorderAlly(GameObject nextAlly)
    //{

    //}

    //public void SetBorderAlly(GameObject allyToBeChild)
    //{
    //    ///// |||||||||||||||||||||||||||||||  ||||||||||||||| X Axis |||||||||||||||||||||||||||||||||||||||||||||||||||||\\\\\
    //    float allyToBeChildPosX = allyToBeChild.transform.position.x;
    //    float PlayerPosX = transform.position.x;
    //    float leftAllyPosX;
    //    if (leftAlly != null) leftAllyPosX = leftAlly.transform.position.x;
    //    //======================================left================================================================================
    //    if (leftAlly != null)
    //    {
    //        leftAllyPosX = leftAlly.transform.position.x;
    //        if (allyToBeChildPosX < leftAllyPosX)
    //        {
    //            leftAlly.GetComponent<Image>().color = Color.white;
    //            leftAlly.GetComponent<ally>().borderSide = borderSide.None;
    //            leftAlly = allyToBeChild;
    //            leftAlly.GetComponent<ally>().borderSide = borderSide.Left;
    //            leftAlly.GetComponent<Image>().color = Color.red;
    //        }
    //    }
    //    else
    //    {
    //        if (allyToBeChildPosX < PlayerPosX)
    //        {
    //            leftAlly = allyToBeChild;
    //            leftAlly.GetComponent<ally>().borderSide = borderSide.Left;
    //            leftAlly.GetComponent<Image>().color = Color.red;
    //        }
    //    }
    //    /////====================================right===============================================================================
    //    float rightAllyPosX;
    //    if (rightAlly != null)
    //    {
    //        rightAllyPosX = rightAlly.transform.position.x;
    //        if (allyToBeChildPosX > rightAllyPosX)
    //        {
    //            rightAlly.GetComponent<Image>().color = Color.white;
    //            rightAlly.GetComponent<ally>().borderSide = borderSide.None;
    //            rightAlly = allyToBeChild;
    //            rightAlly.GetComponent<ally>().borderSide = borderSide.Right;
    //            rightAlly.GetComponent<Image>().color = Color.red;
    //        }
    //    }
    //    else
    //    {
    //        if (allyToBeChildPosX > PlayerPosX)
    //        {
    //            rightAlly = allyToBeChild;
    //            rightAlly.GetComponent<ally>().borderSide = borderSide.Right;
    //            rightAlly.GetComponent<Image>().color = Color.red;
    //        }
    //    }


    //    ///// |||||||||||||||||||||||||||||||||||||||||||||| Y Axis |||||||||||||||||||||||||||||||||||||||||||||||||||||\\\\\
    //    float allyToBeChildPosY = allyToBeChild.transform.position.y;
    //    float PlayerPosY = transform.position.y;
    //    float aboveAllyPosY;
    //    //===============================================above==============================================================
    //    if (aboveAlly != null)
    //    {
    //        aboveAllyPosY = aboveAlly.transform.position.y;
    //        if (allyToBeChildPosY > aboveAllyPosY)
    //        {
    //            aboveAlly.GetComponent<Image>().color = Color.white;
    //            aboveAlly.GetComponent<ally>().borderSide = borderSide.None;
    //            aboveAlly = allyToBeChild;
    //            aboveAlly.GetComponent<ally>().borderSide = borderSide.Above;
    //            aboveAlly.GetComponent<Image>().color = Color.cyan;
    //        }
    //    }
    //    else
    //    {
    //        if (allyToBeChildPosY > PlayerPosY)
    //        {
    //            aboveAlly = allyToBeChild;
    //            aboveAlly.GetComponent<ally>().borderSide = borderSide.Above;
    //            aboveAlly.GetComponent<Image>().color = Color.cyan;

    //        }
    //    }
    //    //====================================================bellow==========================================================
    //    float bellowAllyPosY;
    //    if (bellowAlly != null)
    //    {
    //        bellowAllyPosY = bellowAlly.transform.position.y;
    //        if (allyToBeChildPosY < bellowAllyPosY)
    //        {
    //            bellowAlly.GetComponent<Image>().color = Color.white;
    //            bellowAlly.GetComponent<ally>().borderSide = borderSide.None;
    //            bellowAlly = allyToBeChild;
    //            bellowAlly.GetComponent<ally>().borderSide = borderSide.Bellow;
    //            bellowAlly.GetComponent<Image>().color = Color.cyan;

    //        }
    //    }
    //    else
    //    {
    //        if (allyToBeChildPosY < PlayerPosY)
    //        {
    //            bellowAlly = allyToBeChild;
    //            bellowAlly.GetComponent<ally>().borderSide = borderSide.Bellow;
    //            bellowAlly.GetComponent<Image>().color = Color.cyan;

    //        }
    //    }

    //}

}
