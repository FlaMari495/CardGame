using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

namespace FlMr_CardBase
{
    /// <summary>
    /// カードの位置情報を保持、更新するクラス
    /// </summary>
    internal class DataManager : MonoBehaviour
    {
        /// <summary>
        /// カードの位置情報
        /// </summary>
        private CardObjectData Data { get; set; }

        /// <summary>
        /// 全Position情報をもとに初期化する
        /// </summary>
        /// <param name="positions"></param>
        internal void Initialize(List<Type> positions)
        {
            Data = new CardObjectData(positions);
        }

        /// <summary>
        /// カードの情報すべてをJson形式に変換する
        /// </summary>
        /// <returns></returns>
        internal string GetJsonData() => JsonUtility.ToJson(Data);

        /// <summary>
        /// カードの位置情報を保持するクラス
        /// </summary>
        [Serializable]
        private class CardObjectData
        {
            /// <summary>
            /// どの位置にどのカードが存在するかを保持する変数
            /// </summary>
            [SerializeField] private SerializableDictionary<string, List<int>> dataCore;

            internal CardObjectData(List<Type> positions)
            {
                dataCore = new SerializableDictionary<string, List<int>>();

                foreach (var posInfo in positions)
                {
                    dataCore.Add(posInfo.ToString(), new List<int>());
                }

            }
        }
    }

}