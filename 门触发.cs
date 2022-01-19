using System.Collections;		//KICK YOU ASS								
using System.Collections.Generic;
using UnityEngine;													
using UnityEngine.UI;

public class 门触发 : MonoBehaviour
{
    public GameObject 门左;
    public GameObject 门右;
    public bool 触发了;
    public bool 未到达了;
    public Vector3 左移动方向;
    public Vector3 右移动方向;
    private void Awake()
    {
        触发了 = false;
        未到达了 = true;
    }
    private void Update()
    {
        if (触发了 && 未到达了)
        {
            
            if (门左.transform.position.x > 0)
            {
                触发了 = false;
                未到达了 = false;
            }
            门左.transform.position += 左移动方向;
            门右.transform.position += 右移动方向;


        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "主角")
        {
            触发了 = true;
        }

    }
}
