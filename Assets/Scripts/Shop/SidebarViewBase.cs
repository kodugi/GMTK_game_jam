using System;
using System.Collections.Generic;
using CourseNameSpace;
using PersistentDataNameSpace;
using RegistrationNameSpace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopNameSpace
{
    public class SidebarViewBase: MonoBehaviour
    {
        [SerializeField] private GameObject _sidebarPanel;
        [SerializeField] private GameObject _perksPanel;
        [SerializeField] private GameObject _perkPrefab;
        [SerializeField] private Button _sidebarOpenButton;
        [SerializeField] private Button _sidebarCloseButton;
        [SerializeField] private HintBoxView _hintBoxView;
        [SerializeField] private TextMeshProUGUI _detailsText;
        [SerializeField] private TextMeshProUGUI _coursePoolText;
        [SerializeField] private TextMeshProUGUI _sidebarExplanationText;

        private List<GameObject> _spawnedPerks;

        public void Initialize(ItemManager itemManager)
        {
            itemManager.RaiseTryPurchaseEvent += HandleTryPurchaseEvent;
            _sidebarOpenButton.onClick.AddListener(OpenSidebar);
            _sidebarCloseButton.onClick.AddListener(CloseSidebar);
            CloseSidebar();
            Refresh();
        }

        private void Refresh()
        {
            SetDetailsText();
            SetCoursePoolText();
            UpdatePerksList();
        }

        protected virtual void SetDetailsText()
        {
            _detailsText.text = "Next round: " + (PersistentData.Round + 1) + "\n" + "Goal: " +
                                PersistentData.GetTargetPoints() + " points";
        }

        private void SetCoursePoolText()
        {
            string text = "<b>Course Types</b>\n";
            foreach (CourseType courseType in Enum.GetValues(typeof(CourseType)))
            {
                text += Course.GetCourseTypeName(courseType) + ": " +
                        PersistentData.CourseTypePool.GetValueOrDefault(courseType, 0) + "\n";
            }

            text += "<b>Departments</b>\n";
            foreach (DepartmentType departmentType in Enum.GetValues(typeof(DepartmentType)))
            {
                if (departmentType == DepartmentType.LIBERAL_ARTS)
                {
                    continue;
                }
                text += Course.GetDepartmentName(departmentType) + ": " +
                        PersistentData.DepartmentPool.GetValueOrDefault(departmentType, 0) + "\n";
            }
            _coursePoolText.text = text;
        }

        private void HandleTryPurchaseEvent(object sender, TryPurchaseEventArgs e)
        {
            if (e.Result == PurchaseResultType.SUCCESS)
            {
                Refresh();
            }
        }

        private void UpdatePerksList()
        {
            if (_spawnedPerks != null && _spawnedPerks.Count > 0)
            {
                foreach (GameObject spawnedPerk in _spawnedPerks)
                {
                    Destroy(spawnedPerk);
                }
            }

            foreach (ItemData perkData in PersistentData.PerkList)
            {
                GameObject perk = Instantiate(_perkPrefab, _perksPanel.transform);
                perk.GetComponent<SidebarPerkView>().Initialize(perkData, _hintBoxView);
            }
        }

        private void OpenSidebar()
        {
            _sidebarOpenButton.gameObject.SetActive(false);
            _sidebarCloseButton.gameObject.SetActive(true);
            _sidebarExplanationText.gameObject.SetActive(false);
            _sidebarPanel.SetActive(true);
            Refresh();
        }

        private void CloseSidebar()
        {
            _sidebarOpenButton.gameObject.SetActive(true);
            _sidebarCloseButton.gameObject.SetActive(false);
            _sidebarExplanationText.gameObject.SetActive(true);
            _sidebarPanel.SetActive(false);
        }
    }
}