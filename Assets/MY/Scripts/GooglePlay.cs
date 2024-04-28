using UnityEngine;

public class GooglePlay : MonoBehaviour
{
    public string log;
    public void Login()
    {
        GPGSBinder.Inst.Login();
        InventoryManager.Instance.LoadInventoryFromCloud();
       // GPGSBinder.Inst.LoadCloud("mySave", (sucess, data) => log = $"{sucess}, {data}");

    }

    public void LogOut()
    {
        GPGSBinder.Inst.Logout();
        InventoryManager.Instance.SaveInventoryToCloud();
     //   GPGSBinder.Inst.SaveCloud("mySave", "my Data", sucess => log = $"{sucess}");
    }

    public void DeleteData()
    {
        GPGSBinder.Inst.DeleteCloud("mySave", sucess => log = $"{sucess}");
    }
}
