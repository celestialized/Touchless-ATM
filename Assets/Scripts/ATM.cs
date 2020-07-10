using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

enum Step: int {
    INSERT_CARD,
    INSERT_PIN,
    SELECT_ACTION,
    SELECT_AMOUNT,
    CHECK_BALANCE,
    CONFIRM_WITHDRAWAL,
    TAKE_MONEY,
}

public class ATM : MonoBehaviour
{

    public TextMesh explanation;
    public GameObject insertCardPanel;
    public GameObject pinPanel;
    public GameObject amountPanel;
    public GameObject confirmPanel;
    public GameObject goodbyePanel;

    int MAX_WITHDRAWAL = 2000;
    int MIN_WITHDRAWAL = 50;

    Step currentStep = Step.INSERT_CARD;
    string pin;
    int amount;


    // Start is called before the first frame update
    void Start()
    {
        this.pin = "";
        this.amount = this.MIN_WITHDRAWAL;

        this.insertCardPanel.SetActive(true);
        this.pinPanel.SetActive(false);
        this.amountPanel.SetActive(false);
        this.confirmPanel.SetActive(false);
        this.goodbyePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentStep)
        {
            case Step.INSERT_CARD:
                explanation.text = "Welcome!\nPlease insert your card below";
                break;
            case Step.INSERT_PIN:
                explanation.text = "Enter PIN:\n" + "****".Substring(4 - this.pin.Length);
                break;
            case Step.SELECT_AMOUNT:
                explanation.text = "Amount to withdraw:\n$" + this.amount;
                break;
            case Step.CONFIRM_WITHDRAWAL:
                explanation.text = "Do you want to\nwithdraw $" + this.amount + "?";
                break;
            case Step.TAKE_MONEY:
                explanation.text = "Please take your money below\nHave a nice day!";
                break;
        }
        
    }

    private async Task switchPanel(GameObject oldPanel, GameObject newPanel, int delay = 1000)
    {
        oldPanel.SetActive(false);
        await Task.Delay(delay);
        newPanel.SetActive(true);

    }

    public async void insertCard()
    {
        this.currentStep = Step.INSERT_PIN;
        await switchPanel(this.insertCardPanel, this.pinPanel);

    }

    public void addToPin(string value = "*") {
        if(this.pin.Length < 4)
        {
            this.pin = this.pin + value;
        }
    }

    public void deleteFromPin()
    {
        if(this.pin.Length > 0)
        {
            this.pin = this.pin.Remove(this.pin.Length - 1, 1);
        }
    }

    public async void confirmPin()
    {
        if(this.pin.Length == 4)
        {
            this.currentStep = Step.SELECT_AMOUNT;
            await switchPanel(this.pinPanel, this.amountPanel);
        }
    }


    public void increaseAmount(int value = 50)
    {
        if (this.amount + value <= this.MAX_WITHDRAWAL)
        {
            this.amount += value;
        }
    }

    public void decreaseAmount(int value = 50)
    {
        if (this.amount - value > this.MIN_WITHDRAWAL)
        {
            this.amount -= value;
        }
    }

    public async void confirmAmount()
    {
        this.currentStep = Step.CONFIRM_WITHDRAWAL;
        await switchPanel(this.amountPanel, this.confirmPanel);
    }

    public async void proceedOrCancel(bool shouldProceed)
    {
        if(shouldProceed)
        {
            this.currentStep = Step.TAKE_MONEY;
            await switchPanel(this.confirmPanel, this.goodbyePanel);
        } else
        {
            this.currentStep = Step.SELECT_AMOUNT;
            await switchPanel(this.confirmPanel, this.amountPanel);
        }
    }

    public async void restart()
    {
        this.currentStep = Step.INSERT_CARD;
        this.pin = "";
        this.amount = this.MIN_WITHDRAWAL;
        await switchPanel(this.goodbyePanel, this.insertCardPanel);
    }

}
