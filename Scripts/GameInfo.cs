using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Imparter
{
    public class GameInfo : ScriptableObject
    {

        public static GameInfo Get()
        {
#if !UNITY_EDITOR
        if (_gameInfo == null) 
#endif
            {
                _gameInfo = Resources.Load("Game Info") as GameInfo;
            }
            return _gameInfo;
        }
        private static GameInfo _gameInfo;

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