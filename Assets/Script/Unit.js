#pragma strict

function Start () {
	DEATH_AGE();
}

function Update () {
	if (PlayerPrefs.GetString("Remove") == "GAMEOVER") {
		Destroy(gameObject);
	}
}

function DEATH_AGE () {
	yield WaitForSeconds (5);
	Destroy(gameObject);
}

function Death_CLICKED () {
	if (PlayerPrefs.GetString("Remove") != "GAMEOVER") {
		PlayerPrefs.SetInt("Score_Current", PlayerPrefs.GetInt("Score_Current") + 1);
		PlayerPrefs.SetInt("MoleTotal", PlayerPrefs.GetInt("MoleTotal") - 1);
		PlayerPrefs.SetString("Remove", gameObject.name);
		Destroy(gameObject);
	}
}