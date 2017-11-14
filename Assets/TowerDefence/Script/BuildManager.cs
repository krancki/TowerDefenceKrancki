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

    public TurrentBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

}
