using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemies : MonoBehaviour {

	public GameObject enemyPrefab;
	public List<Sprite> enemySprite;
	public Transform targetTransform;

	// Use this for initialization
	void Start () {

		GameObject enemyObject;
		BaseEnemy enemy;


		//create a Dragon Enemy
		//instantiates a new enemy gameobject from the prefab then accesses the BaseEnemy script in the newly spawned gameobject
		//sets the transform location of the gameobject and sets the world position stays to false
		//sets the gameobject state to active/inactive
		enemyObject = (GameObject)Instantiate (enemyPrefab);
		enemy = enemyObject.GetComponent<BaseEnemy> ();
		enemy.transform.SetParent (targetTransform, false);
		enemy.gameObject.SetActive (false);

		//assigns the corresponding values to this game object
		enemy.enemyName = "Dragon";
		enemy.enemyDescription = "A dragon";
		enemy.maxHP = 300;
		enemy.maxMP = 100;
		enemy.strength = 30;
		enemy.vitality = 30;
		enemy.intelligence = 30;
		enemy.wisdom = 30;
		enemy.enemySprite = enemySprite[0];
		enemy.gameObject.name = enemy.enemyName;
		//function call to assign this gameobject to a dictionary and to a list dictionary is used to hold all enemy objects, the list is used to
		//to spawn random enemies for each combat due to dictionaries not being easily accessed by an index number. Could possibily be adjusted to use
		//a better method of accessing a random enemy to spawn.
		GameManager.instance.enemyDictionary.Add (enemy.enemyName, enemy);
		GameManager.instance.enemyList.Add (enemy);

		//create a Slime Enemy
		enemyObject = (GameObject)Instantiate (enemyPrefab);
		enemy = enemyObject.GetComponent<BaseEnemy> ();
		enemy.transform.SetParent (targetTransform, false);
		enemy.gameObject.SetActive (false);

		enemy.enemyName = "Slime";
		enemy.enemyDescription = "A slime";
		enemy.maxHP = 150;
		enemy.maxMP = 50;
		enemy.strength = 20;
		enemy.vitality = 40;
		enemy.intelligence = 20;
		enemy.wisdom = 40;
		enemy.enemySprite = enemySprite[1];
		enemy.gameObject.name = enemy.enemyName;
		GameManager.instance.enemyDictionary.Add (enemy.enemyName, enemy);
		GameManager.instance.enemyList.Add (enemy);

		//create a Werewolf Enemy
		enemyObject = (GameObject)Instantiate (enemyPrefab);
		enemy = enemyObject.GetComponent<BaseEnemy> ();
		enemy.transform.SetParent (targetTransform, false);
		enemy.gameObject.SetActive (false);

		enemy.enemyName = "Werewolf";
		enemy.enemyDescription = "A werewolf";
		enemy.maxHP = 200;
		enemy.maxMP = 50;
		enemy.strength = 30;
		enemy.vitality = 15;
		enemy.intelligence = 30;
		enemy.wisdom = 15;
		enemy.enemySprite = enemySprite[2];
		enemy.gameObject.name = enemy.enemyName;
		GameManager.instance.enemyDictionary.Add (enemy.enemyName, enemy);
		GameManager.instance.enemyList.Add (enemy);

	}

	// Update is called once per frame
	void Update () {

	}
}
