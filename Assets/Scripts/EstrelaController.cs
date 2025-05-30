using UnityEngine;

public class EstrelaController : MonoBehaviour
{
    public int numeroEstrela;

    private void Start()
    {
        AtualizarEstadoEstrela();
    }

    private void OnEnable()
    {
        AtualizarEstadoEstrela();
    }

    public void AtualizarEstadoEstrela()
    {
        int numeroNivel = int.Parse(transform.parent.parent.name.Substring(5));

        GerenciadorEstrelas gerenciadorEstrelas = FindObjectOfType<GerenciadorEstrelas>();

        if (gerenciadorEstrelas != null)
        {
            int estrelasDesativadas = gerenciadorEstrelas.ObterEstrelasDesativadasDoNivel(numeroNivel);

            if (estrelasDesativadas >= numeroEstrela)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("GerenciadorEstrelas não encontrado!");
        }
    }
}
