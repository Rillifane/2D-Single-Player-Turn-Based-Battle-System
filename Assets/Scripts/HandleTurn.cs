using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//serializable so it can be seen in unity inspector when applied to a gameobject.
//code obtained and adapted from (xOctoManx, 2016).
[System.Serializable]
public class HandleTurn {

	public string Attacker;
	public GameObject AttackersGameObject;
	public GameObject abilityChosen;
	public GameObject AttackersTarget;
	public AttackType attackType;

}
