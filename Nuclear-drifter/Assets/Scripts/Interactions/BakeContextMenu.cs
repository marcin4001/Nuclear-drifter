using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BakeContextMenu : MonoBehaviour
{
    public GameObject contextMenu;
    public bool active = false;
    public GameObject bakingDevice;
    private RectTransform contextMenuTransform;
    // Start is called before the first frame update
    void Start()
    {
        contextMenu.SetActive(false);
        contextMenuTransform = contextMenu.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!RectTransformUtility.RectangleContainsScreenPoint(contextMenuTransform, Input.mousePosition) && Input.anyKeyDown && active)
        {
            Debug.Log("Close");
            Close();
        }

        //if(Input.GetKeyDown(KeyCode.C))
        //{
        //    ShowMenu();
        //}
    }

    public void ShowMenu(GameObject baking_device)
    {
        bakingDevice = baking_device;
        Invoke("SetActive", 0.1f);
        contextMenu.SetActive(true);
        contextMenuTransform.position = new Vector3(Input.mousePosition.x + 10f, Input.mousePosition.y - 10f, 0f);
        Debug.Log("ShowMenu");
    }

    public void SetActive()
    {
        active = true;
    }

    public void Close()
    {
        contextMenu.SetActive(false);
        active = false;
        bakingDevice = null;
    }

    public void BakeRawMeat()
    {
        if(bakingDevice != null)
        {
            bakingDevice.SendMessage("BakeRawMeat", SendMessageOptions.DontRequireReceiver);
            bakingDevice = null;
        }
        Close();
    }

    public void BakeFish()
    {
        if (bakingDevice != null)
        {
            bakingDevice.SendMessage("BakeFish", SendMessageOptions.DontRequireReceiver);
            bakingDevice = null;
        }
        Close();
    }
}
