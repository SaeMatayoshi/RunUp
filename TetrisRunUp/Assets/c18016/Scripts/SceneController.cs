using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    // ハイスコアテキスト
    public Text highscoreText;
    // スコアテキスト
    public Text scoreText;
    public Player playercs;

    AudioSource click;
    // 効果音
    public AudioClip Decision, scoreSound;

    int score;
    int highscore;
    float timer = 0.0f;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        click = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        IsScene();
	}

    public void OnClickStart()
    {
        click.PlayOneShot(Decision);
        Invoke("MainScene", 0.4f);
    }

    // メインシーンに遷移
    void MainScene()
    {
        Destroy(AudioController.game_ob);
        SceneManager.LoadScene("Main");
    }

    public void OnClickAgain()
    {
        click.PlayOneShot(Decision);
        Invoke("MainScene", 0.4f);
    }
    
    public void OnClickTitle()
    {
        Debug.Log("title");
        click.PlayOneShot(Decision);
        Invoke("TitleScene", 0.4f);
    }

    // タイトルシーンに遷移
    void TitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    // リザルトシーンに遷移
    public void OnClickResult()
    {
        click.PlayOneShot(Decision);
        SceneManager.LoadScene("Result");
    }
    
    void IsScene()
    {
        if (SceneManager.GetActiveScene().name == "Result")
        {
            timer += Time.deltaTime;
            if(timer < 0.2)
            {
                TimeInterval();
                timer = 1.0f;
            }
            score = Score.score;
            if(PlayerPrefs.GetInt("HighScore") <score)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
            scoreText.text = "Score : " + score;

            highscore = PlayerPrefs.GetInt("HighScore", score);

            highscoreText.text = "HighScore : " + highscore;
        }
        else
        {
            highscoreText = null;
        }
    }

    void TimeInterval()
    {
        click.PlayOneShot(scoreSound);
    }
}
