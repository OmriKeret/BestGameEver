using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour {

	public void StartGame()
	{
		Application.LoadLevel("Prototype");
	}
	public void ToMainMenu()
	{
		Application.LoadLevel("Main Menu");
	}
	public void ToStore()
	{
		Application.LoadLevel("Store");
	}

	
}
