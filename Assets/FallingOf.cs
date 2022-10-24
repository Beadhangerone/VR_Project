using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;

public class FallingOf : MonoBehaviour
{

    private int count;
    private GameObject ball;
    private Vector3 resetPos;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Ball");
        resetPos = new Vector3(ball.transform.position.x,
                               ball.transform.position.y,
                               ball.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Target")
            count++;

        if (col.gameObject.name == "Ball")
            ball.transform.position = resetPos;

        if(count == 15)
            SceneManager.LoadScene("Level5");
    }
}
