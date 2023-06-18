using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GmeOverManager : MonoBehaviour
{
    private void Start()
    {
        //SoundManager.Instance.PlayBGM("GameOver_BGM");
        //InteractionEvent.Instance.EventPenal.SetActive(false);
        //InteractionEvent.Instance.NowEventIndex = 0;
    }
    public void GameScene()
    {
        SceneManager.LoadScene("TitleScene");
        SoundManager.Instance.StopBGM("GameOver_BGM");
    }
}
