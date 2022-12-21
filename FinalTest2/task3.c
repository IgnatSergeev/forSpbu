#include <stdio.h>
#include <stdbool.h>

#define MAX_INPUT_STRING_SIZE 100
#define STRING_SIZE (MAX_INPUT_STRING_SIZE + 1)

enum State {
    start,
    firstState,
    secondState,
    thirdState,
    fourthState,
    fifthState,
    fail,
    success
} State;

bool isCapitalLetter(char inputChar) {
    if (inputChar >= 65 && inputChar <= 90) {
        return true;
    }
    return false;
}

bool isDigit(char inputChar) {
    if (inputChar >= 48 && inputChar <= 57) {
        return true;
    }
    return false;
}

bool isStringRepresentedAsARegularExpression(char string[]) {
    enum State state = start;
    char currentChar = '\0';
    for (int i = 0; i < STRING_SIZE; i++) {
        currentChar = string[i];
        switch (state) {
            case start: {
                if (isCapitalLetter(currentChar) || isDigit(currentChar) || currentChar == '.' || currentChar == '_' || currentChar == '%' || currentChar == '+' || currentChar == '-') {
                    state = firstState;
                } else {
                    state = fail;
                }
                continue;
            } case firstState: {
                if (isCapitalLetter(currentChar) || isDigit(currentChar) || currentChar == '.' || currentChar == '_' || currentChar == '%' || currentChar == '+' || currentChar == '-') {
                    continue;
                }
                if (currentChar == '@') {
                    state = secondState;
                } else {
                    state = fail;
                }
                continue;
            } case secondState: {
                if (isCapitalLetter(currentChar) || isDigit(currentChar) || currentChar == '-') {
                    state = thirdState;
                } else {
                    state = fail;
                }
                continue;
            } case thirdState: {
                if (isCapitalLetter(currentChar) || isDigit(currentChar) || currentChar == '-') {
                    continue;
                }
                if (currentChar == '.') {
                    state = fourthState;
                } else {
                    state = fail;
                }
                continue;
            } case fourthState: {
                if (isDigit(currentChar) || currentChar == '-') {
                    state = thirdState;
                    continue;
                }
                if (isCapitalLetter(currentChar)) {
                    state = fifthState;
                } else {
                    state = fail;
                }
                continue;
            } case fifthState: {
                if (currentChar == '.') {
                    state = fourthState;
                    continue;
                }
                if (isCapitalLetter(currentChar) || isDigit(currentChar) || currentChar == '-') {
                    state = thirdState;
                    continue;
                }
                if (currentChar == '\n') {
                    state = success;
                } else {
                    state = fail;
                }
                continue;
            } default: {
                break;//можно было и без него, но выглядит некрасиво без кода внутри скобок
            }
        }
        break;
    }

    if (state == success) {
        return true;
    }
    return false;
}

bool test(void) {
    char testString[STRING_SIZE] = "AB0Z3.B%_1@A.1B.A\n";

    bool testResult = isStringRepresentedAsARegularExpression(testString);

    return testResult;
}

int main(void) {
    if (!test()) {
        printf("Тесты провалены\n");
        return -1;
    }
    printf("Тесты пройдены\n");

    printf("Введите строку, которую хотите проверить на принадлежность регулярному выражению\n");
    char string[STRING_SIZE] = {0};
    scanf("%s", string);
    for (int i = 0; i < STRING_SIZE; i++) {
        if (string[i] == '\0') {
            string[i] = '\n';
            break;
        }
    }

    bool result = isStringRepresentedAsARegularExpression(string);

    if (result) {
        printf("Введённая строка является представителем данного регулярного выражения\n");
    } else {
        printf("Введённая строка НЕ является представителем данного регулярного выражения\n");
    }

    return 0;
}