using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClasses : MonoBehaviour {

	public GameObject playerPrefab;
	public List<Sprite> classSprites;
	public Transform targetTransform;

	// Use this for initialization
	void Start () {

		GameObject playerObject;
		BasePlayer playerClass;

		//instantiates a new player gameobject from the prefab then accesses the BasePlayer script in the newly spawned gameobject
		//sets the transform location of the gameobject and sets the world position stays to false
		//sets the gameobject state to active/inactive
		//Fighter Class GameObject
		playerObject = (GameObject)Instantiate (playerPrefab);
		playerObject.gameObject.name = "Fighter";
		playerClass = playerObject.GetComponent<BasePlayer> ();
		playerClass.transform.SetParent (targetTransform, false);
		playerClass.gameObject.SetActive (false);


		//assigns the corresponding values to this game object
		playerClass.playerName = "Rogar";
		playerClass.playerDescription = "A strong and durable hero";
		playerClass.maxHP = 250;
		playerClass.maxMP = 50;
		playerClass.strength = 50;
		playerClass.vitality = 50;
		playerClass.intelligence = 10;
		playerClass.wisdom = 10;
		playerClass.playerSprite = classSprites[0];
		//adds the player game object to a list of player classes to be used later on when creating an instance of a player for the user to use.
		GameManager.instance.classList.Add (playerClass);


		//Thief Class GameObject
		playerObject = (GameObject)Instantiate (playerPrefab);
		playerObject.gameObject.name = "Thief";
		playerClass = playerObject.GetComponent<BasePlayer> ();
		playerClass.transform.SetParent (targetTransform, false);
		playerClass.gameObject.SetActive (false);

		playerClass.playerName = "Jasper the Maniac";
		playerClass.playerDescription = "An agile and tricky hero";
		playerClass.maxHP = 175;
		playerClass.maxMP = 125;
		playerClass.strength = 30;
		playerClass.vitality = 30;
		playerClass.intelligence = 30;
		playerClass.wisdom = 30;
		playerClass.playerSprite = classSprites[2];
		GameManager.instance.classList.Add (playerClass);


		//Wizard Class GameObject
		playerObject = (GameObject)Instantiate (playerPrefab);
		playerObject.gameObject.name = "Wizard";
		playerClass = playerObject.GetComponent<BasePlayer> ();
		playerClass.transform.SetParent (targetTransform, false);
		playerClass.gameObject.SetActive (false);

		playerClass.playerName = "Merlin";
		playerClass.playerDescription = "A physically weak but magically strong hero";
		playerClass.maxHP = 150;
		playerClass.maxMP = 300;
		playerClass.strength = 10;
		playerClass.vitality = 20;
		playerClass.intelligence = 50;
		playerClass.wisdom = 40;
		playerClass.playerSprite = classSprites[1];
		GameManager.instance.classList.Add (playerClass);

	}

}
