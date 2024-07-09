using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button Quickpick_button;
    [SerializeField] private Button Play_Button;
    [SerializeField] private Button AutoPlay_Button;
    [SerializeField] private Button Reset_Button;
    [SerializeField] private Button Delete_Button;
    [SerializeField] private Button GameExit_Button;
    [SerializeField] private Button MaxPopup_Button;
    [SerializeField] private Button selectionPlus_Button;
    [SerializeField] private Button selectionMinus_Button;
    [SerializeField] private Button Stake_button;
    [SerializeField] private Button StakePlus_Button;
    [SerializeField] private Button StakeMinus_Button;
    [SerializeField] private Button ConfirmStake_Button;
    [SerializeField] private Button StakeExit_Button;
    [SerializeField] private Button Settings_Button;
    [SerializeField] private Button Settings_exit_Button;
    [SerializeField] private Button Sound_on_Button;
    [SerializeField] private Button Sound_off_Button;
    [SerializeField] private Button Info_Button;
    [SerializeField] private Button Info_exitButton;
    [SerializeField] private Button Speed_button;

    [Header("Texts")]
    [SerializeField] private TMP_Text Stake_Text;
    [SerializeField] private TMP_Text Win_Text;
    [SerializeField] private TMP_Text TotalBet_text;
    [SerializeField] private TMP_Text Num_selected;
    [SerializeField] private TMP_Text Selection;
    [SerializeField] private TMP_Text StakeMenuText;
    [SerializeField] private TMP_Text Popuptext;
    [SerializeField] private TMP_Text Balance_text;
    //[SerializeField]
    //private TMP_Text Win_Text;

    [Header("Lists")]
    [SerializeField]
    private List<TMP_Text> Payout_Text;
    [SerializeField]
    private List<TMP_Text> Hits_Text;
    [SerializeField]
    private List<GameObject> Win_Objects;
    public List<double> stakeOptions;
    [SerializeField] List<Image> PayoutList;

    [Header("GameObjects")]
    [SerializeField]
    private GameObject Reset_Object;
    [SerializeField]
    private GameObject PlayAnim_Object;
    [SerializeField]
    private GameObject CoinValueDisable_object;
    [SerializeField]
    private GameObject StarAnim_Object;
    [SerializeField] private GameObject StakeMenu;
    [SerializeField] private GameObject StakeList;
    [SerializeField] private GameObject StakeOption_prefab;
    [SerializeField] private GameObject Main_PopUp;
    [SerializeField] private GameObject SpeedOn;
    [SerializeField] private GameObject SpeedOff;

    [Header("Scripts")]
    [SerializeField]
    private KenoBehaviour KenoManager;
    [SerializeField] private Animation_Controller animation_Controller;

    [Header("Popups")]
    [SerializeField]
    private GameObject MainPopup_Object;
    [SerializeField]
    private GameObject MaxPopup_Object;
    [SerializeField]
    private GameObject WinPopup_Object;
    [SerializeField]
    private Transform WinPopup_Transform;
    [SerializeField]
    private GameObject CoinAnim_Object;
    [SerializeField] private GameObject Sound_popup;
    [SerializeField] private GameObject Settings_PopUp;
    [SerializeField] private GameObject Info_PopUp;

    [Header("sprites")]
    [SerializeField] private Sprite Payout_win_sprite;
    [SerializeField] private Sprite Payout_original_sprite;
    [SerializeField] private Sprite SpeedOnSprite;
    [SerializeField] private Sprite SpeedOffSprite;

    [Header("Image Animation Script")]
    [SerializeField]
    private ImageAnimation TitleAnim;

    private double stake = 5;
    private double balance = 999999;

    private int winning = 0;
    internal bool isReset = false;

    private int index;

    private bool isSpeedOn = false;

    [Header("Strings")]
    [SerializeField] private string maxCountString = "A maximum of 10 picks is allowed per play";


    [SerializeField] private List<int> winArray = new List<int>();

    void Start()
    {
        if (Quickpick_button) Quickpick_button.onClick.RemoveAllListeners();
        if (Quickpick_button) Quickpick_button.onClick.AddListener(PickRandomIndices);

        if (Sound_on_Button)
        {

            Sound_on_Button.onClick.RemoveAllListeners();
            Sound_on_Button.onClick.AddListener(delegate { SoundToggle(true); });
        }

        if (Sound_on_Button)
        {

            Sound_off_Button.onClick.RemoveAllListeners();
            Sound_off_Button.onClick.AddListener(delegate { SoundToggle(false); });
        }

        if (selectionMinus_Button)
        {
            selectionMinus_Button.onClick.RemoveAllListeners();
            selectionMinus_Button.onClick.AddListener(delegate { ToggleSelection(false); });
        }

        if (Settings_Button)
        {

            Settings_Button.onClick.RemoveAllListeners();
            Settings_Button.onClick.AddListener(delegate { OpenPopUp(Settings_PopUp); });
        }

        if (Settings_exit_Button)
        {

            Settings_exit_Button.onClick.RemoveAllListeners();
            Settings_exit_Button.onClick.AddListener(delegate { ClosePopUp(Settings_PopUp); });
        }


        if (selectionPlus_Button)
        {
            selectionPlus_Button.onClick.RemoveAllListeners();
            selectionPlus_Button.onClick.AddListener(delegate { ToggleSelection(true); });
        }

        if (Stake_button)
        {
            Stake_button.onClick.RemoveAllListeners();
            Stake_button.onClick.AddListener(delegate { ToggleChangeStake(true); });
        }

        if (ConfirmStake_Button)
        {

            ConfirmStake_Button.onClick.RemoveAllListeners();
            ConfirmStake_Button.onClick.AddListener(ConfirmStake);
        }


        if (StakeExit_Button)
        {

            StakeExit_Button.onClick.RemoveAllListeners();
            StakeExit_Button.onClick.AddListener(delegate { ToggleChangeStake(false); });
        }

        if (Info_Button)
        {

            Info_Button.onClick.RemoveAllListeners();
            Info_Button.onClick.AddListener(delegate { OpenPopUp(Info_PopUp); });
        }

        if (Info_Button)
        {
            Info_exitButton.onClick.RemoveAllListeners();
            Info_exitButton.onClick.AddListener(delegate { ClosePopUp(Info_PopUp); });
        }

        if (Speed_button) {


            Speed_button.onClick.RemoveAllListeners();
            Speed_button.onClick.AddListener(ToggleSpeed);
        }

        if (Play_Button) Play_Button.onClick.RemoveAllListeners();
        if (Play_Button) Play_Button.onClick.AddListener(DummyPlay);

        CheckPlayButton(false);

        if (StakePlus_Button) StakePlus_Button.onClick.RemoveAllListeners();
        if (StakePlus_Button) StakePlus_Button.onClick.AddListener(delegate { ChangeStake(true); });

        if (StakeMinus_Button) StakeMinus_Button.onClick.RemoveAllListeners();
        if (StakeMinus_Button) StakeMinus_Button.onClick.AddListener(delegate { ChangeStake(false); });

        if (Reset_Button) Reset_Button.onClick.RemoveAllListeners();
        if (Reset_Button) Reset_Button.onClick.AddListener(ResetGame);

        if (Delete_Button) Delete_Button.onClick.RemoveAllListeners();
        if (Delete_Button) Delete_Button.onClick.AddListener(CleanButtons);

        if (GameExit_Button) GameExit_Button.onClick.RemoveAllListeners();
        if (GameExit_Button) GameExit_Button.onClick.AddListener(CallOnExitFunction);

        if (MaxPopup_Button) MaxPopup_Button.onClick.RemoveAllListeners();
        if (MaxPopup_Button) MaxPopup_Button.onClick.AddListener(MaxPopupDisable);

        //stake = 5;


        if (Selection) Selection.text = KenoManager.selectionCounter.ToString();

        if (Stake_Text) Stake_Text.text = stake.ToString();
        if (TotalBet_text) TotalBet_text.text = stake.ToString();
        if (Selection) Selection.text = KenoManager.quickpickCounter.ToString();

        PopulateStake();
        //if (Win_Text) Win_Text.text = winning.ToString();
        Application.ExternalCall("window.parent.postMessage", "OnEnter", "*");

        winning = 0;

        if (Balance_text) Balance_text.text = balance.ToString();
    }

    private void CallOnExitFunction()
    {
        Application.ExternalCall("window.parent.postMessage", "onExit", "*");
    }

    private void DummyPlay()
    {
        if (StarAnim_Object) StarAnim_Object.SetActive(true);
        if (isReset)
        {
            ResetGame();
        }
        isReset = true;
        CheckPlayButton(false);
        if (Delete_Button) Delete_Button.interactable = false;
        if (CoinValueDisable_object) CoinValueDisable_object.SetActive(true);
        KenoManager.PlayDummyGame();
        DOVirtual.DelayedCall(0.5f, () =>
        {
            if (StarAnim_Object) StarAnim_Object.SetActive(false);
        });
    }

    void ToggleSpeed() {

        isSpeedOn = !isSpeedOn;
        if (isSpeedOn)
        {

            Speed_button.image.sprite = SpeedOnSprite;
            SpeedOn.SetActive(true);
            SpeedOff.SetActive(false);

        }
        else {

            Speed_button.image.sprite = SpeedOffSprite;
            SpeedOn.SetActive(false);
            SpeedOff.SetActive(true);
        }

    }


    void ToggleSelection(bool inc)
    {

        if (inc)
            KenoManager.quickpickCounter++;
        else
            KenoManager.quickpickCounter--;

        if (KenoManager.quickpickCounter <= 2)
            KenoManager.quickpickCounter = 2;

        if (KenoManager.quickpickCounter >= 10)
            KenoManager.quickpickCounter = 10;

        if (Selection) Selection.text = KenoManager.quickpickCounter.ToString();

    }


    void SoundToggle(bool toggle)
    {

        animation_Controller.PlaySoundPopUpcloseAnim();
        //Main_PopUp.SetActive(false);
        //Sound_popup.SetActive(false);

    }

    void ToggleChangeStake(bool toggle)
    {

        StakeMenu.SetActive(toggle);

    }

    void OpenPopUp(GameObject menu)
    {

        Main_PopUp.SetActive(true);
        menu.SetActive(true);


    }

    internal void UpdateBalance(int win = -1)
    {

        if (win > 0)
            balance += win;
        else
            balance -= stake;

        if (Balance_text) Balance_text.text = balance.ToString();
    }

    void ClosePopUp(GameObject menu)
    {

        Main_PopUp.SetActive(false);
        menu.SetActive(false);
    }

    private void ChangeStake(bool inc)
    {
        int dir = 1;


        if (inc)
        {
            index++;
            dir = -1;
        }
        else
        {

            index--;
        }

        if (index > stakeOptions.Count - 1)
        {
            index = stakeOptions.Count - 1;
            return;

        }
        else if (index < 0)
        {

            index = 0;
            return;

        }

        if (index < stakeOptions.Count || index >= 0)
            StakeList.transform.DOLocalMoveY(StakeList.transform.localPosition.y + (63 * dir), 0.2f);

        StakeMenuText.text = stakeOptions[index].ToString();
    }

    void ConfirmStake()
    {

        stake = stakeOptions[index];
        Stake_Text.text = stake.ToString();
        ToggleChangeStake(false);
    }

    void PopulateStake()
    {

        for (int i = 0; i < stakeOptions.Count; i++)
        {
            GameObject temp = Instantiate(StakeOption_prefab, StakeList.transform);
            temp.transform.GetChild(0).GetComponent<TMP_Text>().text = stakeOptions[i].ToString();
            temp.transform.SetAsFirstSibling();
        }

        StakeList.transform.localPosition = new Vector2(0, (stakeOptions.Count - 1) * 63);
        stake = stakeOptions[0];
        Stake_Text.text = stake.ToString();
        StakeMenuText.text = stake.ToString();
    }

    private void PickRandomIndices()
    {
        if (isReset)
        {
            ResetGame();
        }
        print("triggered");
        KenoManager.PickRandoms();
    }


    internal void CheckPlayButton(bool isActive)
    {
        if (Play_Button) Play_Button.interactable = isActive;
        //if (Random_Button) Random_Button.interactable = isActive;
        if (AutoPlay_Button) AutoPlay_Button.interactable = isActive;
        if (PlayAnim_Object) PlayAnim_Object.SetActive(isActive);
    }

    private void WinPopupEnable(int amount)
    {
        CancelInvoke("WinPopupDisable");
        Popuptext.text = "you have won " + amount.ToString();
        //if (TitleAnim) TitleAnim.StartAnimation();
        //if (WinPopup_Transform) WinPopup_Transform.localScale = Vector3.zero;
        //if (MainPopup_Object) MainPopup_Object.SetActive(true);
        //if (WinPopup_Object) WinPopup_Object.SetActive(true);
        //if (WinPopup_Transform) WinPopup_Transform.DOScale(Vector3.one, 0.5f);
        //if (CoinAnim_Object) CoinAnim_Object.SetActive(true);
        Invoke("WinPopupDisable", 5f);
    }


    private void WinPopupDisable()
    {
        Popuptext.text = "";
        //if (MainPopup_Object) MainPopup_Object.SetActive(false);
        //if (WinPopup_Object) WinPopup_Object.SetActive(false);
        //if (CoinAnim_Object) CoinAnim_Object.SetActive(false);
    }

    internal void MaxPopupEnable()
    {
        CancelInvoke("MaxPopupDisable");
        Popuptext.text = maxCountString;

        //if (MainPopup_Object) MainPopup_Object.SetActive(true);
        //if (MaxPopup_Object) MaxPopup_Object.SetActive(true);
        Invoke("MaxPopupDisable", 2f);
    }

    private void MaxPopupDisable()
    {
        Popuptext.text = "";
        //if (MainPopup_Object) MainPopup_Object.SetActive(false);
        //if (MaxPopup_Object) MaxPopup_Object.SetActive(false);
    }

    //internal void UpdateSelectedText(int count)
    //{
    //    BetAmountUpdate(count);
    //}

    internal void CheckFinalWinning(List<int> winarray, int amount)
    {
        print("win array " + winarray.Count);

        if (winarray.Count >= 2)
        {
            for (int i = 0; i < (winarray.Count - 1); i++)
            {
                PayoutList[i].sprite = Payout_win_sprite;

            }
            WinPopupEnable(amount);
        }
    }

    internal void ResetPaytable()
    {
        for (int i = 0; i < PayoutList.Count; i++)
        {
            PayoutList[i].sprite = Payout_original_sprite;

        }
    }
    internal void CheckWinnings(int count)
    {
        if (count >= 2)
        {
            //if (Win_Objects[count - 2]) Win_Objects[count - 2].SetActive(true);

            winning += int.Parse(Payout_Text[count - 2].text);
        }
        else
        {
            //for (int i = 0; i < Win_Objects.Count; i++)
            //{
            //    if (Win_Objects[i]) Win_Objects[i].SetActive(false);
            //}
            winning = 0;
        }
        if (Win_Text) Win_Text.text = winning.ToString();
    }

    internal void BetAmountUpdate(int count)
    {
        if (Num_selected)
        {
            Num_selected.text = count.ToString();
            if (count == 0)
                Num_selected.text = "";
        }


        for (int i = 0; i < Payout_Text.Count; i++)
        {
            if (Payout_Text[i]) Payout_Text[i].text = "";
        }

        //for (int i = 0; i < Hits_Text.Count; i++)
        //{
        //    if (Hits_Text[i]) Hits_Text[i].text = "";
        //}

        if (count >= 2)
        {

            for (int i = 0; i < count - 1; i++)
            {
                //if (Hits_Text[i - 2]) Hits_Text[i - 2].text = i.ToString();
                if (Payout_Text[i]) Payout_Text[i].text = (stake * i).ToString();
            }

            if (count >= 8)
            {
                print("triggered");
                if (Payout_Text[0]) Payout_Text[0].text = "";
                if (Payout_Text[1]) Payout_Text[1].text = "";
            }
            else if (count >= 5)
            {
                if (Payout_Text[0]) Payout_Text[0].text = "";
            }

        }

    }

    internal void EnableReset()
    {
        if (Reset_Object) Reset_Object.SetActive(true);
        if (Delete_Button) Delete_Button.interactable = true;
        if (CoinValueDisable_object) CoinValueDisable_object.SetActive(false);
        CheckPlayButton(true);
    }

    private void ResetGame()
    {
        //KenoManager.ResetWinAnim();
        //if (TitleAnim) TitleAnim.StopAnimation();
        KenoManager.ResetButtons();
        if (Reset_Object) Reset_Object.SetActive(false);
        isReset = false;
        CheckWinnings(0);
    }

    private void CleanButtons()
    {
        BetAmountUpdate(0);
        //UpdateSelectedText(0);
        CheckWinnings(0);
        KenoManager.CleanPage();
        CheckPlayButton(false);
    }

}
