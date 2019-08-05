using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    PlayerCollider playerCol;
    SliderController sliderCtrl;

    Animator roboAni;
    AudioSource jumpSE;
    public AudioClip jump, highjump,decision,fallOcean;

    //ロボットの歩くスピード
    public float speed;

    //ロボットのRigidbody
    private Rigidbody rigid;
    // ロボ太のジャンプ力
    public float forceUp;
    public float jumpWaitTime = 1;

    //ジャンプの判定
    public static bool isJump;
    bool jumpAni = false;
    bool isSound = false;

    Vector3 playermovePos;
    Vector3 distan;

    int distanX;
    int distanY;

    GameObject energyOb;
    public static bool destroyed = false;

    float timer = 0.0f;
    float interval = 0.5f;

    float timer2 = 0.0f;
    float interval2 = 0.0f;

    // Use this for initialization
    void Start () {
        rigid = gameObject.GetComponent<Rigidbody>();
        playerCol = GetComponentInChildren<PlayerCollider>();
        sliderCtrl = GameObject.FindGameObjectWithTag("GameCtrl").GetComponent<SliderController>();
        jumpSE = GameObject.FindGameObjectWithTag("GameCtrl").GetComponent<AudioSource>();
        roboAni = GetComponent<Animator>();
	}

    RaycastHit hit;
	// Update is called once per frame
	void FixedUpdate() {
            Invoke("PlayerMove", 0.1f);

        if (Physics.Raycast(transform.position, Vector3.up, out hit, Mathf.Infinity))
        {
            Destroy(hit.collider.gameObject);
        }
        Debug.Log(timer2);
    }

    // ロボットの動き
    void PlayerMove()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);

        timer += Time.deltaTime;
        if (timer > interval)
        {
            TimeInterval();
            timer = 0;
        }

        if (playerCol.isJump())
        {
            jumpAni = true;
            Jump();
        }

        if (isJump)
        {
            Invoke("ButtonJump", jumpWaitTime);
        }

        if(FollowCamera.count > 3)
        {
            isSound = true;
            timer2 += Time.deltaTime;
            if (timer2 > interval2)
            {
                TimeInterval2();
                timer2 = -2.0f;
            }

        }
        if (FollowCamera.count > 5)
        {
            Invoke("ResultScene", 0.7f);
        }
    }

    void TimeInterval2()
    {
        if (isSound)
        {
            jumpSE.PlayOneShot(fallOcean);
            isSound = false;
        }
    }

    // リザルトシーンに遷移
    void ResultScene()
    {
        SceneManager.LoadScene("Result");
    }

    // １段ジャンプ
    void Jump()
    {       
        transform.Translate(transform.up * Time.deltaTime * speed);
        rigid.AddForce(transform.up * forceUp);
    }
    
    // ２段以上のジャンプ
    void ButtonJump()
    {
        transform.Translate(transform.up * Time.deltaTime * speed);
        rigid.AddForce((transform.up - rigid.velocity) * forceUp * 3);
    }

    void TimeInterval()
    {
        if (jumpAni)
        {
            roboAni.SetTrigger("Jump");
            jumpSE.PlayOneShot(jump);
            jumpAni = false;
        }
    }

    public GameObject effect;
    GameObject Effect;

    public void OnJBDown()
    {
        if (sliderCtrl.sliderValue() > 0)
        {
            isJump = true;
            jumpSE.PlayOneShot(highjump);
            roboAni.SetTrigger("Transformers1");
            Invoke("EffectIns", 1.0f);
        }
    }

    void EffectIns()
    {
        Effect = (GameObject)Instantiate(effect, new Vector3(transform.position.x - 0.6f,
             transform.position.y - 0.7f, transform.position.z),
             Quaternion.Euler(90, 0, 0));
        Effect.transform.parent = gameObject.transform;
    }

    public void OnJBUp()
    {
        jumpSE.Stop();
            isJump = false;
        roboAni.SetTrigger("Transformers3");
        GameObject[] Effect = GameObject.FindGameObjectsWithTag("Effect");
        foreach (GameObject i in Effect)
        {
            Destroy(i);
        }
        //Invoke("effectDes", 1.0f);
    }

    void effectDes()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Energy")
        {
            energyOb = other.gameObject;
            OnDestroy();
        }
    }

    private void OnDestroy()
    {
        destroyed = true;
        Destroy(energyOb);
    }

    public GameObject pausePanel;

    public void PauseButton()
    {
        if (Time.timeScale != 0)
        {
            jumpSE.PlayOneShot(decision);
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            jumpSE.PlayOneShot(decision);
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }
    
}
