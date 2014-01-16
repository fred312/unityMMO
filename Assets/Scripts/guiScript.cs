using UnityEngine;
using System.Collections;

public class guiScript : MonoBehaviour {

	public Texture healthTexture;

	float playerHealth;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void OnGUI() {
		drawHealth();
	}

	void drawHealth(){
		for(int n=0;n<(int)playerHealth /10; n++){
			GUI.DrawTexture(new Rect((float)(10+(n*55)), 10f, 50f, 50f), healthTexture, ScaleMode.ScaleToFit, true);
		}
	}

	void health(float health){
		playerHealth = health;
	}
}
