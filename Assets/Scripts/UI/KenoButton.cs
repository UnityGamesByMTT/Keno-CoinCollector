using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class KenoButton : MonoBehaviour
{
    [SerializeField]
    private Button This_Button;
    [SerializeField]
    private TMP_Text This_Text;
    [SerializeField]
    private Image This_Image;
    [SerializeField]
    private Sprite slelected_sprite;
    [SerializeField]
    private Sprite original_sprite;
    [SerializeField]
    private Sprite result_sprite;
    [SerializeField]
    private Sprite result_select_sprite;

    [SerializeField] private KenoBehaviour KenoManager;
    [SerializeField] private Image selected_image;

    internal bool isActive = false;
    internal int value = 0;
    internal bool isFortune = false;
    int FortuneIndex = -1;

    private void Start()
    {
        This_Button = this.gameObject.GetComponent<Button>();
        This_Image = this.gameObject.GetComponent<Image>();
        selected_image = this.transform.GetChild(0).GetComponent<Image>();
        if (this.transform.GetChild(0).GetComponent<TMP_Text>() == null)
        {
            This_Text = this.transform.GetChild(1).GetComponent<TMP_Text>();
        }
        else
        {
            This_Text = this.transform.GetChild(0).GetComponent<TMP_Text>();
        }
        if (This_Button) This_Button.onClick.RemoveAllListeners();
        if (This_Button) This_Button.onClick.AddListener(OnKenoSelect);
        value = int.Parse(This_Text.text);
    }

    internal void OnKenoSelect()
    {
        isActive = !isActive;
        if (isActive)
        {
            if (KenoManager.selectionCounter < 10)
            {
                selected_image.gameObject.SetActive(true);
                //selected_image.sprite = slelected_sprite;
                if (This_Image) This_Image.sprite = slelected_sprite;
                if (This_Text) This_Text.color = Color.black;

                KenoManager.AddKeno(value);
            }
            else
            {
                KenoManager.ShowMaxPopup();
                isActive = false;
            }
        }
        else
        {
            if (This_Image) This_Image.sprite = original_sprite;
            if (This_Text) This_Text.color = KenoManager.textColor;
            This_Button.interactable = false;
            selected_image.transform.DOScale(0, 0.2f).onComplete = () =>
            {
                selected_image.gameObject.SetActive(false);
                selected_image.transform.localScale = new Vector3(1, 1, 1);
                This_Button.interactable = true;


            };
            //if (This_Image) This_Image.color = _normalColor;
            KenoManager.selectionCounter--;
            KenoManager.RemoveKeno(value);
        }
    }


    internal void SelectFortune()
    {

        //isFortune = true;
        int fortuenIndex = Random.Range(0, 5);
        FortuneIndex = fortuenIndex;
        if (selected_image) selected_image.sprite = KenoManager.fortuenSprite[fortuenIndex];
        selected_image.transform.localScale = new Vector3(0,0,0);
        selected_image.gameObject.SetActive(true);
        selected_image.transform.DOScale(Vector3.one, 0.3f).onComplete=()=>isFortune=true;



    }

    internal void ResultColor(bool fortune = false)
    {


        if (selected_image) selected_image.sprite = result_sprite;
        selected_image.gameObject.SetActive(true);


        if (This_Text) This_Text.color = Color.black;
        if (isFortune)
        {
            if (selected_image) selected_image.sprite = KenoManager.fortuenResultSprite[FortuneIndex];
            KenoManager.highlightFortune(FortuneIndex);
            FortuneIndex = -1;

        }
        else if (isActive)
        {

            if (selected_image) selected_image.sprite = result_select_sprite;
            //if (This_Image) This_Image.sprite = result_select_sprite;
            //enableWinTick();
            KenoManager.ResultCounter++;
            //KenoManager.ActivateWinning();
            //KenoManager.CheckTransform(this.transform);
        }
    }

    internal void ResetButton()
    {
        isActive = false;

        if (This_Image) This_Image.sprite = original_sprite;
        if (This_Text) This_Text.color = KenoManager.textColor;
        //This_Button.interactable = false;
        selected_image.gameObject.SetActive(false);
        selected_image.sprite = slelected_sprite;
        selected_image.transform.localScale = new Vector3(1, 1, 1);
        //This_Button.interactable = true;

        //selected_image.transform.DOScale(0, 0.2f).onComplete = () =>
        //{

        //};
        //if (This_Image) This_Image.color = _normalColor;
        //if (_winTick) _winTick.SetActive(false);
    }

}
