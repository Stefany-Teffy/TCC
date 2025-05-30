// recebe os valores e operacao desejada pelo usuario
// obtem o resultado da conta ao clicar em ok

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;



public class GetOp : MonoBehaviour, IHasChanged
{
	public Button ok;
	public static int op1;
	public int op2;
	public int tipo;
	public Dropdown tipoOp;
	Dropdown.OptionData mais, menos, selecione;
	public string valor1;
	public string valor2;

	// Variáveis estáticas para armazenar a opção, resultado e expressão da operação
	public static int opcao;
	public static int valorOp;
	public static string operacao;



	// Use this for initialization
	void Start ()
	{
		
		mais = new Dropdown.OptionData ();
		menos = new Dropdown.OptionData ();
		selecione = new Dropdown.OptionData ();
		mais.text = "+";
		menos.text = "-";
		selecione.text = "Selecione: ";
		HasChanged ();
	}

	#region IHasChanged implementation

	public void HasChanged ()
	{
		tipoOp.ClearOptions ();
		tipoOp.RefreshShownValue();
		tipoOp.options.Add (selecione);
		tipoOp.options.Add (mais);
		tipoOp.options.Add (menos);
	}

	#endregion
	 // Método para obter o valor 1
	public void GetOp1(string v1)
	{
		valor1 = v1;
		Debug.Log (v1);
		op1 = int.Parse (valor1);
	}

	 // Método para obter o valor 2
	public void GetOp2(string v2)
	{
		valor2 = v2;
		Debug.Log (v2);
		op2 = int.Parse (valor2);
	}

	// Método para obter a opção selecionada no Dropdown
	public void GetOption(int tp)
	{
		opcao = tp;
		Debug.Log (tp);

	}

	 // Método chamado ao clicar no botao ok para realizar a conta
	public void botaoOk(){
		if (opcao == 1) {
			Debug.Log (op1 + "+" + op2);
			valorOp = op1 + op2;
			Debug.Log (valorOp);
			operacao = op1 + "+" + op2;
		}
		else if(opcao == 2) {
			Debug.Log (op1 + "-" + op2);
			valorOp = op1 - op2;
			Debug.Log (valorOp);
			operacao = op1 + "-" + op2;
		}
	}

}