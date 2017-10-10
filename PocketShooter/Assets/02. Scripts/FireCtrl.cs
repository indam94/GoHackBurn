using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour {

	//총알 프리맵
	public GameObject bullet;
	//총알 발사 좌표
	public Transform firePos;
	//사운드
	//public AudioClip fireSfx;
	//AudioSource 컴포넌트 저장 변수
	//private AudioSource source = null;
	//
	//public MeshRenderer muzzleFlash;

	// Use this for initialization
	void Start () {
		//source = GetComponent<AudioSource> ();
		//
		//muzzleFlash.enabled = false;
	}

	// Update is called once per frame
	void Update () {

		//Debug.DrawRay (firePos.position, firePos.forward * 10.0f, Color.green);

		if (Input.GetMouseButtonDown (0)) {
			Fire ();

			//RaycastHit hit;
			/*
			if(Physics.Raycast(firePos.position, firePos.forward, out hit, 10.0f))
			{
				if (hit.collider.tag == "MONSTER") {
					object[] _params = new object[2];
					_params [0] = hit.point;
					_params [1] = 20;

					hit.collider.gameObject.SendMessage ("OnDamage", _params, SendMessageOptions.DontRequireReceiver);
				}

				if (hit.collider.tag == "BARREL") {
					object[] _params = new object[2];
					_params [0] = firePos.position;
					_params [1] = hit.point;
					hit.collider.gameObject.SendMessage ("OnDamage", _params, SendMessageOptions.DontRequireReceiver);
				}
			}
			*/
		}
	}
	//추후 효과 추가를 위해 따로 생성
	void Fire(){
		//
		CreateBullet ();
		//
		//		source.PlayOneShot(fireSfx, 0.9f);
		//GameMgr.instance.PlaySfx(firePos.position, fireSfx);
		//
		//StartCoroutine(this.ShowMuzzleFlash());
	}

	void CreateBullet(){
		//
		Instantiate (bullet, firePos.position, firePos.rotation);
	}
	/*
	IEnumerator ShowMuzzleFlash(){

		//불규칙 사이즈
		float scale = Random.Range(1.0f, 2.0f);
		muzzleFlash.transform.localScale = Vector3.one * scale;

		//불규칙 회전
		Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360));
		muzzleFlash.transform.localRotation = rot;

		//
		muzzleFlash.enabled = true;

		//
		yield return new WaitForSeconds (Random.Range (0.05f, 0.3f));

		//
		muzzleFlash.enabled = false;
	}
	*/
}
