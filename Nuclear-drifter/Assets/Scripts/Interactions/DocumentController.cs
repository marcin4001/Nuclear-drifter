using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentController : MonoBehaviour
{
    public Image docImg;
    public Sprite[] docSprites;
    private GUIScript gUI;
    private MapControl map;
    private Canvas docCanvas;
    private TypeScene typeSc;
    private AchievementDoc achievementDoc;
    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        map = FindObjectOfType<MapControl>();
        docCanvas = GetComponent<Canvas>();
        typeSc = FindObjectOfType<TypeScene>();
        docCanvas.enabled = false;
        achievementDoc = GetComponent<AchievementDoc>();
    }

    public void OpenDoc(int id, int idItem)
    {
        if (docSprites.Length <= 0) return;
        if (id < 0 || id >= docSprites.Length) return;
        Sprite document = docSprites[id];
        docImg.overrideSprite = document;
        docCanvas.enabled = true;
        map.keyActive = false;
        gUI.DeactiveBtn(false);
        typeSc.inMenu = true;
        Time.timeScale = 0.0f;
        achievementDoc.Check(idItem);
    }

    public void Close()
    {
        docCanvas.enabled = false;
        map.keyActive = true;
        gUI.DeactiveBtn(true);
        typeSc.inMenu = false;
        Time.timeScale = 1.0f;
    }
}
