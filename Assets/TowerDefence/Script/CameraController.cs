using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private bool doMovment = true;

    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 10f;
    public float minY = 10f;
    public float maxY = 70f;

	void Update () {

        if(GameMenager.gameIsOver)
        {
            this.enabled = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            doMovment = !doMovment;

        if (!doMovment)
            return;

            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
            }


            if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
            {
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
            }



            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            }

            if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
            {
                transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            }

        Vector3 pos = transform.position;
        float nextPos = transform.position.y;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            nextPos -= (scroll * 1000 * scrollSpeed * Time.deltaTime);
            //pos.y -= scroll * 100 *scrollSpeed * Time.deltaTime;
 
        }

        pos.y = Mathf.Lerp(pos.y, nextPos, 0.125f);
        //pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

    }


}
