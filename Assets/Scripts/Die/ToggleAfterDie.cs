using UnityEngine;

namespace Die
{
    public class ToggleAfterDie : MonoBehaviour, IDie
    {
        [SerializeField] private GameObject _hide;
        [SerializeField] private GameObject _show;
        public void AfterDeath()
        {
            if (_hide != null) _hide.SetActive(false);
            if (_show != null) _show.SetActive(true);
        }
    }
}
