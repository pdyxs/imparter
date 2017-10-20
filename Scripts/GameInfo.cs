using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Imparter
{
    [SaveLocation("Assets/Config/Resources", "Game Info")]
    public class GameInfo : ScriptableConfig<GameInfo>
    {
#if UNITY_EDITOR
        [MenuItem("Assets/Configuration/Game Info")]
        public static void Create()
        {
            create();
        }
#endif

        [HideInInspector]
        public string gameName;

        public string feedbackEmail;

        public bool isBetweenVersions = false;

        [HideInInspector]
        public List<GameVersion> versions;

        public GameVersion LatestVersion
        {
            get
            {
                long latest = 0;
                GameVersion ret = null;
                foreach (var version in versions)
                {
                    if (version.completed && version.completedTime.val > latest)
                    {
                        latest = version.completedTime.val;
                        ret = version;
                    }
                }
                return ret;
            }
        }

        public GameVersion nextVersion(GameVersion v)
        {
            int i = versions.IndexOf(v);
            if (i < 0 || i == versions.Count - 1)
            {
                return null;
            }
            return versions[i + 1];
        }

        public GameVersion prevVersion(GameVersion v)
        {
            int i = versions.IndexOf(v);
            if (i <= 0)
            {
                return null;
            }
            return versions[i - 1];
        }
    }
}