using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPBarLogic : MonoBehaviour {

    public Image progressBarEmpty;
    public Image progressBarFull;
    private int MAX_HP;
    private int currentHp;

    //GUI fill logic
    public float changeColorDuration = 0.2f;
    public float speed = 0.3f; //speed for life to go down
    float barDisplay = 1f;
    float currentRatio = 0f;
    bool shouldChange;


    Animator anim;

    //logic connections
    private PlayerStatsLogic statsLogic;

    void Awake()
    {
        var HPEmpty = GameObject.Find("HealthHolder/HealthEmpty");
        progressBarFull = GameObject.Find("HealthHolder/Health").GetComponent<Image>();
        progressBarEmpty = HPEmpty.GetComponent<Image>();
        statsLogic = this.gameObject.GetComponent<PlayerStatsLogic>();
        anim = HPEmpty.GetComponent<Animator>();
    }

    private void updateRatio()
    {      
         currentRatio = (float)((float)currentHp / (float)MAX_HP);
         if (progressBarFull.fillAmount > currentRatio)
         {
             progressBarFull.fillAmount = currentRatio;
             anim.SetTrigger("hit");
         }
         else
         {
             setShouldChange();
         }
        //Debug.Log("RATIO IS:" + currentRatio);
        //Debug.Log("currentDisplay is: " + barDisplay);
    }

    public void setShouldChange()
    {
        shouldChange = true;
    }

    public void updateCurrentHP(int hp)
    {
        Debug.Log("updatedCurrentHP");

        currentHp = hp;
        updateRatio();
    }

    public void setMaximumHP(int hp)
    {
        MAX_HP = hp;
    }

    void Update()
    {
        if (shouldChange)
        {
            if (barDisplay > 1)
            {
                barDisplay = 1f;
                return;
            }
            if (barDisplay < 0)
            {
                barDisplay = 0f;
                return;
            }
            if (barDisplay < currentRatio)
            {
                barDisplay += speed * Time.deltaTime;
                if (barDisplay >= currentRatio)
                {
                    barDisplay = currentRatio;
                    shouldChange = false;
                }
                progressBarEmpty.fillAmount = barDisplay;
                progressBarFull.fillAmount = barDisplay;
            }
            else
            {
                barDisplay -= speed * Time.deltaTime;
                if (barDisplay <= currentRatio)
                {
                    barDisplay = currentRatio;
                    shouldChange = false;
                }
                progressBarEmpty.fillAmount = barDisplay;
            }
        }


    }
}
