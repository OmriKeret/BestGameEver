//using UnityEngine;
//using System.Collections;
//using Soomla.Profile;

//public class FacebookConnection : MonoBehaviour {
//    public static FacebookConnection Instance;
//    private bool isInit;
//    void Awake()
//    {
//        if (Instance == null)
//        { 	// making sure we only initialize one instance.
//            Instance = this;
//            GameObject.DontDestroyOnLoad(this.gameObject);
//        }
//        else
//        {				// Destroying unused instances.
//            GameObject.Destroy(this.gameObject);
//        }
//    }
//    // Use this for initialization
//    void Start () {
//        // examples of catching fired events
//        ProfileEvents.OnSoomlaProfileInitialized += () =>
//        {
//            isInit = true;
//        };
//        ProfileEvents.OnSocialActionFinished += onSocialActionFinished;




//        SoomlaProfile.Initialize();
//    }

//    public void onSocialActionFinished(Provider provider, SocialActionType action, string payload)
//    {
//        if (action.ToString().Equals(SocialActionType.UPDATE_STORY))
//        {
//            //TODO: add life
//            Debug.Log("================================\n\n\n\n\n we are on face book!\n\n\n\n\n");
//        }
//        else
//        {
//            Debug.Log("================================\n\n\n\n\n nothing now from facebook\n\n\n\n\n");
//        }
//    }

//    public void Login()
//    {
//        SoomlaProfile.Login(Provider.FACEBOOK);
//    }

//    public void Share()
//    {
//        if (!SoomlaProfile.IsLoggedIn(Provider.FACEBOOK))
//        {
//            SoomlaProfile.Login(Provider.FACEBOOK);
//        }

//        SoomlaProfile.UpdateStoryWithConfirmation(
//            Provider.FACEBOOK,                          // Provider
//            "This is the story.",                       // Text of the story to post
//            "PohnchoJoe the Mexican Samurai ",  // Name
//            "Ponchjoe is an high action bla bla. chip chop!",                            // Caption
//            "Ponchjoe as the defender ",                     // Description
//            "http://about.soom.la/soombots",            // Link to post
//            "http://about.soom.la/.../spockbot.png",    // Image URL
//            "",                                         // Payload
//            null,                                       // Reward
//            "Share and give me my MOJO back!"                                // Message to show in the confirmation dialog
//        );
//    }

//    public void rateApp()
//    {
//        SoomlaProfile.OpenAppRatingPage();
//    }
//}
