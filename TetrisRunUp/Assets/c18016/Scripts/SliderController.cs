using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour {

    public static Slider slider;
    PlayerCollider playerCollider;

    // n秒待つためのtimer
    float timer;

    float jumpEnergy;

    public float sliderValue()
    {
        return jumpEnergy;
    }

	// Use this for initialization
	void Start () {
        slider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
        playerCollider = GameObject.FindGameObjectWithTag("Collider").GetComponent<PlayerCollider>();
        jumpEnergy = slider.maxValue;
	}
	
	// Update is called once per frame
	void Update () {
        SliderEnergy();
	}
    // ロケットボタンの制御
    void SliderEnergy()
    {
        if (Player.isJump)
        {
            Timer();

            // 2秒経ったらエネルギーが減っていく
            if (timer > 2)
            {
                jumpEnergy -= 20 * Time.deltaTime;

                // Sliderが0になったら
                if (jumpEnergy < slider.minValue)
                {
                    Player.isJump = false;
                    // Debug.Log("Empty");
                    timer = 0;
                }
            }
        }
        else
        {
            timer = 0;
        }

        slider.value = jumpEnergy;

        // プレイヤーが死んだら
        if (Player.destroyed)
        {
            jumpEnergy = slider.maxValue;
            Player.destroyed = false;
        }

    }

    void Timer()
    {
        timer += 1 * Time.deltaTime;
    }
}
