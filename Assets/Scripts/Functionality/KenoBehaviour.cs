using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class KenoBehaviour : MonoBehaviour
{
    [Header("Lists")]
    [SerializeField]
    private List<KenoButton> KenoButtonScripts;
    [SerializeField]
    private List<int> SelectedList;
    [SerializeField]
    private List<Transform> Balls_Transform;
    [SerializeField]
    private List<TMP_Text> Balls_Text;
    [SerializeField] private List<int> ResultList;
    [SerializeField] private List<int> templist = new List<int>();
    [SerializeField] private List<int> winArray = new List<int>();
    [SerializeField] private List<int> ForturnWinArray = new List<int>();

    [Header("Integers")]
    [SerializeField]
    private int MaxSelection;
    [SerializeField]
    internal int selectionCounter = 0;
    [SerializeField]
    internal int ResultCounter = 0;
    internal int quickpickCounter = 10;

    [Header("Vectors")]
    [SerializeField]
    private Vector2 initialPosition;
    [SerializeField]
    private int middlePosition;
    [SerializeField]
    private int finalPosition;

    [Header("Scripts")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Animation_Controller animation_Controller;

    [Header("Text")]
    [SerializeField]
    private TMP_Text MainNumber_Text;

    [Header("GameObject")]
    [SerializeField]
    private GameObject DisableScreen_object;
    [SerializeField]
    private List<Transform> Win_Transform;
    [SerializeField]
    private List<Transform> TempWin_Transform;
    [SerializeField] private Transform board_transform;
    [SerializeField] private Transform lamp_transform;
    [SerializeField] internal Color textColor;
    [SerializeField] private GameObject[] fortuneHighlighter;


    [Header("Fortune")]
    [SerializeField] internal Sprite[] fortuenSprite;
    [SerializeField] internal Sprite[] fortuenResultSprite;
    [SerializeField] private List<int> FortuneList;

    [Header("play button")]
    [SerializeField] private Button PlayButton;


    internal void PickRandoms()
    {
        SelectedList.Clear();
        SelectedList.TrimExcess();
        foreach (KenoButton kc in KenoButtonScripts)
        {
            kc.ResetButton();
        }

        templist.Clear();
        templist.TrimExcess();
        templist = GenerateRandomNumbers(quickpickCounter);
        selectionCounter = 0;

        for (int i = 0; i < templist.Count; i++)
        {
            KenoButtonScripts[templist[i]].OnKenoSelect();
            //selectionCounter++;
        }
    }

    internal void CheckTransform(Transform thisObject)
    {
        if (thisObject.childCount <= 1)
        {
            TempWin_Transform[TempWin_Transform.Count - 1].SetParent(thisObject);
            TempWin_Transform[TempWin_Transform.Count - 1].localPosition = new Vector2(2.6f, 0);
            TempWin_Transform[TempWin_Transform.Count - 1].SetAsFirstSibling();
            TempWin_Transform[TempWin_Transform.Count - 1].gameObject.SetActive(true);
            TempWin_Transform.RemoveAt(TempWin_Transform.Count - 1);
        }
        else
        {
            Transform temp = null;
            foreach (Transform t in TempWin_Transform)
            {
                if (t == thisObject.GetChild(0))
                {
                    temp = t;
                    break;
                }
            }
            TempWin_Transform.Remove(temp);
            thisObject.GetChild(0).gameObject.SetActive(true);
        }
    }

    private List<int> GenerateRandomNumbers(int count, bool fortune = false)
    {
        List<int> possibleNumbers = new List<int>();
        List<int> chosenNumbers = new List<int>();

        for (int index = 1; index < 81; index++)
            possibleNumbers.Add(index);

        while (chosenNumbers.Count < count)
        {
            int position = Random.Range(0, possibleNumbers.Count);
            chosenNumbers.Add(possibleNumbers[position]);
            possibleNumbers.RemoveAt(position);
        }

        return chosenNumbers;
    }


    internal void AddKeno(int value)
    {
        selectionCounter++;
        if (!uiManager.isReset)
        {
            SelectedList.Add(value);
        }

        if (selectionCounter >= 2)
        {
            uiManager.CheckPlayButton(true);
        }
        else
        {
            uiManager.CheckPlayButton(false);
        }
        uiManager.BetAmountUpdate(selectionCounter);
        //uiManager.UpdateSelectedText(selectionCounter);
    }

    internal void RemoveKeno(int value)
    {
        SelectedList.Remove(value);
        if (selectionCounter >= 2)
        {
            uiManager.CheckPlayButton(true);
        }
        else
        {
            uiManager.CheckPlayButton(false);
        }

        uiManager.BetAmountUpdate(selectionCounter);

        //uiManager.UpdateSelectedText(selectionCounter);
    }

    internal void ShowMaxPopup()
    {
        uiManager.MaxPopupEnable();
    }

    internal void PlayDummyGame()
    {
        uiManager.UpdateBalance();
        uiManager.ResetPaytable();
        TempWin_Transform.Clear();
        TempWin_Transform.TrimExcess();
        TempWin_Transform.AddRange(Win_Transform);
        if (DisableScreen_object) DisableScreen_object.SetActive(true);
        ResultList.Clear();
        ResultList.TrimExcess();
        ResultList = GenerateRandomNumbers(20, true);
        winArray.Clear();
        StartCoroutine(PlayGameRoutine());
    }

    private IEnumerator PlayGameRoutine()
    {

        for (int i = 0; i < fortuneHighlighter.Length; i++)
        {
            fortuneHighlighter[i].SetActive(false);
        }

        ResetBalls();
        for (int i = 0; i < FortuneList.Count; i++)
        {
            if (FortuneList[i] == FortuneList[i])
            {
                KenoButtonScripts[i].SelectFortune(); ;
                //winArray.Add(ResultList[index]);
                //break;

            }
        }
        yield return new WaitForSeconds(0.2f);
        animation_Controller.lampAnimation.StartAnimation();
        for (int i = 0; i < Balls_Transform.Count; i++)
        {
            StartCoroutine(AnimationForBalls(Balls_Transform[i], Balls_Text[i], i, FortuneList));
            yield return new WaitForSeconds(0.15f);
            //Transform temp = null;
        }
        yield return new WaitForSeconds(0.15f);
        animation_Controller.lampAnimation.StopAnimation();
        for (int i = 0; i < ResultList.Count; i++)
        {
            for (int j = 0; j < SelectedList.Count; j++)
            {
                if (SelectedList[j] == ResultList[i])
                    winArray.Add(ResultList[i]);
            }
        }
        if (winArray.Count > 0)
            yield return animation_Controller.TigerBannerAnim(winArray.Count, PlayButton);

        //for (int i = 0; i < ResultList.Count; i++)
        //{
        //    for (int j = 0; j < FortuneList.Count; j++)
        //    {
        //        if (FortuneList[j] == ResultList[i])
        //            ForturnWinArray.Add(ResultList[i]);
        //    }
        //}

        uiManager.CheckFinalWinning(winArray, 20);
        uiManager.UpdateBalance(20);
        uiManager.EnableReset();
        if (DisableScreen_object) DisableScreen_object.SetActive(false);
    }

    IEnumerator AnimationForBalls(Transform balls, TMP_Text ball_text, int index, List<int> FortuneList)
    {


        KenoButton tempButton = null;
        //KenoButton tempFortuneButton = null;

        for (int j = 0; j < KenoButtonScripts.Count; j++)
        {

                if (KenoButtonScripts[j].value == ResultList[index])
                {

                    tempButton = KenoButtonScripts[j];
                    //winArray.Add(ResultList[index]);
                    break;
                }
            
        }



        yield return new WaitForSeconds(0.1f);
        ball_text.text = ResultList[index].ToString();
        balls.DOScale(2.5f, 0.15f);
        balls.DOLocalMoveY(-180, 0.15f);
        yield return new WaitForSeconds(0.15f);
        balls.DOScale(1, 1f);
        if(tempButton)
        balls.DOLocalMove(tempButton.transform.localPosition, 0.3f);

        yield return new WaitForSeconds(0.3f);
        balls.gameObject.SetActive(false);
        balls.SetParent(lamp_transform);
        balls.localPosition = new Vector3(0, 0, 0);
        if (tempButton)
            tempButton.ResultColor();


    }

    internal void highlightFortune(int index) {
        if (index >= 0) {

            fortuneHighlighter[index].SetActive(true);
        }

    }

    void ResetBalls()
    {


        for (int i = 0; i < Balls_Transform.Count; i++)
        {
            Balls_Transform[i].SetParent(board_transform);
            Balls_Transform[i].gameObject.SetActive(true);
            Balls_Text[i].text = "";
        }

    }
    internal void ActivateWinning()
    {
        uiManager.CheckWinnings(ResultCounter);
    }

    internal void ResetButtons()
    {
        for (int i = 0; i < KenoButtonScripts.Count; i++)
        {
            KenoButtonScripts[i].ResetButton();
        }


        ResultCounter = 0;
        selectionCounter = 0;

        for (int i = 0; i < SelectedList.Count; i++)
        {
            KenoButtonScripts[SelectedList[i] - 1].OnKenoSelect();
        }

        //foreach (Transform ts in Balls_Transform)
        //{
        //    ts.localPosition = initialPosition;
        //}
        //if (MainNumber_Text) MainNumber_Text.text = "00";
    }

    internal void ResetWinAnim()
    {
        foreach (Transform t in Win_Transform)
        {
            t.gameObject.SetActive(false);
        }
        TempWin_Transform.Clear();
        TempWin_Transform.TrimExcess();
    }

    internal void CleanPage()
    {
        for (int i = 0; i < KenoButtonScripts.Count; i++)
        {
            KenoButtonScripts[i].ResetButton();
        }

        SelectedList.Clear();
        SelectedList.TrimExcess();
        ResultCounter = 0;
        selectionCounter = 0;
    }
}
