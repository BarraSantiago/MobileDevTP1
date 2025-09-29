using UnityEngine;

namespace Escenas.Juego.Calibracion
{
    public class CopyMove : MonoBehaviour
    {
        public Transform Target;
        //public float Diferencia = 1;

        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            transform.position = Target.position; // + Target.transform.right * Diferencia;
            //transform.localRotation = Target.localRotation;
        }
    }
}