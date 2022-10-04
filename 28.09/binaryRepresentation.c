#include <stdio.h>
#include <stdbool.h>
#include <malloc.h>
#include <time.h>
#include <math.h>

void representTheNumberInBinary(int number, int numberBinaryRepresentation[]) {
    if (number < 0) {
        number = (int)pow(2,31) + 1 + number;
    }

    for (int i = 0; i < 31; i++) {
        int bit = number & 1;
        number = number >> 1;
        numberBinaryRepresentation[30 - i] = bit;
    }
}


int main() {
    int firstNumber = 0;
    int secondNumber = 0;
    printf("%s", "Введите числа по модулю меньшие 2^30\n");
    scanf("%d", &firstNumber);
    scanf("%d", &secondNumber);
    //проверка что действительно меньше 30

    int *firstNumberBinaryRepresentation = (int*)malloc(31 * sizeof(int));
    if (firstNumberBinaryRepresentation == NULL) {
        printf("Error with allocation");

        return -1;
    }
    int *secondNumberBinaryRepresentation = (int*)malloc(31 * sizeof(int));
    if (secondNumberBinaryRepresentation == NULL) {
        printf("Error with allocation");

        return -1;
    }

    representTheNumberInBinary(firstNumber, firstNumberBinaryRepresentation);
    for (int i = 0; i < 31; i++) {
        printf("%d", firstNumberBinaryRepresentation[i]);
    }
    free(firstNumberBinaryRepresentation);
    free(secondNumberBinaryRepresentation);
    return 0;
}