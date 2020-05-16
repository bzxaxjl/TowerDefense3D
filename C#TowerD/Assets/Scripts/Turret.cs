using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> enemys = new List<GameObject>();
    public float attackRateTime = 1; //多少秒攻击一次
    private float timer = 0;
    public GameObject bulletPrefab;//子弹
    public Transform firePosition;
    public Transform head;//控制炮台面向正在攻击的敌人
    public bool useLaser = false;//是否使用激光攻击
    public float damageRate = 70;
    public LineRenderer laserRenderer;//激光
    public GameObject laserEffect;
    public AudioSource audioSource;
    //public AudioSource music;
    //public AudioClip fire;
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Remove(col.gameObject);
        }
    }
    //private void Awake()
    //{
    //    music = gameObject.AddComponent<AudioSource>();
    //    //设置不一开始就播放音效
    //    music.playOnAwake = false;
    //    //加载音效文件，我把跳跃的音频文件命名为jump
    //    fire = Resources.Load<AudioClip>("Material/fire_2");
    //}

    void Start()
    {
        timer = attackRateTime;//遇见敌人直接攻击
    }
    void Update()
    {
        //if (enemys.Count > 0 && enemys[0] != null)//控制炮台面向敌人
        //{
        //    Vector3 targetPosition = enemys[0].transform.position;
        //    targetPosition.y = head.position.y;
        //    head.LookAt(targetPosition);
        //}//放在前面目的是，炮台先调整方向再进行攻击
        //if (useLaser == false)//使用普通攻击
        //{
        //    timer += Time.deltaTime;
        //    if (enemys.Count > 0 && timer >= attackRateTime)//控制炮台攻击
        //    {
        //        timer = 0;//原来的代码,这里会导致发射的子弹一连串，而不是前面一颗消除后菜发射第二颗
        //        Attack();
        //    }
        //}
        //else if (enemys.Count > 0)
        //{
        //    if (laserRenderer.enabled == false)
        //        laserRenderer.enabled = true;
        //    laserEffect.SetActive(true);
        //    if (enemys[0] == null)
        //    {
        //        UpdateEnemys();
        //    }
        //    if (enemys.Count > 0)
        //    {
        //        laserRenderer.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position });
        //        enemys[0].GetComponent<Enemy>().TakeDamage(damageRate * Time.deltaTime);
        //        laserEffect.transform.position = enemys[0].transform.position;
        //        Vector3 pos = transform.position;
        //        pos.y = enemys[0].transform.position.y;
        //        laserEffect.transform.LookAt(pos);
        //    }
        //}
        //else
        //{
        //    laserEffect.SetActive(false);
        //    laserRenderer.enabled = false;
        //}

        if (enemys.Count > 0 && enemys[0] != null)//控制炮台面向敌人
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }//放在前面目的是，炮台先调整方向再进行攻击

        if (useLaser == false)//使用普通攻击
        {
            timer += Time.deltaTime;
            if (enemys.Count > 0 && timer >= attackRateTime)
            {
                timer = 0;
                //music.clip = fire;
                //music.Play();
                audioSource.Play();
                Attack();
            }
        }

        timer += Time.deltaTime;
        if (enemys.Count>0&&timer >= attackRateTime)//控制炮台攻击
        {
            timer =0;//原来的代码,这里会导致发射的子弹一连串，而不是前面一颗消除后菜发射第二颗
            Attack();
        }
    }
    void Attack()
    {
        if (enemys[0] == null)
        {
            UpdateEnemys();
        }
        if (enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }
        else
        {
            timer = attackRateTime;//没有敌人，重置计时器并等待新的敌人
        }
    }
    void UpdateEnemys()
    {
        //enemys.RemoveAll(null);
        //需要找到哪些元素时空的，才能对敌人的集合进行清空。
        List<int> emptyIndex = new List<int>();//保存所有的空元素
        for (int index = 0; index < enemys.Count; index++)
        {
            if (enemys[index] == null)
            {
                emptyIndex.Add(index);
            }
        }

        for (int i = 0; i < emptyIndex.Count; i++)
        {
            enemys.RemoveAt(emptyIndex[i] - i);
        }
    }
}
