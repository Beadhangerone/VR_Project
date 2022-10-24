using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour
{
    private GameObject ball;
    private Vector3 resetPos;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Ball");
        resetPos = new Vector3(ball.transform.position.x, ball.transform.position.y,ball.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col) 
    {
         if (col.gameObject.name == "Plane")
        {
            Debug.Log("Log");
            ball.GetComponent<Rigidbody>().freezeRotation = true;
            ball.transform.position = resetPos;
        }
    }
}
