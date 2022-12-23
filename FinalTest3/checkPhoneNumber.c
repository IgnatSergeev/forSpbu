#include <stdio.h>
#include <stdbool.h>

#define MAX_STRING_SIZE 100

typedef enum State {
    start,
    fail,
    sign,
    countryCode,
    openingBracket,
    regionCode,
    closingBracket,
    firstEndDigits,
    firstDash,
    secondEndDigits,
    secondDash,
    thirdEndDigits
} State;

bool isDigit(char currentChar) {
    return currentChar >= '0' && currentChar <= '9';
}

bool isTheStringPhoneNumber(char string[]) {
    State state = start;
    for (int i = 0; i < MAX_STRING_SIZE; i++) {
        if (string[i] == '\0') {
            break;
        }
        char currentChar = string[i];
        switch (state) {
            case start: {
                if (currentChar == '+') {
                    state = sign;
                } else {
                    state = fail;
                }
                break;
            } case sign: {
                if (isDigit(currentChar)) {
                    state = countryCode;
                } else {
                    state = fail;
                }
                break;
            } case countryCode: {
                if (isDigit(currentChar)) {
                    continue;
                }
                if (currentChar == '(') {
                    state = openingBracket;
                } else {
                    state = fail;
                }
                break;
            } case openingBracket: {
                if (isDigit(currentChar)) {
                    state = regionCode;
                } else {
                    state = fail;
                }
                break;
            } case regionCode: {
                if (isDigit(currentChar)) {
                    continue;
                }
                if (currentChar == ')') {
                    state = closingBracket;
                } else {
                    state = fail;
                }
                break;
            } case closingBracket: {
                if (isDigit(currentChar)) {
                    state = firstEndDigits;
                } else {
                    state = fail;
                }
                break;
            } case firstEndDigits: {
                if (isDigit(currentChar)) {
                    continue;
                }
                if (currentChar == '-') {
                    state = firstDash;
                } else {
                    state = fail;
                }
                break;
            } case firstDash: {
                if (isDigit(currentChar)) {
                    state = secondEndDigits;
                } else {
                    state = fail;
                }
                break;
            } case secondEndDigits: {
                if (isDigit(currentChar)) {
                    continue;
                }
                if (currentChar == '-') {
                    state = secondDash;
                } else {
                    state = fail;
                }
                break;
            } case secondDash: {
                if (isDigit(currentChar)) {
                    state = thirdEndDigits;
                } else {
                    state = fail;
                }
                break;
            } case thirdEndDigits: {
                if (isDigit(currentChar)) {
                    continue;
                }
                state = fail;
                break;
            } default: {
                state = fail;
            }
        }
    }
    return state == thirdEndDigits;
}

bool test(void) {
    char testString[MAX_STRING_SIZE] = "+123(2)777-2-99999";
    bool testResult = isTheStringPhoneNumber(testString);

    return testResult;
}
int main(void) {
    if (!test()) {
        printf("Тесты провалены\n");
        return -1;
    }
    printf("Тесты пройдены\n");

    //Regular expression: +(digit+)('(')(digit+)(')')(digit+)-(digit+)-(digit+)
    //digit = [0..9]

    printf("Введите строку, о которой хотите узнать является ли она корректным номером телефона\n");
    char string[MAX_STRING_SIZE] = {0};
    scanf("%s", string);

    bool isPhoneNumber = isTheStringPhoneNumber(string);
    if (isPhoneNumber) {
        printf("Она является корректным номером телефона\n");
    } else {
        printf("Она НЕ является корректным номером телефона\n");
    }

    return 0;
}