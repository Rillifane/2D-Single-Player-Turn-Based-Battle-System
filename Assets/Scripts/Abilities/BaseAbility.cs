using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AbilityElement{
	fire,
	water,
	wind,
	earth,
	none
}

public enum AbilityType{

	physical,
	magical
}

public enum AttackType{

	attack,
	skill,
	heal
}

public class BaseAbility : MonoBehaviour {

	public string abilityName;
	public int abilityDamage;
	public int mpCost;
	public string element;
	[Multiline]
	public string abilityDescription;
	public string type;
	public Sprite abilitySprite;

	public AbilityElement abilityElement;
	public AbilityType abilityType;

	// Use this for initialization
	void Start () {

		//assigns the ability name to the text, and the corresponding sprite in the game object spawned from the prefab
		this.transform.Find ("AbilityText").GetComponent<Text> ().text = abilityName;
		this.transform.Find ("AbilityIcon").GetComponent<Image> ().sprite = abilitySprite;
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	//function called when ability is clicked, assigns it as the chosen ability for the players turn
	//then changes to the target selection for the player
	public void abilityClicked(){

		GameManager.instance.handleTurn.abilityChosen = this.gameObject;
		GameManager.instance.handleTurn.attackType = AttackType.skill;
		Debug.Log (gameObject.name);
		GameManager.instance.currentBattleState = GameManager.BattleStates.PlayerTargetChoice;
	}
}
