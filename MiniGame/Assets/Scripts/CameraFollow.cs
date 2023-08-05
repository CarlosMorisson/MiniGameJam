using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform PivotCameraPos;
    private void Awake()
    {
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = PivotCameraPos.position;
    }
}
