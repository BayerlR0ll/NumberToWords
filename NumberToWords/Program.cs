using NumberToWords;

var numberInput = string.Empty;
var numberDisplay = string.Empty;
var quitInput = string.Empty;
var number = 0;
var quit = false;
while (!quit)
{
    var inputOK = false;
    while (!inputOK)
    {
        try
        {
            Console.Write("Please enter a 9 digit number:\t");
            numberInput = Console.ReadLine();
            if (!int.TryParse(numberInput, out number) || Math.Abs(number) > 999999999) Console.WriteLine("Invalid Entry.");
            else inputOK = true;
        }
        catch (Exception) { }
    }
    var isNegative = false;
    if (number < 0) isNegative = true;
    var numberArray = CreateArray(Math.Abs(number));
    numberDisplay = WriteNumberAsWord(numberArray, isNegative);
    Console.WriteLine($"Your Number:\t{number}\nYour Number As A Word:\t{numberDisplay}");

    inputOK = false;
    while (!inputOK)
    {
        Console.Write("Quit? Y/N:\t");
        quitInput = Console.ReadLine();
        
        if (quitInput.ToUpper() == "N")
        {
            numberInput = string.Empty;
            numberDisplay = string.Empty;
            numberArray = Array.Empty<int>();
            inputOK = true;
        }
        else if (quitInput.ToUpper() == "Y")
        {
            quit = true;
            inputOK = true;
        }
        else
        {
            Console.WriteLine("Invalid Entry.");
        }
    }
}

string WriteNumberAsWord(int[] array, bool isNegative)
{
    var digits = array.Length;
    var display = string.Empty;
    if (isNegative) display += NumberWords.negative;
    switch (digits)
    {
        default:
            display += WriteZeroToNine(array[0]);
            break;
        case 2:
            display += TensLogic(array[0], array[1]);
            break;
        case 3:
            display += WriteZeroToNine(array[0]) + WriteHundred() + TensLogic(array[1], array[2]);
            break;
        case 4:
            display += WriteZeroToNine(array[0]) + WriteThousand() + HundredsLogic(array[1]) + TensLogic(array[2], array[3]);
            break;
        case 5:
            display += TensLogic(array[0], array[1]) + WriteThousand() + HundredsLogic(array[2]) + TensLogic(array[3], array[4]);
            break;
        case 6:
            display += WriteZeroToNine(array[0]) + WriteHundred() + TensLogic(array[1], array[2]) + WriteThousand() + HundredsLogic(array[3]) + TensLogic(array[4], array[5]);
            break;
        case 7:
            display += WriteZeroToNine(array[0]) + WriteMillion() + ThousandsLogic(array[1], array[2], array[3]) + HundredsLogic(array[4]) + TensLogic(array[5], array[6]);
            break;
        case 8:
            display += TensLogic(array[0], array[1]) + WriteMillion() + ThousandsLogic(array[2], array[3], array[4]) + HundredsLogic(array[5]) + TensLogic(array[6], array[7]);
            break;
        case 9:
            display += WriteZeroToNine(array[0]) + WriteHundred() + TensLogic(array[1], array[2]) + WriteMillion() + ThousandsLogic(array[3], array[4], array[5]) + HundredsLogic(array[6]) + TensLogic(array[7], array[8]);
            break;
    }
    return display;
}

int[] CreateArray(int absnumber)
{
    return absnumber.ToString().Select(x=>Convert.ToInt32(x)-48).ToArray();
}

#region "Display Logic"
string TensLogic(int tens, int ones)
{
    if (tens > 1) return $"{WriteTwentyToNinty(tens)} {WriteZeroToNine(ones)}";
    else if (tens + ones > 0) return WriteTenToNinteen(ones);
    else return string.Empty;
}

string HundredsLogic(int hundreds)
{
    if (hundreds > 0) return $"{WriteZeroToNine(hundreds)} {WriteHundred}";
    else return string.Empty;
}

string ThousandsLogic(int hundreds, int tens, int ones)
{
    if (hundreds > 0) return $"{HundredsLogic(hundreds)} {TensLogic(tens, ones)} {WriteThousand()}";
    else if (tens > 0) return $"{TensLogic(tens, ones)} {WriteThousand()}";
    else if (ones > 0) return $"{WriteZeroToNine(ones)} {WriteThousand()}";
    else return string.Empty; 
}
#endregion

#region "Write NumberWords"
string WriteZeroToNine(int digitValue)
{
    switch (digitValue)
    {
        default: return NumberWords.zero;
        case 1: return NumberWords.one;
        case 2: return NumberWords.two;
        case 3: return NumberWords.three;
        case 4: return NumberWords.four;
        case 5: return NumberWords.five;
        case 6: return NumberWords.six;
        case 7: return NumberWords.seven;
        case 8: return NumberWords.eight;
        case 9: return NumberWords.nine;
    }
}
string WriteTenToNinteen(int digitValue)
{
    switch (digitValue)
    {
        default: return NumberWords.ten;
        case 1: return NumberWords.eleven;
        case 2: return NumberWords.twelve;
        case 3: return NumberWords.thirteen;
        case 4: return NumberWords.fourteen;
        case 5: return NumberWords.fifteen;
        case 6: return NumberWords.sixteen;
        case 7: return NumberWords.seventeen;
        case 8: return NumberWords.eighteen;
        case 9: return NumberWords.nineteen;
    }
}
string WriteTwentyToNinty(int digitValue)
{
    switch (digitValue)
    {
        default: return string.Empty;
        case 2: return NumberWords.twenty;
        case 3: return NumberWords.thirty;
        case 4: return NumberWords.forty;
        case 5: return NumberWords.fifty;
        case 6: return NumberWords.sixty;
        case 7: return NumberWords.seventy;
        case 8: return NumberWords.eighty;
        case 9: return NumberWords.ninty;
    }
}
string WriteHundred()
{
    return NumberWords.hundred;
}
string WriteThousand()
{
    return NumberWords.thousand;
}
string WriteMillion()
{
    return NumberWords.million;
}
#endregion