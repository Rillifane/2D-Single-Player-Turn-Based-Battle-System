using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculations
{
	private int modifiedDamage;
	private float damageVariance;

	//function to handle physical damage calculations for both player gameobjects and enemy game objects
	//returns a value to be handled by either object and/or to be sent to the gamemanager to display the damage on screen.
	public int dealPhysicalDamage (int attackerStr, int abilityDamage, int defenderVit)
	{
		modifiedDamage = (int)(((attackerStr + abilityDamage) - (defenderVit / 2)) * DamageVariance ());

		if (modifiedDamage < 0) {
			modifiedDamage = 0;
		}
		Debug.Log ("Physical damage : " + modifiedDamage);
		return modifiedDamage;
	}

	//function to handle magical damage calculations for both player gameobjects and enemy game objects
	//returns a value to be handled by either object and/or to be sent to the gamemanager to display the damage on screen.
	public int dealMagicalDamage (int attackerInt, int abilityDamage, int defenderWis)
	{
		modifiedDamage = (int)(((attackerInt + abilityDamage) - (defenderWis / 2)) * DamageVariance ());

		if (modifiedDamage < 0) {
			modifiedDamage = 0;
		}
		Debug.Log ("Magical damage : " + modifiedDamage);
		return modifiedDamage;
	}

	//function to handle healing calculations for the player gameobject
	//returns a value to be handled by either object and/or to be sent to the gamemanager to display the healing on screen.
	public int healDamage (int attackersWis, int abilityDamage)
	{

		modifiedDamage = (int)((attackersWis + abilityDamage) * DamageVariance ());
		Debug.Log ("Player heal : " + modifiedDamage);
		return modifiedDamage;

	}

	//function to help create a damage and healing range for abilities.
	private float DamageVariance ()
	{
		damageVariance = Random.Range (0.9f, 1.1f);
		return damageVariance;
	}
}
