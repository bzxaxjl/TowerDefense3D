using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;

    //表示当前选择的炮台(要建造的炮台)
    public TurretData selectedTurretData;
    public Text moneyText;//调用金钱的变化
    public int money = 1000;
    public Animator moneyAnimator;//获得动画组件

    public void Start()
    {

    }

    void ChangeMoney(int change)//金钱的更新
    {
        money += change;
        moneyText.text = "$" + money;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                //开发炮台的建造
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if(selectedTurretData != null && mapCube.turretGo == null)//判断前者条件，防止不选择turret时点击Cube，报空指针的错误
                    {
                        //可以创建
                        if (money > selectedTurretData.cost)
                        {
                            ChangeMoney(-selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData.turretPrefab);
                        }
                        else
                        {
                            //TODO 钱不够
                            moneyAnimator.SetTrigger("Flicker");
                        }
                    }
                    else if(mapCube.turretGo != null)//在原有炮塔的基础上升级炮塔
                    {
                        //TODO 升级处理
                    }
                    //if (selectedTurretData != null && mapCube.turretGo == null)
                    //{
                    //    //可以创建 
                    //    if (money > selectedTurretData.cost)
                    //    {
                    //        ChangeMoney(-selectedTurretData.cost);
                    //        mapCube.BuildTurret(selectedTurretData);
                    //    }
                    //    else
                    //    {
                    //        //提示钱不够
                    //        moneyAnimator.SetTrigger("Flicker");
                    //    }
                    //}
                    //else if (mapCube.turretGo != null)
                    //{

                    // 升级处理

                    //if (mapCube.isUpgraded)
                    //{
                    //    ShowUpgradeUI(mapCube.transform.position, true);
                    //}
                    //else
                    //{
                    //    ShowUpgradeUI(mapCube.transform.position, false);
                    //}
                    //if (mapCube == selectedMapCube && upgradeCanvas.activeInHierarchy)
                    //{
                    //    StartCoroutine(HideUpgradeUI());
                    //}
                    //else
                    //{
                    //    ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgraded);
                    //}
                    //selectedMapCube = mapCube;
                }

                }
            }
        }
    
    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = laserTurretData;
        }
    }

    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missileTurretData;
        }
    }
    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
        }
    }
}
