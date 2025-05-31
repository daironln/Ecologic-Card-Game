using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
public class HorizontalCardHolder : MonoBehaviour
{

    [SerializeField] private Card selectedCard;
    [SerializeReference] private Card hoveredCard;

    [SerializeField] private GameObject slotPrefab;
    private RectTransform rect;

    [Header("Spawn Settings")]
    [SerializeField] private int cardsToSpawn = 7;
    public List<Card> cards = new List<Card>();

    bool isCrossing = false;
    [SerializeField] private bool tweenCardReturn = true;

    public Card AddCard(EcologicCradType type, bool isPc = false, bool canHold = true)
    {
        
        var obj = Instantiate(slotPrefab, transform);
        

        rect = GetComponent<RectTransform>();
        var card = obj.GetComponentInChildren<Card>();

        cards.Add(card);

        int cardCount = 0;

        card.Type = type;
        card.Init();
        card.PointerEnterEvent.AddListener(CardPointerEnter);
        card.PointerExitEvent.AddListener(CardPointerExit);
        card.BeginDragEvent.AddListener(BeginDrag);
        card.EndDragEvent.AddListener(EndDrag);
        card.name = cardCount.ToString();

        if(isPc)
            canHold = false;

        card.PlayerId = canHold ? 1 : 0;

        card.cardVisual.SetTexts(!isPc);

        card.cardVisual.cardImage.sprite = GameManager.Instance.Images[isPc ? 16 : (int)type];



        card.Effect.SetCard(card);


        cardCount++;
        

        StartCoroutine(Frame());

        IEnumerator Frame()
        {
            yield return new WaitForSecondsRealtime(.1f);
            
            if (card.cardVisual != null)
                card.cardVisual.UpdateIndex(transform.childCount);
            
        }

        return card;
    }

    public void RemoveCard(Card card)
    {
        Debug.Log($"Removing card.. {card.Type} from {this.gameObject.name}");

        cards.Remove(card);
        
        // Destroy(card.transform.parent.gameObject);

        AnimateDestroy(card.cardVisual);

        // card.transform.parent.gameObject.SetActive(false);
        

        // card.OnDestroy();


    }

    private void AnimateDestroy(CardVisual obj)
    {
        Debug.Log($"Visual: {obj.transform.name}");
        void DestroyObj()
        {
            Destroy(obj.parentCard.transform.parent.gameObject);
        }

        obj.GetComponent<RectTransform>().DOBlendableScaleBy(new Vector3(-0.7f, -0.7f, -0.7f), 0.2f).SetEase(Ease.OutBack).OnComplete(DestroyObj);
    }

    private void BeginDrag(Card card)
    {
        selectedCard = card;
    }


    void EndDrag(Card card)
    {
        if (selectedCard == null)
            return;

        selectedCard.transform.DOLocalMove(selectedCard.selected ? new Vector3(0,selectedCard.selectionOffset,0) : Vector3.zero, tweenCardReturn ? .15f : 0).SetEase(Ease.OutBack);

        rect.sizeDelta += Vector2.right;
        rect.sizeDelta -= Vector2.right;

        selectedCard = null;

    }

    void CardPointerEnter(Card card)
    {
        hoveredCard = card;
    }

    void CardPointerExit(Card card)
    {
        hoveredCard = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if (hoveredCard != null)
            {
                Destroy(hoveredCard.transform.parent.gameObject);
                cards.Remove(hoveredCard);

            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            foreach (Card card in cards)
            {
                card.Deselect();
            }
        }

        if (selectedCard == null)
            return;

        if (isCrossing)
            return;

        for (int i = 0; i < cards.Count; i++)
        {

            if (selectedCard.transform.position.x > cards[i].transform.position.x)
            {
                if (selectedCard.ParentIndex() < cards[i].ParentIndex())
                {
                    Swap(i);
                    break;
                }
            }

            if (selectedCard.transform.position.x < cards[i].transform.position.x)
            {
                if (selectedCard.ParentIndex() > cards[i].ParentIndex())
                {
                    Swap(i);
                    break;
                }
            }
        }
    }

    void Swap(int index)
    {
        isCrossing = true;

        Transform focusedParent = selectedCard.transform.parent;
        Transform crossedParent = cards[index].transform.parent;

        cards[index].transform.SetParent(focusedParent);
        cards[index].transform.localPosition = cards[index].selected ? new Vector3(0, cards[index].selectionOffset, 0) : Vector3.zero;
        selectedCard.transform.SetParent(crossedParent);

        isCrossing = false;

        if (cards[index].cardVisual == null)
            return;

        bool swapIsRight = cards[index].ParentIndex() > selectedCard.ParentIndex();
        cards[index].cardVisual.Swap(swapIsRight ? -1 : 1);

        //Updated Visual Indexes
        foreach (Card card in cards)
        {
            card.cardVisual.UpdateIndex(transform.childCount);
        }
    }

}
