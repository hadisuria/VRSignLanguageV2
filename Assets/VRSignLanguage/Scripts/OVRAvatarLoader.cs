using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRAvatarLoader : MonoBehaviour
{
    //public OvrAvatar myAvatar;

    //void Awake()
    //{
    //    Oculus.Platform.Users.GetLoggedInUser().OnComplete(GetLoggedInUserCallback);
    //    Oculus.Platform.Request.RunCallbacks(); //avoids race condition with OvrAvatar.cs Start()
    //}

    //private void GetLoggedInUserCallback(Message<User> message)
    //{
    //    if (!message.IsError)
    //    {
    //        myAvatar.oculusUserID = message.Data.ID.ToString(); //string
    //        myAvatar.oculusUserIDInternal = message.Data.ID; //ulong
    //        Users.Get(message.Data.ID).OnComplete(GetUserInfo);
    //        Debug.Log("Oculus ID is: " + message.Data.ID);
    //    }
    //}

    //void GetUserInfo(Message<User> user)
    //{
    //    Debug.Log("Oculus username is: " + user.Data.OculusID);
    //    Destroy(this.gameObject);
    //}
}
