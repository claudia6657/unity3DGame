using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Player_Controller : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;

    static public int score;
    static public int pickupNum = 10;
    //marble
    static public int marbleNumber = 3;


    private Rigidbody rb;
    private float height;
    private float staticheight;
    private int roundStop = 0;
    private int oneshot = 9;

    //public GameObject[] bowlings;

    //Vector3 zeroPos = new Vector3(0.0f, 1.22f, 0.0f);


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();     
        score = 0;
        //bowlings = GameObject.FindGameObjectsWithTag("Add");
        SetCountText ();
    }

    void Update()
    {
        /*
        if(roundStop > 0)
        {
            Invoke("IsBowlingDown", 3);
        }*/

        if (Input.GetKeyDown("space"))
        {
            Vector3 movement = new Vector3(0.0f, 0.0f, 0.0f);
            rb.AddForce(movement * speed);
            //roundStop = 0;
            marbleNumber = 3;
            score = 0;
            winText.text = "";
            if(oneshot <= 0)
            {
                score = 0;
            }
            oneshot = 9;
            Debug.Log("Space!");
        }
        
        //SetCountText ();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
        SetCountText ();
    }

    //Pick Up cubes
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            score = score + 1;
            SetCountText ();
        }

        if(other.gameObject.CompareTag("Hole"))
        {
            //score = score - 1;
            Vector3 movement = new Vector3(-81.0f, -10.0f, 195.8f);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            //SetCountText ();
        }

        if(other.gameObject.CompareTag("Minus"))
        {
            score = score - 1;
        }
        if(other.gameObject.CompareTag("Plane"))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero; 
            roundStop += 1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Region"))
        {
            transform.position = new Vector3(-81.0f, -10.0f, 195.8f);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            marbleNumber += 1;
            Debug.Log("Drop Out!");
        }
    }

    void SetCountText()
    {
        countText.text = "Score: " + score.ToString ();
        if(marbleNumber <= 0)
        {
            winText.text = "Final Score: " + score.ToString ();
            countText.text = "Score: 0";
        }
    }

    /*void IsBowlingDown()
    {
        staticheight = bowlings[1].transform.position.y;
        foreach (GameObject bowling in bowlings)
        {
            if (bowling.transform.position.y < staticheight)
            {
                bowling.SetActive(false);
                score = score + 1;
                pickupNum -= 1;
                oneshot -= 1;
            }
        }
        roundStop = -1000;
        if(oneshot <= 0)
        {
            winText.text = "Strike!!";
            countText.text = "Score: ";
        }else if (pickupNum <= -1)
        {
            winText.text = "Nice Job!";
            countText.text = "Score: ";
        }else{
            winText.text = "Round Two";
        }
    }

    void BowlingsReset()
    {
        foreach (GameObject bowling in bowlings)
        {
            bowling.SetActive(true);
        }  
    }*/
}
