using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToStart : MonoBehaviour
{
    public GameObject demoContent;
    public Transform fingerChaser;
    public Transform hololensCamera;
    public float addHeight;

    public void Start()
    {
        Debug.developerConsoleVisible = false;
    }

    public void DecidePosition()//確定位置
    {
        Debug.Log("DecidePosition");
        demoContent.SetActive(true); //顯示引擎模型
        float spawnHeight = fingerChaser.transform.position.y + addHeight;
        demoContent.transform.position = new Vector3( fingerChaser.position.x, spawnHeight, fingerChaser.position.z );
        demoContent.transform.LookAt( new Vector3( hololensCamera.position.x, spawnHeight, hololensCamera.position.z ));
        demoContent.transform.Rotate(0, 180, 0);
        this.gameObject.SetActive(false);        
    }
}
