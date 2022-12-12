using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private Vector3 camOffSet;

    private Camera cam;
    void Start()
    {
        cam=Camera.main;
    }

    void FixedUpdate()
    {
        cam.transform.position=new Vector3(cam.transform.position.x,player.transform.position.y+camOffSet.y,cam.transform.position.z);
    }
}
