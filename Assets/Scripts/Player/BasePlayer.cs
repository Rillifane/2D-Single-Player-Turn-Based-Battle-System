using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePlayer : MonoBehaviour
{

	public string playerName, playerDescription;
	public int maxHP, currentHP, maxMP, currentMP, strength, vitality, intelligence, wisdom;
	public Sprite playerSprite;
	private int attackDamageToDeal, abilityDamageToDeal;
	private float damageVariance;
	public bool playerDeath = false;
	private int damageToDeal;

	// Use this for initialization
	void Start ()
	{

		currentHP = maxHP;
		currentMP = maxMP;
		//handles assigning the player character panel with its associated stats and character portrait image when being created.
		this.transform.Find ("CharacterPanel/StatsPanel/HPPanel/MaxHP").GetComponent<Text> ().text = maxHP.ToString();
		this.transform.Find ("CharacterPanel/StatsPanel/HPPanel/CurrentHP").GetComponent<Text> ().text = currentHP.ToString();
		this.transform.Find ("CharacterPanel/StatsPanel/MPPanel/MaxMP").GetComponent<Text> ().text = maxMP.ToString();
		this.transform.Find ("CharacterPanel/StatsPanel/MPPanel/CurrentMP").GetComponent<Text> ().text = currentMP.ToString();
		this.transform.Find ("CharacterPanel/CharacterPortrait").GetComponent<Image> ().sprite = playerSprite;

	}

	// Update is called once per frame
	void Update ()
	{
		//updates the player stats every frame, could potentially be adjusted to be done in a different way as an update like this
		//isn't really needed to be done every frame as it only really needs to be updated when an event / action occurs by the player
		//or to the player.
		this.transform.Find ("CharacterPanel/StatsPanel/HPPanel/MaxHP").GetComponent<Text> ().text = maxHP.ToString();
		this.transform.Find ("CharacterPanel/StatsPanel/HPPanel/CurrentHP").GetComponent<Text> ().text = currentHP.ToString();
		this.transform.Find ("CharacterPanel/StatsPanel/MPPanel/MaxMP").GetComponent<Text> ().text = maxMP.ToString();
		this.transform.Find ("CharacterPanel/StatsPanel/MPPanel/CurrentMP").GetComponent<Text> ().text = currentMP.ToString();
	}

	//function used to handle the player taking damage.
	public void takeDamage (int damageToTake)
	{

		damageToTake = Mathf.Min (damageToTake, currentHP);
		currentHP -= damageToTake;

		if (currentHP <= 0) {

			playerDeath = true;

		}
		Debug.Log ("Player modified damage : " + damageToTake);
	}

	//function used to heal the player only if the current health of the player is greater than 0 but also less then max health
	//if current health goes over max health, current health is set to the maximum player health.
	public void healDamage (int healToTake)
	{

		if ((currentHP < maxHP) && (currentHP > 0)) {

			healToTake = Mathf.Min (healToTake, maxHP);

			currentHP += healToTake;
		} else {

			healToTake = 0;
		}
		if (currentHP > maxHP) {

			currentHP = maxHP;
		}
	}
}
