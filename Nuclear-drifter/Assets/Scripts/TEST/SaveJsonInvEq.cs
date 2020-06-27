using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveJsonInvEq : MonoBehaviour
{
    public InventoryBox invTest;
    [TextArea(10,20)]
    public string jsonText = "";
    // Start is called before the first frame update
    void Start()
    {
        invTest = GetComponent<InventoryBox>();
        jsonText = JsonUtility.ToJson(invTest);
    }

    private void OnGUI()
    {
       if(GUI.Button(new Rect(10, 10, 100, 50), "Load"))
       {
            JsonUtility.FromJsonOverwrite(jsonText, invTest);
       }
    }
}
