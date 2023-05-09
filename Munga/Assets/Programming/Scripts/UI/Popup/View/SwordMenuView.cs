using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Assets.Scripts.UI.Popup.PopupView
{
    public class SwordMenuView : ViewBase
    {
        [Header("좌측 버튼들")]
        [SerializeField] private Button buttonProperty; // 속성
        [SerializeField] private Button buttonSacared; // 성유물

        //[SerializeField] private Image whiteBG;


        private Animator _uiAnimator;


        #region Level

        private int _currentExp;
        private int _currentLevel;
        [SerializeField] private RectTransform LevelLayoutGroup;
        private Text _currentLevelText;
        private Text _maxLevelText;

        #endregion
        private void Awake()
        {
            _uiAnimator = GetComponent<Animator>();

            _currentLevelText = LevelLayoutGroup.transform.GetChild(0).GetComponent<Text>();
            _maxLevelText = LevelLayoutGroup.transform.GetChild(1).GetComponent<Text>();

            _currentLevel = 1;
        }

        private void Start()
        {
            Show();
        }
        
        #region ::: LevelText :::

        public void ChangeExp(int exp)
        {
            // 총 경험치량에서 변경되는 수 만큼 레벨로 보냄
            ChangeLevel();            
        }

        private void ChangeLevel()
        {
            _currentLevelText.text = _currentLevel.ToString();
            // 00 / 00 일때 80 , 55
            // 0 . 00 일때 60, 55
            int ten = 80;
            int one = 60;
            if (_currentLevel % 10 >= 1) // 10 이상
            {
                Debug.Log("10 이상 레벨 적용");
                _currentLevelText.GetComponent<RectTransform>().sizeDelta = new Vector2(ten, 55);
                LevelLayoutGroup.sizeDelta = new Vector2(ten,55);
            }
            else // 10이하
            {
                _currentLevelText.GetComponent<RectTransform>().sizeDelta = new Vector2(one, 55);
                LevelLayoutGroup.sizeDelta = new Vector2(one,55);
            }
        }
        #endregion
        
        public override void Show()
        {
            base.Show();
            //_uiAnimator.SetTrigger("Show");
        }

        public override void Hide()
        {
            base.Hide();
            //_uiAnimator.SetTrigger("Hide");
        }
    }
}