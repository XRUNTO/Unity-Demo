using System.Collections;		//KICK YOU ASS								
using System.Collections.Generic;
using UnityEngine;													
using UnityEngine.UI;

public class 电梯触发2 : MonoBehaviour
{  
    public GameObject 电梯;
    //public Vector3 电梯终点;
    public float 电梯Y高度;
    public float 电梯上升速度;

    private bool 未到达终点;
    private bool 触发了;
    private void Awake()
    {
        未到达终点 = true;
        触发了 = false;
    }
    private void FixedUpdate()
    {
        if (触发了 && 未到达终点)
        {
            if (电梯.transform.position.y > 电梯Y高度)
            {
                未到达终点 = false;
            }
            else
            {
                电梯.transform.position += new Vector3(0, 电梯上升速度, 0);
            }
        }
    }
    
            
    
    /*private void OnTriggerStay(Collider other)
    {
        //未到达终点 = true;
        if (other.name == "主角" && 未到达终点)
        {
            if (电梯.transform.position.y > 电梯终点.y)
            {
                未到达终点 = false;
            }
            else
            {
                电梯.transform.position += new Vector3(0, 0.03f, 0);
            }
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "主角" && 未到达终点)
        {
            触发了 = true;
        }
    }
}


