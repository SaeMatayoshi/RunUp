using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    // プレイヤー
    public GameObject player;
    public float smoothTime;

    //カメラの位置
    private Vector3 camePos;
    private Vector3 targetPos;

    float x, y, z;

    //プレイヤーとカメラの距離
    private Vector3 offset;
    //プレイヤーとカメラの move distance 
    private Vector3 distan;
    private Vector3 velocity = Vector3.zero;

    //determine camera height
    public static int count;

    private bool isFollow;

    // Use this for initialization
    void Start () {
        offset = transform.position - player.transform.position;
        targetPos = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    // カメラの動き
    private void LateUpdate()
    {
        x = player.transform.position.x + offset.x;
        y = transform.position.y;
        z = transform.position.z;

        camePos = new Vector3(x, y, z);
        transform.position = camePos;

        distan.y = camePos.y - player.transform.position.y;
        count = (int)distan.y;

        if (count < 1)
        {
            isFollow = true;
        }

        if(isFollow == true)
        {
            Start();
            y = targetPos.y + 5;
            camePos = new Vector3(x, y, z);
            transform.position = Vector3.SmoothDamp(transform.position, camePos, ref velocity, smoothTime);
            
            isFollow = false;
        }
        
    }
}
