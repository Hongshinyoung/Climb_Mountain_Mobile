using UnityEngine;
using UnityEngine.UI;

public class GooglePlay : MonoBehaviour
{
    public Button saveButton;
    public Button loadButton;
    public void Login()
    {
        GPGSBinder.Inst.Login();
    }

    public void LogOut()
    {
        GPGSBinder.Inst.Logout();
    }
}
