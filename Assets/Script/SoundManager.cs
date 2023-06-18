using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class _Sound
{
    public string name;         // 곡 이름 
    public AudioClip clip;      // 오디오 클립
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;

                if (_instance == null)
                {
                    Debug.Log("no singletone obj");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public _Sound[] bgmSounds;                  // 원소 하나당 곡 이름과 클립 정보 있음, 효과음처럼 재생될 때 동시에 여러 BGM 재생 불가능
    public _Sound[] effectSounds;
    public _Sound[] EventSounds;

    public AudioSource audioSourceBGM;          // 재생기
    public AudioSource[] audioSourceEffects;
    public AudioSource audioSourceEvent;

    public string[] playSoundName;              // 재생중인 효과음들의 이름이 게임 중 실

    private void Start()
    {
        playSoundName = new string[audioSourceEffects.Length];
    }

    public void PlaySE(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (_name == effectSounds[i].name)
            {
                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (!audioSourceEffects[j].isPlaying)
                    {
                        audioSourceEffects[j].clip = effectSounds[i].clip;
                        audioSourceEffects[j].PlayOneShot(audioSourceEffects[j].clip);
                        playSoundName[j] = effectSounds[i].name;
                        return;
                    }
                }
                Debug.Log("모든 가용 오디오소스가 사용 중입니다.");
                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다.");
    }

    public void PlayMakingCookie(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (_name == effectSounds[i].name)
            {
                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (!audioSourceEffects[j].isPlaying)
                    {
                        audioSourceEffects[j].clip = effectSounds[i].clip;
                        //audioSourceEffects[j].loop = true;
                        audioSourceEffects[j].Play();
                        playSoundName[j] = effectSounds[i].name;
                        return;
                    }
                }
                Debug.Log("모든 가용 오디오소스가 사용 중입니다.");
                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다.");
    }

    public void PlayBGM(string _name)
    {
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if (_name == bgmSounds[i].name)
            {
                audioSourceBGM.clip = bgmSounds[i].clip;
                audioSourceBGM.Play();

                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되어 있지 않습니다.");
    }

    public void FadeInSound(string _name)
    {
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if (_name == bgmSounds[i].name)
            {
                if (audioSourceBGM.volume < 0.6f)
                {
                    audioSourceBGM.Play();
                    audioSourceBGM.clip = bgmSounds[i].clip;
                    audioSourceBGM.volume += Time.deltaTime * 0.7f;
                }

            }
        }
    }

    public void FadeOutSound(string _name)
    {
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if (_name == bgmSounds[i].name)
            {
                if (audioSourceBGM.volume >= 0.2f)
                {
                    audioSourceBGM.GetComponent<AudioSource>();

                    if (audioSourceBGM.isPlaying)
                    {
                        audioSourceBGM.clip = bgmSounds[i].clip;
                        audioSourceBGM.volume -= Time.deltaTime * 0.5f;
                       
                        if (audioSourceBGM.volume <= 0.3f)
                        {
                            audioSourceBGM.Pause();
                        }
                    }
                }

            }
        }
    }

    public void PlayEvent(string _name)
    {
        for (int i = 0; i < EventSounds.Length; i++)
        {
            if (_name == EventSounds[i].name)
            {
                audioSourceEvent.clip = EventSounds[i].clip;
                audioSourceEvent.PlayOneShot(audioSourceEvent.clip);
                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되어 있지 않습니다.");
    }

    public void PlayDelaySound(string _name)
    {
        for (int i = 0; i < EventSounds.Length; i++)
        {
            if (_name == EventSounds[i].name)
            {
                audioSourceEvent.clip = EventSounds[i].clip;
                audioSourceEvent.PlayDelayed(1f);
                audioSourceEvent.PlayOneShot(audioSourceEvent.clip);
                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되어 있지 않습니다.");
    }

    public void StopAllSE()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }

    // 특정 효과음의 재생을 멈춘다.
    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (playSoundName[i] == _name)
            {
                audioSourceEffects[i].Stop();
                break;
            }
        }
        Debug.Log("재생 중인" + _name + "사운드가 없습니다. ");
    }

    public void StopBGM(string _name)
    {

        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if (_name == bgmSounds[i].name)
            {
                audioSourceBGM.clip = bgmSounds[i].clip;
                audioSourceBGM.Stop();

                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되어 있지 않습니다.");
    }

}

