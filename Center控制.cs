using System.Collections;		//KICK YOU ASS								
using System.Collections.Generic;
using UnityEngine;													
using UnityEngine.UI;

public class Center控制 : MonoBehaviour
{
    RaycastHit 碰撞信息;
   
    public Camera 射线摄像头;

    public AudioClip 跳跃音效;
    CharacterController 角色控制器;
    Vector3 direction;// 方向

    public float 移动速度 = 5;
    public float 跳跃速度 = 2.5f;
    public float 跳跃重力 = 7f;

    public float 鼠标移动速度 = 5f;

    public float Y旋转最小值 = -45f;
    public float Y旋转最大值 = 45f;

    float RotationY = 0f;//旋转Y
    float RotationX = 0f;//旋转X

    public Transform 摄像机;
    //public Transform 射线检测临时摄像机;
    public GameObject 生成的东西;
    public GameObject[] 装置合集;
    //public GameObject 弹射装置;
    //public GameObject 放置位置;
    float cx;
    float cy;
    float cz;
    public bool 位置存档;
    

    void Start()
    {
        角色控制器 = this.GetComponent<CharacterController>();
        //Screen.lockCursor = true;//旧版锁定光标
        Cursor.lockState = CursorLockMode.Locked;//新版锁定光标;
        Cursor.visible = false;

        cx = PlayerPrefs.GetFloat("Cx");
        cy = PlayerPrefs.GetFloat("Cy");
        cz = PlayerPrefs.GetFloat("Cz");
        if(位置存档)
        {
            this.transform.position = new Vector3(cx, cy, cz);
        }
        //this.transform.position = new Vector3(cx,cy,cz);
        
    }
    void Update()
    {
        PlayerPrefs.SetFloat("Cx", this.transform.position.x);
        PlayerPrefs.SetFloat("Cy", this.transform.position.y);
        PlayerPrefs.SetFloat("Cz", this.transform.position.z);

        float 水平输入值 = Input.GetAxis("Horizontal");//获取水平轴值
        float 垂直输入值 = Input.GetAxis("Vertical");//获取垂直轴输入值

        //跳跃
        if (角色控制器.isGrounded)//检测是否是接地状态
        {
            direction = new Vector3(水平输入值, 0, 垂直输入值);//dir被赋值为 三维坐标（水平输入，0，垂直输入）
            if (Input.GetKeyDown(KeyCode.Space))
            {
                direction.y = 跳跃速度;
                AudioSource.PlayClipAtPoint(跳跃音效, transform.localPosition);
            }
        }

        direction.y -= 跳跃重力 * Time.deltaTime;//dir的Y轴每帧 -= 重力*润滑速率

        角色控制器.Move(角色控制器.transform.TransformDirection(direction * Time.deltaTime * 移动速度));



        //X轴旋转 = 当前旋转量+指定摄像头.transform.局部欧拉角.y轴 + 鼠标输入的X轴 + 速度
        RotationX += 摄像机.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * 鼠标移动速度;


        RotationY -= Input.GetAxis("Mouse Y") * 鼠标移动速度;
        //同上

        RotationY = Mathf.Clamp(RotationY, Y旋转最小值, Y旋转最大值);
        //Y轴限制移动范围,计算平均值.


        this.transform.eulerAngles = new Vector3(0, RotationX, 0);
        //对应挂载脚本的实例.transfrom.欧拉角 = (0,旋转X,0)   鼠标移动视野，对应人物也跟着旋转视野

        摄像机.transform.eulerAngles = new Vector3(RotationY, RotationX, 0);
        //摄像机也跟着移动视野
        
        //射线检测临时摄像机.transform.eulerAngles = new Vector3(RotationY, -RotationX, 0);


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            移动速度 = 8f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            移动速度 = 2.5f;
        }



        Ray 射线 = 射线摄像头.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = LayerMask.GetMask("射线检测层A", "射线检测层B","装置检测层");
        装置合集 = GameObject.FindGameObjectsWithTag("装置");
        //int 遮罩层 = LayerMask.GetMask("检测层");
        //if (Input.GetMouseButton(0))
        if (Input.GetKeyUp(KeyCode.E))
        {//this.transform.position,new Vector3(RotationX,RotationY,0)
            if (Physics.Raycast(射线, out 碰撞信息, 20, mask))
            {               
                Vector3 碰到的点 = 碰撞信息.point;
                //Debug.DrawLine(射线.origin, 碰撞信息.point);
                Collider 碰撞体 = 碰撞信息.collider;
                //Transform trans = 碰撞信息.transform;
                
                if(碰撞体.name =="检测层A")
                {
                    foreach (GameObject respawn in 装置合集)
                    {
                        Destroy(respawn);
                    }
                    Instantiate(生成的东西, 碰撞体.transform.position + new Vector3(0, 0.05f ,0), 碰撞体.transform.rotation);
                }
                if(碰撞体.name == "检测层B")
                {
                    foreach (GameObject respawn in 装置合集)
                    {
                        Destroy(respawn);
                    }
                    Instantiate(生成的东西, 碰撞体.transform.position + new Vector3(0, 0.05f, 0), 碰撞体.transform.rotation);
                }
              
                
                /*if (碰撞体.name == "弹射装置(Clone)")
                {
                    Debug.Log("重复了，不生成");
                    foreach (GameObject respawn in 装置合集)
                    {
                        Destroy(respawn);
                    }
               
                }*/
                //Instantiate(生成的东西, 碰到的点,弹射装置.transform.rotation);
            }
        }
        

        
    }
}
