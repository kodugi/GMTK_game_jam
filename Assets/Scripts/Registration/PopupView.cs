using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RegistrationNameSpace
{
    public class PopupView: PopupViewBase
    {

        public void Initialize(RegistrationManagerBase registrationManager)
        {
            base.Initialize();
            registrationManager.RaiseTryRegisterEvent += HandleTryRegisterEvent;
        }

        private void HandleTryRegisterEvent(object sender, TryRegisterEventArgs e)
        {
            switch (e.Result)
            {
                case RegistrationResultType.SUCCESS:
                    ShowMessage("registration success!");
                    break;
                case RegistrationResultType.FAILURE_BEFORE_START:
                    ShowMessage("Course registration has not started yet.");
                    break;
                case RegistrationResultType.FAILURE_QUOTA_EXCEEDED:
                    ShowMessage("Total quota exceeded.");
                    break;
                case RegistrationResultType.FAILURE_MAXIMUM_CREDIT_EXCEEDED:
                    ShowMessage("Maximum credits exceeded.");
                    break;
                case RegistrationResultType.FAILURE_COURSE_NOT_SELECTED:
                    ShowMessage("Please select a course.");
                    break;
                case RegistrationResultType.FAILURE_TIMETABLE_OVERLAP:
                    ShowMessage("Selected course has overlapping schedule with registered ones.");
                    break;
                case RegistrationResultType.FAILURE_COURSE_ID_OVERLAP:
                    ShowMessage("Selected course has overlapping course id with registered ones.");
                    break;
            }
        }
    }
}