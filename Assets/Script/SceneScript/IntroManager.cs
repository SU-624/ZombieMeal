using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroManager : MonoBehaviour
{
    public AudioSource soundTitle; 
    public AudioSource soundClick; 
    public GameObject FadeOutImage;

    public float fadeOutSeconds = 1.0f;
    bool isFadeOut = true;
    float fadeDeltaTime = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameScene()
    {
        soundClick.Play();

        if (isFadeOut)
        {
            fadeDeltaTime += Time.deltaTime;
            if (fadeDeltaTime >= fadeOutSeconds)
            {
                fadeDeltaTime = fadeOutSeconds;
                isFadeOut = false;
            }

            soundTitle.volume = (float)(0.2 - (fadeDeltaTime / fadeOutSeconds));
        }

        FadeOutImage.SetActive(true);
        
        StartCoroutine("LoadScene");
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("InGameScreen");
    }

    public void ExitGame()
    {
        soundClick.Play();

        Application.Quit();
    }
}
