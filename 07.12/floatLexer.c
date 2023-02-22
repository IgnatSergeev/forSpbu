#include <stdio.h>
#include <stdbool.h>

#define MAX_STRING_SIZE 100

typedef enum State {
    start,
    fail,
    integerState,
    startOfFloatState,
    floatState,
    eState,
    eSignState,
    eDegreeState
} State;

bool isDigit(char currentChar) {
    return currentChar >= '0' && currentChar <= '9';
}

bool wheatherTheFloatIsCorrect(char string[]) {
    State state = start;
    bool endCondition = false;
    for (int i = 0; i < MAX_STRING_SIZE; i++) {
        if (string[i] == '\0') {
            break;
        }
        char currentChar = string[i];
        switch (state) {
            case start: {
                if (isDigit(currentChar)) {
                    state = integerState;
                } else {
                    state = fail;
                }
                break;
            } case integerState: {
                if (isDigit(currentChar)) {
                    continue;
                }
                if (currentChar == 'E') {
                    state = eState;
                    continue;
                }
                if (currentChar == '.') {
                    state = startOfFloatState;
                } else {
                    state = fail;
                }
                break;
            } case startOfFloatState: {
                if (isDigit(currentChar)) {
                    state = floatState;
                } else {
                    state = fail;
                }
                break;
            } case floatState: {
                if (isDigit(currentChar)) {
                    continue;
                }
                if (currentChar == 'E') {
                    state = eState;
                } else {
                    state = fail;
                }
                break;
            } case eState: {
                if (currentChar == '+' || currentChar == '-') {
                    state = eSignState;
                    continue;
                }
                if (isDigit(currentChar)) {
                    state = eDegreeState;
                } else {
                    state = fail;
                }
                break;
            } case eSignState: {
                if (isDigit(currentChar)) {
                    state = eDegreeState;
                } else {
                    state = fail;
                }
                break;
            } case eDegreeState: {
                if (isDigit(currentChar)) {
                    continue;
                }

                state = fail;
                break;
            } default: {
                endCondition = true;
            }
        }
        if (endCondition) {
            break;
        }
    }

    return state == integerState || state == floatState || state == eDegreeState;
}

bool test(void) {
    bool testResult = true;
    char string[MAX_STRING_SIZE] = "123.657E-223";

    testResult = wheatherTheFloatIsCorrect(string);

    return testResult;
}

int main(void) {
    if (!test()) {
        printf("Тесты провалены\n");
        return -1;
    }
    printf("Тесты пройдены\n");

    printf("Введите последовательность символов, которую хотите проверить на принадлежность регулярному выражению\n");
    char string[MAX_STRING_SIZE] = {0};
    scanf("%s", string);

    bool isCorrectFloat = wheatherTheFloatIsCorrect(string);

    if (isCorrectFloat) {
        printf("Данная последовательность принадлежит регулярному выражению\n");
    } else {
        printf("Данная последовательность НЕ принадлежит регулярному выражению\n");
    }
    return 0;
}