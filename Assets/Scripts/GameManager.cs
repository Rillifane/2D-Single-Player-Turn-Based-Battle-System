using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

	public static GameManager instance;

	public enum MenuStates
	{
		Commands,
		Abilities,
		Selection,
		CharacterSelection,
		TakeTurn,
		GameOver,
		None
	}

	public enum BattleStates
	{

		BattlePrep,
		BattleStart,
		TurnStart,
		PlayerTurn,
		PlayerTargetChoice,
		TurnResolve,
		EnemyTurn,
		TurnEnd,
		Lose,
		Win

	}

	public GameObject enemyArea;
	public GameObject abilityArea;

	public GameObject commandMenu;
	public GameObject abilityMenu;
	public GameObject characterMenu;
	public GameObject feedbackMenu;
	public GameObject gameOverMenu;

	public MenuStates currentMenuState;
	public BattleStates currentBattleState;

	public Dictionary<string, BaseAbility> abilityDictionary;
	public Dictionary<AbilityElement, List<BaseAbility>> abilityElementsDictionary;
	public List<BasePlayer> classList;

	public Dictionary<string, BaseEnemy> enemyDictionary;
	public List<BaseEnemy> enemyList;

	public BasePlayer player;
	public GameObject playerArea;

	public Text maxHP, maxMP, currentHP, currentMP, feedbackText, gameOverText;
	public Image playerPortrait;

	public List<GameObject> spawnedEnemies;
	public List<GameObject> playersInPlay;

	public HandleTurn handleTurn;

	public bool playerTargetTurn = false;
	public bool gameOverState = false;

	public DamageCalculations damageCalc;

	//called before Start() method, dictionaries, lists etc needs to be created here first before other classes can access them
	//or else they will have nothing to add the objects to and will end up with empty dictionaries and lists.
	void Awake ()
	{
		//create a static instance of this game object so it can be accessed by every other class using GameManager.instance.
		//saves having to make a reference to the game manager in each new class or having to create a new instance to pass values to/from.
		//note would have to do proper singleton coding to ensure mutliple of these gamemangers are not created if moving to/from game scenes.
		instance = this;

		abilityElementsDictionary = new Dictionary<AbilityElement, List<BaseAbility>> ();
		abilityDictionary = new Dictionary<string, BaseAbility> ();
		enemyDictionary = new Dictionary<string, BaseEnemy> ();

		//used to create a dictionary where the Value section of the dictionary are all the ability element types from the AbilityElement enum
		foreach (AbilityElement element in Enum.GetValues(typeof(AbilityElement))) {
			abilityElementsDictionary [element] = new List<BaseAbility> ();
		}

		spawnedEnemies = new List<GameObject> ();
		playersInPlay = new List<GameObject> ();
		feedbackText.text = "";

		damageCalc = new DamageCalculations ();
	}

	// Use this for initialization
	//assigns the starting menu and battle stats
	void Start ()
	{

		currentMenuState = MenuStates.Commands;
		currentBattleState = BattleStates.BattlePrep;

	}
	
	// Update is called once per frame
	void Update ()
	{
		//state machine for the battlestates
		//could be simplifed and taken in to its own class some minor bugs appear with it being in the update method causing it to go to next turn even when the
		//player has died and been flagged for "game over".
		switch (currentBattleState) {
		case BattleStates.BattlePrep:
			assignPlayerSkills ();
			StartBattle ();
			break;
		case BattleStates.BattleStart:
			currentBattleState = BattleStates.PlayerTurn;
			break;
		case BattleStates.TurnStart:
			handleTurn = new HandleTurn ();
			break;
		case BattleStates.PlayerTurn:
			//player chooses his ability to use here
			handleTurn.Attacker = player.name;
			break;
		case BattleStates.PlayerTargetChoice:
			playerTargetTurn = true;
			currentMenuState = MenuStates.Selection;
			feedbackText.text = "Select Target.";
			break;
		case BattleStates.TurnResolve:
			playerTargetTurn = false;
			feedbackText.text = "";
			StartCoroutine ("DoPlayerTurn");
			currentBattleState = BattleStates.EnemyTurn;
			break;
		case BattleStates.EnemyTurn:
			currentMenuState = MenuStates.TakeTurn;
			StartCoroutine ("DoEnemyTurn");
			currentBattleState = BattleStates.TurnEnd;
			break;
		case BattleStates.TurnEnd:
			//handle any end of turn effects or check for enemy player deaths here
			//if all enemies and player are alive go back to start of players turn else go to win/lose state.
			if (player.playerDeath == true) {
				currentBattleState = GameManager.BattleStates.Lose;
			}
			if (player.playerDeath == false) {
				EnemyDeadCheck ();
				if (gameOverState == true) {

					currentBattleState = BattleStates.Win;

				} else {
					//Coroutine seems to be called before the player death is properly checked. Unknown how to fix it currently
					//might be yield wait timing related.
					StartCoroutine ("GoToNextTurn");
				}
			} 

			break;
		case BattleStates.Lose:
			currentMenuState = MenuStates.GameOver;
//			Debug.Log ("Game Over");
			gameOverMenu.SetActive (true);
			gameOverText.text = "Game Over";
			break;
		case BattleStates.Win:
			currentMenuState = MenuStates.GameOver;
//			Debug.Log ("Win");
			gameOverMenu.SetActive (true);
			gameOverText.text = "You Win";
			break;
		default:
			break;
		}

		//uses to handle which menus should be displayed or set to inactive.
		switch (currentMenuState) {
		case MenuStates.None:
			commandMenu.SetActive (false);
			abilityMenu.SetActive (false);
			characterMenu.SetActive (false);
			feedbackMenu.SetActive (false);
			gameOverMenu.SetActive (false);
			break;
		case MenuStates.Abilities:
			commandMenu.SetActive (true);
			abilityMenu.SetActive (true);
			characterMenu.SetActive (false);
			feedbackMenu.SetActive (false);
			gameOverMenu.SetActive (false);
			break;
		case MenuStates.CharacterSelection:
			commandMenu.SetActive (false);
			abilityMenu.SetActive (false);
			characterMenu.SetActive (true);
			feedbackMenu.SetActive (false);
			gameOverMenu.SetActive (false);
			break;
		case MenuStates.Commands:
			commandMenu.SetActive (true);
			abilityMenu.SetActive (false);
			characterMenu.SetActive (false);
			feedbackMenu.SetActive (false);
			gameOverMenu.SetActive (false);
			break;
		case MenuStates.Selection:
			commandMenu.SetActive (true);
			abilityMenu.SetActive (false);
			characterMenu.SetActive (false);
			feedbackMenu.SetActive (true);
			gameOverMenu.SetActive (false);
			break;
		case MenuStates.TakeTurn:
			commandMenu.SetActive (false);
			abilityMenu.SetActive (false);
			characterMenu.SetActive (false);
			feedbackMenu.SetActive (true);
			gameOverMenu.SetActive (false);
			break;
		case MenuStates.GameOver:
			commandMenu.SetActive (false);
			abilityMenu.SetActive (false);
			characterMenu.SetActive (false);
			feedbackMenu.SetActive (false);
			gameOverMenu.SetActive (true);
			break;
		}
	}

	//method to handle adding the ability game objects to the list and dictionary
	//dictionary adds abilities in abilityelement groups e.g. all fire ability elements would be grouped together for easy access of all fire type abilities.
	public void AddToAbilityDictionary (AbilityElement element, BaseAbility ability)
	{

		ability.gameObject.name = ability.abilityName;
		ability.abilityElement = element;
		abilityDictionary.Add (ability.abilityName, ability);
		abilityElementsDictionary [element].Add (ability);

	}


	//method to set up the start of the battle.
	public void StartBattle ()
	{
		currentMenuState = MenuStates.Commands;
		SetUpPlayer ();
		PrepareCombat ();
		SetInPlayObjects ();
		currentBattleState = BattleStates.BattleStart;

	}

	//method to choose a random number of enemies to spawn in and then randomly select which enemy type to spawn
	//currently the maximum for the range is set to 3, but can hold more. However the container and the method for displaying these gameobjects
	//would have to be adjusted to prevent overlapping or pushing objects off screen.
	public void PrepareCombat ()
	{
		int numberOfMonsters = Random.Range (1, 3);

		Debug.Log ("Number of Enemies : " + numberOfMonsters);

		for (int i = 0; i < numberOfMonsters; i++) {
				
			int monsterNumber = Random.Range (0, enemyList.Count);
			String enemiesToSpawn;
			enemiesToSpawn = (enemyList [monsterNumber]).enemyName;
			SpawnEnemies (enemiesToSpawn);
		}
	}

	public void SpawnEnemies (string enemyName)
	{

		BaseEnemy enemy = ((GameObject)Instantiate (enemyDictionary [enemyName].gameObject)).GetComponent<BaseEnemy> ();
		enemy.gameObject.tag = "Enemy";
		enemy.transform.SetParent (enemyArea.transform, false);
		enemy.gameObject.SetActive (true);


	}

	//method to instantiate a copy of the skills for player use and assigns them to the player skill window to be access by the user.
	//sets the game objects to be active.
	public void assignPlayerSkills ()
	{

		foreach (KeyValuePair<string,BaseAbility> abilityIndex in abilityDictionary) {
			if (abilityIndex.Value.abilityElement != AbilityElement.none) {
				BaseAbility ability = ((GameObject)Instantiate (abilityDictionary [abilityIndex.Value.abilityName].gameObject)).GetComponent<BaseAbility> ();
				ability.gameObject.tag = "Ability";
				ability.transform.SetParent (abilityArea.transform, false);
				ability.gameObject.SetActive (true);
			}
		}

	}

	//method to select a random player gameobject from the 3 in the classList.
	//adds the Player tag to the game object, while not currently used, can be useful for easily finding a group of player game objects in future with the
	//GameObject.FindGameObjectsWithTag method.
	//this method will have to be modified if more than 1 player needs to be spawned in to be used.
	public void SetUpPlayer ()
	{
		Debug.Log ("Class list :" + classList.Count);
		int playerSelect = Random.Range (0, classList.Count);
		player = ((GameObject)Instantiate (classList [playerSelect].gameObject)).GetComponent<BasePlayer> ();
		player.gameObject.name = "Player";
		player.gameObject.tag = "Player";
		player.gameObject.SetActive (true);
		player.transform.SetParent (playerArea.transform, false);
		Debug.Log ("Selected Player : " + playerSelect);
	}

	//method to add all enemy and player objects in play (active) to lists which can be used for targeting / random targeting
	//currently only really used by the enemies when attacking, can be used by a player if an ability with a random enemy target is created.
	public void SetInPlayObjects ()
	{

		spawnedEnemies.AddRange (GameObject.FindGameObjectsWithTag ("Enemy"));
		playersInPlay.AddRange (GameObject.FindGameObjectsWithTag ("Player"));

	}

	//Coroutine to handle taking the enemy turn.
	//checks if the enemyArea contains any children (gameobjects in this case) within it if there are none then it exits out
	//if children are present then it iterates through them, yielding for 0.5 seconds before performing an enemy attack and updating the player information on screen
	//if any damage happens to the player.
	public IEnumerator DoEnemyTurn ()
	{
		for (int i = 0; i < enemyArea.transform.childCount; i++) {
			if (player.playerDeath == false) {
				yield return new WaitForSeconds (0.5f);
				BaseEnemy enemy = enemyArea.transform.GetChild (i).GetComponent<BaseEnemy> ();
				enemy.EnemyAttack ();
			} else {
				yield return null;
			}
		}
	}

	//Coroutine to handle the player turn.
	//switch statement is used to select the right damage formula based on what the type of move was chosen by the player.
	//information about damage / healing is returned and sent to the method to handle displaying on screen text for feedback.
	public IEnumerator DoPlayerTurn ()
	{
		int damage = 0;

		BaseAbility chosenAbility = handleTurn.abilityChosen.GetComponent<BaseAbility> ();
		BaseEnemy chosenTarget = handleTurn.AttackersTarget.GetComponent<BaseEnemy> ();

		switch (handleTurn.attackType) {
		case AttackType.skill:
			damage = damageCalc.dealMagicalDamage (player.intelligence, chosenAbility.abilityDamage, chosenTarget.wisdom);
			player.currentMP -= chosenAbility.mpCost;
			chosenTarget.takeDamage (damage);
			displayFeedBackText (chosenTarget.enemyName, damage, "damageEnemy");
			break;
		case AttackType.attack:
			damage = damageCalc.dealPhysicalDamage (player.intelligence, chosenAbility.abilityDamage, chosenTarget.vitality);
			player.currentMP -= chosenAbility.mpCost;
			chosenTarget.takeDamage (damage);
			displayFeedBackText (chosenTarget.enemyName, damage, "damageEnemy");
			break;
		case AttackType.heal:
			damage = damageCalc.healDamage (player.wisdom, chosenAbility.abilityDamage);
			player.healDamage (damage);
			player.currentMP -= chosenAbility.mpCost;
			displayFeedBackText (player.name, damage, "healPlayer");
			break;
		}
		yield return new WaitForSeconds (0.5f);
	}

	//Coroutine to handle going to the next turn, seems to cause some issues when the player is defeated and the game over screen is active, can cause it to go the
	//next turn, unsure of fix might be related to the yield WaitForSeconds.
	public IEnumerator GoToNextTurn ()
	{
			yield return new WaitForSeconds (1.5f);
			currentBattleState = BattleStates.TurnStart;
			currentMenuState = MenuStates.Commands;
	}

	//method to handle displaying feedback text of damage / healing done by/to player and enemy gameobjects.
	public void displayFeedBackText (string name, int damage, string type)
	{
		switch (type) {
		case "damageEnemy":
			feedbackText.text = "You hit " + name + " for " + damage + " damage.";
			break;
		case "damagePlayer":
			feedbackText.text = name + " hit you for " + damage + " damage.";
			break;
		case "healPlayer":
			feedbackText.text = "You healed yourself for " + damage + " health.";
			break;
		}
	}

	//function to handle what actions the UI buttons will do when selected.
	public void commandButtons (string buttonClicked)
	{

		switch (buttonClicked) {

		case "attackBtn":
			AttackPicked ();
			break;
		case "skillsBtn":
			if (player.currentMP > 4) {
				currentMenuState = MenuStates.Abilities;
				Debug.Log ("Skills Button pressed");
			} else {
				feedbackText.text = "Not enough MP to use abilities.";
			}
			break;
		case "healBtn":
			HealPicked ();
			break;
		case "retryBtn":
			Retry ();
			break;
		case "exitBtn":
			Application.Quit ();
			break;

		}

	}

	//method used for handling regular attacks for the player.
	public void AttackPicked ()
	{
		handleTurn.abilityChosen = GameObject.Find ("Bash");
		handleTurn.attackType = AttackType.attack;
		Debug.Log (handleTurn.abilityChosen.name);
		currentBattleState = BattleStates.PlayerTargetChoice;
		Debug.Log ("Attack Button pressed");
		Debug.Log (currentBattleState);
	}

	//method used for handling healing for the player.
	public void HealPicked ()
	{
		if (player.currentMP > 4) {
			handleTurn.abilityChosen = GameObject.Find ("Heal");
			handleTurn.AttackersTarget = player.gameObject;
			handleTurn.attackType = AttackType.heal;
			currentBattleState = BattleStates.TurnResolve;
			Debug.Log (handleTurn.abilityChosen.name);
			Debug.Log ("Heal Button pressed");
			Debug.Log (currentBattleState);
		} else {

			feedbackText.text = "Not enough MP to heal.";
		}

	}

	//method used to reset the game to a new battle when the user either wins the combat or is defeated
	//all left over player and enemy game objects are destoryed and new ones created.
	//could potentially add in a variation were the user is asked if they want a new character or keep the current one, as well as asking the user
	//if they want to retry the same battle or have a new random battle.
	public void Retry ()
	{
		if (gameOverState == true || player.playerDeath == true) {
			gameOverState = false;
			player.playerDeath = false;
			currentMenuState = MenuStates.None;
			Destroy (player.gameObject);

			for (int i = 0; i < enemyArea.transform.childCount; i++) {
				Destroy (enemyArea.transform.GetChild (i));
			}
			StartBattle ();
		}
	}

	//method to check if enemies are all dead.
	public void EnemyDeadCheck ()
	{

		if (enemyArea.transform.childCount <= 0) {

			gameOverState = true;

		}

	}
}
