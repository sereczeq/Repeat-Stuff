using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotator : MonoBehaviour
{
    //Prędkość obrotu
    public float rotationSpeed = 5f;
    // Update is called once per frame
    void FixedUpdate()
    {
        //Co tick fizyki obróć obiekt o rotationSpeed w osi y
        transform.Rotate(new Vector3(0f, rotationSpeed, 0f));
    }

}
