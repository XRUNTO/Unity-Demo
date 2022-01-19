using System.Collections;		//KICK YOU ASS								
using System.Collections.Generic;
using UnityEngine;													
using UnityEngine.UI;

public class 墙壁弹簧 : MonoBehaviour
{

    private GameObject 主角;
    public Vector3 跳跃方向高度坐标;
    public bool 触发了;
    //private GameObject 弹簧本体;
    public Vector3 弹簧左飞大限;

    public float 主角重力值;

    private void Start()
    {
        主角 = GameObject.Find("主角");
        弹簧左飞大限 = this.transform.position - new Vector3(0,0,5f);
        //弹簧本体 = GameObject.Find("弹簧测试")
    }
    private void FixedUpdate()
    {
        if (触发了)
        {
            主角.transform.position += 跳跃方向高度坐标;
            if (主角.transform.position.z < 弹簧左飞大限.z)
            {
                主角.GetComponent<Center控制>().跳跃重力 = 7;
                触发了 = false;
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "主角")
        {
            //主角.transform.position += 跳跃方向高度坐标;
            触发了 = true;
            主角.GetComponent<Center控制>().跳跃重力 = 主角重力值;
        }
    }
}
