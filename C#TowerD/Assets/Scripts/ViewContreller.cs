using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewContreller : MonoBehaviour
{
    public float speed = 20;
    public float mousespeed = 100;

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //控制前后左右视角移动
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(h * speed, mouse * mousespeed, v * speed) * Time.deltaTime, Space.World);
        //世界坐标系
    }
}

