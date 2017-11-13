using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    private void Awake()
    {
        if(instance !=null)
        {
            Debug.LogError("More than one BuildManager in scence!");
            return;
        }
        instance = this; 
        

    }



    
    private TurrentBlueprint turretToBuild;
    private Node selectedNode;

    public NodeUi nodeUI;
    public GameObject buildEffect;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }



    public void BuildTurretOn(Node node)
    {
        if(PlayerStats.Money <turretToBuild.cost)
        {
           
            Debug.Log("Not enougt money");
            return;
        }
        PlayerStats.Money -= turretToBuild.cost;

        GameObject effect= (GameObject)Instantiate(buildEffect, node.GetBuildPosition()+new Vector3(0,2,0), Quaternion.identity);
        Destroy(effect,5f);
        GameObject turret= (GameObject) Instantiate(turretToBuild.prefab, node.GetBuildPosition(),Quaternion.identity);
        node.turret= turret;
        
        Debug.Log("TuretBuild! Money Left:" + PlayerStats.Money);

    }
    
    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;
        nodeUI.SetTarget(node);

    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurrentBlueprint turret)
    {
        turretToBuild = turret;
        selectedNode = null;
        nodeUI.Hide();
    }

}
