using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Anim
{
    public AnimationClip idle;
    public AnimationClip runFoward;
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

    // Use this for initialization
    void Start () {
        tr = GetComponent<Transform>();	
	}
	
	// Update is called once per frame
	void Update () {
        h = Input.GetAxis ("Horizontal"); // InputManager의 "Horizontal"에 설정된 값을 입력했을때 -1 ~ +1 까지의 값을 반환
        v = Input.GetAxis ("Vertical"); // InputManager의 "Vertical"에 설정된 값을 입력했을때 -1 ~ +1 까지의 값을 반환
        
        // 디버깅 정보를 텍스트 형태로 console 뷰에 표시
        Debug.Log("H=" + h.ToString());
        Debug.Log("V=" + v.ToString());

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        tr.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);

        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));
    }
}
