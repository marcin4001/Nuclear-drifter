using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BetType
{
    Color, Parity, Numbers
}

public enum ColorChoice
{
    Black, Red
}

public enum ParityChoice
{
    Even, Odd
}

public enum NumbersChoice
{
    Row1, Row2, Row3
}

public class Croupier : Job
{
    public Slot currency;
    public Inventory inv;
    public BetType betType;
    public ColorChoice colorChoice;
    public ParityChoice parityChoice;
    public NumbersChoice numberChoice;
    public string[] colors;
    private string red = "Red";
    private string black = "Black";
    private void Start()
    {
        inv = FindObjectOfType<Inventory>();
        colors = new string[37];
        colors[0] = "Green";
        for(int i = 1; i <= 10; i++)
        {
            colors[i] = i % 2 == 1 ? red : black;
            colors[i + 18] = i % 2 == 1 ? red : black;
        }
        for(int i = 11; i <= 18; i++)
        {
            colors[i] = i % 2 == 1 ? black : red;
            colors[i + 18] = i % 2 == 1 ? black : red;
        }
    }

    public override string Work(int opt)
    {
        if(opt == 0)
        {
            betType = BetType.Color;
            colorChoice = ColorChoice.Black;
            return "";
        }
        if(opt == 1)
        {
            betType = BetType.Color;
            colorChoice = ColorChoice.Red;
            return "";
        }
        if(opt == 2)
        {
            betType = BetType.Parity;
            parityChoice = ParityChoice.Even;
            return "";
        }
        if(opt == 3)
        {
            betType = BetType.Parity;
            parityChoice = ParityChoice.Odd;
            return "";
        }
        if(opt == 4)
        {
            betType = BetType.Numbers;
            numberChoice = NumbersChoice.Row1;
            return "";
        }
        if(opt == 5)
        {
            betType = BetType.Numbers;
            numberChoice = NumbersChoice.Row2;
            return "";
        }
        if(opt == 6)
        {
            betType = BetType.Numbers;
            numberChoice = NumbersChoice.Row3;
            return "";
        }
        if(opt == 7)
        {
            currency.amountItem = 5;
        }
        if(opt == 8)
        {
            currency.amountItem = 10;
        }
        if(opt == 9)
        {
            currency.amountItem = 15;
        }
        if(opt == 10)
        {
            currency.amountItem = 20;
        }
        return SpinRoulette();
    }

    public string SpinRoulette()
    {
        Slot money = inv.FindItem(currency.itemElement.idItem);
        if (money == null)
            return "You don't have enough money. Please try a different amount.";
        if(money.amountItem < currency.amountItem)
            return "You don't have enough money. Please try a different amount.";
        int random = Random.Range(0, 36);
        if(betType == BetType.Color) 
        {
            if(random == 0)
            {
                Losing();
                return $"Unfortunately, the spun number is {random} (Green). You lost ${currency.amountItem}. Do you want to play again?";
            }
            if(colorChoice == ColorChoice.Black)
            {
                if (colors[random] == black)
                {
                    currency.amountItem *= 2;
                    Winning();
                    return $"The spun number is {random} ({black}). You won ${currency.amountItem}! Do you want to play again?";
                }
                else
                {
                    Losing();
                    return $"Unfortunately, the spun number is {random} ({red}). You lost ${currency.amountItem}. Do you want to play again?";
                }
            }
            if (colorChoice == ColorChoice.Red)
            {
                if (colors[random] == red)
                {
                    currency.amountItem *= 2;
                    Winning();
                    return $"The spun number is {random} ({red}). You won ${currency.amountItem}! Do you want to play again?";
                }
                else
                {
                    Losing();
                    return $"Unfortunately, the spun number is {random} ({black}). You lost ${currency.amountItem}. Do you want to play again?";
                }
            }
        }
        if(betType == BetType.Parity)
        {
            if(random == 0)
            {
                Losing();
                return $"Unfortunately, the spun number is {random}. You lost ${currency.amountItem}. Do you want to play again?";
            }
            if(parityChoice == ParityChoice.Even)
            {
                if(random % 2 == 0)
                {
                    currency.amountItem *= 2;
                    Winning();
                    return $"The spun number is {random} (Even). You won ${currency.amountItem}! Do you want to play again?";
                }
                else
                {
                    Losing();
                    return $"Unfortunately, the spun number is {random} (Odd). You lost ${currency.amountItem}. Do you want to play again?";
                }
            }
            if(parityChoice == ParityChoice.Odd)
            {
                if (random % 2 == 1)
                {
                    currency.amountItem *= 2;
                    Winning();
                    return $"The spun number is {random} (Odd). You won ${currency.amountItem}! Do you want to play again?";
                }
                else
                {
                    Losing();
                    return $"Unfortunately, the spun number is {random} (Even). You lost ${currency.amountItem}. Do you want to play again?";
                }
            }
        }
        if(betType == BetType.Numbers)
        {
            bool hitRow = false;
            switch(numberChoice)
            {
                case NumbersChoice.Row1:
                    hitRow = random >= 1 && random <= 12;
                    break;
                case NumbersChoice.Row2:
                    hitRow = random >= 13 && random <= 24;
                    break;
                case NumbersChoice.Row3:
                    hitRow = random >= 25 && random <= 36;
                    break;
            }
            if(hitRow)
            {
                currency.amountItem *= 2;
                Winning();
                return $"The spun number is {random}. You won ${currency.amountItem}! Do you want to play again?";
            }
            else
            {
                Losing();
                return $"Unfortunately, the spun number is {random}. You lost ${currency.amountItem}. Do you want to play again?";
            }
        }
        return "";
    }

    private void Winning()
    {
        inv.Add(currency);
    }

    private void Losing()
    {
        inv.RemoveFew(currency);
    }
}
