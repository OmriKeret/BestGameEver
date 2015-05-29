using UnityEngine;
using System.Collections;

public class TestPlay : MonoBehaviour {

	public UnityEngine.Sprite[] sprites;

	// Use this for initialization
	void Start () {
		
		LeanTween.play(gameObject.GetComponent<RectTransform>(), sprites).setLoopPingPong();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
