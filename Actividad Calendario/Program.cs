// See https://aka.ms/new-console-template for more information

int NumberDays(int month, int year)
{
    bool bisiesto = false;
    if (year%400==0 || (year%4==0 && year%100!=0))
        bisiesto = true;
    if (month==4 || month==6 || month==9 || month==11)
        {return 30;}
    else if (month==2 && bisiesto)
        {return 29;}
    else if (month==2 && !bisiesto)
        {return 28;}
    else
        {return 31;}
}



int contador = 0;
int dia = 1;
for (int ano = 1900; ano < 3000; ano++)
{
    for (int mes = 1; mes < 13; mes++)
    {
        if (dia == 7)
            contador += 1;
        Console.WriteLine($"ano tiene valor {ano}, y mes {mes} y cae el día {dia}");
        int funcion = NumberDays(mes, ano);
        dia = dia + funcion - 28;
        if (dia > 7)
            dia = dia - 7;
    }
}
Console.WriteLine($"La cantidad de domingos es {contador}");