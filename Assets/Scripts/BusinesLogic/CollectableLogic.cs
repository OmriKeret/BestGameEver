using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class CollectableLogic : MonoBehaviour {
    //dictionaries to rally data
    public int numOfPowerUpsToPowerUp;
    Dictionary<CollectableTypes, int> collectables;
 //   Dictionary<CollectableTypes, Action> powerUpActions;
   // Dictionary<CollectableTypes, GameObject> collectablesIcon;
    Dictionary<CollectableTypes, GameObject> collectablesSprite;

    //GUI
    public GameObject powerUpContainer;
    public float width;
    public Text powerUPText;
    private char powerUpChar = 'o';

    //superHit
    public float superHitTime = 1f;
    private int count;
    private int originalStr;


    //prefabs
    public GameObject _CoinPrefab;

    //general
    TouchInterpeter touch;
    MovmentLogic movmentLogic;
    AnimationLogic animationLogic;
    GameObject character;
    PlayerStatsLogic playerStatsLogic;
    MissionLogic missionLogic;
    SoundLogic soundLogic;
    // Use this for initialization

    void Start()
    {
        //TODO: SET ASSETST
        //  _BubblePowerUpIcon = Resources.Load("stupidL") as GameObject;
        soundLogic = this.gameObject.GetComponent<SoundLogic>();
        missionLogic = this.gameObject.GetComponent<MissionLogic>();
       // playerStatsLogic = this.gameObject.GetComponent<PlayerStatsLogic>();
       // powerUPText = GameObject.Find("PowerUpText").GetComponent<Text>();
        touch = GameObject.Find("TouchInterpter").GetComponent<TouchInterpeter>();
        movmentLogic = this.gameObject.GetComponent<MovmentLogic>();
        animationLogic = this.gameObject.GetComponent<AnimationLogic>();
        character = GameObject.Find("PlayerManager");
        _CoinPrefab = Resources.Load("Coin") as GameObject;
        BuildDictionary();

    }


    public void GotCollactable(CollectableTypes collactable)
    {
        missionLogic.addCollactable(collactable);
        collectables[collactable]++;

    }


    internal void generatCoin()
    {
        GameObject pref;
        if (collectablesSprite.TryGetValue(CollectableTypes.COIN, out pref))
        {
            var vec = RandomPlaceInsideScreenToPowerUp();
            var sprite = Instantiate(pref, vec, Quaternion.identity) as GameObject;
            sprite.GetComponent<CoinTrigger>().Set();
        }
    }
    private Vector3 RandomPlaceInsideScreenToPowerUp()
    {
        var dist = (this.transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        var buttomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

        return new Vector3((float)UnityEngine.Random.Range(leftBorder, rightBorder), (float)UnityEngine.Random.Range(buttomBorder + 4, topBorder - 2), 0f);

    }
    private bool insideScreen(Transform t)
    {
        var dist = (t.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        var buttomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        // teleport me to the other side of the screen when I reach the edge
        if (t.position.x > rightBorder)
        {
            return false;
        }
        if (t.position.x < leftBorder)
        {
            return false;
        }
        // teleport me to the top or buttom
        if (t.position.y > topBorder)
        {
            return false;
        }
        if (t.position.y < buttomBorder)
        {
            return false;
        }
        return true;
    }

    private void BuildDictionary()
    {
        collectables = new Dictionary<CollectableTypes, int> 	
        {
           {CollectableTypes.COIN, 0}
         };

        collectablesSprite = new Dictionary<CollectableTypes, GameObject> 	
        {
           {CollectableTypes.COIN, _CoinPrefab}
         };
    }
}
