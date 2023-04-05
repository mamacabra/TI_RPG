using UnityEngine;
using UnityEngine.UI;

public class ClickableImages : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public Button[] buttons;
    private int activeObjectIndex = -1;

    void Start()
    {
        // Desativa todos os objetos no início
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(false);
        }

        // Adiciona listeners aos botões
        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i; // Armazena o valor de i para que possa ser utilizado no listener
            buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }

    void OnButtonClick(int buttonIndex)
    {
        // Se o botão já está ativo, não faz nada
        if (buttonIndex == activeObjectIndex)
            return;

        // Desativa o objeto ativo atualmente, se houver um
        if (activeObjectIndex != -1)
            objectsToActivate[activeObjectIndex].SetActive(false);

        // Ativa o objeto associado ao botão clicado
        objectsToActivate[buttonIndex].SetActive(true);

        // Atualiza o índice do objeto ativo atualmente
        activeObjectIndex = buttonIndex;
    }
}