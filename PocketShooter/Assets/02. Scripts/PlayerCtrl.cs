﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Anim
{
    public AnimationClip idle;
    public AnimationClip runForward;
    public AnimationClip runBackward;
    public AnimationClip runRight;
    public AnimationClip runLeft;
}
public class PlayerCtrl : MonoBehaviour {
    private float h = 0.0f;
    private float v = 0.0f;

    private Transform tr;

    public float moveSpeed = 10.0f;
    public float rotSpeed = 100.0f;

    public Anim anim; //인스펙터뷰에 표시한 애니메이션 클래스 변수
    public Animation _animation;

	public int hp = 100;

	public delegate void PlayerDieHandler ();
	public static event PlayerDieHandler OnPlayerDie;

    // Use this for initialization
    void Start () {
        tr = GetComponent<Transform>();

        _animation = GetComponentInChildren<Animation>();

        _animation.clip = anim.idle;
        _animation.Play();
	}
	
	// Update is called once per frame
	void Update () {
        h = Input.GetAxis ("Horizontal"); // InputManager의 "Horizontal"에 설정된 값을 입력했을때 -1 ~ +1 까지의 값을 반환
        v = Input.GetAxis ("Vertical"); // InputManager의 "Vertical"에 설정된 값을 입력했을때 -1 ~ +1 까지의 값을 반환
        
        // 디버깅 정보를 텍스트 형태로 console 뷰에 표시
        //Debug.Log("H=" + h.ToString());
        //Debug.Log("V=" + v.ToString());

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        tr.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);

        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));

        //애니메이션의 자연스러운 변경을 위한 애니메이션 블렌딩
        if (v >= 0.1f)
        {
            //
            _animation.CrossFade(anim.runForward.name, 0.3f);
        }
        else if (v <= -0.1f)
        {
            //
            _animation.CrossFade(anim.runBackward.name, 0.3f);
        }
        else if (h >= 0.1f)
        {
            //
            _animation.CrossFade(anim.runRight.name, 0.3f);
        }
        else if (h <= -0.1f)
        {
            //
            _animation.CrossFade(anim.runLeft.name, 0.3f);
        }
        else
        {
            //
            _animation.CrossFade(anim.idle.name, 0.3f);
        }
    }

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "PUNCH") {
			hp -= 10;

			//imgHpbar.fillAmount = (float)hp / (float)initHp;

			Debug.Log ("Player HP = " + hp.ToString ());

			if (hp <= 0) {
				PlayerDie ();
			}
		}
	}

	void PlayerDie()
	{
		Debug.Log ("Player Die!!");

		//Tag를 찾음
		//GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");
		//
		//foreach (GameObject monster in monsters) {
		//	monster.SendMessage ("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
		//}
		OnPlayerDie();

		//gameMgr.isGameOver = true;
		//GameMgr.instance.isGameOver = true;
	}
}
