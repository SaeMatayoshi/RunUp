using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {

    bool touch = false;
    bool jump = false;

    public bool isTouch()
    {
        return touch;
    }

    public bool isJump()
    {
        return jump;
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () { 

    }

    // ロボットのコライダーがブロックに入ったら
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            jump = true;
        }
    }
    
    // ロボットのコライダーがブロックから出たら
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Block")
        {
            jump = false;
        }
    }
}
