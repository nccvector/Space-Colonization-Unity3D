using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public GameObject look_object;
    public float distance = 17f;
    public float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,distance);
        transform.LookAt(look_object.transform, Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Sin(Time.time * speed)*distance, 0, Mathf.Cos(Time.time * speed)*distance);
        transform.LookAt(look_object.transform, Vector3.up);
    }
}
