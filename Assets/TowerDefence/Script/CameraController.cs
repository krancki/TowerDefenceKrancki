using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    
    private Vector3 nextPos;    
    
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 10f;
    public float minY = 10f;
    public float maxY = 70f;

    [Header("Position of Camera")]

    public float maxZ;
    public float maxX;
    public float minZ;
    public float minX;

    private void Start()
    {
        nextPos = transform.position;
    }

    void Update () {

        if(GameMenager.gameIsOver)
        {
            this.enabled = false;
            return;
        }
        

            if ((Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) && transform.position.z<=maxZ)
            {
                transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
            }
            

            if ((Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness) && transform.position.z >= minZ)
            {
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
            }



            if ((Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness ) && transform.position.x <= maxX)
            {
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            }

            if ((Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness) && transform.position.x >= minX)
            {
                transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            }

       

         Vector3 pos = transform.position;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            nextPos.y = transform.position.y;
            nextPos.y -= (scroll * 100 * scrollSpeed * Time.deltaTime);
            
        }

        pos.y = Mathf.Lerp(pos.y, nextPos.y, 0.125f);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        
        transform.position = pos;

    }


}
