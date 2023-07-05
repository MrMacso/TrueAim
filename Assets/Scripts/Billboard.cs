using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cam;

    private void Start()
    {
        cam = FindObjectOfType<Camera>().transform;
    }
    //rotate object forward player
    void LateUpdate()
    {
        //transform.LookAt(transform.position + cam.forward);
        Vector3 newVector = new Vector3( 0f, transform.position.y + cam.forward.y, 0f);
        transform.LookAt(newVector);
        //transform.rotation.x.Equals(0f);
        //transform.rotation.z.Equals(0f);
    }
}
