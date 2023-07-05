using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoManager : MonoBehaviour
{
    SerialPort data_stream = new SerialPort("COM5", 9600);
    public string recievedString;
    public GameObject test_data;
    public Rigidbody rb;
    public float sensitivity = 0.01f;

    public string[] datas;
    void Start()
    {
        data_stream.Open();
    }

    void Update()
    {
        recievedString = data_stream.ReadLine();

        datas = recievedString.Split(",");
        rb.AddForce(0,0, float.Parse(datas[0])*sensitivity*Time.deltaTime, ForceMode.VelocityChange);
        rb.AddForce( float.Parse(datas[1])*sensitivity*Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        /*float xpos = float.Parse(datas[0]) * sensitivity * Time.deltaTime;
        float zpos = float.Parse(datas[1]) * sensitivity * Time.deltaTime;
        Vector3 move = new(xpos, 0f ,zpos);
        rb.MovePosition(move);*/
        transform.Rotate(0, float.Parse(datas[2]), 0);
    }
}
