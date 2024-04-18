using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooglePlay : MonoBehaviour
{
    GPGSBinder binder;

    public void Login()
    {
        binder.Login();
    }

    public void LogOut()
    {
        binder.Logout();
    }
}
