using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseEnemy : MonoBehaviour
{

	public string enemyName, enemyDescription;
	public int maxHP, currentHP, maxMP, currentMP, strength, vitality, intelligence, wisdom;
	public Sprite enemySprite;
	private int damageToDeal;

	//place holder ability for enemies to use, needs to be expanded upon.
	private BaseAbility swipe;

	// Use this for initialization
	void Start ()
	{

		currentHP = maxHP;
		currentMP = maxMP;

		//assigns the enemy name to the text, and the corresponding sprite in the game object spawned from the prefab
		//enemy name is mostly used for debugging purposes to make sure the correct sprite is spawned for the enemy and if it doesn't spawn
		//it can be used to check which enemy it should be. Can be removed, but will need the text label removing from the prefab
		this.transform.Find ("Name").GetComponent<Text> ().text = enemyName;
		this.transform.Find ("Image").GetComponent<Image> ().sprite = enemySprite;

		swipe = GameObject.Find ("Swipe").GetComponent<BaseAbility> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	//function to take damage and update health, MathF.Min is used to get limit any issues that may occur with
	//negative numbers by using the smaller of the 2 values.
	//enemydeath function is called to make the game object destory itself when health becomes 0
	public void takeDamage (int damageToTake)
	{

		damageToTake = Mathf.Min (damageToTake, currentHP);

		currentHP -= damageToTake;

		if (currentHP <= 0) {
			Debug.Log ("dead");
			EnemyDeath ();
		}
	}

	//function to make the enemy attack, calls an instance of handle turn and assigns the corresponding values to handle turn
	//damage value is then sent to the instance of the player for the player to lose the correct amount of health
	//damage value is also sent to be displayed on screen in the feedback area.
	public void EnemyAttack ()
	{

		HandleTurn enemyAttack = new HandleTurn ();
		enemyAttack.Attacker = this.enemyName;
		enemyAttack.AttackersGameObject = this.gameObject;
		//can be modified to get a random player from the list of players in play, however there is only currently 1 player being spawned and used so
		//only the first element in the list is being chosen
		enemyAttack.AttackersTarget = GameManager.instance.playersInPlay [0];

		damageToDeal = GameManager.instance.damageCalc.dealPhysicalDamage (strength, swipe.abilityDamage, enemyAttack.AttackersTarget.GetComponent<BasePlayer> ().vitality);

		GameManager.instance.player.takeDamage (damageToDeal);

		Debug.Log ("player damage is: " + damageToDeal);
		GameManager.instance.displayFeedBackText (enemyName, damageToDeal, "damagePlayer");
		Debug.Log ("Enemy Damage Dealt to Player : " + damageToDeal);

	}

	//function for when the enemy game object is clicked on during the player targeting turn
	//this is used to assign the correct game object for the target when the players turn is being resolved.
	public void EnemySelected ()
	{

		if (GameManager.instance.playerTargetTurn == true) {

			GameManager.instance.handleTurn.AttackersTarget = this.gameObject;
			Debug.Log (gameObject.name);
			GameManager.instance.playerTargetTurn = false;
			GameManager.instance.currentBattleState = GameManager.BattleStates.TurnResolve;
		}

	}


	//function used to make the enemy destory itself
	private void EnemyDeath ()
	{
		Destroy (this.gameObject);
	}
}
