using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class _Sound
{
    public string name;         // �� �̸� 
    public AudioClip clip;      // ����� Ŭ��
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

    public _Sound[] bgmSounds;                  // ���� �ϳ��� �� �̸��� Ŭ�� ���� ����, ȿ����ó�� ����� �� ���ÿ� ���� BGM ��� �Ұ���
    public _Sound[] effectSounds;
    public _Sound[] EventSounds;

    public AudioSource audioSourceBGM;          // �����
    public AudioSource[] audioSourceEffects;
    public AudioSource audioSourceEvent;

    public string[] playSoundName;              // ������� ȿ�������� �̸��� ���� �� ��

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
                Debug.Log("��� ���� ������ҽ��� ��� ���Դϴ�.");
                return;
            }
        }
        Debug.Log(_name + "���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�.");
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
                Debug.Log("��� ���� ������ҽ��� ��� ���Դϴ�.");
                return;
            }
        }
        Debug.Log(_name + "���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�.");
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
        Debug.Log(_name + "���尡 SoundManager�� ��ϵǾ� ���� �ʽ��ϴ�.");
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
        Debug.Log(_name + "���尡 SoundManager�� ��ϵǾ� ���� �ʽ��ϴ�.");
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
        Debug.Log(_name + "���尡 SoundManager�� ��ϵǾ� ���� �ʽ��ϴ�.");
    }

    public void StopAllSE()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }

    // Ư�� ȿ������ ����� �����.
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
        Debug.Log("��� ����" + _name + "���尡 �����ϴ�. ");
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
        Debug.Log(_name + "���尡 SoundManager�� ��ϵǾ� ���� �ʽ��ϴ�.");
    }

}

