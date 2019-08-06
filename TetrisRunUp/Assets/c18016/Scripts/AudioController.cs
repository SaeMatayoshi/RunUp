using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour {
    public GameObject[] bgm_ob;
    public static GameObject game_ob;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        IsScene();
    }

    // BGMの制御
    void IsScene()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {

            if(!GameObject.Find("Audio"))
            {
                game_ob = Instantiate(bgm_ob[1], transform.position, Quaternion.identity);

                game_ob.name = "Audio";
            }
        }else if(SceneManager.GetActiveScene().name == "Result" && !GameObject.Find("Audio"))
        {
            game_ob = Instantiate(bgm_ob[0], transform.position, Quaternion.identity);

            game_ob.name = "Audio";
            DontDestroyOnLoad(game_ob);
        }else if(SceneManager.GetActiveScene().name == "Title" && !GameObject.Find("Audio"))
        {
            game_ob = Instantiate(bgm_ob[0], transform.position, Quaternion.identity);

            game_ob.name = "Audio";
            
        }
    }
}
