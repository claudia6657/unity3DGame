using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{

    public int point=3;

    // Update is called once per frame
    void Start()
    {
    }

    //Pick Up Balls
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.SetActive(false);
            Player_Controller.score += point;
            Player_Controller.marbleNumber -= 1;
            other.gameObject.transform.position = new Vector3(-81.0f, -10.0f, 195.8f);
            //other.gameObject.transform.position = new Vector3(0.0f, 1.0f, -20.0f);
        }

        if(other.gameObject.CompareTag("Ball"))
        {
            other.gameObject.SetActive(false);
            Player_Controller.score += 1;
            Player_Controller.pickupNum -= 1;
        }




    }
}
