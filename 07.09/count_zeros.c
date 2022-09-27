#include <stdio.h>

int main()
{
    int arraySize = 0;
    int array[1000] = {};
    int counterOfZeros = 0;

    printf("%s", "Введите количество элементов в массиве(это число должно быть меньше или равно 1000)\n");
    scanf("%d", &arraySize);
    if (arraySize > 1000)
    {
        printf("%s", "Введённое число больше 1000");
        return 0;
    }

    printf("%s", "Теперь введите элементы массива\n");
    for (int i = 0; i < arraySize; i++)
    {
        int arrayElement = 0;
        scanf("%d", &arrayElement);
        array[i] = arrayElement;

        if (arrayElement == 0)
        {
            counterOfZeros += 1;
        }
    }

    printf("Количество нулей в массиве = %d", counterOfZeros);

    return 0;
}
