using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    public AudioClip bgmOST1; //재생할 소리를 변수로 담습니다.
    public AudioClip bgmOST2;
    public AudioClip bgmOST3;
    public AudioClip bgmOST4;
    public AudioClip soundBallThrowing;

    AudioSource myAudio; //AudioSorce 컴포넌트를 변수로 담습니다.
    public static SoundMgr instance;  //자기자신을 변수로 담습니다.
    void Awake() //Start보다도 먼저, 객체가 생성될때 호출됩니다
    {
        if (SoundMgr.instance == null) //incetance가 비어있는지 검사합니다.
        {
            SoundMgr.instance = this; //자기자신을 담습니다.
        }
    }
    void Start()
    {
        myAudio = this.gameObject.GetComponent<AudioSource>(); //AudioSource 오브젝트를 변수로 담습니다.
    }

    public void PlaySound(AudioClip whichSound)
    {
        myAudio.PlayOneShot(whichSound);
    }

    public void PlayLoopSound(AudioClip whichSound)
    {
        myAudio.loop = true;
        myAudio.clip = whichSound;
        myAudio.Play();
    }

    void Update()
    {

    }
}
