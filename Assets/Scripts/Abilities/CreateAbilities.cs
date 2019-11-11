using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAbilities : MonoBehaviour {

	public GameObject abilityPrefab;
	public List<Sprite> abilityIcons;
	public Transform targetTransform;

	// Use this for initialization
	void Start () {

		GameObject abilityGameObject;
		BaseAbility ability;

		//create a fire spell
		//instantiates a new ability gameobject from the prefab then accesses the BaseAbility script in the newly spawned gameobject
		//sets the transform location of the gameobject and sets the world position stays to false
		//sets the gameobject state to active/inactive
		abilityGameObject = (GameObject)Instantiate (abilityPrefab);
		ability = abilityGameObject.GetComponent<BaseAbility> ();
		ability.transform.SetParent (targetTransform, false);
		ability.gameObject.SetActive (false);


		//assigns the corresponding values to this game object
		ability.abilityName = "Fire";
		ability.abilityDescription = "A fire Spell.";
		ability.abilityDamage = 40;
		ability.mpCost = 5;
		ability.element = "Fire";
		ability.abilityType = AbilityType.magical;
		ability.abilitySprite = abilityIcons[1];
		//function call to assign this gameobject to a dictionary
		GameManager.instance.AddToAbilityDictionary (AbilityElement.fire, ability);


		//create a wind spell
		abilityGameObject = (GameObject)Instantiate (abilityPrefab);
		ability = abilityGameObject.GetComponent<BaseAbility> ();
		ability.transform.SetParent (targetTransform, false);
		ability.gameObject.SetActive (false);

		ability.abilityName = "Wind";
		ability.abilityDescription = "A wind spell.";
		ability.abilityDamage = 40;
		ability.mpCost = 5;
		ability.element = "Wind";
		ability.abilityType = AbilityType.magical;
		ability.abilitySprite = abilityIcons[1];
		GameManager.instance.AddToAbilityDictionary (AbilityElement.wind, ability);


		//create a water spell
		abilityGameObject = (GameObject)Instantiate (abilityPrefab);
		ability = abilityGameObject.GetComponent<BaseAbility> ();
		ability.transform.SetParent (targetTransform, false);
		ability.gameObject.SetActive (false);

		ability.abilityName = "Water";
		ability.abilityDescription = "A water spell.";
		ability.abilityDamage = 40;
		ability.mpCost = 5;
		ability.element = "Water";
		ability.abilityType = AbilityType.magical;
		ability.abilitySprite = abilityIcons[1];
		GameManager.instance.AddToAbilityDictionary (AbilityElement.water, ability);


		//create a earth spell
		abilityGameObject = (GameObject)Instantiate (abilityPrefab);
		ability = abilityGameObject.GetComponent<BaseAbility> ();
		ability.transform.SetParent (targetTransform, false);
		ability.gameObject.SetActive (false);

		ability.abilityName = "Earth";
		ability.abilityDescription = "A earth spell.";
		ability.abilityDamage = 40;
		ability.mpCost = 5;
		ability.element = "Earth";
		ability.abilityType = AbilityType.magical;
		ability.abilitySprite = abilityIcons[1];
		GameManager.instance.AddToAbilityDictionary (AbilityElement.earth, ability);


		//create a healing spell
		abilityGameObject = (GameObject)Instantiate (abilityPrefab);
		ability = abilityGameObject.GetComponent<BaseAbility> ();
		ability.transform.SetParent (targetTransform, false);
		ability.gameObject.SetActive (true);

		ability.abilityName = "Heal";
		ability.abilityDescription = "A healing spell.";
		ability.abilityDamage = 40;
		ability.mpCost = 5;
		ability.element = "None";
		ability.abilityType = AbilityType.magical;
		ability.abilitySprite = abilityIcons[1];
		GameManager.instance.AddToAbilityDictionary (AbilityElement.none, ability);


		//create a bash attack
		abilityGameObject = (GameObject)Instantiate (abilityPrefab);
		ability = abilityGameObject.GetComponent<BaseAbility> ();
		ability.transform.SetParent (targetTransform, false);
		ability.gameObject.SetActive (true);

		ability.abilityName = "Bash";
		ability.abilityDescription = "A melee bash attack.";
		ability.abilityDamage = 20;
		ability.mpCost = 0;
		ability.element = "None";
		ability.abilityType = AbilityType.physical;
		ability.abilitySprite = abilityIcons[0];
		GameManager.instance.AddToAbilityDictionary (AbilityElement.none, ability);


		//create a swipe attack
		abilityGameObject = (GameObject)Instantiate (abilityPrefab);
		ability = abilityGameObject.GetComponent<BaseAbility> ();
		ability.transform.SetParent (targetTransform, false);
		ability.gameObject.SetActive (true);

		ability.abilityName = "Swipe";
		ability.abilityDescription = "A swipe with claws.";
		ability.abilityDamage = 20;
		ability.mpCost = 0;
		ability.element = "None";
		ability.abilityType = AbilityType.physical;
		ability.abilitySprite = abilityIcons[0];
		GameManager.instance.AddToAbilityDictionary (AbilityElement.none, ability);

	}

}
