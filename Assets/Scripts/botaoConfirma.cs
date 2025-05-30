using UnityEngine;
using UnityEngine.SceneManagement;

public class botaoConfirma : MonoBehaviour
{
    public AudioSource dicaRemoverSource;
    public AudioSource dicaAdicionarSource;
    public int valorAtual;
    public int valorComparacao;
    public int valortotal;

    public void VerificarResposta()
    {
        Debug.Log("Botão de confirmar vitória clicado!");
        valortotal = CalcularValorTotal();
        
        if (changeScenes.nomeAnt == "op2")
        {
            int respostaCorreta = Inventory.resposta;
            if (valortotal == respostaCorreta)
            {
                ativar.ativarModal(); 
            }
            else
            {
                Debug.Log("Resposta incorreta");
                if (valortotal > respostaCorreta)
                {
                    dicaRemoverSource.Play();
                }
                else
                {
                    dicaAdicionarSource.Play();
                }
            }
        }
        else if (changeScenes.nomeAnt == "selecao")
        {
            int numMaximo = numMax.num;
            if (valortotal == numMaximo)
            {
                ativar.ativarModal(); 
            }
            else
            {
                Debug.Log("Valor incorreto");
                if (valortotal > numMaximo)
                {
                    dicaRemoverSource.Play();
                }
                else
                {
                    dicaAdicionarSource.Play();
                }
            } 
        }
    }


    private int CalcularValorTotal()
    {
        int valortotal = 0;
        foreach (Transform slotTransform in Inventory.slots)
        {
            for (int i = 0; i < slotTransform.childCount; i++)
                valortotal += int.Parse(slotTransform.GetChild(i).gameObject.tag);
        }
        return valortotal;
    }
}
