using UnityEngine;

public class CheakPoint : MonoBehaviour
{
    public string playerTag = "Player";
    public float tiempPermanencia = 0.7f; //tiempo que no deja respaunear a un pj desp que el otro lo hizo.
    private bool _habilitadoResp = true;
    private float _tempo;

    // Use this for initialization
    private void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_habilitadoResp)
        {
            _tempo += T.GetDT();
            if (_tempo >= tiempPermanencia)
            {
                _tempo = 0;
                _habilitadoResp = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag) other.GetComponent<Respawn>().AgregarCp(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == playerTag) _habilitadoResp = true;
    }

    //---------------------------------------------------//

    public bool Habilitado()
    {
        if (_habilitadoResp)
        {
            _habilitadoResp = false;
            _tempo = 0;
            return true;
        }

        return _habilitadoResp;
    }
}