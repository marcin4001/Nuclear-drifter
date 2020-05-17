using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    private Button btn;
    private MapControl mc;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        mc = FindObjectOfType<MapControl>();
        if (btn != null && mc != null) btn.onClick.AddListener(() => mc.OpenMap());
    }


}
