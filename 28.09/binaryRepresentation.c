#define RegistrySize 30
#include <stdio.h>
#include <malloc.h>
#include <math.h>
#include <stdbool.h>

void representTheNumberInBinary(int number, int numberBinaryRepresentation[]) {
    if (number < 0) {
        number = (int)pow(2, RegistrySize) + number;
    }

    for (int i = 0; i < RegistrySize; i++) {
        int bit = number & 1;
        number = number >> 1;
        numberBinaryRepresentation[RegistrySize - 1 - i] = bit;
    }
}

void sumTwoBinaries(const int firstNumberBinaryRepresentation[], const int secondNumberBinaryRepresentation[], int sumOfTwoBinaries[]) {
    int extraBit = 0;
    for (int i = RegistrySize - 1; i >= 0; i--) {
        sumOfTwoBinaries[i] = (firstNumberBinaryRepresentation[i] + secondNumberBinaryRepresentation[i] + extraBit) % 2;
        extraBit = ((firstNumberBinaryRepresentation[i] + secondNumberBinaryRepresentation[i] + extraBit) >= 2) ? 1 : 0;
    }
}

int binaryRepresentationToDecimal(const int numberBinaryRepresentation[]) {
    int number = 0;
    int inverter = 0;
    int multiplier = 1;
    int increment = 0;
    if (numberBinaryRepresentation[0] == 1) {
        inverter = 1;
        multiplier = -1;
        increment = 1;
    }

    for (int i = 1; i < RegistrySize; i++) {
        number = number << 1;
        number += (numberBinaryRepresentation[i] + inverter) % 2;
    }
    number += increment;
    number *= multiplier;

    return number;
}

bool test() {
    bool typicalTest = true;

    int *test1NumberBinaryRepresentation = (int*)malloc(RegistrySize * sizeof(int));
    if (test1NumberBinaryRepresentation == NULL) {
        printf("Проблемы с аллокацией");

        return false;
    }
    int *test2NumberBinaryRepresentation = (int*)malloc(RegistrySize * sizeof(int));
    if (test2NumberBinaryRepresentation == NULL) {
        printf("Проблемы с аллокацией");

        free(test1NumberBinaryRepresentation);
        return false;
    }
    representTheNumberInBinary(10, test1NumberBinaryRepresentation);
    representTheNumberInBinary(-2, test2NumberBinaryRepresentation);
    int correctTest1[] = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0};
    int correctTest2[] = {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0};
    for (int i = 0; i < RegistrySize; i++) {
        if (correctTest1[i] != test1NumberBinaryRepresentation[i] || correctTest2[i] != test2NumberBinaryRepresentation[i]) {
            typicalTest = false;
        }
    }

    int *test3SumBinaryRepresentation = (int*)malloc(RegistrySize * sizeof(int));
    if (test3SumBinaryRepresentation == NULL) {
        printf("Проблемы с аллокацией");

        free(test1NumberBinaryRepresentation);
        free(test2NumberBinaryRepresentation);
        return false;
    }
    int correctTest3[] = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0};
    sumTwoBinaries(test1NumberBinaryRepresentation, test2NumberBinaryRepresentation, test3SumBinaryRepresentation);
    for (int i = 0; i < RegistrySize; i++) {
        if (correctTest3[i] != test3SumBinaryRepresentation[i]) {
            typicalTest = false;
        }
    }
    if (binaryRepresentationToDecimal(test3SumBinaryRepresentation) != 8) {
        typicalTest = false;
    }

    free(test3SumBinaryRepresentation);
    free(test2NumberBinaryRepresentation);
    free(test1NumberBinaryRepresentation);
    return typicalTest;
}

int main() {
    if (!test()) {
        printf("Тесты провалены");

        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    int firstNumber = 0;
    int secondNumber = 0;
    printf("Введите числа по модулю меньшие 2^%d\n", RegistrySize - 1);
    scanf("%d", &firstNumber);
    scanf("%d", &secondNumber);
    const int maxNumber = (int)pow(2,RegistrySize - 1) + 1;
    if (firstNumber >= maxNumber || secondNumber >= maxNumber) {
        printf("меньшие 2^30");

        return -1;
    }

    int *firstNumberBinaryRepresentation = (int*)malloc(RegistrySize * sizeof(int));
    if (firstNumberBinaryRepresentation == NULL) {
        printf("Проблемы с аллокацией");

        return -1;
    }
    int *secondNumberBinaryRepresentation = (int*)malloc(RegistrySize * sizeof(int));
    if (secondNumberBinaryRepresentation == NULL) {
        printf("Проблемы с аллокацией");

        return -1;
    }

    printf("Первое число в двоичном представлении:");
    representTheNumberInBinary(firstNumber, firstNumberBinaryRepresentation);
    for (int i = 0; i < RegistrySize; i++) {
        printf("%d", firstNumberBinaryRepresentation[i]);
    }
    printf("\n");
    printf("Второе число в двоичном представлении:");
    representTheNumberInBinary(secondNumber, secondNumberBinaryRepresentation);
    for (int i = 0; i < RegistrySize; i++) {
        printf("%d", secondNumberBinaryRepresentation[i]);
    }
    printf("\n");

    int *sumOfTwoBinaries = (int*)malloc(RegistrySize * sizeof(int));
    if (sumOfTwoBinaries == NULL) {
        printf("Проблемы с аллокацией");

        return -1;
    }
    sumTwoBinaries(firstNumberBinaryRepresentation, secondNumberBinaryRepresentation, sumOfTwoBinaries);

    int sumInDecimal = binaryRepresentationToDecimal(sumOfTwoBinaries);
    printf("Результат суммы:%d\n", sumInDecimal);

    free(firstNumberBinaryRepresentation);
    free(secondNumberBinaryRepresentation);
    free(sumOfTwoBinaries);
    return 0;
}