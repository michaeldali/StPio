using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollCounterScript : MonoBehaviour
{
    TMP_Text scrollText;
    public static int scrollAmount;
    // Start is called before the first frame update
    void Start()
    {
        scrollText = this.GetComponent<TMP_Text>();
        scrollAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scrollText.text = scrollAmount.ToString();
    }
}
