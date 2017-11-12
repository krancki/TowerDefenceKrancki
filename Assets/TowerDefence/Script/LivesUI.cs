using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class LivesUI : MonoBehaviour {


    public Text livesText;
	
	
	
	void Update () {
        livesText.text = "L:"+PlayerStats.Lives.ToString();
	}
}
