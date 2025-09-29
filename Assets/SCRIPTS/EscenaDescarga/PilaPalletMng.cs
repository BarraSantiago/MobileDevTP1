using System.Collections.Generic;
using UnityEngine;

namespace EscenaDescarga
{
    public class PilaPalletMng : MonoBehaviour
    {
        public List<GameObject> BolasasEnCamion = new();
        public int CantAct;

        // Use this for initialization
        private void Start()
        {
            for (int i = 0; i < BolasasEnCamion.Count; i++) BolasasEnCamion[i].GetComponent<Renderer>().enabled = false;
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void Sacar()
        {
            BolasasEnCamion[CantAct - 1].GetComponent<Renderer>().enabled = false;
            CantAct--;
        }

        public void Agregar()
        {
            CantAct++;
            BolasasEnCamion[CantAct - 1].GetComponent<Renderer>().enabled = true;
        }
    }
}