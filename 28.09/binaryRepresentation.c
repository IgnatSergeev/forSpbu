#define RegistrySize 30
#include <stdio.h>
#include <malloc.h>
#include <math.h>

void representTheNumberInBinary(int number, int numberBinaryRepresentation[]) {
    if (number < 0) {
        number = (int)pow(2,RegistrySize) + number;
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

int main() {
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