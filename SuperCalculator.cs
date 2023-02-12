using System.Windows.Forms;

public class SuperCalculator
{
    private static char[] _numbers = "0123456789".ToCharArray();

    public static string GetCorrectNumber(string number)
    {
        number = number.Replace(" ", "").Replace('\t'.ToString(), "");

        while (number.StartsWith("0") && !number.EndsWith("0"))
        {
            number = number.Substring(1);
        }

        return number;
    }

    public static bool IsNumberValid(string number)
    {
        if (number == "")
        {
            return false;
        }

        foreach (char character in number)
        {
            bool exists = false;

            foreach (char singleNumber in _numbers)
            {
                if (character.Equals(singleNumber))
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                return false;
            }
        }

        return true;
    }

    private static string AdaptLength(string numberToCorrect, int length)
    {
        if (length > numberToCorrect.Length)
        {
            for (int i = 1, loopTo = length - numberToCorrect.Length; i <= loopTo; i++)
            {
                numberToCorrect = "0" + numberToCorrect;
            }
        }

        return numberToCorrect;
    }

    public static string Min(string firstNumber, string secondNumber)
    {
        string result = "";

        if (firstNumber == secondNumber)
        {
            return firstNumber;
        }
        else if (firstNumber.Length == 1 & secondNumber.Length == 1)
        {
            if (byte.Parse(firstNumber) < byte.Parse(secondNumber))
            {
                result = firstNumber;
            }
            else
            {
                result = secondNumber;
            }
        }
        else
        {
            if (firstNumber.Length > secondNumber.Length)
            {
                secondNumber = AdaptLength(secondNumber, firstNumber.Length);
            }
            else if (secondNumber.Length > firstNumber.Length)
            {
                firstNumber = AdaptLength(firstNumber, secondNumber.Length);
            }

            for (int i = 0, loopTo = firstNumber.Length - 1; i <= loopTo; i++)
            {
                if (firstNumber[i].ToString() != secondNumber[i].ToString())
                {
                    if (byte.Parse(firstNumber[i].ToString()) < byte.Parse(secondNumber[i].ToString()))
                    {
                        return firstNumber;
                    }
                    else
                    {
                        return secondNumber;
                    }
                }
            }
        }

        return result;
    }

    public static string Max(string firstNumber, string secondNumber)
    {
        string result = "";

        if (firstNumber == secondNumber)
        {
            return firstNumber;
        }
        else if (firstNumber.Length == 1 & secondNumber.Length == 1)
        {
            if (byte.Parse(firstNumber) > byte.Parse(secondNumber))
            {
                result = firstNumber;
            }
            else
            {
                result = secondNumber;
            }
        }
        else
        {
            if (firstNumber.Length > secondNumber.Length)
            {
                secondNumber = AdaptLength(secondNumber, firstNumber.Length);
            }
            else if (secondNumber.Length > firstNumber.Length)
            {
                firstNumber = AdaptLength(firstNumber, secondNumber.Length);
            }
            for (int i = 0, loopTo = firstNumber.Length - 1; i <= loopTo; i++)
            {
                if (firstNumber[i].ToString() != secondNumber[i].ToString())
                {
                    if (byte.Parse(firstNumber[i].ToString()) > byte.Parse(secondNumber[i].ToString()))
                    {
                        return firstNumber;
                    }
                    else
                    {
                        return secondNumber;
                    }
                }
            }
        }
        return result;
    }

    public static string Addition(string firstOperand, string secondOperand)
    {
        string result = "";

        if (firstOperand.Length > secondOperand.Length)
        {
            do
            {
                secondOperand = "0" + secondOperand;
            }
            while (secondOperand.Length != firstOperand.Length);
        }
        else if (secondOperand.Length > firstOperand.Length)
        {
            do
            {
                firstOperand = "0" + firstOperand;
            }
            while (firstOperand.Length != secondOperand.Length);
        }

        int i = secondOperand.Length;
        bool carry = false;

        while (i > 0)
        {
            byte a = byte.Parse(firstOperand[i - 1].ToString());

            if (carry)
            {
                a += 1;
                carry = false;
            }

            byte r = (byte)(a + byte.Parse(secondOperand[i - 1].ToString()));

            if (r >= 10)
            {
                r -= 10;
                carry = true;
            }

            result = r.ToString() + result;
            i--;
        }

        if (carry)
        {
            result = "1" + result;
        }

        return result;
    }

    public static string Subtraction(string firstNumber, string secondNumber)
    {
        string result = "";

        if (firstNumber.Length > secondNumber.Length)
        {
            secondNumber = AdaptLength(secondNumber, firstNumber.Length);
        }
        else if (secondNumber.Length > firstNumber.Length)
        {
            firstNumber = AdaptLength(firstNumber, secondNumber.Length);
        }

        if (firstNumber == "0")
        {
            return secondNumber;
        }
        else if (secondNumber == "0")
        {
            return firstNumber;
        }
        else if (firstNumber == secondNumber)
        {
            return "0";
        }

        string temp = Max(firstNumber, secondNumber);

        if (firstNumber != temp)
        {
            temp = secondNumber;
            secondNumber = firstNumber;
            firstNumber = temp;
        }

        bool carry = false;

        for (int i = firstNumber.Length - 1; i >= 0; i -= 1)
        {
            sbyte firstOp = sbyte.Parse(firstNumber[i].ToString());

            if (carry)
            {
                firstOp = (sbyte)(firstOp - 1);
            }

            sbyte ris = (sbyte)(firstOp - sbyte.Parse(secondNumber[i].ToString()));

            if (ris < 0)
            {
                carry = true;
                ris = (sbyte)(10 - (-ris));
            }
            else
            {
                carry = false;
            }

            result = ris.ToString() + result;
        }

        return result;
    }

    public static string Multiplication(string num1, string num2)
    {
        int length1 = num1.Length;
        int length2 = num2.Length;
        int[] result = new int[length1 + length2];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = 0;
        }

        int i_n1 = 0;

        for (int i = length1 - 1; i >= 0; i--)
        {
            int carry = 0;
            int n1 = num1[i] - '0';
            int i_n2 = 0;

            for (int j = length2 - 1; j >= 0; j--)
            {
                int n2 = num2[j] - '0';
                int sum = n1 * n2 + result[i_n1 + i_n2] + carry;
                carry = sum / 10;
                result[i_n1 + i_n2] = sum % 10;
                i_n2++;
            }

            if (carry > 0)
            {
                result[i_n1 + i_n2] += carry;
            }

            i_n1++;
        }

        int z = result.Length - 1;

        while (z >= 0 && result[z] == 0)
        {
            z--;
        }

        if (z == -1)
        {
            return "0";
        }

        string s = "";

        while (z >= 0)
        {
            s += (result[z--]).ToString();
        }

        return s;
    }

    public static string Division(string num1, string num2)
    {
        string result = "";
        string dividend = num1;
        string divisor = num2;
        int currentDigit = 0;

        while (dividend.Length > divisor.Length || (dividend.Length == divisor.Length && dividend.CompareTo(divisor) >= 0))
        {
            while (dividend.Length > divisor.Length || (dividend.Length == divisor.Length && dividend.CompareTo(divisor) >= 0))
            {
                dividend = Subtract(dividend, divisor);
                currentDigit++;
            }

            result = result + currentDigit.ToString();
            divisor = "1" + new string('0', dividend.Length - divisor.Length + 1);
            currentDigit = 0;
        }

        return result;
    }

    private static string Subtract(string num1, string num2)
    {
        string result = "";
        int n1 = num1.Length, n2 = num2.Length;
        int carry = 0;

        for (int i = n1 - 1, j = n2 - 1; i >= 0; i--, j--)
        {
            int a = num1[i] - '0';
            int b = j >= 0 ? num2[j] - '0' : 0;
            int diff = a - b - carry;

            if (diff < 0)
            {
                diff += 10;
                carry = 1;
            }
            else
            {
                carry = 0;
            }

            result = diff.ToString() + result;
        }

        while (result.Length > 1 && result[0] == '0')
        {
            result = result.Substring(1);
        }

        return result;
    }

    public static string Modulo(string firstNumber, string secondNumber)
    {
        string result = "0";
        string count = "0";

        while (count != firstNumber)
        {
            count = Addition(count, "1");
            result = Addition(result, "1");

            if (result == secondNumber)
            {
                result = "0";
            }
        }

        return result;
    }

    public static string Power(string firstNumber, string secondNumber)
    {
        string result = firstNumber;
        string count = "0";
        secondNumber = Subtract(secondNumber, "1");

        while (count != secondNumber)
        {
            count = Addition(count, "1");
            result = Multiplication(result, firstNumber);
        }

        return result;
    }
}