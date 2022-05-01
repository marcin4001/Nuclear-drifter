using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarrierCanvas : MonoBehaviour
{
    public Canvas canvas;
    public Camera mapCam;
    public Text[] texts;
    private bool active = false;
    private GUIScript gUI;
    private PlayerClickMove player;
    private TypeScene typeSc;
    private PauseMenu pause;
    private MapControl map;
    public GameObject[] buttons;
    public Carrier carrier;
    

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        mapCam.enabled = false;
        gUI = FindObjectOfType<GUIScript>();
        typeSc = FindObjectOfType<TypeScene>();
        player = FindObjectOfType<PlayerClickMove>();
        pause = FindObjectOfType<PauseMenu>();
        map = FindObjectOfType<MapControl>();
    }

    public void Open()
    {
        active = !active;
        mapCam.enabled = active;
        canvas.enabled = active;
        gUI.blockGUI = active;
        player.active = !active;
        typeSc.inMenu = active;
        gUI.DeactiveBtn(!active);
        pause.activeEsc = !active;
        map.keyActive = !active;
        if(active)
        {
            SetTexts();
        }
    }

    private void SetTexts()
    {
        carrier.UpdateLocObj();
        for(int i = 0; i < texts.Length; i++)
        {
            LocObj obj = carrier.locObjs[i];
            if (obj.cost > 0)
            {
                if (obj.unlocked)
                {
                    texts[i].text = obj.name + "($" + obj.cost + ")";
                    buttons[i].GetComponent<Button>().interactable = true;
                }
                else
                {
                    texts[i].text = "##################";
                    buttons[i].GetComponent<Button>().interactable = false;
                }
            }
            else
            {
                texts[i].text = "##################";
                buttons[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    public void WalkTo(int location)
    {
        if (carrier != null)
        {
            carrier.WalkTo(location);
        }
    }
}
