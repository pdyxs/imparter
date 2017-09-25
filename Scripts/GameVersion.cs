using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Imparter.Internal;

namespace Imparter
{
    [System.Serializable]
    public class GameVersion
    {
        public string name;

        public string versionString;

        [TextArea]
        public string notes;

        public bool completed = false;

        public SerialisedLong completedTime;

    }
}