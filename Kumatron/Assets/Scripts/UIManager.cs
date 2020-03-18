using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private Sprite[] _colorPanel;
    [SerializeField]
    private Image _panelColor;
    [SerializeField]
    private Image _rayOn;
    [SerializeField]
    private Sprite[] _rayButton;
    [SerializeField]
    private Image _animalButton;
    [SerializeField]
    private Sprite[] _animalsFace;
    [SerializeField]
    private Text[] _animalsCount;
    public Image pauseMenu;
    public Text animalsText;
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _animalsCount[0].text = "" + _gameManager.chickenNumber;
        _animalsCount[1].text = "" + _gameManager.bullNumber;
        _animalsCount[2].text = "" + _gameManager.cowNumber;
    }
    void FixedUpdate()
    {
        LookHowMuchAnimals();
    }
    public void UpdateColor(int playerHP)
    {
        _panelColor.sprite = _colorPanel[playerHP];
    }
    public void UpdateRayCheck(bool canAbduct)
    {
        if (canAbduct == true)
        {
            _rayOn.sprite = _rayButton[1];
        }
        else
        {
            _rayOn.sprite = _rayButton[0];
        }
    }
    public void CheckAnimal(string animal)
    {
        if (animal == "Chicken")
        {
            _animalButton.sprite = _animalsFace[1];
        }
        else if (animal == "Bull")
        {
            _animalButton.sprite = _animalsFace[2];
        }
        else if (animal == "Cow")
        {
            _animalButton.sprite = _animalsFace[3];
        }
        else if (animal == null)
        {
            _animalButton.sprite = _animalsFace[0];
        }
    }
    public void LookHowMuchAnimals()
    {
        _gameManager.AnimalsNumbers();
        _animalsCount[0].text = "" + _gameManager.chickenNumber;
        _animalsCount[1].text = "" + _gameManager.bullNumber;
        _animalsCount[2].text = "" + _gameManager.cowNumber;
    }
    public void TextAnimalQuantity(int value)
    {
        animalsText.text = value.ToString();
    }
}
