using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GyroController : MonoBehaviour
{
    //public GameObject cameraContainer;

    private bool gyroEnabled;
    private Gyroscope gyro;
    public GameObject player;

    private void Start()
    {
        //setup camera container
        //cameraContainer.transform.position = transform.position;
       //transform.SetParent(cameraContainer.transform, false);
        //cameraContainer.transform.SetParent(player.transform, false);

        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            //cameraContainer.transform.rotation = Quaternion.Euler(90f,90f, 0f);

            return true;
        }
        return false;
    }
    private void Update()
    {
        if (gyroEnabled)
        {
            transform.localRotation = GyroToUnity(gyro.attitude);
        }
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x , q.y , -q.z, -q.w);
    }
}
