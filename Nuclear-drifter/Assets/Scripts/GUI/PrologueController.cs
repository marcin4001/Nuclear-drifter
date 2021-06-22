using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PrologueController : MonoBehaviour
{
    public int currentIndex = 0;
    public PrologueObj[] prologueObjs;
    public Text text;
    public Image backgroundImg;
    public Animator anim;
    public bool activeEnter = true;
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        backgroundImg.overrideSprite = prologueObjs[currentIndex].backgroundImg;
        text.text = prologueObjs[currentIndex].text;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && activeEnter)
        {
            StartCoroutine(ChangeBgImage());
        }
    }

    private IEnumerator ChangeBgImage()
    {
        activeEnter = false;
        anim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1.5f);
        currentIndex += 1;
        if (currentIndex < prologueObjs.Length)
        {
            backgroundImg.overrideSprite = prologueObjs[currentIndex].backgroundImg;
            text.text = prologueObjs[currentIndex].text;
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
        anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        //if (currentIndex < prologueObjs.Length)
        //    text.text = prologueObjs[currentIndex].text;
        activeEnter = true;
    }
}
