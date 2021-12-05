using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.UI
{
    public interface IHintController
    {
        void Show(string alias);
        void Hide();
    }
}
