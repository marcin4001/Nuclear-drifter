using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkingHeadController : MonoBehaviour
{
    public HeadObj headObj;
    public float mouthChangeThreshold = 0.5f;
    public AudioSource source;
    public Image mouthImg;
    public GameObject backgroundImg;
    public bool isBlinking = false;
    public float blinkTime = 0.1f;
    public float blinkInterval = 5f;
    public float blinkingTimer = 0f;
    public bool isComplete = false;
    private float[] spectrumData;
    private int pitch = 0;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(headObj == null) return;
        if (!source.isPlaying)
        {
            if(isComplete)
            {
                StartBlink();
                isComplete = false;
            }
            blinkingTimer += Time.deltaTime;
            if (!isBlinking && blinkingTimer >= blinkInterval)
            {
                StartBlink();
            }

            if(isBlinking && blinkingTimer >= blinkTime)
            {
                StopBlink();
            }
            return;
        }
        spectrumData = new float[256];
        source.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        float averageSpectrum = 0f;
        foreach(float spectrum in spectrumData)
            averageSpectrum += spectrum;
        averageSpectrum = averageSpectrum/spectrumData.Length;
        int indexMouth = Mathf.FloorToInt(averageSpectrum * (headObj.mouth.Length - 1 - pitch) * 1000);
        Debug.Log(indexMouth);
        if (indexMouth < headObj.mouth.Length)
        {
            mouthImg.overrideSprite = headObj.mouth[indexMouth];
        }
        else
        {
            mouthImg.overrideSprite = headObj.mouth[headObj.mouth.Length - 1];
        }
    }

    public void SetHead(NPCBasic _NPC)
    {
        headObj = _NPC.GetComponent<HeadObj>();
        if (headObj == null)
        {
            backgroundImg.SetActive(false);
            return;
        }
        backgroundImg.SetActive(true);
        mouthImg.overrideSprite = headObj.mouth[0];
    }

    public void Close()
    {
        headObj = null;
        source.Stop();
        source.clip = null;
        blinkingTimer = 0f;
    }

    public void Play(AudioClip clip, int _pitch = 0)
    {
        if (headObj == null)
            return;
        source.clip = clip;
        pitch = _pitch;
        source.Play();
        isComplete = true;
    }

    private void StartBlink()
    {
        if (headObj.closedEyes == null)
            return;
        blinkingTimer = 0f;
        isBlinking = true;
        mouthImg.overrideSprite = headObj.closedEyes;
    }

    private void StopBlink()
    {
        isBlinking = false;
        mouthImg.overrideSprite = headObj.mouth[0];
        blinkingTimer = 0f;
    }
}
