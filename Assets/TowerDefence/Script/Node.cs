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
    public GameObject turret;
     
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

        if (!buildManager.CanBuild)
            return;

        if(turret!=null)
        {
            Debug.Log("Is full");
            return;
        }

        buildManager.BuildTurretOn(this);

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
