#include <stdio.h>
#include <stdbool.h>
#include <locale.h>
#include <malloc.h>

#define MAX_LINE_SIZE 100

typedef struct Pair{
    int sumOfDigits;
    int value;
} Pair;

int findMaxSumOfElementsDigits(Pair array[], int arraySize) {
    int maxSumOfDigits = 0;
    for (int i = 0; i < arraySize; i++) {
        if (array[i].sumOfDigits > maxSumOfDigits) {
            maxSumOfDigits = array[i].sumOfDigits;
        }
    }
    return maxSumOfDigits;
}

bool test() {
    bool testResult = true;
    Pair array[] = {{6, 123}, {7, 124}, {6, 114}};
    if (findMaxSumOfElementsDigits(array, 3) != 7) {
        testResult = false;
    }

    return testResult;
}

int main() {
    setlocale(LC_ALL, "");

    if (!test()) {
        printf("Тесты провалены\n");

        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    printf("Введите количество элементов массива,а затем сами элементы(по длине каждый из них не должен превышать %d символов)\n", MAX_LINE_SIZE);
    int numOfArrayElements = 0;
    scanf("%d", &numOfArrayElements);
    if (numOfArrayElements <= 0) {
        printf("Размер массива должен быть положительным");
        return -1;
    }
    Pair *arrayOfSumsOfDigits = calloc(numOfArrayElements, sizeof(Pair));
    if (arrayOfSumsOfDigits == NULL) {
        printf("Проблемы с аллокацией памяти\n");
        return -1;
    }
    for (int i = 0; i < numOfArrayElements; i++) {
        char number[MAX_LINE_SIZE] = {0};
        scanf("%s", number);
        int value = 0;
        int sumOfDigits = 0;
        bool isNegative = false;
        for (int j = 0; j < MAX_LINE_SIZE; j++) {
            if (number[j] == '\0') {
                break;
            }
            if (number[j] == '-') {
                isNegative = true;
                continue;
            }
            int currentDigit = (int)number[j] - 48;

            value *= 10;
            value += currentDigit;
            sumOfDigits += currentDigit;
        }
        if (isNegative) {
            value *= -1;
        }
        Pair pair = {sumOfDigits, value};
        arrayOfSumsOfDigits[i] = pair;
    }

    int maxSumOfDigits = findMaxSumOfElementsDigits(arrayOfSumsOfDigits, numOfArrayElements);

    printf("Вот элементы массива с максимальной суммой цифр:\n");
    for (int i = 0; i < numOfArrayElements; i++) {
        if (arrayOfSumsOfDigits[i].sumOfDigits == maxSumOfDigits) {
            printf("%d ", arrayOfSumsOfDigits[i].value);
        }
    }

    free(arrayOfSumsOfDigits);
    return 0;
}
