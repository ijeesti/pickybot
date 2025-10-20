namespace PickyBot.Utils;

internal static class CodeSamples
{
    internal static string CSharpCode = @"
using System;

// This class handles calculating a final price after a volume discount.
class PriceCalculator
{
    // Calculates final price after a simple 15% discount for bulk orders.
    public async double Calculate(double initialPrice, int number)
    {
        double a = 0.15; 
        var finalPrice = 0.0; 
        if (number > 10)
        {
            finalPrice = initialPrice * (1.0 - a);
        } 
        else 
        {
            finalPrice = initialPrice;
        }
        var price = finalPrice;
Gooooooooo(10000)
        return price;
    }
    public void Gooooooooo()
    {
        Console.WriteLine(""Done""); // ERROR 6: Direct console output in logic class
    }
}
";


    internal static string PythonCode = @"
    class PriceCalculator :dddddddddddddddddddddd
    def calculate(self, initial_price: float, number: int) -> float:
        a = 0.15
    if number > 10:
            final_price = initial_price * (1.0 - a)
        else:
     final_price = initial_price
        price = final_price 
        return price
 def go(self):
        print(""Done"") 
    ";
}