using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private Canvas loading;
    // Start is called before the first frame update
    void Start()
    {
        loading = GetComponent<Canvas>();
        loading.enabled = false;
    }

    public void ShowLoading()
    {
        loading.enabled = true;
    }
}
