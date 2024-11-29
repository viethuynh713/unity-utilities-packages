/*
 * Author: DevDaoSi
 * @2024
 */
using System.Collections.Generic;
using UnityEngine;

namespace Konzit.UI.SO
{
    [CreateAssetMenu(fileName = "NewPopupContainer", menuName = "ScriptableObject/NewPopupContainer")]
    public class PopupSOs : ScriptableObject
    {
        public List<GameObject> PopupPrefabContainer;
    }
}
