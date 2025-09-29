using UnityEngine;

namespace Auto
{
    public class DisableObjects : MonoBehaviour
    {
        public GameObject theObject;

        private Renderer[] _renders;

        private void Start()
        {
            Component[] comps = theObject.transform.GetComponentsInChildren(typeof(Renderer));
            _renders = new Renderer[comps.Length];
            for (int i = 0; i < comps.Length; i++)
                _renders[i] = comps[i] as Renderer;
            if (_renders == null)
                _renders = new Renderer[0];
        }

        private void OnTriggerEnter()
        {
            foreach (Renderer rend in _renders)
                rend.enabled = false;
        }

        private void OnTriggerExit()
        {
            foreach (Renderer rend in _renders)
                rend.enabled = true;
        }
    }
}