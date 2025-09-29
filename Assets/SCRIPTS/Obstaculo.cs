using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    public float reduccionVel;
    public float tiempEmpDesapa = 1;
    public float tiempDesapareciendo = 1;
    public string playerTag = "Player";

    private bool _chocado;
    private bool _desapareciendo;
    private float _tempo1;
    private float _tempo2;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (_chocado)
        {
            _tempo1 += T.GetDT();
            if (_tempo1 > tiempEmpDesapa)
            {
                _chocado = false;
                _desapareciendo = true;
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Collider>().enabled = false;
            }
        }

        if (_desapareciendo)
        {
            //animacion de desaparecer

            _tempo2 += T.GetDT();
            if (_tempo2 > tiempDesapareciendo) gameObject.SetActiveRecursively(false);
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.tag == playerTag) _chocado = true;
    }

    //------------------------------------------------//

    protected virtual void Desaparecer()
    {
    }

    protected virtual void Colision()
    {
    }
}