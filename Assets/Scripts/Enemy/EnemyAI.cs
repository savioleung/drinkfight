using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public int hp = 10;
    [SerializeField]
    GameObject coin;
    [SerializeField]
    GameObject happou;
    public Transform[] wayPoints = new Transform[3];//徘徊する
    int i = 0;
    private NavMeshAgent agent;
    GameObject Player;
    float shootTime = 0.5f, reshootTime = 0.5f;
    float dir;
    // Start is called before the first frame update

    public enum EnemyAiState
    {
        IDLE,
        SEARCHING,
        ATTACKING,
    }
    public EnemyAiState aiState = EnemyAiState.IDLE;

    void Start()
    {
        happou.SetActive(false);
        aiState = EnemyAiState.SEARCHING;
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        dir = Vector3.Distance(transform.position, Player.transform.position);

        if (hp <= 0)
        {
            Death();
        }
        else
        {
            AIUpdate();
        }
    }
    void AIUpdate()
    {
        switch (aiState)
        {
            case EnemyAiState.IDLE:
                Idle();
                break;
            case EnemyAiState.SEARCHING:
                Searching();
                break;
            case EnemyAiState.ATTACKING:
                Attacking(); ;
                break;
        }
    }



    private void Idle()
    {
        agent.stoppingDistance = 0;

        agent.SetDestination(wayPoints[i].position);
        if (dir <= 2)
        {
            i++;
            if (i >= 3) { i = 0; }
            Debug.Log("stoped next:" + i);
        }
        Debug.Log(dir);
    }

    void Searching()
    {

        if (dir < 10)
        {
            aiState = EnemyAiState.ATTACKING;
        }
        else
        {
            agent.SetDestination(Player.transform.position);
        }
    }
    private void Attacking()
    {
        Debug.Log(shootTime);
        transform.LookAt(Player.transform.position);
        if (dir <= 15)
        {
            //時間経過
            shootTime -= 1 * Time.deltaTime;
            //時間が0になったら射撃
            if (shootTime < 0)
            {
                Shoot();
            }
        }
        else
        {
            aiState = EnemyAiState.SEARCHING;
        }
    }

    private void Shoot()
    {
        happou.SetActive(true);
        int i = Random.Range(0, 4);
        Debug.Log(i);
        if (i == 0)
        {
            Player.GetComponent<PlayerEnergy>().getDamage(Random.Range(10,30));
        }
        shootTime = reshootTime;
    }

    void Death()
    {

        for (int i = 0; i < 3; i++)
        {
            GameObject coins = Instantiate(coin, transform.position + new Vector3(UnityEngine.Random.Range(0, 3), 0, UnityEngine.Random.Range(0, 3)), Quaternion.identity) as GameObject;
            coins.name = "coin";
        }
        Destroy(this.gameObject);
    }

}
