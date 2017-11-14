using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {


    public Color hoverColor;
    public Color notEnoughtMoneyColor;
    public Vector3 positionOffset;

    [Header("Option")]
    BuildManager buildManager;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurrentBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;


    private Renderer rend;
    private Color startColor;
    private void Start()
    {

        buildManager = BuildManager.instance;
           
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

       

        if(turret!=null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());

    }


    void BuildTurret(TurrentBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {

            Debug.Log("Not enougt money");
            return;
        }
        PlayerStats.Money -= blueprint.cost;
        turretBlueprint = blueprint;
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition() + new Vector3(0, 2, 0), Quaternion.identity);
        Destroy(effect, 5f);

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        Debug.Log("TuretBuild! ");

    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {

            Debug.Log("Not enougt money to upgrade that");
            return;
        }
        PlayerStats.Money -= turretBlueprint.upgradeCost;

        //Destroy a old turret
        Destroy(turret);

        //Create a new upgradeTurret
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition() + new Vector3(0, 2, 0), Quaternion.identity);
        Destroy(effect, 5f);

        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        isUpgraded = true;

        Debug.Log("TuretUpgrade! ");

    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!buildManager.CanBuild )
            return;
       if(buildManager.HasMoney)
        {
        rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughtMoneyColor;
        }
      
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }


}
