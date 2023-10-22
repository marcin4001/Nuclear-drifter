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
    public bool showLoading = true;
    private LoadingScreen loading;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        loading = FindObjectOfType<LoadingScreen>();
        source = GetComponent<AudioSource>();
        backgroundImg.overrideSprite = prologueObjs[currentIndex].backgroundImg;
        text.text = prologueObjs[currentIndex].text;
        StartCoroutine(StartActiveEnter());
    }

    private IEnumerator StartActiveEnter()
    {
        activeEnter = false;
        yield return new WaitForSeconds(1.5f);
        activeEnter = true;
        if(source != null)
        {
            source.clip = prologueObjs[currentIndex].clip;
            source.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) && activeEnter)
        {
            StartCoroutine(ChangeBgImage());
        }
    }

    private IEnumerator ChangeBgImage()
    {
        if(source != null)
            source.Stop();
        activeEnter = false;
        anim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1.5f);
        currentIndex += 1;
        if(currentIndex >= prologueObjs.Length)
        {
            if(showLoading)
                loading.ShowLoading();
            yield return new WaitForSeconds(0.1f);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            backgroundImg.overrideSprite = prologueObjs[currentIndex].backgroundImg;
            text.text = prologueObjs[currentIndex].text;
        }
        anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        if(source != null)
        {
            source.clip = prologueObjs[currentIndex].clip;
            source.Play();
        }
        activeEnter = true;
    }
}
