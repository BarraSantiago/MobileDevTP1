using System.Collections.Generic;
using UnityEngine;

namespace EscenaDescarga
{
    public class PilaPalletMng : MonoBehaviour
    {
        public List<GameObject> bolasasEnCamion = new();
        public int cantAct;

        // Use this for initialization
        private void Start()
        {
            for (int i = 0; i < bolasasEnCamion.Count; i++) bolasasEnCamion[i].GetComponent<Renderer>().enabled = false;
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void Sacar()
        {
            bolasasEnCamion[cantAct - 1].GetComponent<Renderer>().enabled = false;
            cantAct--;
        }

        public void Agregar()
        {
            cantAct++;
            bolasasEnCamion[cantAct - 1].GetComponent<Renderer>().enabled = true;
        }
    }
}