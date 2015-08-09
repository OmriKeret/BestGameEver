using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupLogic : MonoBehaviour
{

    #region Up
    private Vector3 UpLocation;
    public float UpTime = 0.01f;

    void upPopup()
    {
        LeanTween.move(this.gameObject, UpLocation, UpTime).setOnComplete(() =>
        {

        });   

    }
    #endregion

    #region Down
    private Vector3 DownLocation;
    public float DownTime = 10;

    void downPopup()
    {
        Debug.Log("Popup go down " + this.gameObject.transform.position.y +" , "+ DownLocation.y);
        LeanTween.move(this.gameObject, DownLocation, DownTime).setIgnoreTimeScale(true).setOnComplete(() =>
        {
            
        });   
    }
    #endregion

    Text text;

    void Start()
    {
        UpLocation = transform.position;
        UpTime = 0.4f;
        DownLocation = UpLocation - new Vector3(0, 35, 0);
        DownTime = 0.4f;
        text = GetComponentInChildren<Text>();
    }

    void setText(string message)
    {
        text.text = message;
    }

    public System.Object showPopup(params System.Object[] obj)
    {
        StartCoroutine(startPopupRutine(obj[0].ToString()));
        return null;
    }

    IEnumerator startPopupRutine(string message)
    {
        setText(message);
        downPopup();
        yield return new WaitForSeconds(2);
        upPopup();
    }

    
}
