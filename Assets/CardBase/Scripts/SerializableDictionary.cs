using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace FlMr_CardBase
{
    /// <summary>
    /// シリアル化可能な辞書クラス
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [Serializable]
        private class DataCore
        {
            public DataCore(List<Pair> pair) => Pair = pair;
            public List<Pair> Pair;
        }

        [Serializable]
        private struct Pair
        {
            internal Pair(KeyValuePair<TKey, TValue> pair)
            {
                key = pair.Key;
                value = pair.Value;
            }
            public TKey key;
            public TValue value;
        }

        /// <summary>
        /// シリアル化されるKey,Valueペア
        /// </summary>
        [SerializeField] private DataCore dataCore;

        /// <summary>
        /// デシリアライズのコールバック
        /// </summary>
        public void OnAfterDeserialize()
        {
            // 初期値をもとに辞書を復元
            dataCore.Pair.ForEach(x => Add(x.key, x.value));
        }

        /// <summary>
        /// シリアライズのコールバック
        /// </summary>
        public void OnBeforeSerialize()
        {
            // 辞書の情報をもとに、シリアライズ用のDataCoreクラスのインスタンスを作成
            dataCore = new(this.ToList().ConvertAll(x => new Pair(x)));
        }
    }
}
