using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUi : MonoBehaviour {

    public GameObject ui;
    public Text upgradeCost;
    public Button upgradeButton;
    private Node target;


    public void SetTarget(Node _target)
    {
       
        this.target = _target;
        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            //upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
           // upgradeButton.interactable = true;
        }
        else
        {
            //upgradeButton.interactable = false;
            //upgradeCost.text="Done";
        }

        ui.SetActive(true);
    }

  

    public void Hide()
    {
        ui.SetActive(false);

    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }
}
