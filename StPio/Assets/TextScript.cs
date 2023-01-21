/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.image.enabled == true){
            Time.timeScale = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            this.enabled = false;
        }
                // Exit scroll text UI
        if (scrollTexts[ScrollCounterScript.scrollAmount].enabled = true && Input.GetKeyDown(KeyCode.Tab))
        {
            scrollTexts[ScrollCounterScript.scrollAmount-1].enabled = false;
        }
    }
}
*/