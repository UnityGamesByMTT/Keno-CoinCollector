using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class Animation_Controller : MonoBehaviour
{
   
    [SerializeField] private GameObject MainPopUp;

    [Header("lampAnimation")]
    [SerializeField] internal ImageAnimation lampAnimation;
    [SerializeField] private List<Sprite> lampAnimSprites;
    [SerializeField] private List<Sprite> lampWithGlowSprites;

    [Header("3 lamp with glow")]
    [SerializeField] internal ImageAnimation ThreelampAnimation;
    [SerializeField] private List<Sprite> ThreelampSprites;
    [SerializeField] private List<Sprite> ThreelampWithGlowSprites;

    [Header("Sound Animation")]
    [SerializeField] private GameObject SoundPopup;
    [SerializeField] private GameObject Sound_banner;
    [SerializeField] private Button Yes_Button;
    [SerializeField] private Button No_Button;

    [Header("Initial Animation")]
    [SerializeField] private ImageAnimation[] appearAnim;
    [SerializeField] private ImageAnimation[] characterAnim;
    [SerializeField] private GameObject InitialAnimation;

    [Header("Tiger Spin Animation")]
    [SerializeField] private ImageAnimation TigerSpinAnim;

    [Header("Tiger banner Animation")]
    [SerializeField] private ImageAnimation TigerBanner;
    [SerializeField] private ImageAnimation TigerAppearEffect;
    [SerializeField] private TMP_Text TigerBannerText;


    void Start()
    {

        PlaySoundPopUpOpenAnim();
        //Base.DOScale(Vector3.one,0.5f).SetLoops(-1);
    }


    internal void PlaySoundPopUpcloseAnim()
    {

        StartCoroutine(SoundPopupCloseAnimation());
    }

    void PlaySoundPopUpOpenAnim()
    {

        StartCoroutine(SoundPopupOpenAnimation());

    }


    void PlayInitialAnimation() {

        MainPopUp.SetActive(true);
        InitialAnimation.SetActive(true);
        float delay = 0f;
        for (int i = 0; i < appearAnim.Length; i++)
        {

            StartCoroutine(appear(appearAnim[i], characterAnim[i], delay));
            delay += 0.1f;
        }
        Invoke("PlayTigerSpinAnimation", 5);

    }

    void PlayTigerSpinAnimation() {

        TigerSpinAnim.StartAnimation();
    }

    internal void PLayThreeLampWIthGlowAnim() {

        ThreelampAnimation.StopAnimation();
        ThreelampAnimation.textureArray = ThreelampWithGlowSprites;
        ThreelampAnimation.rendererDelegate.sprite = ThreelampWithGlowSprites[0];
        ThreelampAnimation.StartAnimation();


    }

    internal void ResetThreeLampAnimation()
    {
        ThreelampAnimation.StopAnimation();
        ThreelampAnimation.textureArray = ThreelampSprites;
        ThreelampAnimation.rendererDelegate.sprite = ThreelampSprites[0];
        ThreelampAnimation.StartAnimation();
    }

    internal IEnumerator PlayTigerBannerAnim(int value, Button playbutton) {
        playbutton.gameObject.SetActive(false);
        TigerBanner.gameObject.SetActive(true);
        TigerBannerText.text = value.ToString();

        TigerAppearEffect.StartAnimation();
        yield return new WaitForSeconds(0.7f);
        TigerBanner.GetComponent<Image>().DOFade(1, 0.2f);
        yield return new WaitForSeconds(1f);
        TigerBanner.StartAnimation();
        TigerAppearEffect.StopAnimation();
        yield return new WaitForSeconds(1.65f);
        TigerBannerText.DOFade(1, 0.2f);
        TigerBannerText.gameObject.SetActive(true);
        //yield return new WaitForSeconds(2.2f);
  


    }

    internal void PlaylampAnim() {

        lampAnimation.StopAnimation();
        lampAnimation.textureArray = lampAnimSprites;
        lampAnimation.rendererDelegate.sprite = lampAnimSprites[0];
        lampAnimation.StartAnimation();

    }

    internal void StoplampAnim() {

        lampAnimation.StopAnimation();
    }

    internal void PlayLmapGlowAnim() {

        lampAnimation.StopAnimation();
        lampAnimation.textureArray = lampWithGlowSprites;
        lampAnimation.rendererDelegate.sprite = lampWithGlowSprites[0];
        lampAnimation.StartAnimation();

    }

    internal IEnumerator PlayTigerbannerAnimOff(Button playbutton) {
        TigerBannerText.DOFade(0, 0.2f);
        TigerAppearEffect.StartAnimation();
        yield return new WaitForSeconds(0.7f);
        TigerBanner.GetComponent<Image>().DOFade(0, 0.7f);
        yield return new WaitForSeconds(0.7f);
        playbutton.gameObject.SetActive(true);
        TigerBanner.gameObject.SetActive(false);
    }

    IEnumerator appear(ImageAnimation appearAnim, ImageAnimation characterAnim, float delay=0)
    {
        yield return new WaitForSeconds(delay);
        appearAnim.StartAnimation();
        yield return new WaitForSeconds(0.7f);
        characterAnim.GetComponent<Image>().DOFade(1, 0.1f);
        yield return new WaitForSeconds(1f);
        characterAnim.StartAnimation();
        appearAnim.StopAnimation();
        yield return new WaitForSeconds(1.2f);
        appearAnim.StartAnimation();
        yield return new WaitForSeconds(0.7f);
        characterAnim.GetComponent<Image>().DOFade(0, 0.5f);
        if (delay == 0.3f)
        {
        yield return new WaitForSeconds(1f);

            InitialAnimation.SetActive(false);
            MainPopUp.SetActive(false);

        }


    }



    IEnumerator SoundPopupOpenAnimation()
    {

        MainPopUp.SetActive(true);
        SoundPopup.SetActive(true);
        Sound_banner.transform.localScale = Vector3.zero;
        Yes_Button.transform.localScale = Vector3.zero;
        No_Button.transform.localScale = Vector3.zero;

        Yes_Button.interactable = false;
        No_Button.interactable = false;



        Sound_banner.transform.DOScale(1, 0.3f);
        yield return new WaitForSeconds(0.3f);
        Yes_Button.transform.DOScale(1, 0.2f);
        yield return new WaitForSeconds(0.2f);
        No_Button.transform.DOScale(1, 0.2f);
        Yes_Button.interactable = true;
        No_Button.interactable = true;


    }

    IEnumerator SoundPopupCloseAnimation()
    {
        Yes_Button.interactable = false;
        No_Button.interactable = false;

        No_Button.transform.DOScale(0, 0.2f);
        yield return new WaitForSeconds(0.2f);
        Yes_Button.transform.DOScale(0, 0.2f);
        yield return new WaitForSeconds(0.3f);
        Sound_banner.transform.DOScale(0, 0.3f);

        //MainPopUp.SetActive(false);
        SoundPopup.SetActive(false);
        Sound_banner.transform.localScale = Vector3.one;
        Yes_Button.transform.localScale = Vector3.one;
        No_Button.transform.localScale = Vector3.one;
        Yes_Button.interactable = true;
        No_Button.interactable = true;
        PlayInitialAnimation();

    }






}
