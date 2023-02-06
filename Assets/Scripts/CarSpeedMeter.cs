using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSpeedMeter : MonoBehaviour
{
    public Rigidbody target;
    public float maxSpeed=0.0f;
    
    public float speedArrowMinAngle;
    public float speedArrowMaxAngle;

    [Header("UI")]
    public Text speedLabel;
    public RectTransform arrow;

    public void Update(){
        speed=target.velocity.magnitude*3.6f;

        if (speedLabel!=null){
            speedLabel.text=((int)speed)+"km/h";
        }

        if(arrow!=null){
            arrow.localEulerAngles=new Vector3(0,0,Mathf.Lerp(speedArrowMinAngle,speedArrowMaxAngle,speed/maxSpeed));
                     }
    }


}
