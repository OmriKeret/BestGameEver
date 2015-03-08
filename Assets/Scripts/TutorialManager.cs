using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	bool _clickEnable;
	int _textCounter;
	public Text _textToDisplay;

	// Use this for initialization
	void Start () {
		_clickEnable = true;
		_textCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (_clickEnable && Input.GetButtonDown ("Fire1")) {
			_textCounter++;
			_clickEnable = false;
				}

		switch (_textCounter) {
		case 0:_textToDisplay.text = ("Tap anywhere to dash"); break;
		case 1:_textToDisplay.text = ("You can tap again to double dash"); break;
				}
	
	}
}
