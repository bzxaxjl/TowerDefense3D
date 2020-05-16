using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 10;
    public float hp = 150;
    private float totalHp;
    public GameObject explosionEffect;
    public Slider hpSlider;
    private Transform[] positions;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject go = GameObject.Find("Enemy1");
        //Debug.Log(go.name);
        //测试 ：输出Find()对象
        positions = Waypoints.positions;
        totalHp = hp;
        hpSlider = GetComponentInChildren<Slider>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
    //控制Enemy移动
    void Move()
    {
        if (index > positions.Length - 1) return;
        //到达最大位置（End）则停止
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        //位置向量，normalized取得单位向量  控制移动方向
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {
            index++;
        }
        if (index > positions.Length - 1)
        {
            ReachDestination();
        }
    }
    void ReachDestination()
    {
        GameManager.Instance.Failed();
        GameObject.Destroy(this.gameObject);
        //到达终点销毁敌人对象
    }
    void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
        //销毁对象   
    }
    public void TakeDamage(float damage)
    {
        //控制怪物血量，承伤。
        if (hp <= 0) return;
        hp -= damage;
        hpSlider.value = (float)hp / totalHp;//设置血量，百分比显示
        if (hp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        //ChangeMoney(-selectedTurretData.cost);
        GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 1.5f);
        Destroy(this.gameObject);
    }

}
