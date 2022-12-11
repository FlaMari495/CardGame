using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

namespace FlMr_CardBase
{
    /// <summary>
    /// �J�[�h�̈ʒu����ێ��A�X�V����N���X
    /// </summary>
    internal class DataManager : MonoBehaviour
    {
        /// <summary>
        /// �J�[�h�̈ʒu���
        /// </summary>
        private CardObjectData Data { get; set; }

        /// <summary>
        /// �SPosition�������Ƃɏ���������
        /// </summary>
        /// <param name="positions"></param>
        internal void Initialize(List<Type> positions)
        {
            Data = new CardObjectData(positions);
        }

        /// <summary>
        /// �J�[�h��V������������
        /// </summary>
        /// <param name="posType">�����ʒu</param>
        /// <param name="index">�J�[�h�����Ԗڂɑ}�����邩</param>
        /// <param name="positiveCardId">�J�[�h��Id�Ƃ��Ď��R�����g�p���邩�A���̐������g�p���邩</param>
        internal void AddCard(Type posType, int? index,bool positiveCardId)
        {
            Data.AddCard(posType, index, positiveCardId);
        }

        /// <summary>
        /// �J�[�h���ړ�����
        /// </summary>
        /// <param name="cardObjId">�ړ��Ώۂ̃J�[�hId</param>
        /// <param name="posTo">�����ʒu</param>
        /// <param name="index">�J�[�h�����Ԗڂɑ}�����邩</param>
        internal void MoveCard(int cardObjId,Type posTo, int? index)
        {
            Data.MoveCard(cardObjId, posTo, index);
        }

        /// <summary>
        /// �J�[�h�̏�񂷂ׂĂ�Json�`���ɕϊ�����
        /// </summary>
        /// <returns></returns>
        internal string GetJsonData() => JsonUtility.ToJson(Data);

        /// <summary>
        /// �J�[�h�̈ʒu����ێ�����N���X
        /// </summary>
        [Serializable]
        private class CardObjectData
        {
            /// <summary>
            /// �ǂ̈ʒu�ɂǂ̃J�[�h�����݂��邩��ێ�����ϐ�
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
            /// �J�[�h��ǉ�����B
            /// </summary>
            /// <param name="posType">�J�[�h�̒ǉ��ꏊ</param>
            /// <param name="index">�J�[�h�����Ԗڂɑ}�����邩</param>
            /// <param name="positiveCardId">Id�Ƃ��āA���R�����g�p��r�����̐������g�p���邩</param>
            internal void AddCard(Type posType, int? index, bool positiveCardId)
            {
                // posId�̎擾�@
                // (Type ��string�ɕϊ����邾��)
                string posId = posType.ToString();

                // �J�[�h�I�u�W�F�N�g��Id�̌v�Z
                int newId = positiveCardId switch
                {
                    // id�����̏ꍇ�A�g�p�ς݂�id�̒��ōő�̐��� + 1
                    true => dataCore.Values.SelectMany(l => l).DefaultIfEmpty().Max() + 1,

                    // id�����̏ꍇ�A�g�p�ς݂�id�̒��ōŏ��̐��� - 1
                    false => dataCore.Values.SelectMany(l => l).DefaultIfEmpty().Min() - 1,
                };

                // id��}��
                // �}���ʒu(index)��null�̏ꍇ�͍Ō��
                // null�łȂ�(int�̏ꍇ) index�����̂܂܎g�p
                dataCore[posId].Insert(index ?? dataCore[posId].Count, newId);
            }

            /// <summary>
            /// �w�肵���J�[�h�̈ʒuid���擾����
            /// </summary>
            /// <param name="cardObjId"></param>
            /// <returns>�z�u�ꏊ</returns>
            internal Type GetCardPos(int cardObjId)
            {
                foreach (var item in dataCore)
                {
                    if (item.Value.Contains(cardObjId)) return Type.GetType(item.Key);
                }

                throw new Exception($"�J�[�hId = {cardObjId} �͑��݂��܂���");
            }

            /// <summary>
            /// �w�肵���J�[�h�̈ʒu��ύX����
            /// </summary>
            /// <param name="cardObjId">�J�[�h�I�u�W�F�N�g��Id</param>
            /// <param name="posTo">�ړ���</param>
            /// <param name="afterCard">�}���ʒu�̎w��(�}���ʒu����̃J�[�h���w�肷��) �Ō���̏ꍇ��null</param>
            internal void MoveCard(int cardObjId, Type posTo, int? index)
            {
                // �ړ��������J�[�h�����݂ǂ��ɔz�u����Ă��邩
                Type posFrom = GetCardPos(cardObjId);

                // �ړ��Ώۂ̏����폜����
                dataCore[posFrom.ToString()].Remove(cardObjId);

                // �}��
                string posToId = posTo.ToString();
                dataCore[posToId].Insert(index ?? dataCore[posToId].Count, cardObjId);
            }
        }
    }

 
}