using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Imparter.View
{
    public class VersionInfoView : MonoBehaviour
    {

        public Text titleText;

        public Text descriptionText;

        public Text notesText;

        public Button nextButton;

        public Button prevButton;

        private GameVersion currentVersion;

        public void Show()
        {
            SetupVersion(GameInfo.Get().LatestVersion);
            gameObject.SetActive(true);

            nextButton.onClick.AddListener(Next);
            prevButton.onClick.AddListener(Prev);
        }

        private void Prev()
        {
            SetupVersion(GameInfo.Get().prevVersion(currentVersion));
        }

        private void Next()
        {
            SetupVersion(GameInfo.Get().nextVersion(currentVersion));
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetupVersion(GameVersion v)
        {
            currentVersion = v;
            titleText.text = "v" + v.versionString + ": " + v.name;
            descriptionText.text = v.notes;
            if (v.completed == false)
            {
                notesText.text = "A future version - this is what I'm working on now!";
            }
            else
            {
                notesText.text = "Published on " + v.completedTime.date.ToLongDateString();
            }
            nextButton.interactable = GameInfo.Get().nextVersion(v) != null;
            prevButton.interactable = GameInfo.Get().prevVersion(v) != null;
        }
    }
}