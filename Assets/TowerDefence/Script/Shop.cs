
using UnityEngine;

public class Shop : MonoBehaviour {

    public TurrentBlueprint standardTurret;
    public TurrentBlueprint missileLuncher;
    public TurrentBlueprint laserBeamer;

    private BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;

    }

    public void SelectStadardTurret()
    {
        Debug.Log("Another Turret Purchased");
        buildManager.SelectTurretToBuild(standardTurret);

    }



    public void SelectMissileLauncher()
    {
        Debug.Log("Missile Launcher Selected");
        buildManager.SelectTurretToBuild(missileLuncher);
    }

    public void SelectLaserBeamer()
    {
        Debug.Log("Missile Launcher Selected");
        buildManager.SelectTurretToBuild(laserBeamer);
    }


}
