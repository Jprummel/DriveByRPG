using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TurnOrder : MonoBehaviour
{
    public delegate void OnCharacterDeath(Character killedCharacter);
    public static OnCharacterDeath OnDeath;

    [SerializeField]private List<GameObject> _turnBoxes = new List<GameObject>();
    [SerializeField]private List<GameObject> _activeTurnBoxes = new List<GameObject>();

    [SerializeField]private List<Transform> _boxPositions = new List<Transform>();
    [SerializeField] private List<Transform> _activeBoxPositions = new List<Transform>();

    private List<Character> _activeCharacters = new List<Character>();

    private RectTransform _turnBackgroundRT;

    private Sequence _blinkSqn;

    private Tween _changeTurnboxes;
    private Tween _removeCharacterTween;

    private TweenCallback tCallback;

    private Vector2 _rtSizeDelta = new Vector2(270, 15);

    private void Start()
    {
        OnDeath += RemoveCharacter;

        _turnBackgroundRT = GetComponent<RectTransform>();

        _activeCharacters = TurnBasedStateMachine.ActiveCharacters;

        //Tween Turn order stuff
        StartCoroutine(SetStartSize());

        //insert active character names to turn order
        //BlinkCurrentCharacter(_turnBoxes[0]);
    }

    /// <summary>
    /// Reorder the turnorder of all active characters
    /// </summary>
    public void ReorderList()
    {
        //Make a temporary list to store the old list elements in.
        List<GameObject> tmpList = new List<GameObject>();

        for (int i = 0; i < _activeTurnBoxes.Count; i++)
        {
            tmpList.Add(_activeTurnBoxes[i]);
        }
        //Clear old list
        _activeTurnBoxes.Clear();
        int lastIndex = tmpList.Count - 1;
        //Reorder the list so that everything moves down 1 slot.
        for (int i = 0; i < tmpList.Count; i++)
        {
            if (i != lastIndex)
            {
                _activeTurnBoxes.Add(tmpList[i + 1]);
            }
            else
            {
                _activeTurnBoxes.Add(tmpList[0]);
            }
        }
        ChangeTurnPositions();
        StopBlinking();
        BlinkCurrentCharacter(_activeTurnBoxes[0]);
    }

    /// <summary>
    /// Change the actual positions of the character boxes.
    /// </summary>
    private void ChangeTurnPositions()
    {
        for (int i = 0; i < _activeTurnBoxes.Count; i++)
        {
            _changeTurnboxes = _activeTurnBoxes[i].gameObject.transform.DOMove(_activeBoxPositions[i].transform.position, 1f).SetEase(Ease.OutCubic);
        }
    }   

    /// <summary>
    /// Make the first characters box blink.
    /// </summary>
    /// <param name="currentCharacter">The box that will blink(first in the list)</param>
    private void BlinkCurrentCharacter(GameObject currentCharacter)
    {
        Image imageToBlink;
        imageToBlink = currentCharacter.GetComponent<Image>();      

        _blinkSqn = DOTween.Sequence();

        tCallback = () => ResetCallback(imageToBlink);
        _blinkSqn.OnKill(tCallback);

        _blinkSqn.Append(imageToBlink.DOColor(Color.green, 1f).SetEase(Ease.Flash, overshoot:2.25f));
        _blinkSqn.SetLoops(-1, loopType: LoopType.Yoyo);
    }

    private void StopBlinking()
    {
        _blinkSqn.Kill();
    }

    private void ResetCallback(Image image)
    {
        image.color = Color.white;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="killedCharacter"></param>
    private void RemoveCharacter(Character killedCharacter)
    {
        for (int i = 0; i < _activeTurnBoxes.Count; i++)
        {
            if (killedCharacter == _activeTurnBoxes[i].GetComponent<TurnOrderBoxCharacter>().LinkedCharacter)
            {
                TweenCallback callback = () => OnRemoveComplete(i);
                _removeCharacterTween = _activeTurnBoxes[i].transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(callback);
                _activeBoxPositions.RemoveAt(_activeBoxPositions.Count - 1);
                break;
            }
        }
    }

    private void OnRemoveComplete(int listPos)
    {
        _turnBackgroundRT.DOSizeDelta(new Vector2(_turnBackgroundRT.sizeDelta.x, _turnBackgroundRT.sizeDelta.y - 60), 0.5f).SetEase(Ease.InBack);
        _activeTurnBoxes[listPos].SetActive(false);
        _activeTurnBoxes.RemoveAt(listPos);
        ReorderList();
    }

    IEnumerator SetStartSize()
    {
        //bounce in
        /*for (int i = 0; i < _activeCharacters.Count; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _rtSizeDelta.y += 60;
            _turnBackgroundRT.DOSizeDelta(_rtSizeDelta, 0.25f).SetEase(Ease.InOutExpo);
            yield return new WaitForSeconds(0.1f);
            _activeTurnBoxes.Add(_turnBoxes[i]);
            _activeTurnBoxes[i].GetComponent<TurnOrderBoxCharacter>().LinkedCharacter = _activeCharacters[i];
            _activeTurnBoxes[i].SetActive(true);
            _turnBoxes[i].GetComponentInChildren<Text>().text = _activeCharacters[i].Name;
            _activeBoxPositions.Add(_boxPositions[i]);
            _activeTurnBoxes[i].transform.DOMoveX(_boxPositions[i].position.x, 0.5f).SetEase(Ease.OutBack, overshoot:1.25f);
        }*/

        //fade in
        for (int i = 0; i < _activeCharacters.Count; i++)
        {
            yield return new WaitForSeconds(0.075f);
            _rtSizeDelta.y += 60;
            _turnBackgroundRT.DOSizeDelta(_rtSizeDelta, 0.20f).SetEase(Ease.InOutExpo);

            yield return new WaitForSeconds(0.075f);
            _activeTurnBoxes.Add(_turnBoxes[i]);
            _activeTurnBoxes[i].GetComponent<TurnOrderBoxCharacter>().LinkedCharacter = _activeCharacters[i];
            _activeTurnBoxes[i].SetActive(true);
            _activeTurnBoxes[i].transform.DOMoveX(_boxPositions[i].position.x, 0.45f).SetEase(Ease.OutExpo);

            yield return new WaitForSeconds(0.05f);
            _activeTurnBoxes[i].GetComponent<Image>().DOFade(1, 0.15f);
            _turnBoxes[i].GetComponentInChildren<Text>().text = _activeCharacters[i].Name;
            _activeBoxPositions.Add(_boxPositions[i]);
        }
        BlinkCurrentCharacter(_activeTurnBoxes[0]);
    }

    private void OnDisable()
    {
        OnDeath -= RemoveCharacter;
    }
}
