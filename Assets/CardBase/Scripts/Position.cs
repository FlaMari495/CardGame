using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace FlMr_CardBase
{
    public abstract class Position : MonoBehaviour
    {
        [SerializeField] private int cardNumberLimit = -1;
        public int CardNumberLimit => cardNumberLimit;
        public bool CanAdd => cardNumberLimit < 0 || Cards.Count < cardNumberLimit;

        private DataManager Manager { get; set; }
        protected Type PosType { get; private set; }

        internal void Initialize(DataManager manager)
        {
            Manager = manager;
            PosType = this.GetType();
        }

        /// <summary>
        /// この位置に存在するすべてのカード
        /// </summary>
        public List<int> Cards => Manager.GetCardsOfPos(PosType);

        /// <summary>
        /// カードを新しく生成する
        /// </summary>
        /// <param name="index">生成したカードの挿入位置</param>
        /// <param name="positiveCardId">カードのidを自然数とするか負の整数にするか</param>
        /// <returns>生成に成功したか否か</returns>
        public bool AddCard(int? index,bool positiveCardId)
        {
            // 枚数制限をオーバーしないことの確認
            if (CanAdd)
            {
                // カードの追加
                Manager.AddCard(PosType, index, positiveCardId);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}