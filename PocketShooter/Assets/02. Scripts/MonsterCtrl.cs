using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCtrl : MonoBehaviour
{

    public enum MonsterState { idle, trace, attack, die };

    public MonsterState monsterState = MonsterState.idle;

    private Transform monsterTr;
    private Transform playerTr;
    private UnityEngine.AI.NavMeshAgent nvAgent;
    private Animator animator;
    private GameUI gameUI;

    public float traceDist = 10.0f;
    public float attackDist = 2.0f;

    private int hp = 100;


    private bool isDie = false;
    // Use this for initialization
    void Awake()
    {
        monsterTr = this.gameObject.GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //nvAgent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent> ();
        nvAgent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

        animator = this.gameObject.GetComponent<Animator>();
        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();
        //nvAgent.destination = playerTr.position;

        //StartCoroutine (this.CheckMonsterState ());
        //StartCoroutine (this.MonsterAction ());
    }

    void OnEnable()
    {
        PlayerCtrl.OnPlayerDie += this.OnPlayerDie;
        StartCoroutine(this.CheckMonsterState());
        StartCoroutine(this.MonsterAction());
    }

    void OnDisable()
    {
        PlayerCtrl.OnPlayerDie -= this.OnPlayerDie;
    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(playerTr.position, monsterTr.position);

            if (dist <= attackDist)
            {
                monsterState = MonsterState.attack;
            }
            else if (dist <= traceDist)
            {
                monsterState = MonsterState.trace;
            }
            else
            {
                monsterState = MonsterState.idle;
            }
        }
    }

    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (monsterState)
            {
                case MonsterState.idle:
                    nvAgent.Stop();
                    animator.SetBool("IsTrace", false);
                    break;

                case MonsterState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.Resume();

                    animator.SetBool("IsAttack", false);
                    animator.SetBool("IsTrace", true);
                    break;

                case MonsterState.attack:
                    //추적 중지
                    nvAgent.Stop();
                    animator.SetBool("IsAttack", true);
                    break;
            }
            yield return null;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Ball")
        {
            //혈흔 효과
            //CreateBloodEffect(coll.transform.position);


            //hp차감
            hp -= coll.gameObject.GetComponent<BallCtrl>().damage;
            if (hp <= 0)
            {
                MonsterDie();
            }
            print("!!!");
            //삭제
            Destroy(coll.gameObject);
            //
            animator.SetTrigger("IsHit");
        }
    }

    void MonsterDie()
    {

        gameObject.tag = "Untagged";

        StopAllCoroutines();

        isDie = true;
        monsterState = MonsterState.die;
        nvAgent.Stop();
        animator.SetTrigger("IsDie");

        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;

        foreach (Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = false;
        }
        gameUI.DispScore(1);

        StartCoroutine(this.PushObjectPool());
    }

    IEnumerator PushObjectPool()
    {
        yield return new WaitForSeconds(3.0f);

        isDie = false;
        hp = 100;
        gameObject.tag = "MONSTER";
        monsterState = MonsterState.idle;

        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;

        foreach (Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = true;
        }

        gameObject.SetActive(false);
    }

    void OnPlayerDie()
    {
        Debug.Log("Wow!");
        StopAllCoroutines();
        nvAgent.Stop();
        animator.SetTrigger("IsPlayerDie");
    }

    // Update is called once per frame
    void Update()
    {

    }
}