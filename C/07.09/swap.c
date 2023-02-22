#include <stdio.h>
#include <alg.h>

int main()
{
    int firstVariable = 0;
    int secondVariable = 0;

    printf("%s", "Введите 2 целых числа, не превосходящих 2^31, иначе undefined behaviour\n");
    scanf("%d", &firstVariable);
    scanf("%d", &secondVariable);

    printf("%s\n%d, %d\n","Переменные до изменения:", firstVariable, secondVariable);

    firstVariable = firstVariable + secondVariable;
    secondVariable = firstVariable - secondVariable;
    firstVariable = firstVariable - secondVariable;

    printf("%s\n%d, %d","Переменные после изменения:", firstVariable, secondVariable);

    return 0;
}
