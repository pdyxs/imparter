using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Imparter.View
{
    public class GameInfoView : MonoBehaviour
    {

        public Text nameText;

        public Text versionText;

        public Button infoButton;

        public Button feedbackButton;

        public VersionInfoView versionInfo;

        private void Start()
        {
            nameText.text = GameInfo.Get().gameName;

            var vtext = "";
            GameVersion v = GameInfo.Get().LatestVersion;
            if (v != null)
            {
                vtext = "v" + v.versionString;
            }
            if (GameInfo.Get().isBetweenVersions)
            {
                vtext += "*";
            }
            versionText.text = vtext;

            feedbackButton.onClick.AddListener(OnEmailClicked);

            infoButton.onClick.AddListener(OnInfoClicked);
        }

        private void OnInfoClicked()
        {
            versionInfo.Show();
        }

        private void OnEmailClicked()
        {
            var str = "mailto:" + GameInfo.Get().feedbackEmail +
                                "?subject=" + GameInfo.Get().gameName +
                                "%20v" + GameInfo.Get().LatestVersion.versionString +
                                (GameInfo.Get().isBetweenVersions ? "*" : "") +
                                          "%20Feedback";

            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                Application.ExternalEval("window.open(\"" + str + "\",\"_blank\")");
            }
            else
            {
                Application.OpenURL(str);
            }
        }
    }
}