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
        /// カードを新しく生成する
        /// </summary>
        /// <param name="posType">生成位置</param>
        /// <param name="index">カードを何番目に挿入するか</param>
        /// <param name="positiveCardId">カードのIdとして自然数を使用するか、負の整数を使用するか</param>
        internal void AddCard(Type posType, int? index,bool positiveCardId)
        {
            Data.AddCard(posType, index, positiveCardId);
        }

        /// <summary>
        /// カードを移動する
        /// </summary>
        /// <param name="cardObjId">移動対象のカードId</param>
        /// <param name="posTo">生成位置</param>
        /// <param name="index">カードを何番目に挿入するか</param>
        internal void MoveCard(int cardObjId,Type posTo, int? index)
        {
            Data.MoveCard(cardObjId, posTo, index);
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


            /// <summary>
            /// カードを追加する。
            /// </summary>
            /// <param name="posType">カードの追加場所</param>
            /// <param name="index">カードを何番目に挿入するか</param>
            /// <param name="positiveCardId">Idとして、自然数を使用すrか負の整数を使用するか</param>
            internal void AddCard(Type posType, int? index, bool positiveCardId)
            {
                // posIdの取得　
                // (Type をstringに変換するだけ)
                string posId = posType.ToString();

                // カードオブジェクトのIdの計算
                int newId = positiveCardId switch
                {
                    // idが正の場合、使用済みのidの中で最大の整数 + 1
                    true => dataCore.Values.SelectMany(l => l).DefaultIfEmpty().Max() + 1,

                    // idが負の場合、使用済みのidの中で最小の整数 - 1
                    false => dataCore.Values.SelectMany(l => l).DefaultIfEmpty().Min() - 1,
                };

                // idを挿入
                // 挿入位置(index)がnullの場合は最後尾
                // nullでない(intの場合) indexをそのまま使用
                dataCore[posId].Insert(index ?? dataCore[posId].Count, newId);
            }

            /// <summary>
            /// 指定したカードの位置idを取得する
            /// </summary>
            /// <param name="cardObjId"></param>
            /// <returns>配置場所</returns>
            internal Type GetCardPos(int cardObjId)
            {
                foreach (var item in dataCore)
                {
                    if (item.Value.Contains(cardObjId)) return Type.GetType(item.Key);
                }

                throw new Exception($"カードId = {cardObjId} は存在しません");
            }

            /// <summary>
            /// 指定したカードの位置を変更する
            /// </summary>
            /// <param name="cardObjId">カードオブジェクトのId</param>
            /// <param name="posTo">移動先</param>
            /// <param name="afterCard">挿入位置の指定(挿入位置直後のカードを指定する) 最後尾の場合はnull</param>
            internal void MoveCard(int cardObjId, Type posTo, int? index)
            {
                // 移動したいカードが現在どこに配置されているか
                Type posFrom = GetCardPos(cardObjId);

                // 移動対象の情報を削除する
                dataCore[posFrom.ToString()].Remove(cardObjId);

                // 挿入
                string posToId = posTo.ToString();
                dataCore[posToId].Insert(index ?? dataCore[posToId].Count, cardObjId);
            }
        }
    }

 
}