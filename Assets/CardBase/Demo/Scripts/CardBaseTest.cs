using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlMr_CardBase.Demo
{
    public class CardBaseTest : MonoBehaviour
    {
        [SerializeField] private DataManager manager;
        [SerializeField] private Hand hand;
        [SerializeField] private Deck deck;

        void Start()
        {
            //カードを4枚追加
            deck.AddCard(0, true); // 「Deck」の「0」番目の位置に「正」のidをもつカードを追加
            deck.AddCard(0, true);
            deck.AddCard(0, true);
            deck.AddCard(0, true);

            // カードを2枚移動
            manager.MoveCard(2,typeof(Hand), null); // CardObjectIdが「2」のカードを「Hand」の「最後尾」に移動
            manager.MoveCard(3,typeof(Hand), null);

            Debug.Log(string.Join(",",hand.Cards));

            // 状況確認
            Debug.Log(manager.GetJsonData());
        }
    }
}