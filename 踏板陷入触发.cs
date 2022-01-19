using System.Collections;		//KICK YOU ASS								
using System.Collections.Generic;
using UnityEngine;													
using UnityEngine.UI;

public class 踏板陷入触发 : MonoBehaviour
{
    public GameObject 踏板;
    public Vector3 踏板下沉指定坐标;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "主角")
        {
            踏板.transform.position = 踏板下沉指定坐标;
        }
    }


}
