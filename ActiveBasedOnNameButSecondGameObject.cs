using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBasedOnNameButSecondGameObject : MonoBehaviour
{

    public GameObject spawnableTarget;


    public string name = "Ra(Clone)";


    private void Update()
    {

        GameObject ivanObject = GameObject.Find(name);

      
        // If "Ivan" is found, make the current GameObject visible and active; otherwise, hide and deactivate it
        if (ivanObject != null)
        {
            // Make this GameObject visible
          
         
                spawnableTarget.SetActive(true);

        }
        else
        {
            spawnableTarget.SetActive(false);
        }
    }
}


