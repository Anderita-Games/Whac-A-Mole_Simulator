#pragma strict
import System.Collections.Generic;

var Spawn_Area : RectTransform;
var Canvas_Area : RectTransform;
var Canvas : GameObject;
var Conversion_Rate : float = .38437502402;
var Mole_Positions : List.<String>;
var Time_Left : int = 60;
var Time_Is_Out : boolean = false;
var Mole_Original : GameObject;
var Mole_Clone : GameObject;
var Array_Input_Count : int = 1;

var Title : UnityEngine.UI.Text;
var Score_Current : UnityEngine.UI.Text;
var Score_High : UnityEngine.UI.Text;
var Time_Left_Text : UnityEngine.UI.Text;

var Button_Restart : GameObject;
var Button_Quit : GameObject;

function Start () {
	Spawn_Area.sizeDelta.y = Canvas_Area.sizeDelta.y * Conversion_Rate;
	Spawn_Area.localPosition.y = (Canvas_Area.sizeDelta.y / -2) + (Spawn_Area.sizeDelta.y / 2);
	PlayerPrefs.SetInt("MoleTotal", 0);
	PlayerPrefs.SetInt("Score_Current", 0);
	PlayerPrefs.SetString("Remove", null);
	Button_Restart.SetActive(false);
	Button_Quit.SetActive(false);
	CountDown();
	Spawn_The_Moles();
}

function Update () {
	if (PlayerPrefs.GetString("Remove") != null && PlayerPrefs.GetString("Remove") != "GAMEOVER") {
		Mole_Positions.Remove(PlayerPrefs.GetString("Remove"));
		PlayerPrefs.SetString("Remove", null);
	}
	if (PlayerPrefs.GetInt("Score_Current") > PlayerPrefs.GetInt("Score_High")) {
		PlayerPrefs.SetInt("Score_High", PlayerPrefs.GetInt("Score_Current"));
	}
	Score_Current.text = "Moles Killed: " + PlayerPrefs.GetInt("Score_Current");
	Score_High.text = "Most Moles Killed: " + PlayerPrefs.GetInt("Score_High");
	Time_Left_Text.text = "Time Left: " + Time_Left;
}

function Spawn_The_Moles () {
	var Row_Total : int = 3;
	var Column_Total : int = 7;
	var Row_Current : int = 1;
	var Column_Current : int = 1;
	var Spawn_Area_Y : float = Spawn_Area.sizeDelta.y;
	while (Time_Is_Out != true) {
		var Moles_To_Spawn : int = Random.Range(1, 4);
		while (Moles_To_Spawn > 0 && PlayerPrefs.GetInt("MoleTotal") != Row_Total * Column_Total) {
			Row_Current = Random.Range(1, Row_Total + 1);
			Column_Current = Random.Range(1, Column_Total + 1);
			while (Mole_Positions.Contains(Row_Current + ":" + Column_Current)) {
				Row_Current = Random.Range(1, Row_Total + 1);
				Column_Current = Random.Range(1, Column_Total + 1);
			}
			var Clone_X : float = (Canvas_Area.sizeDelta.x / Column_Total) * (Column_Current - .5) - (Canvas_Area.sizeDelta.x / 2);
			var Clone_Y : float = (Canvas_Area.sizeDelta.y / -2) + (Spawn_Area_Y / Row_Total) * (Row_Total - Row_Current + .5);
			Mole_Clone = Instantiate(Mole_Original);
			Mole_Clone.transform.SetParent(Canvas.transform);
			Mole_Clone.transform.localPosition = Vector3(Clone_X, Clone_Y, 0);
			Mole_Clone.transform.localScale = Vector3(1, 1, 1);
			Mole_Clone.name = Row_Current + ":" + Column_Current;

			Mole_Positions.Add(Mole_Clone.name);
			Array_Input_Count++;
			PlayerPrefs.SetInt("MoleTotal", PlayerPrefs.GetInt("MoleTotal") + 1);
			Moles_To_Spawn--;
		}
		yield WaitForSeconds (2); //Time Intervals for Mole Spawns (Random?)
	}
}

function CountDown () {
	while (Time_Left > 0) {
		Time_Left--;
		yield WaitForSeconds (1);
	}
	Time_Is_Out = true;
	PlayerPrefs.SetString("Remove", "GAMEOVER");
	Button_Restart.SetActive(true);
	Button_Quit.SetActive(true);
	Title.text = "Game Over";
}

function RestartGame () {
	Application.LoadLevel("Game");
}

function ExitGame () {
	Application.Quit();
}