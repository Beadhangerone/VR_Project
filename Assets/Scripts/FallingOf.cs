using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;

public class FallingOf : MonoBehaviour
{

    private int count = 0;

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Target"))
            count += 1;

        if(count == 15)
            SceneManager.LoadScene("Level5");
    }
}
