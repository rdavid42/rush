using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour {


	public playerGestion	player;
	public Text				weaponName;
	public Text				ammoNumber;
	public Image			weaponSprite;
	// Use this for initialization
	void Start () {

	}


	// Update is called once per frame
	void Update () {
		if (player.currentWeapon != null)
		{
			weaponName.text = player.currentWeapon.name;
			ammoNumber.text = player.currentWeapon.ammoCount.ToString() + " ammo";
			weaponSprite.gameObject.SetActive(true);
			weaponSprite.sprite = player.pickedUpWeapon.GetComponent<SpriteRenderer>().sprite;
		}
		else
		{
			weaponName.text = "No weapon";
			ammoNumber.text = "";
			weaponSprite.gameObject.SetActive(false);
		}
	}
}
