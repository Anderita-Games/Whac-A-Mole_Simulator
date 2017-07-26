#pragma strict
var HighScore : UnityEngine.UI.Text;

function Update () {
	HighScore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
}

function StartGame () {
	Application.LoadLevel("Game");
}

function ExitGame () {
	Application.Quit();
}