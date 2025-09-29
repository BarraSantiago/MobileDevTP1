using UnityEngine;

namespace Escenas.PuntosFinales
{
    public class FadeInicioFinal : MonoBehaviour
    {
        public float duracion = 2;
        public float vel = 2;

        private Color _aux;

        private MngPts _mng;

        private bool _mngAvisado;
        private float _tiempInicial;

        // Use this for initialization
        private void Start()
        {
            //renderer.material = IniFin;
            _mng = (MngPts)FindObjectOfType(typeof(MngPts));
            _tiempInicial = _mng.tiempEspReiniciar;

            _aux = GetComponent<Renderer>().material.color;
            _aux.a = 0;
            GetComponent<Renderer>().material.color = _aux;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_mng.tiempEspReiniciar > _tiempInicial - duracion) //aparicion
            {
                _aux = GetComponent<Renderer>().material.color;
                _aux.a += Time.deltaTime / duracion;
                GetComponent<Renderer>().material.color = _aux;
            }
            else if (_mng.tiempEspReiniciar < duracion) //desaparicion
            {
                _aux = GetComponent<Renderer>().material.color;
                _aux.a -= Time.deltaTime / duracion;
                GetComponent<Renderer>().material.color = _aux;

                if (!_mngAvisado)
                {
                    _mngAvisado = true;
                    _mng.DesaparecerGUI();
                }
            }
        }
    }
}