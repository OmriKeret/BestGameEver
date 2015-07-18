using UnityEngine;
using System.Collections;
using Soomla.Profile;
using Facebook;
using Facebook.MiniJSON;
using System.Collections.Generic;

public class SocialDebug : MonoBehaviour {

	// Use this for initialization
    void Start()
    {
        SoomlaProfile.Initialize();

        ProfileEvents.OnLoginFinished += (UserProfile UserProfile, string s) =>
        {
            Soomla.SoomlaUtils.LogDebug("My Perfect Game", "login finished with profile: " + UserProfile.toJSONObject().print());
            SoomlaProfile.GetContacts(Provider.FACEBOOK);
        };
        ProfileEvents.OnSocialActionFinished += onSocialActionFinished;
    }

public void onSocialActionFinished(Provider provider, SocialActionType action, string payload) {
    if (action.ToString().Equals("UPDATE_STORY"))
    {
        Debug.Log("updated story successfully");
    } 
}

    public void login()
    {
        SoomlaProfile.Login(
                Provider.FACEBOOK,                        // Social Provider
                null
            );
    }

    public void post()
    {
        SoomlaProfile.UpdateStory(
            Provider.FACEBOOK,     
            "Check out this great story by SOOMLA!",   // Message
            "SOOMLA is 2 years young!",                // Name
            "SOOMLA is GROWing",                       // Caption
            "soomla_2_years",                          // Desc
            "http://blog.soom.la",                     // Link
            "http://blog.soom.la.../soombot.png",      // Image
            null      // Reward
        );
    }
	//
    public void onChallengeClicked()
    {
        if(SoomlaProfile.IsLoggedIn(Provider.FACEBOOK)) {
            FB.AppRequest("You gotta try this! ponchjoe is smashing!", null, null, null, null, "there is so much data", "Mexican Samurai need your help", appRequestCallback);
        } else {
            login();
        }

    }
    private void appRequestCallback(FBResult result)
    {
        if (result != null)
        {
            var responseObject = Json.Deserialize(result.Text) as Dictionary<string, object>;
            object obj = 0;
            if (responseObject.TryGetValue("cancelled", out obj))
            {
                // request canceled
            }
            else if (responseObject.TryGetValue("request", out obj))
            {
                string[] friends;
                if (responseObject.TryGetValue("to", out obj))
                {
                    IEnumerable<object> objectArray = (IEnumerable<object>)responseObject["to"];
                    int count = 0;
                    foreach(var x in objectArray)
                    {
                        count++;
                    }
                    if (count >= 2)
                    {
                        // invited 2 people
                        //do something
                        Debug.Log("invited 2 people");
                    }

                }
            }
        }
    }    




    //
}
