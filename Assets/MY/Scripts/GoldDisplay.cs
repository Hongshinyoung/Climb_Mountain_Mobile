using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldDisplay : MonoBehaviour
{
    public Text goldText;

    private void Update()
    {
        goldText.text = Gold.Instance.GetGold().ToString();
    }
}
