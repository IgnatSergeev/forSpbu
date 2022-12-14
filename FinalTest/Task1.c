#include <stdbool.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>

char *translateNumberFromBinaryToDecimal(char *string) {
    int number = 0;
    for (int i = 0; i < 32; i++) {
        if (string[i] == '\0') {
            break;//1001
        }
        if (string[i] == '0') {
            number *= 2;
            continue;
        }
        if (string[i] == '1'){
            number *= 2;
            number += 1;
            continue;
        }
        return NULL;
    }

    int array[14] = {0};
    int currentArrayIndex = 13;
    while (number != 0) {
        int currentDigit = number % 10;
        array[currentArrayIndex] = currentDigit;
        --currentArrayIndex;
        number /= 10;
    }

    int arrayStartIndex = -1;
    int arrayEndIndex = 13;
    for (int i = 0; i < 14; i++) {
        if (array[i] != 0) {
            arrayStartIndex = i;
            break;
        }
    }

    char *decimalOutString = calloc(15, sizeof(char));
    if (arrayStartIndex == -1) {
        decimalOutString[0] = '0';
        return decimalOutString;
    }

    int decimalStringIndex = 0;
    for (int i = arrayStartIndex; i <= arrayEndIndex; i++) {
        decimalOutString[decimalStringIndex] = (char)(48 + array[i]);
        ++decimalStringIndex;
    }

    return decimalOutString;
}

bool test() {
    bool testResult = true;
    char *decimalTestString = translateNumberFromBinaryToDecimal("10011");
    if (decimalTestString[0] != '1' || decimalTestString[1] != '9') {
        testResult = false;
    }
    for (int i = 2; i < 15; i++) {
        if (decimalTestString[i] != '\0') {
            testResult = false;
            break;
        }
    }

    free(decimalTestString);
    return testResult;
}

int main() {
    if (!test()) {
        printf("Тесты провалены\n");
        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    printf("Введите бинарное представление числа\n");
    char string[40] = {0};
    scanf("%s", string);
    char *decimalString = translateNumberFromBinaryToDecimal(string);
    if (decimalString == NULL) {
        printf("Встречен неизвестный символ\n");
        free(decimalString);
        return -1;
    }
    printf("Вот это же число, но в десятичной системе счисления: %s", decimalString);

    free(decimalString);
    return 0;
}