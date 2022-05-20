using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;//攝影機 離 母球 相對位置   

    public float speed = 12.0f; //母球 初速
    public float forceSpd = 9.0f; //母球 蓄力 速度
    public float force = 0.0f; //母球 已經蓄了多少力 的大小
    public float distance = 6.0f; //攝影機 離 母球 距離 初始值    
    public float xSpeed = 120.0f; //滑鼠左右移動速度    
    public float ySpeed = 120.0f; //滑鼠上下移動速度    
    public float yMinLimit = -20f;  //滑鼠上下 轉仰角 下限    
    public float yMaxLimit = 80f;   //滑鼠上下 轉仰角 上限    
    public float distanceMin = .5f;  //滾輪 拉 攝影機 離 母球 距離下限    
    public float distanceMax = 15f;  //滾輪 拉 攝影機 離 母球 距離上限    
    private Rigidbody rbody;    
    private circleFillHandler circlefillhandler;
    
    float x = 0.0f;    
    float y = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;        
        Vector3 angles = transform.eulerAngles; //攝影機角度        
        x = angles.y;        
        y = angles.x;        
        rbody = player.GetComponent<Rigidbody>();    
    }

    void Awake(){
		circlefillhandler = GameObject.FindObjectOfType<circleFillHandler> ();
	}

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;

        if (Input.GetMouseButton(0))  //按住時 才有作用
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            y = ClampAngle(y, yMinLimit, yMaxLimit);  //限制 仰角傾仰範圍//繞 Y軸 是 繞球轉，繞X軸 是 傾仰
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            distance = Mathf.Clamp( // 限制 滾輪 拉 遠近 移動範圍
            distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);// (沿Z軸 前後移動）5101520253035404550
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);        
            offset = rotation * negDistance; //依新角度，距離 重新算相對位置
            transform.rotation = rotation; // 攝影機 新角度        
        } // 攝影機新位置 = 新相對位置 + 母球位置     

        transform.position = player.transform.position + offset;     
           
        if (Input.GetMouseButton(1))  // 按滑鼠右鍵 按住 蓄力        
        {
            if(force < 25.0)
            {
                force += Time.deltaTime*forceSpd; // 大小和時間成正比    
                circlefillhandler.FillCircleValue (force);       
            }
        } else if (Input.GetMouseButtonUp(1))  // 按滑鼠右鍵 放開 發射        
        {//眼睛看的方向the direction of camera(eye)： Camera.main.transform.forward
            Vector3 movement = Camera.main.transform.forward;
            movement.y = 0.0f;      // no vertical movement 不上下移動//力量模式impulse:衝力，speed：初速大小
            rbody.AddForce(movement * speed * force, ForceMode.Impulse);
            force = 0.0f;  // 力量用盡歸0，準備下次重新蓄力
        }
    }

    public static float ClampAngle(float angle, float min, float max)    
    { // 用上下限 夾值        
        if (angle < -360F) angle += 360F;        
        if (angle > 360F) angle -= 360F;        
            
        return Mathf.Clamp(angle, min, max);    
    }
}
