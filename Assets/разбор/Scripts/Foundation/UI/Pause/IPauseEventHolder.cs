using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.UI
{
    public interface IPauseEventHolder
    {
        void OnPause(bool shown);
    }
}
