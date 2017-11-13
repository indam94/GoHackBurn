using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{

    public Transform[] points;// 출현 위치 배열

    public GameObject monsterPrefab;// 몬스터 프리팹 할당할 변수
    public List<GameObject> monsterPool = new List<GameObject>();


    public float createTime = 2.0f;
    public int maxMonster = 10;
    public bool isGameOver = false;
    public static GameMgr instance = null;

    void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start()
    {
        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();

        for (int i = 0; i < maxMonster; i++)
        {
            //몬스터 프리팹 생성
            GameObject monster = (GameObject)Instantiate(monsterPrefab);
            //생성한 몬스터 이름설정
            monster.name = "Monster_" + i.ToString();
            monster.SetActive(false);
            monsterPool.Add(monster);
        }

        if (points.Length > 0)
        {
            StartCoroutine(this.CreateMonster());//몬스터 생성 코루틴 함수 호출
        }
    }
    //몬스터 생성 코루틴 함수
    IEnumerator CreateMonster()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(createTime);

            if (isGameOver) yield break;

            foreach (GameObject monster in monsterPool)
            {
                if (!monster.activeSelf)
                {
                    int idx = Random.Range(1, points.Length);
                    monster.transform.position = points[idx].position;
                    monster.SetActive(true);
                    break;
                }
            }
            //int monsterCount = (int) GameObject.FindGameObjectsWithTag("MONSTER").Length;

            //몬스터의 최대 생성 개수보다 많으면 생성하지 않는다.
            /*if(monsterCount<maxMonster)
            {
              yield return new WaitForSeconds(createTime);

              int idx = Random.Range(1, points.Length);

              Instantiate(monsterPrefab, points[idx].position, points[idx].rotation);

            }else{
              yield return null;
            }*/
        }
    }
    // Update is called once per frame
    /*void Update () {

    }*/
}
