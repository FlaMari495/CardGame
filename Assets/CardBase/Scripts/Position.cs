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
        /// ���̈ʒu�ɑ��݂��邷�ׂẴJ�[�h
        /// </summary>
        public List<int> Cards => Manager.GetCardsOfPos(PosType);

        /// <summary>
        /// �J�[�h��V������������
        /// </summary>
        /// <param name="index">���������J�[�h�̑}���ʒu</param>
        /// <param name="positiveCardId">�J�[�h��id�����R���Ƃ��邩���̐����ɂ��邩</param>
        /// <returns>�����ɐ����������ۂ�</returns>
        public bool AddCard(int? index,bool positiveCardId)
        {
            // �����������I�[�o�[���Ȃ����Ƃ̊m�F
            if (CanAdd)
            {
                // �J�[�h�̒ǉ�
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